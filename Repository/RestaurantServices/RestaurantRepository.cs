 
using GraduationProject.DTO.DTOForRestaurants;
using GraduationProject.DTO.DTOPharmacies;
using GraduationProject.DTO.DTOReview;
using GraduationProject.DTO.Images;
using GraduationProject.Migrations;
using GraduationProject.Models;
using GraduationProject.Services.RestaurantServices;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Xml.Linq;
using static Microsoft.Extensions.Logging.EventSource.LoggingEventSource;

namespace GraduationProject.Services
{


    public class RestaurantRepository : IRestaurantService
    {
        private readonly Context context;
        private readonly IHttpContextAccessor httpContextAccessor;

        public RestaurantRepository(Context _context,IHttpContextAccessor httpContextAccessor)
        {
            context = _context;
            this.httpContextAccessor = httpContextAccessor;
        }
        public int Create(AddRestaurantDto restaurantDto, List<IFormFile> imageFiles)
        {
            Menu menu = new Menu();
            context.Menus.Add(menu);
            context.SaveChanges();
            var restaurant = new Restaurant
            {
                Name = restaurantDto.Name,
                Phone = restaurantDto.Phone,
                Email = restaurantDto.Email,
                HasDelivery = restaurantDto.HasDelivery,
                Street = restaurantDto.Street,
                City = restaurantDto.City,
                Latitude = restaurantDto.Latitude,
                Longitude = restaurantDto.Longitude,
                DescriptionOfPlace = restaurantDto.DescriptionOfPlace,
                LinkOfPlace = restaurantDto.LinkOfPlace,
                MenuId = menu.MenuId
            };
            context.Restaurants.Add(restaurant);
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
                image.ServicId = restaurant.RestaurantId;
                image.Image = fileName;
                image.serviceName = restaurant.Name;
                context.images.Add(image);
                context.SaveChanges();
            }
            return restaurant.RestaurantId;
        }

        public void Delete(int id)
        {
            Restaurant restaurant = context.Restaurants.Include(r => r.menu.menuItems).FirstOrDefault(r => r.RestaurantId == id);
            List<Images> images = context.images.Where(m => m.ServicId == restaurant.RestaurantId).
            Where(s => s.serviceName == restaurant.Name).ToList();
            if(images.Any())
            {
                foreach (var image in images)
                {
                    context.images.Remove(image);
                }
            }
          
            if(restaurant.menu.menuItems.Any())
            {
                foreach (var menuItem in restaurant.menu.menuItems)
                {
                    context.MenuItems.Remove(menuItem);
                }
            }
           
            context.Menus.Remove(restaurant.menu);
            context.Restaurants.Remove(restaurant);
            context.SaveChanges();
        }

        public void Update(int id, AddRestaurantDto dto)
        {

            Restaurant service = context.Restaurants.FirstOrDefault(s => s.RestaurantId == id);
            service.Name = dto.Name;
            service.Phone = dto.Phone;
            service.City = dto.City;
            service.Street = dto.Street;
            service.DescriptionOfPlace = dto.DescriptionOfPlace;
            service.StartWork = dto.StartWork;
            service.EndWork = dto.EndWork;
            service.Latitude = dto.Latitude;
            service.Longitude = dto.Longitude;
           
            context.SaveChanges();
        }


        public int CreateMenuForRest(AddMenuItemsDto dto)
        {
            MenuItem menuItem = new MenuItem();
            menuItem.Name = dto.Name;
            menuItem.Price = dto.Price;
            menuItem.Taste = dto.Taste;
            menuItem.size = dto.size;
            menuItem.TypeOfMeal = dto.TypeOfMeal;
            Restaurant rest = context.Restaurants.Include(r=>r.menu.menuItems).FirstOrDefault(r => r.Name == dto.NameOfRest);
            menuItem.MenuId = rest.MenuId;
            context.MenuItems.Add(menuItem);
            context.SaveChanges();
            return menuItem.MenuItemId;
        }

        public void DeleteMenu(int id)
        {
            MenuItem menuItem = context.MenuItems.FirstOrDefault(m => m.MenuItemId == id);
            context.MenuItems.Remove(menuItem);
            context.SaveChanges();
        }

