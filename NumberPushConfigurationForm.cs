using System.Drawing.Drawing2D;

namespace NovaWrightNumberPush
{
    public class NumberPushConfigurationForm : Form
    {
        private readonly NumberPushGameConfigurationRepository configurationRepository;
        private readonly NumberPushGameInfoRepository gameInfoRepository;

        private NumberPushGameConfiguration configuration;
        private NumberPushGameInfo gameInfo;

        private readonly TextBox gameNameTextBox;
        private readonly TextBox developerNameTextBox;
        private readonly TextBox versionTextBox;
        private readonly TextBox descriptionTextBox;

        private readonly TextBox gameTitleTextBox;
        private readonly TextBox gameSubtitleTextBox;

        private readonly TextBox backgroundColorTextBox;
        private readonly TextBox boardColorTextBox;
        private readonly TextBox gridColorTextBox;
        private readonly TextBox wallColorTextBox;
        private readonly TextBox goalColorTextBox;
        private readonly TextBox crateColorTextBox;
        private readonly TextBox crateBorderColorTextBox;
        private readonly TextBox crateNumberColorTextBox;
        private readonly TextBox crateDistanceColorTextBox;
        private readonly TextBox playerColorTextBox;
        private readonly TextBox primaryTextColorTextBox;
        private readonly TextBox secondaryTextColorTextBox;
        private readonly TextBox titleColorTextBox;

        private readonly CheckBox showGridCheckBox;
        private readonly NumericUpDown cellSizeNumericUpDown;

        private readonly Panel previewPanel;

        private readonly Button saveButton;
        private readonly Button resetButton;
        private readonly Button cancelButton;

        public NumberPushConfigurationForm()
        {
            Text =
                "Number Push Configuration";

            StartPosition =
                FormStartPosition.CenterParent;

            ClientSize =
                new Size(
                    1100,
                    720);

            MinimumSize =
                new Size(
                    1100,
                    720);

            FormBorderStyle =
                FormBorderStyle.FixedSingle;

            MaximizeBox = false;

            BackColor =
                Color.FromArgb(
                    230,
                    235,
                    241);

            Font =
                new Font(
                    "Segoe UI",
                    9F);

            configurationRepository =
                new NumberPushGameConfigurationRepository();

            gameInfoRepository =
                new NumberPushGameInfoRepository();

            configuration =
                configurationRepository.Load();

            gameInfo =
                gameInfoRepository.Load();

            gameNameTextBox =
                new TextBox();

            developerNameTextBox =
                new TextBox();

            versionTextBox =
                new TextBox();

            descriptionTextBox =
                new TextBox();

            gameTitleTextBox =
                new TextBox();

            gameSubtitleTextBox =
                new TextBox();

            backgroundColorTextBox =
                new TextBox();

            boardColorTextBox =
                new TextBox();

            gridColorTextBox =
                new TextBox();

            wallColorTextBox =
                new TextBox();

            goalColorTextBox =
                new TextBox();

            crateColorTextBox =
                new TextBox();

            crateBorderColorTextBox =
                new TextBox();
            crateNumberColorTextBox =
                new TextBox();

            crateDistanceColorTextBox =
                new TextBox();

            playerColorTextBox =
                new TextBox();

            primaryTextColorTextBox =
                new TextBox();

            secondaryTextColorTextBox =
                new TextBox();

            titleColorTextBox =
                new TextBox();

            showGridCheckBox =
                new CheckBox();

            cellSizeNumericUpDown =
                new NumericUpDown();

            previewPanel =
                new Panel();

            saveButton =
                new Button();

            resetButton =
                new Button();

            cancelButton =
                new Button();

            BuildInterface();

            LoadConfiguration();
        }

