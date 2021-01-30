using System;
using System.ComponentModel.DataAnnotations;

namespace Library.MsSqlDataAccess.Entity
{
    public class OutMail
    {
        [Key]
        public int MailId { get; set; }
        public int CompanyId { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }
        public string ReceiverIdentifier { get; set; }
        public int State { get; set; }
        public DateTime InsertAt { get; set; }
    }
}
