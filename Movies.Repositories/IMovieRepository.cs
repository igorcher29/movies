using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Movies.Domain;
using System.Web;

namespace Movies.Repositories
{
    public interface IMovieRepository
    {
        IEnumerable<Movie> GetMovies();
        Movie GetMovie(int? id);
        void Create(Movie movie, HttpPostedFileBase uploadImage);
        void Update(Movie movie, HttpPostedFileBase uploadImage);
    }
}
