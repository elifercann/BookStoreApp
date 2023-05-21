namespace Entities.DTOs
{
    //record typelar readonly oluşturulur yani bu aşağıdaki değerlere set değerini init vermelisin
    //public record BookDtoForUpdate
    //{
    //    public int Id { get; init; }
    //    public String Title { get; init; }
    //    public decimal Price { get; init; }
    //}
    //yukarıdaki kodla aynı işi yapar
    public record BookDtoForUpdate(int Id,String Title,decimal price);
}
