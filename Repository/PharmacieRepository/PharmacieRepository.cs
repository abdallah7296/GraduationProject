using GraduationProject.DTO.DTOForDoctors;
using GraduationProject.DTO.DTOPharmacies;
using GraduationProject.DTO.DTOReview;
using GraduationProject.DTO.Images;
using GraduationProject.Models;
using GraduationProject.Services.PharmaciesServices;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace GraduationProject.Repository.PharmacieRepository
{
    public class PharmacieRepository : IPharmaciesServices
    {
        Context context = new Context();
        private readonly IHttpContextAccessor httpContextAccessor;

        public PharmacieRepository(IHttpContextAccessor httpContextAccessor)
        {
            this.httpContextAccessor = httpContextAccessor;
        }
        public List<DTOOPharmacie> GetAllPharmaciesActiveNow()
        {
            DateTime now = DateTime.Now;
            List<Pharmacies> pharmacies = context.pharmacies.
                Where(d => d.StartWork <= now && d.EndWork >= now).
                ToList() ;
            if (!pharmacies.Any())
                return null;
            List<DTOOPharmacie> dTOPharmacies = new List<DTOOPharmacie>();
            foreach(var pharmacie in pharmacies)
            {
                DTOOPharmacie dTOPharmacie = new DTOOPharmacie();
                dTOPharmacie.Name = pharmacie.Name;
                dTOPharmacie.City = pharmacie.City;
                dTOPharmacie.Street = pharmacie.Street;
                dTOPharmacie.StartWork = pharmacie.StartWork;
                dTOPharmacie.EndWork = pharmacie.EndWork;
                dTOPharmacie.PhoneNumber = pharmacie.PhoneNumber;
                dTOPharmacie.DescriptionOfPlace = pharmacie.DescriptionOfPlace;
                dTOPharmacie.LinkOfPlace = pharmacie.LinkOfPlace;
                dTOPharmacie.Latitude = pharmacie.Latitude;
                dTOPharmacie.Longitude = pharmacie.Longitude;
                dTOPharmacie.id = pharmacie.Id;
                List<string> imagesDto = new List<string>();
                List<Images> imgs = context.images.Where(i => i.ServicId == pharmacie.Id).
                    Where(i => i.serviceName == pharmacie.Name).ToList();
                foreach (var img in imgs)
                {
                  
                    HttpContext httpContext = httpContextAccessor.HttpContext;
                    imagesDto.Add($"{httpContext.Request.Scheme}://{httpContext.Request.Host}/imgs/{img.Image}");
                }
         
                dTOPharmacie.Images = imagesDto;
                dTOPharmacies.Add(dTOPharmacie);
            }
            return dTOPharmacies;
        }

        public List<DTOOPharmacie> GetAllPlayPharmacies()
        {
            List<Pharmacies> pharmacies = context.pharmacies.ToList();
            if (!pharmacies.Any())
                return null;
            List<DTOOPharmacie> dTOPharmacies = new List<DTOOPharmacie>();
            foreach (var pharmacie in pharmacies)
            {
                DTOOPharmacie dTOPharmacie = new DTOOPharmacie();
                dTOPharmacie.Name = pharmacie.Name;
                dTOPharmacie.City = pharmacie.City;
                dTOPharmacie.Street = pharmacie.Street;
                dTOPharmacie.StartWork = pharmacie.StartWork;
                dTOPharmacie.EndWork = pharmacie.EndWork;
                dTOPharmacie.PhoneNumber = pharmacie.PhoneNumber;
                dTOPharmacie.DescriptionOfPlace = pharmacie.DescriptionOfPlace;
                dTOPharmacie.LinkOfPlace = pharmacie.LinkOfPlace;
                dTOPharmacie.Longitude = pharmacie.Longitude;
                dTOPharmacie.Latitude = pharmacie.Latitude;
                dTOPharmacie.id = pharmacie.Id;

                List<string> imagesDto = new List<string>();
                List<Images> imgs = context.images.Where(i => i.ServicId == pharmacie.Id).
                    Where(i => i.serviceName == pharmacie.Name).ToList();
                foreach (var img in imgs)
                {
                   
                    HttpContext httpContext = httpContextAccessor.HttpContext;
                    imagesDto.Add($"{httpContext.Request.Scheme}://{httpContext.Request.Host}/imgs/{img.Image}");
                }

                dTOPharmacie.Images = imagesDto;
                dTOPharmacies.Add(dTOPharmacie);
            }
            return dTOPharmacies;
        }

        public List<Pharmacies> GetAllPlayPharmaciesBySortReview()
        {


            List<Pharmacies> Pharmacies = context.pharmacies.ToList();
             foreach (var pharmacie in Pharmacies)
            {
                List<Review> reviews = context.Reviews.Where(r => r.ServicId == pharmacie.Id).
                    Where(i => i.serviceName == pharmacie.Name).ToList();


                if (reviews.Any())
                {
                    int averageRate = (int)reviews.Average(r => r.Rating);
                    pharmacie.averageRate = averageRate;
                    context.SaveChanges();
                }
            }
            List<Pharmacies> pharmacies = context.pharmacies.
                         OrderByDescending(d => d.averageRate).ToList();
            if (!pharmacies.Any())
                return null;
            return pharmacies;
            //List<DTOOPharmacie> dTOPharmacies = new List<DTOOPharmacie>();
            //foreach (var pharmacie in pharmacies)
            //{
            //    DTOOPharmacie dTOPharmacie = new DTOOPharmacie();
            //    dTOPharmacie.Name = pharmacie.Name;
            //    dTOPharmacie.City = pharmacie.City;
            //    dTOPharmacie.Street = pharmacie.Street;
            //    dTOPharmacie.StartWork = pharmacie.StartWork;
            //    dTOPharmacie.EndWork = pharmacie.EndWork;
            //    dTOPharmacie.PhoneNumber = pharmacie.PhoneNumber;
            //    dTOPharmacie.DescriptionOfPlace = pharmacie.DescriptionOfPlace;
            //    dTOPharmacie.LinkOfPlace = pharmacie.LinkOfPlace;
            //    dTOPharmacie.Longitude = pharmacie.Longitude;
            //    dTOPharmacie.Latitude = pharmacie.Latitude;
            //    List<string> imagesDto = new List<string>();
            //    List<Images> imgs = context.images.Where(i => i.ServicId == pharmacie.Id).
            //        Where(i => i.serviceName == pharmacie.Name).ToList();
            //    foreach (var img in imgs)
            //    {
            //        imagesDto.Add(img.Image);
            //    }

            //    dTOPharmacie.Images = imagesDto;
            //    dTOPharmacies.Add(dTOPharmacie);
            //}
            //return dTOPharmacies;
        }
        public DTOOPharmacie GetPharmacieByID(int id)
        {
            Pharmacies pharmacie = context.pharmacies.FirstOrDefault(p => p.Id == id);
            if (pharmacie == null )
                return null;
            DTOOPharmacie dTOPharmacie = new DTOOPharmacie();
            dTOPharmacie.Name = pharmacie.Name;
            dTOPharmacie.City = pharmacie.City;
            dTOPharmacie.Street = pharmacie.Street;
            dTOPharmacie.StartWork = pharmacie.StartWork;
            dTOPharmacie.EndWork = pharmacie.EndWork;
            dTOPharmacie.PhoneNumber = pharmacie.PhoneNumber;
            dTOPharmacie.DescriptionOfPlace = pharmacie.DescriptionOfPlace;
            dTOPharmacie.LinkOfPlace = pharmacie.LinkOfPlace;
            dTOPharmacie.Longitude = pharmacie.Longitude;
            dTOPharmacie.Latitude = pharmacie.Latitude;
            dTOPharmacie.id = pharmacie.Id;

            List<string> imagesDto = new List<string>();
            List<Images> imgs = context.images.Where(i => i.ServicId == pharmacie.Id).
                Where(i => i.serviceName == pharmacie.Name).ToList();
            foreach (var img in imgs)
            {
                //  ImagesDto imageDto = new ImagesDto();
                // imageDto.Image = img.Image;
                HttpContext httpContext = httpContextAccessor.HttpContext;
                imagesDto.Add($"{httpContext.Request.Scheme}://{httpContext.Request.Host}/imgs/{img.Image}");
            }

            dTOPharmacie.Images = imagesDto;
            return dTOPharmacie;
         
        }

        public List<DTOOPharmacie> Search(string name)
        {
            List<Pharmacies> pharmacies = context.pharmacies
             .Where(p => p.Name.Contains(name) ||
       p.PhoneNumber.Contains(name) || p.Street.Contains(name)).ToList();

            if (!pharmacies.Any())
                return null;
            List<DTOOPharmacie> dTOPharmacies = new List<DTOOPharmacie>();
            foreach (var pharmacie in pharmacies)
            {
                DTOOPharmacie dTOPharmacie = new DTOOPharmacie();
                dTOPharmacie.Name = pharmacie.Name;
                dTOPharmacie.City = pharmacie.City;
                dTOPharmacie.Street = pharmacie.Street;
                dTOPharmacie.StartWork = pharmacie.StartWork;
                dTOPharmacie.EndWork = pharmacie.EndWork;
                dTOPharmacie.PhoneNumber = pharmacie.PhoneNumber;
                dTOPharmacie.DescriptionOfPlace = pharmacie.DescriptionOfPlace;
                dTOPharmacie.LinkOfPlace = pharmacie.LinkOfPlace;
                dTOPharmacie.Longitude = pharmacie.Longitude;
                dTOPharmacie.Latitude = pharmacie.Latitude;
                dTOPharmacie.id = pharmacie.Id;

                List<string> imagesDto = new List<string>();
                List<Images> imgs = context.images.Where(i => i.ServicId == pharmacie.Id).
                    Where(i => i.serviceName == pharmacie.Name).ToList();
                foreach (var img in imgs)
                {
                    //  ImagesDto imageDto = new ImagesDto();
                    // imageDto.Image = img.Image;
                    HttpContext httpContext = httpContextAccessor.HttpContext;
                    imagesDto.Add($"{httpContext.Request.Scheme}://{httpContext.Request.Host}/imgs/{img.Image}");
                }

                dTOPharmacie.Images = imagesDto;
                dTOPharmacies.Add(dTOPharmacie);
            }
            return dTOPharmacies;
        }
        //Add,,Update,Delete FOR Review.......................................................................

        public int CreateReview(string U, int pharmacieId, DTOReview dTOReview)
        {
            Pharmacies pharmacie = context.pharmacies.FirstOrDefault(p => p.Id == pharmacieId);

            Review review = new Review();
            review.ReviewDate = DateTime.Now;
            review.Rating = dTOReview.Rating;
            review.Comment = dTOReview.Comment;
            review.ServicId = pharmacieId;
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
        public List<DTOOReview> GetAllReviews(int ServiceId,string name)
        {
            List<Review> reviews = context.Reviews.Where(r => r.ServicId == ServiceId).
                Where(r => r.serviceName == name).ToList();
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

       
        //Add,,Update,Delete.......................................................................
        public int Create(AddPharmacieDto dto, List<IFormFile> imageFiles)
        {
            Pharmacies pharmacie = new Pharmacies();
            pharmacie.Name = dto.Name;
            pharmacie.City = dto.City;
            pharmacie.Street = dto.Street;
            pharmacie.StartWork = dto.StartWork;
            pharmacie.EndWork = dto.EndWork;
            pharmacie.PhoneNumber = dto.PhoneNumber;
            pharmacie.DescriptionOfPlace = dto.DescriptionOfPlace;
            pharmacie.LinkOfPlace = dto.LinkOfPlace;
            pharmacie.Longitude = dto.Longitude;
            pharmacie.Latitude = dto.Latitude;
            context.pharmacies.Add(pharmacie);
            context.SaveChanges();

            List<Images> images = new List<Images>();
            foreach (var file in imageFiles)
            {
                string fileName = file.FileName;
                string filePath = Path.GetFullPath(Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\imgs"));
                using (var fileStream = new FileStream(Path.Combine(filePath, fileName), FileMode.Create))
                {
                    file.CopyTo(fileStream);
                }
                Images image = new Images();
                image.Image = fileName;
                image.ServicId = pharmacie.Id;
                image.serviceName = pharmacie.Name;
                context.images.Add(image);
                context.SaveChanges();
                images.Add(image);
            }
             return pharmacie.Id;

        }

        public void Delete(int id)
        {
            Pharmacies pharmacie = context.pharmacies
                .FirstOrDefault(p => p.Id == id);
            List<Images> images = context.images.Where(m => m.ServicId == pharmacie.Id).
                Where(s => s.serviceName == pharmacie.Name).ToList();
            foreach (var image in images)
            {
                context.images.Remove(image);
            }
            context.pharmacies.Remove(pharmacie);
            context.SaveChanges();
        }

         public void Update(int id, AddPharmacieDto dto)
        {
            Pharmacies pharmacie = context.pharmacies
            .FirstOrDefault(p => p.Id == id);
            pharmacie.Name = dto.Name;
            pharmacie.City = dto.City;
            pharmacie.Street = dto.Street;
            pharmacie.StartWork = dto.StartWork;
            pharmacie.EndWork = dto.EndWork;
            pharmacie.PhoneNumber = dto.PhoneNumber;
            pharmacie.DescriptionOfPlace = dto.DescriptionOfPlace;
            pharmacie.LinkOfPlace = dto.LinkOfPlace;
            pharmacie.Longitude = dto.Longitude;
            pharmacie.Latitude = dto.Latitude;
            context.SaveChanges();
            
         }

     }
}
