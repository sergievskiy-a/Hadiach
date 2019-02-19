using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Hadyach.Common.Data.Contracts;

namespace Hadyach.Data.Entities
{
    public class Tag : IEntity<int>
    {
        public int Id { get; set; }
        
        public string Value { get; set; }

        public List<ArticleTag> TagArticles { get; set; }
    }
}
