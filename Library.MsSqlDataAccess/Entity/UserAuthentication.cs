using System;
using System.ComponentModel.DataAnnotations;

namespace Library.MsSqlDataAccess.Entity
{
    public class UserAuthentication
    {
        [Key]
        public string AuthKey { get; set; }
        public int UserId { get; set; }
        public DateTime ExpireAt { get; set; } 
        public DateTime InsertAt { get; set; }
    }
}
