using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project_DAL
{
    public partial class Owner
    {
 
        

        public override bool Equals(object obj)
        {
            return obj is Owner owner &&
                   Person_id == owner.Person_id &&
                   Boardgame_id == owner.Boardgame_id;
        }

        public override int GetHashCode()
        {
            int hashCode = 1388529831;
            hashCode = hashCode * -1521134295 + Person_id.GetHashCode();
            hashCode = hashCode * -1521134295 + Boardgame_id.GetHashCode();
            return hashCode;
        }
    }
}