        public void UpdateMenuItem(int id, AddMenuItemsDto dto)
        {
            MenuItem menuItem = context.MenuItems.FirstOrDefault(m => m.MenuItemId == id);
            menuItem.Name = dto.Name;
            menuItem.Price = dto.Price;
            menuItem.Taste = dto.Taste;
            menuItem.size = dto.size;
            menuItem.TypeOfMeal = dto.TypeOfMeal;
            Restaurant rest = context.Restaurants.Include(r => r.menu.menuItems).FirstOrDefault(r => r.Name == dto.NameOfRest);
            menuItem.MenuId = rest.MenuId;
            context.SaveChanges();
         }
        //..............................................................................
        public RestaurantDto GetResturantById(int id)
        {
               var restaurant = context.Restaurants.
                Include(r => r.menu.menuItems)
               .FirstOrDefault(r => r.RestaurantId == id);
               if (restaurant == null)
                   return null;
               RestaurantDto  rest = new RestaurantDto();
                rest.City = restaurant.City;
                rest.Street = restaurant.Street;
                rest.Email = restaurant.Email;
                rest.HasDelivery = restaurant.HasDelivery;
                rest.Phone = restaurant.Phone;
                rest.Name = restaurant.Name;
                rest.Latitude = restaurant.Latitude;
                rest.Longitude = restaurant.Longitude;
            rest.StartWork = restaurant.StartWork;
            rest.EndWork = restaurant.EndWork;
            rest.id = restaurant.RestaurantId;
               List<string> imagesDto = new List<string>();
            List<Images> imgs = context.images.Where(i => i.ServicId == id).
                Where(i => i.serviceName ==  restaurant.Name).ToList();
            foreach (var img in imgs)

            {
                //  ImagesDto imageDto = new ImagesDto();
                // imageDto.Image = img.Image;
                HttpContext httpContext = httpContextAccessor.HttpContext;
                imagesDto.Add($"{httpContext.Request.Scheme}://{httpContext.Request.Host}/imgs/{img.Image}");
            }
             rest.Images = imagesDto;
         
                rest.DescriptionOfPlace = restaurant.DescriptionOfPlace;
                rest.LinkOfPlace = restaurant.LinkOfPlace;
               List<MenuItemsDto> menuItems = new List<MenuItemsDto>();
               foreach (var item in restaurant.menu.menuItems)
               {
                  MenuItemsDto menuItem = new MenuItemsDto();
                  menuItem.Name = item.Name;
                  menuItem.Taste = item.Taste;
                  menuItem.Price = item.Price;
                  menuItem.size = item.size;
                  menuItem.Description = item.Description;
                  menuItem.TypeOfMeal = item.TypeOfMeal;
                menuItem.id = item.MenuItemId;
                  menuItems.Add(menuItem);
              }
              rest.menuItems = menuItems;
            return rest;
        }

        public List<RestaurantDto> getAll()
        {
            List<Restaurant> restaurants = context.Restaurants.
                Include(r => r.menu.menuItems).ToList();
            if (!restaurants.Any() )
                return null;
            List<RestaurantDto> RestaurantsDto = new List<RestaurantDto>();
            foreach (var restaurant in restaurants)
            {
                RestaurantDto rest = new RestaurantDto();
                rest.City = restaurant.City;
                rest.Street = restaurant.Street;
                rest.Email = restaurant.Email;
                rest.HasDelivery = restaurant.HasDelivery;
                rest.Phone = restaurant.Phone;
                rest.Name = restaurant.Name;
                rest.Latitude = restaurant.Latitude;
                rest.Longitude = restaurant.Longitude;
                rest.StartWork = restaurant.StartWork;
                rest.EndWork = restaurant.EndWork;
                rest.id = restaurant.RestaurantId;
                List<string> imagesDto = new List<string>();
                List<Images> imgs = context.images.Where(i => i.ServicId == restaurant.RestaurantId).
                Where(i => i.serviceName == restaurant.Name).ToList();
                foreach (var img in imgs)
                {
                    HttpContext httpContext = httpContextAccessor.HttpContext;
                    imagesDto.Add($"{httpContext.Request.Scheme}://{httpContext.Request.Host}/imgs/{img.Image}");
                }
                rest.Images = imagesDto;
                rest.DescriptionOfPlace = restaurant.DescriptionOfPlace;
                rest.LinkOfPlace = restaurant.LinkOfPlace;
                List<MenuItemsDto> menuItems = new List<MenuItemsDto>();
                foreach (var item in restaurant.menu.menuItems)
                {
                    MenuItemsDto menuItem = new MenuItemsDto();
                    menuItem.Name = item.Name;
                    menuItem.Taste = item.Taste;
                    menuItem.Price = item.Price;
                    menuItem.size = item.size;
                    menuItem.Description = item.Description;
                    menuItem.TypeOfMeal = item.TypeOfMeal;
                    menuItem.id = item.MenuItemId;

                    menuItems.Add(menuItem);
                }
                rest.menuItems = menuItems;
                RestaurantsDto.Add(rest);
            }
            return RestaurantsDto;
        }

