using Microsoft.AspNetCore.Mvc;
using managing_humanitarian_collections_api.Services;
using managing_humanitarian_collections_api.Entities;
using System.Collections.Generic;
using managing_humanitarian_collections_api.Models.Collection;
using Microsoft.AspNetCore.Authorization;

namespace managing_humanitarian_collections_api.Controllers
{
    

    [Route("/api/collection/{collectionId}/collectionPoint")]
    [Authorize]
    public class CollectionPointController : ControllerBase
    {
        private readonly ICollectionPointService _collectionPointService;

        public CollectionPointController(ICollectionPointService collectionPointService)
        {
            _collectionPointService = collectionPointService;
        }
        [HttpPost]
        public ActionResult CreateCollectionpointForCollection([FromRoute] int collectionId, [FromBody] CreateCollectionPointDto dto)
        {
            var newPointId = _collectionPointService.CreatePointForCollection(collectionId, dto);

            return Ok("Punkt zbiórki został dodany");
        }

        [HttpGet("{collectionPointId}")]
        public ActionResult<CollectionPointDto> Get([FromRoute] int collectionId, [FromRoute] int collectionPointId)
        {
            CollectionPointDto colectionPoint = _collectionPointService.GetById(collectionId, collectionPointId);
            return Ok(colectionPoint);
        }
        [HttpGet]
        public ActionResult<List<CollectionPointDto>> Get([FromRoute] int collectionId)
        {
            var result = _collectionPointService.GetAll(collectionId);
            return Ok(result);
        }
        [Authorize(Roles = "Organizator")]
        [HttpPut("{collectionPointId}")]
        public ActionResult EditcollectionPoint([FromBody] CreateCollectionPointDto dto,[FromRoute] int collectionId, int collectionPointId)
        {

            _collectionPointService.EditCollectionPoint(collectionId, collectionPointId, dto);
            return Ok("Profil został zedytowany");
        }
        [Authorize(Roles = "Organizator")]
        [HttpDelete("{id}")]
        public ActionResult DeleteThisCollectionPoint([FromRoute] int id, int collectionId)
        {
            _collectionPointService.DeleteCollectionPoint(id, collectionId);

            return Ok("Punkt zbiórki usunięty");
        }
    }
}
