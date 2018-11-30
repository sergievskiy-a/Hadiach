using Hadyach.Common.Data.Contracts;

namespace Hadyach.Data.Entities.Articles
{
    public class Article : IEntity<int>
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
    }
}