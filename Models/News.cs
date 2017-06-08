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

        [Display(Name = "图片 Url")]
        public string PictureUrl { get; set; }

        [Display(Name = "新闻日期")]
        [DataType(DataType.Date)]
        public DateTime? OriginalDate { get; set; }

        [Display(Name = "发布日期")]
        [DataType(DataType.Date)]
        public DateTime? PublishDate { get; set; }

        [Display(Name = "已发布?")]
        public bool? IsPublished { get; set; }

        [Display(Name = "高校名")]
        public string UniversityName { get; set; }
    }
}
