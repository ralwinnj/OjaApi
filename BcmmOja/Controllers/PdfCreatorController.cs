//using BcmmOja.Utility;
//using DinkToPdf;
//using DinkToPdf.Contracts;
//using Microsoft.AspNetCore.Mvc;
//using System.IO;

//namespace BcmmOja.Controllers
//{
//    [Route("api/[controller]")]
//    [ApiController]
//    public class PdfCreatorController : ControllerBase
//    {
//        private IConverter _converter;
//        public PdfCreatorController(IConverter converter)
//        {
//            _converter = converter;
//        }

//        [HttpGet]
//        public IActionResult CreatePDF()
//        {
//            var globalSettings = new GlobalSettings
//            {
//                ColorMode = ColorMode.Color,
//                Orientation = Orientation.Portrait,
//                PaperSize = PaperKind.A4,
//                Margins = new MarginSettings { Top = 10 },
//                DocumentTitle = "Applicant List",
//                Out = @"c:\Applicant_List.pdf"
//            };

//            var objectSettings = new ObjectSettings
//            {
//                PagesCount = true,
//                HtmlContent = TemplateGenerator.GetHTMLString(),
//                WebSettings = { DefaultEncoding = "utf-8", UserStyleSheet = Path.Combine(Directory.GetCurrentDirectory(), "assets", "styles.css") },
//                HeaderSettings = { FontName = "Arial", FontSize = 9, Right = "Page [page] of [toPage]", Line = true },
//                FooterSettings = { FontName = "Arial", FontSize = 9, Line = true, Center = "Applicant List" }
//            };

//            var pdf = new HtmlToPdfDocument()
//            {
//                GlobalSettings = globalSettings,
//                Objects = { objectSettings }
//            };
//            _converter.Convert(pdf);

//            return Ok("Successfully created PDF document.");
//        }
//    }
//}