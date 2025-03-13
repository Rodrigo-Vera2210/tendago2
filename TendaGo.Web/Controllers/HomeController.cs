using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;



namespace TendaGo.Web.Controllers
{
    public class HomeController : Controller
    {
        [Authorize]
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult UploadImage()
        {
            var image = Request.Files[0];
            var barcodeReader = new ZXing.BarcodeReader();
            var bmp = new System.Drawing.Bitmap(image.InputStream);
            //var source = new BitmapLuminanceSource(bmp);
            var result = barcodeReader.Decode(bmp);
            if (result != null)
            {
                return Json(new { text = result.Text }, JsonRequestBehavior.AllowGet);
            }
            return Json(new { text = "Error en el BarCode" }, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public object Local(LocalEnum id = LocalEnum.Id)
        {
            switch (id)
            {
                case LocalEnum.Id:
                    return Session.GetLocal()?.Id;
                case LocalEnum.Local:
                    return Session.GetLocal()?.Local;
                case LocalEnum.Tipo:
                    return Session.GetLocal()?.Tipo;
                case LocalEnum.Ruc:
                    return Session.GetLocal()?.DefaultRUC;
            }

            return Session.GetLocal(); 
        }

        [HttpPost]
        public JsonResult SetLocalSeleccionado(int idLocal, string local)
        {
            Session.SetLocal(idLocal, local);
            return Json(new { success = true, mensaje = $"Ha seleccionado el local {idLocal}-{local}." }, JsonRequestBehavior.AllowGet);
        }

    }

    public enum LocalEnum
    {
        Id,
        Local,
        Tipo,
        Ruc
    }
}