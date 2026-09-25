namespace NovaWrightNumberPush
{
    public class NumberPushGameRepository
    {
        private readonly string gameFolder;

        private readonly NumberPushGameInfoRepository gameInfoRepository;
        private readonly NumberPushGameConfigurationRepository configurationRepository;
        private readonly NumberPushLevelRepository levelRepository;

        public NumberPushGameRepository()
        {
            gameFolder = Path.Combine(AppContext.BaseDirectory, "Game");

            Directory.CreateDirectory(gameFolder);

            gameInfoRepository = new NumberPushGameInfoRepository();

            configurationRepository = new NumberPushGameConfigurationRepository();

            levelRepository = new NumberPushLevelRepository();
        }

        public NumberPushGame Load()
        {
            NumberPushGame game = new NumberPushGame
            {
                Info = gameInfoRepository.Load(),

                Configuration = configurationRepository.Load(),
            };

            List<string> validationErrors = NumberPushGameValidator.Validate(game, levelRepository);

            if (validationErrors.Count > 0)
            {
                throw new InvalidOperationException(
                    "The Number Push game contains invalid data:\n\n"
                        + string.Join("\n", validationErrors)
                );
            }

            return game;
        }
    }
}
