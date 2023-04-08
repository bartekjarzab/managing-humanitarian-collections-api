using Microsoft.AspNetCore.Mvc;
using managing_humanitarian_collections_api.Services;
using managing_humanitarian_collections_api.Entities;
using System.Collections.Generic;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using managing_humanitarian_collections_api.Models.Comment;

namespace managing_humanitarian_collections_api.Controllers
{

    [Route("/api/")]
    [ApiController]
    [Authorize]
    public class CommentController : ControllerBase
    {
        private readonly ICommentService _commentService;

        public CommentController(ICommentService commentService)
        {
            _commentService = commentService;
        }
        #region Tworzenie komentarza
        [HttpPost("collection/{collectionId}/comments")]
        public ActionResult AddComment([FromBody] CreateCommentDto dto, [FromRoute] int collectionId)
        {
            var newCommentId = _commentService.CreateComment(collectionId, dto);

            return Ok(newCommentId);

        }
        #endregion
        #region Pobranie wszystkich komentarzy dla zbiórki
        [HttpGet("collection/{collectionId}/comments")]
        public ActionResult GetCommentsPerCollection([FromRoute] int collectionId)
        {
            var commentsDto = _commentService.GetAllCollectionComments(collectionId);
            return Ok(commentsDto);
        }
        [Authorize(Roles = "Admin, Organizator")]
        #endregion
        [HttpDelete("comments/{id}")]
        public ActionResult DeleteThisComment([FromRoute] int id)
        {
            _commentService.DeleteComment(id);

            return Ok("Komentarz usunięty");
        }
    }
}