        public List<RestaurantDto> Search(string name)
        {
            var restaurants = context.Restaurants
     .Include(r => r.menu.menuItems).
     Where(p => p.Name.Contains(name) ||
       p.Phone.ToString().Contains(name) ||p.menu.menuItems.Any(it=>it.Name==name) || 
       p.menu.menuItems.Any(it => it.TypeOfMeal == name) || p.menu.menuItems.Any(it => it.Price.ToString() == name)
       || p.Street.Contains(name)).ToList();
            if (!restaurants.Any())
                return null;
            List<RestaurantDto> RestaurantsDto = new List<RestaurantDto>();
            foreach (var restaurant in restaurants)
            {
                RestaurantDto rest = new RestaurantDto();
                rest.City = restaurant.City;
                rest.Street = restaurant.Street;
                rest.Email = restaurant.Email;
                rest.HasDelivery = restaurant.HasDelivery;
                rest.Phone = restaurant.Phone;
                rest.Name = restaurant.Name;
                rest.Latitude = restaurant.Latitude;
                rest.Longitude = restaurant.Longitude;
                rest.StartWork = restaurant.StartWork;
                rest.EndWork = restaurant.EndWork;
                rest.id = restaurant.RestaurantId;
                List<string> imagesDto = new List<string>();
                List<Images> imgs = context.images.Where(i => i.ServicId == restaurant.RestaurantId).
                Where(i => i.serviceName == restaurant.Name).ToList();
                foreach (var img in imgs)
                {
                    HttpContext httpContext = httpContextAccessor.HttpContext;
                    imagesDto.Add($"{httpContext.Request.Scheme}://{httpContext.Request.Host}/imgs/{img.Image}");
                }
                rest.Images = imagesDto;
                rest.DescriptionOfPlace = restaurant.DescriptionOfPlace;
                rest.LinkOfPlace = restaurant.LinkOfPlace;
                List<MenuItemsDto> menuItems = new List<MenuItemsDto>();
                foreach (var item in restaurant.menu.menuItems)
                {
                    MenuItemsDto menuItem = new MenuItemsDto();
                    menuItem.Name = item.Name;
                    menuItem.Taste = item.Taste;
                    menuItem.Price = item.Price;
                    menuItem.size = item.size;
                    menuItem.Description = item.Description;
                    menuItem.TypeOfMeal = item.TypeOfMeal;
                    menuItem.id = item.MenuItemId;

                    menuItems.Add(menuItem);
                }
                rest.menuItems = menuItems;
                RestaurantsDto.Add(rest);
            }
            return RestaurantsDto;
        }

  
    public List<MenuItemsDto> GetAll(int restaurantId)//menu
        {
            Restaurant restaurant = context.Restaurants.Include(m=>m.menu.menuItems).
                FirstOrDefault(r=>r.RestaurantId==restaurantId);
            if (restaurant == null)
                return null;
            List<MenuItemsDto> menuItems = new List<MenuItemsDto>();
            foreach (var item in restaurant.menu.menuItems)
            {
                MenuItemsDto menuItem = new MenuItemsDto();
                menuItem.Name = item.Name;
                menuItem.Taste = item.Taste;
                menuItem.Price = item.Price;
                menuItem.size = item.size;
                menuItem.Description = item.Description;
                menuItem.TypeOfMeal = item.TypeOfMeal;
                menuItem.id = item.MenuItemId;
                menuItems.Add(menuItem);
            }
            return menuItems;
        }
        public int CreateReview( string U,int RestaurantId, DTOReview dTOReview)
        {
          Restaurant restaurant = context.Restaurants.SingleOrDefault
                (d => d.RestaurantId == RestaurantId) ;
            Review review = new Review();
            review.ReviewDate = DateTime.Now;
            review.Rating = dTOReview.Rating;
            review.Comment = dTOReview.Comment;
            review.ServicId = RestaurantId;
            review.serviceName = dTOReview.serviceName;
            review.UserId = U;
            context.Reviews.Add(review);
            context.SaveChanges();
            //restaurant.Reviews.Add(review);

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
            List<Review> reviews = context.Reviews.Where(r => r.ServicId== ServiceId).
                 Where(i => i.serviceName == name).ToList();
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

        public List<RestauranttDto> searchByNameOfCategory(string name)
        {

            MenuItem menuItem = context.MenuItems.FirstOrDefault(r => r.TypeOfMeal.Contains(name));
            List<Restaurant> restaurants = context.Restaurants.
                Where(r => r.menu.menuItems.Contains(menuItem) ||
             r.menu.menuItems.Any(mi => mi.TypeOfMeal.Contains(name))).ToList();
            if (!restaurants.Any())
                return null;
            List<RestauranttDto> RestaurantsDto = new List<RestauranttDto>();
            foreach (var restaurant in restaurants)
            {
                RestauranttDto rest = new RestauranttDto();
                rest.City = restaurant.City;
                rest.Street = restaurant.Street;
                rest.Email = restaurant.Email;
                rest.HasDelivery = restaurant.HasDelivery;
                rest.Phone = restaurant.Phone;
                rest.Name = restaurant.Name;
                rest.Longitude = restaurant.Longitude;
                rest.Latitude = restaurant.Latitude;
                rest.StartWork = restaurant.StartWork;
                rest.EndWork = restaurant.EndWork;
                rest.id = restaurant.RestaurantId;
                List<string> imagesDto = new List<string>();
                List<Images> imgs = context.images.Where(i => i.ServicId == restaurant.RestaurantId).
                    Where(i => i.serviceName == restaurant.Name).ToList();
                foreach (var img in imgs)
                {
                    HttpContext httpContext = httpContextAccessor.HttpContext;
                    imagesDto.Add($"{httpContext.Request.Scheme}://{httpContext.Request.Host}/imgs/{img.Image}");
                }
                rest.Images = imagesDto;
                rest.DescriptionOfPlace = restaurant.DescriptionOfPlace;
                rest.LinkOfPlace = restaurant.LinkOfPlace;
                RestaurantsDto.Add(rest);
            }
            return RestaurantsDto;
        }

        public List<Restaurant> getAllBtSortReview()
        {


            List<Restaurant> rests = context.Restaurants
             .ToList();
            foreach (var rest in rests)
            {
                List<Review> reviews = context.Reviews.Where(r => r.ServicId == rest.RestaurantId).
                    Where(i => i.serviceName == rest.Name).ToList();


                if (reviews.Any())
                {
                    int averageRate = (int)reviews.Average(r => r.Rating);
                    rest.averageRate = averageRate;
                    context.SaveChanges();
                }
            }

            List<Restaurant> restaurants = context.Restaurants.Include(r=>r).
               OrderByDescending(d => d.averageRate).ToList();
            return restaurants;
            //if (!restaurants.Any())
            //    return null;
            //List<RestaurantDto> RestaurantsDto = new List<RestaurantDto>();
            //foreach (var restaurant in restaurants)
            //{
            //    RestaurantDto rest = new RestaurantDto();
            //    rest.City = restaurant.City;
            //    rest.Street = restaurant.Street;
            //    rest.Email = restaurant.Email;
            //    rest.HasDelivery = restaurant.HasDelivery;
            //    rest.Phone = restaurant.Phone;
            //    rest.Name = restaurant.Name;
            //    rest.Latitude = restaurant.Latitude;
            //    rest.Longitude = restaurant.Longitude;
            //    List<string> imagesDto = new List<string>();
            //    List<Images> imgs = context.images.Where(i => i.ServicId == restaurant.RestaurantId).
            //    Where(i => i.serviceName == restaurant.Name).ToList();
            //    foreach (var img in imgs)

            //    {
            //        //  ImagesDto imageDto = new ImagesDto();
            //        // imageDto.Image = img.Image;
            //        imagesDto.Add(img.Image);
            //    }
            //    rest.Images = imagesDto;
              
            //    rest.DescriptionOfPlace = restaurant.DescriptionOfPlace;
            //    rest.LinkOfPlace = restaurant.LinkOfPlace;
            //    List<MenuItemsDto> menuItems = new List<MenuItemsDto>();
            //    foreach (var item in restaurant.menu.menuItems)
            //    {
            //        MenuItemsDto menuItem = new MenuItemsDto();
            //        menuItem.Name = item.Name;
            //        menuItem.Taste = item.Taste;
            //        menuItem.Price = item.Price;
            //        menuItem.size = item.size;
            //        menuItem.Description = item.Description;
            //        menuItem.TypeOfMeal = item.TypeOfMeal;
            //        menuItems.Add(menuItem);
            //    }
            //    rest.menuItems = menuItems;
            //    RestaurantsDto.Add(rest);
            //}
            //return RestaurantsDto;
          
        }

       
    }
}
