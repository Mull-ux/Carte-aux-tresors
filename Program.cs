using static CarteAuxTresors.Core;

namespace CarteAuxTresors
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var filePath = GetFilePath();

            var map = InitializeMap(filePath);

            InitializeMountainsAndTreasures(filePath, map);

            var adventurers = InitializeAdventurers(filePath);

            ExecuteMovementSequence(adventurers, map);

            PrintResult(map, adventurers);
        }
    }
}