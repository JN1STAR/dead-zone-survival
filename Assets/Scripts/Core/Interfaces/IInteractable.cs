namespace DeadZone.Core.Interfaces
{
    /// <summary>
    /// Interface for objects that can be interacted with by the player.
    /// </summary>
    public interface IInteractable
    {
        /// <summary>
        /// Gets the interaction prompt text.
        /// </summary>
        string InteractionPrompt { get; }

        /// <summary>
        /// Gets the interaction distance in units.
        /// </summary>
        float InteractionDistance { get; }

        /// <summary>
        /// Called when the player interacts with this object.
        /// </summary>
        void OnInteract();

        /// <summary>
        /// Check if this object can currently be interacted with.
        /// </summary>
        bool CanInteract { get; }
    }
}
