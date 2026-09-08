using WebApplication1.Models;

namespace WebApplication1.ViewModels;

public class PhonesWithOrders
{
    public List<Order> Orders { get; set; }
    public List<Phone> Phones { get; set; }
}