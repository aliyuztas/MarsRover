using System;

using MarsRover.Service.Enums;
using MarsRover.Service.Interfaces;

namespace MarsRover.Service.Models {
    /// <summary>
    /// Rover.
    /// </summary>
    public class Rover {
        #region Ctor

        /// <summary>
        /// Rover.
        /// </summary>
        /// <param name="configuration"> Configuration parameters. </param>
        /// <param name="startPosition"> Start position text. </param>
        /// <param name="instructions"> Instructions text. </param>
        public Rover(IConfiguration configuration, string startPosition, string instructions) {
            this.Positions = GetPositions(configuration, startPosition, instructions);
        }

        #endregion

        #region Properties

        /// <summary>
        /// Rover's positions.
        /// </summary>
        public Position[] Positions { get; }

        #endregion

        #region Private Methods

        /// <summary>
        /// Get rover's positions by start position text and instructions text.
        /// </summary>
        /// <returns> Rovers position array. </returns>
        private Position[] GetPositions(IConfiguration configuration, string startPosition, string instructions) {
            var positions = new Position[instructions.Length + 1];

            positions[0] = new Position(configuration, startPosition);

            for (var i = 1; i < positions.Length; i++) {
                var previousPosition = positions[i - 1];

                var instructionIndex = i - 1;

                var instruction = GetInstruction(instructions, instructionIndex);

                positions[i] = new Position(configuration, previousPosition.Latitude, previousPosition.Longitude, previousPosition.Direction, instruction);
            }

            return positions;
        }

        /// <summary>
        /// Get rover's instruction.
        /// </summary>
        /// <param name="instructions"> Instructions text. </param>
        /// <param name="instructionIndex"> instruction array index. </param>
        /// <returns> Instruction. </returns>
        private Instruction GetInstruction(string instructions, int instructionIndex) {
            Enum.TryParse(instructions[instructionIndex].ToString(), out Instruction instruction);

            return instruction;
        }

        #endregion
    }
}
