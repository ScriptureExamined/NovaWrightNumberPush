namespace NovaWrightNumberPush
{
    public enum NumberPushMode
    {
        Exact,
        BlockingAllowed,
    }

    public class NumberPushLevel
    {
        public int LevelNumber { get; set; }

        public int Rows { get; set; }

        public int Columns { get; set; }

        public NumberPushMode PushMode { get; set; } = NumberPushMode.Exact;

        public Point PlayerStart { get; set; }

        public List<Rectangle> Walls { get; set; } = new();

        public List<Point> Goals { get; set; } = new();

        public List<NumberPushCrate> Crates { get; set; } = new();
    }
}
