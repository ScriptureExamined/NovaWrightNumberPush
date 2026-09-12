using NovaWright.NumberPush.LevelGenerator;

namespace NovaWrightNumberPush
{
    public class NumberPushGeneratorForm : Form
    {
        private readonly NumericUpDown levelNumberInput;
        private readonly NumericUpDown firstLevelInput;
        private readonly NumericUpDown lastLevelInput;
        private readonly NumericUpDown seedInput;

        private readonly Button generateButton;
        private readonly Button generateRangeButton;
        private readonly Button closeButton;

        private readonly Label statusLabel;
        private readonly ProgressBar progressBar;

        private readonly NumberPushGame game;
        private readonly NumberPushGameSession gameSession;

        private readonly CheckBox openLevelCheckBox;
        private readonly CheckBox openMarkdownCheckBox;
        private readonly CheckBox showCrateNumbersCheckBox;

        public NumberPushGeneratorForm(
            NumberPushGame game,
            NumberPushGameSession gameSession)
        {
            this.game = game;

            this.gameSession =
                gameSession;

            Text =
                "Number Push Level Generator";

            StartPosition =
                FormStartPosition.CenterParent;

            ClientSize =
                new Size(
                    600,
                    575);

            BackColor =
                Color.FromArgb(
                    12,
                    15,
                    22);

            Label titleLabel =
                new Label();

            titleLabel.Text =
                "LEVEL GENERATOR";

            titleLabel.Font =
                new Font(
                    "Segoe UI",
                    22,
                    FontStyle.Bold);

            titleLabel.ForeColor =
                Color.FromArgb(
                    70,
                    170,
                    255);

            titleLabel.AutoSize =
                true;

            titleLabel.Location =
                new Point(
                    30,
                    25);

            Controls.Add(
                titleLabel);

            Label levelLabel =
                new Label();

            levelLabel.Text =
                "Level Number";

            levelLabel.Font =
                new Font(
                    "Segoe UI",
                    10);

            levelLabel.ForeColor =
                Color.White;

            levelLabel.AutoSize =
                true;

            levelLabel.Location =
                new Point(
                    30,
                    85);

            Controls.Add(
                levelLabel);

            levelNumberInput =
                new NumericUpDown();

            levelNumberInput.Minimum =
                1;

            levelNumberInput.Maximum =
                42;

            levelNumberInput.Value =
                1;

            levelNumberInput.Font =
                new Font(
                    "Segoe UI",
                    11);

            levelNumberInput.Location =
                new Point(
                    30,
                    115);

            levelNumberInput.Size =
                new Size(
                    150,
                    30);

            Controls.Add(
                levelNumberInput);

            Label rangeLabel =
                new Label();

            rangeLabel.Text =
                "Generate Level Range";

            rangeLabel.Font =
                new Font(
                    "Segoe UI",
                    10);

            rangeLabel.ForeColor =
                Color.White;

            rangeLabel.AutoSize =
                true;

            rangeLabel.Location =
                new Point(
                    30,
                    225);

            Controls.Add(
                rangeLabel);

            firstLevelInput =
                new NumericUpDown();

            firstLevelInput.Minimum =
                1;

            firstLevelInput.Maximum =
                1000;

            firstLevelInput.Value =
                1;

            firstLevelInput.Font =
                new Font(
                    "Segoe UI",
                    11);

            firstLevelInput.Location =
                new Point(
                    30,
                    255);

            firstLevelInput.Size =
                new Size(
                    100,
                    30);

            Controls.Add(
                firstLevelInput);

            lastLevelInput =
                new NumericUpDown();

            lastLevelInput.Minimum =
                1;

            lastLevelInput.Maximum =
                1000;

            lastLevelInput.Value =
                20;

            lastLevelInput.Font =
                new Font(
                    "Segoe UI",
                    11);

            lastLevelInput.Location =
                new Point(
                    140,
                    255);

            lastLevelInput.Size =
                new Size(
                    100,
                    30);

            Controls.Add(
                lastLevelInput);

            Label seedLabel =
                new Label();

            seedLabel.Text =
                "Generator Seed";

            seedLabel.Font =
                new Font(
                    "Segoe UI",
                    10);

            seedLabel.ForeColor =
                Color.White;

            seedLabel.AutoSize =
                true;

            seedLabel.Location =
                new Point(
                    220,
                    85);

            Controls.Add(
                seedLabel);

            seedInput =
                new NumericUpDown();

            seedInput.Minimum =
                int.MinValue;

            seedInput.Maximum =
                int.MaxValue;

            seedInput.Value =
                12345;

            seedInput.Font =
                new Font(
                    "Segoe UI",
                    11);

            seedInput.Location =
                new Point(
                    220,
                    115);

            seedInput.Size =
                new Size(
                    180,
                    30);

            Controls.Add(
                seedInput);

            generateButton =
                new Button();

            generateButton.Text =
                "GENERATE LEVEL";

            generateButton.Font =
                new Font(
                    "Segoe UI",
                    11,
                    FontStyle.Bold);

            generateButton.ForeColor =
                Color.White;

            generateButton.BackColor =
                Color.FromArgb(
                    24,
                    29,
                    41);

            generateButton.FlatStyle =
                FlatStyle.Flat;

            generateButton.FlatAppearance.BorderColor =
                Color.FromArgb(
                    70,
                    170,
                    255);

            generateButton.FlatAppearance.BorderSize =
                1;

            generateButton.Size =
                new Size(
                    180,
                    45);

            generateButton.Location =
                new Point(
                    30,
                    160);

            generateButton.Cursor =
                Cursors.Hand;

            generateButton.Click +=
                GenerateButton_Click;

            Controls.Add(
                generateButton);

            generateRangeButton =
                new Button();

            generateRangeButton.Text =
                "GENERATE RANGE";

            generateRangeButton.Font =
                new Font(
                    "Segoe UI",
                    11,
                    FontStyle.Bold);

            generateRangeButton.ForeColor =
                Color.White;

            generateRangeButton.BackColor =
                Color.FromArgb(
                    24,
                    29,
                    41);

            generateRangeButton.FlatStyle =
                FlatStyle.Flat;

            generateRangeButton.FlatAppearance.BorderColor =
                Color.FromArgb(
                    70,
                    170,
                    255);

            generateRangeButton.FlatAppearance.BorderSize =
                1;

            generateRangeButton.Size =
                new Size(
                    180,
                    45);

            generateRangeButton.Location =
                new Point(
                    30,
                    295);

            generateRangeButton.Cursor =
                Cursors.Hand;

            generateRangeButton.Click +=
                GenerateRangeButton_Click;

            Controls.Add(
                generateRangeButton);

            // --------------------------------------------------------
            // Progress Bar
            // --------------------------------------------------------

            progressBar =
                new ProgressBar();

            progressBar.Style =
                ProgressBarStyle.Marquee;

            progressBar.MarqueeAnimationSpeed =
                30;

            progressBar.Size =
                new Size(
                    540,
                    20);

            progressBar.Location =
                new Point(
                    30,
                    350);

            progressBar.Visible =
                false;

            Controls.Add(
                progressBar);

            // --------------------------------------------------------
            // Status
            // --------------------------------------------------------

            statusLabel =
                new Label();

            statusLabel.Text =
                "Ready.";

            statusLabel.Font =
                new Font(
                    "Segoe UI",
                    10);

            statusLabel.ForeColor =
                Color.LightGray;

            statusLabel.AutoSize =
                true;

            statusLabel.Location =
                new Point(
                    30,
                    380);

            Controls.Add(
                statusLabel);

            // --------------------------------------------------------
            // Options
            // --------------------------------------------------------

            openLevelCheckBox =
                new CheckBox();

            openLevelCheckBox.Text =
                "Open generated level after creation";

            openLevelCheckBox.Checked =
                true;

            openLevelCheckBox.AutoSize =
                true;

            openLevelCheckBox.ForeColor =
                Color.White;

            openLevelCheckBox.Location =
                new Point(
                    230,
                    440);

            Controls.Add(
                openLevelCheckBox);

            openMarkdownCheckBox =
                new CheckBox();

            openMarkdownCheckBox.Text =
                "Open Markdown solution after creation";

            openMarkdownCheckBox.Checked =
                true;

            openMarkdownCheckBox.AutoSize =
                true;

            openMarkdownCheckBox.ForeColor =
                Color.White;

            openMarkdownCheckBox.Location =
                new Point(
                    230,
                    470);

            Controls.Add(
                openMarkdownCheckBox);

            showCrateNumbersCheckBox =
                new CheckBox();

            showCrateNumbersCheckBox.Text =
                "Show crate numbers";

            showCrateNumbersCheckBox.Checked =
                true;

            showCrateNumbersCheckBox.ForeColor =
                Color.White;

            showCrateNumbersCheckBox.AutoSize =
                true;

            showCrateNumbersCheckBox.Location =
                new Point(
                    230,
                    500);

            Controls.Add(
                showCrateNumbersCheckBox);

            // --------------------------------------------------------
            // Close
            // --------------------------------------------------------

            closeButton =
                new Button();

            closeButton.Text =
                "CLOSE";

            closeButton.Font =
                new Font(
                    "Segoe UI",
                    10);

            closeButton.ForeColor =
                Color.White;

            closeButton.BackColor =
                Color.FromArgb(
                    24,
                    29,
                    41);

            closeButton.FlatStyle =
                FlatStyle.Flat;

            closeButton.FlatAppearance.BorderColor =
                Color.FromArgb(
                    90,
                    100,
                    115);

            closeButton.FlatAppearance.BorderSize =
                1;

            closeButton.Size =
                new Size(
                    120,
                    40);

            closeButton.Location =
                new Point(
                    30,
                    465);

            closeButton.Cursor =
                Cursors.Hand;

            closeButton.Click +=
                CloseButton_Click;

            Controls.Add(
                closeButton);
        }

