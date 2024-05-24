

using System.ComponentModel.DataAnnotations;

namespace Exam_Mvcproject.Areas.Admin.ViewModels
{
    public class AdminLoginVM
    {
        [Required]
        [StringLength(maximumLength:10,MinimumLength =10)]
        public string UserName { get; set; }
        [Required]
        [MinLength(8)]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
