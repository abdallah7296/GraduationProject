using GraduationProject.DTO.DTOForRestaurants;
using GraduationProject.DTO.DTOReview;
using GraduationProject.Models;
using GraduationProject.Services.RestaurantServices;
using GraduationProject.Services.WorkSpaceServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using OpenQA.Selenium;
using System.Collections.Generic;
using System.IO;
using System.Security.Claims;
using System.Threading.Tasks;

namespace GraduationProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class RestaurantController : ControllerBase
    {
        private readonly IRestaurantService _restaurantService;
        private readonly UserManager<User> usermanger;
        public RestaurantController(IRestaurantService restaurantService, UserManager<User> usermanger)
        {
            _restaurantService = restaurantService;
            this.usermanger = usermanger;
        }

        //Add,,Delete,,Update............................................................................
        [HttpPost("Create  Restaurant")]
        [Authorize(Roles = "Admin")]
        public ActionResult CreateRestaurant([FromForm]AddRestaurantDto restaurantDto, List<IFormFile> imageFiles)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            _restaurantService.Create(restaurantDto,imageFiles);
            return Ok("Add Done");
        }
        [Authorize(Roles = "Admin")]
        [HttpPut("UpdateRestaurant")]
        public ActionResult Update(int id, [FromForm] AddRestaurantDto dto)
        {
            _restaurantService.Update(id, dto); 
            return Ok("Update Resturant Done");
        }
        [Authorize(Roles = "Admin")]
        [HttpDelete("DeleteRestaurant")]
        public ActionResult Delete( int id)
        {
            _restaurantService.Delete(id);
            return Ok("Delete Restaurant Done");
        }

        [Authorize(Roles = "Admin")]
        [HttpPost("CreateMenuItemForRestaurant")]
        public ActionResult CreateMenuItemForRestaurant([FromForm] AddMenuItemsDto dto)
        {
            _restaurantService.CreateMenuForRest(dto);
            return Ok("Add Done");
        }
        [Authorize(Roles = "Admin")]
        [HttpPut("UpdateMenuItemForRestaurant")]
        public ActionResult UpdateMenuItemForRestaurant(int id, [FromForm] AddMenuItemsDto dto)
        {
            _restaurantService.UpdateMenuItem(id,dto);
            return Ok("Update  Done");
        }
        [HttpDelete("DeleteMenuItemForRestaurant")]
        [Authorize(Roles = "Admin")]
        public ActionResult DeleteMenuItemForRestaurant( int id)
        {
            _restaurantService.DeleteMenu(id);
            return Ok("Delete Done");
        }

        //.......................................................................................
        [HttpGet("GetAllRestaurants")]
        public IActionResult GetAllRestaurants()
        {
            List<RestaurantDto> restaurants = _restaurantService.getAll();
            if (restaurants == null)
                return BadRequest("There is No Data");
            return Ok(restaurants);
 
        }
        [HttpGet("GetAllRestaurantsBySortReview")]
        public IActionResult GetAllRestaurantsBySortReview()
        {
            List<Restaurant> restaurants = _restaurantService.getAllBtSortReview();
            if (restaurants == null)
                return BadRequest("There is No Data");
            return Ok(restaurants);

        }
        [HttpGet("GetRestaurantById")]
        public IActionResult GetRestaurant(int id)
        {
            var Restaurant = _restaurantService.GetResturantById(id);
            if (Restaurant == null)
                return BadRequest("There is No Data");
            return Ok(Restaurant);
        }


        [HttpPost("Search")]
        public ActionResult<RestaurantDto> Search([FromForm] string name)
        {
           var restaurant = _restaurantService.Search(name);
            if (restaurant == null)
                return BadRequest("There is No Data");
            return Ok(restaurant);
        }

     
 

        [HttpGet("GetAllMenuItemsForResturant")]
        public ActionResult<List<MenuItemsDto>> GetAll(int restaurantId)  //[FromRoute] int restaurantId)
        {
            var result = _restaurantService.GetAll(restaurantId);
            if (result == null)
                return BadRequest("There is No Data");
            return Ok(result);
        }

        //Review............................................................................

        [HttpPost("CreateReviewForResturant")]
        public async Task<IActionResult> CreateReview(string U, int ResrId, [FromForm] DTOReview dTOReview)//,  IFormFile file)
        {
            User user = await usermanger.GetUserAsync(User);
            var id = _restaurantService.CreateReview(user.Id,ResrId, dTOReview);
            return Ok("Add Done");
        }


        [HttpPut("UpdateReview")]
        public ActionResult UpdateReview(int ResrId, [FromForm] DTOReview dTOReview)//, IFormFile file)
        {
            _restaurantService.UpdateReview(ResrId, dTOReview);//,file);
            return Ok("Update Review Done");
        }


        [HttpDelete("DeleteReview")]
        public ActionResult DeleteReview(  int id)
        {
            _restaurantService.DeleteReview(id);
            return Ok("Delete  Review Done");
        }

        [HttpPost("GetAllReviewForResturant")]
        public ActionResult GetAllReview(int ResrId, [FromForm] string name)
        {
            List<DTOOReview> dTOReviews =  _restaurantService.GetAllReviews(ResrId, name);
            return Ok(dTOReviews);
        }
   
    }
}
