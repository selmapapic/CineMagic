using CineMagic.Facade.Models.Projection;
using System.Collections.Generic;
using System.Threading.Tasks;
using CineMagic.Facade.Models.PlayingNow;

namespace CineMagic.Facade.Repositories
{
    public interface IProjectionsRespository
    {
        Task<IList<ProjectionRes>> GetProjectionsForMovieAsync(ProjectionGetDetailsByMovieIdReq req);
        Task<IList<ProjectionRes>> GetAllProjectionsAsync();
        Task<PlayingNowGetDetailsRes> GetAllProjectionsByDaysAsync();
        Task<ProjectionRes> GetProjectionById(ProjectionGetDetailsReq req);
    }
}
