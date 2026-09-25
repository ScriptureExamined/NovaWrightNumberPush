namespace NovaWright.NumberPush.LevelGenerator
{
    public class CrateComplexity
    {
        public List<int> Distances { get; }

        public int CrateCount => Distances.Count;

        public int TotalDistance => Distances.Sum();

        public int MaximumDistance => Distances.Max();

        public CrateComplexity(IEnumerable<int> distances)
        {
            Distances = distances.OrderBy(distance => distance).ToList();

            if (Distances.Count == 0)
            {
                throw new ArgumentException(
                    "At least one crate distance is required.",
                    nameof(distances)
                );
            }
        }

        public override string ToString()
        {
            return "[" + string.Join(",", Distances) + "]";
        }
    }
}
