using Microsoft.AspNetCore.Mvc;
using managing_humanitarian_collections_api.Services;
using managing_humanitarian_collections_api.Entities;
using System.Collections.Generic;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using managing_humanitarian_collections_api.Models.Admin;

namespace managing_humanitarian_collections_api.Controllers
{

    [Route("/api/admin/users")]
    //[ApiController]
    [Authorize(Roles ="Admin")]
    public class AdminController : ControllerBase
    {
        private readonly IAdminService _adminService;

        public AdminController(IAdminService adminService)
        {
            _adminService = adminService;
        }
      
        [HttpDelete("{id}")]
        public ActionResult BanThisUser([FromRoute] int id)
        {
            _adminService.BanUser(id);

            return NoContent();
        }

        [HttpGet]
        public ActionResult<IEnumerable<UsersDto>> GetAll()
        {
            var usersDtos = _adminService.GetAll();

            return Ok(usersDtos);
        }

    }
}
