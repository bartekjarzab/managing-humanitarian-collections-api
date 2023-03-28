using AutoMapper;
using managing_humanitarian_collections_api.Authorization;
using managing_humanitarian_collections_api.Entities;
using managing_humanitarian_collections_api.Exceptions;
using managing_humanitarian_collections_api.Models.Admin;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Linq;

namespace managing_humanitarian_collections_api.Services
{
    #region Intefejsy
    public interface IAdminService
    {
        List<UsersDto> GetAll();
        void BanUser(int id);
    }

    #endregion
    public class AdminService : IAdminService
    {
        private readonly ManagingCollectionsDbContext _dbContext;
        private readonly IMapper _mapper;
        private readonly ILogger _logger;
        private readonly IAuthorizationService _authorizationService;
        private readonly IUserContextService _userContextService;

        public AdminService(ManagingCollectionsDbContext dbContext, IMapper mapper, ILogger<AdminService> logger, IAuthorizationService authorizationService, IUserContextService userContextService)
        {
            _dbContext = dbContext;
            _mapper = mapper;
            _logger = logger;
            _authorizationService = authorizationService;
            _userContextService = userContextService;
        }

        public void BanUser(int id)
        {
            _logger.LogError($"Uzytkownik o Id {id} został zbanowany");

            var user = _dbContext
                .Users
                .FirstOrDefault(r => r.Id == id);

            if (user is null)
                throw new NotFoundException("Nie znaleziono użytkownika");

            _dbContext.Users.Remove(user);
            _dbContext.SaveChanges();

        }
        #region Lista wszystkich Uzytkowników 
        public List<UsersDto> GetAll()
        {
            var users = _dbContext
                .Users
                .Include(u => u.Role)
                .ToList();

            if (users is null) throw new NotFoundException("Użytkownik nie znaleziony");

            var usersDtos = _mapper.Map<List<UsersDto>>(users);

            return usersDtos;
        }
        #endregion
        }
}
