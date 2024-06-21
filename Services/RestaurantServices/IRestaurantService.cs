using GraduationProject.DTO.DTOForRestaurants;
using GraduationProject.DTO.DTOReview;
using GraduationProject.Models;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
namespace GraduationProject.Services.RestaurantServices
{
    public interface IRestaurantService
    {
        RestaurantDto GetResturantById(int id);
        List<RestaurantDto> getAll();
        List<Restaurant> getAllBtSortReview();

        int Create(AddRestaurantDto dto, List<IFormFile> imageFiles);
        void Delete(int id);
        void Update(int id, AddRestaurantDto dto  );

        int CreateMenuForRest(AddMenuItemsDto dto);
        void DeleteMenu(int id);
        void UpdateMenuItem(int id, AddMenuItemsDto dto);

        List<RestaurantDto> Search(string name);
     //   List<RestauranttDto> searchByNameOfMeal(string name);
        //List<RestauranttDto> searchByNameOfCategory(string name);

        List<MenuItemsDto> GetAll(int restaurantId);
        //   List<MenuItem> GetmenuById(int menuId);
        int CreateReview(string UserId, int WorkspaceId, DTOReview dTOReview);
        void DeleteReview(int id);
        void UpdateReview(int id, DTOReview dto);
        List<DTOOReview> GetAllReviews(int ServiceId, string name);
    }
}
