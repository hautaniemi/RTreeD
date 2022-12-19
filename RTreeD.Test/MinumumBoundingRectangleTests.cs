
namespace RTreeD.Test
{
    public class MinumumBoundingRectangleTests
    {
        [Fact]
        public void IntersectPointTest()
        {
            var testCube = new MinumumBoundingRectangle(0, 0, 0, 10, 10, 10);
            Assert.True(testCube.Intersects(new MinumumBoundingRectangle(5, 5, 5, 5, 5, 5)));
        }

        [Fact]
        public void DoesNotIntersectPointTest()
        {
            var testCube = new MinumumBoundingRectangle(0, 0, 0, 10, 10, 10);
            Assert.False(testCube.Intersects(new MinumumBoundingRectangle(15, 15, 15, 15, 15, 15)));
        }

        [Fact]
        public void InterstCubeTest()
        {
            var testCube = new MinumumBoundingRectangle(0, 0, 0, 10, 10, 10);
            Assert.True(testCube.Intersects(new MinumumBoundingRectangle(-5, -5, -5, 5, 5, 5)));
        }

        [Fact]
        public void DoesNotIntersectCubeTest()
        {
            var testCube = new MinumumBoundingRectangle(0, 0, 0, 10, 10, 10);
            Assert.False(testCube.Intersects(new MinumumBoundingRectangle(15, 15, 15, 45, 45, 45)));
        }

        [Fact]
        public void IntersectEdgeTest()
        {
            var testCube = new MinumumBoundingRectangle(0, 0, 0, 10.0000001, 10, 10);
            Assert.True(testCube.Intersects(new MinumumBoundingRectangle(10.0000001, 10, 10, 45, 45, 45)));
        }
        [Fact]
        public void DoesNotIntersectEdgeTest()
        {
            var testCube = new MinumumBoundingRectangle(0, 0, 0, 10, 10, 10);
            Assert.False(testCube.Intersects(new MinumumBoundingRectangle(10.000000000001, 10, 10, 45, 45, 45)));
        }

    }
}