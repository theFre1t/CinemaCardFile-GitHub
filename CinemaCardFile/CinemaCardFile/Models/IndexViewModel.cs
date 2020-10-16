using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CinemaCardFile.Models
{
    public class IndexViewModel
    {
        public IEnumerable<Film> films { get; set; }
        public PaginationModel pageViewModel { get; set; }
    }
}
