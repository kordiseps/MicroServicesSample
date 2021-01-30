namespace Library.Model.Event
{
    public interface IMailProcess2Finished
    {
        string CompanyId { get; set; }
        string MailID { get; set; }
    }
}
