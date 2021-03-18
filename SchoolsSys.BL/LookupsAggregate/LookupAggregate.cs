using SchoolsSys.BL.Converters;
using SchoolsSys.BL.DTOs;
using SchoolsSys.BL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SchoolsSys.BL.LookupsAggregate
{
    public class LookupAggregate
    {
        private readonly SchoolsSysDBContext _ctx;

        public LookupAggregate(SchoolsSysDBContext ctx)
        {
            _ctx = ctx;
        }

        public List<LookupsDTO> GetAllGrades()
        {
            try
            {
                return _ctx.Grades.ToList().Select(g => g.ConvertToGradeDTO()).ToList();
            }
            catch (Exception ex)
            {
                //elma to be implemented
                Elmah.ErrorLog.GetDefault(HttpContext.Current).Log(new Elmah.Error(ex));
                throw;
            }
        }

        public List<LookupsDTO> GetAllClasses ()
        {
            try
            {
                return _ctx.Classes.ToList().Select(g => g.ConvertToClassDTO()).ToList();
            }
            catch (Exception ex)
            {
                //elma to be implemented
                Elmah.ErrorLog.GetDefault(HttpContext.Current).Log(new Elmah.Error(ex));
                throw;
            }
        }

        public List<LookupsDTO> GetAllClassesByGradeId(int gradeId)
        {
            try
            {
                return _ctx.Classes.Where(c=>c.GradeId == gradeId).ToList().Select(g => g.ConvertToClassDTO()).ToList();
            }
            catch (Exception ex)
            {
                //elma to be implemented
                Elmah.ErrorLog.GetDefault(HttpContext.Current).Log(new Elmah.Error(ex));
                throw;
            }
        }
    }
}
