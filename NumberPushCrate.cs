namespace NovaWrightNumberPush
{
    public class NumberPushCrate
    {
        public Point Position { get; set; }

        public int Distance { get; set; }

        public NumberPushCrate(Point position, int distance)
        {
            Position = position;
            Distance = distance;
        }
    }
}
