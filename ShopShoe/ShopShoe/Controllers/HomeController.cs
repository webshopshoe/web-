using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ShopShoe.Models;
using PagedList;
using PagedList.Mvc;

namespace ShopShoe.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        DatabaseWebBanGiay db = new DatabaseWebBanGiay();
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult cart()
        {
            return View();
        }
        [HttpGet]
        public ActionResult checkout()
        {
            return View();
        }
        [HttpPost]
        public ActionResult checkout(FormCollection f)
        {
            string sEmail = f["txtEmail"].ToString();
            string sMatkhau = f["txtmatkhau"].ToString();
            KhachHang kh = db.KhachHangs.SingleOrDefault(n => n.MatKhau == sMatkhau && n.Email == sEmail);
            if(kh!=null)
            {
                ViewBag.Thongbao = "chúc mừng bạn đăng nhập thành công";
                Session["TaiKhoan"] = kh;
                return View();
            }
            ViewBag.Thongbao = "Tên tài khoản hoặc mật khẩu không đúng";
            return View();
        }
        public ActionResult checkout2()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult checkout2(KhachHang _kh)
        {
            var check = db.KhachHangs.FirstOrDefault(s => s.Email == _kh.Email);
            if(ModelState.IsValid)
            {
               if(check==null)
                {
                    db.Configuration.ValidateOnSaveEnabled = false;
                    db.KhachHangs.Add(_kh);
                    db.SaveChanges();
                    return RedirectToAction("checkout");
                }
                else
                {
                    ViewBag.error = "Email already exists";
                    return View();
                }
            }
           
            return View();
        }
        public ActionResult contact()
        {
            return View();
        }
        public ActionResult details()
        {
            return View();
        }
        public ActionResult productgird()
        {
            return View();
        }
        public ActionResult productlitst()
        {
            return View();
        }
        public ActionResult ProductBoy()
        {
            return View();
        }
        public ActionResult ProductChild()
        {
            return View();
        }
        public ActionResult productHot()
        {
            return View();
        }
        public ActionResult Adias()
        {
            return View();
        }
        public ActionResult ProductNike()
        {
            return View();
        }
        public ActionResult ProductPuma()
        {
            return View();
        }
        public ActionResult ProductBrooks()
        {
            return View();
        }
        public ActionResult ProductNewBalance()
        {
            return View();
        }
        public PartialViewResult partialHotProduct()
        {
            var listGiay = db.Giays.Take(12).ToList();
            return PartialView(listGiay);
        }
        public PartialViewResult UUDaiPartial()
        {
            var listGiay = db.Giays.Take(3).ToList();
            return PartialView(listGiay);
        }
        public PartialViewResult boyPartial(int? page)
        {
            // so sp tren trang
            int pageSize = 8;
            // so trang
            int pageNumber = (page ?? 1);
            var listgiay = db.Giays
                .Where(n => n.MaLoaiGiay == 1)
                .ToList()
                .OrderBy(n => n.GiaBan)
                .ToPagedList(pageNumber, pageSize);
            return PartialView(listgiay);
        }
        public PartialViewResult girdPartial(int? page)
        {
            // so sp tren trang
            int pageSize = 8;
            // so trang
            int pageNumber = (page ?? 1);
            var listgiay = db.Giays
                .Where(n => n.MaLoaiGiay == 2)
                .ToList()
                .OrderBy(n=>n.GiaBan)
                .ToPagedList(pageNumber, pageSize);
            return PartialView(listgiay);
        }
        public PartialViewResult ChildPartial(int?page)
        {
            // so sp tren trang
            int pageSize = 8;
            // so trang
            int pageNumber = (page ?? 1);
            var listgiay = db.Giays
                .Where(n => n.MaLoaiGiay == 3)
                .ToList()
                .OrderBy(n => n.GiaBan)
                .ToPagedList(pageNumber, pageSize);
            return PartialView(listgiay);
        }
        public PartialViewResult HotPartial(int ? page)
        {
            // so sp tren trang
            int pageSize = 8;
            // so trang
            int pageNumber = (page ?? 1);
            var listgiay = db.Giays
                .Where(n => n.MaLoaiGiay == 4)
                .ToList()
                .OrderBy(n => n.GiaBan)
                .ToPagedList(pageNumber, pageSize);
            return PartialView(listgiay);
        }
        public PartialViewResult AdiasPartial(int ? page)
        {
            // so sp tren trang
            int pageSize = 8;
            // so trang
            int pageNumber = (page ?? 1);
            var listgiay = db.Giays
                .Where(n => n.MaThuongHieu==1001)
                .ToList()
                .OrderBy(n => n.GiaBan)
                .ToPagedList(pageNumber, pageSize);
            return PartialView(listgiay);
        }
        public PartialViewResult NikePartial(int ? page)
        {
            // so sp tren trang
            int pageSize = 8;
            // so trang
            int pageNumber = (page ?? 1);
            var listgiay = db.Giays
                .Where(n => n.MaThuongHieu == 1003)
                .ToList()
                .OrderBy(n => n.GiaBan)
                .ToPagedList(pageNumber, pageSize);
            return PartialView(listgiay);
        }
        public PartialViewResult PumaPartial(int? page)
        {
            // so sp tren trang
            int pageSize = 8;
            // so trang
            int pageNumber = (page ?? 1);
            var listgiay = db.Giays
                .Where(n => n.MaThuongHieu == 1004)
                .ToList()
                .OrderBy(n => n.GiaBan)
                .ToPagedList(pageNumber, pageSize);
            return PartialView(listgiay);
        }
        public PartialViewResult BrooksPartial(int? page)
        {
            // so sp tren trang
            int pageSize = 8;
            // so trang
            int pageNumber = (page ?? 1);
            var listgiay = db.Giays
                .Where(n => n.MaThuongHieu == 1002)
                .ToList()
                .OrderBy(n => n.GiaBan)
                .ToPagedList(pageNumber, pageSize);
            return PartialView(listgiay);
        }
        public PartialViewResult NewBalancePartial(int ? page)
        {
            // so sp tren trang
            int pageSize = 8;
            // so trang
            int pageNumber = (page ?? 1);
            var listgiay = db.Giays
                .Where(n => n.MaThuongHieu == 1005)
                .ToList()
                .OrderBy(n => n.GiaBan)
                .ToPagedList(pageNumber, pageSize);
            return PartialView(listgiay);
        }
        [HttpPost]
        public ActionResult KetquaTimkiem(FormCollection f,int? page)
        {
            string sTukhoa = f["search"].ToString();
            List<Giay> listKQTK = db.Giays.Where(n => n.TenGiay.Contains(sTukhoa)).ToList();
            //phan trang
            int pageNumber = (page ?? 1);
            int pageSize = 9;
            if(listKQTK.Count==0)
            {
                ViewBag.Thongbao = "Không tìm thấy sản phẩm nào";
                return View(db.Giays.OrderBy(n => n.TenGiay).ToPagedList(pageNumber, pageSize));
            }    
            return View(listKQTK.OrderBy(n=>n.TenGiay).ToPagedList(pageNumber,pageSize));
        }

    }
}