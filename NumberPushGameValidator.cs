namespace NovaWrightNumberPush
{
    public static class NumberPushGameValidator
    {
        public static List<string> Validate(
            NumberPushGame game,
            NumberPushLevelRepository levelRepository)
        {
            List<string> errors =
                new List<string>();

            if (game == null)
            {
                errors.Add(
                    "The game could not be loaded.");

                return errors;
            }

            ValidateGameInfo(
                game,
                errors);

            ValidateConfiguration(
                game,
                errors);

            ValidateLevels(
                levelRepository,
                errors);

            return errors;
        }

        private static void ValidateGameInfo(
            NumberPushGame game,
            List<string> errors)
        {
            if (game.Info == null)
            {
                errors.Add(
                    "Game information is missing.");

                return;
            }

            if (string.IsNullOrWhiteSpace(
                    game.Info.GameName))
            {
                errors.Add(
                    "Game name is required.");
            }

            if (string.IsNullOrWhiteSpace(
                    game.Info.DeveloperName))
            {
                errors.Add(
                    "Developer name is required.");
            }

            if (string.IsNullOrWhiteSpace(
                    game.Info.Version))
            {
                errors.Add(
                    "Game version is required.");
            }
        }

        private static void ValidateConfiguration(
            NumberPushGame game,
            List<string> errors)
        {
            if (game.Configuration == null)
            {
                errors.Add(
                    "Game configuration is missing.");

                return;
            }

            ValidateColor(
                game.Configuration.BackgroundColor,
                "BackgroundColor",
                errors);

            ValidateColor(
                game.Configuration.BoardColor,
                "BoardColor",
                errors);

            ValidateColor(
                game.Configuration.GridColor,
                "GridColor",
                errors);

            ValidateColor(
                game.Configuration.WallColor,
                "WallColor",
                errors);

            ValidateColor(
                game.Configuration.GoalColor,
                "GoalColor",
                errors);

            ValidateColor(
                game.Configuration.CrateColor,
                "CrateColor",
                errors);

            ValidateColor(
                game.Configuration.CrateBorderColor,
                "CrateBorderColor",
                errors);

            ValidateColor(
                game.Configuration.PlayerColor,
                "PlayerColor",
                errors);

            ValidateColor(
                game.Configuration.PrimaryTextColor,
                "PrimaryTextColor",
                errors);

            ValidateColor(
                game.Configuration.SecondaryTextColor,
                "SecondaryTextColor",
                errors);

            ValidateColor(
                game.Configuration.TitleColor,
                "TitleColor",
                errors);

            if (game.Configuration.CellSize < 20 ||
                game.Configuration.CellSize > 100)
            {
                errors.Add(
                    "Cell size must be between 20 and 100.");
            }
        }

        private static void ValidateLevels(
            NumberPushLevelRepository levelRepository,
            List<string> errors)
        {
            List<int> levelNumbers =
                levelRepository.GetAvailableLevelNumbers();

            if (levelNumbers.Count == 0)
            {
                errors.Add(
                    "The game must contain at least one level.");

                return;
            }

            HashSet<int> validatedLevelNumbers =
                new HashSet<int>();

            foreach (int levelNumber in levelNumbers)
            {
                NumberPushLevel level;

                try
                {
                    level =
                        levelRepository.LoadLevel(
                            levelNumber);
                }
                catch (Exception ex)
                {
                    errors.Add(
                        $"Level {levelNumber} could not be loaded: " +
                        ex.Message);

                    continue;
                }

                ValidateLevel(
                    level,
                    validatedLevelNumbers,
                    errors);
            }
        }

        private static void ValidateLevel(
            NumberPushLevel level,
            HashSet<int> levelNumbers,
            List<string> errors)
        {
            if (level == null)
            {
                errors.Add(
                    "The game contains a missing level.");

                return;
            }

            if (!levelNumbers.Add(
                    level.LevelNumber))
            {
                errors.Add(
                    $"Level {level.LevelNumber} is duplicated.");
            }

            if (level.LevelNumber <= 0)
            {
                errors.Add(
                    "A level has an invalid level number.");
            }

            if (level.Rows <= 0 ||
                level.Columns <= 0)
            {
                errors.Add(
                    $"Level {level.LevelNumber} has invalid board dimensions.");

                return;
            }

            ValidatePosition(
                level.PlayerStart,
                level,
                "player start",
                errors);

            ValidateWalls(
                level,
                errors);

            ValidateGoals(
                level,
                errors);

            ValidateCrates(
                level,
                errors);
        }

        private static void ValidateWalls(
            NumberPushLevel level,
            List<string> errors)
        {
            foreach (Rectangle wall in level.Walls)
            {
                if (wall.X < 0 ||
                    wall.Y < 0 ||
                    wall.X >= level.Columns ||
                    wall.Y >= level.Rows)
                {
                    errors.Add(
                        $"Level {level.LevelNumber} contains a wall outside the board.");
                }

                if (wall.Width <= 0 ||
                    wall.Height <= 0)
                {
                    errors.Add(
                        $"Level {level.LevelNumber} contains a wall with invalid dimensions.");
                }

                if (wall.X + wall.Width > level.Columns ||
                    wall.Y + wall.Height > level.Rows)
                {
                    errors.Add(
                        $"Level {level.LevelNumber} contains a wall that extends outside the board.");
                }
            }
        }

        private static void ValidateGoals(
            NumberPushLevel level,
            List<string> errors)
        {
            foreach (Point goal in level.Goals)
            {
                ValidatePosition(
                    goal,
                    level,
                    "goal",
                    errors);
            }

            if (level.Goals.Count == 0)
            {
                errors.Add(
                    $"Level {level.LevelNumber} has no goals.");
            }
        }

        private static void ValidateCrates(
            NumberPushLevel level,
            List<string> errors)
        {
            foreach (NumberPushCrate crate in level.Crates)
            {
                if (crate == null)
                {
                    errors.Add(
                        $"Level {level.LevelNumber} contains a missing crate.");

                    continue;
                }

                ValidatePosition(
                    crate.Position,
                    level,
                    "crate",
                    errors);

                if (crate.Distance <= 0)
                {
                    errors.Add(
                        $"Level {level.LevelNumber} contains a crate with an invalid distance.");
                }
            }

            if (level.Crates.Count == 0)
            {
                errors.Add(
                    $"Level {level.LevelNumber} has no crates.");
            }

            if (level.Crates.Count !=
                level.Goals.Count)
            {
                errors.Add(
                    $"Level {level.LevelNumber} has " +
                    $"{level.Crates.Count} crates but " +
                    $"{level.Goals.Count} goals.");
            }
        }

        private static void ValidatePosition(
            Point position,
            NumberPushLevel level,
            string objectName,
            List<string> errors)
        {
            if (position.X < 0 ||
                position.Y < 0 ||
                position.X >= level.Columns ||
                position.Y >= level.Rows)
            {
                errors.Add(
                    $"Level {level.LevelNumber} has a {objectName} outside the board.");
            }
        }

        private static void ValidateColor(
            string color,
            string propertyName,
            List<string> errors)
        {
            try
            {
                NumberPushColorHelper.FromHex(
                    color);
            }
            catch
            {
                errors.Add(
                    $"{propertyName} contains an invalid color value.");
            }
        }
    }
}