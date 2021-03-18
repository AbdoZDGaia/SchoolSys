using SchoolsSys.BL.Converters;
using SchoolsSys.BL.DTOs;
using SchoolsSys.BL.LookupsAggregate;
using SchoolsSys.BL.Models;
using SchoolsSys.BL.UnitOfWork;
using System.Collections.Generic;
using System.Web.Http;

namespace SchoolsSys.API.Controllers
{
    [RoutePrefix("api/Queries")]
    public class QueriesController : ApiController
    {
        [HttpGet]
        [Route("GetAllStudents")]
        public List<StudentDTO> GetAllStudents()
        {
            using (var UoW = new UnitOfWork(new SchoolsSysDBContext()))
            {
                var _students = UoW.Students;
                var result = _students.GetAllStudents();
                return result;
            }
        }

        [HttpGet]
        [Route("GetStudentsById")]
        public StudentDTO GetStudentsById(int StudentId)
        {
            using (var UoW = new UnitOfWork(new SchoolsSysDBContext()))
            {
                var _students = UoW.Students;
                var result = _students.GetFirstOrDefault(s=>s.StudentId == StudentId).ConvertToStudentDTO();
                return result;
            }
        }

        [HttpGet]
        [Route("GetAllGrades")]
        public List<LookupsDTO> GetAllGrades()
        {
            var _ctx = new SchoolsSysDBContext();
            var aggr = new LookupAggregate(_ctx);
            var result = aggr.GetAllGrades();
            return result;
        }

        [HttpGet]
        [Route("GetAllClasses")]
        public List<LookupsDTO> GetAllClasses()
        {
            var _ctx = new SchoolsSysDBContext();
            var aggr = new LookupAggregate(_ctx);
            var result = aggr.GetAllClasses();
            return result;
        }

        [HttpGet]
        [Route("GetClassesByGradeId")]
        public List<LookupsDTO> GetClassesByGradeId(int gradeId)
        {
            var _ctx = new SchoolsSysDBContext();
            var aggr = new LookupAggregate(_ctx);
            var result = aggr.GetAllClassesByGradeId(gradeId);
            return result;
        }
    }
}
