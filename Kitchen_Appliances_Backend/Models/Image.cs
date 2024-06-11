namespace Kitchen_Appliances_Backend.Models;

public partial class Image
{
    public int Id { get; set; }

    public string Url { get; set; }

    public int ProductId { get; set; }

    public virtual Product Product { get; set; } = null!;
}
