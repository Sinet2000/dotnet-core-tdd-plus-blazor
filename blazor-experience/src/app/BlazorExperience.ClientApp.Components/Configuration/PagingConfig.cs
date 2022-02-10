using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorExperience.ClientApp.Components.Configuration
{
    public class PagingConfig
    {
        public bool Enabled { get; set; }
        public int PageSize { get; set; }

        public int NumOfItemsToSkip(int pageNumber)
        {
            if (Enabled)
            {
                return (pageNumber - 1) * PageSize;
            }

            return 0;
        }

        public int NumOfItemsToTake(int totalItemsCount)
        {
            if (Enabled)
            {
                return PageSize;
            }

            return totalItemsCount;
        }

        public int PrevPageNumber(int currentPageNumber)
        {
            if (currentPageNumber > 1)
            {
                return currentPageNumber - 1;
            }
            else
                return 1;
        }

        public int NextPageNumber(int currentPageNumber, int totalItemsCount)
        {

        }
    }
}
