namespace Jewelery.Infrastructure.Exeption.CustomExeptionType
{
    public class J_NotFoundExeption : Exception
    {
        public J_NotFoundExeption() { }
        public J_NotFoundExeption(String mess) : base(mess) { }
        public J_NotFoundExeption(String mess, Exception iner) : base(mess, iner) { }
    }
}
