# Boma Resort Nha Trang - Umbraco CMS Website

Dự án website **Boma Resort Nha Trang** - khu nghỉ dưỡng 5 sao sang trọng tại Nha Trang, Việt Nam. Website được xây dựng và phát triển trên nền tảng **Umbraco CMS** và **.NET 10.0** hiện đại, kết hợp với cơ sở dữ liệu SQLite tối ưu để đem lại trải nghiệm mượt mà nhất.

---

## Tính Năng Nổi Bật (Key Features)

- **Giao diện Sang trọng & Hiện đại**: Thiết kế theo tiêu chuẩn Resort 5 sao sử dụng **TailwindCSS** với khả năng hiển thị tương thích hoàn hảo trên mọi thiết bị di động (Responsive).
- **Hệ thống Slider Động trên Trang Chủ**: Slider chuyển cảnh hình ảnh tự động sau 5 giây (Autoplay) và hỗ trợ tương tác trực quan bằng các nút điều hướng và chấm tròn định vị.
- **Quản lý Danh Sách & Chi Tiết Phòng**: Trình bày trực quan diện tích, tầm nhìn, loại giường và các tiện ích chi tiết đi kèm của từng loại phòng.
- **Tích hợp Lưu trữ Dữ liệu Tự động (SQLite)**:
  - Form **Đặt phòng (Booking)** và **Liên hệ (Contact)** tự động lưu trữ thông tin khách hàng vào cơ sở dữ liệu SQLite cục bộ (`umbraco/Data/Umbraco.sqlite.db`).
  - Sử dụng **NPoco ORM** để tự động kiểm tra và khởi tạo bảng dữ liệu (`CustomBookings` và `CustomContacts`) một cách an toàn khi khởi động.
- **Tối ưu hóa Mã Nguồn**:
  - Loại bỏ hoàn toàn cơ chế gửi email cũ giúp tăng tốc xử lý và loại bỏ các cảnh báo API bị lỗi thời (`CS0618`).
  - Giao diện Razor (`.cshtml`) được làm sạch khoảng trắng, xóa bỏ comment thừa và căn lề chuẩn chỉnh 4 spaces.

---

## Tech Stack

- **Backend**: .NET 10.0, ASP.NET Core MVC.
- **CMS**: Umbraco CMS v13/v14.
- **Database**: SQLite cục bộ.
- **ORM**: NPoco (Micro-ORM mặc định của Umbraco).
- **Frontend**: Razor View Engine (`.cshtml`), TailwindCSS (via CDN), Vanilla JavaScript.
- **Version Control**: Git.

---

## Hướng Dẫn Khởi Chạy Dự Án Cục Bộ (Getting Started)

Để chạy dự án trên máy tính của bạn, vui lòng đảm bảo đã cài đặt **.NET 10.0 SDK**.

### 1. Di chuyển vào thư mục code chính:
```bash
cd Umbraco
```

### 2. Khôi phục các gói thư viện (Restore NuGet Packages):
```bash
dotnet restore
```

### 3. Biên dịch dự án (Build Project):
```bash
dotnet build
```

### 4. Khởi chạy máy chủ (Run Server):
```bash
dotnet run
```
*Sau khi chạy, truy cập đường dẫn cục bộ hiển thị trên terminal (thông thường là `http://localhost:5000` hoặc `https://localhost:5001`) để trải nghiệm website.*

---

## Cấu Trúc Mã Nguồn Chính (Directory Structure)

```text
c:\Users\ACER\Umbraco\
│
├── Umbraco.sln               # File Solution của Visual Studio
├── README.md                 # Tài liệu hướng dẫn dự án này
│
└── Umbraco/                  # Thư mục chứa mã nguồn chính của ứng dụng
    ├── controller/
    │   └── FormSurfaceController.cs   # Xử lý Logic Form, Database (SQLite)
    │
    ├── Views/
    │   ├── master.cshtml              # Giao diện khung chính (Header, Footer, Tailwind)
    │   ├── homePage.cshtml            # Trang chủ và Slider động
    │   ├── RoomList.cshtml            # Trang danh sách phòng nghỉ & Ưu đãi đặc biệt
    │   ├── roomDetail.cshtml          # Trang chi tiết phòng
    │   ├── contactPage.cshtml         # Trang liên hệ
    │   └── ...                        # Các trang dịch vụ khác (Spa, Wedding, Restaurant...)
    │
    └── wwwroot/                       # Các tài nguyên tĩnh (Hình ảnh, CSS, Favicon)
```

---

## Quản Lý Cơ Sở Dữ Liệu SQLite

Toàn bộ dữ liệu của biểu mẫu gửi về sẽ được tự động lưu trữ trong tệp tin:
`Umbraco/umbraco/Data/Umbraco.sqlite.db`

Các bảng dữ liệu tự động khởi tạo bao gồm:
- **`CustomBookings`**: Lưu trữ đơn đặt phòng.
- **`CustomContacts`**: Lưu trữ thông tin liên hệ của khách hàng.

Có thể sử dụng các công cụ như **DB Browser for SQLite** hoặc extension **SQLite Viewer** trong VS Code để kiểm tra và quản lý dữ liệu lưu trữ trực quan.


