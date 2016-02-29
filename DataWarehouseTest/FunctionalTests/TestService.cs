namespace DataWarehouseTest.FunctionalTests
{
    using System;
    using System.Linq;
    using System.Collections.Generic;
    using System.Net;
    using System.Runtime.Serialization.Json;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using DataWarehouse;

    using DataWarehouseTest.OrderServiceReference;

    /// <summary>
    /// Summary description for TestGetItems
    /// </summary>
    [TestClass]
    public class TestService
    {
        [TestInitialize]
        public void InitializeTests()
        {
            //_client = new OrderServiceClient();
        }

        [TestMethod]
        public void TestValidGetItems()
        {
            // Repository should never be empty
            var response = MakeRequest(TestUtils.getItemsUrl); 
            var deserializedResponse  = TestUtils.ValidateResponse<IEnumerable<KeyValuePair<string, Item>>>(response, Status.OK, ResponseMessage.Success);
            Assert.IsTrue(deserializedResponse.Any());
        }

        [TestMethod]
        public void TestValidPurchaseItems()
        {
            // Choose an item from the repository
            var getItemsResponse = _client.GetItems();
            var items = TestUtils.ValidateResponse<IEnumerable<KeyValuePair<string, Item>>>(getItemsResponse, Status.OK, ResponseMessage.Success);
            Assert.IsTrue(items.Any());
            var item = items.First();
          
            // Purchase should succeed
            var response = _client.PurchaseItem(item.Key);
            TestUtils.ValidateResponse<Item>(response, Status.OK, ResponseMessage.Success);
        }

        [TestMethod]
        public void TestInvalidPurchaseItems()
        {
            // Purchase should failed for an invalid guid
            var response = _client.PurchaseItem("SomeInvalidValue");
            TestUtils.ValidateResponse<Item>(response, Status.Failed, ResponseMessage.OutOfItem);

            // Purchase should fail with null id value
            response = _client.PurchaseItem(null);
            TestUtils.ValidateResponse<Item>(response, Status.Failed, ResponseMessage.InvalidId);

            // Purchase should fail with empty id value
            response = _client.PurchaseItem(string.Empty);
            TestUtils.ValidateResponse<Item>(response, Status.Failed, ResponseMessage.InvalidId);

            // Purchase should fail with whitespace id value
            response = _client.PurchaseItem("   ");
            TestUtils.ValidateResponse<Item>(response, Status.Failed, ResponseMessage.InvalidId);
        }

        public static Response MakeRequest(string requestUrl)
        {
            try
            {
                HttpWebRequest request = WebRequest.Create(requestUrl) as HttpWebRequest;
                using (HttpWebResponse response = request.GetResponse() as HttpWebResponse)
                {
                    if (response.StatusCode != HttpStatusCode.OK)
                        throw new Exception(
                            String.Format("Server error (HTTP {0}: {1}).", response.StatusCode, response.StatusDescription));
                    DataContractJsonSerializer jsonSerializer = new DataContractJsonSerializer(typeof(Response));
                    object objResponse = jsonSerializer.ReadObject(response.GetResponseStream());
                    Response jsonResponse = objResponse as Response;
                    return jsonResponse;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return null;
            }
        }

        private OrderServiceClient _client;
    }
}
