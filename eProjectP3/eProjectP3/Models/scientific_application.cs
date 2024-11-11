namespace eProjectP3.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class scientific_application
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public scientific_application()
        {
            online_purchase = new HashSet<online_purchase>();
        }

        [Key]
        public int Apparatus_ID { get; set; }

        [StringLength(50)]
        public string Img { get; set; }

        public decimal? Cost { get; set; }

        [Column(TypeName = "text")]
        public string Description { get; set; }

        [StringLength(255)]
        public string Name { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<online_purchase> online_purchase { get; set; }
    }
}
