namespace Jewelery.Infrastructure.Exeption.CustomExeptionType
{
    public class J_ForbiddenExeption : Exception
    {
        public J_ForbiddenExeption() { }
        public J_ForbiddenExeption(String mess) : base(mess) { }
        public J_ForbiddenExeption(String mess, Exception iner) : base(mess, iner) { }
    }
}
