namespace DataWarehouse
{
    using System.Runtime.Serialization;
    using System.ServiceModel;
    using System.ServiceModel.Web;

    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IOrderService" in both code and config file together.
    [ServiceContract]
    public interface IOrderService
    {
        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Wrapped, UriTemplate = "getitems")]
        Response GetItems();

        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Wrapped, UriTemplate = "purchaseitem/{id}")]
        Response PurchaseItem(string id);

        // TODO: Add your service operations here
    }
}