        private void BuildInterface()
        {
            Label headingLabel =
                new Label
                {
                    Text =
                        "Number Push Configuration",
                    Font =
                        new Font(
                            "Segoe UI",
                            18F,
                            FontStyle.Bold),
                    AutoSize = true,
                    Location =
                        new Point(
                            25,
                            15)
                };

            Controls.Add(
                headingLabel);

            Label descriptionLabel =
                new Label
                {
                    Text =
                        "Configure your game's identity, appearance, and board settings.",
                    AutoSize = true,
                    Location =
                        new Point(
                            27,
                            50),
                    ForeColor =
                        Color.FromArgb(
                            80,
                            80,
                            80)
                };

            Controls.Add(
                descriptionLabel);

            Panel settingsPanel =
                new Panel
                {
                    Location =
                        new Point(
                            20,
                            80),
                    Size =
                        new Size(
                            530,
                            575),
                    BorderStyle =
                        BorderStyle.FixedSingle,
                    BackColor =
                        Color.White,
                    AutoScroll = true
                };

            Controls.Add(
                settingsPanel);

            BuildGameInfoSettings(
                settingsPanel);

            BuildTextSettings(
                settingsPanel);

            BuildColorSettings(
                settingsPanel);

            BuildBoardSettings(
                settingsPanel);

            Panel previewContainer =
                new Panel
                {
                    Location =
                        new Point(
                            570,
                            80),
                    Size =
                        new Size(
                            510,
                            575),
                    BorderStyle =
                        BorderStyle.FixedSingle,
                    BackColor =
                        Color.White
                };

            Controls.Add(
                previewContainer);

            Label previewTitle =
                new Label
                {
                    Text =
                        "Live Preview",
                    Font =
                        new Font(
                            "Segoe UI",
                            12F,
                            FontStyle.Bold),
                    AutoSize = true,
                    Location =
                        new Point(
                            20,
                            15)
                };

            previewContainer.Controls.Add(
                previewTitle);

            Label previewDescription =
                new Label
                {
                    Text =
                        "Changes are reflected as you edit the settings.",
                    AutoSize = true,
                    Location =
                        new Point(
                            20,
                            43),
                    ForeColor =
                        Color.DimGray
                };

            previewContainer.Controls.Add(
                previewDescription);

            previewPanel.Location =
                new Point(
                    20,
                    75);

            previewPanel.Size =
                new Size(
                    468,
                    475);

            previewPanel.BorderStyle =
                BorderStyle.FixedSingle;

            previewPanel.Paint +=
                PreviewPanel_Paint;

            previewContainer.Controls.Add(
                previewPanel);

            saveButton.Text =
                "Save";

            saveButton.Size =
                new Size(
                    100,
                    36);

            saveButton.Location =
                new Point(
                    760,
                    670);

            saveButton.Click +=
                SaveButton_Click;

            Controls.Add(
                saveButton);

            resetButton.Text =
                "Reset Defaults";

            resetButton.Size =
                new Size(
                    120,
                    36);

            resetButton.Location =
                new Point(
                    870,
                    670);

            resetButton.Click +=
                ResetButton_Click;

            Controls.Add(
                resetButton);

            cancelButton.Text =
                "Cancel";

            cancelButton.Size =
                new Size(
                    90,
                    36);

            cancelButton.Location =
                new Point(
                    990,
                    670);

            cancelButton.Click +=
                CancelButton_Click;

            Controls.Add(
                cancelButton);

            AcceptButton =
                saveButton;

            CancelButton =
                cancelButton;
        }

