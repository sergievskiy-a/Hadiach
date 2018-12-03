using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Hadyach.Dtos.Articles;
using Hadyach.Models.Articles;
using Hadyach.Services.Contracts.Services.Articles;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Hadyach.Web.Controllers
{
    [Route("api/article")]
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
        public async Task<IActionResult> GetAll()
        {
            return Ok(await this.articleService.GetManyAsync<ArticleDto>());
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            return this.Ok(await this.articleService.GetAsync<ArticleDto>(id));
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] AddArticleDto dto)
        {
            var model = this.mapper.Map<AddArticleModel>(dto);
            var created = await this.articleService.AddAsync<ArticleDto>(model);

            return this.Created($"/api/article/{created.Id}", created);
        }

        [Authorize]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await this.articleService.DeleteAsync(id);
            return Ok();
        }
    }
}
