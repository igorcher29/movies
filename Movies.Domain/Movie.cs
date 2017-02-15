using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Movies.Domain
{
    public class Movie
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        [Display(Name ="Название")]
        [Required(ErrorMessage ="Название обязательно должно быть указано")]
        public string Name { get; set; }

        [Display(Name = "Описание")]
        [DataType(DataType.MultilineText)]
        public string Description { get; set; }

        [Required(ErrorMessage ="Укажите год выпуска фильма")]
        [Display(Name ="Год выпуска")]
        public int YearOfRelease { get; set; }

        [Required(ErrorMessage = "Введите имя режиссёра")]
        [Display(Name = "Режиссёр")]
        public string DirectorName { get; set; }

        public byte[] ImageData { get; set; }
        public string ImageMimeType { get; set; }
    }
}
