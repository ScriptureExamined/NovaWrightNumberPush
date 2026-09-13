using NovaWrightNumberPush;
using System.Diagnostics;

namespace NovaWright.NumberPush.LevelGenerator
{
    public class NumberPushLevelGenerator
    {
        private readonly Random random;

        private int? currentRows;
        private int? currentColumns;

        public NumberPushLevelGenerator(int seed)
        {
            random =
                new Random(seed);
        }

        public NumberPushLevel? Generate(
    int levelNumber,
    NumberPushDifficulty difficulty,
    NumberPushGenerationDiagnostics? diagnostics = null)
        {
            if (difficulty == null)
            {
                throw new ArgumentNullException(
                    nameof(difficulty));
            }

            if (diagnostics != null)
            {
                diagnostics.LevelNumber =
                    levelNumber;

                diagnostics.TargetMinimumPushes =
                    difficulty.MinimumSolutionPushes;

                diagnostics.TargetMaximumPushes =
                    difficulty.MaximumSolutionPushes;
            }

            if (difficulty.StartingRows <= 0 ||
                difficulty.StartingColumns <= 0)
            {
                throw new ArgumentException(
                    "Starting board dimensions must be greater than zero.",
                    nameof(difficulty));
            }

            if (difficulty.MaximumRows <
                difficulty.StartingRows ||
                difficulty.MaximumColumns <
                difficulty.StartingColumns)
            {
                throw new ArgumentException(
                    "Maximum board dimensions cannot be smaller than the starting dimensions.",
                    nameof(difficulty));
            }

            if (!currentRows.HasValue ||
                !currentColumns.HasValue)
            {
                currentRows =
                    difficulty.StartingRows;

                currentColumns =
                    difficulty.StartingColumns;
            }
            else
            {
                currentRows =
                    Math.Max(
                        currentRows.Value,
                        difficulty.StartingRows);

                currentColumns =
                    Math.Max(
                        currentColumns.Value,
                        difficulty.StartingColumns);
            }

            int rows =
                currentRows.Value;

            int columns =
                currentColumns.Value;

            while (rows <= difficulty.MaximumRows &&
                   columns <= difficulty.MaximumColumns)
            {
                for (int attempt = 0;
                     attempt < difficulty.MaximumAttempts;
                     attempt++)
                {
                    if (diagnostics != null)
                    {
                        diagnostics.TotalAttempts++;
                    }

                    Stopwatch candidateTimer =
    Stopwatch.StartNew();

                    NumberPushLevel? candidate =
                        CreateCandidate(
                            levelNumber,
                            difficulty,
                            rows,
                            columns);

                    candidateTimer.Stop();

                    if (diagnostics != null)
                    {
                        diagnostics.CandidateGenerationMilliseconds +=
                            candidateTimer.ElapsedMilliseconds;
                    }

                    if (candidate == null)
                    {
                        if (diagnostics != null)
                        {
                            diagnostics.WallGenerationFailures++;
                        }

                        continue;
                    }

                    if (diagnostics != null)
                    {
                        diagnostics.TotalSolverCalls++;
                    }

                    Stopwatch solverTimer =
                        Stopwatch.StartNew();

                    NumberPushSolver solver =
                        new NumberPushSolver(
                            candidate);

                    int minimumSolution =
                        solver.FindMinimumPushes();

                    solverTimer.Stop();

                    if (diagnostics != null)
                    {
                        diagnostics.SolverMilliseconds +=
                            solverTimer.ElapsedMilliseconds;
                    }

                    if (minimumSolution < 0)
                    {
                        if (diagnostics != null)
                        {
                            diagnostics.UnsolvableCandidates++;
                        }

                        continue;
                    }

                    if (minimumSolution <
                        difficulty.MinimumSolutionPushes)
                    {
                        if (diagnostics != null)
                        {
                            diagnostics.BelowTargetCandidates++;
                        }

                        continue;
                    }

                    if (minimumSolution >
                        difficulty.MaximumSolutionPushes)
                    {
                        if (diagnostics != null)
                        {
                            diagnostics.AboveTargetCandidates++;
                        }

                        continue;
                    }

                    if (diagnostics != null)
                    {
                        diagnostics.AcceptedCandidates++;
                    }

                    currentRows =
                        rows;

                    currentColumns =
                        columns;

                    return candidate;
                }

                Console.WriteLine(
                    $"No suitable level at " +
                    $"{rows}x{columns}. " +
                    $"Increasing board size.");

                rows += 2;
                columns += 2;

                currentRows =
                    rows;

                currentColumns =
                    columns;
            }

            return null;
        }

