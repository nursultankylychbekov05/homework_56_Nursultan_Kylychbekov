using System.ComponentModel.DataAnnotations;

namespace WebApplication1.Models;

public class Brand
{
    public int Id { get; set; }

    [Required(ErrorMessage = "Название бренда обязательно")]
    [Display(Name = "Название бренда")]
    public string Name { get; set; } = string.Empty;

    [Required(ErrorMessage = "Email обязателен")]
    [EmailAddress(ErrorMessage = "Некорректный формат Email")]
    [Display(Name = "Email компании")]
    public string Email { get; set; } = string.Empty;

    [Required(ErrorMessage = "Дата основания обязательна")]
    [DataType(DataType.Date)]
    [ValidFoundedDate]
    [Display(Name = "Дата основания")]
    public DateTime FoundedDate { get; set; } = DateTime.Now;
}