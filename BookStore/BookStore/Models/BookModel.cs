using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using BookStore.Enums;
using Microsoft.AspNetCore.Http;

namespace BookStore.Models
{
    public class BookModel
    {
        public int Id { get; set; }
        [StringLength(100, MinimumLength = 5)]
        [Required(ErrorMessage = "Please Enter the Title of the Book")]
        public string Title { get; set; }
        [Required(ErrorMessage = "Please Enter the Author Name")]
        public string Author { get; set; }

        [StringLength(500)]
        public string Description { get; set; }
        public string Category { get; set; }
        [Required (ErrorMessage ="Please Choose the Language of your book")]
        public int LanguageId { get; set; }
        public string Language { get; set; }

        [Display (Name ="Total Pages Of Book")]
        [Required(ErrorMessage = "Please Enter the Total pages")]
        public int? TotalPages { get; set; }
        [Display(Name ="Choose the cover photo of your book")]
        [Required]
        public IFormFile CoverPhoto { get; set; }
        public string CoverImageUrl { get; set; }

        [Display(Name = "Choose the gallery Images of your book")]
        [Required]
        public IFormFileCollection GalleryFiles { get; set; }

        public List<GalleryModel> Gallery { get; set; }

        [Display(Name = "Upload yur book in pdf format")]
        [Required]
        public IFormFile BookPdf { get; set; }
        public string BookPdfUrl { get; set; }

    }
}
