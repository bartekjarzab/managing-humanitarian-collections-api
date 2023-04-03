using Microsoft.AspNetCore.Mvc;
using managing_humanitarian_collections_api.Services;
using managing_humanitarian_collections_api.Models;
using managing_humanitarian_collections_api.Entities;
using System.Collections.Generic;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;

namespace managing_humanitarian_collections_api.Controllers
{

    [Route("/api/collection/{collectionId}/comments")]
    [ApiController]
    [Authorize]
    public class CommentController : ControllerBase
    {
        private readonly ICommentService _commentService;

        public CommentController(ICommentService commentService)
        {
            _commentService = commentService;
        }
        #region Tworzenie zamowienia
        [HttpPost()]
        public ActionResult AddComment([FromBody] CreateCommentDto dto, [FromRoute] int collectionId)
        {
            var newCommentId = _commentService.CreateComment(collectionId, dto);

            return Ok(newCommentId);

        }
        #endregion

        [HttpGet()]
        public ActionResult GetCommentsPerCollection([FromRoute] int collectionId)
        {
            var commentsDto = _commentService.GetAllCollectionComments(collectionId);
            return Ok(commentsDto);
        }


    }
}
