﻿using AutoMapper;
using System;
using managing_humanitarian_collections_api.Authorization;
using managing_humanitarian_collections_api.Entities;
using managing_humanitarian_collections_api.Exceptions;
using managing_humanitarian_collections_api.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Linq;

namespace managing_humanitarian_collections_api.Services
{
    #region Intefejsy
    public interface ICommentService
    {
        int CreateComment(int collectionId, CreateCommentDto dto);
        IEnumerable<CommentDto> GetAllCollectionComments(int collectionId);
    }

    #endregion
    public class CommentService : ICommentService
    {
        private readonly ManagingCollectionsDbContext _dbContext;
        private readonly IMapper _mapper;
        private readonly ILogger _logger;
        private readonly IAuthorizationService _authorizationService;
        private readonly IUserContextService _userContextService;

        public CommentService(ManagingCollectionsDbContext dbContext, IMapper mapper, ILogger<CollectionService> logger, IAuthorizationService authorizationService, IUserContextService userContextService)
        {
            _dbContext = dbContext;
            _mapper = mapper;
            _logger = logger;
            _authorizationService = authorizationService;
            _userContextService = userContextService;
        }
        #region Utworzenie komentarz
        public int CreateComment(int collectionId, CreateCommentDto dto)
        {

            //var collection = GetCollectionById(collectionId);

            var comment = _mapper.Map<Comment>(dto);
            comment.CreatedById = _userContextService.GetUserId;
            comment.CollectionId = collectionId;
            _dbContext.Comments.Add(comment);
            _dbContext.SaveChanges();

            return comment.Id;
        }

        public IEnumerable<CommentDto> GetAllCollectionComments(int collectionId)
        {
            var comments = _dbContext
                .Comments
                .Include(r => r.CreatedBy)
                .ThenInclude(r => r.Profile)
                .Where(r => r.CollectionId == collectionId)
                .ToList();

            if (comments is null) throw new NotFoundException("Nie znaleziono komentarzy");

            var commentDtos = _mapper.Map<List<CommentDto>>(comments);

            return commentDtos;
        }
        #endregion


    }
}