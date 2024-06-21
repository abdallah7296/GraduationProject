using GraduationProject.DTO.DTOForWorkspace;
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
using System.Security.Claims;
using System.Xml.Linq;
using ZendeskApi_v2.Models.HelpCenter.Categories;

namespace GraduationProject.Services.WorkSpaceServices
{
    public class WorkspaceRepository : IWorkspaceRepository
    {
        Context context = new Context();
        private readonly IHttpContextAccessor httpContextAccessor;

        public WorkspaceRepository(IHttpContextAccessor httpContextAccessor) 
        {
            this.httpContextAccessor = httpContextAccessor;
        }

        public List<DTOOWorkspace> GetAllWorkspacs()
        {
            List<Workspace> workspaces = context.workspaces.ToList();
            if (!workspaces.Any())
                return null;
            List<DTOOWorkspace> dTOWorkspaces = new List<DTOOWorkspace>();
            foreach (var workspace in workspaces)
            {
                DTOOWorkspace dTOWorkspace = new DTOOWorkspace();
                dTOWorkspace.Name = workspace.Name;
                dTOWorkspace.PhoneNumber = workspace.PhoneNumber;
                dTOWorkspace.City = workspace.City;
                dTOWorkspace.Street = workspace.Street;
                dTOWorkspace.DescriptionOfPlace = workspace.DescriptionOfPlace;
                dTOWorkspace.LinkOfPlace = workspace.LinkOfPlace;
                dTOWorkspace.StartWork = workspace.StartWork;
                dTOWorkspace.EndWork = workspace.EndWork;
                dTOWorkspace.Latitude = workspace.Latitude;
                dTOWorkspace.Longitude = workspace.Longitude;
                dTOWorkspace.id = workspace.Id;
                List<string> imagesDto = new List<string>();
                List<Images> imgs = context.images.Where(i => i.ServicId == workspace.Id).
                    Where(i => i.serviceName == workspace.Name).ToList();
                foreach (var img in imgs)

                {
                    //  ImagesDto imageDto = new ImagesDto();
                    // imageDto.Image = img.Image;
                    HttpContext httpContext = httpContextAccessor.HttpContext;
                    imagesDto.Add($"{httpContext.Request.Scheme}://{httpContext.Request.Host}/imgs/{img.Image}");
                }
                dTOWorkspace.Images = imagesDto;
                dTOWorkspaces.Add(dTOWorkspace);
            }
            return dTOWorkspaces;
        }

