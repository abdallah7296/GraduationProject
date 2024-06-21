using GraduationProject.DTO.DTOForWorkspace;
using GraduationProject.DTO.DTOReview;
using GraduationProject.DTO.SuperMarket;
using GraduationProject.Models;
using GraduationProject.Services.SuperMarketServices;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace GraduationProject.Repository.SuperMarketRepository
{
    public class SuperMarketRepository : ISuperMarketServices
    {

        Context context = new Context();
        private readonly IHttpContextAccessor httpContextAccessor;
        public SuperMarketRepository(IHttpContextAccessor httpContextAccessor)
        {
            this.httpContextAccessor = httpContextAccessor;
        }

      


        public int Create(AddSupermarketDto dto, List<IFormFile> imageFiles)
        {
            SuperMarket service = new SuperMarket();
            service.Name = dto.Name;
            service.PhoneNumber = dto.PhoneNumber;
            service.City = dto.City;
            service.Street = dto.Street;
            service.DescriptionOfPlace = dto.DescriptionOfPlace;
            service.StartWork = dto.StartWork;
            service.EndWork = dto.EndWork;
            service.Latitude = dto.Latitude;
            service.Longitude = dto.Longitude;
            context.SuperMarkets.Add(service);
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
            SuperMarket service = context.SuperMarkets.SingleOrDefault(d => d.Id == id);
            List<Images> images = context.images.Where(m => m.ServicId == service.Id).
                Where(s => s.serviceName == service.Name).ToList();
            foreach (var image in images)
            {
                context.images.Remove(image);
            }
            context.SuperMarkets.Remove(service);
            context.SaveChanges();
        }

        public void Update(int id, AddSupermarketDto dto )
        {
            SuperMarket service = context.SuperMarkets.FirstOrDefault(s => s.Id == id);
            service.Name = dto.Name;
            service.PhoneNumber = dto.PhoneNumber;
            service.City = dto.City;
            service.Street = dto.Street;
            service.DescriptionOfPlace = dto.DescriptionOfPlace;
            service.StartWork = dto.StartWork;
            service.EndWork = dto.EndWork;
            service.Latitude = dto.Latitude;
            service.Longitude = dto.Longitude;
            context.SaveChanges();
            
        }
        public List<SuperMarketDto> GetAllSuperMarket()
        {
            List<SuperMarket> superMarkets = context.SuperMarkets.ToList();
            if (!superMarkets.Any())
                return null;
            List<SuperMarketDto> dTOSuperMarkets = new List<SuperMarketDto>();
            foreach (var superMarket in superMarkets)
            {
                SuperMarketDto dTOSuperMarket = new SuperMarketDto();
                dTOSuperMarket.Name = superMarket.Name;
                dTOSuperMarket.PhoneNumber = superMarket.PhoneNumber;
                dTOSuperMarket.City = superMarket.City;
                dTOSuperMarket.Street = superMarket.Street;
                dTOSuperMarket.DescriptionOfPlace = superMarket.DescriptionOfPlace;
                dTOSuperMarket.StartWork = superMarket.StartWork;
                dTOSuperMarket.EndWork = superMarket.EndWork;
                dTOSuperMarket.Latitude = superMarket.Latitude;
                dTOSuperMarket.Longitude = superMarket.Longitude;
                dTOSuperMarket.averageRate = superMarket.averageRate;
                dTOSuperMarket.id = superMarket.Id;
                List<string> imagesDto = new List<string>();
                List<Images> imgs = context.images.Where(i => i.ServicId == superMarket.Id).
                    Where(i => i.serviceName == superMarket.Name).ToList();
                foreach (var img in imgs)
                {
                    HttpContext httpContext = httpContextAccessor.HttpContext;
                    imagesDto.Add($"{httpContext.Request.Scheme}://{httpContext.Request.Host}/imgs/{img.Image}");
                }
                dTOSuperMarket.Images = imagesDto;
                dTOSuperMarkets.Add(dTOSuperMarket);
            }
            return dTOSuperMarkets;
        }

        public List<SuperMarketDto> GetAllSuperMarketByReview()
        {
            List<SuperMarket> SuperMarkets = context.SuperMarkets.ToList();        
            foreach (var SuperMarket in SuperMarkets)
            {
                List<Review> reviews = context.Reviews.Where(r => r.ServicId == SuperMarket.Id).
                    Where(i => i.serviceName == SuperMarket.Name).ToList();
                if (reviews.Any())
                {
                    int averageRate = (int)reviews.Average(r => r.Rating);
                    SuperMarket.averageRate = averageRate;
                    context.SaveChanges();
                }
            }

            List<SuperMarket> superMarkets = context.SuperMarkets.OrderByDescending(d => d.averageRate).ToList(); 
            if (!superMarkets.Any())
                return null;
            List<SuperMarketDto> dTOSuperMarkets = new List<SuperMarketDto>();
            foreach (var superMarket in superMarkets)
            {
                SuperMarketDto dTOSuperMarket = new SuperMarketDto();
                dTOSuperMarket.Name = superMarket.Name;
                dTOSuperMarket.PhoneNumber = superMarket.PhoneNumber;
                dTOSuperMarket.City = superMarket.City;
                dTOSuperMarket.Street = superMarket.Street;
                dTOSuperMarket.DescriptionOfPlace = superMarket.DescriptionOfPlace;
                dTOSuperMarket.StartWork = superMarket.StartWork;
                dTOSuperMarket.EndWork = superMarket.EndWork;
                dTOSuperMarket.Latitude = superMarket.Latitude;
                dTOSuperMarket.Longitude = superMarket.Longitude;
                dTOSuperMarket.averageRate = superMarket.averageRate;
                dTOSuperMarket.id = superMarket.Id;
                List<string> imagesDto = new List<string>();
                List<Images> imgs = context.images.Where(i => i.ServicId == superMarket.Id).
                    Where(i => i.serviceName == superMarket.Name).ToList();
                foreach (var img in imgs)
                {
                    HttpContext httpContext = httpContextAccessor.HttpContext;
                    imagesDto.Add($"{httpContext.Request.Scheme}://{httpContext.Request.Host}/imgs/{img.Image}");
                }
                dTOSuperMarket.Images = imagesDto;
                dTOSuperMarkets.Add(dTOSuperMarket);
            }
            return dTOSuperMarkets;
        }

        public List<SuperMarketDto> GetAllSuperMarketDtoActiveNow()
        {
            DateTime now = DateTime.Now;
            List<SuperMarket> superMarkets = context.SuperMarkets.Where(d => d.StartWork <= now && d.EndWork >= now).ToList();
            if (!superMarkets.Any())
                return null;
            List<SuperMarketDto> dTOSuperMarkets = new List<SuperMarketDto>();
            foreach (var superMarket in superMarkets)
            {
                SuperMarketDto dTOSuperMarket = new SuperMarketDto();
                dTOSuperMarket.Name = superMarket.Name;
                dTOSuperMarket.PhoneNumber = superMarket.PhoneNumber;
                dTOSuperMarket.City = superMarket.City;
                dTOSuperMarket.Street = superMarket.Street;
                dTOSuperMarket.DescriptionOfPlace = superMarket.DescriptionOfPlace;
                dTOSuperMarket.StartWork = superMarket.StartWork;
                dTOSuperMarket.EndWork = superMarket.EndWork;
                dTOSuperMarket.Latitude = superMarket.Latitude;
                dTOSuperMarket.Longitude = superMarket.Longitude;
                dTOSuperMarket.averageRate = superMarket.averageRate;
                dTOSuperMarket.id = superMarket.Id;

                List<string> imagesDto = new List<string>();
                List<Images> imgs = context.images.Where(i => i.ServicId == superMarket.Id).
                    Where(i => i.serviceName == superMarket.Name).ToList();
                foreach (var img in imgs)
                {
                    HttpContext httpContext = httpContextAccessor.HttpContext;
                    imagesDto.Add($"{httpContext.Request.Scheme}://{httpContext.Request.Host}/imgs/{img.Image}");
                }
                dTOSuperMarket.Images = imagesDto;
                dTOSuperMarkets.Add(dTOSuperMarket);
            }
            return dTOSuperMarkets;
        }

        public SuperMarketDto GetSuperMarketByID(int id)
        {
           SuperMarket superMarket = context.SuperMarkets.FirstOrDefault(s=>s.Id==id);
            if (superMarket==null)
                return null;
                SuperMarketDto dTOSuperMarket = new SuperMarketDto();
                dTOSuperMarket.Name = superMarket.Name;
                dTOSuperMarket.PhoneNumber = superMarket.PhoneNumber;
                dTOSuperMarket.City = superMarket.City;
                dTOSuperMarket.Street = superMarket.Street;
                dTOSuperMarket.DescriptionOfPlace = superMarket.DescriptionOfPlace;
                dTOSuperMarket.StartWork = superMarket.StartWork;
                dTOSuperMarket.EndWork = superMarket.EndWork;
                dTOSuperMarket.Latitude = superMarket.Latitude;
                dTOSuperMarket.Longitude = superMarket.Longitude;
                dTOSuperMarket.id = superMarket.Id;

                List<string> imagesDto = new List<string>();
                List<Images> imgs = context.images.Where(i => i.ServicId == superMarket.Id).
                    Where(i => i.serviceName == superMarket.Name).ToList();
                foreach (var img in imgs)
                {
                   HttpContext httpContext = httpContextAccessor.HttpContext;
                   imagesDto.Add($"{httpContext.Request.Scheme}://{httpContext.Request.Host}/imgs/{img.Image}");
                }
                dTOSuperMarket.Images = imagesDto;
            return dTOSuperMarket;
        }

        public List<SuperMarketDto> Search(string name)
        {
            var superMarkets = context.SuperMarkets
                   .Where(p => p.Name.Contains(name) ||
                    p.PhoneNumber.Contains(name) || p.Street.Contains(name)).ToList();
            if (!superMarkets.Any())
                return null;
            List<SuperMarketDto> dTOSuperMarkets = new List<SuperMarketDto>();
            foreach (var superMarket in superMarkets)
            {
                SuperMarketDto dTOSuperMarket = new SuperMarketDto();
                dTOSuperMarket.Name = superMarket.Name;
                dTOSuperMarket.PhoneNumber = superMarket.PhoneNumber;
                dTOSuperMarket.City = superMarket.City;
                dTOSuperMarket.Street = superMarket.Street;
                dTOSuperMarket.DescriptionOfPlace = superMarket.DescriptionOfPlace;
                dTOSuperMarket.StartWork = superMarket.StartWork;
                dTOSuperMarket.EndWork = superMarket.EndWork;
                dTOSuperMarket.Latitude = superMarket.Latitude;
                dTOSuperMarket.Longitude = superMarket.Longitude;
                dTOSuperMarket.id = superMarket.Id;

                List<string> imagesDto = new List<string>();
                List<Images> imgs = context.images.Where(i => i.ServicId == superMarket.Id).
                    Where(i => i.serviceName == superMarket.Name).ToList();
                foreach (var img in imgs)
                {
                    HttpContext httpContext = httpContextAccessor.HttpContext;
                    imagesDto.Add($"{httpContext.Request.Scheme}://{httpContext.Request.Host}/imgs/{img.Image}");
                }
                dTOSuperMarket.Images = imagesDto;
                dTOSuperMarkets.Add(dTOSuperMarket);
            }
            return dTOSuperMarkets;
        }
    

        //.............................................................
        public int CreateReview(string U, int SuperMarketId, DTOReview dTOReview)
        {
            Review review = new Review();
            review.ReviewDate = DateTime.Now;
            review.Rating = dTOReview.Rating;
            review.Comment = dTOReview.Comment;
            review.ServicId = SuperMarketId;
            review.serviceName = dTOReview.serviceName;
            review.UserId = U;
            context.Reviews.Add(review);
            context.SaveChanges();
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
        public List<DTOOReview> GetAllReviews(int ServiceId, string name)
        {
            List<Review> reviews = context.Reviews.Where(r => r.ServicId == ServiceId).
                Where(r => r.serviceName == name).ToList();
            List<DTOOReview> dTOReviews = new List<DTOOReview>();
            foreach (var review in reviews)
            {
                DTOOReview dTOReview = new DTOOReview();
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
