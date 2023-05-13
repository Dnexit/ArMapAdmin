using ArMapAdmin.Models;
using ArMapAdmin.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;

namespace ArMapAdmin.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ModelController : Controller
    {
        private readonly ModelsService _modelsService;

        public ModelController(ModelsService modelsService)
        {
            _modelsService = modelsService;
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpGet]
        public async Task<List<Model>> Get()
        {
            return await _modelsService.GetAsync();
        }

        [HttpGet("{objectId:length(24)}")]
        public async Task<ActionResult<Model>>Get(string objectId)
        {
            var model = await _modelsService.GetAsync(objectId);

            if (model is null)
            {
                return NotFound();
            }

            return model;
        }

        [HttpDelete("{id:length(24)}")]
        public async Task<IActionResult> Delete(string id)
        {
            var model = await _modelsService.GetAsync(id);

            if (model is null)
            {
                return NotFound();
            }

            await _modelsService.RemoveAsync(id);

            return NoContent();
        }

        [HttpPut("{id:length(24)}")]
        public async Task<IActionResult> Update(string id, Model updatedModel)
        {
            var model = await _modelsService.GetAsync(id);

            if (model is null)
            {
                return NotFound();
            }

            updatedModel._id = model._id;

            await _modelsService.UpdateAsync(id, updatedModel);

            return NoContent();
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromForm]Model newModel)
        {
            newModel.id = newModel._id.ToString();
            await _modelsService.CreateAsync(newModel);
            return CreatedAtAction(nameof(Get), new { id = newModel._id }, newModel);
        }
    }
}
