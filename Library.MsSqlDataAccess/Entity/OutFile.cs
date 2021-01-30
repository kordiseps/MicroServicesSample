using System;
using System.ComponentModel.DataAnnotations;

namespace Library.MsSqlDataAccess.Entity
{
    public class OutFile
    {
        [Key]
        public string OutFileId { get; set; }
        public int CompanyId { get; set; }
        public string DocumentName { get; set; }
        public string ReceiverIdentifier { get; set; }
        public int State { get; set; }
        public DateTime InsertAt { get; set; }
    }
}
