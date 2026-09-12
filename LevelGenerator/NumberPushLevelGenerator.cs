using NovaWrightNumberPush;

namespace NovaWright.NumberPush.LevelGenerator
{
    public class NumberPushLevelGenerator
    {
        private readonly Random random;

        // --------------------------------------------------------
        // Current board size
        //
        // These persist between generated levels.
        //
        // Once the generator has to increase the board size,
        // that larger size becomes the new normal size.
        // --------------------------------------------------------

        private int? currentRows;

        private int? currentColumns;

        public NumberPushLevelGenerator(int seed)
        {
            random = new Random(seed);
        }

        public NumberPushLevel? Generate(
            int levelNumber,
            DifficultySettings settings)
        {
            if (settings.StartingRows <= 0 ||
                settings.StartingColumns <= 0)
            {
                throw new ArgumentException(
                    "Starting board dimensions must be greater than zero.",
                    nameof(settings));
            }

            if (settings.RowIncrease <= 0 ||
                settings.ColumnIncrease <= 0)
            {
                throw new ArgumentException(
                    "Board size increases must be greater than zero.",
                    nameof(settings));
            }

            if (settings.MaximumRows <
                settings.StartingRows ||
                settings.MaximumColumns <
                settings.StartingColumns)
            {
                throw new ArgumentException(
                    "Maximum board dimensions cannot be smaller than the starting dimensions.",
                    nameof(settings));
            }

            // ----------------------------------------------------
            // Establish the starting size.
            //
            // This only happens the first time Generate() is
            // called. After that, the current board size persists
            // between levels.
            // ----------------------------------------------------

            if (!currentRows.HasValue ||
                !currentColumns.HasValue)
            {
                currentRows =
                    settings.StartingRows;

                currentColumns =
                    settings.StartingColumns;
            }
            else
            {
                // If a later difficulty profile starts at a larger
                // size, make sure we never move backwards.
                currentRows =
                    Math.Max(
                        currentRows.Value,
                        settings.StartingRows);

                currentColumns =
                    Math.Max(
                        currentColumns.Value,
                        settings.StartingColumns);
            }

            int rows =
                currentRows.Value;

            int columns =
                currentColumns.Value;

            while (rows <= settings.MaximumRows &&
                   columns <= settings.MaximumColumns)
            {
                for (int attempt = 0;
                     attempt < settings.MaximumAttempts;
                     attempt++)
                {
                    NumberPushLevel? candidate =
                        CreateCandidate(
                            levelNumber,
                            settings,
                            rows,
                            columns);

                    if (candidate == null)
                    {
                        continue;
                    }

                    NumberPushSolver solver =
                        new NumberPushSolver(candidate);

                    int minimumSolution =
                        solver.FindMinimumPushes();

                    if (minimumSolution < 0)
                    {
                        continue;
                    }

                    if (minimumSolution <
                        settings.MinimumSolutionPushes)
                    {
                        continue;
                    }

                    if (minimumSolution >
                        settings.MaximumSolutionPushes)
                    {
                        continue;
                    }

                    // ------------------------------------------------
                    // Candidate accepted.
                    //
                    // Remember the board size so the next level
                    // starts at this same size.
                    // ------------------------------------------------

                    currentRows = rows;
                    currentColumns = columns;

                    return candidate;
                }

                Console.WriteLine(
                    $"No suitable level at " +
                    $"{rows}x{columns}. " +
                    $"Increasing board size.");

                // ----------------------------------------------------
                // Increase the persistent board size.
                //
                // The next level will also begin at this size.
                // ----------------------------------------------------

                rows += settings.RowIncrease;
                columns += settings.ColumnIncrease;

                currentRows = rows;
                currentColumns = columns;
            }

            return null;
        }

        private NumberPushLevel? CreateCandidate(
            int levelNumber,
            DifficultySettings settings,
            int rows,
            int columns)
        {
            NumberPushLevel level =
                new NumberPushLevel
                {
                    LevelNumber = levelNumber,
                    Rows = rows,
                    Columns = columns
                };

            CreateOuterWalls(level);

            if (!CreateInteriorWalls(
                    level,
                    settings))
            {
                return null;
            }

            List<Point> availableCells =
                GetAvailableCells(level);

            int crateCount =
                random.Next(
                    settings.MinimumCrates,
                    settings.MaximumCrates + 1);

            if (availableCells.Count <
                crateCount * 2 + 1)
            {
                return null;
            }

            Shuffle(availableCells);

            // ----------------------------------------------------
            // Create crates
            // ----------------------------------------------------

            List<Point> cratePositions =
                availableCells
                    .Take(crateCount)
                    .ToList();

            foreach (Point cratePosition in cratePositions)
            {
                int distance =
                    random.Next(
                        settings.MinimumCrateDistance,
                        settings.MaximumCrateDistance + 1);

                level.Crates.Add(
                    new NumberPushCrate(
                        cratePosition,
                        distance));
            }

            // ----------------------------------------------------
            // Create goals
            // ----------------------------------------------------

            List<Point> goalCandidates =
                availableCells
                    .Where(
                        cell =>
                            !cratePositions.Contains(cell))
                    .ToList();

            Shuffle(goalCandidates);

            if (goalCandidates.Count < crateCount)
            {
                return null;
            }

            for (int i = 0;
                 i < crateCount;
                 i++)
            {
                level.Goals.Add(
                    goalCandidates[i]);
            }

            // ----------------------------------------------------
            // Create player starting position
            // ----------------------------------------------------

            List<Point> playerCandidates =
                availableCells
                    .Where(
                        cell =>
                            !cratePositions.Contains(cell) &&
                            !level.Goals.Contains(cell))
                    .ToList();

            if (playerCandidates.Count == 0)
            {
                return null;
            }

            level.PlayerStart =
                playerCandidates[
                    random.Next(
                        playerCandidates.Count)];

            return level;
        }

