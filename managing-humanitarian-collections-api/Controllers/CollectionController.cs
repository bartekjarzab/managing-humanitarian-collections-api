using Microsoft.AspNetCore.Mvc;
using managing_humanitarian_collections_api.Services;
using managing_humanitarian_collections_api.Models;
using managing_humanitarian_collections_api.Entities;
using System.Collections.Generic;

namespace managing_humanitarian_collections_api.Controllers
{

    [Route("/api/collection")]
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
            var id = _collectionService.Create(dto);

            return Created($"/api/collection/{id}", null);
        }
        [HttpGet("{id}")]
        public ActionResult<CollectionDto> Get([FromRoute] int id)
        {
            var collection = _collectionService.GetById(id);
            return Ok(collection);
        }

        [HttpGet]
        public ActionResult<IEnumerable<CollectionDto>> GetAll()
        {
            var collectionDtos = _collectionService.GetAll();

            return Ok(collectionDtos);
        }

        //[HttpGet("{id}")]

        //public ActionResult<List<int>> GetIds([FromRoute] int id)
        //{
        //    var ids = _collectionService.GetCollectionPointIds(id);
        //    return Ok(ids);
        //}
                
        


    }
}
