﻿namespace CarRentingSystem.Controllers
{
    using CarRentingSystem.Data;
    using CarRentingSystem.Models.Home;
    using CarRentingSystem.Services.Statistics;
    using Microsoft.AspNetCore.Mvc;
    using System.Linq;

    public class HomeController : Controller
    {
        private readonly IStatisticsService statistics;
        private readonly CarRentingDbContext data;

        public HomeController(CarRentingDbContext data, IStatisticsService statistics)
        {
            this.statistics = statistics;
            this.data = data;
        }

        public IActionResult Index()
        {
            var cars = this.data
               .Cars
               .OrderByDescending(c => c.Id)
               .Select(c => new CarIndexViewModel
               {
                   Id = c.Id,
                   Brand = c.Brand,
                   Model = c.Model,
                   Year = c.Year,
                   ImageUrl = c.ImageUrl
               })
               .Take(3)
               .ToList();

            var totalStatistics = this.statistics.Total();

            return View(new IndexViewModel
            {
                TotalCars = totalStatistics.TotalCars,
                TotalUsers = totalStatistics.TotalUsers,
                Cars = cars
            });
        }

        public IActionResult Error() => View();
    }
}
