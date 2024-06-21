using EO.Internal;
using GraduationProject.DTO.DTOForDoctors;
using GraduationProject.DTO.DTOForWorkspace;
using GraduationProject.DTO.DTOPharmacies;
using GraduationProject.DTO.DTOReview;
using GraduationProject.DTO.Images;
using GraduationProject.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace GraduationProject.Services.DoctorsServices
{
    public class DoctorRepository : IDoctorService
    {
        Context context = new Context();
        private readonly IHttpContextAccessor httpContextAccessor;

        //Add,,Update,Delete FOR Doctor.......................................................................
        public DoctorRepository(IHttpContextAccessor httpContextAccessor)
        {
            this.httpContextAccessor = httpContextAccessor;
        }
        public int Add(AddDoctorDto doctorDto, List<IFormFile> imageFiles)
        {
            Doctor doctor = new Doctor();
            doctor.Name = doctorDto.Name;
            doctor.PhoneNumber = doctorDto.PhoneNumber;
            doctor.Street = doctorDto.Street;
            doctor.City = doctorDto.City;
            doctor.StartWork = doctorDto.StartWork;
            doctor.EndWork = doctorDto.EndWork;
            doctor.Latitude = doctorDto.Latitude;
            doctor.Longitude = doctorDto.Longitude;
            doctor.Specialization
             = doctorDto.specialization;
            doctor.DescriptionOfPlace = doctorDto.DescriptionOfPlace;
            doctor.LinkOfPlace = doctorDto.LinkOfPlace;
            context.Doctors.Add(doctor);
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
                image.serviceName = doctor.Name;
                image.ServicId = doctor.Id;
                context.images.Add(image);
                context.SaveChanges();
                images.Add(image);
            }
            return doctor.Id;
        }
       
        public void Delete(int id )
        {
            var doctor = context.Doctors.FirstOrDefault(d => d.Id == id);
            if (doctor is null) throw new NotFoundException("doctor not found");
            List<Images> images = context.images.Where(m => m.ServicId == doctor.Id).
              Where(s => s.serviceName == doctor.Name).ToList();
            foreach (var image in images)
            {
                context.images.Remove(image);
            }
            context.Doctors.Remove(doctor);
            context.SaveChanges();

        }
        public void Update(int id,  AddDoctorDto doctor )
        {
            var OldDoctor = context.Doctors.FirstOrDefault(d => d.Id == id);
            if (doctor is null) throw new NotFoundException("doctor not found");
             OldDoctor.Name = doctor.Name;
            OldDoctor.Street = doctor.Street;
            OldDoctor.PhoneNumber = doctor.PhoneNumber;
            OldDoctor.Specialization = doctor.specialization;
            OldDoctor.City = doctor.City;
            OldDoctor.Latitude = doctor.Latitude;
            OldDoctor.Longitude = doctor.Longitude;
            OldDoctor.StartWork = doctor.StartWork;
            OldDoctor.EndWork = doctor.EndWork;
            OldDoctor.DescriptionOfPlace = doctor.DescriptionOfPlace;
            OldDoctor.LinkOfPlace = doctor.LinkOfPlace;
           
            context.SaveChanges();
        }
        
        //.....................................................................................

        public List<DTODoctor> GetAllDectors()
        {
            HttpContext httpContext = httpContextAccessor.HttpContext;
            var doctors = context.Doctors.ToList();
             List<DTODoctor> dTODoctors = new List<DTODoctor>();
             if (!doctors.Any()  )
                return null;
            foreach (var doctor in doctors)
             {
                DTODoctor dTODoctor  = new DTODoctor();
                dTODoctor.Name = doctor.Name;
                dTODoctor.Street = doctor.Street;
                dTODoctor.City = doctor.City;
                dTODoctor.StartWork = doctor.StartWork;
                dTODoctor.EndWork = doctor.EndWork;
                dTODoctor.Latitude = doctor.Latitude;
                dTODoctor.Longitude = doctor.Longitude;
                dTODoctor.PhoneNumber = doctor.PhoneNumber;
                dTODoctor.specialization = doctor.Specialization;
                dTODoctor.DescriptionOfPlace = doctor.DescriptionOfPlace;
                dTODoctor.LinkOfPlace = doctor.LinkOfPlace;
                dTODoctor.id = doctor.Id;
                List<string> imagesDto = new List<string>();
                List<Images> imgs = context.images.Where(i => i.ServicId == doctor.Id).
                    Where(i => i.serviceName == doctor.Name).ToList();
                foreach (var img in imgs)
                {
                    
                    imagesDto.Add($"{httpContext.Request.Scheme}://{httpContext.Request.Host}/imgs/{img.Image}");
                }
                dTODoctor.Images = imagesDto;
                dTODoctors.Add(dTODoctor);
            }
            return dTODoctors;
        }
        public List<Doctor> GetAllDectorsBySortReview()
        {

            var Doctors = context.Doctors.ToList();
            int averageRate;
            foreach (var doctor in Doctors)
            {
                List<Review> reviews = context.Reviews.Where(r => r.ServicId == doctor.Id).
                    Where(i => i.serviceName == doctor.Name).ToList();
                if (reviews.Any())
                {
                    averageRate = (int)reviews.Average(r => r.Rating);
                    doctor.averageRate = averageRate;
                    context.SaveChanges();
                }
            }
    
            var doctors = context.Doctors.OrderByDescending(d => d.averageRate).ToList();
           // List<DTODoctor> dTODoctors = new List<DTODoctor>();
            if (!doctors.Any())
                return null;
            return doctors;
            //foreach (var doctor in doctors)
            //{
            //    DTODoctor dTODoctor = new DTODoctor();
            //    dTODoctor.Name = doctor.Name;
            //    dTODoctor.Street = doctor.Street;
            //    dTODoctor.City = doctor.City;
            //    dTODoctor.StartWork = doctor.StartWork;
            //    dTODoctor.EndWork = doctor.EndWork;
            //    dTODoctor.Latitude = doctor.Latitude;
            //    dTODoctor.Longitude = doctor.Longitude;
            //    dTODoctor.PhoneNumber = doctor.PhoneNumber;
            //    dTODoctor.specialization = doctor.Specialization;
            //    dTODoctor.DescriptionOfPlace = doctor.DescriptionOfPlace;
            //    dTODoctor.LinkOfPlace = doctor.LinkOfPlace;
            //    dTODoctor.id = doctor.Id;
            //    dTODoctor.AvrageRate = doctor.averageRate;
            //    List<string> imagesDto = new List<string>();
            //    List<Images> imgs = context.images.Where(i => i.ServicId == doctor.Id).
            //        Where(i => i.serviceName == doctor.Name).ToList();
            //    foreach (var img in imgs)
            //    {
            //        //  ImagesDto imageDto = new ImagesDto();
            //        // imageDto.Image = img.Image;
            //        imagesDto.Add(img.Image);
            //    }
            //    dTODoctor.Images = imagesDto;
            //    dTODoctors.Add(dTODoctor);
            //}
            //return dTODoctors;
        }
        public DTODoctor GetDoctorById(int id)
        {
            HttpContext httpContext = httpContextAccessor.HttpContext; 
            var doctor = context.Doctors.SingleOrDefault(d => d.Id == id);
            if (doctor == null)
                return null;
            DTODoctor dTODoctor = new DTODoctor();
            dTODoctor.Name = doctor.Name;
            dTODoctor.Street = doctor.Street;
            dTODoctor.City = doctor.City;
            dTODoctor.StartWork = doctor.StartWork;
            dTODoctor.EndWork = doctor.EndWork;
            dTODoctor.Latitude = doctor.Latitude;
            dTODoctor.Longitude = doctor.Longitude;
            dTODoctor.PhoneNumber = doctor.PhoneNumber;
            dTODoctor.specialization = doctor.Specialization;
            dTODoctor.DescriptionOfPlace = doctor.DescriptionOfPlace;
            dTODoctor.LinkOfPlace = doctor.LinkOfPlace;
            dTODoctor.id = doctor.Id;

            List<string> imagesDto = new List<string>();
            List<Images> imgs = context.images.Where(i => i.ServicId == doctor.Id).
                Where(i => i.serviceName == doctor.Name).ToList();
            foreach (var img in imgs)

            {
                //  ImagesDto imageDto = new ImagesDto();
                // imageDto.Image = img.Image;
                imagesDto.Add($"{httpContext.Request.Scheme}://{httpContext.Request.Host}/imgs/{img.Image}");
            }
            dTODoctor.Images = imagesDto; 
            return dTODoctor;
        }

        public List<DTODoctor> Search(string name)
        {
            HttpContext httpContext = httpContextAccessor.HttpContext;
            List<Doctor> doctors = context.Doctors.ToList();
            if (!doctors.Any())
                return null;
            List<DTODoctor> dTODoctors = new List<DTODoctor>();
            if (!string.IsNullOrEmpty(name))
            {
                doctors = context.Doctors
         .Where(p => p.Name.Contains(name) ||
                p.PhoneNumber.Contains(name) ||
             p.Specialization.Contains(name) || p.Street.Contains(name)).ToList();
                
                foreach (var doctor in doctors)
                {
                    DTODoctor dTODoctor = new DTODoctor();
                    dTODoctor.Name = doctor.Name;
                    dTODoctor.Street = doctor.Street;
                    dTODoctor.City = doctor.City;
                    dTODoctor.StartWork = doctor.StartWork;
                    dTODoctor.EndWork = doctor.EndWork;
                    dTODoctor.Latitude = doctor.Latitude;
                    dTODoctor.Longitude = doctor.Longitude;
                    dTODoctor.PhoneNumber = doctor.PhoneNumber;
                    dTODoctor.specialization = doctor.Specialization;
                    dTODoctor.DescriptionOfPlace = doctor.DescriptionOfPlace;
                    dTODoctor.LinkOfPlace = doctor.LinkOfPlace;
                    dTODoctor.id = doctor.Id;
                    List<string> imagesDto = new List<string>();
                    List<Images> imgs = context.images.Where(i => i.ServicId == doctor.Id).
                        Where(i => i.serviceName == doctor.Name).ToList();
                    foreach (var img in imgs)
                    {
                        //  ImagesDto imageDto = new ImagesDto();
                        // imageDto.Image = img.Image;
                        imagesDto.Add($"{httpContext.Request.Scheme}://{httpContext.Request.Host}/imgs/{img.Image}");
                    }
                    dTODoctor.Images = imagesDto;
                    dTODoctors.Add(dTODoctor);
                }
            }
            return dTODoctors;
        }

            public List<DTODoctor> searchByNameOfSpecialization(string name)
        {
            HttpContext httpContext = httpContextAccessor.HttpContext;
            List<Doctor>  doctors = context.Doctors.
                Where(d => d.Specialization == name).ToList();
            if (!doctors.Any())
                return null;
            List<DTODoctor> dTODoctors = new List<DTODoctor>();
            foreach (var doctor in doctors)
            {
                DTODoctor dTODoctor = new DTODoctor();
                dTODoctor.Name = doctor.Name;
                dTODoctor.Street = doctor.Street;
                dTODoctor.City = doctor.City;
                dTODoctor.StartWork = doctor.StartWork;
                dTODoctor.EndWork = doctor.EndWork;
                dTODoctor.PhoneNumber = doctor.PhoneNumber;
                dTODoctor.specialization = doctor.Specialization;
                dTODoctor.DescriptionOfPlace = doctor.DescriptionOfPlace;
                dTODoctor.LinkOfPlace = doctor.LinkOfPlace;
                dTODoctor.Longitude = doctor.Longitude;
                dTODoctor.Latitude = doctor.Latitude;
                dTODoctor.id = doctor.Id;

                List<string> imagesDto = new List<string>();
                List<Images> imgs = context.images.Where(i => i.ServicId == doctor.Id).
                    Where(i => i.serviceName == doctor.Name).ToList();
                foreach (var img in imgs)
                { 
                    //  ImagesDto imageDto = new ImagesDto();
                    // imageDto.Image = img.Image;
                    imagesDto.Add($"{httpContext.Request.Scheme}://{httpContext.Request.Host}/imgs/{img.Image}");
                }
                dTODoctor.Images = imagesDto;
                dTODoctors.Add(dTODoctor);
            }
            return dTODoctors;
        }

        public List<DTODoctor> GetAllDoctorsActiveNow()
        {
            DateTime now = DateTime.Now;
            List<Doctor> doctors = context.Doctors.
                Where(d => d.StartWork <= now && d.EndWork >= now).ToList();
            if (!doctors.Any())
                return null;
            List<DTODoctor> dTODoctors = new List<DTODoctor>();
            foreach (var doctor in doctors)
            {
                DTODoctor dTODoctor = new DTODoctor();
                dTODoctor.Name = doctor.Name;
                dTODoctor.Street = doctor.Street;
                dTODoctor.City = doctor.City;
                dTODoctor.StartWork = doctor.StartWork;
                dTODoctor.EndWork = doctor.EndWork;
                dTODoctor.PhoneNumber = doctor.PhoneNumber;
                dTODoctor.specialization = doctor.Specialization;
                dTODoctor.DescriptionOfPlace = doctor.DescriptionOfPlace;
                dTODoctor.LinkOfPlace = doctor.LinkOfPlace;
                dTODoctor.Longitude = doctor.Longitude;
                dTODoctor.Latitude = doctor.Latitude;
                dTODoctor.id = doctor.Id;

                List<string> imagesDto = new List<string>();
                List<Images> imgs = context.images.Where(i => i.ServicId == doctor.Id).
                    Where(i => i.serviceName == doctor.Name).ToList();
                foreach (var img in imgs)
                {
                    //  ImagesDto imageDto = new ImagesDto();
                    // imageDto.Image = img.Image
                    HttpContext httpContext = httpContextAccessor.HttpContext;
                    imagesDto.Add($"{httpContext.Request.Scheme}://{httpContext.Request.Host}/imgs/{img.Image}");
                }
                dTODoctor.Images = imagesDto;
                dTODoctors.Add(dTODoctor);
            }
            return dTODoctors;
        }


        //Add,,Update,Delete FOR Review.......................................................................
        public int CreateReview(string U, int DoctorId, DTOReview dTOReview)
        { 
            Doctor doctor = context.Doctors.SingleOrDefault
                (d => d.Id == DoctorId);
            Review review = new Review();
            review.ReviewDate = DateTime.Now;
            review.Rating = dTOReview.Rating;
            review.Comment = dTOReview.Comment;
            review.ServicId = DoctorId;
            review.UserId = U;
            review.serviceName = dTOReview.serviceName;
            //   var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            //  review.UserId = "4d01d3c0-7ee9-4654-b272-47dc4d275eec";
            context.Reviews.Add(review);
            context.SaveChanges();
         //   doctor.Reviews.Add(review);
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
            List<Review> reviews = context.Reviews.Where(r => r.ServicId == ServiceId ).
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
