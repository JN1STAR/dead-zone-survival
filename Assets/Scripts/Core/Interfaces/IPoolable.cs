namespace DeadZone.Core.Interfaces
{
    /// <summary>
    /// Interface for objects that can be pooled and reused.
    /// </summary>
    public interface IPoolable
    {
        /// <summary>
        /// Called when the object is taken from the pool.
        /// </summary>
        void OnSpawn();

        /// <summary>
        /// Called when the object is returned to the pool.
        /// </summary>
        void OnDespawn();

        /// <summary>
        /// Gets whether this object is currently in use.
        /// </summary>
        bool IsActive { get; set; }
    }
}
