using Hadyach.Common.Data.Contracts;
using System;
using System.Collections.Generic;

namespace Hadyach.Data.Entities
{
    public class Article : IEntity<int>
    {
        public int Id { get; set; }

        public DateTime CreatedDateTime { get; set; }

        public DateTime PublishedDateTime { get; set; }

        public DateTime ModifiedDateTime { get; set; }

        public bool Pinned { get; set; }

        public int? CategoryId { get; set; }

        public Category Category { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public List<ArticleTag> ArticleTags { get; set; }
    }
}