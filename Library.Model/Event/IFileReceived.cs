namespace Library.Model.Event
{
    public interface IFileReceived
    {
        string CompanyId { get; set; }
        string FileID { get; set; }
    }
}
