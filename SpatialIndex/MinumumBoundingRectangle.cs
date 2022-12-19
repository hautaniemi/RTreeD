namespace RTreeD
{
    /// <summary>
    /// The 3D rectangle of minumum size that covers all geometries within a node or spatial object
    /// </summary>
    public readonly record struct MinumumBoundingRectangle
    {
        public readonly double MinX { get; }
        public readonly double MinY { get; }
        public readonly double MinZ { get; }
        public readonly double MaxX { get; }
        public readonly double MaxY { get; }
        public readonly double MaxZ { get; }

        /// <summary>
        /// Explict constructor
        /// </summary>
        /// <param name="minX"></param>
        /// <param name="minY"></param>
        /// <param name="minZ"></param>
        /// <param name="maxX"></param>
        /// <param name="maxY"></param>
        /// <param name="maxZ"></param>
       public MinumumBoundingRectangle(double minX, double minY, double minZ, double maxX, double maxY, double maxZ)
        {
            this.MinX = minX;
            this.MinY = minY;
            this.MinZ = minZ;
            this.MaxX = maxX;
            this.MaxY = maxY;
            this.MaxZ = maxZ;
        }
        /// <summary>
        /// Creates a new Minumum Bounding Rectangle that covers all Minumum Bounding Rectangles in items
        /// </summary>
        /// <param name="items"></param>
        public MinumumBoundingRectangle(IEnumerable<MinumumBoundingRectangle> items) 
        {
            this.MinX = double.PositiveInfinity;
            this.MinY = double.PositiveInfinity;
            this.MinZ = double.PositiveInfinity;
            this.MaxX = double.NegativeInfinity;
            this.MaxY = double.NegativeInfinity;
            this.MaxZ = double.NegativeInfinity;

            foreach(var item in items)
            {
                this.MinX = Math.Min(MinX, item.MinX);
                this.MinY = Math.Min(MinY, item.MinY);
                this.MinZ = Math.Min(MinZ, item.MinZ);
                this.MaxX = Math.Max(MaxX, item.MaxX);
                this.MaxY = Math.Max(MaxY, item.MaxY);
                this.MaxZ = Math.Max(MaxZ, item.MaxZ);
            }
        }
        /// <summary>
        /// Determines whether <paramref name="other"/> intersects this MinumumBoundingRectangle
        /// </summary>
        /// <param name="other"></param>
        public bool Intersects(in MinumumBoundingRectangle other) =>
           this.MinX <= other.MaxX &&
           this.MinY <= other.MaxY &&
           this.MinZ <= other.MaxZ &&
           this.MaxX >= other.MinX &&
           this.MaxY >= other.MinY &&
           this.MaxZ >= other.MinZ;

    }
}