using Microsoft.AspNetCore.Mvc;
using managing_humanitarian_collections_api.Services;
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
        [Authorize(Roles = "Organizator")]
        [HttpPost]
        public ActionResult CreateCollection([FromBody] CreateCollectionDto dto)
        {
            var id = _collectionService.CreateCollection(dto);
            return Ok(id);
        }
        [Authorize(Roles = "Admin, Organizator")]
        [HttpPut("{id}/updateStatus")]
        public ActionResult Update([FromBody] UpdateCollectionStatusDto dto, [FromRoute] int id)
        {
            _collectionService.UpdateCollectionStatus(id, dto);
            return Ok("Status zbiórki został zmieniony");
        }
        [HttpGet("{collectionId}")]
        public ActionResult GetCollectionInfo([FromRoute] int collectionId)
        {
            var collectionDto = _collectionService.GetCollection(collectionId);
            return Ok(collectionDto);
        }
        [Authorize(Roles = "Admin, Organizator")]
        [HttpDelete("{id}")]
        public ActionResult DeleteCollection([FromRoute] int id)
        {
            _collectionService.Delete(id);

            return Ok("Usunięto zbiórkę");
        }
        [AllowAnonymous]
        [HttpGet]
        public ActionResult<IEnumerable<CollectionDto>> GetAll()
        {
            var collectionDtos = _collectionService.GetAllCollections();

            return Ok(collectionDtos);
        }
        [Authorize(Roles = "Admin, Organizator")]
        [HttpPost("{collectionId}/collectionProduct")]
        public ActionResult CreateCollectionNeededProduct([FromRoute] int collectionId, [FromBody] CreateCollectionProductDto dto)
        {
            var newListId = _collectionService.CreateCollectionNeededProducts(collectionId, dto);

            return Ok("Dodano przedmiot do zbiórki");
        }
        [HttpGet("{collectionId}/collectionProduct")]
        public ActionResult<List<CollectionProductsListDto>> Get([FromRoute] int collectionId)
        {
            var result = _collectionService.GetAllProducts(collectionId);
            return Ok(result);
        }
        [AllowAnonymous]
        [HttpGet("collectionPerOrganizer/{id}")]
        public ActionResult GetOrganiserCollections([FromRoute] int id)
        {
            var collections = _collectionService.GetCollectionPerOrganiser(id);
            return Ok(collections);
        }
        [Authorize(Roles = "Admin, Organizator")]
        [HttpPut("{collectionId}")]
        public ActionResult EditCollection([FromBody] EditCollectionDto dto, int collectionId)
        {

            _collectionService.EditCollection(collectionId, dto);
            return Ok("Profil został zedytowany");
        }

        [HttpDelete("{collectionId}/collectionProduct/{collectionProductId}")]
        public ActionResult DeleteCollectionProduct([FromRoute] int collectionProductId, int collectionId)
        {
            _collectionService.DeleteCollectionproduct(collectionProductId, collectionId);

            return Ok("Przedmiot usunięty");
        }
        [HttpPut("{collectionId}/collectionProduct/{collectionProductId}")]
        public ActionResult EditCollectionProduct([FromBody] CreateCollectionProductDto dto, [FromRoute] int collectionId, int collectionProductId)
        {

            _collectionService.EditCollectionProduct(collectionProductId, collectionId, dto);
            return Ok("Przedmiot zbiórki zmieniono");
        }
        [HttpGet("{collectionId}/collectionProduct/{collectionProductId}")]
        public ActionResult<CollectionProductsListDto> Get([FromRoute] int collectionId, [FromRoute] int collectionProductId)
        {
            CollectionProductsListDto collectionProduct = _collectionService.GetCollectionProductById(collectionId, collectionProductId);
            return Ok(collectionProduct);
        }
    }
}
