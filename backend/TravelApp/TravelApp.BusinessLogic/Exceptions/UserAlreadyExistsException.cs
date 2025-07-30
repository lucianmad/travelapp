namespace TravelApp.BusinessLogic.Exceptions;

public class UserAlreadyExistsException : Exception
{
    public UserAlreadyExistsException() : base("User already exists!")
    {
    }
}