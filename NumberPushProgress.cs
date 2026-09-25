namespace NovaWrightNumberPush
{
    public class NumberPushProgress
    {
        public int HighestUnlockedLevel { get; set; } = 1;

        public int CurrentLevel { get; set; } = 1;

        public List<NumberPushLevelProgress> Levels { get; set; } = new();
    }
}
