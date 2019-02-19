using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Hadyach.Data.Contracts;
using Hadyach.Data.Entities;
using Hadyach.Dtos.Categories;

namespace Hadyach.Services.Resolvers
{
    public class TagsResolver : IMemberValueResolver<object, object, List<ArticleTag>, List<string>>
    {
        public List<string> Resolve(object source, object destination, List<ArticleTag> sourceMember, List<string> destMember, ResolutionContext context)
        {
            return sourceMember.Select(x => x.Tag.Value).ToList();
        }
    }
}
