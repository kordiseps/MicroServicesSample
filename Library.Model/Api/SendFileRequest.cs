namespace Library.Model.Api
{
    public class SendFileRequest
    {
        public string AuthKey { get; set; }
        public string DocumentName { get; set; }
        public string FileBase64Content { get; set; }
        public string ReceiverIdentifier { get; set; }
    }
}
