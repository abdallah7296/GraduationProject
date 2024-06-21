using GraduationProject.DTO.DTOForWorkspace;
using GraduationProject.DTO.DTOPharmacies;
using GraduationProject.DTO.DTOPlayStation;
using GraduationProject.DTO.DTOReview;
using GraduationProject.DTO.Images;
using GraduationProject.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Connections;
using Microsoft.EntityFrameworkCore;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using ZendeskApi_v2.Models.Views.Executed;

namespace GraduationProject.Services.PlayStationServices
{
    public class PlayStationRepository : IPlayStationRepository
    {
        Context context = new Context();
        private readonly IHttpContextAccessor httpContextAccessor;

        public PlayStationRepository(IHttpContextAccessor httpContextAccessor)
        {
            this.httpContextAccessor = httpContextAccessor;
        }

        public int Create(AddPlaystationDto dto, List<IFormFile> imageFiles)
        {
            PlayStation service = new PlayStation();
            service.Name = dto.Name;
            service.PhoneNumber = dto.PhoneNumber;
            service.City = dto.City;
            service.Street = dto.Street;
            service.DescriptionOfPlace = dto.DescriptionOfPlace;
            service.LinkOfPlace = dto.LinkOfPlace;
            service.StartWork = dto.StartWork;
            service.EndWork = dto.EndWork;
            service.Latitude = dto.Latitude;
            service.Longitude = dto.Longitude;
            context.playStations.Add(service);
            context.SaveChanges();
            foreach (var file in imageFiles)
            {
                string fileName = file.FileName;
                string filePath = Path.GetFullPath(Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\imgs"));
                using (var fileStream = new FileStream(Path.Combine(filePath, fileName), FileMode.Create))
                {
                    file.CopyTo(fileStream);
                }
                Images image = new Images();
                image.ServicId = service.Id;
                image.Image = fileName;
                image.serviceName = service.Name;
                context.images.Add(image);
                context.SaveChanges();
            }
            return service.Id;
        }

        public void Delete(int id)
        {
            PlayStation service = context.playStations.Include(p=>p.games).SingleOrDefault(d => d.Id == id);
            List<Images> images = context.images.Where(m => m.ServicId == service.Id).
                Where(s => s.serviceName == service.Name).ToList();
            foreach (var image in images)
            {
                context.images.Remove(image);
            }
            foreach (var gm in service.games)
            {
                context.games.Remove(gm);
            }
            context.playStations.Remove(service);
            context.SaveChanges();
        }

        public void Update(int id, AddPlaystationDto dto)
        {
            PlayStation service = context.playStations.FirstOrDefault(s => s.Id == id);
            service.Name = dto.Name;
            service.PhoneNumber = dto.PhoneNumber;
            service.City = dto.City;
            service.Street = dto.Street;
            service.DescriptionOfPlace = dto.DescriptionOfPlace;
            service.LinkOfPlace = dto.LinkOfPlace;
            service.StartWork = dto.StartWork;
            service.EndWork = dto.EndWork;
            service.Latitude = dto.Latitude;
            service.Longitude = dto.Longitude;
            context.SaveChanges();
            
        }
        public int CreateGameForPlaystation(AddGamesDto dto)
        {
            Games game = new Games();
            game.description = dto.description;
            game.name = dto.name;
            game.type = dto.type;
            PlayStation playStation = context.playStations.FirstOrDefault(p => p.Name == dto.NameOfPlaystation);
            game.PlayStationId = playStation.Id;
            context.games.Add(game);
            context.SaveChanges();
            return game.id;
        }

        public void DeleteGame(int id)
        {
            Games game = context.games.FirstOrDefault(g => g.id == id);
            context.games.Remove(game);
            context.SaveChanges();
        }

        public void UpdateGame(int id, AddGamesDto dto)
        {
            Games game = context.games.FirstOrDefault(g => g.id == id);
            game.description = dto.description;
            game.name = dto.name;
            game.type = dto.type;
            context.SaveChanges();
        }

        //..............................................................................
        public List<DTOPlayStation> GetAllPlayStations()
        {
            List<PlayStation> playStations = context.playStations.Include(r => r.games).ToList();
            if (!playStations.Any()  )
                return null;
            List<DTOPlayStation> dTOPlayStations = new List<DTOPlayStation>();
            foreach(var  st in playStations)
            {
                DTOPlayStation dTR = new DTOPlayStation();
                dTR.Name = st.Name;
                dTR.Street = st.Street;
                dTR.City = st.City;
                dTR.DescriptionOfPlace = st.DescriptionOfPlace;
                dTR.LinkOfPlace = st.LinkOfPlace;
                dTR.PriceOfHour = st.PriceOfHour;
                dTR.PhoneNumber = st.PhoneNumber;
                dTR.StartWork = st.StartWork;
                dTR.EndWork = st.EndWork;
                dTR.Longitude = st.Longitude;
                dTR.Latitude = st.Latitude;
                dTR.id = st.Id;
                List<string> imagesDto = new List<string>();
                List<Images> imgs = context.images.Where(i => i.ServicId == st.Id).
                    Where(i => i.serviceName == st.Name).ToList();
                foreach (var img in imgs)
                {
                    //  ImagesDto imageDto = new ImagesDto();
                    // imageDto.Image = img.Image;
                    HttpContext httpContext = httpContextAccessor.HttpContext;
                    imagesDto.Add($"{httpContext.Request.Scheme}://{httpContext.Request.Host}/imgs/{img.Image}");
                }
                dTR.Images = imagesDto;
                List<DTOGames> games = new List<DTOGames>();
                foreach (var gm in st.games)
                {
                    DTOGames dTOGames = new DTOGames();
                    dTOGames.name = gm.name;
                    dTOGames.description = gm.description;
                    dTOGames.type = gm.type;
                    dTOGames.id = gm.id;
                    games.Add(dTOGames);
                }
                dTR.dTOGames = games;
                dTOPlayStations.Add(dTR);
            }

            return dTOPlayStations;
        }
        public List<PlayStation> GetAllPlayStationsBySortReview()
        {


            List<PlayStation> PlayStations = context.playStations.ToList();
            foreach (var playStation in PlayStations)
            {
                List<Review> reviews = context.Reviews.Where(r => r.ServicId == playStation.Id).
                    Where(i => i.serviceName == playStation.Name).ToList();


                if (reviews.Any())
                {
                    int averageRate = (int)reviews.Average(r => r.Rating);
                    playStation.averageRate = averageRate;
                    context.SaveChanges();
                }
            }
            return PlayStations;
        }
        public List<DTOPlayStation> GetAllPlayStationsActiveNow()//user
        {
            DateTime now = DateTime.Now;
            List<PlayStation> playStations = context.playStations.Include(r => r.games)
                .Where(d => d.StartWork <= now && d.EndWork >= now).ToList();
            if (!playStations.Any())
                return null;
            List<DTOPlayStation> dTOPlayStations = new List<DTOPlayStation>();
            foreach (var st in playStations)
            {
                DTOPlayStation dTR = new DTOPlayStation();
                dTR.Name = st.Name;
                dTR.Street = st.Street;
                dTR.City = st.City;
                dTR.DescriptionOfPlace = st.DescriptionOfPlace;
                dTR.LinkOfPlace = st.LinkOfPlace;
                dTR.PriceOfHour = st.PriceOfHour;
                dTR.PhoneNumber = st.PhoneNumber;
                dTR.StartWork = st.StartWork;
                dTR.EndWork = st.EndWork;
                dTR.Latitude = st.Latitude;
                dTR.Longitude = st.Longitude;
                dTR.id = st.Id;
                List<string> imagesDto = new List<string>();
                List<Images> imgs = context.images.Where(i => i.ServicId == st.Id).
                    Where(i => i.serviceName == st.Name).ToList();
                foreach (var img in imgs)
                {
                    //  ImagesDto imageDto = new ImagesDto();
                    // imageDto.Image = img.Image;
                    HttpContext httpContext = httpContextAccessor.HttpContext;
                    imagesDto.Add($"{httpContext.Request.Scheme}://{httpContext.Request.Host}/imgs/{img.Image}");
                }
                dTR.Images = imagesDto;
                List<DTOGames> games = new List<DTOGames>();
                foreach (var gm in st.games)
                {
                    DTOGames dTOGames = new DTOGames();
                    dTOGames.name = gm.name;
                    dTOGames.description = gm.description;
                    dTOGames.type = gm.type;
                    dTOGames.id = gm.id;
                    games.Add(dTOGames);
                }
                dTR.dTOGames = games;
                dTOPlayStations.Add(dTR);
            }

            return dTOPlayStations;
        }

        public DTOPlayStation GetPlayStationByID(int id)
        {
            PlayStation st = context.playStations.Include(r => r.games).SingleOrDefault(p => p.Id == id);
            if (st  == null)
                return null;
            DTOPlayStation  dTR = new DTOPlayStation();
           
                dTR.Name = st.Name;
                dTR.Street = st.Street;
                dTR.City = st.City;
                dTR.DescriptionOfPlace = st.DescriptionOfPlace;
                dTR.LinkOfPlace = st.LinkOfPlace;
                dTR.PriceOfHour = st.PriceOfHour;
                dTR.PhoneNumber = st.PhoneNumber;
                dTR.StartWork = st.StartWork;
                dTR.EndWork = st.EndWork;
                dTR.Longitude = st.Longitude;
                dTR.Latitude = st.Latitude;
                dTR.id = st.Id;

            List<string> imagesDto = new List<string>();
            List<Images> imgs = context.images.Where(i => i.ServicId == st.Id).
                Where(i => i.serviceName == st.Name).ToList();
            foreach (var img in imgs)
            {
                //  ImagesDto imageDto = new ImagesDto();
                // imageDto.Image = img.Image;
                HttpContext httpContext = httpContextAccessor.HttpContext;
                imagesDto.Add($"{httpContext.Request.Scheme}://{httpContext.Request.Host}/imgs/{img.Image}");
            }
            dTR.Images = imagesDto;
            List<DTOGames> games = new List<DTOGames>();
                foreach (var gm in st.games)
                {
                    DTOGames dTOGames = new DTOGames();
                    dTOGames.name = gm.name;
                    dTOGames.description = gm.description;
                    dTOGames.type = gm.type;
                    dTOGames.id = gm.id;
                    games.Add(dTOGames);
               }
                dTR.dTOGames = games;

         return dTR;    
       }

        public List<DTOPlayStation> Search(string name)
        {

           List<PlayStation> playStations = context.playStations.Include(r => r.games)
                      .Where(p => p.Name.Contains(name) ||
                  p.PhoneNumber.Contains(name) || p.Street.Contains(name) || 
                 p.PriceOfHour.ToString().Contains(name) ).ToList();
            if (!playStations.Any())
                return null;
            List<DTOPlayStation> dTOPlayStations = new List<DTOPlayStation>();
            foreach (var st in playStations)
            {
                DTOPlayStation dTR = new DTOPlayStation();
                dTR.Name = st.Name;
                dTR.Street = st.Street;
                dTR.City = st.City;
                dTR.DescriptionOfPlace = st.DescriptionOfPlace;
                dTR.LinkOfPlace = st.LinkOfPlace;
                dTR.PriceOfHour = st.PriceOfHour;
                dTR.PhoneNumber = st.PhoneNumber;
                dTR.StartWork = st.StartWork;
                dTR.EndWork = st.EndWork;
                dTR.Latitude = st.Latitude;
                dTR.Longitude = st.Longitude;
                dTR.id = st.Id;
                List<string> imagesDto = new List<string>();
                List<Images> imgs = context.images.Where(i => i.ServicId == st.Id).
                    Where(i => i.serviceName == st.Name).ToList();
                foreach (var img in imgs)
                {
                    //  ImagesDto imageDto = new ImagesDto();
                    // imageDto.Image = img.Image;
                    HttpContext httpContext = httpContextAccessor.HttpContext;
                    imagesDto.Add($"{httpContext.Request.Scheme}://{httpContext.Request.Host}/imgs/{img.Image}");
                }
                dTR.Images = imagesDto;
                List<DTOGames> games = new List<DTOGames>();
                foreach (var gm in st.games)
                {
                    DTOGames dTOGames = new DTOGames();
                    dTOGames.name = gm.name;
                    dTOGames.description = gm.description;
                    dTOGames.type = gm.type;
                    dTOGames.id = gm.id;
                    games.Add(dTOGames);
                }
                dTR.dTOGames = games;
                dTOPlayStations.Add(dTR);
            }

            return dTOPlayStations;
        }
        public List<DTOGames> GetAllGamesByPlaystationId(int PlayStationId)
        {
            PlayStation st = context.playStations.Include(r => r.games).
                SingleOrDefault(p => p.Id == PlayStationId);
            if (st == null)
                return null;
            List<DTOGames> games = new List<DTOGames>();
            foreach (var gm in st.games)
            {
                DTOGames dTOGames = new DTOGames();
                dTOGames.name = gm.name;
                dTOGames.id = gm.id;
                dTOGames.description = gm.description;
                dTOGames.type = gm.type;
                games.Add(dTOGames);
            }
            return games;
        }
        //Create,,delete,,update for review.............................................................................
        public int CreateReview(string U,int PlaystationId, DTOReview dTOReview)
        {
           PlayStation playStation = context.playStations.SingleOrDefault
                (d => d.Id == PlaystationId);
            Review review = new Review();
            review.ReviewDate = DateTime.Now;
            review.Rating = dTOReview.Rating;
            review.Comment = dTOReview.Comment;
            review.ServicId = PlaystationId;
            review.serviceName = dTOReview.serviceName;
            review.UserId = U;
            //   var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            //  review.UserId = "4d01d3c0-7ee9-4654-b272-47dc4d275eec";
            context.Reviews.Add(review);
            context.SaveChanges();
          //  playStation.Reviews.Add(review);

            return review.Id;
        }

        public void DeleteReview(int id)
        {
            Review review = context.Reviews.FirstOrDefault(r => r.Id == id);
            context.Reviews.Remove(review);
            context.SaveChanges();
        }

        public void UpdateReview(int id, DTOReview dTOReview)
        {
            Review oldreview = context.Reviews.FirstOrDefault(r => r.Id == id);
            oldreview.ReviewDate = DateTime.Now;
            oldreview.Rating = dTOReview.Rating;
            oldreview.Comment = dTOReview.Comment;
            context.SaveChanges();
        }
        public List<DTOOReview> GetAllReviews(int ServiceId,string name)
        {
            List<Review> reviews = context.Reviews.Where(r => r.ServicId == ServiceId).
                Where(r=>r.serviceName==name).ToList();
            List<DTOOReview> dTOReviews = new List<DTOOReview>();
            foreach (var review in reviews)
            {
                DTOOReview dTOReview = new DTOOReview();
                //  dTOReview.ReviewDate = review.ReviewDate;
                dTOReview.Comment = review.Comment;
                dTOReview.Rating = review.Rating;
                dTOReview.ReviewDate = review.ReviewDate;
                dTOReview.serviceName = review.serviceName;

                dTOReviews.Add(dTOReview);
            }

            return dTOReviews;
        }

     
    }
}
