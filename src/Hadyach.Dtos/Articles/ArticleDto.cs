using Hadyach.Dtos.Articles.Base;
using Hadyach.Dtos.Categories;
using System;
using System.Collections.Generic;

namespace Hadyach.Dtos.Articles
{
    public class ArticleDto : BaseArticleDto
    {
        public int Id { get; set; }

        public DateTime ModifiedDateTime { get; set; }

        public CategoryDto Category { get; set; }
    }
}