        private bool CreateInteriorWalls(
            NumberPushLevel level,
            DifficultySettings settings)
        {
            int wallCount =
                random.Next(
                    settings.MinimumInteriorWalls,
                    settings.MaximumInteriorWalls + 1);

            List<Point> candidates = new();

            for (int y = 1;
                 y < level.Rows - 1;
                 y++)
            {
                for (int x = 1;
                     x < level.Columns - 1;
                     x++)
                {
                    candidates.Add(
                        new Point(x, y));
                }
            }

            Shuffle(candidates);

            int wallsAdded = 0;

            foreach (Point candidate in candidates)
            {
                if (wallsAdded >= wallCount)
                {
                    break;
                }

                level.Walls.Add(
                    new Rectangle(
                        candidate.X,
                        candidate.Y,
                        1,
                        1));

                if (!IsBoardConnected(level))
                {
                    level.Walls.RemoveAt(
                        level.Walls.Count - 1);

                    continue;
                }

                wallsAdded++;
            }

            return wallsAdded == wallCount;
        }

        private bool IsBoardConnected(
            NumberPushLevel level)
        {
            List<Point> availableCells =
                GetAvailableCells(level);

            if (availableCells.Count == 0)
            {
                return false;
            }

            HashSet<Point> visited = new();

            Queue<Point> queue = new();

            Point start =
                availableCells[0];

            visited.Add(start);
            queue.Enqueue(start);

            Point[] directions =
            {
                new Point(0, -1),
                new Point(0, 1),
                new Point(-1, 0),
                new Point(1, 0)
            };

            while (queue.Count > 0)
            {
                Point current =
                    queue.Dequeue();

                foreach (Point direction in directions)
                {
                    Point next =
                        new Point(
                            current.X + direction.X,
                            current.Y + direction.Y);

                    if (IsWall(level, next))
                    {
                        continue;
                    }

                    if (visited.Add(next))
                    {
                        queue.Enqueue(next);
                    }
                }
            }

            return visited.Count ==
                   availableCells.Count;
        }

        private void CreateOuterWalls(
            NumberPushLevel level)
        {
            for (int x = 0;
                 x < level.Columns;
                 x++)
            {
                level.Walls.Add(
                    new Rectangle(
                        x,
                        0,
                        1,
                        1));

                level.Walls.Add(
                    new Rectangle(
                        x,
                        level.Rows - 1,
                        1,
                        1));
            }

            for (int y = 1;
                 y < level.Rows - 1;
                 y++)
            {
                level.Walls.Add(
                    new Rectangle(
                        0,
                        y,
                        1,
                        1));

                level.Walls.Add(
                    new Rectangle(
                        level.Columns - 1,
                        y,
                        1,
                        1));
            }
        }

        private List<Point> GetAvailableCells(
            NumberPushLevel level)
        {
            List<Point> cells = new();

            for (int y = 1;
                 y < level.Rows - 1;
                 y++)
            {
                for (int x = 1;
                     x < level.Columns - 1;
                     x++)
                {
                    Point point =
                        new Point(x, y);

                    if (!IsWall(level, point))
                    {
                        cells.Add(point);
                    }
                }
            }

            return cells;
        }

        private bool IsWall(
            NumberPushLevel level,
            Point position)
        {
            if (position.X < 0 ||
                position.X >= level.Columns ||
                position.Y < 0 ||
                position.Y >= level.Rows)
            {
                return true;
            }

            return level.Walls.Any(
                wall =>
                    wall.X == position.X &&
                    wall.Y == position.Y);
        }

        private void Shuffle<T>(
            List<T> list)
        {
            for (int i = list.Count - 1;
                 i > 0;
                 i--)
            {
                int j =
                    random.Next(i + 1);

                (list[i], list[j]) =
                    (list[j], list[i]);
            }
        }
    }
}