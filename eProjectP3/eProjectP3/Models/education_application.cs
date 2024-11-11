namespace eProjectP3.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class education_application
    {
        [Key]
        public int Education_ID { get; set; }

        [StringLength(255)]
        public string Subject { get; set; }

        [StringLength(50)]
        public string Type { get; set; }

        public int? Staff_ID { get; set; }

        [Column(TypeName = "date")]
        public DateTime? Date { get; set; }

        public TimeSpan? Time { get; set; }

        public virtual staff staff { get; set; }
    }
}
