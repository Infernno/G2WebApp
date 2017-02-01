namespace G2WebApp.Core.Util
{
    public class OperationResult
    {
        public bool Success { get; set; }
        public object Data { get; set; }
        public string Message { get; set; }

        private OperationResult() { }

        public static OperationResult Successful(object Data = null, string Message = null)
        {
            return new OperationResult
            {
                Success = true,
                Data = Data
            };
        }

        public static OperationResult Failed(object Data = null, string Message = null)
        {
            return new OperationResult
            {
                Success = false,
                Data = Data,
                Message = Message
            };
        }
    }
}
