using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace eProjectP3.Models
{
    public class online_purchase_detail
    {
        [Key]
        public int Purchase_Detail_ID { get; set; }

        public int Purchase_ID { get; set; }

        [ForeignKey("Purchase_ID")] // Add foreign key attribute
        public virtual online_purchase OnlinePurchase { get; set; }

        public int? Item_ID { get; set; }
        [StringLength(50)]
        public string Item_Type { get; set; }
        public int? Quantity { get; set; }

        [ForeignKey("Item_ID")]
        public virtual medical_application MedicalApplication { get; set; }

        [ForeignKey("Item_ID")]
        public virtual scientific_application ScientificApplication { get; set; }
    }
}