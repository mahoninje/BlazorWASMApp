using System.ComponentModel.DataAnnotations;

namespace BlazorWASMApp.Web.Dto
{
    /// <summary>
    /// Дто для формы логина
    /// </summary>
    public class LoginDto
    {
        [Required]
        public string Login { get; set; }

        [Required]
        public string Password { get; set; }
    }
}
