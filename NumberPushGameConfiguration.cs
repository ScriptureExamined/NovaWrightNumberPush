namespace NovaWrightNumberPush
{
    public class NumberPushGameConfiguration
    {
        public string GameTitle { get; set; } =
            "NUMBER PUSH";

        public string GameSubtitle { get; set; } =
            "Push each numbered crate exactly its numbered distance.";

        public string BackgroundColor { get; set; } =
            "#0C0F16";

        public string BoardColor { get; set; } =
            "#181D29";

        public string GridColor { get; set; } =
            "#2D3746";

        public string WallColor { get; set; } =
            "#374152";

        public string GoalColor { get; set; } =
            "#50D296";

        public string CrateColor { get; set; } =
            "#965F2D";

        public string CrateBorderColor { get; set; } =
            "#DCA04B";

        public string CrateNumberColor { get; set; } =
    "#FFFFFF";

        public string CrateDistanceColor { get; set; } =
            "#FFFFFF";

        public string PlayerColor { get; set; } =
            "#46AAFF";

        public string PrimaryTextColor { get; set; } =
            "#FFFFFF";

        public string SecondaryTextColor { get; set; } =
            "#D3D3D3";

        public string TitleColor { get; set; } =
            "#46AAFF";

        public bool ShowGrid { get; set; } = true;

        public int CellSize { get; set; } = 50;
    }
}