        private void BuildGameInfoSettings(
            Panel parent)
        {
            GroupBox group =
                CreateGroupBox(
                    "Game Information",
                    15,
                    15,
                    490,
                    185);

            parent.Controls.Add(
                group);

            AddLabelAndControl(
                group,
                "Game Name:",
                gameNameTextBox,
                15,
                30,
                360);

            AddLabelAndControl(
                group,
                "Developer:",
                developerNameTextBox,
                15,
                65,
                360);

            AddLabelAndControl(
                group,
                "Version:",
                versionTextBox,
                15,
                100,
                150);

            Label descriptionLabel =
                new Label
                {
                    Text =
                        "Description:",
                    AutoSize = true,
                    Location =
                        new Point(
                            15,
                            139)
                };

            group.Controls.Add(
                descriptionLabel);

            descriptionTextBox.Location =
                new Point(
                    100,
                    135);

            descriptionTextBox.Width =
                360;

            descriptionTextBox.Height =
                35;

            descriptionTextBox.Multiline =
                true;

            group.Controls.Add(
                descriptionTextBox);

            gameNameTextBox.TextChanged +=
                ConfigurationChanged;

            developerNameTextBox.TextChanged +=
                ConfigurationChanged;

            versionTextBox.TextChanged +=
                ConfigurationChanged;

            descriptionTextBox.TextChanged +=
                ConfigurationChanged;
        }

        private void BuildTextSettings(
            Panel parent)
        {
            GroupBox group =
                CreateGroupBox(
                    "Game Display",
                    15,
                    215,
                    490,
                    125);

            parent.Controls.Add(
                group);

            AddLabelAndControl(
                group,
                "Game Title:",
                gameTitleTextBox,
                15,
                30,
                360);

            AddLabelAndControl(
                group,
                "Subtitle:",
                gameSubtitleTextBox,
                15,
                70,
                360);

            gameTitleTextBox.TextChanged +=
                ConfigurationChanged;

            gameSubtitleTextBox.TextChanged +=
                ConfigurationChanged;
        }

        private void BuildColorSettings(
            Panel parent)
        {
            GroupBox group =
                CreateGroupBox(
                    "Appearance",
                    15,
                    355,
                    490,
                    385);

            parent.Controls.Add(
                group);

            AddColorControl(
                group,
                "Background:",
                backgroundColorTextBox,
                15,
                30);

            AddColorControl(
                group,
                "Board:",
                boardColorTextBox,
                255,
                30);

            AddColorControl(
                group,
                "Grid:",
                gridColorTextBox,
                15,
                70);

            AddColorControl(
                group,
                "Wall:",
                wallColorTextBox,
                255,
                70);

            AddColorControl(
                group,
                "Goal:",
                goalColorTextBox,
                15,
                110);

            AddColorControl(
                group,
                "Crate:",
                crateColorTextBox,
                255,
                110);

            AddColorControl(
                group,
                "Crate Border:",
                crateBorderColorTextBox,
                15,
                150);

            AddColorControl(
    group,
    "Crate Number:",
    crateNumberColorTextBox,
    255,
    150);

            AddColorControl(
                group,
                "Crate Distance:",
                crateDistanceColorTextBox,
                15,
                190);

            AddColorControl(
                group,
                "Player:",
                playerColorTextBox,
                255,
                190);

            AddColorControl(
                group,
                "Primary Text:",
                primaryTextColorTextBox,
                15,
                230);

            AddColorControl(
                group,
                "Secondary Text:",
                secondaryTextColorTextBox,
                255,
                230);

            AddColorControl(
                group,
                "Title:",
                titleColorTextBox,
                15,
                270);

            Label helpLabel =
                new Label
                {
                    Text =
                        "Use #RRGGBB values, such as #46AAFF.",
                    AutoSize = true,
                    Location =
                        new Point(
                            255,
                            234),
                    ForeColor =
                        Color.DimGray
                };

            group.Controls.Add(
                helpLabel);

            TextBox[] colorBoxes =
            {
                backgroundColorTextBox,
                boardColorTextBox,
                gridColorTextBox,
                wallColorTextBox,
                goalColorTextBox,
                crateColorTextBox,
                crateBorderColorTextBox,
                crateNumberColorTextBox,
                crateDistanceColorTextBox,
                playerColorTextBox,
                primaryTextColorTextBox,
                secondaryTextColorTextBox,
                titleColorTextBox
            };

            foreach (TextBox textBox in colorBoxes)
            {
                textBox.TextChanged +=
                    ConfigurationChanged;
            }
        }

