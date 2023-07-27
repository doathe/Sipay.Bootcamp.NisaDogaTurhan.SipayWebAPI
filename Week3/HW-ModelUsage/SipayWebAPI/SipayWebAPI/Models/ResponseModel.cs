namespace SipayData.Models;

// Response Model defined
public class ResponseModel<T>
{
    public ResponseModel(T Data)
    {
        this.StatusCode = 200;
        this.Message = "OK";
        this.Data = Data;

    }

    public T Data { get; set; }
    public int StatusCode { get; set; }
    public string Message { get; set; }
    
}
