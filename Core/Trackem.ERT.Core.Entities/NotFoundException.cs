namespace Trackem.ERT.Core.Entities;
public class NotFoundException : Exception
{
    public NotFoundException(int Id) : base($"No Reaord found with id {Id}")
    {
    }
}