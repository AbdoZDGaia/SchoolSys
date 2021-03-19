using SchoolsSys.BL.Converters;
using SchoolsSys.BL.DTOs;
using SchoolsSys.BL.Models;
using SchoolsSys.BL.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Web;

namespace SchoolsSys.BL.Repository
{
    public class StudentsRepository : RepositoryBase<Student>, IStudentsRepository
    {
        public StudentsRepository(DbContext context ) : base(context)
        {
        }

        public string UploadProfileImage(HttpRequest Request)
        {
            try
            {
                var file = Request.Files[0];
                var folderName = ConfigurationManager.AppSettings["Temp"];
                var pathToSave = Path.Combine(HttpContext.Current.Server.MapPath("~/"), folderName);

                if (!Directory.Exists(pathToSave))
                    Directory.CreateDirectory(pathToSave);

                if (file.ContentLength > 0)
                {
                    var fileName = Guid.NewGuid() + file.FileName.Trim('"');
                    var fullPath = Path.Combine(pathToSave, fileName);
                    var dbPath = Path.Combine(folderName, fileName);
                    file.SaveAs(fullPath);

                    return dbPath;
                }
                else
                {
                    return "Failed";
                }
            }
            catch (Exception ex)
            {
                //elma to be implemented
                Elmah.ErrorLog.GetDefault(HttpContext.Current).Log(new Elmah.Error(ex));
                throw;
            }
        }

        public List<string> UploadAttachments(HttpRequest Request)
        {
            try
            {
                var files = Request.Files;
                List<string> filesAdded = new List<string>();
                var folderName = ConfigurationManager.AppSettings["Temp"];
                var pathToSave = Path.Combine(HttpContext.Current.Server.MapPath("~/"), folderName);
                var httpRequest = HttpContext.Current.Request;

                if (!Directory.Exists(pathToSave))
                    Directory.CreateDirectory(pathToSave);

                foreach (string file in files)
                {
                    var postedFile = httpRequest.Files[file];
                    if (postedFile.ContentLength > 0)
                    {
                        var fileName = Guid.NewGuid() + postedFile.FileName.Trim('"');
                        var fullPath = Path.Combine(pathToSave, fileName);
                        var dbPath = Path.Combine(folderName, fileName);
                        postedFile.SaveAs(fullPath);
                        filesAdded.Add(dbPath);
                    }
                }

                return filesAdded;
            }
            catch (Exception ex)
            {
                //elma to be implemented
                Elmah.ErrorLog.GetDefault(HttpContext.Current).Log(new Elmah.Error(ex));
                throw;
            }
        }

        public List<StudentDTO> GetAllStudents()
        {
            try
            {
                var result = Get(includeProperties: "Attachments,Class,Grade").Select(g => g.ConvertToStudentDTO()).ToList();
                return result;
            }
            catch (Exception ex)
            {
                //elma to be implemented
                Elmah.ErrorLog.GetDefault(HttpContext.Current).Log(new Elmah.Error(ex));
                throw;
            }
        }

        public StudentDTO GetStudentsById(int studentId)
        {
            try
            {
                var result = GetByID(studentId).ConvertToStudentDTO();
                return result;
            }
            catch (Exception ex)
            {
                //elma to be implemented
                Elmah.ErrorLog.GetDefault(HttpContext.Current).Log(new Elmah.Error(ex));
                throw;
            }
        }

        public List<StudentDTO> GetAllStudentsPaging(int pageIndex, int pageSize)
        {
            try
            {
                var result = Get(includeProperties: "Attachments,Class,Grade",
                skip: (pageIndex - 1) * pageSize,
                take: pageSize).Select(s => s.ConvertToStudentDTO()).ToList();
                return result;
            }
            catch (Exception ex)
            {
                Elmah.ErrorLog.GetDefault(HttpContext.Current).Log(new Elmah.Error(ex));
                throw;
            }
        }
    }
}
