using Hadyach.Dtos.Categories.Base;

namespace Hadyach.Dtos.Categories
{
    public class CategoryDto : BaseCategoryDto
    {
        public int Id { get; set; }
        public CategoryDto ParentCategory { get; set; }
    }
}
