using System;
using System.ComponentModel.DataAnnotations;

namespace Library.MsSqlDataAccess.Entity
{
    public class CompanyAccount
    {
        [Key]
        public int CompanyId { get; set; }
        public string Title { get; set; }
        public string Identifier { get; set; }
        public DateTime InsertAt { get; set; }
    }
}
