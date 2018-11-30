using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Hadyach.Dtos.Articles;
using Hadyach.Models.Articles;
using Hadyach.Services.Contracts.Services.Articles;
using Microsoft.AspNetCore.Mvc;

namespace Hadyach.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ArticleApiController : ControllerBase
    {
        private readonly IArticleService articleService;
        private readonly IMapper mapper;

        public ArticleApiController(IArticleService articleService, IMapper mapper)
        {
            this.articleService = articleService;
            this.mapper = mapper;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            return Ok(this.articleService.GetMany<ArticleDto>());
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            return this.Ok(this.articleService.Get<ArticleDto>(id));
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] AddArticleDto dto)
        {
            var model = this.mapper.Map<AddArticleModel>(dto);
            var created = await this.articleService.AddAsync<ArticleDto>(model);

            return this.Created($"/api/article/{created.Id}", created);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await this.articleService.DeleteAsync(id);
            return Ok();
        }
    }
}
