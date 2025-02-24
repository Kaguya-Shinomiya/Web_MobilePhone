using Microsoft.EntityFrameworkCore;
using WebDoDienTu.Models;
using Microsoft.AspNetCore.Identity;
using WebDoDienTu.Service;
using OfficeOpenXml;
using WebDoDienTu.Hubs;
using CloudinaryDotNet;
using Microsoft.Extensions.Options;
using WebDoDienTu.Service.MomoPayment;
using WebDoDienTu.Service.VNPayPayment;
using WebDoDienTu.Service.MailKit;
using WebDoDienTu.Service.Cloudinary;
using WebDoDienTu.Data;
using WebDoDienTu.Service.PayPal;
using Hangfire;
using Hangfire.SqlServer;
using WebDoDienTu.Repository;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.AspNetCore.Localization;
using System.Globalization;
using WebDoDienTu.Mappings;
using WebDoDienTu.Service.ProductRecommendation;

internal class Program
{
    private static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
        var config = builder.Configuration;

        // Configure database
        builder.Services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

        // Configure Identity
        builder.Services.AddIdentity<ApplicationUser, IdentityRole>()
                        .AddEntityFrameworkStores<ApplicationDbContext>()
                        .AddDefaultTokenProviders()
                        .AddDefaultUI();

        // Configure localization
        builder.Services.AddLocalization(options => options.ResourcesPath = "Resources");
        builder.Services.AddControllersWithViews()
                        .AddViewLocalization(LanguageViewLocationExpanderFormat.Suffix)
                        .AddDataAnnotationsLocalization();

        // Configure JSON options
        builder.Services.AddControllers().AddJsonOptions(options =>
        {
            options.JsonSerializerOptions.ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.Preserve;
        });


        // Configure file upload limits
        builder.WebHost.ConfigureKestrel(options =>
        {
            options.ListenAnyIP(5122);
            options.ListenAnyIP(7209, listenOptions =>
            {
                listenOptions.UseHttps();
            });
            options.Limits.MaxRequestBodySize = 100 * 1024 * 1024;
        });


        // Configure cookies and sessions
        builder.Services.ConfigureApplicationCookie(option =>
        {
            option.LoginPath = $"/Identity/Account/Login";
            option.LogoutPath = $"/Identity/Account/Logout";
            option.LogoutPath = $"/Identity/Account/AccesDenied";
        });

        builder.Services.AddDistributedMemoryCache();
        builder.Services.AddSession(options =>
        {
            options.IdleTimeout = TimeSpan.FromMinutes(30);
            options.Cookie.HttpOnly = true;
            options.Cookie.IsEssential = true;
        });

        // Configure third-party authentication
        builder.Services.AddAuthentication()
            .AddGoogle(googleOptions =>
            {
                googleOptions.ClientId = builder.Configuration["Authentication:Google:ClientId"];
                googleOptions.ClientSecret = builder.Configuration["Authentication:Google:ClientSecret"];
            });

        builder.Services.AddAuthentication()
            .AddFacebook(facebookOptions =>
            {
                facebookOptions.AppId = builder.Configuration["Authentication:Facebook:AppId"];
                facebookOptions.AppSecret = builder.Configuration["Authentication:Facebook:AppSecret"];
                facebookOptions.AccessDeniedPath = "/AccessDeniedPathInfo";
            });

        // Configure email services
        builder.Services.Configure<EmailSettings>(config.GetSection("EmailSettings"));
        builder.Services.AddTransient<IEmailSender, EmailSender>();

        // Configure Cloudinary
        builder.Services.Configure<CloudinarySettings>(builder.Configuration.GetSection("Cloudinary"));
        builder.Services.AddSingleton<ICloudinary>(provider =>
        {
            var settings = provider.GetRequiredService<IOptions<CloudinarySettings>>().Value;

            var account = new Account(
                settings.CloudName,
                settings.ApiKey,
                settings.ApiSecret
            );

            return new Cloudinary(account);
        });

        // Configure Hangfire
        builder.Services.AddHangfire(configuration => configuration.SetDataCompatibilityLevel(CompatibilityLevel.Version_170)
            .UseSimpleAssemblyNameTypeSerializer()
            .UseDefaultTypeSerializer()
            .UseSqlServerStorage(config.GetConnectionString("DefaultConnection"), new SqlServerStorageOptions
            {
                CommandBatchMaxTimeout = TimeSpan.FromMinutes(5),
                SlidingInvisibilityTimeout = TimeSpan.FromMinutes(5),
                QueuePollInterval = TimeSpan.Zero,
                UseRecommendedIsolationLevel = true,
                DisableGlobalLocks = true
            }));
        builder.Services.AddHangfireServer();

