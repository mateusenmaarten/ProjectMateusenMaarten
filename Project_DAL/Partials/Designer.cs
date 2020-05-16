using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project_DAL
{
    public partial class Designer
    {
        public override bool Equals(object obj)
        {
            return obj is Designer designer &&
                   Boardgame_id == designer.Boardgame_id &&
                   Person_id == designer.Person_id;
        }

        public override int GetHashCode()
        {
            int hashCode = 386492991;
            hashCode = hashCode * -1521134295 + Boardgame_id.GetHashCode();
            hashCode = hashCode * -1521134295 + Person_id.GetHashCode();
            return hashCode;
        }
    }
}
