using System;
using System.Data.Entity;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using Movies.Domain;
using Movies.DatabaseModel;


namespace Movies.Repositories
{
    public class MovieRepository : IMovieRepository
    {
        MovieDbContext _ctx;

        public MovieRepository(MovieDbContext dataContext)
        {
            _ctx = dataContext;
        }
        public void Create(Movie movie, HttpPostedFileBase uploadImage)
        {
            if (uploadImage != null)
            {
                movie.ImageMimeType = uploadImage.ContentType;
                movie.ImageData = new byte[uploadImage.ContentLength];
                uploadImage.InputStream.Read(movie.ImageData, 0, uploadImage.ContentLength);
            }
            _ctx.Movies.Add(movie);
            _ctx.SaveChanges();
        }

        public Movie GetMovie(int? id)
        {
            Movie dbEntry = null;
            if (id!=null)
            {
                dbEntry = _ctx.Movies.Find(id);
            }
            return dbEntry;
        }

        public IEnumerable<Movie> GetMovies()
        {
            return _ctx.Movies.OrderBy(c => c.Name);
        }

        public void Update(Movie movie, HttpPostedFileBase uploadImage)
        {
            Movie dbEntry = _ctx.Movies.Find(movie.Id);
            if (dbEntry != null)
            {
                dbEntry.Name = movie.Name;
                dbEntry.Description = movie.Description;
                dbEntry.DirectorName = movie.DirectorName;
                dbEntry.YearOfRelease = movie.YearOfRelease;
                if (uploadImage != null)
                {
                    dbEntry.ImageMimeType = uploadImage.ContentType;
                    dbEntry.ImageData = new byte[uploadImage.ContentLength];
                    uploadImage.InputStream.Read(dbEntry.ImageData, 0, uploadImage.ContentLength);
                }
                _ctx.Entry(dbEntry).State = EntityState.Modified;
                _ctx.SaveChanges();
            }
        }
    }
}
