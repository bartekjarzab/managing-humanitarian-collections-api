using Microsoft.AspNetCore.Mvc;
using managing_humanitarian_collections_api.Services;
using managing_humanitarian_collections_api.Models;
using managing_humanitarian_collections_api.Entities;
using System.Collections.Generic;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;

namespace managing_humanitarian_collections_api.Controllers
{

    [Route("/api/collection")]
    [ApiController]
    [Authorize]
    public class CollectionController : ControllerBase
    {
        private readonly ICollectionService _collectionService;

        public CollectionController(ICollectionService collectionService)
        {
            _collectionService = collectionService;
        }
        [HttpPost]
        public ActionResult CreateCollection([FromBody] CreateCollectionDto dto)
        {
           
            var id = _collectionService.CreateCollection(dto);

            return Created($"/api/collection/{id}", null);
        }
        [HttpPut("{id}")]
        public ActionResult Update([FromBody] UpdateCollectionStatusDto dto, [FromRoute] int id)
        {

            _collectionService.UpdateCollectionStatus(id, dto);

            return Ok();
        }
        [HttpDelete("{id}")]
        public ActionResult DeleteCollection([FromRoute] int id)
        {
            _collectionService.Delete(id);

            return NoContent();
        }

        [HttpGet]
        public ActionResult<IEnumerable<CollectionDto>> GetAll()
        {
            var collectionDtos = _collectionService.GetAll();

            return Ok(collectionDtos);
        }

        [HttpGet("{id}")]
        public ActionResult<IEnumerable<CollectionWithProductsDto>> GetCollectionWithProduct([FromRoute] int id)
        {
            var collections = _collectionService.GetCollectionWithProducts(id);
            return Ok(collections);
        }

    }
}
