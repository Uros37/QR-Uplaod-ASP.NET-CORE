using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using QRCoder;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing.Imaging;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using QRGEN.Models;
using QRGEN.Models.Database;
using ZXing.QrCode.Internal;
using iTextSharp.text.pdf.qrcode;
using Microsoft.AspNetCore.JsonPatch.Internal;
using NPOI.OpenXmlFormats.Dml;

namespace QRGEN.Controllers
{

    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger; 
        private readonly QRGENDbContext _context;


        public HomeController(ILogger<HomeController> logger, QRGENDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        public static Bitmap ByteArrayToImage(byte[] source)
        {
            using (var ms = new MemoryStream(source))
            {
                return new Bitmap(ms);
            }
        }

        [HttpPost]
        public IActionResult Login(string email)
        {
            var user_email = Request.Form["email"].ToString();
            if (user_email != "")
            {
                User currentUser = _context.Users.FirstOrDefault(ff => ff.Email == user_email);
                if(currentUser != null)
                {
                    QRCodeGenerator qRCodeGenerator = new QRCodeGenerator();
                    QRCodeData qRCodeData = qRCodeGenerator.CreateQrCode(currentUser.Email, QRCodeGenerator.ECCLevel.Q);
                    BitmapByteQRCode qRCode = new BitmapByteQRCode(qRCodeData);
                    return Json(new
                    {
                        status = true,
                        qrcodeData = Convert.ToBase64String(qRCode.GetGraphic(20))
                    });

                }
                else
                {
                    User newUser = new User{
                        Email = user_email,
                    };
                    _context.Users.Add(newUser);
                    var saveState = _context.SaveChanges();
                    if (saveState > 0)
                    {
                        QRCodeGenerator qRCodeGenerator = new QRCodeGenerator();
                        QRCodeData qRCodeData = qRCodeGenerator.CreateQrCode(currentUser.Email, QRCodeGenerator.ECCLevel.Q);
                        BitmapByteQRCode qRCode = new BitmapByteQRCode(qRCodeData);
                        return Json(new
                        {
                            status = true,
                            qrcodeData = Convert.ToBase64String(qRCode.GetGraphic(20))
                        });
                    }
                    else
                    {
                        return Json(new
                        {
                            status = false,
                            message = "User register is failed."
                        });
                    }
                }
            }
            return Json(new
            {
                status = false,
                message = "Bad request."
            });
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
