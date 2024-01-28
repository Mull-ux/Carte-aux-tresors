using CarteAuxTresors;

namespace TestCarteAuxTresors
{
    [TestFixture]
    public class Test_ParseLine
    {
        [Test]
        public void ParseLine_ValidLine_ReturnsParsedNumbers()
        {
            // Arrange
            string line = "1-2-3";

            // Act
            var result = Core.ParseLine(line);

            // Assert
            Assert.That(result, Is.EqualTo(new int[] { 1, 2, 3 }));
        }

        [Test]
        public void ParseLine_InvalidLine_IgnoresNonNumeric()
        {
            // Arrange
            string line = "1-a-3";

            // Act
            var result = Core.ParseLine(line);

            // Assert
            Assert.That(result, Is.EqualTo(new int[] { 1, 3 }));
        }
    }
}