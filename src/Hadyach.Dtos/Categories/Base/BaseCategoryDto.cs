namespace Hadyach.Dtos.Categories.Base
{
    public class BaseCategoryDto
    {
        public string Title { get; set; }

        public string Alias { get; set; }

        public string Description { get; set; }

        public int? ParentCategoryId { get; set; }
    }
}
