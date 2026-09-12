namespace NovaWright.NumberPush.LevelGenerator
{
    public class NumberPushDifficulty
    {
        public int LevelNumber { get; }

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
        }
    }
}