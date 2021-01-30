namespace Library.Model.Api
{
    public class GetAuthKeyResponse : BaseResponse
    {
        public string AuthKey { get; set; }
        public string ExpireAt { get; set; }
    }
}
