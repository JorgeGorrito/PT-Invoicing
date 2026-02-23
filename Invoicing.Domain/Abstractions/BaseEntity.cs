namespace Invoicing.Domain.Abstractions;

public abstract class BaseEntity<TId> : IEquatable<BaseEntity<TId>>
{
    public TId Id { get; protected set; } = default!;
    
    protected BaseEntity(TId id)
    {
        Id = id;
    }

    protected BaseEntity() { }

    public override bool Equals(object? obj)
    {
        // Si es nulo o no es una entidad, no son iguales
        if (obj is not BaseEntity<TId> other)
            return false;

        // Si es la misma referencia de memoria, son iguales
        if (ReferenceEquals(this, other))
            return true;
        
        // Si el ID es el default (ej: 0), no son iguales (son entidades transitorias)
        if (EqualityComparer<TId>.Default.Equals(Id, default))
            return false;

        // Son iguales si tienen el mismo ID
        return EqualityComparer<TId>.Default.Equals(Id, other.Id);
    }

    public bool Equals(BaseEntity<TId>? other)
    {
        if (other is null) return false;
        if (ReferenceEquals(this, other)) return true;
        if (EqualityComparer<TId>.Default.Equals(Id, default)) return false;

        return EqualityComparer<TId>.Default.Equals(Id, other.Id);
    }

    public override int GetHashCode()
    {
        return Id?.GetHashCode() ?? 0;
    }

    public static bool operator ==(BaseEntity<TId>? left, BaseEntity<TId>? right)
    {
        if (left is null && right is null) return true;
        if (left is null || right is null) return false;
        return left.Equals(right);
    }   

    public static bool operator !=(BaseEntity<TId>? left, BaseEntity<TId>? right)
    {
        return !(left == right);
    }
}  