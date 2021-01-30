using System;
using System.ComponentModel.DataAnnotations;

namespace Library.MsSqlDataAccess.Entity
{
    public class InMail
    {
        [Key]
        public int MailId { get; set; }
        public int ReceiverCompanyId { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }
        public string SenderIdentifier { get; set; }
        public int State { get; set; }
        public DateTime InsertAt { get; set; }
    }
}
