using System;

using MarsRover.Service.Enums;
using MarsRover.Service.Interfaces;
using MarsRover.Service.Models;
using MarsRover.Service.Services;

using Moq;

using NUnit.Framework;

namespace MarsRover.Test {
    /// <summary>
    /// 
    /// </summary>
    [TestFixture]
    public class RoverServiceTest {
        private Mock<IConfiguration> mockConfiguration;

        private IRoverService roverService;

        [SetUp]
        public void Setup() {
            mockConfiguration = new Mock<IConfiguration>();

            mockConfiguration.Setup(o => o.PositionSplitChar).Returns(' ');
            mockConfiguration.Setup(o => o.PositionsLength).Returns(3);
            mockConfiguration.Setup(o => o.MaxLatitude).Returns(5);
            mockConfiguration.Setup(o => o.MaxLongitude).Returns(5);
            mockConfiguration.Setup(o => o.MinLatitude).Returns(0);
            mockConfiguration.Setup(o => o.MinLongitude).Returns(0);

            roverService = new RoverService();
        }

        [Test]
        public void Discover_ShouldPass_WhenPositionAndInstructions() {
            var rovers = new[]
            {
                new Rover(mockConfiguration.Object,"1 2 N", "LMLMLMLMM"),
                new Rover(mockConfiguration.Object,"3 3 E", "MMRMMRMRRM")
            };

            var result = roverService.Discover(rovers);

            Assert.AreEqual(result, "1 3 N 5 1 E");
        }

        [Test]
        public void Position_ShouldFail_WhenPositionIsEmpty() {
            Assert.Throws<ArgumentNullException>(() => new Position(mockConfiguration.Object, string.Empty));
        }

        [Test]
        public void Position_ShouldFail_WhenPositionLengthDifferentFromSpecificNumber() {
            Assert.Throws<Exception>(() => new Position(mockConfiguration.Object, "1 2"));
        }

        [Test]
        public void Position_ShouldFail_WhenPositionLatitudeIsGreaterThanSpecificNumber() {
            Assert.Throws<Exception>(() => new Position(mockConfiguration.Object, "6 2 N"));
        }

        [Test]
        public void Position_ShouldFail_WhenPositionLatitudeIsGreaterThanSpecificNumber_MultiParameters() {
            Assert.Throws<Exception>(() => new Position(mockConfiguration.Object, 6, 2, Direction.N, Instruction.L));
        }

        [Test]
        public void Position_ShouldFail_WhenPositionLatitudeIsLessThanSpecificNumber() {
            Assert.Throws<Exception>(() => new Position(mockConfiguration.Object, "-1 2 N"));
        }

        [Test]
        public void Position_ShouldFail_WhenPositionLatitudeIsLessThanSpecificNumber_MultiParameters() {
            Assert.Throws<Exception>(() => new Position(mockConfiguration.Object, -1, 2, Direction.N, Instruction.L));
        }

        [Test]
        public void Position_ShouldFail_WhenPositionLongitudeIsGreaterThanSpecificNumber() {
            Assert.Throws<Exception>(() => new Position(mockConfiguration.Object, "1 6 N"));
        }

        [Test]
        public void Position_ShouldFail_WhenPositionLongitudeIsGreaterThanSpecificNumber_MultiParameters() {
            Assert.Throws<Exception>(() => new Position(mockConfiguration.Object, 1, 6, Direction.N, Instruction.L));
        }

        [Test]
        public void Position_ShouldFail_WhenPositionLongitudeIsLessThanSpecificNumber() {
            Assert.Throws<Exception>(() => new Position(mockConfiguration.Object, "1 -1 N"));
        }

        [Test]
        public void Position_ShouldFail_WhenPositionLongitudeIsLessThanSpecificNumber_MultiParameters() {
            Assert.Throws<Exception>(() => new Position(mockConfiguration.Object, 1, -1, Direction.N, Instruction.L));
        }
    }
}