        private void BuildBoardSettings(
            Panel parent)
        {
            GroupBox group =
                CreateGroupBox(
                    "Board",
                    15,
                    715,
                    490,
                    90);

            parent.Controls.Add(
                group);

            showGridCheckBox.Text =
                "Show Grid";

            showGridCheckBox.AutoSize = true;

            showGridCheckBox.Location =
                new Point(
                    15,
                    32);

            showGridCheckBox.CheckedChanged +=
                ConfigurationChanged;

            group.Controls.Add(
                showGridCheckBox);

            Label cellSizeLabel =
                new Label
                {
                    Text =
                        "Cell Size:",
                    AutoSize = true,
                    Location =
                        new Point(
                            180,
                            35)
                };

            group.Controls.Add(
                cellSizeLabel);

            cellSizeNumericUpDown.Minimum =
                25;

            cellSizeNumericUpDown.Maximum =
                100;

            cellSizeNumericUpDown.Increment =
                5;

            cellSizeNumericUpDown.Width =
                70;

            cellSizeNumericUpDown.Location =
                new Point(
                    250,
                    30);

            cellSizeNumericUpDown.ValueChanged +=
                ConfigurationChanged;

            group.Controls.Add(
                cellSizeNumericUpDown);

            Label pixelsLabel =
                new Label
                {
                    Text =
                        "pixels",
                    AutoSize = true,
                    Location =
                        new Point(
                            325,
                            35)
                };

            group.Controls.Add(
                pixelsLabel);
        }

        private GroupBox CreateGroupBox(
            string text,
            int x,
            int y,
            int width,
            int height)
        {
            return new GroupBox
            {
                Text = text,
                Location =
                    new Point(
                        x,
                        y),
                Size =
                    new Size(
                        width,
                        height)
            };
        }

        private void AddLabelAndControl(
            Control parent,
            string labelText,
            TextBox textBox,
            int x,
            int y,
            int width)
        {
            Label label =
                new Label
                {
                    Text = labelText,
                    AutoSize = true,
                    Location =
                        new Point(
                            x,
                            y + 4)
                };

            parent.Controls.Add(
                label);

            textBox.Location =
                new Point(
                    x + 85,
                    y);

            textBox.Width =
                width;

            parent.Controls.Add(
                textBox);
        }

        private void AddColorControl(
            Control parent,
            string labelText,
            TextBox textBox,
            int x,
            int y)
        {
            Label label =
                new Label
                {
                    Text = labelText,
                    AutoSize = true,
                    Location =
                        new Point(
                            x,
                            y + 4)
                };

            parent.Controls.Add(
                label);

            textBox.Location =
                new Point(
                    x + 105,
                    y);

            textBox.Width =
                125;

            parent.Controls.Add(
                textBox);
        }

        private void LoadConfiguration()
        {
            gameNameTextBox.Text =
                gameInfo.GameName;

            developerNameTextBox.Text =
                gameInfo.DeveloperName;

            versionTextBox.Text =
                gameInfo.Version;

            descriptionTextBox.Text =
                gameInfo.Description;

            gameTitleTextBox.Text =
                configuration.GameTitle;

            gameSubtitleTextBox.Text =
                configuration.GameSubtitle;

            backgroundColorTextBox.Text =
                configuration.BackgroundColor;

            boardColorTextBox.Text =
                configuration.BoardColor;

            gridColorTextBox.Text =
                configuration.GridColor;

            wallColorTextBox.Text =
                configuration.WallColor;

            goalColorTextBox.Text =
                configuration.GoalColor;

            crateColorTextBox.Text =
                configuration.CrateColor;

            crateBorderColorTextBox.Text =
                configuration.CrateBorderColor;

            crateNumberColorTextBox.Text =
    configuration.CrateNumberColor;

            crateDistanceColorTextBox.Text =
                configuration.CrateDistanceColor;

            playerColorTextBox.Text =
    configuration.PlayerColor;

            primaryTextColorTextBox.Text =
                configuration.PrimaryTextColor;

            secondaryTextColorTextBox.Text =
                configuration.SecondaryTextColor;

            titleColorTextBox.Text =
                configuration.TitleColor;

            showGridCheckBox.Checked =
                configuration.ShowGrid;

            cellSizeNumericUpDown.Value =
                Math.Clamp(
                    configuration.CellSize,
                    25,
                    100);

            previewPanel.Invalidate();
        }

