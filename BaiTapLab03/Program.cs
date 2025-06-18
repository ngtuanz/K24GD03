using System;
using System.Collections;

internal class MatHang
{
    struct MAtHang
    {
        public int MaMH;
        public string TenMH;
        public int SoLuong;
        public float DonGia;

        public MAtHang(int MaMH, string TenMH, int SoLuong, float DonGia)
        {
            this.MaMH = MaMH;
            this.TenMH = TenMH;
            this.SoLuong = SoLuong;
            this.DonGia = DonGia;
        }

        public float ThanhTien()
        {
            return SoLuong * DonGia;
        }

        public void Xuat()
        {
            Console.WriteLine($"MaMH: {MaMH}, TenMH: {TenMH}, SoLuong: {SoLuong}, DonGia: {DonGia}, ThanhTien: {ThanhTien()}");
        }
    }

    static void ThemMatHang(Hashtable danhsach, MAtHang m)
    {
        danhsach.Add(m.MaMH, m);
    }

    static bool TimMatHang(Hashtable danhsach, int MaMH)
    {
        return danhsach.ContainsKey(MaMH);
    }

    static void XoaMatHang(Hashtable danhsach, int MaMH)
    {
        if (TimMatHang(danhsach, MaMH))
            danhsach.Remove(MaMH);
    }

    static void XuatDanhSach(Hashtable danhsach)
    {
        Console.WriteLine("\nDANH SACH MAT HANG:");
        foreach (DictionaryEntry entry in danhsach)
        {
            MAtHang m = (MAtHang)entry.Value;
            m.Xuat();
        }
    }

    public static void Main(string[] args)
    {
        Hashtable danhsach = new Hashtable();

        char tiep;
        do
        {
            Console.Write("Nhap ma mat hang: ");
            int ma = int.Parse(Console.ReadLine());
            Console.Write("Nhap ten mat hang: ");
            string ten = Console.ReadLine();
            Console.Write("Nhap so luong: ");
            int sl = int.Parse(Console.ReadLine());
            Console.Write("Nhap don gia: ");
            float dg = float.Parse(Console.ReadLine());

            MAtHang mh = new MAtHang(ma, ten, sl, dg);
            ThemMatHang(danhsach, mh);

            Console.Write("Nhap tiep? (y/n): ");
            tiep = Console.ReadLine()[0];
        } while (tiep == 'y' || tiep == 'Y');

        XuatDanhSach(danhsach);

        Console.Write("\nNhap ma mat hang can tim va xoa: ");
        int timMa = int.Parse(Console.ReadLine());
        if (TimMatHang(danhsach, timMa))
        {
            Console.WriteLine("Tim thay. Tien hanh xoa...");
            XoaMatHang(danhsach, timMa);
        }
        else
        {
            Console.WriteLine("Khong tim thay mat hang co ma: " + timMa);
        }

        Console.WriteLine("\nDanh sach sau khi xoa:");
        XuatDanhSach(danhsach);
    }
}
