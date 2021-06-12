using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BookStore.Helpers
{
    public class MyCustomValidationAttribute :ValidationAttribute
    {
        public MyCustomValidationAttribute(string text)
        {
            Text = text;
                
        }
        public string Text { get; set; }
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value !=null)
            {
                string bookname = value.ToString();
                if (bookname.Contains("MVC"))
                {
                    return ValidationResult.Success;
                }
            }

            return new ValidationResult(ErrorMessage??"BookName dosn't contain the desired value");
        }
    }
}
