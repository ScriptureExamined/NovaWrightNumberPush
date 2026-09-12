namespace NovaWright.NumberPush.LevelGenerator
{
    public class NumberPushDifficulty
    {
        public int LevelNumber { get; }

        public int Complexity { get; }

        public int MinimumCrates { get; }

        public int MaximumCrates { get; }

        public int MinimumCrateDistance { get; }

        public int MaximumCrateDistance { get; }

        public int MinimumInteriorWalls { get; }

        public int MaximumInteriorWalls { get; }

        public int MinimumSolutionPushes { get; }

        public int MaximumSolutionPushes { get; }

        public int StartingRows { get; }

        public int StartingColumns { get; }

        public int MaximumRows { get; }

        public int MaximumColumns { get; }

        public int MaximumAttempts { get; }

        public NumberPushDifficulty(
            int levelNumber)
        {
            if (levelNumber <= 0)
            {
                throw new ArgumentOutOfRangeException(
                    nameof(levelNumber),
                    "Level number must be greater than zero.");
            }

            LevelNumber =
                levelNumber;

            Complexity =
                1 +
                (levelNumber - 1) / 10;

            MinimumCrates =
                Math.Min(
                    1 + Complexity / 3,
                    6);

            MaximumCrates =
                Math.Min(
                    MinimumCrates + 1,
                    8);

            MinimumCrateDistance =
                Math.Max(
                    1,
                    Complexity / 2);

            MaximumCrateDistance =
                Math.Max(
                    MinimumCrateDistance,
                    Complexity);

            MinimumInteriorWalls =
                Complexity * 2;

            MaximumInteriorWalls =
                Complexity * 4 + 4;

            MinimumSolutionPushes =
                Complexity * 4;

            MaximumSolutionPushes =
                Complexity * 7 + 6;

            StartingRows =
                10;

            StartingColumns =
                12;

            MaximumRows =
                18 +
                Complexity / 5 * 2;

            MaximumColumns =
                20 +
                Complexity / 5 * 2;

            MaximumAttempts =
                1500;
        }
    }
}