        private async void GenerateRangeButton_Click(
            object? sender,
            EventArgs e)
        {
            int firstLevel =
                (int)firstLevelInput.Value;

            int lastLevel =
                (int)lastLevelInput.Value;

            int seed =
                (int)seedInput.Value;

            if (lastLevel < firstLevel)
            {
                MessageBox.Show(
                    "The last level cannot be smaller than the first level.",
                    "Number Push Generator",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);

                return;
            }

            int levelCount =
                lastLevel - firstLevel + 1;

            statusLabel.Text =
                $"Generating Levels {firstLevel}-{lastLevel}...";

            generateButton.Enabled =
                false;

            generateRangeButton.Enabled =
                false;

            progressBar.Visible =
                true;

            try
            {
                List<NumberPushGenerationResult> results =
                    await Task.Run(
                        () =>
                        {
                            NumberPushGenerationService service =
                                new NumberPushGenerationService(
                                    seed);

                            return service.GenerateRange(
                                firstLevel,
                                lastLevel);
                        });

                NumberPushLevelRepository levelRepository =
                    new NumberPushLevelRepository();

                string outputDirectory =
                    Path.Combine(
                        AppContext.BaseDirectory,
                        "GeneratedLevels");

                int successfulLevels = 0;

                foreach (NumberPushGenerationResult result in results)
                {
                    if (!result.IsSuccessful)
                    {
                        throw new InvalidOperationException(
                            $"Level {result.Level?.LevelNumber ?? 0} could not be generated.");
                    }

                    levelRepository.SaveLevel(
                        result.Level!);

                    NumberPushMarkdownExporter.Export(
                        result.Level!,
                        result.Solution!,
                        result.Difficulty!,
                        seed,
                        outputDirectory);

                    successfulLevels++;
                }

                statusLabel.Text =
                    $"Generated {successfulLevels} levels successfully.\r\n" +
                    $"Levels: {firstLevel}-{lastLevel}\r\n" +
                    $"Seed: {seed}\r\n" +
                    $"Output: {outputDirectory}";

                MessageBox.Show(
                    $"Successfully generated levels {firstLevel}-{lastLevel}.",
                    "Number Push Generator",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                statusLabel.Text =
                    "Range generation failed.";

                MessageBox.Show(
                    ex.Message,
                    "Number Push Generator",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
            }
            finally
            {
                progressBar.Visible =
                    false;

                generateButton.Enabled =
                    true;

                generateRangeButton.Enabled =
                    true;
            }
        }

        private async void GenerateButton_Click(
            object? sender,
            EventArgs e)
        {
            int levelNumber =
                (int)levelNumberInput.Value;

            int seed =
                (int)seedInput.Value;

            statusLabel.Text =
                $"Generating Level {levelNumber}...";

            generateButton.Enabled =
                false;

            progressBar.Visible =
                true;

            try
            {
                NumberPushGenerationResult result =
                    await Task.Run(
                        () =>
                        {
                            NumberPushGenerationService service =
                                new NumberPushGenerationService(
                                    seed);

                            return service.Generate(
                                levelNumber);
                        });

                if (!result.IsSuccessful)
                {
                    statusLabel.Text =
                        "Generation failed.";

                    MessageBox.Show(
                        $"Level {levelNumber} could not be generated.",
                        "Number Push Generator",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning);

                    return;
                }

                NumberPushLevelRepository levelRepository =
                    new NumberPushLevelRepository();

                levelRepository.SaveLevel(
                    result.Level!);

                string projectDirectory =
                    Directory.GetParent(
                        AppContext.BaseDirectory)!
                    .Parent!
                    .Parent!
                    .Parent!
                    .FullName;

                string outputDirectory =
                    Path.Combine(
                        projectDirectory,
                        "Game",
                        "GeneratedLevels");

                string markdownFile =
                    NumberPushMarkdownExporter.Export(
                        result.Level!,
                        result.Solution!,
                        result.Difficulty!,
                        seed,
                        outputDirectory);

                if (openMarkdownCheckBox.Checked)
                {
                    System.Diagnostics.Process.Start(
                        new System.Diagnostics.ProcessStartInfo
                        {
                            FileName = markdownFile,
                            UseShellExecute = true
                        });
                }

                statusLabel.Text =
                    $"Level {levelNumber} generated successfully.\r\n" +
                    $"Complexity: {result.Difficulty!.Complexity}\r\n" +
                    $"Board: {result.Level!.Rows} x {result.Level.Columns}\r\n" +
                    $"Crates: {result.CrateCount}\r\n" +
                    $"Interior walls: {result.InteriorWallCount}\r\n" +
                    $"Solution: {result.Solution!.MinimumPushes} pushes\r\n" +
                    $"Generation: {result.GenerationMilliseconds} ms\r\n" +
                    $"Solver: {result.SolutionMilliseconds} ms\r\n" +
                    $"Seed: {seed}\r\n" +
                    $"Markdown: {markdownFile}";

                Hide();

                if (openLevelCheckBox.Checked)
                {
                    Hide();

                    using NumberPushForm gameForm =
                        new NumberPushForm(
                            game,
                            gameSession,
                            result.Level!,
                            showCrateNumbersCheckBox.Checked);

                    gameForm.ShowDialog(this);

                    Show();
                }
            }
            finally
            {
                progressBar.Visible =
                    false;

                generateButton.Enabled =
                    true;
            }
        }

        private void CloseButton_Click(
            object? sender,
            EventArgs e)
        {
            Close();
        }
    }
}