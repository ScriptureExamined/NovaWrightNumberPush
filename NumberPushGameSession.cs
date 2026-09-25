namespace NovaWrightNumberPush
{
    public class NumberPushGameSession
    {
        public NumberPushGame Game { get; }

        public NumberPushProgress Progress { get; }

        private readonly NumberPushLevelRepository levelRepository;

        public NumberPushGameSession(
            NumberPushGame game,
            NumberPushProgress progress,
            NumberPushLevelRepository levelRepository
        )
        {
            Game = game;

            Progress = progress;

            this.levelRepository = levelRepository;
        }

        public List<int> GetAvailableLevelNumbers()
        {
            return levelRepository.GetAvailableLevelNumbers();
        }

        public NumberPushLevel? GetLevel(int levelNumber)
        {
            if (!levelRepository.LevelExists(levelNumber))
            {
                return null;
            }

            return levelRepository.LoadLevel(levelNumber);
        }

        public int? GetNextLevelNumber(int currentLevelNumber)
        {
            return levelRepository.GetNextLevelNumber(currentLevelNumber);
        }
    }
}
