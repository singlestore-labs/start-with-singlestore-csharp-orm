using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace SingleStoreORM.Models
{
    [Table("messages")]
	public class Message
    {
        public long Id { get; set; }
        public string Content { get; set; }
        public DateTime CreateDate { get; set; }
    }
}
