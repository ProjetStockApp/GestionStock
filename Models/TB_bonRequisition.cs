//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace StockApp.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class TB_bonRequisition
    {
        public TB_bonRequisition()
        {
            this.TB_detail_requisition = new HashSet<TB_detail_requisition>();
        }
    
        public int Id_bon_requisition { get; set; }
        public string Description { get; set; }
        public string UserId { get; set; }
        public Nullable<System.DateTime> Date_requisition { get; set; }
        public Nullable<System.DateTime> DateCreer { get; set; }
        public string CreerPar { get; set; }
        public string IsSoumettre { get; set; }
        public string Validate { get; set; }
        public Nullable<System.DateTime> Date_aprouver { get; set; }
        public Nullable<System.DateTime> Date_validation { get; set; }
        public string Administrateur { get; set; }
        public string Directeur { get; set; }
    
        public virtual AspNetUser AspNetUser { get; set; }
        public virtual ICollection<TB_detail_requisition> TB_detail_requisition { get; set; }
    }
}
