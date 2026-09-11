namespace NovaWrightNumberPush
{
    public partial class NumberPushForm : Form
    {
        private readonly NumberPushGame game;
        private readonly NumberPushProgressRepository progressRepository;

        private NumberPushProgress progress = null!;
        private NumberPushGameConfiguration configuration = null!;
        private NumberPushLevel currentLevel = null!;

        private Point playerPosition;

        private List<NumberPushCrate> crates = new();

        private int pushCount;

        private bool levelComplete;

        private const int BoardColumns = 12;
        private const int BoardRows = 10;
        private const int CellSize = 50;

        public NumberPushForm(
    NumberPushGame game,
    NumberPushLevel level)
        {
            this.game =
                game;

            Text =
                game.Info.GameName;

            StartPosition =
                FormStartPosition.CenterScreen;

            ClientSize =
                new Size(
                    900,
                    700);

            KeyPreview = true;
            DoubleBuffered = true;

            progressRepository =
                new NumberPushProgressRepository();

            progress =
                progressRepository.Load();

            configuration =
                game.Configuration;

            BackColor =
                NumberPushColorHelper.FromHex(
                    configuration.BackgroundColor);

            KeyDown +=
                NumberPushForm_KeyDown;

            Paint +=
                NumberPushForm_Paint;

            LoadLevel(
    level);
        }

        private void LoadLevel(
    NumberPushLevel level)
        {
            currentLevel =
                level;

            playerPosition =
                currentLevel.PlayerStart;

            crates =
                currentLevel.Crates
                    .Select(crate =>
                        new NumberPushCrate(
                            crate.Position,
                            crate.Distance))
                    .ToList();

            pushCount = 0;
            levelComplete = false;

            Invalidate();
        }

        private void ResetLevel()
        {
            LoadLevel(
                currentLevel);
        }

        private void NumberPushForm_KeyDown(
            object? sender,
            KeyEventArgs e)
        {
            if (e.KeyCode == Keys.R)
            {
                ResetLevel();
                return;
            }

            Point direction = Point.Empty;

            switch (e.KeyCode)
            {
                case Keys.Up:
                case Keys.W:
                    direction = new Point(0, -1);
                    break;

                case Keys.Down:
                case Keys.S:
                    direction = new Point(0, 1);
                    break;

                case Keys.Left:
                case Keys.A:
                    direction = new Point(-1, 0);
                    break;

                case Keys.Right:
                case Keys.D:
                    direction = new Point(1, 0);
                    break;
            }

            if (direction != Point.Empty)
            {
                MovePlayer(direction);
            }
        }

        private void MovePlayer(Point direction)
        {
            if (levelComplete)
            {
                return;
            }

            Point nextPosition = new Point(
                playerPosition.X + direction.X,
                playerPosition.Y + direction.Y);

            if (IsWall(nextPosition))
            {
                return;
            }

            NumberPushCrate? crate =
                GetCrateAt(nextPosition);

            if (crate == null)
            {
                playerPosition = nextPosition;
                Invalidate();
                return;
            }

            if (TryPushCrate(crate, direction))
            {
                playerPosition = nextPosition;
                pushCount++;

                Invalidate();

                if (IsLevelComplete())
                {
                    ShowLevelComplete();
                }
            }
        }

        private bool TryPushCrate(
            NumberPushCrate crate,
            Point direction)
        {
            Point testPosition = crate.Position;

            for (int i = 0;
                 i < crate.Distance;
                 i++)
            {
                testPosition = new Point(
                    testPosition.X + direction.X,
                    testPosition.Y + direction.Y);

                if (IsWall(testPosition))
                {
                    return false;
                }

                if (GetCrateAt(testPosition) != null &&
                    testPosition != crate.Position)
                {
                    return false;
                }
            }

            crate.Position = testPosition;

            return true;
        }

        private bool IsLevelComplete()
        {
            return crates.All(
                crate =>
                    currentLevel.Goals.Contains(
                        crate.Position));
        }

        private void ShowLevelComplete()
        {
            levelComplete = true;

            DialogResult result =
                MessageBox.Show(
                    $"LEVEL {currentLevel.LevelNumber} COMPLETE!\n\n" +
                    $"Pushes: {pushCount}",
                    "Number Push",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);

            progressRepository.RecordCompletion(
    progress,
    currentLevel.LevelNumber,
    pushCount);

            NumberPushLevelRepository levelRepository =
    new NumberPushLevelRepository();

            int? nextLevel =
                levelRepository.GetNextLevelNumber(
                    currentLevel.LevelNumber);

            if (nextLevel.HasValue)
            {
                NumberPushLevel nextLevelData =
                    levelRepository.LoadLevel(
                        nextLevel.Value);

                LoadLevel(
                    nextLevelData);
            }
            else
            {
                Close();
            }
        }

        private bool IsWall(Point position)
        {
            if (position.X < 0 ||
                position.X >= BoardColumns ||
                position.Y < 0 ||
                position.Y >= BoardRows)
            {
                return true;
            }

            return currentLevel.Walls.Any(
                wall =>
                    wall.X == position.X &&
                    wall.Y == position.Y);
        }

        private NumberPushCrate? GetCrateAt(
            Point position)
        {
            return crates.FirstOrDefault(
                crate =>
                    crate.Position == position);
        }

        private void NumberPushForm_Paint(
    object? sender,
    PaintEventArgs e)
        {
            Graphics g = e.Graphics;

            int boardWidth =
                BoardColumns * configuration.CellSize;

            int boardHeight =
                BoardRows * configuration.CellSize;

            int startX =
                (ClientSize.Width - boardWidth) / 2;

            int startY = 120;

            DrawHeader(g);

            using Brush floorBrush =
                new SolidBrush(
                    NumberPushColorHelper.FromHex(
                        configuration.BoardColor));

            using Pen gridPen =
                new Pen(
                    NumberPushColorHelper.FromHex(
                        configuration.GridColor),
                    1);

            for (int row = 0;
                 row < BoardRows;
                 row++)
            {
                for (int column = 0;
                     column < BoardColumns;
                     column++)
                {
                    int x =
                        startX +
                        column * configuration.CellSize;

                    int y =
                        startY +
                        row * configuration.CellSize;

                    g.FillRectangle(
                        floorBrush,
                        x,
                        y,
                        configuration.CellSize,
                        configuration.CellSize);

                    if (configuration.ShowGrid)
                    {
                        g.DrawRectangle(
                            gridPen,
                            x,
                            y,
                            configuration.CellSize,
                            configuration.CellSize);
                    }
                }
            }

            DrawWalls(g, startX, startY);
            DrawGoals(g, startX, startY);
            DrawCrates(g, startX, startY);
            DrawPlayer(g, startX, startY);
            DrawStatus(g);
        }

        private void DrawHeader(Graphics g)
        {
            using Font titleFont =
                new Font(
                    "Segoe UI",
                    20,
                    FontStyle.Bold);

            using Brush titleBrush =
                new SolidBrush(
                    NumberPushColorHelper.FromHex(
                        configuration.TitleColor));

            g.DrawString(
                configuration.GameTitle,
                titleFont,
                titleBrush,
                30,
                25);

            using Font infoFont =
                new Font(
                    "Segoe UI",
                    10);

            using Brush infoBrush =
                new SolidBrush(
                    NumberPushColorHelper.FromHex(
                        configuration.SecondaryTextColor));

            g.DrawString(
                configuration.GameSubtitle,
                infoFont,
                infoBrush,
                32,
                62);
        }

        private void DrawWalls(
    Graphics g,
    int startX,
    int startY)
        {
            using Brush wallBrush =
                new SolidBrush(
                    NumberPushColorHelper.FromHex(
                        configuration.WallColor));

            foreach (Rectangle wall in currentLevel.Walls)
            {
                int x =
                    startX +
                    wall.X * configuration.CellSize;

                int y =
                    startY +
                    wall.Y * configuration.CellSize;

                g.FillRectangle(
                    wallBrush,
                    x,
                    y,
                    configuration.CellSize,
                    configuration.CellSize);
            }
        }

        private void DrawGoals(
    Graphics g,
    int startX,
    int startY)
        {
            using Pen goalPen =
                new Pen(
                    NumberPushColorHelper.FromHex(
                        configuration.GoalColor),
                    3);

            foreach (Point goal in currentLevel.Goals)
            {
                int x =
                    startX +
                    goal.X * configuration.CellSize;

                int y =
                    startY +
                    goal.Y * configuration.CellSize;

                Rectangle rectangle =
                    new Rectangle(
                        x + 8,
                        y + 8,
                        configuration.CellSize - 16,
                        configuration.CellSize - 16);

                g.DrawEllipse(
                    goalPen,
                    rectangle);
            }
        }

        private void DrawCrates(
    Graphics g,
    int startX,
    int startY)
        {
            using Brush crateBrush =
                new SolidBrush(
                    NumberPushColorHelper.FromHex(
                        configuration.CrateColor));

            using Pen cratePen =
                new Pen(
                    NumberPushColorHelper.FromHex(
                        configuration.CrateBorderColor),
                    2);

            using Font numberFont =
                new Font(
                    "Segoe UI",
                    18,
                    FontStyle.Bold);

            using Brush numberBrush =
                new SolidBrush(
                    NumberPushColorHelper.FromHex(
                        configuration.PrimaryTextColor));

            foreach (NumberPushCrate crate in crates)
            {
                int x =
                    startX +
                    crate.Position.X *
                    configuration.CellSize;

                int y =
                    startY +
                    crate.Position.Y *
                    configuration.CellSize;

                Rectangle rectangle =
                    new Rectangle(
                        x + 5,
                        y + 5,
                        configuration.CellSize - 10,
                        configuration.CellSize - 10);

                g.FillRectangle(
                    crateBrush,
                    rectangle);

                g.DrawRectangle(
                    cratePen,
                    rectangle);

                string number =
                    crate.Distance.ToString();

                SizeF textSize =
                    g.MeasureString(
                        number,
                        numberFont);

                g.DrawString(
                    number,
                    numberFont,
                    numberBrush,
                    x +
                        (configuration.CellSize -
                         textSize.Width) / 2,
                    y +
                        (configuration.CellSize -
                         textSize.Height) / 2);
            }
        }

        private void DrawPlayer(
    Graphics g,
    int startX,
    int startY)
        {
            using Brush playerBrush =
                new SolidBrush(
                    NumberPushColorHelper.FromHex(
                        configuration.PlayerColor));

            int x =
                startX +
                playerPosition.X *
                configuration.CellSize;

            int y =
                startY +
                playerPosition.Y *
                configuration.CellSize;

            Rectangle playerRectangle =
                new Rectangle(
                    x + 12,
                    y + 12,
                    configuration.CellSize - 24,
                    configuration.CellSize - 24);

            g.FillEllipse(
                playerBrush,
                playerRectangle);
        }

        private void DrawStatus(Graphics g)
        {
            using Font statusFont =
                new Font(
                    "Segoe UI",
                    10,
                    FontStyle.Bold);

            using Brush statusBrush =
                new SolidBrush(
                    NumberPushColorHelper.FromHex(
                        configuration.SecondaryTextColor));

            string status =
                $"LEVEL {currentLevel.LevelNumber}    " +
                $"PUSHES {pushCount}    R = RESET";

            SizeF textSize =
                g.MeasureString(
                    status,
                    statusFont);

            g.DrawString(
                status,
                statusFont,
                statusBrush,
                (ClientSize.Width -
                 textSize.Width) / 2,
                650);
        }
    }
}