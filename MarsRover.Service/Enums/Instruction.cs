namespace MarsRover.Service.Enums {
    /// <summary>
    /// Instruction enum.
    /// </summary>
    public enum Instruction {
        /// <summary>
        /// Left. 90 degree rotation in the positive direction.
        /// </summary>
        L = 90,

        /// <summary>
        /// Right. 90 degree rotation in the negative direction.
        /// </summary>
        R = -90,

        /// <summary>
        /// Move. 1 unit of forward movement in the specified direction.
        /// </summary>
        M = 0
    }
}