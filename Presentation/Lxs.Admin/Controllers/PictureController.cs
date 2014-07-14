using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Lxs.Admin.Controllers
{
    public class PictureController : Controller
    {
        //
        // GET: /Picture/
        [HttpPost]
        public ActionResult AsyncUpload()
        {
            Stream stream = null;
            var fileName = "";
            var contentType = "";
            var path = @"/imgextra/";

            if (String.IsNullOrEmpty(Request["qqfile"]))
            {
                // IE
                HttpPostedFileBase httpPostedFile = Request.Files[0];
                if (httpPostedFile == null)
                    throw new ArgumentException("No file uploaded");
                stream = httpPostedFile.InputStream;
                fileName = Path.GetFileName(httpPostedFile.FileName);
                contentType = httpPostedFile.ContentType;

            }
            else
            {
                //Webkit, Mozilla
                stream = Request.InputStream;
                fileName = Request["qqfile"];
            }
            var fileExtension = Path.GetExtension(fileName);

            var file = path + Guid.NewGuid().ToString().Replace("-", "") + fileExtension;


            var buffer = new byte[stream.Length];
            stream.Read(buffer, 0, buffer.Length);
            System.IO.File.WriteAllBytes(Server.MapPath(file), buffer);

            return Json(new { success = true, imageUrl = file });
        }

    }
}
