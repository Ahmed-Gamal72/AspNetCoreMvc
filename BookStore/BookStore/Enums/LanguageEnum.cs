using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BookStore.Enums
{
    public enum LanguageEnum
    {
        [Display(Name ="English Language")]
        English=10,
        [Display(Name = "Arabic Language")]
        Arabic = 11,
        [Display(Name = "France Language")]
        France = 12,
        [Display(Name = "Hindi Language")]
        Hindi = 13,
        [Display(Name = "German Language")]
        German = 14
    }
}
