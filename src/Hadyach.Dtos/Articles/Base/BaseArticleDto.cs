using System;

namespace Hadyach.Dtos.Articles.Base
{
    public class BaseArticleDto
    {
        public DateTime PublishedDateTime { get; set; }

        public bool Pinned { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public int? CategoryId { get; set; }
    }
}
