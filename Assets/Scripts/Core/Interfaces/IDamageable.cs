namespace DeadZone.Core.Interfaces
{
    /// <summary>
    /// Interface for objects that can take damage.
    /// </summary>
    public interface IDamageable
    {
        /// <summary>
        /// Apply damage to this object.
        /// </summary>
        /// <param name="damage">Amount of damage to apply.</param>
        /// <param name="damageType">Type of damage being applied.</param>
        void TakeDamage(float damage, DamageType damageType = DamageType.Normal);

        /// <summary>
        /// Current health of the object.
        /// </summary>
        float CurrentHealth { get; }

        /// <summary>
        /// Maximum health of the object.
        /// </summary>
        float MaxHealth { get; }

        /// <summary>
        /// Check if the object is alive.
        /// </summary>
        bool IsAlive { get; }
    }

    /// <summary>
    /// Types of damage that can be applied.
    /// </summary>
    public enum DamageType
    {
        Normal,
        Headshot,
        Critical,
        Fire,
        Explosive
    }
}
