using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASAPTASK.Core.Helper
{
    public class Helper
    {
        public static bool IsList(object o)
        {
            return o is ICollection &&
               o.GetType().IsGenericType &&
              ( o.GetType().GetGenericTypeDefinition().IsAssignableFrom(typeof(List<>))
              || o.GetType().GetGenericTypeDefinition().IsAssignableFrom(typeof(ICollection)));
        }

    }
}
