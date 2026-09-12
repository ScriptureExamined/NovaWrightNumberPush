namespace NovaWright.NumberPush.LevelGenerator
{
    public class DifficultyProfile
    {
        public string Name { get; set; } = string.Empty;

        public int FirstLevel { get; set; }

        public int LastLevel { get; set; }

        public int StartingRows { get; set; } = 10;

        public int StartingColumns { get; set; } = 12;

        public int RowIncrease { get; set; } = 2;

        public int ColumnIncrease { get; set; } = 2;

        public int MaximumRows { get; set; } = 18;

        public int MaximumColumns { get; set; } = 20;

        public int MinimumCrates { get; set; }

        public int MaximumCrates { get; set; }

        public int MinimumCrateDistance { get; set; }

        public int MaximumCrateDistance { get; set; }

        public int MinimumInteriorWalls { get; set; }

        public int MaximumInteriorWalls { get; set; }

        public int StartingMinimumSolutionPushes { get; set; }

        public int StartingMaximumSolutionPushes { get; set; }

        public int MinimumPushesIncreasePerLevel { get; set; }

        public int MaximumPushesIncreasePerLevel { get; set; }

        public int MaximumAttempts { get; set; } = 1500;

        public bool AppliesToLevel(int levelNumber)
        {
            return levelNumber >= FirstLevel &&
                   levelNumber <= LastLevel;
        }
    }
}