        public List<DTOOWorkspace> GetAllWorkspacesActiveNow()
        {
            DateTime now = DateTime.Now;
            List<Workspace> workspaces = context.workspaces.
                Where(d => d.StartWork <= now && d.EndWork >= now).ToList();
            if (!workspaces.Any())
                return null;
            List<DTOOWorkspace> dTOWorkspaces = new List<DTOOWorkspace>();
            foreach (var workspace in workspaces)
            {
                DTOOWorkspace dTOWorkspace = new DTOOWorkspace();
                dTOWorkspace.Name = workspace.Name;
                dTOWorkspace.PhoneNumber = workspace.PhoneNumber;
                dTOWorkspace.City = workspace.City;
                dTOWorkspace.Street = workspace.Street;
                dTOWorkspace.DescriptionOfPlace = workspace.DescriptionOfPlace;
                dTOWorkspace.LinkOfPlace = workspace.LinkOfPlace;
                dTOWorkspace.StartWork = workspace.StartWork;
                dTOWorkspace.EndWork = workspace.EndWork;
                dTOWorkspace.Latitude = workspace.Latitude;
                dTOWorkspace.Longitude = workspace.Longitude;
                dTOWorkspace.id = workspace.Id;
                List<string> imagesDto = new List<string>();
                List<Images> imgs = context.images.Where(i => i.ServicId == workspace.Id).
                    Where(i => i.serviceName == workspace.Name).ToList();
                foreach (var img in imgs)
                {
                    //  ImagesDto imageDto = new ImagesDto();
                    // imageDto.Image = img.Image;
                    HttpContext httpContext = httpContextAccessor.HttpContext;
                    imagesDto.Add($"{httpContext.Request.Scheme}://{httpContext.Request.Host}/imgs/{img.Image}");
                }
                dTOWorkspace.Images = imagesDto;
                dTOWorkspaces.Add(dTOWorkspace);
            }
            return dTOWorkspaces;
        }
        public List<Workspace> GetAllWorkspacesByReview()
        {


            List<Workspace> workspaces = context.workspaces
             .ToList();
            foreach (var workspace in workspaces)
            {
                List<Review> reviews =  context.Reviews.Where(r=>r.ServicId==workspace.Id ).
                    Where(i => i.serviceName == workspace.Name).ToList();
                
                
                if (reviews.Any())
                {
                    int averageRate =( int) reviews.Average(r => r.Rating);
                    workspace.averageRate = averageRate;
                    context.SaveChanges();
                }
            }

            List<Workspace> Workspaces = context.workspaces.
               OrderByDescending(d => d.averageRate).ToList();
            if (!Workspaces.Any())
                return null;
         
            return Workspaces;
        }
        public DTOOWorkspace GetWorkSpaceByID(int id)
        {
            Workspace workspace = context.workspaces.SingleOrDefault(d => d.Id == id);
            if (workspace == null)
                return null;
            DTOOWorkspace dTOWorkspace = new DTOOWorkspace();
            dTOWorkspace.Name = workspace.Name;
            dTOWorkspace.PhoneNumber = workspace.PhoneNumber;
            dTOWorkspace.City = workspace.City;
            dTOWorkspace.Street = workspace.Street;
            dTOWorkspace.DescriptionOfPlace = workspace.DescriptionOfPlace;
            dTOWorkspace.LinkOfPlace = workspace.LinkOfPlace;
            dTOWorkspace.StartWork = workspace.StartWork;
            dTOWorkspace.EndWork = workspace.EndWork;
            dTOWorkspace.Latitude = workspace.Latitude;
            dTOWorkspace.Longitude = workspace.Longitude;
            dTOWorkspace.id = workspace.Id;
            List<string> imagesDto = new List<string>();
            List<Images> imgs = context.images.Where(i => i.ServicId == workspace.Id).
                Where(i => i.serviceName == workspace.Name).ToList();
            foreach (var img in imgs)
            {
                //  ImagesDto imageDto = new ImagesDto();
                // imageDto.Image = img.Image;
                HttpContext httpContext = httpContextAccessor.HttpContext;
                imagesDto.Add($"{httpContext.Request.Scheme}://{httpContext.Request.Host}/imgs/{img.Image}");
            }
            dTOWorkspace.Images = imagesDto;
            return dTOWorkspace;
        }

        public List<DTOOWorkspace> Search (string name)
        {
            var workspaces = context.workspaces
      .Where(p => p.Name.Contains(name) ||
        p.PhoneNumber.Contains(name) || p.Street.Contains(name)).ToList(); 
            if (!workspaces.Any())
                return null;
            List<DTOOWorkspace> dTOWorkspaces = new List<DTOOWorkspace>();
            foreach (var workspace in workspaces)
            {
                DTOOWorkspace dTOWorkspace = new DTOOWorkspace();
                dTOWorkspace.Name = workspace.Name;
                dTOWorkspace.PhoneNumber = workspace.PhoneNumber;
                dTOWorkspace.City = workspace.City;
                dTOWorkspace.Street = workspace.Street;
                dTOWorkspace.DescriptionOfPlace = workspace.DescriptionOfPlace;
                dTOWorkspace.LinkOfPlace = workspace.LinkOfPlace;
                dTOWorkspace.StartWork = workspace.StartWork;
                dTOWorkspace.EndWork = workspace.EndWork;
                dTOWorkspace.Latitude = workspace.Latitude;
                dTOWorkspace.Longitude = workspace.Longitude;
                dTOWorkspace.id = workspace.Id;
                List<string> imagesDto = new List<string>();
                List<Images> imgs = context.images.Where(i => i.ServicId == workspace.Id).
                    Where(i => i.serviceName == workspace.Name).ToList();
                foreach (var img in imgs)
                {
                    //  ImagesDto imageDto = new ImagesDto();
                    // imageDto.Image = img.Image;
                    HttpContext httpContext = httpContextAccessor.HttpContext;
                    imagesDto.Add($"{httpContext.Request.Scheme}://{httpContext.Request.Host}/imgs/{img.Image}");
                }
                dTOWorkspace.Images = imagesDto;
                dTOWorkspaces.Add(dTOWorkspace);
            }
            return dTOWorkspaces;
        }
        //Add,,Update,Delete FOR WorkSpace......................................................................

