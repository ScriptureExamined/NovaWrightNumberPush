using NovaWright.NumberPush.LevelGenerator;
using System.Diagnostics;

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

        private CheckBox reportDiagnosticsCheckBox = null!;

        public NumberPushGeneratorForm(
            NumberPushGame game,
            NumberPushGameSession gameSession)
        {
            this.game =
                game;

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
                9;

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
                9;

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

            openLevelCheckBox =
                new CheckBox();

            openLevelCheckBox.Text =
                "Open generated level after creation";

            openLevelCheckBox.Checked =
                false;

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
                false;

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
                false;

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

            reportDiagnosticsCheckBox =
                new CheckBox();

            reportDiagnosticsCheckBox.Text =
                "Report diagnostic data";

            reportDiagnosticsCheckBox.AutoSize =
                true;

            reportDiagnosticsCheckBox.Checked =
                true;

            reportDiagnosticsCheckBox.Location =
                new Point(
                    230,
                    525);

            reportDiagnosticsCheckBox.ForeColor =
                Color.White;

            Controls.Add(
                reportDiagnosticsCheckBox);

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

            bool reportDiagnostics =
                reportDiagnosticsCheckBox.Checked;

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

            DateTime startTime =
                DateTime.Now;

            Stopwatch creationTimer =
                Stopwatch.StartNew();

            statusLabel.Text =
                $"Generating level 1 of {levelCount}...";

            generateButton.Enabled =
                false;

            generateRangeButton.Enabled =
                false;

            progressBar.Style =
                ProgressBarStyle.Continuous;

            progressBar.Minimum =
                0;

            progressBar.Maximum =
                levelCount;

            progressBar.Value =
                0;

            progressBar.Visible =
                true;

            Progress<int> generationProgress =
    new Progress<int>(
        completed =>
        {
            int currentLevel =
                Math.Min(
                    completed + 1,
                    levelCount);

            statusLabel.Text =
                $"Generating level {currentLevel} of {levelCount}...";

            progressBar.Value =
                Math.Min(
                    completed,
                    progressBar.Maximum);
        });

            try
            {
                List<NumberPushGenerationResult> results =
                    await Task.Run(
                        () =>
                        {
                            NumberPushGenerationService service =
                                new NumberPushGenerationService(
                                    seed,
                                    reportDiagnostics);

                            return service.GenerateRange(
                                firstLevel,
                                lastLevel,
                                generationProgress);
                        });

                creationTimer.Stop();

                NumberPushLevelRepository levelRepository =
                    new NumberPushLevelRepository();

                string outputDirectory =
                    Path.Combine(
                        AppContext.BaseDirectory,
                        "GeneratedLevels");

                if (reportDiagnostics)
                {
                    string projectDirectory =
                        Directory.GetParent(
                            AppContext.BaseDirectory)!
                        .Parent!
                        .Parent!
                        .Parent!
                        .FullName;

                    string diagnosticsDirectory =
                        Path.Combine(
                            projectDirectory,
                            "Game",
                            "GeneratedLevels");

                    Directory.CreateDirectory(
                        diagnosticsDirectory);

                    string diagnosticsFile =
                        Path.Combine(
                            diagnosticsDirectory,
                            $"GenerationDiagnostics_{firstLevel:D3}-{lastLevel:D3}.txt");

                    List<string> reportLines =
                        new List<string>();

                    reportLines.Add(
                        "NUMBER PUSH GENERATION DIAGNOSTICS");

                    reportLines.Add(
                        $"Levels: {firstLevel}-{lastLevel}");

                    reportLines.Add(
                        $"Seed: {seed}");

                    reportLines.Add(
                        "");

                    foreach (NumberPushGenerationResult result in results)
                    {
                        if (result.Diagnostics == null)
                        {
                            continue;
                        }

                        reportLines.Add(
                            "========================================");

                        reportLines.Add(
                            result.Diagnostics.GetReportText());

                        reportLines.Add(
                            "");
                    }

                    TimeSpan totalCreationTime =
                        creationTimer.Elapsed;

                    reportLines.Add(
                        "========================================");

                    reportLines.Add(
                        $"TOTAL CREATION TIME: {totalCreationTime:hh\\:mm\\:ss}");

                    reportLines.Add(
                        $"START TIME:          {startTime:yyyy-MM-dd hh:mm:ss tt}");

                    reportLines.Add(
                        "========================================");

                    File.WriteAllLines(
                        diagnosticsFile,
                        reportLines);
                }

                int successfulLevels =
                    0;

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
                creationTimer.Stop();

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

                progressBar.Style =
                    ProgressBarStyle.Marquee;
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

            bool reportDiagnostics =
                reportDiagnosticsCheckBox.Checked;

            DateTime startTime =
                DateTime.Now;

            Stopwatch creationTimer =
                Stopwatch.StartNew();

            statusLabel.Text =
                $"Generating Level {levelNumber}...";

            generateButton.Enabled =
                false;

            generateRangeButton.Enabled =
                false;

            progressBar.Style =
                ProgressBarStyle.Marquee;

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
                                    seed,
                                    reportDiagnostics);

                            return service.Generate(
                                levelNumber);
                        });

                creationTimer.Stop();

                if (!result.IsSuccessful)
                {
                    statusLabel.Text =
                        "Generation failed.";

                    if (reportDiagnostics &&
                        result.Diagnostics != null)
                    {
                        string failureProjectDirectory =
                            Directory.GetParent(
                                AppContext.BaseDirectory)!
                            .Parent!
                            .Parent!
                            .Parent!
                            .FullName;

                        string diagnosticsDirectory =
                            Path.Combine(
                                failureProjectDirectory,
                                "Game",
                                "GeneratedLevels");

                        Directory.CreateDirectory(
                            diagnosticsDirectory);

                        string diagnosticsFile =
                            Path.Combine(
                                diagnosticsDirectory,
                                $"GenerationDiagnostics_{levelNumber:D3}-{levelNumber:D3}.txt");

                        List<string> reportLines =
                            new List<string>();

                        reportLines.Add(
                            "NUMBER PUSH GENERATION DIAGNOSTICS");

                        reportLines.Add(
                            "GENERATION FAILED");

                        reportLines.Add(
                            $"Level: {levelNumber}");

                        reportLines.Add(
                            $"Seed: {seed}");

                        reportLines.Add(
                            "");

                        reportLines.Add(
                            "========================================");

                        reportLines.Add(
                            result.Diagnostics.GetReportText());

                        reportLines.Add(
                            "");

                        TimeSpan totalCreationTime =
                            creationTimer.Elapsed;

                        reportLines.Add(
                            "========================================");

                        reportLines.Add(
                            $"GENERATION FAILED AFTER: {totalCreationTime:hh\\:mm\\:ss}");

                        reportLines.Add(
                            $"START TIME:              {startTime:yyyy-MM-dd HH:mm:ss}");

                        reportLines.Add(
                            "========================================");

                        File.WriteAllLines(
                            diagnosticsFile,
                            reportLines);

                        MessageBox.Show(
                            $"Level {levelNumber} could not be generated.\r\n\r\n" +
                            $"Diagnostics were saved to:\r\n{diagnosticsFile}",
                            "Number Push Generator",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Warning);
                    }
                    else
                    {
                        MessageBox.Show(
                            $"Level {levelNumber} could not be generated.",
                            "Number Push Generator",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Warning);
                    }

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

                Directory.CreateDirectory(
                    outputDirectory);

                string markdownFile =
                    NumberPushMarkdownExporter.Export(
                        result.Level!,
                        result.Solution!,
                        result.Difficulty!,
                        seed,
                        outputDirectory);

                if (reportDiagnostics &&
                    result.Diagnostics != null)
                {
                    string diagnosticsDirectory =
                        Path.Combine(
                            projectDirectory,
                            "Game",
                            "GeneratedLevels");

                    Directory.CreateDirectory(
                        diagnosticsDirectory);

                    string diagnosticsFile =
                        Path.Combine(
                            diagnosticsDirectory,
                            $"GenerationDiagnostics_{levelNumber:D3}-{levelNumber:D3}.txt");

                    List<string> reportLines =
                        new List<string>();

                    reportLines.Add(
                        "NUMBER PUSH GENERATION DIAGNOSTICS");

                    reportLines.Add(
                        $"Levels: {levelNumber}-{levelNumber}");

                    reportLines.Add(
                        $"Seed: {seed}");

                    reportLines.Add(
                        "");

                    reportLines.Add(
                        "========================================");

                    reportLines.Add(
                        result.Diagnostics.GetReportText());

                    reportLines.Add(
                        "");

                    TimeSpan totalCreationTime =
                        creationTimer.Elapsed;

                    reportLines.Add(
                        "========================================");

                    reportLines.Add(
                        $"TOTAL CREATION TIME: {totalCreationTime:hh\\:mm\\:ss}");

                    reportLines.Add(
                        $"START TIME:          {startTime:yyyy-MM-dd HH:mm:ss}");

                    reportLines.Add(
                        "========================================");

                    File.WriteAllLines(
                        diagnosticsFile,
                        reportLines);
                }

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

                if (openLevelCheckBox.Checked)
                {
                    Hide();

                    using NumberPushForm gameForm =
                        new NumberPushForm(
                            game,
                            gameSession,
                            result.Level!,
                            showCrateNumbersCheckBox.Checked);

                    gameForm.ShowDialog(
                        this);

                    Show();
                }
                else
                {
                    MessageBox.Show(
                        $"Successfully generated level {levelNumber}.",
                        "Number Push Generator",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                creationTimer.Stop();

                statusLabel.Text =
                    "Generation failed.";

                MessageBox.Show(
                    ex.ToString(),
                    "Number Push Generator",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
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

        private void CloseButton_Click(
            object? sender,
            EventArgs e)
        {
            Close();
        }
    }
}