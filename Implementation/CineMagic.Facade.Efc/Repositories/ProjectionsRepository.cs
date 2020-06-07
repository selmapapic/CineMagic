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
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore.SqlServer.Query.Internal;

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
                    Id=p.Id,
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


            IList<CinemaHallGetDetailsRes> cinemaHalls = await _dbContext.CinemaHalls.Select
            (c => new CinemaHallGetDetailsRes
            {
                Id = c.Id,
                HallImageUrl = c.HallImageUrl,
                numberOfSeats = c.AllSeats.Count()
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
                    AvailableSeats = seatGetDetailsRes,
                    AllCinemaHalls = cinemaHalls
                }).FirstOrDefaultAsync();

            return res;
        }

        public Task<IList<ProjectionRes>> GetProjectionsForMovieAsync(ProjectionGetDetailsReq req)
        {
            throw new NotImplementedException();
            //IMPLEMENTIRATI OVO
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

        public async Task<IList<CinemaHallGetDetailsRes>> GetAllCinemaHalls()
        {
            IList<CinemaHallGetDetailsRes> cinemaHalls = await _dbContext.CinemaHalls.Select
                ( c => new CinemaHallGetDetailsRes
                {
                    Id = c.Id,
                    HallImageUrl = c.HallImageUrl,
                    numberOfSeats = c.AllSeats.Count()
                }).ToListAsync();

            return cinemaHalls;
        }

        public async Task<bool> AddProjections(ProjectionRes res)
        {
            try
            {
                Movie movie = await _dbContext.Movies.Where(m => m.Name == res.MovieName).FirstOrDefaultAsync();
                CinemaHall cinHall = await _dbContext.CinemaHalls.Where(m => m.Id == res.CinemaHallId).FirstOrDefaultAsync();



                Projection projection = new Projection
                {

                    ProjectionTime = res.ProjectionTime,
                    MovieId = movie.Id,

                    CinemaHallId = cinHall.Id,

                };


                _dbContext.Add(projection);
                await _dbContext.SaveChangesAsync();

                Projection project = _dbContext.Projections.Where(m => m.ProjectionTime == res.ProjectionTime && m.MovieId == movie.Id).FirstOrDefault();

                List<AvailableSeatGetDetailsRes> avalSeats = await _dbContext.Seats
                    .Where(s => s.CinemaHallId == project.CinemaHallId)
                    .Select(avs => new AvailableSeatGetDetailsRes
                    {
                        ProjectionId = project.Id,
                        SeatId = avs.Id,
                        Name = avs.Name
                    }).ToListAsync();

                foreach (var avSeat in avalSeats)
                {
                    AvailableSeat availableSeat = new AvailableSeat
                    {
                        ProjectionId = avSeat.ProjectionId,
                        SeatId = avSeat.SeatId
                    };

                    _dbContext.AvailableSeats.Add(availableSeat);
                    await _dbContext.SaveChangesAsync();

                }

                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task<bool> EditProjections(ProjectionRes res)
        {
            try
            {
                Movie movie = await _dbContext.Movies.Where(m => m.Name == res.MovieName).FirstOrDefaultAsync();
                CinemaHall cinHall = await _dbContext.CinemaHalls.Where(m => m.Id == res.CinemaHallId).FirstOrDefaultAsync();

                //IEnumerable<AvailableSeat> available = await _dbContext.CinemaHalls.Where(m => m.Id == res.CinemaHallId).AllSeats.Select(p => new AvailableSeat
                //{
                //    ProjectionId = res.Id,
                //    SeatId = p.SeatNumber
                //};



                Projection projection = new Projection
                {

                    ProjectionTime = res.ProjectionTime,
                    MovieId = movie.Id,

                    CinemaHallId = cinHall.Id,

                };


                _dbContext.Add(projection);
                await _dbContext.SaveChangesAsync();

                Projection project = _dbContext.Projections.Where(m => m.ProjectionTime == res.ProjectionTime && m.MovieId == movie.Id).FirstOrDefault();

                foreach (var seat in cinHall.AllSeats)
                {

                    Seat seat1 = _dbContext.Seats.Where(m => m.Id == seat.Id).FirstOrDefault();
                    if (seat1 == null)
                    {
                        Seat newSeat = new Seat
                        {
                            Name = seat.Name
                        };
                        _dbContext.Seats.Add(newSeat);
                        await _dbContext.SaveChangesAsync();
                    }

                    Seat addedSeat = _dbContext.Seats.Where(m => m.Id == seat.Id).FirstOrDefault();
                    AvailableSeat avl = new AvailableSeat
                    {
                        ProjectionId = project.Id,
                        SeatId = addedSeat.Id
                    };
                    _dbContext.AvailableSeats.Add(avl);
                }

                return true;
            }
            catch
            {
                return false;
            }
            //    Movie movie = await _dbContext.Movies.Where(m => m.Name == res.MovieName).FirstOrDefaultAsync();
            //    CinemaHall cinHall = await _dbContext.CinemaHalls.Where(m => m.Id == res.CinemaHallId).FirstOrDefaultAsync();
            //    Projection projection = new Projection
            //    {
            //        Id=res.Id,
            //        ProjectionTime = res.ProjectionTime,
            //        MovieId = movie.Id,
            //        Movie = movie,
            //        CinemaHallId = cinHall.Id,
            //        CinemaHall = cinHall
            //    };
            //    _dbContext.Update(projection);
            //    await _dbContext.SaveChangesAsync();
            //    return true;
            //}
            //catch
            //{
            //    return false;
        
            
        }

        public  async Task<bool> DeleteProjections(int id)
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

        public async Task<IList<ProjectionRes>> GetProjections()
        {
            IList<CinemaHallGetDetailsRes> cinemaHalls = await _dbContext.CinemaHalls.Select
                (c => new CinemaHallGetDetailsRes
                {
                    Id = c.Id,
                    HallImageUrl = c.HallImageUrl,
                    numberOfSeats = c.AllSeats.Count(),
                    
                }).ToListAsync();
                    
            //IList<AvailableSeatGetDetailsRes> seatGetDetailsRes = await _dbContext.AvailableSeats
            //   .Where(avs => avs.ProjectionId == req.Id)
            //   .Select(s => new AvailableSeatGetDetailsRes
            //   {
            //       Id = s.Id,
            //       SeatId = s.SeatId,
            //       ProjectionId = req.Id,
            //       Name = s.Seat.Name
            //   }).ToListAsync();

            IList<ProjectionRes> res = await _dbContext.Projections
               .Select(p => new ProjectionRes
               {
                   Id = p.Id,
                   ProjectionTime = p.ProjectionTime,
                   MovieId = p.MovieId,
                   MovieName = p.Movie.Name,
                   CinemaHallId = p.CinemaHallId,
                   CinemaHall = new CinemaHallGetDetailsRes
                   {
                       Id = p.CinemaHall.Id,
                       HallImageUrl = p.CinemaHall.HallImageUrl,
                      numberOfSeats = p.CinemaHall.SeatNumber
                   },
                   AllCinemaHalls = cinemaHalls
                  
                }).ToListAsync();


            return res;
        }
    }
}
