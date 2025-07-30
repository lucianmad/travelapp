namespace TravelApp.BusinessLogic.Exceptions;

public class InvalidCredentialsException : Exception
{
    public InvalidCredentialsException() : base("Invalid credentials!")
    {
    }
}