namespace Library.Model.Event
{
    public interface IMailProcess1Finished
    {
        string CompanyId { get; set; }
        string MailID { get; set; }
    }
}
