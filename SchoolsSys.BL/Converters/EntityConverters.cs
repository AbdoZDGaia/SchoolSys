using SchoolsSys.BL.DTOs;
using SchoolsSys.BL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolsSys.BL.Converters
{
    public static class EntityConverters
    {
        public static StudentDTO ConvertToStudentDTO(this Student input)
        {
            StudentDTO result = null;
            if (input != null)
            {
                result = new StudentDTO()
                {
                    GradeId = input.GradeId,
                    LastName = input.LastName,
                    MiddleName = input.MiddleName,
                    MobileNumber = input.MobileNumber,
                    PickupLatitude = input.PickupLatitude.Value,
                    PickupLongitude = input.PickupLongitude.Value,
                    ProfilePicturePath = input.ProfilePicturePath,
                    StudentId = input.StudentId,
                    SubscribedToBus = input.SubscribedToBus.Value,
                    BirthDate = input.BirthDate,
                    ClassId = input.ClassId,
                    DropOffLatitude = input.DropOffLatitude.Value,
                    DropOffLongitude = input.DropOffLongitude.Value,
                    Email = input.Email,
                    FirstName = input.FirstName,
                    GenderTypeId = input.GenderTypeId,
                    
                };
            }
            return result;
        }

        public static Student PopulateNewStudentFromDTO(StudentDTO student)
        {
            var result = new Student();

            result.GradeId = student.GradeId;
            result.LastName = student.LastName;
            result.MiddleName = student.MiddleName;
            result.MobileNumber = student.MobileNumber;
            result.PickupLatitude = student.PickupLatitude;
            result.PickupLongitude = student.PickupLongitude;
            result.ProfilePicturePath = student.ProfilePicturePath;
            result.StudentId = student.StudentId;
            result.SubscribedToBus = student.SubscribedToBus;
            result.BirthDate = student.BirthDate;
            result.ClassId = student.ClassId;
            result.DropOffLatitude = student.DropOffLatitude;
            result.DropOffLongitude = student.DropOffLongitude;
            result.Email = student.Email;
            result.FirstName = student.FirstName;
            result.GenderTypeId = student.GenderTypeId;

            return result;
        }

        public static AttachmentDTO ConvertToAttachmentDTO(this Attachment input)
        {
            AttachmentDTO result = null;
            if (input != null)
            {
                result = new AttachmentDTO()
                {
                    AttachmentId = input.AttachmentId,
                    FileName = input.FileName,
                    Path = input.Path,
                    StudentId = input.StudentId
                };
            }
            return result;
        }

    }
}
