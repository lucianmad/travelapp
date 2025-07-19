namespace TravelApp.BusinessLogic.Exceptions;

public class EntityNotFoundException : Exception
{
    public int Id { get; set; }
    public string EntityName { get; set; }
    public EntityNotFoundException(string entityName, int id) : base($"{entityName} with id {id} not found!")
    {
        Id = id;
        EntityName = entityName;
    }
}