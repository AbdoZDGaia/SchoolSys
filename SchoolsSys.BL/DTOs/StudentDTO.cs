using System;
using System.Collections.Generic;

namespace SchoolsSys.BL.DTOs
{
    public class StudentDTO
    {
        public int StudentId { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public int GradeId { get; set; }
        public int ClassId { get; set; }
        public DateTime BirthDate { get; set; }
        public int GenderTypeId { get; set; }
        public decimal PickupLongitude { get; set; }
        public decimal PickupLatitude { get; set; }
        public decimal DropOffLongitude { get; set; }
        public decimal DropOffLatitude { get; set; }
        public bool SubscribedToBus { get; set; }
        public string MobileNumber { get; set; }
        public string Email { get; set; }
        public string ProfilePicturePath { get; set; }

        public List<string>  Attachments { get; set; }
    }
}
