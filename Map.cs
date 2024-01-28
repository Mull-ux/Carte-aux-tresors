using System.Text;

namespace CarteAuxTresors
{
    internal class Map
    {
        int width;
        int height;
        Dictionary<Tuple<int, int>, IObjectOnTheMap?> map;

        public Map(int width, int height)
        {
            this.width = width;
            this.height = height;
            map = new Dictionary<Tuple<int, int>, IObjectOnTheMap?>();

            for (int i = 0; i < this.width; i++)
            {
                for (int j = 0; j < this.height; j++)
                {
                    map.Add(Tuple.Create(i, j), null);
                }
            }
        }

        public void AddObjectToMap(IObjectOnTheMap item)
        {
            var abscissa = item.Abscissa;
            var ordinate = item.Ordinate;
            var key = Tuple.Create(abscissa, ordinate);

            if (!map.ContainsKey(key))
            {
                throw new Exception("Object coordinates are outside the map");
            }

            map[key] = item;
        }

        public void TakeTreasure(Tuple<int, int> key)
        {
            if (map[key] is Treasure treasure)
            {
                Treasure.ReduceNbOfTreasures(treasure);

                if(treasure.NbOfTreasures == 0)
                {
                    map[key] = null;
                }
            }
        }

        public string PrintMap()
        {
            var resultBuilder = new StringBuilder();
            var lines = new List<string>();

            lines.Add($"C - {width} - {height}");

            foreach (var item in map)
            {
                if (item.Value is Mountain mountain)
                {
                    lines.Add($"M - {mountain.Abscissa} - {mountain.Ordinate}");
                }
                else if (item.Value is Treasure treasure)
                {
                    lines.Add($"T - {treasure.Abscissa} - {treasure.Ordinate} - {treasure.NbOfTreasures}");
                }
            }

            foreach (var line in lines.OrderBy(l => l))
            {
                resultBuilder.AppendLine(line);
            }

            return resultBuilder.ToString();
        }

        public Dictionary<Tuple<int, int>, IObjectOnTheMap?> Map_ {  get { return map; } }
    }
}