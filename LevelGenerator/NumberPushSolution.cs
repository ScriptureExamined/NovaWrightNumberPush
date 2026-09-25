namespace NovaWright.NumberPush.LevelGenerator
{
    public class NumberPushSolution
    {
        public bool IsSolved { get; set; }

        public int MinimumPushes { get; set; }

        public List<NumberPushSolutionStep> Steps { get; set; } = new();

        public Dictionary<int, int> CratePushCounts { get; set; } = new();

        public Dictionary<int, int> CrateFirstPushNumbers { get; set; } = new();

        public Dictionary<int, int> CrateLastPushNumbers { get; set; } = new();

        public Dictionary<int, Point> CrateGoalPositions { get; set; } = new();

        public int CratesMoved
        {
            get { return CratePushCounts.Count; }
        }
    }

    public class NumberPushSolutionStep
    {
        public int PushNumber { get; set; }

        public int CrateIndex { get; set; }

        public int CrateNumber
        {
            get { return CrateIndex + 1; }
        }

        public Point PlayerStart { get; set; }

        public Point PlayerPushPosition { get; set; }

        public Point CrateStart { get; set; }

        public Point CrateEnd { get; set; }

        public Point Direction { get; set; }

        public int Distance { get; set; }

        public List<Point> PlayerPath { get; set; } = new();
    }
}
