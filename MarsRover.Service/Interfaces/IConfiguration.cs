namespace MarsRover.Service.Interfaces {
    /// <summary>
    /// Configuration.
    /// </summary>
    public interface IConfiguration {
        /// <summary>
        /// Position split char.
        /// </summary>
        public char PositionSplitChar { get; set; }

        /// <summary>
        /// Position array length.
        /// </summary>
        public int PositionsLength { get; set; }

        /// <summary>
        /// Max latitude value.
        /// </summary>
        public int MaxLatitude { get; set; }

        /// <summary>
        /// Min latitude value.
        /// </summary>
        public int MinLatitude { get; set; }

        /// <summary>
        /// Max longitude value.
        /// </summary>
        public int MaxLongitude { get; set; }

        /// <summary>
        /// Min longitude value.
        /// </summary>
        public int MinLongitude { get; set; }
    }
}