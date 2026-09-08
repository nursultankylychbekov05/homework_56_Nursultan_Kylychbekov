using System.ComponentModel.DataAnnotations;

namespace WebApplication1.Models;

public class Phone
{
    public int Id { get; set; }

    [Required(ErrorMessage = "Название телефона обязательно")]
    [Display(Name = "Название")]
    public string Name { get; set; } = string.Empty;

    [Required(ErrorMessage = "Производитель обязателен")]
    [Display(Name = "Производитель")]
    public string Company { get; set; } = string.Empty;

    [Required(ErrorMessage = "Укажите цену")]
    [Range(1, 1000000, ErrorMessage = "Цена должна быть больше 0")]
    [Display(Name = "Цена ($)")]
    public int Price { get; set; }

    [Required(ErrorMessage = "Описание обязательно")]
    [Display(Name = "Описание")]
    public string Description { get; set; } = string.Empty;

    [Required(ErrorMessage = "Ссылка на изображение обязательна")]
    [Display(Name = "Ссылка на фото")]
    public string ImageUrl { get; set; } = string.Empty;

    public List<Review> Reviews { get; set; } = new();
}