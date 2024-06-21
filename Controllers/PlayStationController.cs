using GraduationProject.DTO.DTOForRestaurants;
using GraduationProject.DTO.DTOForWorkspace;
using GraduationProject.DTO.DTOPlayStation;
using GraduationProject.DTO.DTOReview;
using GraduationProject.Models;
using GraduationProject.Services.DoctorsServices;
using GraduationProject.Services.PlayStationServices;
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
    [Authorize]
    public class PlayStationController : ControllerBase
    {
        private readonly IPlayStationRepository  playStationRepository;
        private readonly UserManager<User> usermanger;
        public PlayStationController(IPlayStationRepository _playStationRepository, UserManager<User> usermanger)
        {
            playStationRepository = _playStationRepository;
            this.usermanger = usermanger;
        }

        //Create,,delete,,update ....................................................................................
        [Authorize(Roles = "Admin")]
        [HttpPost("CreatePlayStation")]
        public ActionResult CreatePlayStation([FromForm] AddPlaystationDto Service, List<IFormFile> files)//,  IFormFile file)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            playStationRepository.Create(Service, files);
            return Ok("Add Done");
        }

        [Authorize(Roles = "Admin")]
        [HttpPut("UpdatePlayStation")]
        public ActionResult UpdatePlayStation(int id, [FromForm] AddPlaystationDto Service)//, IFormFile file)
        {
            playStationRepository.Update(id, Service);
            return Ok("Update PlayStation Done");
        }

        [Authorize(Roles = "Admin")]
        [HttpDelete("DeletePlayStation")]
        public ActionResult DeletePlayStation(int id)
        {
            playStationRepository.Delete(id);

            return Ok("Delete PlayStation  Done");
        }

        [Authorize(Roles = "Admin")]
        [HttpPost("CreateGameForPlaystation")]
        public ActionResult CreateGame([FromForm] AddGamesDto game )
        {
            var id = playStationRepository.CreateGameForPlaystation(game);
            return Ok("Add Done");
        }

        [Authorize(Roles = "Admin")]
        [HttpPut("UpdateGameForPlaystation")]
        public ActionResult UpdateGame(int id, [FromForm] AddGamesDto game ) 
        {
            playStationRepository.UpdateGame(id, game);
            return Ok("Update Game Done");
        }
        [Authorize(Roles = "Admin")]
        [HttpDelete("DeleteGameForPlaystation")]
        public ActionResult DeleteGame(int id)
        {
            playStationRepository.DeleteGame(id);

            return Ok("Delete Game  Done");
        }

        //...........................................................................
        [HttpGet("GetAllPlayStations")]
        public IActionResult GetAllPlayStations()
        {
            List<DTOPlayStation> dTOPlaaystations = playStationRepository.GetAllPlayStations();
            if (!dTOPlaaystations.Any())
                return BadRequest("There is No Data");
            return Ok(dTOPlaaystations);
        }
        [HttpGet("GetAllPlayStationsBySortReview")]
        public IActionResult GetAllPlayStationsBySortReview()
        {
            List<PlayStation> dTOPlaaystations = playStationRepository.GetAllPlayStationsBySortReview();
            if (!dTOPlaaystations.Any())
                return BadRequest("There is No Data");
            return Ok(dTOPlaaystations);
        }

        [HttpGet("GetPlayStationById")]
        public IActionResult GetPlayStationById(int id)
        {
            DTOPlayStation dTOPlaaystation = playStationRepository.GetPlayStationByID(id);
            if (dTOPlaaystation == null)
                return BadRequest("There is No Data");
            return Ok(dTOPlaaystation);
        }

        [HttpPost("Search")]
        //  [Authorize(Roles = UserRoles.Admin )]
        public ActionResult Search([FromForm] string name)
        {

            var dTOPlaaystation = playStationRepository.Search(name);
            if (dTOPlaaystation == null)
                return BadRequest("There is No Data");
            return Ok(dTOPlaaystation);
        }


        [HttpGet("GetAllPlayStationsActiveNow")]
        public IActionResult GetAllWorkspacesActiveNow()
        {
            List<DTOPlayStation> dTOPlaaystations = playStationRepository.GetAllPlayStationsActiveNow();
            if (dTOPlaaystations == null)
                return BadRequest("There is No Data");
            return Ok(dTOPlaaystations);
        }

        [HttpGet("GetAllGamesForPlayStation")]
        public ActionResult AllGamesOFPlayStation(int PlaystationId)  //[FromRoute] int restaurantId)
        {
           List<DTOGames> games = playStationRepository.GetAllGamesByPlaystationId(PlaystationId);
            if (games == null)
                return BadRequest("There is No Data");
            return Ok(games);
        }

        //Review............................................................................

        [HttpPost("CreateReviewForPlayStation")]
        public async Task<IActionResult> CreateReview(string U, int PlayStationId, [FromForm] DTOReview dTOReview)//,  IFormFile file)
        {
            User user = await usermanger.GetUserAsync(User);
            var id = playStationRepository.CreateReview(user.Id, PlayStationId, dTOReview);
            return Ok("Add Done");
        }


        [HttpPut("UpdateReview")]
        public ActionResult UpdateReview(int PlayStationId, [FromForm] DTOReview dTOReview)//, IFormFile file)
        {
            playStationRepository.UpdateReview(PlayStationId, dTOReview);//,file);
            return Ok("Update Review Done");
        }


        [HttpDelete("DeleteReview")]
        public ActionResult DeleteReview(   int id)
        {
            playStationRepository.DeleteReview(id);
            return Ok("Delete  Review Done");
        }

        [HttpPost("GetAllReviewForPlayStation")]
        public ActionResult GetAllReview(int PlayStationId, [FromForm] string name)
        {
            List<DTOOReview> dTOReviews = playStationRepository.GetAllReviews(PlayStationId, name);
            return Ok(dTOReviews);
        }
        //Review............................................................................
 
    }
}
