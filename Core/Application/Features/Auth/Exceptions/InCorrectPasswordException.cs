using Application.Bases;

namespace Application.Features.Auth.Exceptions
{
    public class InCorrectPasswordException : BaseExceptions
     {
        public InCorrectPasswordException() : base("Giriş uğursuzdur!")
        {
        }
    }
}
