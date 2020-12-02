using System;

using MarsRover.Service.Constants;
using MarsRover.Service.Enums;
using MarsRover.Service.Interfaces;

namespace MarsRover.Service.Models {
    /// <summary>
    /// Rover's position.
    /// </summary>
    public class Position {
        #region Ctor

        /// <summary>
        /// Rover's position.
        /// </summary>
        /// <param name="configuration"> Configuration parameters. </param>
        /// <param name="value"> Position string value. Like "1 2 N". </param>
        public Position(IConfiguration configuration, string value) {
            if (string.IsNullOrEmpty(value)) {
                throw new ArgumentNullException(string.Format(MessageConstants.ArgumentCannotBeNull, nameof(value)));
            }

            var positions = value.Split(configuration.PositionSplitChar);

            if (positions.Length != configuration.PositionsLength) {
                throw new Exception(string.Format(MessageConstants.ArrayLengthCannotBeDifferentThanNumber, configuration.PositionsLength));
            }

            int.TryParse(positions[0], out var latitude);

            if (latitude > configuration.MaxLatitude) {
                throw new Exception(string.Format(MessageConstants.LatitudeValueCannotBeGreaterThanNumber, configuration.MaxLatitude));
            }

            if (latitude < configuration.MinLatitude) {
                throw new Exception(string.Format(MessageConstants.LatitudeValueCannotBeLessThanNumber, configuration.MinLatitude));
            }

            Latitude = latitude;

            int.TryParse(positions[1], out var longitude);

            if (longitude > configuration.MaxLongitude) {
                throw new Exception(string.Format(MessageConstants.LongitudeValueCannotBeGreaterThanNumber, configuration.MaxLongitude));
            }

            if (longitude < configuration.MinLongitude) {
                throw new Exception(string.Format(MessageConstants.LongitudeValueCannotBeLessThanNumber, configuration.MinLongitude));
            }

            Longitude = longitude;

            Direction = GetDirection(positions[2]);
        }

        /// <summary>
        /// Rover's position.
        /// </summary>
        /// <param name="configuration"> Configuration parameters. </param>
        /// <param name="latitude"> Latitude. </param>
        /// <param name="longitude"> Longitude. </param>
        /// <param name="direction"> Geographical direction. E, N, W, S. </param>
        /// <param name="instruction"> Move instruction. Left(L), Right(R), Move(M). </param>
        public Position(IConfiguration configuration, int latitude, int longitude, Direction direction, Instruction instruction) {
            if (latitude > configuration.MaxLatitude) {
                throw new Exception(string.Format(MessageConstants.LatitudeValueCannotBeGreaterThanNumber, configuration.MaxLatitude));
            }

            if (latitude < configuration.MinLatitude) {
                throw new Exception(string.Format(MessageConstants.LatitudeValueCannotBeLessThanNumber, configuration.MinLatitude));
            }

            Latitude = GetLatitude(latitude, direction, instruction);

            if (longitude > configuration.MaxLongitude) {
                throw new Exception(string.Format(MessageConstants.LongitudeValueCannotBeGreaterThanNumber, configuration.MaxLongitude));
            }

            if (longitude < configuration.MinLongitude) {
                throw new Exception(string.Format(MessageConstants.LongitudeValueCannotBeLessThanNumber, configuration.MinLongitude));
            }

            Longitude = GetLongitude(longitude, direction, instruction);

            Direction = GetDirection(direction, instruction);
        }

        #endregion

        #region Properties     

        /// <summary>
        /// Position's latitude.
        /// </summary>
        public int Latitude { get; }

        /// <summary>
        /// Position's longitude.
        /// </summary>
        public int Longitude { get; }

        /// <summary>
        /// Position's direction. Geographical direction. E, N, W, S.
        /// </summary>
        public Direction Direction { get; }

        #endregion

        #region Private Methods

        /// <summary>
        /// Get new latitude value by latitude, direction and instruction.
        /// </summary>
        /// <param name="latitude"> Latitude. </param>
        /// <param name="direction"> Direction. </param>
        /// <param name="instruction"> Instruction. </param>
        /// <returns></returns>
        private int GetLatitude(int latitude, Direction direction, Instruction instruction) {
            if (instruction != Instruction.M) {
                return latitude;
            }

            int moveValue;

            switch (direction) {
                case Direction.E:
                    moveValue = 1;
                    break;
                case Direction.W:
                    moveValue = -1;
                    break;
                default:
                    moveValue = 0;
                    break;
            }

            return latitude + moveValue;
        }

        /// <summary>
        /// Get new longitude value by longitude, direction and instruction.
        /// </summary>
        /// <param name="longitude"></param>
        /// <param name="direction"></param>
        /// <param name="instruction"></param>
        /// <returns></returns>
        private int GetLongitude(int longitude, Direction direction, Instruction instruction) {
            if (instruction != Instruction.M) {
                return longitude;
            }

            int moveValue;

            switch (direction) {
                case Direction.N:
                    moveValue = 1;
                    break;
                case Direction.S:
                    moveValue = -1;
                    break;
                default:
                    moveValue = 0;
                    break;
            }

            return longitude + moveValue;
        }

        /// <summary>
        /// Get direction enum value by direction text.
        /// </summary>
        /// <param name="directionText"> Direction text. </param>
        /// <returns> Direction enum. </returns>
        private Direction GetDirection(string directionText) {
            Enum.TryParse(directionText, out Direction direction);

            return direction;
        }

        /// <summary>
        /// Get new direction enum value by direction enum and instruction.
        /// </summary>
        /// <param name="direction"> Direction enum. </param>
        /// <param name="instruction"> Instruction. </param>
        /// <returns> Direction enum. </returns>
        private Direction GetDirection(Direction direction, Instruction instruction) {
            var degrees = direction.GetHashCode() + instruction.GetHashCode();

            if (degrees >= 360) {
                degrees %= 360;
            }

            if (degrees < 0) {
                degrees += 360;
            }

            return (Direction)degrees;
        }

        #endregion
    }
}