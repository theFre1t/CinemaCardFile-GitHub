using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CinemaCardFile.Models
{
    public class PaginationModel
    {
        public int currPage { get; private set; } // Число активной страницы
        public int maxPages { get; private set; } // Общее число страниц

        public PaginationModel(int count, int currPage, int sizePage)
        {
            this.currPage = currPage;
            maxPages = count / sizePage; //Вычисляем общее число страниц
            //Проверка на остаток
            if (count % sizePage != 0)
            {
                maxPages++;
            }
        }

        //Проверка на возможность двигаться назад по страницам 
        public bool GoToFirstPage
        {
            get
            {
                return (currPage > 1);
            }
        }

        //Проверка на возможность двигаться вперед по страницам 
        public bool GoToLastPage
        {
            get
            {
                return (currPage < maxPages);
            }
        }
    }
}
