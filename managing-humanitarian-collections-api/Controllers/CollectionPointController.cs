using Microsoft.AspNetCore.Mvc;
using managing_humanitarian_collections_api.Services;
using managing_humanitarian_collections_api.Models;
using managing_humanitarian_collections_api.Entities;
using System.Collections.Generic;

namespace managing_humanitarian_collections_api.Controllers
{

    [Route("/api/collection/{collectionId}/collectionPoint")]
    public class CollectionPointController : ControllerBase
    {
        private readonly ICollectionPointService _collectionPointService;

        public CollectionPointController(ICollectionPointService collectionPointService)
        {
            _collectionPointService = collectionPointService;
        }
        //[HttpPost]
        //public ActionResult CreateCollectionPoint([FromBody] CreateCollectionPointDto dto)
        //{
        //    var id = _collectionPointService.Create(dto);

        //    return Created($"/api/collectionPoint/{id}", null);
        //}
        [HttpPost]
        public ActionResult CreatePointCollectionForCollection([FromRoute] int collectionId, [FromBody] CreateCollectionPointDto dto)
        {
            var newPointId = _collectionPointService.CreatePointForCollection(collectionId, dto);

            return Created($"api/collection/{collectionId}/collectionPoint/{newPointId}", null);
        }
        //zwracanie informacji dotyczących adresu punktu



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
    }
}
