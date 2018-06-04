using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using MetanitExampleCoreMVC.CustomAttributes;

namespace MetanitExampleCoreMVC.Models
{
    [NamePasswordEqual]
    public class Person : IValidatableObject
    {
        [Display(Name = "Имя и фамилия")]
        [Required(ErrorMessage = "Не указано имя")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "Длина строки должна быть от 3 до 50 символов")]
        [PersonName(new string[] { "Tom", "Sam", "Alice" }, ErrorMessage = "Недопустимое имя")]
        public string Name { get; set; }

        [UIHint("Url")]
        [Display(Name = "Электронная почта")]
        [Required(ErrorMessage = "Не указан электронный адрес")]
        [EmailAddress(ErrorMessage = "Некорректный адрес")]
        [Remote(action: "CheckEmail", controller: "Validation", ErrorMessage = "Email уже используется")]
        public string Email { get; set; }

        [DataType(DataType.Password)]
        [ScaffoldColumn(false)]
        [Display(Name = "Пароль")]
        [Required]
        public string Password { get; set; }

        [ScaffoldColumn(false)]
        [Compare("Password", ErrorMessage = "Пароли не совпадают")]
        public string PasswordConfirm { get; set; }

        [Display(Name = "Возраст")]
        [Required]
        [Range(1, 110, ErrorMessage = "Недопустимый возраст")]
        public int Age { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            List<ValidationResult> errors = new List<ValidationResult>();

            if (string.IsNullOrWhiteSpace(this.Name))
            {
                errors.Add(new ValidationResult("Введите имя!", new List<string>() { "Name" }));
            }
            if (string.IsNullOrWhiteSpace(this.Email))
            {
                errors.Add(new ValidationResult("Введите электронный адрес!"));
            }
            if (this.Age < 0 || this.Age > 120)
            {
                errors.Add(new ValidationResult("Недопустимый возраст!"));
            }

            return errors;
        }
    }
}
