namespace TravelApp.BusinessLogic.Exceptions;

public class InvalidFilterException : Exception
{
    public InvalidFilterException(string filterType, string filterValue) : base(
        $"Invalid {filterType}: '{filterValue}' not found")
    {
    }
}