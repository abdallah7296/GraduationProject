using GraduationProject.DTO.DTOForDoctors;
using GraduationProject.DTO.DTOForWorkspace;
using GraduationProject.DTO.DTOPharmacies;
using GraduationProject.DTO.DTOReview;
using GraduationProject.Models;
using GraduationProject.Services.DoctorsServices;
using GraduationProject.Services.PharmaciesServices;
using GraduationProject.Services.WorkSpaceServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GraduationProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class pharmacieController : ControllerBase
    {
        private readonly IPharmaciesServices   pharmaciesServices;
        private readonly UserManager<User> usermanger;
        public pharmacieController(IPharmaciesServices _pharmaciesServices, UserManager<User> usermanger)
        {
            pharmaciesServices = _pharmaciesServices;
            this.usermanger = usermanger;
        }
        //create,,delete,,update for pharmacie.....................................................................................
        [Authorize(Roles = "Admin")]
        [HttpPost("CreatePharmacie")]
        public ActionResult CreatePharmacie([FromForm] AddPharmacieDto dTOPharmacie, List<IFormFile> files)//,  IFormFile file)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            pharmaciesServices.Create(dTOPharmacie, files);
            return Ok("Add Done");
        }

        [Authorize(Roles = "Admin")]
        [HttpPut("UpdatePharmacie")]
        public ActionResult UpdatePharmacie(int id,  [FromForm] AddPharmacieDto dTOPharmacie)
        {
            pharmaciesServices.Update(id, dTOPharmacie);
            return Ok("Update Pharmacie Done");
        }

        [Authorize(Roles = "Admin")]
        [HttpDelete("DeletePharmacie")]
        public ActionResult DeletePharmacie(int id)
        {
            pharmaciesServices.Delete(id);

            return Ok("Delete Pharmacie Done");
        }

        //................................................................
        [HttpGet("GetAllPharmacies")]
        public IActionResult GetAllPharmacies()
        {
            List<DTOOPharmacie>   pharmacies  = pharmaciesServices.GetAllPlayPharmacies();
            if (pharmacies  == null)
                return BadRequest("There is No Data");
            return Ok(pharmacies);
        }

        [HttpGet("GetAllPharmaciesBySortReview")]
        public IActionResult GetAllPharmaciesBySortReview()
        {
            List<Pharmacies> pharmacies = pharmaciesServices.GetAllPlayPharmaciesBySortReview();
            if (pharmacies == null)
                return BadRequest("There is No Data");
            return Ok(pharmacies);
        }

        [HttpPost("GetPharmacieById")]//route
        //  [Authorize(Roles = UserRoles.Admin )]
        public ActionResult GetPharmacieById(int id)
        {
             DTOOPharmacie pharmacies = pharmaciesServices.GetPharmacieByID(id);
            if (pharmacies == null)
                return BadRequest("There is No Data");
            return Ok(pharmacies);
        }


        [HttpPost("Search")]
        //  [Authorize(Roles = UserRoles.Admin )]
        public ActionResult Search([FromForm] string name)
        {
            var pharmacie = pharmaciesServices.Search(name) ;
            if (pharmacie == null)
                return BadRequest("There is No Data");
            return Ok(pharmacie);
        }


        [HttpGet("GetAllPharmaciesActiveNow")]
        public IActionResult GetAllPharmaciesActiveNow()
        {
            List<DTOOPharmacie> pharmacies = pharmaciesServices.GetAllPharmaciesActiveNow();
            if (pharmacies == null)
                return BadRequest("There is No Data");
            return Ok(pharmacies);
        }

         //Review............................................................................

        [HttpPost("CreateReviewForPharmacie")]
        public  async Task<IActionResult> CreateReview(int pharmacieId, [FromForm] DTOReview dTOReview)//,  IFormFile file)
        {
            User user = await usermanger.GetUserAsync(User);
            var id = pharmaciesServices.CreateReview(user.Id, pharmacieId, dTOReview);
            return Ok("Add Done");
        }


        [HttpPut("UpdateReview")]
        public ActionResult UpdateReview(int pharmacieId, [FromForm] DTOReview dTOReview)//, IFormFile file)
        {
            pharmaciesServices.UpdateReview(pharmacieId, dTOReview);//,file);
            return Ok("Update Review Done");
        }


        [HttpDelete("DeleteReview")]
        public ActionResult DeleteReview(   int id)
        {
            pharmaciesServices.DeleteReview(id);
            return Ok("Delete  Review Done");
        }

        [HttpPost("GetAllReviewForPharmacies")]
        public ActionResult GetAllReview(int pharmacieId, string name)
        {
            List<DTOOReview> dTOReviews = pharmaciesServices.GetAllReviews(pharmacieId, name);
            return Ok(dTOReviews);
        }
        
    
    }
}
