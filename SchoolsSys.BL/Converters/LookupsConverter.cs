using SchoolsSys.BL.DTOs;
using SchoolsSys.BL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolsSys.BL.Converters
{
    public static class LookupsConverter
    {
        public static LookupsDTO ConvertToGradeDTO (this Grade input)
        {
            var result = new LookupsDTO();
            if (input != null)
            {
                result.Id = input.GradeId;
                result.NameAr = input.GradeName;
                result.NameEn = input.GradeNameEn;
            }

            return result;
        }

        public static LookupsDTO ConvertToClassDTO(this Class input)
        {
            var result = new LookupsDTO();
            if (input != null)
            {
                result.Id = input.ClassId;
                result.NameAr = input.ClassName;
                result.NameEn = input.ClassNameEn;
            }

            return result;
        }
    }
}
