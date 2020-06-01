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

namespace CineMagic.Facade.Efc.Repositories
{
    public class ProjectionsRepository : IProjectionsRespository
    {
        private CineMagicDbContext _dbContext;
        private IList<ProjectionRes> monday = new List<ProjectionRes> ();
        private IList<ProjectionRes> tuesday = new List<ProjectionRes>();
        private IList<ProjectionRes> wednesday = new List<ProjectionRes>();
        private IList<ProjectionRes> thursday = new List<ProjectionRes>();
        private IList<ProjectionRes> friday = new List<ProjectionRes>();
        private IList<ProjectionRes> saturday = new List<ProjectionRes>();
        private IList<ProjectionRes> sunday = new List<ProjectionRes>();
        public ProjectionsRepository(CineMagicDbContext dbContext)
        {
            this._dbContext = dbContext;
        }

        public async Task<IList<ProjectionRes>> GetAllProjectionsAsync()
        {
            IList<ProjectionRes> res = await _dbContext.Projections
                .Select(p => new ProjectionRes
                {
                    ProjectionTime = p.ProjectionTime,
                    MovieId = p.MovieId,
                    MovieName = p.Movie.Name
                }).ToListAsync();


            return res;
        }

        private bool IsInThisWeek(DateTime date)
        {
            DayOfWeek now = DateTime.Now.DayOfWeek;
            int daysTillToday = now - DayOfWeek.Monday;
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



        public async Task<IList<ProjectionRes>> GetProjectionsForMovieAsync(ProjectionGetDetailsReq req)
        {

            IList<ProjectionRes> res = await _dbContext.Projections
                .Where(p => p.MovieId == req.MovieId)
                .Select(p => new ProjectionRes
                {
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
    }
}
