using GraduationProject.DTO.DTOForWorkspace;
using GraduationProject.DTO.DTOPlayStation;
using GraduationProject.DTO.DTOReview;
using GraduationProject.DTO.SuperMarket;
using GraduationProject.Models;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;

namespace GraduationProject.Services.SuperMarketServices
{
    public interface ISuperMarketServices
    {
        int Create(AddSupermarketDto dto, List<IFormFile> file);
        void Delete(int id);
        void Update(int id, AddSupermarketDto dto );
        List<SuperMarketDto> GetAllSuperMarket();
        List<SuperMarketDto> Search(string name);
        SuperMarketDto GetSuperMarketByID(int id);
        List<SuperMarketDto> GetAllSuperMarketDtoActiveNow();
        List<SuperMarketDto> GetAllSuperMarketByReview();
        int CreateReview(string UserId, int SuperMarketId, DTOReview dTOReview);
        void DeleteReview(int id);
        void UpdateReview(int id, DTOReview dto);
        List<DTOOReview> GetAllReviews(int ServiceId, string name);
    }
}
