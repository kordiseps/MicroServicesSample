using System;
using System.ComponentModel.DataAnnotations;

namespace Library.MsSqlDataAccess.Entity
{
    public class InFile
    {
        [Key]
        public string InFileId { get; set; }
        public int ReceiverCompanyId { get; set; }
        public string DocumentName { get; set; }
        public string SenderIdentifier { get; set; }
        public int State { get; set; }
        public DateTime InsertAt { get; set; }
    }
}
