﻿namespace DataWarehouse
{
    using System; 

    public class OrderService : IOrderService
    {
        /// <summary>
        /// Implementation of the GetItems method to return all items in the repository
        /// </summary>
        /// <returns>Response for the requested operation</returns>
        public Response GetItems()
        {
            return Response.CreateResponse(Status.OK, ResponseMessage.Success, Database.GetAllItems());
        }

        /// <summary>
        /// Implementation for purchasing an item specified by the id.
        /// </summary>
        /// <param name="id">Id of the item to be purchased</param>
        /// <returns>Response for the requested operation</returns>
        public Response PurchaseItem(string id)
        {
            string errorMessage = string.Empty;
            Response response;
            Item purchasedItem;
            if ((purchasedItem = PurchaseItem(id, out errorMessage)) != null)
            {
                response = Response.CreateResponse(Status.OK, ResponseMessage.Success, purchasedItem);
            }
            else
            {
                // Capture the errorMessage if thats properly set.  If not, return unhandled error.
                response = Response.CreateResponse<Item>(Status.Failed, 
                    string.IsNullOrWhiteSpace(errorMessage) ? ResponseMessage.UnhandledError : errorMessage, null);
            }

            return response;
        }

        /// <summary>
        /// Purchase an Item specified by id.
        /// </summary>
        /// <param name="id">Id of the item to be purchased</param>
        /// <param name="errorMessage">ErrorMessage if any</param>
        /// <returns>Item purchased</returns>
        public static Item PurchaseItem(string id, out string errorMessage)
        {
            errorMessage = string.Empty;
            Item purchasedItem = null;
            
            // Validate Id before proceeding.
            // Since the id is validated not to be null, the trygetvalue and tryremove will never throw exceptions. 
            if (string.IsNullOrWhiteSpace(id))
            {
                errorMessage = ResponseMessage.InvalidId;
                return purchasedItem;
            }
            
            // Check the repository to see if the item is available.
            // Its possible that by the time the user places the order, some of them get sold out.
            if (Database.TryGetValue(id, out purchasedItem))
            {
                // Key was found. Hence call the purchaseItem. This does an Interlocked.Decrement. 
                // The decrement will return a unique value to each thread here. 
                int itemCount = purchasedItem.DecrementCount();

                // If item count becomes 0, then remove this item from the repository. Its no longer available. 
                RemoveItemFromRepositoryIfNeeded(id, itemCount);

                if (itemCount < 0)
                {
                    // it has already been sold out. 
                    purchasedItem = null;
                    errorMessage = ResponseMessage.OutOfItem;
                }
            }
            else
            {
                // Repository is out of the item which the user is trying to buy. 
                errorMessage = ResponseMessage.OutOfItem;
            }

            return purchasedItem;
        }

        /// <summary>
        /// If the itemcount becomes 0, then clean the repository and remove the item.
        /// </summary>
        /// <param name="id">Identifier of the item</param>
        /// <param name="itemCount">Item count in the repository</param>
        private static void RemoveItemFromRepositoryIfNeeded(string id, int itemCount)
        {
            // Last item has been purchased.  Clean the repository to remove this item.
            if (itemCount == 0)
            {
                Item removeItem;
                if (!Database.TryRemove(id, out removeItem))
                {
                    // Should never happen because only one thread should be removing it at a time. 
                    throw new ApplicationException("Failed to clean the repository!");
                }
            }
        }
        // End of RemoveItemFromRespository
    }
}
