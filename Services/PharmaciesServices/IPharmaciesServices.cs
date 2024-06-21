using GraduationProject.DTO.DTOForWorkspace;
using GraduationProject.DTO.DTOPharmacies;
using GraduationProject.DTO.DTOReview;
using GraduationProject.Models;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;

namespace GraduationProject.Services.PharmaciesServices
{
    public interface IPharmaciesServices
    {
        List<DTOOPharmacie> GetAllPlayPharmacies();
        List<Pharmacies> GetAllPlayPharmaciesBySortReview();


       List< DTOOPharmacie> Search(string name);//USER
        DTOOPharmacie GetPharmacieByID(int id);
        List<DTOOPharmacie> GetAllPharmaciesActiveNow();//USER
        int Create(AddPharmacieDto dto, List<IFormFile> file);
        void Delete(int id);
        void Update(int id,AddPharmacieDto dto);//, IFormFile file);
        int CreateReview(string UserId, int WorkspaceId, DTOReview dTOReview);
        void DeleteReview(int id);
        void UpdateReview(int id, DTOReview dto);
        List<DTOOReview> GetAllReviews(int ServiceId, string name);
    }
}
