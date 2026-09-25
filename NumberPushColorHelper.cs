namespace NovaWrightNumberPush
{
    public static class NumberPushColorHelper
    {
        public static Color FromHex(string hex)
        {
            if (string.IsNullOrWhiteSpace(hex))
            {
                throw new ArgumentException("A color value is required.", nameof(hex));
            }

            hex = hex.Trim().TrimStart('#');

            if (hex.Length != 6)
            {
                throw new ArgumentException(
                    "Color values must contain exactly six hexadecimal characters.",
                    nameof(hex)
                );
            }

            int red = Convert.ToInt32(hex.Substring(0, 2), 16);

            int green = Convert.ToInt32(hex.Substring(2, 2), 16);

            int blue = Convert.ToInt32(hex.Substring(4, 2), 16);

            return Color.FromArgb(red, green, blue);
        }
    }
}
