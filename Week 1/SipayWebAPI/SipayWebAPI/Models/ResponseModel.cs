namespace SipayWebAPI.Models
{
    // Response Model created.
    public class ResponseModel<T>
    {
        public ResponseModel(T Data)
        {
            this.StatusCode = 200;
            this.Message = "OK";
            this.Data = Data;

        }

        public int StatusCode { get; set; }
        public string Message { get; set; }
        public T Data { get; set; }
    }
}