        private void ConfigurationChanged(
            object? sender,
            EventArgs e)
        {
            previewPanel.Invalidate();
        }

        private void PreviewPanel_Paint(
            object? sender,
            PaintEventArgs e)
        {
            Graphics graphics =
                e.Graphics;

            graphics.SmoothingMode =
                SmoothingMode.AntiAlias;

            Color backgroundColor =
                GetPreviewColor(
                    backgroundColorTextBox.Text,
                    Color.FromArgb(
                        12,
                        15,
                        22));

            Color boardColor =
                GetPreviewColor(
                    boardColorTextBox.Text,
                    Color.FromArgb(
                        24,
                        29,
                        41));

            Color gridColor =
                GetPreviewColor(
                    gridColorTextBox.Text,
                    Color.FromArgb(
                        45,
                        55,
                        70));

            Color wallColor =
                GetPreviewColor(
                    wallColorTextBox.Text,
                    Color.FromArgb(
                        55,
                        65,
                        82));

            Color goalColor =
                GetPreviewColor(
                    goalColorTextBox.Text,
                    Color.FromArgb(
                        80,
                        210,
                        150));

            Color crateColor =
                GetPreviewColor(
                    crateColorTextBox.Text,
                    Color.FromArgb(
                        150,
                        95,
                        45));

            Color crateBorderColor =
                GetPreviewColor(
                    crateBorderColorTextBox.Text,
                    Color.FromArgb(
                        220,
                        160,
                        75));

            Color crateNumberColor =
    GetPreviewColor(
        crateNumberColorTextBox.Text,
        Color.White);

            Color crateDistanceColor =
                GetPreviewColor(
                    crateDistanceColorTextBox.Text,
                    Color.White);

            Color playerColor =
                GetPreviewColor(
                    playerColorTextBox.Text,
                    Color.FromArgb(
                        70,
                        170,
                        255));

            Color primaryTextColor =
                GetPreviewColor(
                    primaryTextColorTextBox.Text,
                    Color.White);

            Color secondaryTextColor =
                GetPreviewColor(
                    secondaryTextColorTextBox.Text,
                    Color.LightGray);

            Color titleColor =
                GetPreviewColor(
                    titleColorTextBox.Text,
                    Color.FromArgb(
                        70,
                        170,
                        255));

            using SolidBrush backgroundBrush =
                new SolidBrush(
                    backgroundColor);

            graphics.FillRectangle(
                backgroundBrush,
                previewPanel.ClientRectangle);

            using SolidBrush titleBrush =
                new SolidBrush(
                    titleColor);

            using Font titleFont =
                new Font(
                    "Segoe UI",
                    13F,
                    FontStyle.Bold);

            graphics.DrawString(
                string.IsNullOrWhiteSpace(
                    gameTitleTextBox.Text)
                        ? "NUMBER PUSH"
                        : gameTitleTextBox.Text,
                titleFont,
                titleBrush,
                18,
                15);

            using SolidBrush subtitleBrush =
                new SolidBrush(
                    secondaryTextColor);

            using Font subtitleFont =
                new Font(
                    "Segoe UI",
                    8F);

            graphics.DrawString(
                string.IsNullOrWhiteSpace(
                    gameSubtitleTextBox.Text)
                        ? "Push each numbered crate exactly its numbered distance."
                        : gameSubtitleTextBox.Text,
                subtitleFont,
                subtitleBrush,
                18,
                43);

            int cellSize =
                Math.Clamp(
                    (int)cellSizeNumericUpDown.Value,
                    25,
                    45);

            int boardColumns = 9;
            int boardRows = 6;

            int boardWidth =
                boardColumns * cellSize;

            int boardHeight =
                boardRows * cellSize;

            int boardX =
                (previewPanel.ClientSize.Width -
                 boardWidth) / 2;

            int boardY =
                85;

            using SolidBrush boardBrush =
                new SolidBrush(
                    boardColor);

            graphics.FillRectangle(
                boardBrush,
                boardX,
                boardY,
                boardWidth,
                boardHeight);

            if (showGridCheckBox.Checked)
            {
                using Pen gridPen =
                    new Pen(
                        gridColor,
                        1);

                for (int x = 0;
                     x <= boardColumns;
                     x++)
                {
                    int drawX =
                        boardX +
                        x * cellSize;

                    graphics.DrawLine(
                        gridPen,
                        drawX,
                        boardY,
                        drawX,
                        boardY + boardHeight);
                }

                for (int y = 0;
                     y <= boardRows;
                     y++)
                {
                    int drawY =
                        boardY +
                        y * cellSize;

                    graphics.DrawLine(
                        gridPen,
                        boardX,
                        drawY,
                        boardX + boardWidth,
                        drawY);
                }
            }

            DrawPreviewWall(
                graphics,
                wallColor,
                boardX,
                boardY,
                cellSize,
                0,
                0);

            DrawPreviewWall(
                graphics,
                wallColor,
                boardX,
                boardY,
                cellSize,
                1,
                0);

            DrawPreviewWall(
                graphics,
                wallColor,
                boardX,
                boardY,
                cellSize,
                2,
                0);

            DrawPreviewWall(
                graphics,
                wallColor,
                boardX,
                boardY,
                cellSize,
                6,
                0);

            DrawPreviewWall(
                graphics,
                wallColor,
                boardX,
                boardY,
                cellSize,
                7,
                0);

            DrawPreviewWall(
                graphics,
                wallColor,
                boardX,
                boardY,
                cellSize,
                8,
                0);

            DrawPreviewWall(
                graphics,
                wallColor,
                boardX,
                boardY,
                cellSize,
                0,
                5);

            DrawPreviewWall(
                graphics,
                wallColor,
                boardX,
                boardY,
                cellSize,
                8,
                5);

            DrawPreviewGoal(
                graphics,
                goalColor,
                boardX,
                boardY,
                cellSize,
                7,
                3);

            DrawPreviewCrate(
    graphics,
    crateColor,
    crateBorderColor,
    crateNumberColor,
    crateDistanceColor,
    boardX,
    boardY,
    cellSize,
    4,
    3,
    "1",
    "3");

            DrawPreviewPlayer(
                graphics,
                playerColor,
                boardX,
                boardY,
                cellSize,
                2,
                3);

            using SolidBrush statusBrush =
                new SolidBrush(
                    secondaryTextColor);

            using Font statusFont =
                new Font(
                    "Segoe UI",
                    8F);

            graphics.DrawString(
                "Level 1    Pushes: 0",
                statusFont,
                statusBrush,
                18,
                boardY + boardHeight + 20);

            using SolidBrush primaryBrush =
                new SolidBrush(
                    primaryTextColor);

            using Font instructionFont =
                new Font(
                    "Segoe UI",
                    8F);

            graphics.DrawString(
                "Arrow Keys / WASD to move     R to reset",
                instructionFont,
                primaryBrush,
                18,
                boardY + boardHeight + 42);
        }

