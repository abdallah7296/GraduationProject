using GraduationProject.DTO.AnalysisCentersDto;
using GraduationProject.DTO.DTOForWorkspace;
using GraduationProject.DTO.DTOReview;
using GraduationProject.Models;
using GraduationProject.Services.AnalysisCentersServices;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using ZendeskApi_v2.Models.Views.Executed;

namespace GraduationProject.Repository.AnalysisCentersRepository
{
    public class AnalysisCentersRepository : IAnalysisCentersServices
    {
        Context context = new Context();
        private readonly IHttpContextAccessor httpContextAccessor;

        public AnalysisCentersRepository(IHttpContextAccessor httpContextAccessor)
        {
            this.httpContextAccessor = httpContextAccessor;
        }

        public int Create(AddAnalysisCentersDto dto, List<IFormFile> imageFiles)
        {
            AnalysisCenters service = new AnalysisCenters();
            service.Name = dto.Name;
            service.PhoneNumber = dto.PhoneNumber;
            service.City = dto.City;
            service.Street = dto.Street;
            service.DescriptionOfPlace = dto.DescriptionOfPlace;
            service.StartWork = dto.StartWork;
            service.EndWork = dto.EndWork;
            service.Latitude = dto.Latitude;
            service.Longitude = dto.Longitude;
            context.analysisCenters.Add(service);
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
            AnalysisCenters service = context.analysisCenters.SingleOrDefault(d => d.Id == id);
            List<Images> images = context.images.Where(m => m.ServicId == service.Id).
                Where(s => s.serviceName == service.Name).ToList();
            foreach (var image in images)
            {
                context.images.Remove(image);
            }
            context.analysisCenters.Remove(service);
            context.SaveChanges();
        }

