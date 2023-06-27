using Business.Abstract;
using Business.Concrete;
using DataAccess.Abstract;
using DataAccess.Concrete.EfCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MvcWebUI
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllersWithViews().AddRazorRuntimeCompilation(); ;

            //DataAccess
            services.AddTransient<ICategoryDal, EfCoreCategoryDal>();
            services.AddTransient<ICustomerDal, EfCoreCustomerDal>();
            services.AddTransient<IProductDal, EfCoreProductDal>();
            services.AddTransient<IPurchaseDal, EfCorePurchaseDal>();
            services.AddTransient<ISalesDal, EfCoreSalesDal>();
            services.AddTransient<IStockDal, EfCoreStockDal>();
            services.AddTransient<IReportDal, EfCoreReportDal>();

            //Business
            services.AddTransient<ICategoryService, CategoryManager>();
            services.AddTransient<ICustomerService, CustomerManager>();
            services.AddTransient<IProductService, ProductManager>();
            services.AddTransient<IPurchaseService, PurchaseManager>();
            services.AddTransient<ISaleService, SaleManager>();
            services.AddTransient<IStockService, StockManager>();
            services.AddTransient<IReportService, ReportManager>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
