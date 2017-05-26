//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Data.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Products
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Products()
        {
            this.Favorites = new HashSet<Favorites>();
            this.Sales = new HashSet<Sales>();
        }
    
        public int ProductID { get; set; }
        public string Name { get; set; }
        public string Price { get; set; }
        public Nullable<System.DateTime> DateAdded { get; set; }
        public Nullable<bool> Status { get; set; }
        public Nullable<int> Photo { get; set; }
        public string City { get; set; }
        public string ProductNote { get; set; }
        public Nullable<int> MemberID { get; set; }
        public Nullable<int> CategoryID { get; set; }
        public Nullable<int> TotalLike { get; set; }
    
        public virtual Categories Categories { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Favorites> Favorites { get; set; }
        public virtual Members Members { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Sales> Sales { get; set; }
    }
}