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
    
    public partial class CT_Menu
    {
        public int MaMenu { get; set; }
        public int MaMa { get; set; }
        public int MaCombo { get; set; }
    
        public virtual Combo Combo { get; set; }
        public virtual MonAn MonAn { get; set; }
        public virtual Menu Menu { get; set; }
    }
}