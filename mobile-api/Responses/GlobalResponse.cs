namespace mobile_api.Responses
{
    public class GlobalResponse: HttpResponseMessage
    {
        public int StatusCode { get; set; }
        public string Message { get; set; }
        public object Data { get; set; }
    }
}