        // Configure services
        builder.Services.AddScoped<IProductRepository, ProductRepository>();
        builder.Services.AddScoped<IProductService, ProductService>();
        builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();
        builder.Services.AddScoped<ICategoryService, CategoryService>();
        builder.Services.AddScoped<IOrderService, OrderService>();
        builder.Services.AddScoped<IPostCategoryRepository, PostCategoryRepository>();
        builder.Services.AddScoped<IPostRepository, PostRepository>();
        builder.Services.AddScoped<ICommentRepository, CommentRepository>();
        builder.Services.AddSingleton<IVnPayService, VnPayService>();
        builder.Services.AddScoped<IMomoPaymentService, MomoPaymentService>();
        builder.Services.AddScoped<IProductViewRepository, ProductViewRepository>();
        builder.Services.AddScoped<IProductViewService, ProductViewService>();
        builder.Services.AddScoped<IPayPalPaymentService, PayPalPaymentService>();
        builder.Services.AddScoped<RecommendationService>();
        builder.Services.AddScoped<ProductRecommendationService>();

        // Configure Excel license
        ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

        // AutoMapper
        builder.Services.AddAutoMapper(typeof(MappingProfile));

        // Configure SignalR
        builder.Services.AddSignalR();

        // Add the processing server as IHostedService
        builder.Services.AddRazorPages();

        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (!app.Environment.IsDevelopment())
        {
            app.UseExceptionHandler("/Home/Error");
            // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
            app.UseHsts();
        }

        app.UseHttpsRedirection();
        app.UseStaticFiles();

        // Configure localization
        var supportedCultures = new[] { "en-US", "vi-VN" };
        var localizationOptions = new RequestLocalizationOptions
        {
            DefaultRequestCulture = new RequestCulture("vi-VN"),
            SupportedCultures = supportedCultures.Select(c => new CultureInfo(c)).ToList(),
            SupportedUICultures = supportedCultures.Select(c => new CultureInfo(c)).ToList(),
            RequestCultureProviders = new List<IRequestCultureProvider>
            {
                new QueryStringRequestCultureProvider { QueryStringKey = "culture", UIQueryStringKey = "ui-culture" },
                new CookieRequestCultureProvider(),
                new AcceptLanguageHeaderRequestCultureProvider()
            }
        };
        app.UseRequestLocalization(localizationOptions);

        // Middleware for storing selected culture in cookies
        app.Use(async (context, next) =>
        {
            if (context.Request.Query.ContainsKey("culture"))
            {
                var culture = context.Request.Query["culture"];
                context.Response.Cookies.Append(
                    CookieRequestCultureProvider.DefaultCookieName,
                    CookieRequestCultureProvider.MakeCookieValue(new RequestCulture(culture)),
                    new CookieOptions
                    {
                        Expires = DateTimeOffset.UtcNow.AddYears(1),
                        HttpOnly = true,
                        SameSite = SameSiteMode.Lax,
                        Secure = context.Request.IsHttps
                    });
            }
            await next.Invoke();
        });
        app.UseRouting();
        app.UseSession();
        app.UseAuthorization();

        // Create default admin user
        CreateDefaultAdminUser(app).GetAwaiter().GetResult();

        app.MapRazorPages();
        app.MapControllerRoute(
            name: "Admin",
            pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}");
        app.MapControllerRoute(
            name: "default",
            pattern: "{controller=Home}/{action=Index}/{id?}");

        app.MapControllerRoute(
            name: "adminChat",
            pattern: "Admin/Chat/{action=AdminChat}/{id?}",
            defaults: new { controller = "Chat", action = "AdminChat" });

        app.MapControllerRoute(
            name: "userChat",
            pattern: "User/Chat/{action=UserChat}/{id?}",
            defaults: new { controller = "Chat", action = "UserChat" });

        app.MapHub<ChatHub>("/chathub");
        app.MapHub<PostHub>("/posthub");

        app.UseHangfireDashboard("/hangfire");

        app.Run();
    }

    private static async Task CreateDefaultAdminUser(WebApplication app)
    {
        using (var scope = app.Services.CreateScope())
        {
            var userManager = scope.ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>();
            var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();

            // Tạo vai trò Admin nếu chưa tồn tại
            var role = new IdentityRole(SD.Role_Admin);
            var roleExists = await roleManager.RoleExistsAsync(role.Name);
            if (!roleExists)
            {
                await roleManager.CreateAsync(role);
            }

            // Tạo tài khoản Admin nếu chưa tồn tại
            var user = await userManager.FindByEmailAsync("admin@example.com");
            if (user == null)
            {
                user = new ApplicationUser { UserName = "admin@example.com", Email = "admin@example.com" };
                var result = await userManager.CreateAsync(user, "Password123!");
                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(user, role.Name);
                    Console.WriteLine("Admin user created successfully.");
                }
                else
                {
                    Console.WriteLine($"Failed to create user: {string.Join(", ", result.Errors.Select(e => e.Description))}");
                }
            }
            else
            {
                Console.WriteLine("Admin user already exists.");
            }
        }
    }
}

