namespace RTreeD.Test
{
    internal class Cube : ISpatialData, IEquatable<Cube>
    {
        private readonly MinumumBoundingRectangle _mbr;

        public Cube(double minX, double minY, double minZ, double maxX, double maxY, double maxZ)
        {
            _mbr = new MinumumBoundingRectangle(
                minX,
                minY,
                minZ,
                maxX,
                maxY,
                maxZ
                );
        }

        public ref readonly MinumumBoundingRectangle MBR => ref _mbr;

        public bool Equals(Cube? other) =>
            other != null
                && this.MBR.Equals(other.MBR);

        public override bool Equals(object? obj) => 
            Equals(obj as Cube);

        public override int GetHashCode() =>
            _mbr.GetHashCode();
        

        public static IEnumerable<Cube> CreateCubes(double[,] cubes) =>
            Enumerable.Range(0, cubes.GetLength(0))
                .Select(i => new Cube(cubes[i, 0], cubes[i, 1], cubes[i, 2], cubes[i, 3], cubes[i, 4], cubes[i, 5]));

        public static IEnumerable<Cube> CreatePoints(double[,] points) =>
            Enumerable.Range(0, points.GetLength(0))
                .Select(i => new Cube(points[i, 0], points[i, 1], points[i, 2], points[i, 0], points[i, 1], points[i, 2]));

    }
}