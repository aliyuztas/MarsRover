using MarsRover.Service.Models;

namespace MarsRover.Service.Interfaces {
    /// <summary>
    /// Rover service.
    /// </summary>
    public interface IRoverService {
        /// <summary>
        /// Rovers discovers the matrix.
        /// </summary>
        /// <param name="rovers"> Rover array. </param>
        /// <returns> The end positions of the rovers. </returns>
        string Discover(Rover[] rovers);
    }
}