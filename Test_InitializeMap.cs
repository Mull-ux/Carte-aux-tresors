using CarteAuxTresors;

namespace TestCarteAuxTresors
{
    [TestFixture]
    public class Test_InitializeMap
    {
        [Test]
        public void InitializeMap_ValidFile_ReturnsMap()
        {
            // Arrange
            string tempFilePath = Path.GetTempFileName();
            File.WriteAllLines(tempFilePath, new string[] { "C - 3 - 4", "M - 1 - 0", "T - 1 - 3 - 2" });

            try
            {
                // Act
                var result = Core.InitializeMap(tempFilePath);

                // Assert
                Assert.That(result, Is.Not.Null);
            }
            finally
            {
                File.Delete(tempFilePath);
            }
        }

        [Test]
        public void InitializeMap_InvalidFile_ThrowsException()
        {
            // Arrange
            string tempFilePath = Path.GetTempFileName();
            File.WriteAllLines(tempFilePath, new string[] {"M - 1 - 0", "T - 1 - 3 - 2" });

            try
            {
                // Act & Assert
                Assert.Throws<Exception>(() => Core.InitializeMap(tempFilePath));
            }
            finally
            {
                File.Delete(tempFilePath);
            }
        }
    }
}