// // using System;
// // using System.Collections.Generic;
// // using System.Linq;
// // using System.Threading.Tasks;

// // namespace Presentation.Properties
// // {
// //     public class Startup
// //     {

// //     }
// // }
// using Core.Services;
// namespace Presentation
// {
//     public class Startup(IConfiguration configuration, IWebHostEnvironment webHostEnvironment)
//     {
//         private const string _defaultCorsPolicyName = "localhost";
//         private readonly IWebHostEnvironment _webHostEnvironment = webHostEnvironment;
//         public IConfiguration Configuration { get; } = configuration;
//         public void ConfigureServices(IServiceCollection services)
//         {
//             //enable cors
//             var corsOriginsString = Configuration.GetValue<string>("GeneralConfig:CorsOrigins");
//             var corsOrigins = string.IsNullOrEmpty(corsOriginsString)
//             ? []
//             : corsOriginsString.Split(",", StringSplitOptions.RemoveEmptyEntries).ToList();
//             services.AddCors(
//                 options => options.AddPolicy(
//                     _defaultCorsPolicyName,
//                     builder => builder
//                         .AllowAnyHeader()
//                         .AllowAnyMethod()
//                         .AllowCredentials()
//                         .WithOrigins(
//                             [.. corsOrigins]
//                         )
//                         .WithExposedHeaders("Content-Disposition")
//                 )
//             );
//             services.AddControllers();
//             services.AddControllers(options => options.SuppressImplicitRequiredAttributeForNonNullableReferenceTypes = true);



//             // services.Configure<KestrelServerOptions>(options =>
//             // {
//             //     options.AllowSynchronousIO = true;
//             // });
//             // services.Configure<IISServerOptions>(options =>
//             // {
//             //     options.AllowSynchronousIO = true;
//             // });

        

//             #region Services Registration
            
//             services.AddHttpContextAccessor();
//             services.AddScoped<ICarServices, CarServices>();

        

//             #endregion
//         }

//         public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
//         {   app.UseRouting();
//             app.UseAuthentication();
//             app.UseAuthorization();
//             app.UseHttpsRedirection();
//             app.UseEndpoints(endpoints =>
//             {
//                 endpoints.MapControllers();
//             });
//             app.UseExceptionHandler("/Error");

//             //route configuration
//             app.UseEndpoints(endpoints =>
//             {
//                 endpoints.MapControllerRoute(
//                     name: "default",
//                     pattern: "{controller=Home}/{action=Index}/{id?}");
//             });

//         }
//     }
// }