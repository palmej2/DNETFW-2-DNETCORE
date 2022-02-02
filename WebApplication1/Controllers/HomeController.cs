using Microsoft.Owin.Security.DataHandler.Encoder;
using System.Text;
using System.Web.Mvc;
using System.Web.Security;

namespace WebApplication1.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            var unencryptedText = "TEST VALUE";

            ViewBag.encryptedText = Encrypt(unencryptedText);

            return View();
        }

        public string Encrypt(string text)
        {
            var secureUtf8 = new UTF8Encoding(encoderShouldEmitUTF8Identifier: false, throwOnInvalidBytes: true);
            var encodedText = secureUtf8.GetBytes(text);
            var protectedText = MachineKey.Protect(encodedText);
            return TextEncodings.Base64Url.Encode(protectedText);
        }

        public string Decrypt(string text)
        {
            var encodedText = TextEncodings.Base64Url.Decode(text);
            var bytes = MachineKey.Unprotect(encodedText);
            var secureUtf8 = new UTF8Encoding(encoderShouldEmitUTF8Identifier: false, throwOnInvalidBytes: true);
            return secureUtf8.GetString(bytes);
        }
    }
}