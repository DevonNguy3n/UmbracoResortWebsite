using Microsoft.AspNetCore.Mvc;
using Umbraco.Cms.Core.Cache;
using Umbraco.Cms.Core.Logging;
using Umbraco.Cms.Core.Routing;
using Umbraco.Cms.Core.Services;
using Umbraco.Cms.Core.Web;
using Umbraco.Cms.Web.Website.Controllers;
using Umbraco.Cms.Infrastructure.Persistence;
using NPoco;

namespace Umbraco.Controllers
{
    public class BookingFormModel
    {
        public string? FullName { get; set; }
        public string? CheckIn { get; set; }
        public string? CheckOut { get; set; }
        public string? Adults { get; set; }
        public string? Children { get; set; }
        public string? RoomType { get; set; }
        public string? Email { get; set; }
        public string? Message { get; set; }
    }

    public class ContactFormModel
    {
        public string? Name { get; set; }
        public string? Email { get; set; }
        public string? Phone { get; set; }
        public string? ServiceType { get; set; }
        public string? Message { get; set; }
    }

    [TableName("CustomBookings")]
    [PrimaryKey("Id", AutoIncrement = true)]
    public class BookingRecord
    {
        public int Id { get; set; }
        public string? FullName { get; set; }
        public string? CheckIn { get; set; }
        public string? CheckOut { get; set; }
        public string? Adults { get; set; }
        public string? Children { get; set; }
        public string? RoomType { get; set; }
        public string? Email { get; set; }
        public string? Message { get; set; }
        public DateTime CreatedDate { get; set; }
    }

    [TableName("CustomContacts")]
    [PrimaryKey("Id", AutoIncrement = true)]
    public class ContactRecord
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Email { get; set; }
        public string? Phone { get; set; }
        public string? ServiceType { get; set; }
        public string? Message { get; set; }
        public DateTime CreatedDate { get; set; }
    }

    public class FormSurfaceController : SurfaceController
    {
        private readonly IUmbracoDatabaseFactory _databaseFactory;

        public FormSurfaceController(
            IUmbracoContextAccessor umbracoContextAccessor,
            IUmbracoDatabaseFactory databaseFactory,
            ServiceContext services,
            AppCaches appCaches,
            IProfilingLogger profilingLogger,
            IPublishedUrlProvider publishedUrlProvider) 
            : base(umbracoContextAccessor, databaseFactory, services, appCaches, profilingLogger, publishedUrlProvider)
        {
            _databaseFactory = databaseFactory;

            InitializeDatabase();
        }

        private void InitializeDatabase()
        {
            try
            {
                using (var db = _databaseFactory.CreateDatabase())
                {
                    db.Execute(@"
                        CREATE TABLE IF NOT EXISTS CustomBookings (
                            Id INTEGER PRIMARY KEY AUTOINCREMENT,
                            FullName TEXT NULL,
                            CheckIn TEXT NULL,
                            CheckOut TEXT NULL,
                            Adults TEXT NULL,
                            Children TEXT NULL,
                            RoomType TEXT NULL,
                            Email TEXT NULL,
                            Message TEXT NULL,
                            CreatedDate TEXT NULL
                        );
                    ");

                    db.Execute(@"
                        CREATE TABLE IF NOT EXISTS CustomContacts (
                            Id INTEGER PRIMARY KEY AUTOINCREMENT,
                            Name TEXT NULL,
                            Email TEXT NULL,
                            Phone TEXT NULL,
                            ServiceType TEXT NULL,
                            Message TEXT NULL,
                            CreatedDate TEXT NULL
                        );
                    ");
                }
            }
            catch (Exception)
            {
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> HandleBooking(BookingFormModel model)
        {
            if (model == null || string.IsNullOrEmpty(model.FullName) || string.IsNullOrEmpty(model.Email))
            {
                TempData["BookingStatus"] = "Vui lòng điền đầy đủ họ tên và email!";
                return CurrentUmbracoPage();
            }

            try
            {
                using (var db = _databaseFactory.CreateDatabase())
                {
                    var record = new BookingRecord
                    {
                        FullName = model.FullName,
                        CheckIn = model.CheckIn,
                        CheckOut = model.CheckOut,
                        Adults = model.Adults,
                        Children = model.Children,
                        RoomType = model.RoomType,
                        Email = model.Email,
                        Message = model.Message,
                        CreatedDate = DateTime.UtcNow
                    };
                    db.Insert(record);
                }

                TempData["BookingSuccess"] = "Đơn đặt phòng đã được gửi thành công!";
                return RedirectToCurrentUmbracoPage();
            }
            catch (Exception ex)
            {
                TempData["BookingStatus"] = $"Lỗi hệ thống: {ex.Message}";
                return CurrentUmbracoPage();
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> HandleContact(ContactFormModel model)
        {
            if (model == null || string.IsNullOrEmpty(model.Name) || string.IsNullOrEmpty(model.Email))
            {
                TempData["ContactStatus"] = "Vui lòng điền đầy đủ họ tên và email!";
                return CurrentUmbracoPage();
            }

            try
            {
                using (var db = _databaseFactory.CreateDatabase())
                {
                    var record = new ContactRecord
                    {
                        Name = model.Name,
                        Email = model.Email,
                        Phone = model.Phone,
                        ServiceType = model.ServiceType,
                        Message = model.Message,
                        CreatedDate = DateTime.UtcNow
                    };
                    db.Insert(record);
                }

                TempData["ContactStatus"] = "Gửi yêu cầu liên hệ thành công!";
                return RedirectToCurrentUmbracoPage();
            }
            catch (Exception ex)
            {
                TempData["ContactStatus"] = $"Lỗi hệ thống: {ex.Message}";
                return CurrentUmbracoPage();
            }
        }
    }
}