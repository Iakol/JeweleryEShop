namespace Jewelery.Infrastructure.Exeption.CustomExeptionType
{
    public class J_ServiceTemporarilyUnavailableExeption : Exception
    {
        public J_ServiceTemporarilyUnavailableExeption() { }
        public J_ServiceTemporarilyUnavailableExeption(String mess) :base(mess) { }
        public J_ServiceTemporarilyUnavailableExeption(String mess, Exception iner): base(mess, iner) { }
    }
}
