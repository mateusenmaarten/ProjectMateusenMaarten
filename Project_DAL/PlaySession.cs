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
    
    public partial class PlaySession
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public PlaySession()
        {
            this.Players = new HashSet<Player>();
        }
    
        public int PlaySession_id { get; set; }
        public int Boardgame_id { get; set; }
        public System.DateTime SessionDate { get; set; }
    
        public virtual Boardgame Boardgame { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Player> Players { get; set; }
    }
}
