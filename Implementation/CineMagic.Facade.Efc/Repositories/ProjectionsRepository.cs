using CineMagic.Dal.Context;
using CineMagic.Dal.Entities;
using CineMagic.Facade.Models.Projection;
using CineMagic.Facade.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CineMagic.Facade.Models.PlayingNow;
using CineMagic.Facade.Models.CinemaHall;
using CineMagic.Facade.Models.Seat;

namespace CineMagic.Facade.Efc.Repositories
{
    public class ProjectionsRepository : IProjectionsRespository
    {
        private CineMagicDbContext _dbContext;
        private ICinemaHallRepository _cinemaHallRepository;

        private IList<ProjectionRes> monday = new List<ProjectionRes> ();
        private IList<ProjectionRes> tuesday = new List<ProjectionRes>();
        private IList<ProjectionRes> wednesday = new List<ProjectionRes>();
        private IList<ProjectionRes> thursday = new List<ProjectionRes>();
        private IList<ProjectionRes> friday = new List<ProjectionRes>();
        private IList<ProjectionRes> saturday = new List<ProjectionRes>();
        private IList<ProjectionRes> sunday = new List<ProjectionRes>();

        public ProjectionsRepository(CineMagicDbContext dbContext, ICinemaHallRepository cinemaHallRepository)
        {
            this._dbContext = dbContext;
            this._cinemaHallRepository = cinemaHallRepository;
        }

        public async Task<IList<ProjectionRes>> GetAllProjectionsAsync()
        {
            IList<ProjectionRes> res = await _dbContext.Projections
                .Select(p => new ProjectionRes
                {
                    ProjectionTime = p.ProjectionTime,
                    MovieId = p.MovieId,
                    MovieName = p.Movie.Name,

                }).ToListAsync();


            return res;
        }

        private bool IsInThisWeek(DateTime date)
        {
            DayOfWeek now = DateTime.Now.DayOfWeek;
            int daysTillToday;

            if (now == DayOfWeek.Sunday)
            {
                daysTillToday = 6;
            }
            else
            {
                daysTillToday = now - DayOfWeek.Monday;
            }
            DateTime thisWeekMonday = DateTime.Now.AddDays(-daysTillToday);
            DateTime thisWeekSunday = thisWeekMonday.AddDays(6);
            if (DateTime.Compare(date.Date, thisWeekMonday.Date) >= 0 && DateTime.Compare(date.Date, thisWeekSunday.Date) <= 0) return true;
            return false;
        }

        public async Task<PlayingNowGetDetailsRes> GetAllProjectionsByDaysAsync()
        {
            IList<ProjectionRes> res = await _dbContext.Projections
                .Select(p => new ProjectionRes
                {
                    ProjectionTime = p.ProjectionTime,
                    MovieId = p.MovieId,
                    MovieName = p.Movie.Name,
                    PosterURL = p.Movie.PosterUrl
                }).ToListAsync();

            foreach(var item in res)
            {
                if (IsInThisWeek(item.ProjectionTime) && item.ProjectionTime.DayOfWeek == DayOfWeek.Monday) monday.Add(item);
                else if (IsInThisWeek(item.ProjectionTime) && item.ProjectionTime.DayOfWeek == DayOfWeek.Tuesday) tuesday.Add(item);
                else if (IsInThisWeek(item.ProjectionTime) && item.ProjectionTime.DayOfWeek == DayOfWeek.Wednesday) wednesday.Add(item);
                else if (IsInThisWeek(item.ProjectionTime) && item.ProjectionTime.DayOfWeek == DayOfWeek.Thursday) thursday.Add(item);
                else if (IsInThisWeek(item.ProjectionTime) && item.ProjectionTime.DayOfWeek == DayOfWeek.Friday) friday.Add(item);
                else if (IsInThisWeek(item.ProjectionTime) && item.ProjectionTime.DayOfWeek == DayOfWeek.Saturday) saturday.Add(item);
                else if (IsInThisWeek(item.ProjectionTime) && item.ProjectionTime.DayOfWeek == DayOfWeek.Sunday) sunday.Add(item);
            }

            return new PlayingNowGetDetailsRes
            {
                MondayProjections = monday,
                TuesdayProjections = tuesday,
                WednesdayProjections = wednesday,
                ThursdayProjections = thursday,
                FridayProjections = friday,
                SaturdayProjections = saturday,
                SundayProjections = sunday
            };
        }



        public async Task<IList<ProjectionRes>> GetProjectionsForMovieAsync(ProjectionGetDetailsByMovieIdReq req)
        {

            IList<ProjectionRes> res = await _dbContext.Projections
                .Where(p => p.MovieId == req.MovieId)
                .Select(p => new ProjectionRes
                {
                    Id = p.Id,
                    ProjectionTime = p.ProjectionTime,
                    MovieId = p.MovieId,
                    MovieName = p.Movie.Name
                    //CinemaHall =  _dbContext.CinemaHalls
                    //    .Where(c => c.Id == p.CinemaHallId)
                    //    .Select(c => new CinemaHall
                    //    {

                    //    }).FirstOrDefault(),

                }).ToListAsync();

            return res;
        }

        public async Task<ProjectionRes> GetProjectionById(ProjectionGetDetailsReq req)
        {
            IList<AvailableSeatGetDetailsRes> seatGetDetailsRes = await _dbContext.AvailableSeats
                .Where(avs => avs.ProjectionId == req.Id)
                .Select(s => new AvailableSeatGetDetailsRes
                {
                    Id = s.Id,
                    SeatId = s.SeatId,
                    ProjectionId = req.Id,
                    Name = s.Seat.Name
                }).ToListAsync();
            

            ProjectionRes res = await _dbContext.Projections
                .Where(p => p.Id == req.Id)
                .Select(p => new ProjectionRes
                {
                    Id = p.Id,
                    ProjectionTime = p.ProjectionTime,
                    MovieId = p.MovieId,
                    MovieName = p.Movie.Name,
                    CinemaHall = new CinemaHallGetDetailsRes
                    {
                        Id = p.CinemaHallId,
                        HallImageUrl = p.CinemaHall.HallImageUrl
                    },
                    AvailableSeats = seatGetDetailsRes
                }).FirstOrDefaultAsync();

            return res;
        }

        public Task<IList<ProjectionRes>> GetProjectionsForMovieAsync(ProjectionGetDetailsReq req)
        {
            throw new NotImplementedException();
            //IMPLEMENTIRATI OVO
        }

        public async Task<Boolean> AddProjection(Projection projection)
        {

            try
            {
                _dbContext.Add(projection);
                await _dbContext.SaveChangesAsync();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task<Projection> GetProjectionEntityClassWithId(ProjectionGetDetailsReq req)
        {
            Projection projection = await _dbContext.Projections.Where(p => p.Id == req.Id).FirstOrDefaultAsync();
            return projection;
        }

        public async Task<Boolean> DeleteProjection(int id)
        {
            Projection projection = await _dbContext.Projections.Where(p => p.Id == id).FirstOrDefaultAsync();
            try
            {

                _dbContext.Projections.Remove(projection);
                await _dbContext.SaveChangesAsync();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task<Boolean> EditProjection(Projection projection)
        {
            try
            {
                _dbContext.Update(projection);
                await _dbContext.SaveChangesAsync();
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
