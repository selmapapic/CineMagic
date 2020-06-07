using CineMagic.Facade.Models.Projection;
using System.Collections.Generic;
using System.Threading.Tasks;
using CineMagic.Facade.Models.PlayingNow;
using CineMagic.Dal.Entities;
using System;
using CineMagic.Facade.Models.CinemaHall;

namespace CineMagic.Facade.Repositories
{
    public interface IProjectionsRespository
    {
        Task<IList<ProjectionRes>> GetProjectionsForMovieAsync(ProjectionGetDetailsByMovieIdReq req);
        Task<IList<ProjectionRes>> GetAllProjectionsAsync();
        Task<PlayingNowGetDetailsRes> GetAllProjectionsByDaysAsync();
        Task<ProjectionRes> GetProjectionById(ProjectionGetDetailsReq req);
        
        public Task<Projection> GetProjectionEntityClassWithId(ProjectionGetDetailsReq projectionGetDetailsReq);
       

        Task<IList<CinemaHallGetDetailsRes>> GetAllCinemaHalls();
        public Task<Boolean> AddProjections(ProjectionRes projection);
        Task<Boolean> EditProjections(ProjectionRes projection);
        Task<Boolean> DeleteProjections(int id);
        Task<IList<ProjectionRes>> GetProjections();
    }
}
