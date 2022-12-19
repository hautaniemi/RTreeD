
namespace RTreeD.Test
{

    public class RTreeTests
    {
        private static readonly double[,] _pointsCoordinates = new double[,]
            {
                {0, 0, 0},          {10, 10, 10},           {20, 20, 20},                   {15, 0, 0},                 {0, 25, 0},         {0, 0, 5},                  {0, 5, 15},         {25, 15, 5},
                {0, 0, 0},          {10, 10, 10},           {20, 20, 20},                   {15, 0, 0},                 {0, 25, 0},         {0, 0, 5},                  {0, 5, 15},         {25, 15, 5},
                {-0, -0, -0},       {-10, -10, -10},        {-20, -20, -20},                {-15, -0, -0},              {-0, -25, -0},      {-0, -0, -5},               {-0, -5, -15},      {-25, -15, -5},
                {-0, 0, -0},        {-10, -10, 10},         {20, -20, 20},                  {-15, -0, -0},              {0, -25, -0},       {-0, -0, -5},               {-0, -5, 15},       {-25, 15, -5},
                {0.5, 0.5, 0.5},    {10.25, 10.25, 10.25},  {20.1333, 20.1333, 20.1333},    {15.000000000001, 0, 0},    {0, -25.12, 0},     {0, 0, -5.000000333212},    {0, -5.5, -15.5},   {25, -15.93, -5.01},
            };

        private static readonly double[,] _cubeCoordinates = new double[,]
            {
                {0, 0, 0, 0, 0, 0 },            {10, 10, 10, 10, 10, 10},       {20, 20, 20, 20, 20, 20},
                {15, 0, 0, 15, 0, 0},           {0, 25, 0, 0, 25, 0},           {0, 0, 5, 0, 0, 5}, 
                {0, 15, 25, 0, 0, 0 },          {5, 0, 25, 0, 0, 0 },           {5, 15, 0, 0, 0, 0 },               {5, 15, 25, 0, 0, 0 },
                {0, 0, 0, 0, 15, 25 },          {0, 0, 0, 5, 0, 25, },          {0, 0, 0, 5, 15, 0, },              {0, 0, 0, 5, 15, 25},

                {0, 0, 0, 0, 0, 0 },            {10, 10, 10, 10, 10, 10},       {20, 20, 20, 20, 20, 20},
                {15, 0, 0, 15, 0, 0},           {0, 25, 0, 0, 25, 0},           {0, 0, 5, 0, 0, 5},
                {0, 15, 25, 0, 0, 0 },          {5, 0, 25, 0, 0, 0 },           {5, 15, 0, 0, 0, 0 },               {5, 15, 25, 0, 0, 0 },
                {0, 0, 0, 0, 15, 25 },          {0, 0, 0, 5, 0, 25, },          {0, 0, 0, 5, 15, 0, },              {0, 0, 0, 5, 15, 25},

                {0, 0, 0, 0, 0, 0 },            {-10, -10, -10, -10, -10, -10},   {-20, -20, -20, -20, -20, -20},
                {-15, 0, 0, -15, 0, 0},         {0, -25, 0, 0, -25, 0},           {0, 0, -5, 0, 0, -5},
                {0, -15, -25, 0, 0, 0 },        {-5, 0, -25, 0, 0, 0 },           {-5, -15, 0, 0, 0, 0 },           {-5, -15, -25, 0, 0, 0 },
                {0, 0, 0, 0, -15, -25 },        {0, 0, 0, -5, 0, -25, },          {0, 0, 0, -5, -15, 0, },          {0, 0, 0, -5, -15, -25},

                {0, 0, 0, 0, 0, 0 },            {-10, -10, -10, 10, 10, 10},      {-20, -20, -20, 20, 20, 20},
                {-15, 0, 0, 15, 0, 0},          {0, -25, 0, 0, 25, 0},            {0, 0, -5, 0, 0, 5},              {-5, -15, -25, 5, 15, 25},

                {0, 0, 0, 0, 0, 0 },            {-10.89723, -10.212, -10.423, 10.62378, 10.3913, 10.836456},    {-20.73492, -20.73492, -20.73492, 20.73492, 20.73492, 20.73492},
                {-15.001, 0, 0, 15.983, 0, 0},  {0, -25.72345, 0, 0, 25.72345, 0},  {0, 0, -5.0000000001, 0, 0, 5.0000000001},  {-5.99999999999999, -15.99999999999999, -25.99999999999999, 5.0000000001, 15.0000000001, 25.0000000001},
            };

        private static readonly List<Cube> _points = Cube.CreatePoints(_pointsCoordinates).ToList();
        private static readonly List<Cube> _cubes = Cube.CreateCubes(_cubeCoordinates).ToList();

        [Fact]
        public void BulkLoadPointsTest()
        {
            var tree = new RTree(_points, 5);
            Assert.Equal(_points.Count, tree.Count);
            Assert.True(new HashSet<ISpatialData>(_points).SetEquals(tree.GetAllItems()));
        }

        [Fact]
        public void BulkLoadCubesTest()
        {
            var tree = new RTree(_cubes, 5);
            Assert.Equal(_cubes.Count, tree.Count);
            Assert.True(new HashSet<ISpatialData>(_cubes).SetEquals(tree.GetAllItems())); 
        }
        [Fact]
        public void SearchPointsTest()
        {
            var tree = new RTree(_points, 5);

            var expected = Enumerable.Range(0, _pointsCoordinates.GetLength(0)).
               Where(i => _pointsCoordinates[i, 0] == 0 && _pointsCoordinates[i, 1] == 0 && _pointsCoordinates[i, 2] == 0).Count();

            var results = tree.Search(new MinumumBoundingRectangle(0, 0, 0, 0, 0 ,0));

            Assert.Equal(expected, results.Count);
        }
        [Fact]
        public void SearchCubesTest()
        {
            var searchWindow = new MinumumBoundingRectangle(-5, -5, -5, 5, 5, 5);
            var tree = new RTree(_cubes, 5);

            var expected = _cubes.Where(c => c.MBR.Intersects(searchWindow)).Cast<ISpatialData>().ToHashSet();
            var results = tree.Search(searchWindow);

            Assert.True(expected.SetEquals(results));
        }
    }
}