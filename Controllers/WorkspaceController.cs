using GraduationProject.DTO.DTOForWorkspace;
using GraduationProject.DTO.DTOReview;
using GraduationProject.Models;
using GraduationProject.Services.DoctorsServices;
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
    public class WorkspaceController : ControllerBase
    {
        private readonly IWorkspaceRepository workspaceRepository;
        private readonly UserManager<User> usermanger;
        public WorkspaceController(IWorkspaceRepository _workspaceRepository, UserManager<User> usermanger)
        {
            workspaceRepository = _workspaceRepository;
            this.usermanger = usermanger;
        }
        //Create,,delete,,update for workspace.....................................................................................
        // [Authorize(Roles = "Admin")]
        [HttpPost("CreateWorkspace")]
        [Authorize(Roles = "Admin")]
        public ActionResult CreateWorkspace([FromForm] AddWorkspaceDto workspace, List<IFormFile> files)//,  IFormFile file)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            workspaceRepository.Create(workspace, files);
            return Ok("Add Done");
        }

        [Authorize(Roles = "Admin")]
        [HttpPut("UpdateWorkspace")]
        public ActionResult Update(int id, [FromForm] AddWorkspaceDto workspace)//, IFormFile file)
        {
            workspaceRepository.Update(id, workspace);//,file);
            return Ok("Update Workspace Done");
        }

        [Authorize(Roles = "Admin")]
        [HttpDelete("DeleteWorkspace")]
        public ActionResult Delete(int id)
        {
            workspaceRepository.Delete(id);

            return Ok("Delete Workspace Done");
        }
        //......................................................................................................................
        [HttpGet("GetAllWorkspaces")]
        public IActionResult GetAllWorkspaces()
        {
            List<DTOOWorkspace> dTOWorkspaces = workspaceRepository.GetAllWorkspacs();
            if (dTOWorkspaces == null)
                return BadRequest("There is No Data");
            return Ok(dTOWorkspaces);
        }


        [HttpGet("GetWorkspaceById")]
        public IActionResult GetWorkspaceById(int id)
        {
            var Workspace = workspaceRepository.GetWorkSpaceByID(id);
            if (Workspace == null)
                return BadRequest("There is No Data");
            return Ok(Workspace);
        }



        [HttpPost("Search")]
        //  [Authorize(Roles = UserRoles.Admin )]
        public ActionResult Search([FromForm] string name)
        {
            var dTOWorkspace = workspaceRepository.Search(name);
            if (dTOWorkspace== null)
                return BadRequest("There is No Data");
            return Ok(dTOWorkspace);
        }

 
        [HttpGet("GetAllWorkspacesActiveNow")]
        public IActionResult GetAllWorkspacesActiveNow()
        {
            List<DTOOWorkspace> dTOWorkspaces = workspaceRepository.GetAllWorkspacesActiveNow();
            if (dTOWorkspaces == null)
                return BadRequest("There is No Data");
            return Ok(dTOWorkspaces);
        }

        [HttpGet("GetAllWorkspacesbySortReview")]
        public IActionResult GetAllWorkspacesbySortReview()
        {
            List<Workspace> dTOWorkspaces = workspaceRepository.GetAllWorkspacesByReview();
            if (dTOWorkspaces == null)
                return BadRequest("There is No Data");
            return Ok(dTOWorkspaces);
        }
        
     
        //Review............................................................................
      
        [HttpPost("CreateReviewForWorkSpace")]
        public async Task<IActionResult> CreateReview(int WorkspaceId, [FromForm] DTOReview dTOReview)//,  IFormFile file)
        {
            User user = await usermanger.GetUserAsync(User);
            var id = workspaceRepository.CreateReview(user.Id,WorkspaceId,dTOReview);
            return Ok("Add Done");
        }

       
        [HttpPut("UpdateReview")]
        public ActionResult UpdateReview(int WorkspaceId, [FromForm] DTOReview dTOReview)//, IFormFile file)
        {
            workspaceRepository.UpdateReview( WorkspaceId,dTOReview);//,file);
            return Ok("Update Review Done");
        }

       
        [HttpDelete("Delete   Review")]
        public ActionResult DeleteReview(  int id)
        {
            workspaceRepository.DeleteReview(id);
            return Ok("Delete  Review Done");
        }

        [HttpPost("GetAllReviewForWorkspace")]
        public ActionResult GetAllReview(int WorkspaceId, [FromForm] string name)
        {
            List<DTOOReview> dTOReviews =  workspaceRepository.GetAllReviews(WorkspaceId, name);
            return Ok(dTOReviews);
        }
 
    }
}
