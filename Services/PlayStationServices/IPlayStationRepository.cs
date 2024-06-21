using GraduationProject.DTO.DTOForRestaurants;
using GraduationProject.DTO.DTOForWorkspace;
using GraduationProject.DTO.DTOPlayStation;
using GraduationProject.DTO.DTOReview;
using GraduationProject.Models;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;

namespace GraduationProject.Services.PlayStationServices
{
    public interface IPlayStationRepository
    {
        int Create(AddPlaystationDto dto, List<IFormFile> file);
        void Delete(int id);
        void Update(int id, AddPlaystationDto dto);

        int CreateGameForPlaystation(AddGamesDto dto);
        void DeleteGame(int id);
        void UpdateGame(int id, AddGamesDto dto);

        List<DTOPlayStation> GetAllPlayStations();
        List<PlayStation> GetAllPlayStationsBySortReview();
        List<DTOPlayStation> Search(string name);//USER
        DTOPlayStation GetPlayStationByID(int id);
        List<DTOPlayStation> GetAllPlayStationsActiveNow();//USER
        List<DTOGames> GetAllGamesByPlaystationId (int PlayStationId);
        int CreateReview(string UserId, int WorkspaceId, DTOReview dTOReview);
        void DeleteReview(int id);
        void UpdateReview(int id, DTOReview dto);
        List<DTOOReview> GetAllReviews(int ServiceId, string name);
    }
}
