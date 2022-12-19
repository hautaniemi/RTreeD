namespace RTreeD
{
    /// <summary>
    /// A node in an R-tree that contains other nodes or spatial objects (if a Leaf node)
    /// </summary>
    internal class Node : ISpatialData
    {
        private readonly MinumumBoundingRectangle _mbr;

        internal Node(List<ISpatialData> items, int height)
        {
            this.Height = height;
            this.Items = items;
            _mbr = new MinumumBoundingRectangle(items.Select(i => i.MBR));
        }

        
        internal readonly List<ISpatialData> Items;

        /// <summary>
        /// The nodes or spatial objects contained by this <see cref="Node"/>
        /// </summary>
        internal IReadOnlyList<ISpatialData> Children => Items;

        /// <summary>
        /// The height of the node in the <see cref="RTree"/>
        /// </summary>
        /// <remarks>
        /// A node containing spatial objects has a <see cref="Height"/> of 1.
        /// </remarks>
        public int Height { get; }

        /// <summary>
        /// Determines if the current <see cref="Node"/> is a leaf node and thus contains spatial objects.
        /// </summary>
        public bool IsLeaf => Height == 1;
        /// <summary>
        /// The Minumum Bounding Rectangle of all the objects contained under the current <see cref="Node"/>
        /// </summary>
        public ref readonly MinumumBoundingRectangle MBR => ref _mbr;
    }
}