        private static void DrawPreviewWall(
            Graphics graphics,
            Color color,
            int boardX,
            int boardY,
            int cellSize,
            int column,
            int row)
        {
            using SolidBrush brush =
                new SolidBrush(
                    color);

            graphics.FillRectangle(
                brush,
                boardX + column * cellSize,
                boardY + row * cellSize,
                cellSize,
                cellSize);
        }

        private static void DrawPreviewGoal(
            Graphics graphics,
            Color color,
            int boardX,
            int boardY,
            int cellSize,
            int column,
            int row)
        {
            int padding =
                Math.Max(
                    4,
                    cellSize / 6);

            using Pen pen =
                new Pen(
                    color,
                    3);

            graphics.DrawRectangle(
                pen,
                boardX +
                    column * cellSize +
                    padding,
                boardY +
                    row * cellSize +
                    padding,
                cellSize -
                    padding * 2,
                cellSize -
                    padding * 2);
        }

        private static void DrawPreviewCrate(
    Graphics graphics,
    Color crateColor,
    Color borderColor,
    Color crateNumberColor,
    Color crateDistanceColor,
    int boardX,
    int boardY,
    int cellSize,
    int column,
    int row,
    string crateNumber,
    string distance)
        {
            int padding =
                Math.Max(
                    3,
                    cellSize / 8);

            Rectangle rectangle =
                new Rectangle(
                    boardX +
                        column * cellSize +
                        padding,
                    boardY +
                        row * cellSize +
                        padding,
                    cellSize -
                        padding * 2,
                    cellSize -
                        padding * 2);

            using SolidBrush crateBrush =
                new SolidBrush(
                    crateColor);

            using Pen borderPen =
                new Pen(
                    borderColor,
                    2);

            graphics.FillRectangle(
                crateBrush,
                rectangle);

            graphics.DrawRectangle(
                borderPen,
                rectangle);

            using SolidBrush distanceBrush =
                new SolidBrush(
                    crateDistanceColor);

            using Font distanceFont =
                new Font(
                    "Segoe UI",
                    Math.Max(
                        9,
                        cellSize / 2.5F),
                    FontStyle.Bold);

            StringFormat centerFormat =
                new StringFormat
                {
                    Alignment =
                        StringAlignment.Center,
                    LineAlignment =
                        StringAlignment.Center
                };

            graphics.DrawString(
                distance,
                distanceFont,
                distanceBrush,
                rectangle,
                centerFormat);

            using SolidBrush crateNumberBrush =
                new SolidBrush(
                    crateNumberColor);

            using Font crateNumberFont =
                new Font(
                    "Segoe UI",
                    Math.Max(
                        7,
                        cellSize / 5F),
                    FontStyle.Bold);

            graphics.DrawString(
                crateNumber,
                crateNumberFont,
                crateNumberBrush,
                rectangle.X + 4,
                rectangle.Y + 2);
        }

