using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlazorExperience.Core.Attributes
{
    public class RangeAttribute : Attribute
    {
        internal RangeAttribute(int rangeStart, int rangeEnd)
        {
            Range = new KeyValuePair<int, int>(rangeStart, rangeEnd);
        }

        public KeyValuePair<int, int> Range { get; private set; }
    }
}
