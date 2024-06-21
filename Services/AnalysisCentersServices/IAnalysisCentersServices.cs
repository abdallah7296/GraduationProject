using GraduationProject.DTO.AnalysisCentersDto;
using GraduationProject.DTO.DTOForWorkspace;
using GraduationProject.DTO.DTOReview;
using GraduationProject.DTO.SuperMarket;
using GraduationProject.Models;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;

namespace GraduationProject.Services.AnalysisCentersServices
{
    public interface IAnalysisCentersServices
    {
        int Create(AddAnalysisCentersDto dto, List<IFormFile> file);
        void Delete(int id);
        void Update(int id, AddAnalysisCentersDto dto);
        List<AnalysisCentersDto> GetAllAnalysisCenters();
        List<AnalysisCentersDto> Search(string name);
        AnalysisCentersDto GetAnalysisCenterByID(int id);
        List<AnalysisCentersDto> GetAllAnalysisCentersActiveNow();
        List<AnalysisCentersDto> GetAllAnalysisCentersByReview();
        int CreateReview(string UserId, int AnalysisCentersID, DTOReview dTOReview);
        void DeleteReview(int id);
        void UpdateReview(int id, DTOReview dto);
        List<DTOOReview> GetAllReviews(int ServiceId, string name);
    }
}
