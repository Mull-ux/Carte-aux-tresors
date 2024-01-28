using System.Text;

namespace CarteAuxTresors
{
    internal class Adventurer
    {
        string name;
        int abscissa;
        int ordinate;
        char direction;
        string movementSequence;
        int nbOfTreasureCollected;

        public Adventurer(string name, int abscissa, int ordinate, char direction, string movementSequence)
        {
            this.name = name;
            this.abscissa = abscissa;
            this.ordinate = ordinate;
            this.direction = direction;
            this.movementSequence = movementSequence;
            this.nbOfTreasureCollected = 0;
        }

        public static void UpdatePosition(Adventurer adventurer, Tuple<int,int> newPosition)
        {
            adventurer.abscissa = newPosition.Item1;
            adventurer.ordinate = newPosition.Item2;
        }

        public static void UpdateDirection(Adventurer adventurer, char rotation)
        {
            adventurer.direction = rotation;
        }

        public static void UpdateCollectedTreasures(Adventurer adventurer)
        {
            adventurer.nbOfTreasureCollected++;
        }

        public string PrintAdventurer()
        {
            var printedAdventurer = $"A - {name.Trim()} - {abscissa} - {ordinate} - {direction} - {nbOfTreasureCollected}";
            return printedAdventurer;
        }

        public string Name { get { return name; } }
        public int Abscissa { get { return abscissa; } }
        public int Ordinate { get { return ordinate; } }
        public char Direction { get { return direction; } }
        public string MovementSequence { get {  return movementSequence; } }
        public int NbOfTreasureCollected { get {  return nbOfTreasureCollected; } }
    }
}