        public int Create(AddWorkspaceDto dTOWorkspace, List<IFormFile> imageFiles)
        {
            Workspace workspace = new Workspace();
            workspace.Name = dTOWorkspace.Name;
            workspace.PhoneNumber = dTOWorkspace.PhoneNumber;
            workspace.City = dTOWorkspace.City;
            workspace.Street = dTOWorkspace.Street;
            workspace.DescriptionOfPlace = dTOWorkspace.DescriptionOfPlace;
            workspace.LinkOfPlace = dTOWorkspace.LinkOfPlace;
            workspace.StartWork = dTOWorkspace.StartWork;
            workspace.EndWork = dTOWorkspace.EndWork;
            workspace.Latitude = dTOWorkspace.Latitude;
            workspace.Longitude = dTOWorkspace.Longitude;
            context.workspaces.Add(workspace);
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
                image.ServicId = workspace.Id;
                image.Image = fileName;
                image.serviceName = workspace.Name;
                context.images.Add(image);
                context.SaveChanges();
            }
            return workspace.Id;
        }

        public void Delete(int id)
        {
            Workspace workspace = context.workspaces.SingleOrDefault(d => d.Id == id);
            List<Images> images = context.images.Where(m => m.ServicId == workspace.Id).
                Where(s=>s.serviceName==workspace.Name).ToList();
            foreach (var image in images)
            {
                context.images.Remove(image);
            }
            context.workspaces.Remove(workspace);
            context.SaveChanges();
        }
        public void Update(int id, AddWorkspaceDto dTOWorkspace )
        {
            Workspace workspace = context.workspaces.SingleOrDefault(d => d.Id == id);
            workspace.Name = dTOWorkspace.Name;
            workspace.PhoneNumber = dTOWorkspace.PhoneNumber;
            workspace.City = dTOWorkspace.City;
            workspace.Street = dTOWorkspace.Street;
            workspace.DescriptionOfPlace = dTOWorkspace.DescriptionOfPlace;
            workspace.LinkOfPlace = dTOWorkspace.LinkOfPlace;
            workspace.StartWork = dTOWorkspace.StartWork;
            workspace.EndWork = dTOWorkspace.EndWork;
            workspace.Latitude = dTOWorkspace.Latitude;
            workspace.Longitude = dTOWorkspace.Longitude;
            context.SaveChanges();
            
            
        }
        ////................................................................................./// /
        //Review...................................................................
        public int CreateReview(string U,   int WorkspaceId, DTOReview dTOReview)
        {
            Review review = new Review();
            review.ReviewDate = DateTime.Now;
            review.Rating = dTOReview.Rating;
            review.Comment = dTOReview.Comment;
            review.ServicId = WorkspaceId;
            
            review.serviceName = dTOReview.serviceName;
            review.UserId = U;
            //   var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            //  review.UserId = "4d01d3c0-7ee9-4654-b272-47dc4d275eec";
            context.Reviews.Add(review);
            context.SaveChanges();
//            workspace.Reviews.Add(review);

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
