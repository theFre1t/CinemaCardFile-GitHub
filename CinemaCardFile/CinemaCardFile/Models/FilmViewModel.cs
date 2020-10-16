using Microsoft.AspNetCore.Http;

namespace CinemaCardFile
{
    public class FilmViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Descrpion { get; set; }
        public string Year { get; set; }
        public string Producer { get; set; }
        public string Username { get; set; }
        public IFormFile Poster { get; set; }
    }
}
