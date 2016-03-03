namespace DataWarehouse
{
    using System.ServiceModel;
    using System.ServiceModel.Web;

    [ServiceContract]
    public interface IOrderService
    {
        /// <summary>
        /// Method executed when all the items are requested.
        /// </summary>
        /// <returns>Response for the request</returns>
        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Wrapped, UriTemplate = "getitems")]
        Response GetItems();

        /// <summary>
        /// Purchase the item specified by the id. 
        /// </summary>
        /// <param name="id">Id of the item to be purchased</param>
        /// <returns>Response for the purchase item request</returns>
        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Wrapped, UriTemplate = "purchaseitem/{id}")]
        Response PurchaseItem(string id);

        // TODO: Add more your service operations here
    }
}
