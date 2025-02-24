using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OfficeOpenXml;
using System.Text.Json;
using WebDoDienTu.Data;
using WebDoDienTu.Models;


namespace WebDoDienTu.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = SD.Role_Admin)]

    public class ThongKeController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ThongKeController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            // Thống kê doanh thu theo tháng (Area Chart)
            var revenueData = await _context.Orders
                            .GroupBy(o => new { o.OrderDate.Year, o.OrderDate.Month })
                            .Select(g => new
                            {
                                Year = g.Key.Year,
                                Month = g.Key.Month,
                                Revenue = g.Sum(o => o.TotalPrice)
                            })
                            .ToListAsync(); 

            var revenueByMonth = new Dictionary<int, decimal>();
            for (int i = 1; i <= 12; i++)
            {
                revenueByMonth[i] = revenueData
                    .Where(x => x.Month == i)
                    .Sum(x => x.Revenue);
            }

            ViewBag.RevenueByMonth = revenueByMonth;

            // Thống kê sản phẩm bán chạy (Pie Chart)
            var topProducts = _context.OrderDetails
                .GroupBy(od => od.Product)
                .Select(g => new
                {
                    ProductName = g.Key.ProductName,
                    TotalQuantity = g.Sum(od => od.Quantity)
                })
                .OrderByDescending(x => x.TotalQuantity)
                .Take(5)
                .ToList();

            // Mã hóa JSON
            ViewBag.ProductNames = JsonSerializer.Serialize(topProducts.Select(x => x.ProductName).ToList());
            ViewBag.ProductQuantities = JsonSerializer.Serialize(topProducts.Select(x => x.TotalQuantity).ToList());
            return View();
        }

        public async Task<IActionResult> MonthlyRevenue()
        {
            var revenueData = await _context.Orders
                .GroupBy(o => new { o.OrderDate.Year, o.OrderDate.Month })
                .Select(g => new
                {
                    Year = g.Key.Year,
                    Month = g.Key.Month,
                    Revenue = g.Sum(o => o.TotalPrice)
                })
                .ToListAsync(); // Thực hiện truy vấn và chuyển đổi kết quả thành danh sách in-memory

            var revenueByMonth = new Dictionary<int, decimal>();
            for (int i = 1; i <= 12; i++)
            {
                revenueByMonth[i] = revenueData
                    .Where(x => x.Month == i)
                    .Sum(x => x.Revenue);
            }

            ViewBag.RevenueByMonth = revenueByMonth;
            return View();
        }

        public ActionResult MostPurchasedProducts()
        {
            var topProducts = _context.OrderDetails
                .GroupBy(od => od.Product)
                .Select(g => new
                {
                    ProductName = g.Key.ProductName,
                    TotalQuantity = g.Sum(od => od.Quantity)
                })
                .OrderByDescending(x => x.TotalQuantity)
                .Take(5)
                .ToList();

            // Mã hóa JSON
            ViewBag.ProductNames = JsonSerializer.Serialize(topProducts.Select(x => x.ProductName).ToList());
            ViewBag.ProductQuantities = JsonSerializer.Serialize(topProducts.Select(x => x.TotalQuantity).ToList());

            return View();
        }


        public ActionResult ExportRevenueByMonth()
        {
            var revenueByMonth = _context.Orders
                .GroupBy(o => new { Month = o.OrderDate.Month, Year = o.OrderDate.Year })
                .Select(g => new { Month = g.Key.Month, Year = g.Key.Year, TotalRevenue = g.Sum(o => o.TotalPrice) })
                .OrderBy(g => g.Year)
                .ThenBy(g => g.Month)
                .ToList();

            using (var package = new ExcelPackage())
            {
                var worksheet = package.Workbook.Worksheets.Add("RevenueByMonth");

                // Add logo
                var logoPath = "E:\\DO AN CO SO\\WebDoDienTu\\WebDoDienTu\\wwwroot\\image\\mau-logo-dep.jpg"; // Replace with the actual path to your logo image
                var logo = new FileInfo(logoPath);

                if (logo.Exists)
                {
                    var picture = worksheet.Drawings.AddPicture("Logo", logo);
                    picture.SetPosition(0, 0, 0, 0); // Adjust the position if needed
                    picture.SetSize(100, 100); // Adjust the size if needed
                }

                // Merge cells and set company information
                worksheet.Cells["C1:G1"].Merge = true;
                worksheet.Cells["C1:G1"].Value = "CÔNG TY TNHH ABC.VN";
                worksheet.Cells["C2:G2"].Merge = true;
                worksheet.Cells["C2:G2"].Value = " Phân khu đào tạo E1, Khu Công Nghệ cao TP.HCM, Phường Hiệp Phú, TP. Thủ Đức, TP.HCM";
                worksheet.Cells["C3:E3"].Merge = true;
                worksheet.Cells["C3:E3"].Value = "Điện Thoại: 038 997 8430";
                worksheet.Cells["F3:G3"].Merge = true;
                worksheet.Cells["F3:G3"].Value = "- Fax: ..........";

                worksheet.Cells["A5:G5"].Merge = true;
                worksheet.Cells["A5:G5"].Style.Font.Size = 14;
                worksheet.Cells["A5:G5"].Style.Font.Bold = true;
                worksheet.Cells["A5:G5"].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                worksheet.Cells["A5:G5"].Value = "BÁO CÁO DOANH THU THEO THÁNG";

                worksheet.Cells["A6:G6"].Merge = true;
                worksheet.Cells["A6:G6"].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                worksheet.Cells["A6:G6"].Value = $"Ngày {DateTime.Now:dd} Tháng {DateTime.Now:MM} Năm {DateTime.Now:yyyy}";

                // Set table headers with bold text and blue background
                var headerCells = worksheet.Cells["A13:D13"];
                headerCells.Style.Font.Bold = true;
                headerCells.Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
                headerCells.Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.LightBlue);

                worksheet.Cells["A13"].Value = "STT";
                worksheet.Cells["B13"].Value = "Tháng";
                worksheet.Cells["C13"].Value = "Năm";
                worksheet.Cells["D13"].Value = "Doanh thu";

                // Set custom column widths
                worksheet.Column(1).Width = 5; // STT
                worksheet.Column(2).Width = 15; // Tháng
                worksheet.Column(3).Width = 15; // Năm
                worksheet.Column(4).Width = 20; // Doanh thu

                // Thiết lập định dạng tiền VND cho cột doanh thu
                var revenueColumn = worksheet.Column(4);
                revenueColumn.Style.Numberformat.Format = "#,##0";

                // Thêm dữ liệu và thiết lập border cho các ô chứa dữ liệu
                int row = 14; // Start data from row 14
                int stt = 1;
                decimal totalRevenue = 0;
                foreach (var item in revenueByMonth)
                {
                    worksheet.Cells[row, 1].Value = stt;
                    worksheet.Cells[row, 2].Value = item.Month;
                    worksheet.Cells[row, 3].Value = item.Year;
                    worksheet.Cells[row, 4].Value = item.TotalRevenue;
                    totalRevenue += item.TotalRevenue;

                    // Thiết lập border cho các ô chứa dữ liệu
                    for (int col = 1; col <= 4; col++)
                    {
                        var cell = worksheet.Cells[row, col];
                        cell.Style.Border.Top.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                        cell.Style.Border.Left.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                        cell.Style.Border.Right.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                        cell.Style.Border.Bottom.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                    }

                    row++;
                    stt++;
                }

                // Thêm ô chứa tổng doanh thu
                worksheet.Cells[row, 1].Value = "Tổng";
                worksheet.Cells[row, 1].Style.Font.Bold = true;
                worksheet.Cells[row, 4].Value = totalRevenue;
                worksheet.Cells[row, 4].Style.Numberformat.Format = "#,##0";
                worksheet.Cells[row, 1, row, 4].Style.Border.Top.Style = OfficeOpenXml.Style.ExcelBorderStyle.Double;

                // Add border to header
                for (int col = 1; col <= 4; col++)
                {
                    var cell = worksheet.Cells[13, col];
                    cell.Style.Border.Top.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                    cell.Style.Border.Left.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                    cell.Style.Border.Right.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                    cell.Style.Border.Bottom.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                }

                var stream = new MemoryStream();
                package.SaveAs(stream);
                stream.Position = 0;

                string excelName = $"Doanh thu nam {DateTime.Now:yyyy}-{DateTime.Now:yyyyMMddHHmmssfff}.xlsx";

                return File(stream, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", excelName);
            }
        }



    }
}
