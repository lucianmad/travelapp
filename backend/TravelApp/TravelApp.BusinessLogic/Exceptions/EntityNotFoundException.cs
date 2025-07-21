namespace TravelApp.BusinessLogic.Exceptions;

public class EntityNotFoundException : Exception
{
    public EntityNotFoundException(string entityName, int id) : base($"{entityName} with id {id} not found!")
    {
    }
}