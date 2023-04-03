using Microsoft.AspNetCore.Mvc;
using managing_humanitarian_collections_api.Services;
using managing_humanitarian_collections_api.Models;
using managing_humanitarian_collections_api.Entities;
using System.Collections.Generic;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using managing_humanitarian_collections_api.Models.Collection;

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
            return Ok(id);
        }
        [HttpPut("{id}")]
        public ActionResult Update([FromBody] UpdateCollectionStatusDto dto, [FromRoute] int id)
        {
            _collectionService.UpdateCollectionStatus(id, dto);
            return Ok();
        }
        [AllowAnonymous]
        [HttpGet("{collectionId}")]
        public ActionResult GetCollectionInfo([FromRoute] int collectionId)
        {
            var collectionDto = _collectionService.GetCollection(collectionId);
            return Ok(collectionDto);
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
            var collectionDtos = _collectionService.GetAllCollections();

            return Ok(collectionDtos);
        }
        [HttpPost("{collectionId}/collectionProduct")]
        public ActionResult CreateCollectionNeededProduct([FromRoute] int collectionId, [FromBody] CreateCollectionProductDto dto)
        {
            var newListId = _collectionService.CreateCollectionNeededProducts(collectionId, dto);

            return Created($"api/collection/{collectionId}/collectionProduct/{newListId}", null);
        }
        [AllowAnonymous]
        [HttpGet("{collectionId}/collectionProducts")]
        public ActionResult<List<CollectionProductsListDto>> Get([FromRoute] int collectionId)
        {
            var result = _collectionService.GetAllProducts(collectionId);
            return Ok(result);
        }
        [HttpGet("ordersPerDonator/{id}")]
        public ActionResult GetOrganiserCollections([FromRoute] int id)
        {
            var collections = _collectionService.GetCollectionPerOrganiser(id);
            return Ok(collections);
        }
    }
}
