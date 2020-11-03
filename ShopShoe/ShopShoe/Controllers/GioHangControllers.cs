using Microsoft.Win32.SafeHandles;
using ShopShoe.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ShopShoe.Controllers
{
    public class GioHangController : Controller
    {

        DatabaseWebBanGiay db = new DatabaseWebBanGiay();

        // Lấy GioHang
        public List<GioHang> LayGioHang()
        {
            List<GioHang> ListGioHang = Session["GioHang"] as List<GioHang>;
            if (ListGioHang == null)
            {
                ListGioHang = new List<GioHang>();
                Session["GioHang"] = ListGioHang;
            }
            return ListGioHang;
        }
        public ActionResult ThemGioHang(int iMaGiay, string strURL)
        {
            Giay giay = db.Giays.SingleOrDefault(n => n.MaGiay == iMaGiay);
            if (giay == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            //Lấy sesion giỏ hàng
            List<GioHang> listGioHang = LayGioHang();
            // kiểm tra sách này đã tồn tại trong sesion giỏ hàng chưa
            GioHang sp = listGioHang.Find(n => n.iMaGiay == iMaGiay);
            if (sp == null)
            {
                sp = new GioHang(iMaGiay);
                // thêm sản phầm
                listGioHang.Add(sp);
                return Redirect(strURL);
            }
            else
            {
                sp.iSoLuong++;
                return Redirect(strURL);
            }
        }
        // Sửa giỏ hàng
        public ActionResult CapNhatGioHang(int iMaGiay, FormCollection f)
        {
            //Kiểm tra mã sản phẩm
            Giay giay = db.Giays.SingleOrDefault(n => n.MaGiay == iMaGiay);
            // nếu get sai mã sản phẩn sẽ trả về lỗi 404
            if (giay == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            // lấy sesion của giỏ hàng
            List<GioHang> listGioHang = LayGioHang();
            // kiểm tra giỏ hàng đã tồn tại session
            GioHang sp = listGioHang.Find(n => n.iMaGiay == iMaGiay);
            if (sp != null)
            {
                sp.iSoLuong = int.Parse(f["txtSoLuong"].ToString());
            }
            return View("GioHang");
        }
        //Xóa giỏ hàng 
        public ActionResult XoaGioHang(int iMaGiay)
        {
            //Kiểm tra mã sản phẩm
            Giay giay = db.Giays.SingleOrDefault(n => n.MaGiay == iMaGiay);
            // nếu get sai mã sản phẩn sẽ trả về lỗi 404
            if (giay == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            // lấy sesion của giỏ hàng
            List<GioHang> listGioHang = LayGioHang();
            GioHang sp = listGioHang.Find(n => n.iMaGiay == iMaGiay);
            if (sp != null)
            {
                listGioHang.RemoveAll(n => n.iMaGiay == iMaGiay);
            }
            if (listGioHang.Count == 0)
            {
                return RedirectToAction("Index","Home");
            }
            return RedirectToAction("GioHang");
        }
        // xây dựng chức năng giỏ hàng
        public ActionResult GioHang()
        {
            if (Session["GioHang"] == null)
            {
                return RedirectToAction("Index", "Home");
            }
            List<GioHang> listGioHang = LayGioHang();
            return View("listGioHang");
        }
        //Tính tổng số lượng và tổng tiền
        //Tính tổng số lượng
        private int TongSoLuong()
        {
            int iTongSoLuong = 0;
            List<GioHang> listGioHang = Session["GioHang"] as List<GioHang>;
            if (listGioHang != null)
            {
                iTongSoLuong = listGioHang.Sum(n => n.iSoLuong);
            }
            return iTongSoLuong;
        }
        //Tính tổng thành tiền
        private double TongTien()
        {
            double dTongTien = 0;
            List<GioHang> listGioHang = Session["GioHang"] as List<GioHang>;
            if (listGioHang != null)
            {
                dTongTien = listGioHang.Sum(n => n.ThanhTien);
            }
            return dTongTien;
        }
        //tạo partial giỏ hàng
        public ActionResult GioHangPartial()
        {
            if (TongSoLuong() == 0)
            {
                return PartialView();
            }
            ViewBag.TongSoLuong = TongSoLuong();
            ViewBag.TongTien = TongTien();
            return PartialView();
        }
        //Xây dựng 1 view cho người dùng chỉnh sửa giỏ hàng
        public ActionResult SuaGioHang()
        {
            if (Session["GioHang"] == null)
            {
                return RedirectToAction("Index", "Home");
            }
            List<GioHang> listGioHang = LayGioHang();
            return View(listGioHang);

        }
        //Xây dựng chức năng đặt hàng
        public ActionResult DatHang()
        {
            //Kiểm tra đăng đăng nhập
            if (Session["TaiKhoan"] == null || Session["TaiKhoan"].ToString() == "")
            {
                return RedirectToAction("checkout", "Home");
            }
            //Kiểm tra giỏ hàng
            if (Session["GioHang"] == null)
            {
                RedirectToAction("Index", "Home");
            }
            //Thêm đơn hàng
            DonHang ddh = new DonHang();
            KhachHang kh = (KhachHang)Session["TaiKhoan"];
            List<GioHang> gh = LayGioHang();
            ddh.MaKH = kh.MaKH;
            ddh.NgayDat = DateTime.Now;
            db.DonHangs.Add(ddh);
            db.SaveChanges();
            //Thêm chi tiết đơn hàng
            foreach (var item in gh)
            {
                ChiTietDonHang ctDH = new ChiTietDonHang();
                ctDH.MaDonHang = ddh.MaDonHang;
                ctDH.MaGiay = item.iMaGiay;
                ctDH.SoLuong = item.iSoLuong;
                //  ctDH.DonGia = (decimal)item.dDonGia;
                db.ChiTietDonHangs.Add(ctDH);
            }
            db.SaveChanges();
            return RedirectToAction("Index", "Home");
        }

    }
}