using SchoolsSys.BL.Converters;
using SchoolsSys.BL.DTOs;
using SchoolsSys.BL.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace SchoolsSys.BL.Repository
{
    public class AttachmentsRepository : RepositoryBase<Attachment>, IAttachmentsRepository
    {
        public SchoolsSysDBContext SchoolsSysDBContext
        {
            get { return Context as SchoolsSysDBContext; }
        }

        public AttachmentsRepository(SchoolsSysDBContext ctx) : base(ctx)
        {
        }

        public string UploadAttachment(HttpRequest Request)
        {
            try
            {
                var file = Request.Files[0];
                var folderName = ConfigurationManager.AppSettings["AttachmentsUploads"];
                var pathToSave = Path.Combine(HttpContext.Current.Server.MapPath("~/"), folderName);

                if (!Directory.Exists(pathToSave))
                    Directory.CreateDirectory(pathToSave);

                if (file.ContentLength > 0)
                {
                    var fileName = file.FileName.Trim('"');
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
    }
}
