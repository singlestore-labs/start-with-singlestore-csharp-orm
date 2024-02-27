using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace SingleStoreORM.Models
{
    [Table("messages")]
	public class Message
    {
        public long Id { get; set; }
        public string Content { get; set; }

        // We manually set the default value for this field because SingleStore's zero-value for DateTime type exceeds
        // the default value for DateTime in .NET. Beginning with SingleStore 8.0, the global sync variable data_conversion_compatibility_level
        // controls the way certain data conversions are performed. Consequently, SingleStore refrains from converting out-of-range timestamps
        // unless you explicitly modify the data_conversion_compatibility_level value (https://docs.singlestore.com/cloud/reference/sql-reference/data-types/data-type-conversion/)
        public DateTime CreateDate { get; set; } = new DateTime(1001, 1, 1, 0, 0, 0, 0);
    }
}
