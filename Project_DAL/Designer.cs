//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Project_DAL
{
    using System;
    using System.Collections.Generic;
    
    public partial class Designer
    {
        public int Designer_id { get; set; }
        public int Boardgame_id { get; set; }
        public Nullable<int> Person_id { get; set; }
    
        public virtual Boardgame Boardgame { get; set; }
        public virtual Person Person { get; set; }
    }
}