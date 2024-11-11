namespace eProjectP3.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("staff")]
    public partial class staff
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public staff()
        {
            education_application = new HashSet<education_application>();
        }

        [Key]
        public int Staff_ID { get; set; }

        [StringLength(255)]
        public string Name { get; set; }

		[StringLength(255)]
        public string Position { get; set; }

		[StringLength(20)]
        public string Contact_Number { get; set; }

		[StringLength(255)]
        public string Email { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<education_application> education_application { get; set; }
    }
}
