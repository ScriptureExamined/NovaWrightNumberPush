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
                Stopwatch boardSizeTimer =
                    Stopwatch.StartNew();

                for (int attempt = 0;
                                 attempt < difficulty.MaximumAttempts;
                     attempt++)
                {
                    if (diagnostics != null)
                    {
                        diagnostics.TotalAttempts++;

                        string boardSize =
    $"{rows}x{columns}";

                        if (diagnostics.BoardSizeAttempts.ContainsKey(
                            boardSize))
                        {
                            diagnostics.BoardSizeAttempts[boardSize]++;
                        }
                        else
                        {
                            diagnostics.BoardSizeAttempts[boardSize] = 1;
                        }
                    }

                    Stopwatch candidateTimer =
    Stopwatch.StartNew();

                    NumberPushLevel? candidate =
    CreateCandidate(
        levelNumber,
        difficulty,
        rows,
        columns,
        diagnostics);

                    candidateTimer.Stop();

                    if (diagnostics != null)
                    {
                        diagnostics.CandidateGenerationMilliseconds +=
                            candidateTimer.ElapsedMilliseconds;
                    }

                    if (candidate == null)
                    {
                        continue;
                    }

                    if (diagnostics != null)
                    {
                        diagnostics.TotalSolverCalls++;

                        string boardSize =
                            $"{rows}x{columns}";

                        if (diagnostics.BoardSizeSolverCalls.ContainsKey(
                            boardSize))
                        {
                            diagnostics.BoardSizeSolverCalls[boardSize]++;
                        }
                        else
                        {
                            diagnostics.BoardSizeSolverCalls[boardSize] = 1;
                        }
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
                        long solverMilliseconds =
                            solverTimer.ElapsedMilliseconds;

                        diagnostics.SolverMilliseconds +=
    solverMilliseconds;

                        diagnostics.StatesExplored +=
    solver.StatesExplored;

                        diagnostics.MaximumStatesExplored =
                            Math.Max(
                                diagnostics.MaximumStatesExplored,
                                solver.StatesExplored);

                        diagnostics.TotalLegalPushes +=
    solver.TotalLegalPushes;

                        diagnostics.MaximumLegalPushes =
                            Math.Max(
                                diagnostics.MaximumLegalPushes,
                                solver.MaximumLegalPushes);

                        diagnostics.ZeroLegalPushStates +=
                            solver.ZeroLegalPushStates;

                        string boardSize =
                            $"{rows}x{columns}";

                        if (diagnostics.BoardSizeStatesExplored.ContainsKey(
                            boardSize))
                        {
                            diagnostics.BoardSizeStatesExplored[boardSize] +=
                                solver.StatesExplored;
                        }
                        else
                        {
                            diagnostics.BoardSizeStatesExplored[boardSize] =
                                solver.StatesExplored;
                        }

                        if (minimumSolution < 0)
                        {
                            diagnostics.UnsolvableSolverMilliseconds +=
                                solverMilliseconds;

                            diagnostics.UnsolvableStates +=
                                solver.StatesExplored;

                            diagnostics.MaximumUnsolvableStates =
                                Math.Max(
                                    diagnostics.MaximumUnsolvableStates,
                                    solver.StatesExplored);

                            if (diagnostics.BoardSizeUnsolvableStates.ContainsKey(
    boardSize))
                            {
                                diagnostics.BoardSizeUnsolvableStates[boardSize] +=
                                    solver.StatesExplored;
                            }
                            else
                            {
                                diagnostics.BoardSizeUnsolvableStates[boardSize] =
                                    solver.StatesExplored;
                            }
                        }
                        else if (minimumSolution <
                                 difficulty.MinimumSolutionPushes)
                        {
                            diagnostics.BelowTargetSolverMilliseconds +=
                                solverMilliseconds;
                        }
                        else if (minimumSolution >
                                 difficulty.MaximumSolutionPushes)
                        {
                            diagnostics.AboveTargetSolverMilliseconds +=
                                solverMilliseconds;
                        }
                        else
                        {
                            diagnostics.AcceptedSolverMilliseconds +=
                                solverMilliseconds;

                            diagnostics.AcceptedStates =
                                solver.StatesExplored;
                        }
                    }

                    if (minimumSolution < 0)
                    {
                        if (diagnostics != null)
                        {
                            diagnostics.UnsolvableCandidates++;
                        }

                        continue;
                    }

                    if (diagnostics != null)
                    {
                        if (diagnostics.SolvablePushCounts.ContainsKey(
                            minimumSolution))
                        {
                            diagnostics.SolvablePushCounts[minimumSolution]++;
                        }
                        else
                        {
                            diagnostics.SolvablePushCounts[minimumSolution] = 1;
                        }
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

                        boardSizeTimer.Stop();

                        string boardSize =
                            $"{rows}x{columns}";

                        if (diagnostics.BoardSizeMilliseconds.ContainsKey(
                            boardSize))
                        {
                            diagnostics.BoardSizeMilliseconds[boardSize] +=
                                boardSizeTimer.ElapsedMilliseconds;
                        }
                        else
                        {
                            diagnostics.BoardSizeMilliseconds[boardSize] =
                                boardSizeTimer.ElapsedMilliseconds;
                        }
                    }

                    currentRows =
                        rows;

                    currentColumns =
                        columns;

                    return candidate;
                }

                boardSizeTimer.Stop();

                if (diagnostics != null)
                {
                    string boardSize =
                        $"{rows}x{columns}";

                    if (diagnostics.BoardSizeMilliseconds.ContainsKey(
                        boardSize))
                    {
                        diagnostics.BoardSizeMilliseconds[boardSize] +=
                            boardSizeTimer.ElapsedMilliseconds;
                    }
                    else
                    {
                        diagnostics.BoardSizeMilliseconds[boardSize] =
                            boardSizeTimer.ElapsedMilliseconds;
                    }
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
    int columns,
    NumberPushGenerationDiagnostics? diagnostics)
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
                if (diagnostics != null)
                {
                    diagnostics.WallGenerationFailures++;
                }

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
                    {
                        List<Point> reachable =
                            GetWallReachablePositions(
                                level,
                                level.Crates[index].Position,
                                level.Crates[index].Distance);

                        return reachable.Count;
                    })
                .ToList();

            Dictionary<int, List<Point>> reachableGoals =
                new Dictionary<int, List<Point>>();

            foreach (int crateIndex in crateOrder)
            {
                NumberPushCrate crate =
                    level.Crates[crateIndex];

                List<Point> reachable =
                    GetWallReachablePositions(
                        level,
                        crate.Position,
                        crate.Distance)
                    .Where(
                        cell =>
                            !cratePositions.Contains(cell))
                    .ToList();

                Shuffle(
                    reachable);

                if (reachable.Count == 0)
                {
                    if (diagnostics != null)
                    {
                        diagnostics.WallReachabilityFailures++;
                    }

                    return null;
                }

                reachableGoals[crateIndex] =
                    reachable;
            }

            if (!TryAssignGoals(
                crateOrder,
                reachableGoals,
                0,
                usedGoals,
                level))
            {
                if (diagnostics != null)
                {
                    diagnostics.WallReachabilityFailures++;
                }

                return null;
            }

            bool cratesCanReachGoals =
    CanCratesReachAssignedGoals(
        level,
        crateOrder);

            if (diagnostics != null)
            {
                if (cratesCanReachGoals)
                {
                    diagnostics.CrateReachabilityPasses++;
                }
                else
                {
                    diagnostics.CrateReachabilityFailures++;
                }
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

        private bool TryAssignGoals(
    List<int> crateOrder,
    Dictionary<int, List<Point>> reachableGoals,
    int crateOrderIndex,
    HashSet<Point> usedGoals,
    NumberPushLevel level)
        {
            if (crateOrderIndex >= crateOrder.Count)
            {
                return true;
            }

            int crateIndex =
                crateOrder[crateOrderIndex];

            List<Point> goals =
                reachableGoals[crateIndex];

            foreach (Point goal in goals)
            {
                if (usedGoals.Contains(goal))
                {
                    continue;
                }

                usedGoals.Add(goal);

                level.Goals.Add(
                    goal);

                if (TryAssignGoals(
                    crateOrder,
                    reachableGoals,
                    crateOrderIndex + 1,
                    usedGoals,
                    level))
                {
                    return true;
                }

                level.Goals.RemoveAt(
                    level.Goals.Count - 1);

                usedGoals.Remove(goal);
            }

            return false;
        }

        private bool CanCratesReachAssignedGoals(
    NumberPushLevel level,
    List<int> crateOrder)
        {
            for (int orderIndex = 0;
                 orderIndex < crateOrder.Count;
                 orderIndex++)
            {
                int crateIndex =
                    crateOrder[orderIndex];

                NumberPushCrate crate =
                    level.Crates[crateIndex];

                Point goal =
                    level.Goals[orderIndex];

                if (!CanCrateReachGoal(
                    level,
                    crate.Position,
                    crate.Distance,
                    goal))
                {
                    return false;
                }
            }

            return true;
        }

        private bool CanCrateReachGoal(
            NumberPushLevel level,
            Point start,
            int distance,
            Point goal)
        {
            if (start == goal)
            {
                return true;
            }

            Queue<Point> queue =
                new Queue<Point>();

            HashSet<Point> visited =
                new HashSet<Point>();

            queue.Enqueue(
                start);

            visited.Add(
                start);

            while (queue.Count > 0)
            {
                Point position =
                    queue.Dequeue();

                Point[] directions =
                {
            new Point(0, -1),
            new Point(1, 0),
            new Point(0, 1),
            new Point(-1, 0)
        };

                foreach (Point direction in directions)
                {
                    Point destination =
                        new Point(
                            position.X +
                                direction.X * distance,
                            position.Y +
                                direction.Y * distance);

                    if (!CanMoveCrate(
                        level,
                        position,
                        direction,
                        distance))
                    {
                        continue;
                    }

                    if (destination == goal)
                    {
                        return true;
                    }

                    if (visited.Add(
                        destination))
                    {
                        queue.Enqueue(
                            destination);
                    }
                }
            }

            return false;
        }

        private bool CanMoveCrate(
            NumberPushLevel level,
            Point position,
            Point direction,
            int distance)
        {
            for (int step = 1;
                 step <= distance;
                 step++)
            {
                Point cell =
                    new Point(
                        position.X +
                            direction.X * step,
                        position.Y +
                            direction.Y * step);

                if (cell.X < 0 ||
                    cell.X >= level.Columns ||
                    cell.Y < 0 ||
                    cell.Y >= level.Rows)
                {
                    return false;
                }

                if (level.Walls.Any(
     wall =>
         wall.Contains(
             cell)))
                {
                    return false;
                }
            }

            return true;
        }

        private List<Point> GetWallReachablePositions(
    NumberPushLevel level,
    Point startPosition,
    int distance)
        {
            HashSet<Point> visited =
                new HashSet<Point>();

            Queue<Point> queue =
                new Queue<Point>();

            visited.Add(
                startPosition);

            queue.Enqueue(
                startPosition);

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
                    Point finalPosition =
                        current;

                    bool valid =
                        true;

                    for (int step = 1;
                         step <= distance;
                         step++)
                    {
                        Point next =
                            new Point(
                                current.X +
                                    direction.X * step,
                                current.Y +
                                    direction.Y * step);

                        if (IsWall(
                            level,
                            next))
                        {
                            valid = false;
                            break;
                        }

                        finalPosition =
                            next;
                    }

                    if (!valid)
                    {
                        continue;
                    }

                    if (visited.Add(
                        finalPosition))
                    {
                        queue.Enqueue(
                            finalPosition);
                    }
                }
            }

            visited.Remove(
                startPosition);

            return visited.ToList();
        }

        private int GetCrateDistance(
    NumberPushDifficulty difficulty,
    int crateIndex)
        {
            int requiredDistance =
    1 +
    difficulty.Complexity / 5;

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