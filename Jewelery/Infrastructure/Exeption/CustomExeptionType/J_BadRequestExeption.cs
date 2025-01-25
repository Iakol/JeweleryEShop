namespace Jewelery.Infrastructure.Exeption.CustomExeptionType
{
    public class J_BadRequestExeption : Exception
    {
        public J_BadRequestExeption() { }
        public J_BadRequestExeption(String mess) : base(mess) { }
        public J_BadRequestExeption(String mess, Exception iner) : base(mess, iner) { }
    }
}
