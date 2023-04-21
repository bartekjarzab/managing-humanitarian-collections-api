using AutoMapper;
using managing_humanitarian_collections_api.Authorization;
using managing_humanitarian_collections_api.Entities;
using managing_humanitarian_collections_api.Exceptions;
using managing_humanitarian_collections_api.Models.Admin;
using managing_humanitarian_collections_api.Models.User;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Linq;

namespace managing_humanitarian_collections_api.Services
{
    #region Intefejsy
    public interface IUserService
    {
        List<UsersDto> GetAll();
        void BanUser(int id);
        UserProfileDto GetUserProfile(int id);
        void EditProfile(EditProfileDto dto, int id);
    }

    #endregion
    public class UserService : IUserService
    {
        private readonly ManagingCollectionsDbContext _dbContext;
        private readonly IMapper _mapper;
        private readonly ILogger _logger;
        private readonly IAuthorizationService _authorizationService;
        private readonly IUserContextService _userContextService;

        public UserService(ManagingCollectionsDbContext dbContext, IMapper mapper, ILogger<UserService> logger, IAuthorizationService authorizationService, IUserContextService userContextService)
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
                .Where(r => r.RoleId <= 2)
                .ToList();

            if (users is null) throw new NotFoundException("Użytkownik nie znaleziony");

            var usersDtos = _mapper.Map<List<UsersDto>>(users);

            return usersDtos;
        }
        #endregion

        public UserProfileDto GetUserProfile(int id)
        {
            var profile = _dbContext
                .Users
                .Include(r => r.Role)
                .Include(r => r.Profile)
                .ThenInclude(r=> r.Avatar)
                .FirstOrDefault(r => r.Id == id);

            var profileDto = _mapper.Map<UserProfileDto>(profile);

            return profileDto;
        }

        public void EditProfile(EditProfileDto dto, int id)
        {
            var profile = _dbContext.Profiles.FirstOrDefault(p => p.User.Id == id);

            if (dto.FirstName != null)
            {
                profile.FirstName = dto.FirstName;
            }
            if (dto.LastName != null)
            {
                profile.LastName = dto.LastName;
            }
            if (dto.Name != null)
            {
                profile.Name = dto.Name;
            }
            if (dto.Nip != null)
            {
                profile.Nip = dto.Nip;
            }
            if (dto.Regon != null)
            {
                profile.Regon = dto.Regon;
            }
            if (dto.ContactNumber != null)
            {
                profile.ContactNumber = dto.ContactNumber;
            }
            if (dto.AvatarId != null)
            {
                profile.AvatarId = dto.AvatarId;
            }
            _dbContext.SaveChanges();
        }

    }


}
