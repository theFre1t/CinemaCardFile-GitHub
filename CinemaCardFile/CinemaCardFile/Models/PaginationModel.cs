using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CinemaCardFile.Models
{
    public class PaginationModel
    {
        public int currPage { get; private set; }
        public int maxPages { get; private set; }

        public PaginationModel(int count, int currPage, int sizePage)
        {
            this.currPage = currPage;
            maxPages = count / sizePage;
            if (count % sizePage != 0)
            {
                maxPages++;
            }
        }

        public bool GoToFirstPage
        {
            get
            {
                return (currPage > 1);
            }
        }

        public bool GoToLastPage
        {
            get
            {
                return (currPage < maxPages);
            }
        }
    }
}
