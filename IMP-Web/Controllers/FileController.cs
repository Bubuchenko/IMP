using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace IMP_Web.Controllers
{
    public class FileController : Controller
    {
        // GET: File
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public bool Upload(HttpPostedFileBase file)
        {
            if(file != null && file.ContentLength > 0)
            {
                var fileName = Path.GetFileName(file.FileName);
                string path = Path.Combine(ConfigurationManager.AppSettings["DownloadFileDirectory"], fileName);
                file.SaveAs(path);
                return true;
            }
            return false;
        }
    }
}