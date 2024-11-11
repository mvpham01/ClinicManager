namespace eProjectP3.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("feedback")]
    public partial class feedback
    {
        [Key]
        public int Feedback_ID { get; set; }

        public int? Client_ID { get; set; }

        [Column(TypeName = "text")]
        public string Feedback_Text { get; set; }

        [Column(TypeName = "date")]
        public DateTime? Date { get; set; }

        public virtual client client { get; set; }
    }
}
