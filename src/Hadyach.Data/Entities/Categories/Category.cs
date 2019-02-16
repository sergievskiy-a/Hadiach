﻿using Hadyach.Common.Data.Contracts;

namespace Hadyach.Data.Entities.Categories
{
    public class Category : IEntity<int>
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public int? ParentCategoryId { get; set; }

        public Category ParentCategory { get; set; }
    }
}
