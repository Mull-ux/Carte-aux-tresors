using System.Configuration;
using System.Text;

namespace CarteAuxTresors
{
    internal class Core
    {
        internal static string GetFilePath(string? filePath = null)
        {
            filePath ??= ConfigurationManager.AppSettings["FilePath"];
            
            if (!File.Exists(filePath))
            {
                throw new FileNotFoundException("The specified file does not exist.");
            }

            return filePath;
        }

        internal static Map InitializeMap(string filePath) 
        {
            try
            {
                string[] lines = File.ReadAllLines(filePath);

                foreach (var line in lines) 
                {
                    var firstChar = line[0];
                    if (string.IsNullOrWhiteSpace(line) || firstChar != 'C')
                    {
                        continue;
                    }

                    var width = ParseLine(line)[0];
                    var height = ParseLine(line)[1];

                    return new Map(width, height);
                }

                throw new Exception("The file doesn't contain any map declaration");
            }
            catch(Exception ex)
            {
                throw new Exception($"An error occurred while reading the file: {ex.Message}");
            }
        }

        internal static void InitializeMountainsAndTreasures(string filePath, Map map)
        {
            string[] lines = File.ReadAllLines(filePath);

            foreach (var line in lines)
            {
                var firstChar = line[0];
                if (string.IsNullOrWhiteSpace(line) || firstChar == 'C' || firstChar == 'A')
                {
                    continue;
                }

                var parsedLine = ParseLine(line) ?? throw new Exception("Error when parsing the line");

                var abscissa = 0;
                var ordinate = 0;
                var nbOfTreasures = 0;

                switch (firstChar)
                {
                    case 'M':
                        abscissa = parsedLine[0];
                        ordinate = parsedLine[1];
                        var mountain = new Mountain(abscissa, ordinate);
                        map.AddObjectToMap(mountain);
                        break;

                    case 'T':
                        abscissa = parsedLine[0];
                        ordinate = parsedLine[1];
                        nbOfTreasures = parsedLine[2];
                        var treasure = new Treasure(abscissa, ordinate, nbOfTreasures);
                        map.AddObjectToMap(treasure);
                        break;

                    default:
                        throw new Exception($"Type not recognized : {firstChar}");
                }
            }
        }

        internal static List<Adventurer> InitializeAdventurers(string filePath)
        {
            try
            {
                string[] lines = File.ReadAllLines(filePath);
                var adventurers = new List<Adventurer>();

                foreach (var line in lines)
                {
                    var firstChar = line[0];
                    if (string.IsNullOrWhiteSpace(line) || firstChar != 'A')
                    {
                        continue;
                    }

                    var splitedLine = line.Split('-');
                    var name = splitedLine[1];
                    if(!int.TryParse(splitedLine[2], out int abscissa)) { throw new Exception("Unable to cast abscissa"); };
                    if(!int.TryParse(splitedLine[3], out int ordinate)) { throw new Exception("Unable to cast ordinate"); };
                    var direction = splitedLine[4].Trim()[0];
                    var movementSequence = splitedLine[5];

                    var adventurer = new Adventurer(name, abscissa, ordinate, direction, movementSequence);
                    adventurers.Add(adventurer);
                }

                if(adventurers.Count == 0) { throw new Exception("The file doesn't contain any aventurer declaration"); }

                return adventurers;
            }
            catch (Exception ex)
            {
                throw new Exception($"An error occurred while reading the file: {ex.Message}");
            }
        }

        internal static void ExecuteMovementSequence(List<Adventurer> adventurers, Map map)
        {
            int maxSequenceLength = adventurers.Max(a => a.MovementSequence.Trim().Length);

            for (int i = 0; i < maxSequenceLength; i++)
            {
                foreach (var adventurer in adventurers)
                {
                    if (i < adventurer.MovementSequence.Trim().Length)
                    {
                        var movement = adventurer.MovementSequence.Trim()[i];

                        switch (movement)
                        {
                            case 'A':
                                StepForward(map, adventurer, adventurers);
                                break;

                            case 'D':
                            case 'G':
                                Turn(adventurer, movement);
                                break;

                            default:
                                throw new Exception("Movement not recognized");
                        }
                    }
                }
            }
        }
        
