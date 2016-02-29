using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DataWarehouseTest
{
    using System.Collections;
    using System.Collections.Concurrent;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading;

    using DataWarehouse;

    [TestClass]
    public class OrderServiceTest
    {
        [TestInitialize]
        public void InitializeTests()
        {
            _service = new OrderService();
        }

        [TestMethod]
        public void TestGetItemsWithValidItem()
        {
            var response  = _service.GetItems();

            IEnumerable<KeyValuePair<string, Item>> result = TestUtils.ValidateResponse <IEnumerable<KeyValuePair<string, Item>>>(response, Status.OK, ResponseMessage.Success);
            var resultCount = result.Count();

            // Add new item and verify count
            Database.AddItem("DummyItemName", "DummyItemDescription", 100, 10);

            var newResponse = _service.GetItems();
            IEnumerable<KeyValuePair<string, Item>> newResult = TestUtils.ValidateResponse<IEnumerable<KeyValuePair<string, Item>>>(newResponse, Status.OK, ResponseMessage.Success);
            var newResultCount = newResult.Count();
            
            // Assert the new Count accounts for the additional item.
            Assert.AreEqual(resultCount + 1, newResultCount);
        }

        [TestMethod]
        public void TestGetItemsWithBadAddItem()
        {
            var response = _service.GetItems();

            IEnumerable<KeyValuePair<string, Item>> result = TestUtils.ValidateResponse<IEnumerable<KeyValuePair<string, Item>>>(response, Status.OK, ResponseMessage.Success);
            var resultCount = result.Count();

            // Verify that add new item throws exception when 0 count is added.
            try
            {
                Database.AddItem("DummyItemName", "DummyItemDescription", 100, 0);
            }
            catch (ApplicationException ae)
            {
                Assert.AreEqual("Cannot add an item with 0 initial count", ae.Message);
            }

            var newResponse = _service.GetItems();
            IEnumerable<KeyValuePair<string, Item>> newResult = TestUtils.ValidateResponse<IEnumerable<KeyValuePair<string, Item>>>(newResponse, Status.OK, ResponseMessage.Success);
            var newResultCount = newResult.Count();

            // Assert the new Count accounts for the additional item.
            Assert.AreEqual(resultCount, newResultCount);
        }

        [TestMethod]
        public void TestPurchaseItems()
        {
            // Add an item to the repository
            KeyValuePair<string, Item> record = Database.AddItem("DummyItemName", "DummyItemDescription", 100, 1);

            // Ensure that after purchase the count is reduced by 1
            var purchaseResponse  = _service.PurchaseItem(record.Key);
            TestUtils.ValidateResponse<Item>(purchaseResponse, Status.OK, ResponseMessage.Success);
            
            // Purchase the item again and you should get no item found message
            purchaseResponse = _service.PurchaseItem(record.Key);
            TestUtils.ValidateResponse<Item>(purchaseResponse, Status.Failed, ResponseMessage.OutOfItem, resultIsNull: true);
        }


        [TestMethod]
        public void TestMultiThreadedPurchaseItems()
        {
            // Add an item to the repository with item count 10
            KeyValuePair<string, Item> record = Database.AddItem("DummyItemName", "DummyItemDescription", 100, 10);
            
            // Number of requests to be made to purchase this item. 5 should fail because only 10 are available.
            const int numberOfRequests = 15; 
            ConcurrentQueue<Response> responses = new ConcurrentQueue<Response>();

            ExecuteParallelRequests(numberOfRequests, record, responses);

            // Examine the responses from all threads. There should be exactly 15 responses
            Assert.AreEqual(numberOfRequests, responses.Count());

            int failedRequests = 0, successRequests = 0; 
            foreach (var response in responses)
            {
                Assert.IsNotNull(response);
                Assert.IsFalse(string.IsNullOrWhiteSpace(response.RequestStatus));

                // 10 requests should succeed and 5 should fail because only 10 items were available.
                if (response.RequestStatus.Equals(Status.OK.ToString()))
                {
                    successRequests++;
                }
                else if (response.RequestStatus.Equals(Status.Failed.ToString()))
                {
                    failedRequests++;
                }
                else
                {
                    //TODO: Handle more statuses in future.
                }
            }

            Assert.AreEqual(5, failedRequests);
            Assert.AreEqual(10, successRequests);

            // Ensure repository does not have the item. 
            var items = _service.GetItems();
            IEnumerable<KeyValuePair<string, Item>> itemsResult = TestUtils.ValidateResponse<IEnumerable<KeyValuePair<string, Item>>>(items, Status.OK, ResponseMessage.Success);

            // Repository should not contain the item with the key added
            Assert.IsFalse(itemsResult.Any(ir => ir.Key == record.Key));
        }

        private void ExecuteParallelRequests(int numberOfRequests, KeyValuePair<string, Item> record, ConcurrentQueue<Response> responses)
        {
            Thread[] allThreads = new Thread[numberOfRequests];

            for (int index = 0; index < allThreads.Length; index++)
            {
                // Create threads to purchase items and save the response in ConcurrentQueue
                allThreads[index] = new Thread(() => PurchaseItem(record.Key, responses));
            }

            // Kickstart all threads
            foreach (var thread in allThreads)
            {
                thread.Start();
            }

            // Wait for all threads to finish
            foreach (var thread in allThreads)
            {
                thread.Join();
            }
        }

        private void PurchaseItem(string id, ConcurrentQueue<Response> responses)
        {
            var purchaseResponse = _service.PurchaseItem(id);
            responses.Enqueue(purchaseResponse);
        }

        private OrderService _service;
    }

}
