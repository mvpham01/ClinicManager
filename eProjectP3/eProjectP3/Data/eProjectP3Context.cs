using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace eProjectP3.Data
{
    public class eProjectP3Context : DbContext
    {
        // You can add custom code to this file. Changes will not be overwritten.
        // 
        // If you want Entity Framework to drop and regenerate your database
        // automatically whenever you change your model schema, please use data migrations.
        // For more information refer to the documentation:
        // http://msdn.microsoft.com/en-us/data/jj591621.aspx
    
        public eProjectP3Context() : base("name=eProjectP3Context")
        {
        }

        public DbSet<eProjectP3.Models.medical_type> medical_type { get; set; }
        public DbSet<eProjectP3.Models.client> clients { get; set; }
        public DbSet<eProjectP3.Models.feedback> feedbacks { get; set; }
        public DbSet<eProjectP3.Models.online_purchase> online_purchases { get; set; }
        public DbSet<eProjectP3.Models.online_purchase_detail> online_purchase_details { get; set; }
        public DbSet<eProjectP3.Models.medical_application> medical_application { get; set; }
        public DbSet<eProjectP3.Models.scientific_application> scientific_application { get; set; }
        public DbSet<eProjectP3.Models.staff> staffs { get; set; }

        public System.Data.Entity.DbSet<eProjectP3.Models.education_application> education_application { get; set; }

        public System.Data.Entity.DbSet<eProjectP3.Models.business_application> business_application { get; set; }
    }
}
