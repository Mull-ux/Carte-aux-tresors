namespace CarteAuxTresors
{
    internal class Treasure : IObjectOnTheMap
    {
        int abscissa;
        int ordinate;
        int nbOfTreasures;

        public Treasure(int abscissa, int ordinate, int nbOfTreasures)
        {
            this.abscissa = abscissa;
            this.ordinate = ordinate;
            this.nbOfTreasures = nbOfTreasures;
        }

        public static void ReduceNbOfTreasures(Treasure treasure)
        {
            treasure.nbOfTreasures--;
        }

        public int Abscissa { get { return abscissa; } }
        public int Ordinate { get { return ordinate; } }
        public int NbOfTreasures { get {  return nbOfTreasures; } }
    }
}
