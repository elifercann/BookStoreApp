namespace Entities.Exceptions
{
    //katılıma kapatıldı sınıf
    public sealed class BookNotFoundException : NotFoundException
    {
        public BookNotFoundException(int id) : base($"The book with id : {id} could not found")
        {
        }
    }
}
