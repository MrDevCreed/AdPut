namespace AppService.Dto.Responses.Common
{

    public class ResponseBase<TData>
    {
        public StatusCode StatusCode { get; set; }

        public string Message { get; set; }

        public bool IsSuccses => (int)StatusCode >= 200 && (int)StatusCode <= 299;

        public TData Data { get; set; }
    }
}
