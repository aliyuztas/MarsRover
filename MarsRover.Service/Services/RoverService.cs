using System.Linq;

using MarsRover.Service.Interfaces;
using MarsRover.Service.Models;

namespace MarsRover.Service.Services {
    /// <inheritdoc />
    public class RoverService : IRoverService {
        /// <inheritdoc />
        public string Discover(Rover[] rovers) {
            var lastPositionTexts = string.Empty;

            for (var i = 0; i < rovers.Length; i++) {
                var rover = rovers[i];

                var lastPosition = rover.Positions.Last();

                var lastPositionText = $"{lastPosition.Latitude} {lastPosition.Longitude} {lastPosition.Direction}";

                lastPositionTexts += string.IsNullOrEmpty(lastPositionTexts)
                    ? lastPositionText
                    : " " + lastPositionText;
            }

            return lastPositionTexts;
        }
    }
}