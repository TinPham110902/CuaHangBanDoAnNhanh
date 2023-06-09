//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace DoAnNhanh.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Web;

    public partial class MonAn
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public MonAn()
        {
            this.CT_Combo = new HashSet<CT_Combo>();
            this.CT_HD = new HashSet<CT_HD>();
            this.CT_Menu = new HashSet<CT_Menu>();
            Hinh = "~/hinh/Add.png";

        }

        public int MaMa { get; set; }
        public string TenMa { get; set; }
        public int Sl { get; set; }
        public int Gia { get; set; }
        public Nullable<int> MaKm { get; set; }
        public int MaLoai { get; set; }
        public string Hinh { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CT_Combo> CT_Combo { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CT_HD> CT_HD { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CT_Menu> CT_Menu { get; set; }
        public virtual Loai Loai { get; set; }

        [NotMapped]
        public HttpPostedFileBase ImageUpload { get; set; }
    }
}
