namespace eProjectP3.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class online_purchase
    {
        [Key]
        public int Purchase_ID { get; set; }

        public int Client_ID { get; set; }
        public decimal Total_Amount { get; set; }

        [Column(TypeName = "date")]
        public DateTime? Purchase_Date { get; set; }

        [ForeignKey("Client_ID")]
        public virtual client Client { get; set; }

        // Navigation property for related details
        public virtual ICollection<online_purchase_detail> PurchaseDetails { get; set; }
    }

}
