﻿using System.Security.Claims;
using MyCinema.Data;
using MyCinema.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MyCinema.Services;
using MyCinema.Services.IServices;
using System.Text.Json;

namespace MyCinema.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminController : Controller
    {
        private readonly IAdminService _adminService;
        public AdminController(IAdminService adminService)
        {
            _adminService = adminService;
        }
        public async Task<IActionResult> Index()
        {
            var settings =await _adminService.GetAppSettingsAsync();
            return View(settings);
        }

        [HttpPost]
        public async Task<IActionResult> Update(string key, string value)
        {
            await _adminService.UpdateSetting(key,value);
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> InsertLanguagesInDb()
        {
            await _adminService.InsertLanguagesInDB();
            return RedirectToAction("Index");
        }
        public async Task<IActionResult> InsertGenresInDb()
        {
            await _adminService.InsertGenresInDB();
            return RedirectToAction("Index");
        }
        public async Task<IActionResult> SeedDb()
        {
            await _adminService.SeedDb();
            return RedirectToAction("Index");
        }
    }
}
