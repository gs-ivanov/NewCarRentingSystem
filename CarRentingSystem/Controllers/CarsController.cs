namespace CarRentingSystem.Controllers
{
    using CarRentingSystem.Data;
    using CarRentingSystem.Data.Models;
    using CarRentingSystem.Models.Cars;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using System.Collections.Generic;
    using System.Linq;

    using FileSystem = System.IO.File;

    public class CarsController : Controller
    {
        private readonly CarRentingDbContext data;

        public CarsController(CarRentingDbContext data)
        {
            this.data = data;
        }

        // GET -> Browser renders Form
        public IActionResult Add() => View(new AddCarFormModel
        {
            Categories = this.GetCarCategories()
        });

        [HttpPost]
        public IActionResult Add(AddCarFormModel car, IFormFile image)//Ako sa mnogo failove: IEnumerable<IFormFile>
        {
            //if (image==null|| image.Length>2*1024*1024)
            //{
            //    this.ModelState.AddModelError("Image", "The image is not valid. It is required and it should be less than 2 MB");
            //}

            ////Работи - Копира снимка в руут директорията на приложението.
            //image.CopyTo(FileSystem.OpenWrite($"C:/Users/George/Desktop/CSharp/PROJECTS/NewCarRentingSystem/CarRentingSystem/wwwroot/images/{image.FileName}"));

            if (!this.data.Categories.Any(c=>c.Id==car.CategoryId))
            {
                this.ModelState.AddModelError(nameof(car.CategoryId), "Category does not exist");
            }

            if (!ModelState.IsValid)
            {
                car.Categories = this.GetCarCategories();
                return View(car);
            }

            var carData = new Car
            {
                Brand = car.Brand,
                Model = car.Model,
                ImageUrl = car.ImageUrl,
                Description = car.Description,
                Year = car.Year,
                CategoryId = car.CategoryId
            };

            this.data.Add(carData);

            this.data.SaveChanges();

            return RedirectToAction("Index", "Home");
        }

        private IEnumerable<CarCategoryViewModel> GetCarCategories()
            => this.data
            .Categories
            .Select(c => new CarCategoryViewModel
            {
                Id = c.Id,

                Name = c.Name
            })
            .ToList();
    }
}
