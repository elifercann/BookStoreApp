using System.ComponentModel.DataAnnotations;

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
    public record BookDtoForUpdate : BookDtoForManipulation
    {
        [Required]
        public int Id { get; init; }
    }
}
