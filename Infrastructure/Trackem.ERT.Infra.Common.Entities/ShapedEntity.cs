namespace Trackem.ERT.Infra.Common.Entities;

public class ShapedEntity
{
    public ShapedEntity()
    {
        Entity = new Entity();
    }

    public int Id { get; set; }
    public long SystemId { get; set; }
    public Entity Entity { get; set; }
}