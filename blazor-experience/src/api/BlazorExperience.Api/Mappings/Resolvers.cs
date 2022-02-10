using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using BlazorExperience.Shared.Constants;

namespace BlazorExperience.Api.Mappings
{
    public class DateToFormattedStringResolver : IMemberValueResolver<object, object, DateTime?, string>
    {
        public string Resolve(object source, object destination, DateTime? sourceMember, string destMember, ResolutionContext context)
        {
            return !sourceMember.HasValue ? "" : sourceMember.Value.ToString(Formatters.FullDateTimeFormat);
        }
    }

    public class BoolToStringResolver : IMemberValueResolver<object, object, bool, string>
    {
        public string Resolve(object source, object destination, bool sourceMember, string destMember, ResolutionContext context)
        {
            return sourceMember ? "Yes" : "No";
        }
    }
}
