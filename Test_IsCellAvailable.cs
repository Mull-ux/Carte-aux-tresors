using CarteAuxTresors;

namespace TestCarteAuxTresors
{
    [TestFixture]
    public class Test_IsCellAvailable
    {
        [Test]
        public void IsCellAvailable_EmptyCell_ReturnsTrue()
        {
            // Arrange
            var map = new Map(5, 5);
            var adventurers = new List<Adventurer>();
            var nextPosition = Tuple.Create(2, 2);

            // Act
            var result = Core.IsCellAvailable(map, nextPosition, adventurers);

            // Assert
            Assert.That(result, Is.True);
        }

        [Test]
        public void IsCellAvailable_CellWithMountain_ReturnsFalse()
        {
            // Arrange
            var map = new Map(5, 5);
            var mountain = new Mountain(2, 2);
            map.AddObjectToMap(mountain);
            var adventurers = new List<Adventurer>();
            var nextPosition = Tuple.Create(2, 2);

            // Act
            var result = Core.IsCellAvailable(map, nextPosition, adventurers);

            // Assert
            Assert.That(result, Is.False);
        }

        [Test]
        public void IsCellAvailable_CellWithAdventurer_ReturnsFalse()
        {
            // Arrange
            var map = new Map(5, 5);
            var adventurers = new List<Adventurer> { new Adventurer("Test", 2, 2, 'N', "") };
            var nextPosition = Tuple.Create(2, 2);

            // Act
            var result = Core.IsCellAvailable(map, nextPosition, adventurers);

            // Assert
            Assert.That(result, Is.False);
        }
    }
}