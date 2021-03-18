using SchoolsSys.BL.Converters;
using SchoolsSys.BL.DTOs;
using SchoolsSys.BL.Models;
using SchoolsSys.BL.UnitOfWork;
using System.Collections.Generic;
using System.Web;
using System.Web.Http;

namespace SchoolSys.API.Controllers
{
    [RoutePrefix("api/Students")]
    public class StudentsController : ApiController
    {
        [HttpPost]
        [Route("CreateStudent")]
        public StudentDTO CreateStudent(StudentDTO student)
        {
            using (var UoW = new UnitOfWork(new SchoolsSysDBContext()))
            {
                var _students = UoW.Students;

                if (student == null || student.StudentId > 0)
                {
                    return new StudentDTO();
                }
                _students.Add(EntityConverters.PopulateNewStudentFromDTO(student));
                UoW.Complete();
            }
            return student;
        }

        [HttpPost]
        [Route("Upload")]
        public string Upload()
        {
            using (var UoW = new UnitOfWork(new SchoolsSysDBContext()))
            {
                var _students = UoW.Students;
                var result = _students.UploadProfileImage(HttpContext.Current.Request);
                UoW.Complete();
                switch (result)
                {
                    case "Failed":
                        return "";
                    default:
                        return result;
                }
            }
        }

        [HttpPost]
        [Route("UploadFiles")]
        public List<string> UploadFiles()
        {
            using (var UoW = new UnitOfWork(new SchoolsSysDBContext()))
            {
                var _students = UoW.Students;
                var result = _students.UploadAttachments(HttpContext.Current.Request);

                return result;
            }
        }
    }
}
