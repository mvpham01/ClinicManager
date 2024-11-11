namespace eProjectP3.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class medical_application
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public medical_application()
        {
            online_purchase = new HashSet<online_purchase>();
        }

        [Key]
        public int Medicine_ID { get; set; }

		public int? Medicine_Type_ID { get; set; }

		[StringLength(50)]
        public string Img { get; set; }

		public decimal? Cost { get; set; }

		[StringLength(50)]
        public string Code { get; set; }

		[StringLength(255)]
        public string Name { get; set; }

        public virtual medical_type medical_type { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<online_purchase> online_purchase { get; set; }
    }
}
