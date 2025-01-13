namespace CarRentingSystem.Models.Cars
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using static Data.DataConstants;

    public class AddCarFormModel   // : IValidatableObject
    {
        [Required]
        [MinLength(CarBrandMinLenght)]
        [StringLength(CarBrandMaxLenght, MinimumLength = CarBrandMinLenght)]
        public string Brand { get; init; }

        [Required]
        [StringLength(CarModelMaxLenght, MinimumLength = CarModelMinLenght)]
        public string Model { get; init; }

        [Required]
        [StringLength(
            int.MaxValue,
            MinimumLength = CarDescriptionlMinLenght,
            ErrorMessage = "The field Description must be a string with a minimum length of {1}")]
        public string Description { get; init; }

        [Display(Name = "Image URL")]
        [Required]
        [Url]
        public string ImageUrl { get; init; }

        [Range(CarYearlMinValue, CarYearlMaxValue)]
        public int Year { get; init; }

        [Display(Name = "Category")]
        public int CategoryId { get; init; }

        public IEnumerable<CarCategoryViewModel> Categories { get; set; }

        public bool Whatever { get; init; }

        //public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        //{
        //    if (this.Brand == "Mercedes" && this.Model == "300D")
        //    {
        //        yield return new ValidationResult("300D is noat Mercedes?!");
        //    }
        //}
    }
}
