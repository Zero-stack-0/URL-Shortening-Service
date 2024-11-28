namespace Service.Dto
{
    public class ResponseModel
    {
        public object? Data { get; set; }
        public string Message { get; set; }
        public int StatusCode { get; set; }
    }
}