using System;
using System.Collections.Generic;

namespace UniversityNews_aNewTry.Models
{
    public partial class News
    {
        public string Title { get; set; }
        public string NewsUrl { get; set; }
        public string PictureUrl { get; set; }
        public DateTime? OriginalDate { get; set; }
        public DateTime? PublishDate { get; set; }
        public bool? IsPublished { get; set; }
        public string UniversityName { get; set; }
    }
}
