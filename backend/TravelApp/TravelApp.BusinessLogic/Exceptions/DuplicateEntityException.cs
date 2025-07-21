namespace TravelApp.BusinessLogic.Exceptions;

public class DuplicateEntityException : Exception
{
    public DuplicateEntityException(string entityName, string name) : base($"{entityName} with name {name} already exists!")
    {
    }
}