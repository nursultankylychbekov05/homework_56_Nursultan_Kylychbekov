using System.ComponentModel.DataAnnotations;

namespace WebApplication1.Models;

public class Review
{
    public int Id { get; set; }

    [Required(ErrorMessage = "Имя автора обязательно для заполнения")]
    public string AuthorName { get; set; } = string.Empty;

    [Required(ErrorMessage = "Введите текст отзыва")]
    public string Text { get; set; } = string.Empty;

    public int Rating { get; set; }
    public int PhoneId { get; set; }
    public Phone? Phone { get; set; }
}