namespace DataWarehouse
{
    using System.Runtime.Serialization;

    [DataContract]
    public class Response
    {
        [DataMember]
        public string RequestStatus
        {
            get
            {
                return _status.ToString();
            }
            private set { }
        }

        [DataMember]
        public string Message { get; private set; }

        [DataMember]
        public string Body { get; private set; }

        public static Response CreateResponse<T>(Status status, string message, T result)
        {
            var response = new Response { _status = status, Message = message, Body = SerializationHelper.Serialize(result) };
            return response;
        }

        private Status _status;
    }

    public enum Status
    {
        OK,
        Failed,
    }
}