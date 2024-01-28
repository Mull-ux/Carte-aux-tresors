using CarteAuxTresors;

namespace TestCarteAuxTresors
{
    [TestFixture]
    public class Test_Turn
    {
        [Test]
        public void Turn_WithRotationD_UpdatesDirectionCorrectly()
        {
            // Arrange
            var adventurer = new Adventurer("Test", 0, 0, 'N', "");

            // Act
            Core.Turn(adventurer, 'D');

            // Assert
            Assert.That(adventurer.Direction, Is.EqualTo('E'));
        }

        [Test]
        public void Turn_WithRotationG_UpdatesDirectionCorrectly()
        {
            // Arrange
            var adventurer = new Adventurer("Test", 0, 0, 'N', "");

            // Act
            Core.Turn(adventurer, 'G');

            // Assert
            Assert.That(adventurer.Direction, Is.EqualTo('O'));
        }

        [Test]
        public void Turn_WithInvalidDirection_ThrowsArgumentException()
        {
            // Arrange
            var adventurer = new Adventurer("Test", 0, 0, 'X', "");

            // Act & Assert
            Assert.Throws<ArgumentException>(() => Core.Turn(adventurer, 'D'));
        }
    }
}