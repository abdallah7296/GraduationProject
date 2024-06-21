using GraduationProject.DTO.DTOForDoctors;
using GraduationProject.DTO.DTOForWorkspace;
using GraduationProject.DTO.DTOReview;
using GraduationProject.Models;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;

namespace GraduationProject.Services.DoctorsServices
{
    public interface IDoctorService
    {
        List<DTODoctor> GetAllDectors();
        List<Doctor> GetAllDectorsBySortReview();
        DTODoctor GetDoctorById(int id);
        int Add(AddDoctorDto doctor, List<IFormFile> formFiles);
        void Update(int id, AddDoctorDto doctor);
        void Delete(int id);
        List<DTODoctor> Search(string name);
        List<DTODoctor> searchByNameOfSpecialization(string name);
     
        List<DTODoctor> GetAllDoctorsActiveNow();
        int CreateReview(string UserId, int WorkspaceId, DTOReview dTOReview);
        void DeleteReview(int id);
        void UpdateReview(int id, DTOReview dto);
        List<DTOOReview> GetAllReviews(int ServiceId, string name);
    }
}
