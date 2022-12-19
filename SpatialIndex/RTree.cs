using RTreeD;

namespace RTreeD
{
    /// <summary>
    /// An R-Tree implementation for 3D spatial indexing
    /// </summary>
    public class RTree
    {
        private enum SortDimension { X, Y, Z };

        private readonly Node _root;

        private readonly int _maxEntries;
        /// <summary>
        /// The number of spatial objects indexed in the current <see cref="RTree"/>
        /// </summary>
        public int Count { get; private set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="RTree"/> with <paramref name="items"/>
        /// and <paramref name="maxEntries"/> of elements per node.
        /// </summary>
        /// <param name="items"></param>
        /// <param name="maxEntries"></param>
        public RTree(IEnumerable<ISpatialData> items, int maxEntries)
        {
            _maxEntries = maxEntries;
            Count = 0;
            _root = BulkLoad(items);
        }

        private Node BulkLoad(IEnumerable<ISpatialData> items) 
        {
            // Build the tree from top down using the OMT algorithm
            // http://ftp.informatik.rwth-aachen.de/Publications/CEUR-WS/Vol-74/files/FORUM_18.pdf
            var data = items.ToArray();
            int treeHeight = (int)Math.Ceiling(Math.Log(data.Length, _maxEntries));
            int rootMaxEntries = (int)Math.Ceiling(data.Length / Math.Pow(_maxEntries, treeHeight - 1));
            return CreateNode(data, treeHeight, rootMaxEntries, SortDimension.X);
        }

        private Node CreateNode(ISpatialData[] data, int height, int maxEntries, SortDimension sortDimension) 
        {
            // Create a leaf node if the items will fit
            if (data.Length <= maxEntries)
            {
                Count += data.Length;
                return new Node(data.ToList(), 1);
            }
            // Each level of the tree is sorted by a different spatial dimension in rotation
            var sortedData = SortByDimension(data, sortDimension);
            var nextSortDimension = GetNextSortDimension(sortDimension);
            var children = new List<ISpatialData>(maxEntries);

            // Split this level into nodes, packing all but the last with maxEntries of items
            foreach (var chunk in sortedData.Chunk(maxEntries))
            {
                children.Add(CreateNode(chunk, height - 1, _maxEntries, nextSortDimension)); ;
            }

            return new Node(children, height);
        }

        private static SortDimension GetNextSortDimension(SortDimension current)
        {
            return current switch
            {
                SortDimension.X => SortDimension.Y,
                SortDimension.Y => SortDimension.Z,
                SortDimension.Z => SortDimension.X,
                _ => throw new Exception("Unrecognised current SortDimension value. Cannot get next SortDimension value."),
            };
        }

        private static IEnumerable<ISpatialData> SortByDimension(IEnumerable<ISpatialData> data, SortDimension sortDimension) 
        {
            return sortDimension switch
            {
                SortDimension.X => data.OrderBy(i => i.MBR.MinX),
                SortDimension.Y => data.OrderBy(i => i.MBR.MinY),
                SortDimension.Z => data.OrderBy(i => i.MBR.MinZ),
                _ => throw new Exception("Unrecognised SortDimension value. Cannot sort data."),
            };
        }
        /// <summary>
        /// Get all of the spatial objects indexed by this <see cref="RTree"/>
        /// </summary>
        /// <returns>
        /// A list of the spatial objects from this <see cref="RTree"/>
        /// </returns>
        public List<ISpatialData> GetAllItems()
        {
            return GetAllItems(new List<ISpatialData>(), _root);
        }

        private List<ISpatialData> GetAllItems(List<ISpatialData> items, Node current)
        {
            if (current.IsLeaf)
            {
                items.AddRange(current.Items);
            }
            else
            {
                foreach(var node in current.Children.Cast<Node>())
                {
                    GetAllItems(items, node);
                }
            }
            return items;
        }
        /// <summary>
        /// Get all of the spatial objects in this <see cref="RTree"/> that overlap with <paramref name="rectangle"/>
        /// </summary>
        /// <param name="rectangle"></param>
        /// <returns>
        /// A list of the the spatial objects in <see cref="RTree"/> overlapping with <paramref name="rectangle"/>
        /// </returns>
        public List<ISpatialData> Search(MinumumBoundingRectangle rectangle)
        {
            var results = new List<ISpatialData>();

            if (!_root.MBR.Intersects(rectangle))
                return results;

            var search = new Stack<Node>();
            search.Push(_root);

            while (search.Count != 0)
            {
                var node = search.Pop();

                if (node.IsLeaf)
                {
                    results.AddRange(node.Items.Where(i => i.MBR.Intersects(rectangle)));
                }
                else
                {
                    foreach (var child in node.Children.Cast<Node>())
                    {
                        if (child.MBR.Intersects(rectangle))
                            search.Push(child);
                    }
                }
            
            }
            return results;
        }

    }
}
