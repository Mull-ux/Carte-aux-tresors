using CarteAuxTresors;

namespace TestCarteAuxTresors
{
    [TestFixture]
    public class Test_CalculateNextPosition
    {
        [Test]
        [TestCase('N', 1, 1, 1, 0)]
        [TestCase('E', 1, 1, 2, 1)]
        [TestCase('O', 1, 1, 0, 1)]
        [TestCase('S', 1, 1, 1, 2)]
        public void CalculateNextPosition_ValidDirection_ReturnsCorrectPosition(char direction, int x, int y, int expectedX, int expectedY)
        {
            // Arrange
            var currentPosition = Tuple.Create(x, y);

            // Act
            var result = Core.CalculateNextPosition(currentPosition, direction);

            // Assert
            Assert.That(result, Is.EqualTo(Tuple.Create(expectedX, expectedY)));
        }

        [Test]
        public void CalculateNextPosition_InvalidDirection_ThrowsArgumentException()
        {
            // Arrange
            var currentPosition = Tuple.Create(1, 1);

            // Act & Assert
            Assert.Throws<ArgumentException>(() => Core.CalculateNextPosition(currentPosition, 'X'));
        }
    }
}