        static void StepForward(Map map, Adventurer adventurer, List<Adventurer> adventurers)
        {
            var currentPosition = Tuple.Create(adventurer.Abscissa, adventurer.Ordinate);
            var currentDirection = adventurer.Direction;

            var nextPosition = CalculateNextPosition(currentPosition, currentDirection);

            if (IsCellAvailable(map, nextPosition, adventurers))
            {
                Adventurer.UpdatePosition(adventurer, nextPosition);
                if (IsTreasure(map, nextPosition))
                {
                    map.TakeTreasure(nextPosition);
                    Adventurer.UpdateCollectedTreasures(adventurer);
                }
            }
        }

        internal static Tuple<int, int> CalculateNextPosition(Tuple<int, int> currentPosition, char direction)
        {
            switch (direction)
            {
                case 'N':
                    return Tuple.Create(currentPosition.Item1, currentPosition.Item2 - 1);
                case 'E':
                    return Tuple.Create(currentPosition.Item1 + 1, currentPosition.Item2);
                case 'O':
                    return Tuple.Create(currentPosition.Item1 - 1, currentPosition.Item2);
                case 'S':
                    return Tuple.Create(currentPosition.Item1, currentPosition.Item2 + 1);
                default:
                    throw new ArgumentException("Invalid direction");
            }
        }

        internal static bool IsCellAvailable(Map map, Tuple<int, int> nextPosition, List<Adventurer> adventurers)
        {
            if (!map.Map_.ContainsKey(nextPosition)) { return false; };
            if (map.Map_[nextPosition] is Mountain) { return false; };
            foreach (var adventurer in adventurers)
            {
                if (adventurer.Abscissa == nextPosition.Item1 && adventurer.Ordinate == nextPosition.Item2)
                {
                    return false;
                }
            }
            return true;
        }

        static bool IsTreasure(Map map, Tuple<int, int> position)
        {
            if (map.Map_[position] is Treasure) 
            { 
                return true; 
            }

            return false;
        }

        internal static void Turn(Adventurer adventurer, char rotation)
        {
            char[] directions = { 'N', 'E', 'S', 'O' };
            var currentDirection = adventurer.Direction;
            int currentIndex = Array.IndexOf(directions, currentDirection);
            char newDirection;

            if (currentIndex == -1) { throw new ArgumentException("Invalid direction"); }

            if (rotation == 'D')
            {
                newDirection = directions[(currentIndex + 1) % directions.Length];
                Adventurer.UpdateDirection(adventurer, newDirection);
            }
            else
            {
                newDirection = directions[(currentIndex - 1 + directions.Length) % directions.Length];
                Adventurer.UpdateDirection(adventurer, newDirection);
            }
        }

        internal static int[] ParseLine(string line)
        {
            var parsedLine = new List<int>();
            var splitedLine = line.Split('-');

            foreach(var item in splitedLine) 
            {
                if (int.TryParse(item, out int result))
                {
                    parsedLine.Add(result);
                }
            }

            return parsedLine.ToArray();
        }

        internal static void PrintResult(Map map, List<Adventurer> adventurers)
        {
            var filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Result.txt");
            var resultBuilder = new StringBuilder();

            resultBuilder.AppendLine(map.PrintMap().TrimEnd());

            foreach(var adventurer in adventurers)
            {
                resultBuilder.AppendLine(adventurer.PrintAdventurer());
            }

            try
            {
                using var writer = new StreamWriter(filePath);
                writer.Write(resultBuilder.ToString().TrimEnd());
            }
            catch (Exception ex)
            {
                throw new ArgumentException($"An error occurred: {ex.Message}");
            }
        }
    }
}