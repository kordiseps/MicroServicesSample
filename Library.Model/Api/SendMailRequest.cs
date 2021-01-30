namespace Library.Model.Api
{
    public class SendMailRequest
    {
        public string AuthKey { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }
        public string ReceiverIdentifier { get; set; }
    }
}
