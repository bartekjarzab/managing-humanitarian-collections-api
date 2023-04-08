using Microsoft.AspNetCore.Mvc;
using managing_humanitarian_collections_api.Services;
using managing_humanitarian_collections_api.Entities;
using System.Collections.Generic;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using managing_humanitarian_collections_api.Models.Admin;
using managing_humanitarian_collections_api.Models.User;

namespace managing_humanitarian_collections_api.Controllers
{

    [Route("/api/users")]
    //[ApiController]
    
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }
        [Authorize(Roles = "Admin")]
        [HttpDelete("{id}")]
        public ActionResult BanThisUser([FromRoute] int id)
        {
            _userService.BanUser(id);

            return Ok("Użytkownik został zbanowany");
        }
        [Authorize(Roles = "Admin")]
        [HttpGet]
        public ActionResult<IEnumerable<UsersDto>> GetAll()
        {
            var usersDtos = _userService.GetAll();

            return Ok(usersDtos);
        }

        [HttpGet("{userId}")]
        public ActionResult GetCollectionInfo([FromRoute] int userId)
        {
            var profileDto = _userService.GetUserProfile(userId);
            return Ok(profileDto);
        }

        [HttpPut("{userId}/edit")]
        public ActionResult EditProfile([FromBody] EditProfileDto dto, int userId)
        {

            _userService.EditProfile(dto, userId);
            return Ok("Profil został zedytowany");
        }

    }
}
