using GraduationProject.DTO.AnalysisCentersDto;
using GraduationProject.DTO.DTOForWorkspace;
using GraduationProject.DTO.DTOReview;
using GraduationProject.DTO.SuperMarket;
using GraduationProject.Models;
using GraduationProject.Services.AnalysisCentersServices;
using GraduationProject.Services.SuperMarketServices;
using GraduationProject.Services.WorkSpaceServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GraduationProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AnalysisCentersController : ControllerBase
    {
        private readonly IAnalysisCentersServices analysisCentersServices;
        private readonly UserManager<User> usermanger;
        public AnalysisCentersController(IAnalysisCentersServices _analysisCentersServices, UserManager<User> usermanger)
        {
            analysisCentersServices = _analysisCentersServices;
            this.usermanger = usermanger;
        }
        //Create,,delete,,update ....................................................................................
        [Authorize(Roles = "Admin")]
        [HttpPost("CreateAnalysisCenter")]
        public ActionResult CreateAnalysisCenter([FromForm] AddAnalysisCentersDto Service, List<IFormFile> files)//,  IFormFile file)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            analysisCentersServices.Create(Service, files);
            return Ok("Add Done");
        }

        [Authorize(Roles = "Admin")]
        [HttpPut("UpdateAnalysisCenter")]
        public ActionResult UpdateAnalysisCenter(int id, [FromForm] AddAnalysisCentersDto Service)//, IFormFile file)
        {
            analysisCentersServices.Update(id, Service);
            return Ok("Update AnalysisCenter Done");
        }

        [Authorize(Roles = "Admin")]
        [HttpDelete("DeleteAnalysisCenter")]
        public ActionResult DeleteAnalysisCenter(int id)
        {
            analysisCentersServices.Delete(id);
            return Ok("Delete AnalysisCenter Done");
        }
        //...............................................................................
        [HttpGet("GetAllAnalysisCenters")]
        public IActionResult GetAllAnalysisCenters()
        {
            var analysisCenters = analysisCentersServices.GetAllAnalysisCenters();
            if (!analysisCenters.Any())
                return BadRequest("There is No Data");
            return Ok(analysisCenters);
        }


        [HttpGet("GetAnalysisCenterById")]
        public IActionResult GetAnalysisCenterById(int id)
        {
            var analysisCenter = analysisCentersServices.GetAnalysisCenterByID(id);
            if (analysisCenter == null)
                return BadRequest("There is No Data");
            return Ok(analysisCenter);
        }



        [HttpPost("Search")]
        //  [Authorize(Roles = UserRoles.Admin )]
        public ActionResult Search([FromForm] string name)
        {
            var analysisCenter = analysisCentersServices.Search(name);
            if (analysisCenter == null)
                return BadRequest("There is No Data");
            return Ok(analysisCenter);
        }


        [HttpGet("GetAllAnalysisCentersActiveNow")]
        public IActionResult GetAllAnalysisCentersActiveNow()
        {
            var analysisCenters = analysisCentersServices.GetAllAnalysisCentersActiveNow();
            if (!analysisCenters.Any())
                return BadRequest("There is No Data");
            return Ok(analysisCenters);
        }

        [HttpGet("GetAllAnalysisCentersBySortReview")]
        public IActionResult GetAllAnalysisCentersBySortReview()
        {
            var analysisCenters = analysisCentersServices.GetAllAnalysisCentersByReview();
            if (!analysisCenters.Any())
                return BadRequest("There is No Data");
            return Ok(analysisCenters);
        }
        
        [HttpPost("CreateReviewForAnalysisCenter")]
        public async Task<IActionResult> CreateReview(int AnalysisCentersID, [FromForm] DTOReview dTOReview)//,  IFormFile file)
        {
            User user = await usermanger.GetUserAsync(User);
            var id = analysisCentersServices.CreateReview(user.Id, AnalysisCentersID, dTOReview);
            return Ok("Add Done");
        }


        [HttpPut("UpdateReview")]
        public ActionResult UpdateReview(int AnalysisCentersID, [FromForm] DTOReview dTOReview)//, IFormFile file)
        {
            analysisCentersServices.UpdateReview(AnalysisCentersID, dTOReview);//,file);
            return Ok("Update Review Done");
        }


        [HttpDelete("Delete   Review")]
        public ActionResult DeleteReview(int id)
        {
            analysisCentersServices.DeleteReview(id);
            return Ok("Delete  Review Done");
        }

        [HttpPost("GetAllReviewForAnalysisCenter")]
        public ActionResult GetAllReview(int AnalysisCentersID, [FromForm] string name)
        {
            List<DTOOReview> dTOReviews = analysisCentersServices.GetAllReviews(AnalysisCentersID, name);
            return Ok(dTOReviews);
        }
    }
}
