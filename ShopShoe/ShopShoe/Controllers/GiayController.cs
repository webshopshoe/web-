using ShopShoe.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ShopShoe.Controllers
{
    public class GiayController : Controller
    {
        // GET: Giay
        DatabaseWebBanGiay db = new DatabaseWebBanGiay();
        public PartialViewResult SachMoiPartial()
        {
            var lstSachMoi = db.Giays.Take(3).ToList();
            return PartialView(lstSachMoi);
        }
        //Xem chi tiết
        public ViewResult XemChiTiet(int MaGiay = 0)
        {
            Giay giay = db.Giays.SingleOrDefault(n => n.MaGiay == MaGiay);
            if (giay == null)
            {
                //Trả về trang báo lỗi 
                Response.StatusCode = 404;
                return null;
            }
            //ChuDe cd = db.ChuDes.Single(n => n.MaChuDe == sach.MaChuDe);
            //ViewBag.TenCD = cd.TenChuDe;
            ViewBag.TenThuongHieu = db.ThuongHieux.Single(n => n.MaThuongHieu == giay.MaThuongHieu).TenThuongHieu;
            ViewBag.TenNSX = db.NhaSanXuats.Single(n => n.MaNSX == giay.MaNSX).TenNSX;
            return View(giay);
        }


    }
}
