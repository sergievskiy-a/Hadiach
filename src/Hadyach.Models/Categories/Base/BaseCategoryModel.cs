namespace Hadyach.Models.Categories.Base
{
    public class BaseCategoryModel
    {
        public string Title { get; set; }

        public string Description { get; set; }

        public int? ParentCategoryId { get; set; }
    }
}
