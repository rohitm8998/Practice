using Trackem.ERT.Infra.Common.Entities;

namespace Trackem.ERT.Infra.Contracts;
public interface IDataShaper<T>
{
    IEnumerable<ShapedEntity> ShapeData(IEnumerable<T> entities, string fieldsString);
    ShapedEntity ShapeData(T entity, string fieldsString);
}