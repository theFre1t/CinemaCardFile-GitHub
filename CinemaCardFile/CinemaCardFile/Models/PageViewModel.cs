using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CinemaCardFile.Models
{
    public class PageViewModel
    {
        public int currPage { get; private set; }
        public int maxPages { get; private set; }

        public PageViewModel(int count, int currPage, int sizePage)
        {
            this.currPage = currPage;
            maxPages = count / sizePage;
            if (count % sizePage != 0)
            {
                maxPages++;
            }
        }

        public bool backPage
        {
            get
            {
                return (currPage > 1);
            }
        }

        public bool nextPage
        {
            get
            {
                return (currPage < maxPages);
            }
        }
    }
}