        private NumberPushLevel? CreateCandidate(
    int levelNumber,
    NumberPushDifficulty difficulty,
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

            CreateOuterWalls(
                level);

            if (!CreateInteriorWalls(
                level,
                difficulty))
            {
                return null;
            }

            List<Point> availableCells =
                GetAvailableCells(
                    level);

            int crateCount =
                difficulty.MinimumCrates;

            if (availableCells.Count <
                crateCount * 2 + 1)
            {
                return null;
            }

            Shuffle(
                availableCells);

            List<Point> cratePositions =
                availableCells
                    .Take(crateCount)
                    .ToList();

            foreach (Point cratePosition in cratePositions)
            {
                int distance =
                    GetCrateDistance(
                        difficulty,
                        cratePositions.IndexOf(
                            cratePosition));

                level.Crates.Add(
                    new NumberPushCrate(
                        cratePosition,
                        distance));
            }

            HashSet<Point> usedGoals =
                new HashSet<Point>();

            List<int> crateOrder =
                Enumerable.Range(
                    0,
                    level.Crates.Count)
                .OrderBy(
                    index =>
                        level.Crates[index].Distance)
                .ToList();

            foreach (int crateIndex in crateOrder)
            {
                NumberPushCrate crate =
                    level.Crates[crateIndex];

                List<Point> compatibleGoals =
                    availableCells
                        .Where(
                            cell =>
                                !cratePositions.Contains(
                                    cell) &&
                                !usedGoals.Contains(
                                    cell) &&
                                IsGeometricallyCompatibleGoal(
                                    crate.Position,
                                    crate.Distance,
                                    cell))
                        .ToList();

                if (compatibleGoals.Count == 0)
                {
                    return null;
                }

                Shuffle(
                    compatibleGoals);

                Point selectedGoal =
                    compatibleGoals[0];

                level.Goals.Add(
                    selectedGoal);

                usedGoals.Add(
                    selectedGoal);
            }

            List<Point> playerCandidates =
                availableCells
                    .Where(
                        cell =>
                            !cratePositions.Contains(
                                cell) &&
                            !level.Goals.Contains(
                                cell))
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

        private bool IsGeometricallyCompatibleGoal(
    Point cratePosition,
    int distance,
    Point goalPosition)
        {
            if (distance <= 0)
            {
                return false;
            }

            int deltaX =
                Math.Abs(
                    goalPosition.X -
                    cratePosition.X);

            int deltaY =
                Math.Abs(
                    goalPosition.Y -
                    cratePosition.Y);

            bool sameRow =
                deltaY == 0;

            bool sameColumn =
                deltaX == 0;

            if (!sameRow &&
                !sameColumn)
            {
                return false;
            }

            int displacement =
                Math.Max(
                    deltaX,
                    deltaY);

            return
                displacement > 0 &&
                displacement % distance == 0;
        }

        private int GetCrateDistance(
    NumberPushDifficulty difficulty,
    int crateIndex)
        {
            int requiredDistance =
                1 +
                difficulty.Complexity / 3;

            int distance =
                requiredDistance -
                crateIndex;

            return Math.Max(
                difficulty.MinimumCrateDistance,
                Math.Min(
                    difficulty.MaximumCrateDistance,
                    distance));
        }

        private bool CreateInteriorWalls(
            NumberPushLevel level,
            NumberPushDifficulty difficulty)
        {
            int wallCount =
                random.Next(
                    difficulty.MinimumInteriorWalls,
                    difficulty.MaximumInteriorWalls + 1);

            List<Point> candidates =
                new List<Point>();

            for (int y = 1;
                 y < level.Rows - 1;
                 y++)
            {
                for (int x = 1;
                     x < level.Columns - 1;
                     x++)
                {
                    candidates.Add(
                        new Point(
                            x,
                            y));
                }
            }

            Shuffle(
                candidates);

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

                if (!IsBoardConnected(
                    level))
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
            int interiorCellCount =
                (level.Rows - 2) *
                (level.Columns - 2);

            int availableCellCount =
                interiorCellCount -
                level.Walls.Count(
                    wall =>
                        wall.X > 0 &&
                        wall.X < level.Columns - 1 &&
                        wall.Y > 0 &&
                        wall.Y < level.Rows - 1);

            if (availableCellCount <= 0)
            {
                return false;
            }

            HashSet<Point> wallPositions =
                level.Walls
                    .Select(
                        wall =>
                            new Point(
                                wall.X,
                                wall.Y))
                    .ToHashSet();

            Point start =
                new Point(
                    -1,
                    -1);

            for (int y = 1;
                 y < level.Rows - 1;
                 y++)
            {
                for (int x = 1;
                     x < level.Columns - 1;
                     x++)
                {
                    Point position =
                        new Point(
                            x,
                            y);

                    if (!wallPositions.Contains(
                        position))
                    {
                        start =
                            position;

                        break;
                    }
                }

                if (start.X >= 0)
                {
                    break;
                }
            }

            if (start.X < 0)
            {
                return false;
            }

            HashSet<Point> visited =
                new HashSet<Point>();

            Queue<Point> queue =
                new Queue<Point>();

            visited.Add(
                start);

            queue.Enqueue(
                start);

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

                    if (next.X <= 0 ||
                        next.X >= level.Columns - 1 ||
                        next.Y <= 0 ||
                        next.Y >= level.Rows - 1)
                    {
                        continue;
                    }

                    if (wallPositions.Contains(
                        next))
                    {
                        continue;
                    }

                    if (visited.Add(
                        next))
                    {
                        queue.Enqueue(
                            next);
                    }
                }
            }

            return visited.Count ==
                   availableCellCount;
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
            List<Point> cells =
                new List<Point>();

            for (int y = 1;
                 y < level.Rows - 1;
                 y++)
            {
                for (int x = 1;
                     x < level.Columns - 1;
                     x++)
                {
                    Point point =
                        new Point(
                            x,
                            y);

                    if (!IsWall(
                        level,
                        point))
                    {
                        cells.Add(
                            point);
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
                    random.Next(
                        i + 1);

                (list[i], list[j]) =
                    (list[j], list[i]);
            }
        }
    }
}