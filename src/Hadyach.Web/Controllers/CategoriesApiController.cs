using System.Threading.Tasks;
using AutoMapper;
using Hadyach.Dtos.Categories;
using Hadyach.Models.Categories;
using Hadyach.Services.Contracts.Services.Categories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Hadyach.Web.Controllers
{
    [Route("api/categories")]
    [ApiController]
    public class CategoriesApiController : ControllerBase
    {
        private readonly ICategoryService categoryService;
        private readonly IMapper mapper;

        public CategoriesApiController(ICategoryService categoryService, IMapper mapper)
        {
            this.categoryService = categoryService;
            this.mapper = mapper;
        }

        /// <summary>
        /// Returns Categories.
        /// </summary>
        /// <param name="skip"></param>
        /// <param name="top"></param>
        /// <returns>Existed categories</returns>
        [HttpGet]
        public async Task<IActionResult> GetAll(int skip = 0, int top = 5)
        {
            return Ok(await this.categoryService.GetManyAsync<CategoryDto>(skip, top));
        }

        /// <summary>
        /// Returns Category.
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Existed Category</returns>
        /// <response code="404">If not found</response>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            return this.Ok(await this.categoryService.GetAsync<CategoryDto>(id));
        }

        /// <summary>
        /// Creates an Category.
        /// </summary>
        /// <param name="dto"></param>
        /// <returns>A newly created Category</returns>
        /// <response code="201">Returns the newly created item</response>
        /// <response code="400">If the item is null</response>
        /// <response code="401">If unauthorize</response>
        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] AddCategoryDto dto)
        {
            var model = this.mapper.Map<AddCategoryModel>(dto);
            var created = await this.categoryService.AddAsync<CategoryDto>(model);

            return this.Created($"/api/categories/{created.Id}", created);
        }

        /// <summary>
        /// Updates an Category.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="dto"></param>
        /// <returns>A newly created Category</returns>
        /// <response code="200">Returns the updated item</response>
        /// <response code="400">If the item is null</response>
        /// <response code="404">If the item doesn't exist.</response>
        /// <response code="401">If unauthorize</response>
        [Authorize]
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] UpdateCategoryDto dto)
        {
            var model = this.mapper.Map<UpdateCategoryModel>(dto);
            model.Id = id;

            var created = await this.categoryService.UpdateAsync<CategoryDto>(model);

            return this.Ok(created);
        }

        /// <summary>
        /// Deletes an Category.
        /// </summary>
        /// <param name="id"></param>
        /// <response code="200">Item was deleted</response>
        /// <response code="404">If the item doesn't exist.</response>
        /// <response code="401">If unauthorize</response>
        [Authorize]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await this.categoryService.DeleteAsync(id);
            return this.Ok();
        }
    }
}
