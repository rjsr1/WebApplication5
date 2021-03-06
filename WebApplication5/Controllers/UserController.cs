﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

namespace WebApplication5.Controllers
{
    public class UserController : Controller
    {
        private string PathValue = "";
        // GET: User
        public ActionResult Index()
        {
            return View();
        }
        public FileResult Download(string arquivo)
        {
            string pathValue = System.Configuration.ConfigurationManager.AppSettings["relativePath"];
            return File(pathValue+arquivo, "application/zip",arquivo);
        }
        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Login(Models.User user)
        {
            
                if (user.IsValid(user.UserName, user.Password))
                {
                    ViewBag.arquivos = user.Pegar_Arquivos();
                    return View("FilePage");
                }
                else
                {
                 ModelState.AddModelError("User", "Passaporte ou login incorreto");
                return View();
            }
            
        }
        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Index", "Home");
        }
       [Authorize]
       public ActionResult FilePage(Models.User user)
        {
            //buscar files no banco pra mostrar na view.
            ViewBag.arquivos=user.Pegar_Arquivos();
            return View();
        }
        
        [Authorize(Roles ="Admin")]
        public ActionResult AlterarPath(string path)
        {
            PathValue = path;
            ViewBag.add(path);
            return View("FilePage");
        }
    }
}