namespace NovaWright.NumberPush.LevelGenerator
{
    public class NumberPushDifficulty
    {
        public int LevelNumber { get; }

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

            int progression =
                levelNumber - 1;

            MinimumCrates =
                Math.Min(
                    1 + progression / 12,
                    6);

            MaximumCrates =
                Math.Min(
                    2 + progression / 10,
                    8);

            MinimumCrateDistance =
                1 + progression / 10;

            MaximumCrateDistance =
                2 + progression / 8;

            MinimumInteriorWalls =
                progression / 3;

            MaximumInteriorWalls =
                5 + progression / 2;

            MinimumSolutionPushes =
                1 + progression;

            MaximumSolutionPushes =
                6 + progression * 2;

            StartingRows =
                10;

            StartingColumns =
                12;

            MaximumRows =
                18 + progression / 15 * 2;

            MaximumColumns =
                20 + progression / 15 * 2;

            MaximumAttempts =
                1500;
        }
    }
}