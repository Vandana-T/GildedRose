namespace DataWarehouse
{
    /// <summary>
    /// Response Message enum to indicate the status of the request
    /// </summary>
    public static class ResponseMessage
    {
        public const string Success = "Succeeded";

        public const string OutOfItem = "Sorry, the requested item is no longer available!";

        public const string InvalidId = "Invalid Item Id";

        public const string UnhandledError = "Unknown error occurred";
    }
}