namespace Library.Model.Event
{
    public interface IMailReceived
    {
        string CompanyId { get; set; }
        string MailID { get; set; }
    }
}
