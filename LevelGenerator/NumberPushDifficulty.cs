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
    levelNumber;

            MinimumCrates =
    1 +
    Complexity / 3;

            MaximumCrates =
                MinimumCrates + 1;

            MinimumCrateDistance =
    1;

            MaximumCrateDistance =
                Math.Max(
                    1,
                    Complexity);

            MinimumInteriorWalls =
                Complexity * 2;

            MaximumInteriorWalls =
                Complexity * 4 + 4;

            MinimumSolutionPushes =
    4 +
    Complexity / 2;

            MaximumSolutionPushes =
                10 +
                Complexity;

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