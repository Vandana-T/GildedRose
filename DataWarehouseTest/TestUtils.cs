namespace DataWarehouseTest
{
    using DataWarehouse;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    class TestUtils
    {
        public static T ValidateResponse<T>(Response response, Status expectedStatus, string expectedMessage, bool resultIsNull = false)
        {
            Assert.IsNotNull(response);
            Assert.AreEqual(expectedStatus.ToString(), response.RequestStatus);
            Assert.AreEqual(expectedMessage, response.Message);
            Assert.IsFalse(string.IsNullOrWhiteSpace(response.Body));

            var result = SerializationHelper.Deserialize<T>(response.Body);
            if (resultIsNull)
            {
                Assert.IsNull(result);
            }
            else
            {
                Assert.IsNotNull(result);
            }

            return result;
        }

        public const string BaseUrl = "http://localhost:8080/DataWarehouse";

        public static readonly string getItemsUrl = string.Concat(BaseUrl, "/OrderService.svc/getitems");
    }
}
