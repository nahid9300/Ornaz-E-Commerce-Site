using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using AutoMapper;
using OrnamentsWebApplication.Models;
using OrnamentsWebApplication.ViewModel;
using Ornaments.Model.Model;

namespace OrnamentsWebApplication
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);


            Mapper.Initialize(cfg=> {
            cfg.CreateMap<CategoryViewModel, Category>();
            cfg.CreateMap<Category, CategoryViewModel>();

             cfg.CreateMap<ProductViewModel, Product>();
             cfg.CreateMap<Product, ProductViewModel>();

             cfg.CreateMap<ConfigViewModel, Config>();
             cfg.CreateMap<Config, ConfigViewModel>();

           
            });
        }
    }
}
