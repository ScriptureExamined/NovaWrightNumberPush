namespace NovaWright.NumberPush.LevelGenerator
{
    public static class DifficultyProfiles
    {
        public static DifficultyProfile Early =>
            new DifficultyProfile
            {
                Name = "Early",

                FirstLevel = 1,
                LastLevel = 18,

                StartingRows = 10,
                StartingColumns = 12,

                RowIncrease = 2,
                ColumnIncrease = 2,

                MaximumRows = 18,
                MaximumColumns = 20,

                MinimumCrates = 1,
                MaximumCrates = 2,

                MinimumCrateDistance = 1,
                MaximumCrateDistance = 2,

                MinimumInteriorWalls = 0,
                MaximumInteriorWalls = 5,

                StartingMinimumSolutionPushes = 1,
                StartingMaximumSolutionPushes = 4,

                MinimumPushesIncreasePerLevel = 1,
                MaximumPushesIncreasePerLevel = 1,

                MaximumAttempts = 1000
            };

        public static DifficultyProfile EarlyAdvanced =>
            new DifficultyProfile
            {
                Name = "Early Advanced",

                FirstLevel = 19,
                LastLevel = 24,

                StartingRows = 10,
                StartingColumns = 12,

                RowIncrease = 2,
                ColumnIncrease = 2,

                MaximumRows = 18,
                MaximumColumns = 20,

                MinimumCrates = 2,
                MaximumCrates = 3,

                MinimumCrateDistance = 1,
                MaximumCrateDistance = 3,

                MinimumInteriorWalls = 4,
                MaximumInteriorWalls = 10,

                StartingMinimumSolutionPushes = 10,
                StartingMaximumSolutionPushes = 15,

                MinimumPushesIncreasePerLevel = 1,
                MaximumPushesIncreasePerLevel = 1,

                MaximumAttempts = 1500
            };

        public static DifficultyProfile Advanced =>
            new DifficultyProfile
            {
                Name = "Advanced",

                FirstLevel = 25,
                LastLevel = 30,

                StartingRows = 10,
                StartingColumns = 12,

                RowIncrease = 2,
                ColumnIncrease = 2,

                MaximumRows = 18,
                MaximumColumns = 20,

                MinimumCrates = 2,
                MaximumCrates = 3,

                MinimumCrateDistance = 1,
                MaximumCrateDistance = 3,

                MinimumInteriorWalls = 7,
                MaximumInteriorWalls = 14,

                StartingMinimumSolutionPushes = 16,
                StartingMaximumSolutionPushes = 22,

                MinimumPushesIncreasePerLevel = 1,
                MaximumPushesIncreasePerLevel = 1,

                MaximumAttempts = 1500
            };

        public static DifficultyProfile Hard =>
            new DifficultyProfile
            {
                Name = "Hard",

                FirstLevel = 31,
                LastLevel = 36,

                StartingRows = 10,
                StartingColumns = 12,

                RowIncrease = 2,
                ColumnIncrease = 2,

                MaximumRows = 18,
                MaximumColumns = 20,

                MinimumCrates = 3,
                MaximumCrates = 3,

                MinimumCrateDistance = 1,
                MaximumCrateDistance = 4,

                MinimumInteriorWalls = 8,
                MaximumInteriorWalls = 16,

                StartingMinimumSolutionPushes = 22,
                StartingMaximumSolutionPushes = 30,

                MinimumPushesIncreasePerLevel = 1,
                MaximumPushesIncreasePerLevel = 1,

                MaximumAttempts = 2000
            };

        public static DifficultyProfile Expert =>
            new DifficultyProfile
            {
                Name = "Expert",

                FirstLevel = 37,
                LastLevel = 42,

                StartingRows = 10,
                StartingColumns = 12,

                RowIncrease = 2,
                ColumnIncrease = 2,

                MaximumRows = 18,
                MaximumColumns = 20,

                MinimumCrates = 3,
                MaximumCrates = 4,

                MinimumCrateDistance = 1,
                MaximumCrateDistance = 4,

                MinimumInteriorWalls = 10,
                MaximumInteriorWalls = 20,

                StartingMinimumSolutionPushes = 30,
                StartingMaximumSolutionPushes = 40,

                MinimumPushesIncreasePerLevel = 1,
                MaximumPushesIncreasePerLevel = 1,

                MaximumAttempts = 3000
            };
    }
}