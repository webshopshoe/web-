using Microsoft.Win32.SafeHandles;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Web;

namespace ShopShoe.Models
{
    public class GioHang
    {
        DatabaseWebBanGiay db = new DatabaseWebBanGiay();
        public int iMaGiay { get; set; }
        public string sTenGiay { get; set; }
        public string sAnhBia { get; set; }
        public decimal dDonGia { get; set; }
        public int iSoLuong { get; set; }
        public double ThanhTien
        {
            get { return (double)(iSoLuong * dDonGia); }
        }
        //Hàm tạo cho giỏ hàng
        public GioHang(int MaGiay)
        {
            iMaGiay = MaGiay;
            Giay giay = db.Giays.Single(n => n.MaGiay == MaGiay);
            sTenGiay = giay.TenGiay;
            sAnhBia = giay.AnhBia;
            dDonGia = decimal.Parse(giay.GiaBan.ToString());
            iSoLuong = 1;


        }
    }
}