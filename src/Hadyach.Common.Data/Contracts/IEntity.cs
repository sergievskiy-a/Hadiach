namespace Hadyach.Common.Data.Contracts
{
    /// <summary>
    /// Entity with Id
    /// </summary>
    /// <typeparam name="TId">Type of identitifer</typeparam>
    public interface IEntity<TId>
    {
        TId Id { get; set; }
    }
}
