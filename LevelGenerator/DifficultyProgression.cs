namespace NovaWright.NumberPush.LevelGenerator
{
    public static class DifficultyProgression
    {
        public static DifficultyProfile GetProfile(
            int levelNumber)
        {
            DifficultyProfile[] profiles =
{
    DifficultyProfiles.Early,
    DifficultyProfiles.EarlyAdvanced,
    DifficultyProfiles.Advanced,
    DifficultyProfiles.Hard,
    DifficultyProfiles.Expert
};

            foreach (DifficultyProfile profile in profiles)
            {
                if (profile.AppliesToLevel(levelNumber))
                {
                    return profile;
                }
            }

            throw new ArgumentOutOfRangeException(
                nameof(levelNumber),
                levelNumber,
                "No difficulty profile exists for this level.");
        }

        public static DifficultySettings GetSettings(
            int levelNumber)
        {
            DifficultyProfile profile =
                GetProfile(levelNumber);

            int levelsIntoProfile =
                levelNumber -
                profile.FirstLevel;

            int minimumPushes =
                profile.StartingMinimumSolutionPushes +
                levelsIntoProfile *
                profile.MinimumPushesIncreasePerLevel;

            int maximumPushes =
                profile.StartingMaximumSolutionPushes +
                levelsIntoProfile *
                profile.MaximumPushesIncreasePerLevel;

            return new DifficultySettings
            {
                Name = profile.Name,

                FirstLevel = profile.FirstLevel,
                LastLevel = profile.LastLevel,

                StartingRows =
                    profile.StartingRows,

                StartingColumns =
                    profile.StartingColumns,

                RowIncrease =
                    profile.RowIncrease,

                ColumnIncrease =
                    profile.ColumnIncrease,

                MaximumRows =
                    profile.MaximumRows,

                MaximumColumns =
                    profile.MaximumColumns,

                MinimumCrates =
                    profile.MinimumCrates,

                MaximumCrates =
                    profile.MaximumCrates,

                MinimumCrateDistance =
                    profile.MinimumCrateDistance,

                MaximumCrateDistance =
                    profile.MaximumCrateDistance,

                MinimumInteriorWalls =
                    profile.MinimumInteriorWalls,

                MaximumInteriorWalls =
                    profile.MaximumInteriorWalls,

                MinimumSolutionPushes =
                    minimumPushes,

                MaximumSolutionPushes =
                    maximumPushes,

                MinimumPushesIncreasePerLevel =
                    profile.MinimumPushesIncreasePerLevel,

                MaximumPushesIncreasePerLevel =
                    profile.MaximumPushesIncreasePerLevel,

                MaximumAttempts =
                    profile.MaximumAttempts
            };
        }
    }
}