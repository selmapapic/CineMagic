using CineMagic.Dal.Context;
using CineMagic.Facade.Models.Projection;
using CineMagic.Facade.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CineMagic.Facade.Efc.Repositories
{
    public class ProjectionsRepository : IProjectionsRespository
    {
        private CineMagicDbContext _dbContext;
        public ProjectionsRepository(CineMagicDbContext dbContext)
        {
            this._dbContext = dbContext;
        }

        public Task<IList<ProjectionGetDetailsRes>> GetAllProjectionsAsync()
        {
            throw new NotImplementedException();
        }

        public async Task<IList<ProjectionGetDetailsRes>> GetProjectionsForMovieAsync(ProjectionGetDetailsReq req)
        {

            IList<ProjectionGetDetailsRes> res = await _dbContext.Projections
                .Where(p => p.MovieId == req.MovieId)
                .Select(p => new ProjectionGetDetailsRes
                {
                    ProjectionTime = p.ProjectionTime,

                }).ToListAsync();

            return res;
        }
    }
}
