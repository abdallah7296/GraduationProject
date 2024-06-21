using GraduationProject.DTO.DTOForDoctors;
using GraduationProject.DTO.DTOForWorkspace;
using GraduationProject.DTO.DTOReview;
using GraduationProject.Models;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;

namespace GraduationProject.Services.WorkSpaceServices
{
    public interface IWorkspaceRepository
    {
       List<DTOOWorkspace> GetAllWorkspacs();
       List<DTOOWorkspace> Search(string name);
       DTOOWorkspace GetWorkSpaceByID(int id);
       List<DTOOWorkspace> GetAllWorkspacesActiveNow();
        List<Workspace> GetAllWorkspacesByReview();
        int Create(AddWorkspaceDto dto, List<IFormFile> file);
        void Delete(int id);
        void Update(int id, AddWorkspaceDto dto );
        int CreateReview(string UserId, int WorkspaceId, DTOReview dTOReview);
        void DeleteReview(int id);
        void UpdateReview(int id, DTOReview dto);
        List<DTOOReview> GetAllReviews(int ServiceId, string name);
    }
}
