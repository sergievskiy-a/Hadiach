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
    [Route("api/articles")]
    [ApiController]
    public class ArticlesApiController : ControllerBase
    {
        private readonly IArticleService articleService;
        private readonly IMapper mapper;

        public ArticlesApiController(IArticleService articleService, IMapper mapper)
        {
            this.articleService = articleService;
            this.mapper = mapper;
        }

        /// <summary>
        /// Returns Articles.
        /// </summary>
        /// <param name="skip"></param>
        /// <param name="top"></param>
        /// <returns>Existed articles</returns>
        [HttpGet]
        public async Task<IActionResult> GetAll(int skip = 0, int top = 5, int? categoryId = null)
        {
            return Ok(await this.articleService.GetManyAsync<ArticleDto>(skip, top, categoryId));
        }

        /// <summary>
        /// Returns Article.
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Existed article</returns>
        /// <response code="404">If not found</response>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            return this.Ok(await this.articleService.GetAsync<ArticleDto>(id));
        }

        /// <summary>
        /// Creates an Article.
        /// </summary>
        /// <param name="dto"></param>
        /// <returns>A newly created Article</returns>
        /// <response code="201">Returns the newly created item</response>
        /// <response code="400">If the item is null</response>
        /// <response code="401">If unauthorize</response>
        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] AddArticleDto dto)
        {
            var model = this.mapper.Map<AddArticleModel>(dto);
            var created = await this.articleService.AddAsync<ArticleDto>(model);

            return this.Created($"/api/article/{created.Id}", created);
        }

        /// <summary>
        /// Updates an Article.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="dto"></param>
        /// <returns>A newly created Article</returns>
        /// <response code="200">Returns the updated item</response>
        /// <response code="400">If the item is null</response>
        /// <response code="404">If the item doesn't exist.</response>
        /// <response code="401">If unauthorize</response>
        [Authorize]
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] UpdateArticleDto dto)
        {
            var model = this.mapper.Map<UpdateArticleModel>(dto);
            model.Id = id;

            var created = await this.articleService.UpdateAsync<ArticleDto>(model);

            return this.Ok(created);
        }

        /// <summary>
        /// Deletes an Article.
        /// </summary>
        /// <param name="id"></param>
        /// <response code="200">Item was deleted</response>
        /// <response code="404">If the item doesn't exist.</response>
        /// <response code="401">If unauthorize</response>
        [Authorize]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await this.articleService.DeleteAsync(id);
            return this.Ok();
        }
    }
}
