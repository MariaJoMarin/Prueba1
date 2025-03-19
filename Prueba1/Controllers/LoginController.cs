﻿using Firebase.Auth;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Prueba1.Firebase;
using Prueba1.Models;

namespace Prueba1.Controllers
{
    public class LoginController : Controller
    {
        // GET: LoginController
        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> Login(string email, string password)
        {
            try
            {
                UserCredential userCredential = await FirebaseAuthHelper.setFirebaseAuthClient().SignInWithEmailAndPasswordAsync(email, password);
                UserHelper userHelper = new UserHelper();
                UserModel user = await userHelper.getUserInfo(email);

                HttpContext.Session.SetString("userSession", JsonConvert.SerializeObject(user));
                return RedirectToAction("Main", "Home");
            }

            catch {
                return RedirectToAction("Index", "Error");
            }
            
        }

        public IActionResult LogOut(int id)
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Index");
        }

        // GET: LoginController/Details/5
        public IActionResult Details(int id)
        {
            return View();
        }

        // GET: LoginController/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: LoginController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: LoginController/Edit/5
        public IActionResult Edit(int id)
        {
            return View();
        }

        // POST: LoginController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: LoginController/Delete/5
        public IActionResult Delete(int id)
        {
            return View();
        }

        // POST: LoginController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
