using CarteAuxTresors;

namespace TestCarteAuxTresors
{
    [TestFixture]
    public class Test_InitializeMountainsAndTreasures
    {
        [Test]
        public void InitializeMountainsAndTreasures_ValidFile_AddsObjectsToMap()
        {
            // Arrange
            string tempFilePath = Path.GetTempFileName();
            File.WriteAllLines(tempFilePath, new string[] { "C - 10 - 10", "M - 1 - 0", "T - 1 - 3 - 2" });
            var map = new Map(10, 10);
            var mountainKey = Tuple.Create(1, 0);
            var treasureKey = Tuple.Create(1, 3);

            try
            {
                // Act
                Core.InitializeMountainsAndTreasures(tempFilePath, map);

                // Assert
                Assert.That(map.Map_[mountainKey], Is.InstanceOf<Mountain>());
                Assert.That(map.Map_[treasureKey], Is.InstanceOf<Treasure>());
            }
            finally
            {
                File.Delete(tempFilePath);
            }   
        }

        [Test]
        public void InitializeMountainsAndTreasures_InvalidFile_ThrowsException()
        {
            // Arrange
            string tempFilePath = Path.GetTempFileName();
            File.WriteAllLines(tempFilePath, new string[] { "C - 10 - 10", "M - 1 - 15", "T - 11 - 3 - 2" });
            var map = new Map(10, 10);

            try
            {
                // Act & Assert
                Assert.Throws<Exception>(() => Core.InitializeMountainsAndTreasures(tempFilePath, map));
            }
            finally
            {
                File.Delete(tempFilePath);
            }
        }
    }
}