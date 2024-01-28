using CarteAuxTresors;

namespace TestCarteAuxTresors
{
    [TestFixture]
    public class Test_GetFilePath
    {
        [SetUp]
        public void Setup()
        {
            AppDomain.CurrentDomain.SetData("APP_CONFIG_FILE", "C:/Users/mouha/source/repos/TestCarteAuxTresors/App.config");
        }

        [Test]
        public void GetFilePath_FileExists_ReturnsFilePath()
        {
            // Arrange & Act
            var result = Core.GetFilePath();

            // Assert
            Assert.That(result, Is.Not.Null);
        }

        [Test]
        public void GetFilePath_FileDoesNotExist_ThrowsFileNotFoundException()
        {
            // Arrange
            var filePath = "example.txt";

            // Act & Assert
            Assert.Throws<FileNotFoundException>(() => Core.GetFilePath(filePath));
        }
    }
}