        public void Update(int id, AddAnalysisCentersDto dto )
        {

            AnalysisCenters service = context.analysisCenters.FirstOrDefault(s => s.Id == id);
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
        //.....................................................................
        public List<AnalysisCentersDto> GetAllAnalysisCenters()
        {
            List<AnalysisCenters> analysisCenters = context.analysisCenters.ToList();
            if (!analysisCenters.Any())
                return null;
            List<AnalysisCentersDto> analysisCentersDto = new List<AnalysisCentersDto>();
            foreach (var analysisCenter in analysisCenters)
            {
                AnalysisCentersDto analysisCenterDto = new AnalysisCentersDto();
                analysisCenterDto.Name = analysisCenter.Name;
                analysisCenterDto.PhoneNumber = analysisCenter.PhoneNumber;
                analysisCenterDto.City = analysisCenter.City;
                analysisCenterDto.Street = analysisCenter.Street;
                analysisCenterDto.DescriptionOfPlace = analysisCenter.DescriptionOfPlace;
                analysisCenterDto.LinkOfPlace = analysisCenter.LinkOfPlace;
                analysisCenterDto.StartWork = analysisCenter.StartWork;
                analysisCenterDto.EndWork = analysisCenter.EndWork;
                analysisCenterDto.Latitude = analysisCenter.Latitude;
                analysisCenterDto.Longitude = analysisCenter.Longitude;
                analysisCenterDto.averageRate = analysisCenter.averageRate;
                analysisCenterDto.id = analysisCenter.Id;
                List<string> imagesDto = new List<string>();
                List<Images> imgs = context.images.Where(i => i.ServicId == analysisCenter.Id).
                    Where(i => i.serviceName == analysisCenter.Name).ToList();
                foreach (var img in imgs)
                {
                    HttpContext httpContext = httpContextAccessor.HttpContext;
                    imagesDto.Add($"{httpContext.Request.Scheme}://{httpContext.Request.Host}/imgs/{img.Image}");
                }
                analysisCenterDto.Images = imagesDto;
                analysisCentersDto.Add(analysisCenterDto);
            }
            return analysisCentersDto;
        }

        public List<AnalysisCentersDto> GetAllAnalysisCentersActiveNow()
        {
            DateTime now = DateTime.Now;
            List<AnalysisCenters> analysisCenters = context.analysisCenters.Where(d => d.StartWork <= now && d.EndWork >= now).ToList();
            if (!analysisCenters.Any())
                return null;
            List<AnalysisCentersDto> analysisCentersDto = new List<AnalysisCentersDto>();
            foreach (var analysisCenter in analysisCenters)
            {
                AnalysisCentersDto analysisCenterDto = new AnalysisCentersDto();
                analysisCenterDto.Name = analysisCenter.Name;
                analysisCenterDto.PhoneNumber = analysisCenter.PhoneNumber;
                analysisCenterDto.City = analysisCenter.City;
                analysisCenterDto.Street = analysisCenter.Street;
                analysisCenterDto.DescriptionOfPlace = analysisCenter.DescriptionOfPlace;
                analysisCenterDto.LinkOfPlace = analysisCenter.LinkOfPlace;
                analysisCenterDto.StartWork = analysisCenter.StartWork;
                analysisCenterDto.EndWork = analysisCenter.EndWork;
                analysisCenterDto.Latitude = analysisCenter.Latitude;
                analysisCenterDto.Longitude = analysisCenter.Longitude;
                analysisCenterDto.averageRate = analysisCenter.averageRate;
                analysisCenterDto.id = analysisCenter.Id;

                List<string> imagesDto = new List<string>();
                List<Images> imgs = context.images.Where(i => i.ServicId == analysisCenter.Id).
                    Where(i => i.serviceName == analysisCenter.Name).ToList();
                foreach (var img in imgs)
                {
                    HttpContext httpContext = httpContextAccessor.HttpContext;
                    imagesDto.Add($"{httpContext.Request.Scheme}://{httpContext.Request.Host}/imgs/{img.Image}");
                }
                analysisCenterDto.Images = imagesDto;
                analysisCentersDto.Add(analysisCenterDto);
            }
            return analysisCentersDto;
             
        }

        public List<AnalysisCentersDto> GetAllAnalysisCentersByReview()
        {

            List<AnalysisCenters> AnalysisCenters = context.analysisCenters.ToList();
            foreach (var AnalysisCenter in AnalysisCenters)
            {
                List<Review> reviews = context.Reviews.Where(r => r.ServicId == AnalysisCenter.Id).
                    Where(i => i.serviceName == AnalysisCenter.Name).ToList();


                if (reviews.Any())
                {
                    int averageRate = (int)reviews.Average(r => r.Rating);
                    AnalysisCenter.averageRate = averageRate;
                    context.SaveChanges();
                }
            }

            List<AnalysisCenters> analysisCenters = context.analysisCenters.OrderByDescending(d => d.averageRate).ToList();
            if (!analysisCenters.Any())
                return null;
            List<AnalysisCentersDto> analysisCentersDto = new List<AnalysisCentersDto>();
            foreach (var analysisCenter in analysisCenters)
            {
                AnalysisCentersDto analysisCenterDto = new AnalysisCentersDto();
                analysisCenterDto.Name = analysisCenter.Name;
                analysisCenterDto.PhoneNumber = analysisCenter.PhoneNumber;
                analysisCenterDto.City = analysisCenter.City;
                analysisCenterDto.Street = analysisCenter.Street;
                analysisCenterDto.DescriptionOfPlace = analysisCenter.DescriptionOfPlace;
                analysisCenterDto.LinkOfPlace = analysisCenter.LinkOfPlace;
                analysisCenterDto.StartWork = analysisCenter.StartWork;
                analysisCenterDto.EndWork = analysisCenter.EndWork;
                analysisCenterDto.Latitude = analysisCenter.Latitude;
                analysisCenterDto.Longitude = analysisCenter.Longitude;
                analysisCenterDto.averageRate = analysisCenter.averageRate;
                analysisCenterDto.id = analysisCenter.Id;

                List<string> imagesDto = new List<string>();
                List<Images> imgs = context.images.Where(i => i.ServicId == analysisCenter.Id).
                    Where(i => i.serviceName == analysisCenter.Name).ToList();
                foreach (var img in imgs)
                {
                    HttpContext httpContext = httpContextAccessor.HttpContext;
                    imagesDto.Add($"{httpContext.Request.Scheme}://{httpContext.Request.Host}/imgs/{img.Image}");
                }
                analysisCenterDto.Images = imagesDto;
                analysisCentersDto.Add(analysisCenterDto);
            }
            return analysisCentersDto;
        }
        public AnalysisCentersDto GetAnalysisCenterByID(int id)
        {
            AnalysisCenters analysisCenter = context.analysisCenters.FirstOrDefault(a=>a.Id==id);
            if (analysisCenter==null)
                return null;
                AnalysisCentersDto analysisCenterDto = new AnalysisCentersDto();
                analysisCenterDto.Name = analysisCenter.Name;
                analysisCenterDto.PhoneNumber = analysisCenter.PhoneNumber;
                analysisCenterDto.City = analysisCenter.City;
                analysisCenterDto.Street = analysisCenter.Street;
                analysisCenterDto.DescriptionOfPlace = analysisCenter.DescriptionOfPlace;
                analysisCenterDto.LinkOfPlace = analysisCenter.LinkOfPlace;
                analysisCenterDto.StartWork = analysisCenter.StartWork;
                analysisCenterDto.EndWork = analysisCenter.EndWork;
                analysisCenterDto.Latitude = analysisCenter.Latitude;
                analysisCenterDto.Longitude = analysisCenter.Longitude;
                analysisCenterDto.averageRate = analysisCenter.averageRate;
                analysisCenterDto.id = analysisCenter.Id;

            List<string> imagesDto = new List<string>();
                List<Images> imgs = context.images.Where(i => i.ServicId == analysisCenter.Id).
                    Where(i => i.serviceName == analysisCenter.Name).ToList();
                foreach (var img in imgs)
                {
                   HttpContext httpContext = httpContextAccessor.HttpContext;
                  imagesDto.Add($"{httpContext.Request.Scheme}://{httpContext.Request.Host}/imgs/{img.Image}");
                } 
                analysisCenterDto.Images = imagesDto;
            return analysisCenterDto;
        }

        public List<AnalysisCentersDto> Search(string name)
        {
            List<AnalysisCenters> analysisCenters = context.analysisCenters.
             Where(p => p.Name.Contains(name) ||
                p.PhoneNumber.Contains(name) ||
              p.Street.Contains(name)).ToList();


            if (!analysisCenters.Any())
                return null;
            List<AnalysisCentersDto> analysisCentersDto = new List<AnalysisCentersDto>();
            foreach (var analysisCenter in analysisCenters)
            {
                AnalysisCentersDto analysisCenterDto = new AnalysisCentersDto();
                analysisCenterDto.Name = analysisCenter.Name;
                analysisCenterDto.PhoneNumber = analysisCenter.PhoneNumber;
                analysisCenterDto.City = analysisCenter.City;
                analysisCenterDto.Street = analysisCenter.Street;
                analysisCenterDto.DescriptionOfPlace = analysisCenter.DescriptionOfPlace;
                analysisCenterDto.LinkOfPlace = analysisCenter.LinkOfPlace;
                analysisCenterDto.StartWork = analysisCenter.StartWork;
                analysisCenterDto.EndWork = analysisCenter.EndWork;
                analysisCenterDto.Latitude = analysisCenter.Latitude;
                analysisCenterDto.Longitude = analysisCenter.Longitude;
                analysisCenterDto.id = analysisCenter.Id;

                List<string> imagesDto = new List<string>();
                List<Images> imgs = context.images.Where(i => i.ServicId == analysisCenter.Id).
                    Where(i => i.serviceName == analysisCenter.Name).ToList();
                foreach (var img in imgs)
                {
                    HttpContext httpContext = httpContextAccessor.HttpContext;
                    imagesDto.Add($"{httpContext.Request.Scheme}://{httpContext.Request.Host}/imgs/{img.Image}");
                }
                analysisCenterDto.Images = imagesDto;
                analysisCentersDto.Add(analysisCenterDto);
            }
            return analysisCentersDto;
        }


        //......................................................................
        public int CreateReview(string U, int AnalysisCentersID, DTOReview dTOReview)
        {  
            Review review = new Review();
            review.ReviewDate = DateTime.Now;
            review.Rating = dTOReview.Rating;
            review.Comment = dTOReview.Comment;
            review.ServicId = AnalysisCentersID;
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
