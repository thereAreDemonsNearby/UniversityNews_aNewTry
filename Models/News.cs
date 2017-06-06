using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace UniversityNews_aNewTry.Models
{
    public partial class News
    {
        public string Title { get; set; }

        [Display(Name = "Url")]
        public string NewsUrl { get; set; }

        [Display(Name = "Picture Url")]
        public string PictureUrl { get; set; }

        [Display(Name = "Original Date")]
        [DataType(DataType.Date)]
        public DateTime? OriginalDate { get; set; }

        [Display(Name = "Publish Date")]
        [DataType(DataType.Date)]
        public DateTime? PublishDate { get; set; }

        [Display(Name = "Published?")]
        public bool? IsPublished { get; set; }

        [Display(Name = "University Name")]
        public string UniversityName { get; set; }
    }
}
