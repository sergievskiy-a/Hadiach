using System;
using System.Collections.Generic;

namespace Hadyach.Models.Articles.Base
{
    public class BaseArticleModel
    {
        public DateTime PublishedDateTime { get; set; }

        public bool Pinned { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public int? CategoryId { get; set; }

        public List<string> Tags { get; set; }
    }
}
