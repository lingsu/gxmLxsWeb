using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Lxs.Core
{
    public abstract class BaseEntity
    {
        public int Id { get; set; }
        //public static bool operator ==(BaseEntity x, BaseEntity y)
        //{
        //    return Equals(x, y);
        //}

        //public static bool operator !=(BaseEntity x, BaseEntity y)
        //{
        //    return !(x == y);
        //}
    }

}
