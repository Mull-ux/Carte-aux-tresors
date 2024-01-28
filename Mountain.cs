namespace CarteAuxTresors
{
    internal class Mountain : IObjectOnTheMap
    {
        int abscissa;
        int ordinate;

        public Mountain(int abscissa, int ordinate)
        {
            this.abscissa = abscissa;
            this.ordinate = ordinate;
        }

        public int Abscissa { get { return abscissa; } }
        public int Ordinate { get { return ordinate; } }
    }
}
