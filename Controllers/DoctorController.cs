using GraduationProject.DTO.DTOForDoctors;
using GraduationProject.DTO.DTOPharmacies;
using GraduationProject.DTO.DTOReview;
using GraduationProject.Models;
using GraduationProject.Services;
using GraduationProject.Services.DoctorsServices;
using GraduationProject.Services.WorkSpaceServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Server.IIS;
using OpenQA.Selenium.DevTools.V116.Console;
using System.Collections.Generic;
using System.Threading.Tasks;
using ZendeskApi_v2.Models.Constants;

namespace GraduationProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
   // [Authorize]
    public class DoctorController : ControllerBase
    {

        private readonly IDoctorService doctorRepository;

        private readonly UserManager<User> usermanger; 
        public DoctorController(IDoctorService _doctorRepository, UserManager<User> usermanger)
        {
            doctorRepository = _doctorRepository;
            this.usermanger = usermanger;
        }


        //Add,,delete,update for doctor.............................................................................
       [Authorize(Roles = "Admin")]
        [HttpPost("AddDoctor")]
        public ActionResult AddDoctor([FromForm] AddDoctorDto doctor, List<IFormFile> imageFiles)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            doctorRepository.Add(doctor, imageFiles);
            return Ok("Add is Done");
        }
        [Authorize(Roles = "Admin")]
        [HttpPut("UpdateDoctor")]
        public ActionResult Update(int id, [FromForm] AddDoctorDto doctor)
        {
            doctorRepository.Update(id,  doctor);
            return Ok("Update Doctor Done");
        }

        [Authorize(Roles = "Admin")]
        //  [Authorize(Roles = UserRoles.Admin )]
        [HttpDelete("DeleteDoctor")]
        public IActionResult Delete(int id)
        {
            doctorRepository.Delete(id);
            return Ok("Delete Doctor Done");
        }

        [HttpGet("GetAllDoctors")]
        public IActionResult GetAllDoctors()
        {
            List<DTODoctor> doctors = doctorRepository.GetAllDectors();
            if(doctors==null)
                return BadRequest("There is No Data");
            return Ok(doctors);
        }
        [HttpGet("GetAllDoctorsBySortReview")]
        public IActionResult GetAllDoctorsBySortReview()
        {
            List<Doctor> doctors = doctorRepository.GetAllDectorsBySortReview();
            if (doctors == null)
                return BadRequest("There is No Data");
            return Ok(doctors);
        }
        [HttpPost("GetDoctorById")]//route
        //[Authorize(Roles = UserRoles.Admin)]
        public ActionResult GetDoctorById(int id)
        {
           DTODoctor doctor = doctorRepository.GetDoctorById(id);
            if (doctor == null)
                return BadRequest("There is No Data");
            return Ok(doctor);
        }


        [HttpPost("Search")]
        //  [Authorize(Roles = UserRoles.Admin )]
        public ActionResult Search([FromForm] string name)
        {
            List<DTODoctor> doctor = doctorRepository.Search(name);
            if (doctor == null)
                return BadRequest("There is No Data");
            return Ok(doctor);
        }

 

        [HttpGet("GetAllDoctorsActiveNow")]
        public IActionResult GetAllDoctorsActiveNow()
        {
            List<DTODoctor> doctors = doctorRepository.GetAllDoctorsActiveNow();
            if (doctors == null)
                return BadRequest("There is No Data");
            return Ok(doctors);
        }
      
        //Review............................................................................

        [HttpPost("CreateReviewForDoctor")]
        public async Task<IActionResult> CreateReview(int DoctorId, [FromForm] DTOReview dTOReview)//,  IFormFile file)
        {
            User user = await usermanger.GetUserAsync(User);
            var id = doctorRepository.CreateReview(user.Id,DoctorId, dTOReview);
            return Ok("Add Done");
        }


        [HttpPut("UpdateReview")]
        public ActionResult UpdateReview(int DoctorId, [FromForm] DTOReview dTOReview)//, IFormFile file)
        {
            doctorRepository.UpdateReview(DoctorId, dTOReview);//,file);
            return Ok("Update Review Done");
        }


        [HttpDelete("DeleteReview")]
        public ActionResult DeleteReview( int id)
        {
            doctorRepository.DeleteReview(id);
            return Ok("Delete  Review Done");
        }

        [HttpPost("GetAllReviewForDoctor")]
        public ActionResult GetAllReview(int DoctorId,string name)
        {
            List<DTOOReview> dTOReviews = doctorRepository.GetAllReviews(DoctorId,name);
            return Ok(dTOReviews);
        }
    
    }
}
