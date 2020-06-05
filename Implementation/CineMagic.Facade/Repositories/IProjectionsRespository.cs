﻿using CineMagic.Facade.Models.Projection;
using System.Collections.Generic;
using System.Threading.Tasks;
using CineMagic.Facade.Models.PlayingNow;
using CineMagic.Dal.Entities;
using System;

namespace CineMagic.Facade.Repositories
{
    public interface IProjectionsRespository
    {
        Task<IList<ProjectionRes>> GetProjectionsForMovieAsync(ProjectionGetDetailsReq req);
        Task<IList<ProjectionRes>> GetAllProjectionsAsync();
        Task<PlayingNowGetDetailsRes> GetAllProjectionsByDaysAsync();
        public Task<Boolean> AddProjection(Projection projection);
    }
}
