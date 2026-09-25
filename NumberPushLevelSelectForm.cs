namespace NovaWrightNumberPush
{
    public class NumberPushLevelSelectForm : Form
    {
        private readonly NumberPushProgressRepository progressRepository;

        private readonly NumberPushGameRepository gameRepository;

        private readonly NumberPushGame game;

        private readonly NumberPushGameSession gameSession;

        private NumberPushProgress progress;

        private readonly ListBox levelListBox;

        private readonly Button continueButton;
        private readonly Button startButton;
        private readonly Button configurationButton;
        private readonly Button generatorButton;
        private readonly CheckBox showCrateNumbersCheckBox;

        public int SelectedLevelNumber { get; private set; }

        public NumberPushLevelSelectForm()
        {
            Text = "NovaWright Number Push";
            StartPosition = FormStartPosition.CenterScreen;
            ClientSize = new Size(700, 650);
            MinimumSize = new Size(700, 650);

            MaximumSize = new Size(700, 650);
            BackColor = Color.FromArgb(12, 15, 22);

            progressRepository = new NumberPushProgressRepository();

            gameRepository = new NumberPushGameRepository();

            NumberPushLevelRepository levelRepository = new NumberPushLevelRepository();

            progress = progressRepository.Load();

            try
            {
                game = gameRepository.Load();

                gameSession = new NumberPushGameSession(game, progress, levelRepository);
            }
            catch (InvalidOperationException ex)
            {
                MessageBox.Show(
                    ex.Message,
                    "Invalid Number Push Game",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );

                Close();

                return;
            }

            List<int> availableLevelNumbers = gameSession.GetAvailableLevelNumbers();

            progressRepository.UpdateAvailableLevels(progress, availableLevelNumbers);

            Label titleLabel = new Label();

            titleLabel.Text = "NUMBER PUSH";

            titleLabel.Font = new Font("Segoe UI", 24, FontStyle.Bold);

            titleLabel.ForeColor = Color.FromArgb(70, 170, 255);

            titleLabel.AutoSize = true;

            titleLabel.Location = new Point(30, 25);

            Controls.Add(titleLabel);

            Label subtitleLabel = new Label();

            subtitleLabel.Text = "Select a level to play.";

            subtitleLabel.Font = new Font("Segoe UI", 10);

            subtitleLabel.ForeColor = Color.LightGray;

            subtitleLabel.AutoSize = true;

            subtitleLabel.Location = new Point(33, 70);

            Controls.Add(subtitleLabel);

            continueButton = new Button();

            continueButton.Text = $"CONTINUE — LEVEL {progress.HighestUnlockedLevel}";

            continueButton.Font = new Font("Segoe UI", 11, FontStyle.Bold);

            continueButton.ForeColor = Color.White;

            continueButton.BackColor = Color.FromArgb(24, 29, 41);

            continueButton.FlatStyle = FlatStyle.Flat;

            continueButton.FlatAppearance.BorderColor = Color.FromArgb(70, 170, 255);

            continueButton.FlatAppearance.BorderSize = 1;

            continueButton.Size = new Size(300, 45);

            continueButton.Location = new Point(30, 105);

            continueButton.Cursor = Cursors.Hand;

            continueButton.Click += ContinueButton_Click;

            Controls.Add(continueButton);

            configurationButton = new Button();

            configurationButton.Text = "Configuration";

            configurationButton.Size = new Size(180, 45);

            configurationButton.Font = new Font("Segoe UI", 11, FontStyle.Bold);

            configurationButton.ForeColor = Color.White;

            configurationButton.BackColor = Color.FromArgb(24, 29, 41);

            configurationButton.FlatStyle = FlatStyle.Flat;

            configurationButton.FlatAppearance.BorderColor = Color.FromArgb(70, 170, 255);

            configurationButton.FlatAppearance.BorderSize = 1;

            configurationButton.Cursor = Cursors.Hand;

            configurationButton.Location = new Point(260, 530);

            configurationButton.Click += ConfigurationButton_Click;

            Controls.Add(configurationButton);

            generatorButton = new Button();

            generatorButton.Text = "Level Generator";

            generatorButton.Size = new Size(180, 45);

            generatorButton.Font = new Font("Segoe UI", 11, FontStyle.Bold);

            generatorButton.ForeColor = Color.White;

            generatorButton.BackColor = Color.FromArgb(24, 29, 41);

            generatorButton.FlatStyle = FlatStyle.Flat;

            generatorButton.FlatAppearance.BorderColor = Color.FromArgb(70, 170, 255);

            generatorButton.FlatAppearance.BorderSize = 1;

            generatorButton.Cursor = Cursors.Hand;

            generatorButton.Location = new Point(470, 530);

            generatorButton.Click += GeneratorButton_Click;

            Controls.Add(generatorButton);

            showCrateNumbersCheckBox = new CheckBox();

            showCrateNumbersCheckBox.Text = "Show crate numbers";

            showCrateNumbersCheckBox.Font = new Font("Segoe UI", 9);

            showCrateNumbersCheckBox.ForeColor = Color.LightGray;

            showCrateNumbersCheckBox.AutoSize = true;

            showCrateNumbersCheckBox.Location = new Point(30, 585);

            showCrateNumbersCheckBox.Checked = false;

            Controls.Add(showCrateNumbersCheckBox);

            levelListBox = new ListBox();

            levelListBox.Font = new Font("Segoe UI", 10);

            levelListBox.BackColor = Color.FromArgb(24, 29, 41);

            levelListBox.ForeColor = Color.White;

            levelListBox.BorderStyle = BorderStyle.FixedSingle;

            levelListBox.Location = new Point(30, 170);

            levelListBox.Size = new Size(640, 340);

            levelListBox.SelectedIndexChanged += LevelListBox_SelectedIndexChanged;

            Controls.Add(levelListBox);

            startButton = new Button();

            startButton.Text = "START LEVEL";

            startButton.Font = new Font("Segoe UI", 11, FontStyle.Bold);

            startButton.ForeColor = Color.White;

            startButton.BackColor = Color.FromArgb(24, 29, 41);

            startButton.FlatStyle = FlatStyle.Flat;

            startButton.FlatAppearance.BorderColor = Color.FromArgb(70, 170, 255);

            startButton.FlatAppearance.BorderSize = 1;

            startButton.Size = new Size(180, 45);

            startButton.Location = new Point(50, 530);

            startButton.Enabled = false;

            startButton.Cursor = Cursors.Hand;

            startButton.Click += StartButton_Click;

            Controls.Add(startButton);

            LoadLevels();
        }

        private void LoadLevels()
        {
            levelListBox.Items.Clear();

            List<int> levelNumbers = gameSession.GetAvailableLevelNumbers();

            foreach (int levelNumber in levelNumbers)
            {
                NumberPushLevelProgress? levelProgress = progressRepository.GetLevelProgress(
                    progress,
                    levelNumber
                );

                string status;

                if (levelNumber > progress.HighestUnlockedLevel)
                {
                    status = "LOCKED";
                }
                else if (levelProgress?.Completed == true)
                {
                    if (levelProgress.BestPushes.HasValue)
                    {
                        status = $"Completed — Best: " + $"{levelProgress.BestPushes.Value} pushes";
                    }
                    else
                    {
                        status = "Completed";
                    }
                }
                else
                {
                    status = "Unlocked";
                }

                levelListBox.Items.Add(
                    new LevelListItem(levelNumber, $"Level {levelNumber} — {status}")
                );
            }
        }

        private void ContinueButton_Click(object? sender, EventArgs e)
        {
            int levelNumber = progress.HighestUnlockedLevel;

            List<int> availableLevels = gameSession.GetAvailableLevelNumbers();

            if (!availableLevels.Contains(levelNumber))
            {
                int? nextLevel = availableLevels
                    .Where(candidate => candidate >= levelNumber)
                    .Cast<int?>()
                    .FirstOrDefault();

                if (!nextLevel.HasValue)
                {
                    MessageBox.Show(
                        "There are no more available levels.",
                        "Number Push",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information
                    );

                    return;
                }

                levelNumber = nextLevel.Value;
            }

            StartLevel(levelNumber);
        }

        private void ConfigurationButton_Click(object? sender, EventArgs e)
        {
            using NumberPushConfigurationForm configurationForm = new NumberPushConfigurationForm();

            configurationForm.ShowDialog(this);

            game.Configuration = new NumberPushGameConfigurationRepository().Load();
        }

        private void GeneratorButton_Click(object? sender, EventArgs e)
        {
            using NumberPushGeneratorForm generatorForm = new NumberPushGeneratorForm(
                game,
                gameSession
            );

            generatorForm.ShowDialog(this);

            LoadLevels();
        }

        private void LevelListBox_SelectedIndexChanged(object? sender, EventArgs e)
        {
            startButton.Enabled = false;

            if (levelListBox.SelectedItem is not LevelListItem item)
            {
                return;
            }

            if (item.LevelNumber > progress.HighestUnlockedLevel)
            {
                return;
            }

            SelectedLevelNumber = item.LevelNumber;

            startButton.Enabled = true;
        }

        private void StartButton_Click(object? sender, EventArgs e)
        {
            if (SelectedLevelNumber <= 0)
            {
                return;
            }

            StartLevel(SelectedLevelNumber);
        }

        private void StartLevel(int levelNumber)
        {
            NumberPushLevel? level = gameSession.GetLevel(levelNumber);

            if (level == null)
            {
                MessageBox.Show(
                    $"Level {levelNumber} could not be found.",
                    "Number Push",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );

                return;
            }

            Hide();

            using NumberPushForm gameForm = new NumberPushForm(
                game,
                gameSession,
                level,
                showCrateNumbersCheckBox.Checked
            );

            gameForm.ShowDialog(this);

            progress = progressRepository.Load();

            LoadLevels();

            Show();
        }

        private class LevelListItem
        {
            public int LevelNumber { get; }

            private readonly string displayText;

            public LevelListItem(int levelNumber, string displayText)
            {
                LevelNumber = levelNumber;

                this.displayText = displayText;
            }

            public override string ToString()
            {
                return displayText;
            }
        }
    }
}
