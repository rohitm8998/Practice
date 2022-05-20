namespace Trackem.ERT.Infra.Common.Entities.LinkModels;
public class LinkResourceBase
{
    public LinkResourceBase()
    {
    }
    public List<Link> Links { get; set; } = new List<Link>();
}