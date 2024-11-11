namespace eProjectP3.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class business_application
    {
        [Key]
        public int Transaction_ID { get; set; }

        [StringLength(50)]
        public string Sector { get; set; }

        public decimal? Amount { get; set; }

        [Column(TypeName = "date")]
        public DateTime? Date { get; set; }
    }
}
