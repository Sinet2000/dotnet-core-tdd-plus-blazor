using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace BlazorExperience.Api.Mappings
{
    public static class Extensions
    {
        public static IEnumerable<SelectListItem> ToSelectListItems<T>(this IEnumerable<T> items, Func<T, string> textSelector, Func<T, object> valueSelector)
        {
            return ToSelectListItems(items, textSelector, valueSelector, false);
        }

        public static IEnumerable<SelectListItem> ToSelectListItems<T>(this IEnumerable<T> items, Func<T, string> textSelector, Func<T, object> valueSelector, bool allowEmpty)
        {
            return ToSelectListItems(items, textSelector, valueSelector, allowEmpty, i => false);
        }

        public static IEnumerable<SelectListItem> ToSelectListItems<T>(this IEnumerable<T> items, Func<T, string> textSelector, Func<T, object> valueSelector, Func<T, bool> selected)
        {
            return ToSelectListItems(items, textSelector, valueSelector, false, selected);
        }

        public static IEnumerable<SelectListItem> ToSelectListItems<T>(this IEnumerable<T> items, Func<T, string> textSelector, Func<T, object> valueSelector, bool allowEmpty, Func<T, bool> selected)
        {
            var list = items.OrderBy(textSelector).Select(item =>
                new SelectListItem
                {
                    Selected = selected(item),
                    Text = textSelector(item),
                    Value = valueSelector(item).ToString()
                }).ToList();

            if (allowEmpty)
            {
                list.Insert(0, new SelectListItem
                {
                    Selected = false,
                    Text = "",
                    Value = ""
                });
            }

            return list;
        }
    }
}
