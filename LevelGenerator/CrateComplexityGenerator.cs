namespace NovaWright.NumberPush.LevelGenerator
{
    public class CrateComplexityGenerator
    {
        private readonly Queue<CrateComplexity> pendingComplexities =
            new Queue<CrateComplexity>();

        private int maximumDistance = 3;

        private int maximumCrates = 2;

        public CrateComplexityGenerator()
        {
            AddInitialComplexities();
        }

        public CrateComplexity Next()
        {
            if (pendingComplexities.Count == 0)
            {
                ExpandComplexity();
            }

            return pendingComplexities.Dequeue();
        }

        private void AddInitialComplexities()
        {
            pendingComplexities.Enqueue(
                new CrateComplexity(
                    new[] { 1 }));

            pendingComplexities.Enqueue(
                new CrateComplexity(
                    new[] { 2 }));

            pendingComplexities.Enqueue(
                new CrateComplexity(
                    new[] { 1, 1 }));

            pendingComplexities.Enqueue(
                new CrateComplexity(
                    new[] { 1, 2 }));

            pendingComplexities.Enqueue(
                new CrateComplexity(
                    new[] { 2, 2 }));

            pendingComplexities.Enqueue(
                new CrateComplexity(
                    new[] { 3 }));
        }

        private void ExpandComplexity()
        {
            maximumDistance++;

            List<CrateComplexity> combinations =
                GenerateCombinations(
                    maximumCrates,
                    maximumDistance);

            foreach (CrateComplexity complexity in combinations)
            {
                pendingComplexities.Enqueue(
                    complexity);
            }

            maximumCrates++;
        }

        private List<CrateComplexity> GenerateCombinations(
            int crateCount,
            int maximumDistance)
        {
            List<CrateComplexity> results =
                new List<CrateComplexity>();

            GenerateCombinations(
                results,
                new List<int>(),
                crateCount,
                maximumDistance,
                1);

            return results
                .OrderBy(
                    complexity =>
                        complexity.TotalDistance)
                .ThenBy(
                    complexity =>
                        complexity.MaximumDistance)
                .ThenBy(
                    complexity =>
                        string.Join(
                            ",",
                            complexity.Distances))
                .ToList();
        }

        private void GenerateCombinations(
            List<CrateComplexity> results,
            List<int> current,
            int remaining,
            int maximumDistance,
            int minimumDistance)
        {
            if (remaining == 0)
            {
                results.Add(
                    new CrateComplexity(
                        current));

                return;
            }

            for (int distance = minimumDistance;
                 distance <= maximumDistance;
                 distance++)
            {
                current.Add(distance);

                GenerateCombinations(
                    results,
                    current,
                    remaining - 1,
                    maximumDistance,
                    distance);

                current.RemoveAt(
                    current.Count - 1);
            }
        }
    }
}