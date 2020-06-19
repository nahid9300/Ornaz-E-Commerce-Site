using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AutoMapper;
using Ornaments.BLL.BLL;
using Ornaments.Model.Model;
using OrnamentsWebApplication.ViewModel;

namespace OrnamentsWebApplication.Controllers
{
    
    public class ConfigController : Controller
    {
      ConfigurationManager _configurationManager=new ConfigurationManager();

      [HttpGet]
      public ActionResult Create()
      {
          ConfigViewModel configViewModel = new ConfigViewModel();
          configViewModel.Configs = _configurationManager.GetAll();
          return View(configViewModel);
      }
      [HttpPost]
      public ActionResult Create(ConfigViewModel configViewModel)
      {
          string message = "";
          Config config = Mapper.Map<Config>(configViewModel);
          if (_configurationManager.Add(config))
          {
              message = "Url created successfully";
          }
          else
          {
              message = "url did not created";
          }
          ViewBag.Message = message;
          configViewModel.Configs = _configurationManager.GetAll();

          return View(configViewModel);
      }


      [HttpGet]
      public ActionResult Edit(string Key)
      {
          Config config = _configurationManager.GetByKey(Key);
          ConfigViewModel configViewModel = Mapper.Map<ConfigViewModel>(config);


          configViewModel.Configs = _configurationManager.GetAll();


          return View(configViewModel);
      }

      [HttpPost]
      public ActionResult Edit(ConfigViewModel configViewModel)
      {
          string message = "";
          Config config = Mapper.Map<Config>(configViewModel);
          if (_configurationManager.Update(config))
          {
              message = "Url Updated";
          }
          else
          {
              message = "Url Not Updated";
          }

          ViewBag.Message = message;

          configViewModel.Configs = _configurationManager.GetAll();
          return View(configViewModel);
      }

      [HttpGet]
      public ActionResult Delete(String Key)
      {
          Config config = _configurationManager.GetByKey(Key);
          string message = "";
          if (_configurationManager.Delete(Key))
          {
              message = "Deleted Succsessfully!!";
          }
          else
          {
              message = "Not deleted ";
          }
          ViewBag.Message = message;

          return RedirectToAction("Create");
      }

    }
}