namespace Ben.CandyHouse
{
    /// <summary>
    /// The types of operations that can be executed by a Sesame.
    /// </summary>
    public enum ControlOperation
    {
        /// <summary>
        /// An unused operation type that just indicates that and invalid <see cref="ControlOperation"/> value was provided.
        /// </summary>
        Unknown,

        /// <summary>
        /// Lock the Sesame device.
        /// </summary>
        Lock,

        /// <summary>
        /// Unlock the Sesame device.
        /// </summary>
        Unlock,
    }
}