        private static void DrawPreviewPlayer(
            Graphics graphics,
            Color color,
            int boardX,
            int boardY,
            int cellSize,
            int column,
            int row)
        {
            int padding =
                Math.Max(
                    5,
                    cellSize / 5);

            Rectangle rectangle =
                new Rectangle(
                    boardX +
                        column * cellSize +
                        padding,
                    boardY +
                        row * cellSize +
                        padding,
                    cellSize -
                        padding * 2,
                    cellSize -
                        padding * 2);

            using SolidBrush brush =
                new SolidBrush(
                    color);

            graphics.FillEllipse(
                brush,
                rectangle);
        }

        private static Color GetPreviewColor(
            string value,
            Color fallback)
        {
            try
            {
                return NumberPushColorHelper.FromHex(
                    value);
            }
            catch
            {
                return fallback;
            }
        }

        private void SaveButton_Click(
            object? sender,
            EventArgs e)
        {
            try
            {
                ValidateColor(
                    backgroundColorTextBox.Text,
                    "Background");

                ValidateColor(
                    boardColorTextBox.Text,
                    "Board");

                ValidateColor(
                    gridColorTextBox.Text,
                    "Grid");

                ValidateColor(
                    wallColorTextBox.Text,
                    "Wall");

                ValidateColor(
                    goalColorTextBox.Text,
                    "Goal");

                ValidateColor(
                    crateColorTextBox.Text,
                    "Crate");

                ValidateColor(
                    crateBorderColorTextBox.Text,
                    "Crate Border");

                ValidateColor(
    crateNumberColorTextBox.Text,
    "Crate Number");

                ValidateColor(
                    crateDistanceColorTextBox.Text,
                    "Crate Distance");

                ValidateColor(
                    playerColorTextBox.Text,
                    "Player");

                ValidateColor(
                    primaryTextColorTextBox.Text,
                    "Primary Text");

                ValidateColor(
                    secondaryTextColorTextBox.Text,
                    "Secondary Text");

                ValidateColor(
                    titleColorTextBox.Text,
                    "Title");

                gameInfo.GameName =
                    gameNameTextBox.Text.Trim();

                gameInfo.DeveloperName =
                    developerNameTextBox.Text.Trim();

                gameInfo.Version =
                    versionTextBox.Text.Trim();

                gameInfo.Description =
                    descriptionTextBox.Text.Trim();

                configuration.GameTitle =
                    gameTitleTextBox.Text.Trim();

                configuration.GameSubtitle =
                    gameSubtitleTextBox.Text.Trim();

                configuration.BackgroundColor =
                    backgroundColorTextBox.Text.Trim();

                configuration.BoardColor =
                    boardColorTextBox.Text.Trim();

                configuration.GridColor =
                    gridColorTextBox.Text.Trim();

                configuration.WallColor =
                    wallColorTextBox.Text.Trim();

                configuration.GoalColor =
                    goalColorTextBox.Text.Trim();

                configuration.CrateColor =
                    crateColorTextBox.Text.Trim();

                configuration.CrateBorderColor =
                    crateBorderColorTextBox.Text.Trim();

                configuration.CrateNumberColor =
                    crateNumberColorTextBox.Text.Trim();

                configuration.CrateDistanceColor =
                    crateDistanceColorTextBox.Text.Trim();

                configuration.PlayerColor =
                    playerColorTextBox.Text.Trim();

                configuration.PrimaryTextColor =
                    primaryTextColorTextBox.Text.Trim();

                configuration.SecondaryTextColor =
                    secondaryTextColorTextBox.Text.Trim();

                configuration.TitleColor =
                    titleColorTextBox.Text.Trim();

                configuration.ShowGrid =
                    showGridCheckBox.Checked;

                configuration.CellSize =
                    (int)cellSizeNumericUpDown.Value;

                gameInfoRepository.Save(
                    gameInfo);

                configurationRepository.Save(
                    configuration);

                DialogResult =
                    DialogResult.OK;

                Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    ex.Message,
                    "Invalid Configuration",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
            }
        }

        private void ResetButton_Click(
            object? sender,
            EventArgs e)
        {
            DialogResult result =
                MessageBox.Show(
                    "Reset all configuration settings to their default values?",
                    "Reset Configuration",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question);

            if (result != DialogResult.Yes)
            {
                return;
            }

            configuration =
                new NumberPushGameConfiguration();

            gameInfo =
                new NumberPushGameInfo();

            LoadConfiguration();
        }

        private void CancelButton_Click(
            object? sender,
            EventArgs e)
        {
            DialogResult =
                DialogResult.Cancel;

            Close();
        }

        private static void ValidateColor(
            string value,
            string name)
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                throw new ArgumentException(
                    $"{name} color is required.");
            }

            string trimmed =
                value.Trim();

            if (trimmed.StartsWith("#"))
            {
                trimmed =
                    trimmed.Substring(1);
            }

            if (trimmed.Length != 6)
            {
                throw new ArgumentException(
                    $"{name} color must contain exactly six hexadecimal characters.");
            }

            try
            {
                Convert.ToInt32(
                    trimmed,
                    16);
            }
            catch
            {
                throw new ArgumentException(
                    $"{name} color contains invalid hexadecimal characters.");
            }
        }
    }
}