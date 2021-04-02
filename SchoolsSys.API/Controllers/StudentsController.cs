using SchoolsSys.BL.Converters;
using SchoolsSys.BL.DTOs;
using SchoolsSys.BL.UnitOfWork;
using System.Collections.Generic;
using System.Web;
using System.Web.Http;

namespace SchoolSys.API.Controllers
{
    [RoutePrefix("api/Students")]
    public class StudentsController : ApiController
    {
        private readonly IUnitOfWork _unitOfWork;

        public StudentsController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }


        [HttpPost]
        [Route("CreateStudent")]
        public StudentDTO CreateStudent(StudentDTO student)
        {
            if (student == null || student.StudentId > 0)
            {
                return new StudentDTO();
            }
            _unitOfWork.StudentsRepo.Add(EntityConverters.PopulateNewStudentFromDTO(student));
            _unitOfWork.Commit();
            return student;
        }

        [HttpPost]
        [Route("Upload")]
        public string Upload()
        {
            var result = _unitOfWork.StudentsRepo.UploadProfileImage(HttpContext.Current.Request);
            _unitOfWork.Commit();

            switch (result)
            {
                case "Failed":
                    return "";
                default:
                    return result;
            }
        }

        [HttpPost]
        [Route("UploadFiles")]
        public List<string> UploadFiles()
        {
            var result = _unitOfWork.StudentsRepo.UploadAttachments(HttpContext.Current.Request);
            _unitOfWork.Commit();

            return result;
        }
    }
}
