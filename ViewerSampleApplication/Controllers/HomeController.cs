using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using GroupDocs.Viewer;
using GroupDocs.Viewer.Options;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ViewerSampleApplication.Models;

namespace ViewerSampleApplication.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            License lic = new License();
            lic.SetLicense(@"D:/GroupDocs.Total.NET.lic");

            string outputDirectory = ("Output/");
            string outputFilePath = Path.Combine(outputDirectory, "output.pdf");

            using (Viewer viewer = new Viewer("SourceDocuments/sample.docx"))
            {
                PdfViewOptions options = new PdfViewOptions(outputFilePath);
                viewer.View(options);
            }
            var fileStream = new FileStream("Output/" + "output.pdf",
                                    FileMode.Open,
                                    FileAccess.Read
                                  );
            var fsResult = new FileStreamResult(fileStream, "application/pdf");
            ViewBag.filePath = "Output/output.pdf";
            return View();
        }
        public ActionResult RenderFile()
        {
            var fileStream = new FileStream("Output/" + "output.pdf",
                                    FileMode.Open,
                                    FileAccess.Read
                                  );  
            return File(fileStream, "application/pdf");
        }
        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
