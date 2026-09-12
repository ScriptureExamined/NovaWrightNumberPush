using NovaWrightNumberPush;

namespace NovaWright.NumberPush.LevelGenerator
{
    public class NumberPushGenerationResult
    {
        public NumberPushLevel? Level { get; set; }

        public NumberPushSolution? Solution { get; set; }

        public DifficultyProfile? Profile { get; set; }

        public DifficultySettings? Settings { get; set; }

        public long GenerationMilliseconds { get; set; }

        public long SolutionMilliseconds { get; set; }

        public long TotalMilliseconds
        {
            get
            {
                return
                    GenerationMilliseconds +
                    SolutionMilliseconds;
            }
        }

        public bool IsSuccessful
        {
            get
            {
                return
                    Level != null &&
                    Solution != null &&
                    Solution.IsSolved;
            }
        }

        public int InteriorWallCount
        {
            get
            {
                if (Level == null)
                {
                    return 0;
                }

                int outerWallCount =
                    (Level.Columns * 2) +
                    ((Level.Rows - 2) * 2);

                return
                    Level.Walls.Count -
                    outerWallCount;
            }
        }

        public int CrateCount
        {
            get
            {
                return
                    Level?.Crates.Count ?? 0;
            }
        }
    }
}