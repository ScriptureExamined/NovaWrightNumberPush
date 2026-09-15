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
                        CalculateCrateInteractions(
                            candidate,
                            diagnostics);
                    }

                    bool noReachableGoals;
                    int failedCrateDistance;

                    if (!CanCratesReachDistinctGoals(
                            candidate,
                            out noReachableGoals,
                            out failedCrateDistance))
                    {
                        if (diagnostics != null)
                        {
                            diagnostics.PreSolverRejectedCandidates++;

                            if (noReachableGoals)
                            {
                                diagnostics.PreSolverNoReachableGoals++;

                                switch (failedCrateDistance)
                                {
                                    case 1:
                                        diagnostics.PreSolverFailedCrateDistance1++;
                                        break;

                                    case 2:
                                        diagnostics.PreSolverFailedCrateDistance2++;
                                        break;

                                    case 3:
                                        diagnostics.PreSolverFailedCrateDistance3++;
                                        break;

                                    default:
                                        diagnostics.PreSolverFailedCrateDistance4Plus++;
                                        break;
                                }
                            }
                            else
                            {
                                diagnostics.PreSolverNoDistinctGoalMatching++;
                            }
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

                    NumberPushSolution solution =
    solver.FindSolution();

                    int minimumSolution =
                        solution.MinimumPushes;

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

                            diagnostics.UnsolvableStatesExplored +=
                                solver.StatesExplored;

                            diagnostics.UnsolvableZeroLegalPushStates +=
                                solver.ZeroLegalPushStates;

                            diagnostics.UnsolvableMaximumLegalPushes =
                                Math.Max(
                                    diagnostics.UnsolvableMaximumLegalPushes,
                                    solver.MaximumLegalPushes);

                            diagnostics.UnsolvableDuplicateStates +=
                                solver.DuplicateStates;
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

                        diagnostics.MinimumPushes =
                            solution.MinimumPushes;

                        for (int crateIndex = 0;
     crateIndex < candidate.Crates.Count;
     crateIndex++)
                        {
                            NumberPushCrate crate =
                                candidate.Crates[crateIndex];

                            diagnostics.CrateStartPositions[crateIndex] =
                                crate.Position;

                            diagnostics.CrateDistances[crateIndex] =
                                crate.Distance;

                            diagnostics.CrateReachableGoals[crateIndex] =
                                GetReachableGoals(
                                    candidate,
                                    crate.Position,
                                    crate.Distance);

                            diagnostics.CrateReachablePositions[crateIndex] =
                                GetReachablePositions(
                                    candidate,
                                    crate.Position,
                                    crate.Distance);

                            diagnostics.CrateGoalPushDistances[crateIndex] =
                                GetCrateGoalPushDistances(
                                    candidate,
                                    crate.Position,
                                    crate.Distance);
                        }

                        diagnostics.CratesMoved =
                            solution.CratesMoved;

                        int previousCrateIndex =
    -1;

                        int currentConsecutivePushes =
                            0;

                        int maximumConsecutivePushes =
                            0;

                        int cratePushOrderChanges =
                            0;

                        foreach (NumberPushSolutionStep step in
                                 solution.Steps)
                        {
                            if (step.CrateIndex !=
                                previousCrateIndex)
                            {
                                if (previousCrateIndex != -1)
                                {
                                    cratePushOrderChanges++;
                                }

                                currentConsecutivePushes = 1;

                                previousCrateIndex =
                                    step.CrateIndex;
                            }
                            else
                            {
                                currentConsecutivePushes++;
                            }

                            maximumConsecutivePushes =
                                Math.Max(
                                    maximumConsecutivePushes,
                                    currentConsecutivePushes);
                        }

                        diagnostics.CratePushOrderChanges =
                            cratePushOrderChanges;

                        diagnostics.MaximumConsecutivePushes =
                            maximumConsecutivePushes;

                        diagnostics.CratePushCounts =
                            new Dictionary<int, int>(
                                solution.CratePushCounts);

                        diagnostics.CrateFirstPushNumbers =
                            new Dictionary<int, int>(
                                solution.CrateFirstPushNumbers);

                        diagnostics.CrateLastPushNumbers =
                            new Dictionary<int, int>(
                                solution.CrateLastPushNumbers);

                        diagnostics.CrateGoalPositions =
    new Dictionary<int, Point>(
        solution.CrateGoalPositions);
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

        private Dictionary<Point, int> GetCrateGoalPushDistances(
    NumberPushLevel level,
    Point startPosition,
    int distance)
        {
            Dictionary<Point, int> goalDistances =
                new Dictionary<Point, int>();

            Dictionary<Point, int> distances =
                new Dictionary<Point, int>();

            Queue<Point> queue =
                new Queue<Point>();

            distances[startPosition] =
                0;

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

                int currentDistance =
                    distances[current];

                foreach (Point direction in directions)
                {
                    Point destination =
                        new Point(
                            current.X +
                                direction.X * distance,
                            current.Y +
                                direction.Y * distance);

                    if (!CanMoveCrateDistance(
                            level,
                            current,
                            direction,
                            distance))
                    {
                        continue;
                    }

                    if (distances.ContainsKey(
                            destination))
                    {
                        continue;
                    }

                    distances[destination] =
                        currentDistance + 1;

                    queue.Enqueue(
                        destination);
                }
            }

            foreach (Point goal in level.Goals)
            {
                if (distances.TryGetValue(
                        goal,
                        out int pushDistance))
                {
                    goalDistances[goal] =
                        pushDistance;
                }
            }

            return goalDistances;
        }

        private bool CanCratesReachDistinctGoals(
    NumberPushLevel level,
    out bool noReachableGoals,
    out int failedCrateDistance)
        {
            noReachableGoals = false;
            failedCrateDistance = 0;

            List<HashSet<Point>> reachableGoals =
                new List<HashSet<Point>>();

            foreach (NumberPushCrate crate in level.Crates)
            {
                HashSet<Point> goals =
                    GetReachableGoals(
                        level,
                        crate.Position,
                        crate.Distance);

                if (goals.Count == 0)
                {
                    noReachableGoals = true;
                    failedCrateDistance = crate.Distance;
                    return false;
                }

                reachableGoals.Add(
                    goals);
            }

            reachableGoals =
                reachableGoals
                    .OrderBy(
                        goals =>
                            goals.Count)
                    .ToList();

            HashSet<Point> assignedGoals =
                new HashSet<Point>();

            return TryMatchGoals(
                reachableGoals,
                0,
                assignedGoals);
        }

        private HashSet<Point> GetReachablePositions(
    NumberPushLevel level,
    Point startPosition,
    int distance)
        {
            HashSet<Point> reachable =
                new();

            Queue<Point> queue =
                new();

            reachable.Add(startPosition);
            queue.Enqueue(startPosition);

            while (queue.Count > 0)
            {
                Point current =
                    queue.Dequeue();

                foreach (Point direction in
                         new[]
                         {
                     new Point(1, 0),
                     new Point(-1, 0),
                     new Point(0, 1),
                     new Point(0, -1)
                         })
                {
                    Point destination =
                        new Point(
                            current.X + direction.X * distance,
                            current.Y + direction.Y * distance);

                    if (!CanMoveCrateDistance(
                            level,
                            current,
                            direction,
                            distance))
                    {
                        continue;
                    }

                    if (reachable.Add(destination))
                    {
                        queue.Enqueue(destination);
                    }
                }
            }

            reachable.Remove(startPosition);

            return reachable;
        }

        private HashSet<Point> GetReachableGoals(
            NumberPushLevel level,
            Point start,
            int distance)
        {
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
                    Point destination =
                        new Point(
                            current.X +
                                direction.X * distance,
                            current.Y +
                                direction.Y * distance);

                    if (!CanMoveCrateDistance(
                            level,
                            current,
                            direction,
                            distance))
                    {
                        continue;
                    }

                    if (visited.Add(
                            destination))
                    {
                        queue.Enqueue(
                            destination);
                    }
                }
            }

            return level.Goals
                .Where(
                    goal =>
                        visited.Contains(
                            goal))
                .ToHashSet();
        }

        private bool CanMoveCrateDistance(
            NumberPushLevel level,
            Point start,
            Point direction,
            int distance)
        {
            for (int step = 1;
                 step <= distance;
                 step++)
            {
                Point position =
                    new Point(
                        start.X +
                            direction.X * step,
                        start.Y +
                            direction.Y * step);

                if (IsWall(
                        level,
                        position))
                {
                    return false;
                }
            }

            return true;
        }

        private bool TryMatchGoals(
            List<HashSet<Point>> reachableGoals,
            int crateIndex,
            HashSet<Point> assignedGoals)
        {
            if (crateIndex >=
                reachableGoals.Count)
            {
                return true;
            }

            foreach (Point goal in
                     reachableGoals[crateIndex])
            {
                if (assignedGoals.Contains(
                        goal))
                {
                    continue;
                }

                assignedGoals.Add(
                    goal);

                if (TryMatchGoals(
                        reachableGoals,
                        crateIndex + 1,
                        assignedGoals))
                {
                    return true;
                }

                assignedGoals.Remove(
                    goal);
            }

            return false;
        }

        private void CalculateCrateInteractions(
    NumberPushLevel level,
    NumberPushGenerationDiagnostics diagnostics)
        {
            diagnostics.CrateInteractionPairs = 0;
            diagnostics.CrateInteractionBlocks = 0;

            HashSet<string> interactionPairs =
                new HashSet<string>();

            Point[] directions =
            {
        new Point(0, -1),
        new Point(0, 1),
        new Point(-1, 0),
        new Point(1, 0)
    };

            for (int firstIndex = 0;
                 firstIndex < level.Crates.Count;
                 firstIndex++)
            {
                NumberPushCrate firstCrate =
                    level.Crates[firstIndex];

                foreach (Point direction in directions)
                {
                    bool wallClear =
                        true;

                    for (int step = 1;
                         step <= firstCrate.Distance;
                         step++)
                    {
                        Point position =
                            new Point(
                                firstCrate.Position.X +
                                    direction.X * step,
                                firstCrate.Position.Y +
                                    direction.Y * step);

                        if (IsWall(
                                level,
                                position))
                        {
                            wallClear = false;
                            break;
                        }
                    }

                    if (!wallClear)
                    {
                        continue;
                    }

                    for (int secondIndex = 0;
                         secondIndex < level.Crates.Count;
                         secondIndex++)
                    {
                        if (firstIndex == secondIndex)
                        {
                            continue;
                        }

                        NumberPushCrate secondCrate =
                            level.Crates[secondIndex];

                        for (int step = 1;
                             step <= firstCrate.Distance;
                             step++)
                        {
                            Point position =
                                new Point(
                                    firstCrate.Position.X +
                                        direction.X * step,
                                    firstCrate.Position.Y +
                                        direction.Y * step);

                            if (position ==
                                secondCrate.Position)
                            {
                                diagnostics.CrateInteractionBlocks++;

                                int lowerIndex =
                                    Math.Min(
                                        firstIndex,
                                        secondIndex);

                                int higherIndex =
                                    Math.Max(
                                        firstIndex,
                                        secondIndex);

                                interactionPairs.Add(
                                    $"{lowerIndex}:{higherIndex}");

                                break;
                            }
                        }
                    }
                }
            }

            diagnostics.CrateInteractionPairs =
                interactionPairs.Count;
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

            List<Point> cratePositions =
    new List<Point>();

            for (int crateIndex = 0;
                 crateIndex < crateCount;
                 crateIndex++)
            {
                int distance =
                    GetCrateDistance(
                        difficulty,
                        crateIndex);

                List<Point> viablePositions =
                    new List<Point>();

                int bestMoveCount =
                    -1;

                foreach (Point candidatePosition in availableCells)
                {
                    int moveCount =
                        0;

                    Point[] directions =
                    {
            new Point(0, -1),
            new Point(0, 1),
            new Point(-1, 0),
            new Point(1, 0)
        };

                    foreach (Point direction in directions)
                    {
                        if (CanMoveCrateDistance(
                                level,
                                candidatePosition,
                                direction,
                                distance))
                        {
                            moveCount++;
                        }
                    }

                    if (moveCount > bestMoveCount)
                    {
                        bestMoveCount =
                            moveCount;

                        viablePositions.Clear();

                        viablePositions.Add(
                            candidatePosition);
                    }
                    else if (moveCount == bestMoveCount)
                    {
                        viablePositions.Add(
                            candidatePosition);
                    }
                }

                if (viablePositions.Count == 0 ||
                    bestMoveCount == 0)
                {
                    return null;
                }

                Point cratePosition =
                    viablePositions[
                        random.Next(
                            viablePositions.Count)];

                cratePositions.Add(
                    cratePosition);

                availableCells.Remove(
                    cratePosition);

                level.Crates.Add(
                    new NumberPushCrate(
                        cratePosition,
                        distance));
            }

            HashSet<Point> assignedGoals =
    new HashSet<Point>();

            Dictionary<int, HashSet<Point>> crateReachableGoals =
                new Dictionary<int, HashSet<Point>>();

            foreach (Point candidateGoal in availableCells)
            {
                for (int crateIndex = 0;
                     crateIndex < crateCount;
                     crateIndex++)
                {
                    NumberPushCrate crate =
                        level.Crates[crateIndex];

                    HashSet<Point> reachablePositions =
                        GetReachablePositions(
                            level,
                            crate.Position,
                            crate.Distance);

                    if (reachablePositions.Contains(
                            candidateGoal))
                    {
                        if (!crateReachableGoals.ContainsKey(
                                crateIndex))
                        {
                            crateReachableGoals[crateIndex] =
                                new HashSet<Point>();
                        }

                        crateReachableGoals[crateIndex].Add(
                            candidateGoal);
                    }
                }
            }

            for (int crateIndex = 0;
                 crateIndex < crateCount;
                 crateIndex++)
            {
                if (!crateReachableGoals.ContainsKey(
                        crateIndex) ||
                    crateReachableGoals[crateIndex].Count == 0)
                {
                    return null;
                }
            }

            while (level.Goals.Count < crateCount)
            {
                List<Point> goalCandidates =
                    availableCells
                        .Where(
                            position =>
                                !assignedGoals.Contains(
                                    position))
                        .Where(
                            position =>
                                crateReachableGoals.Any(
                                    pair =>
                                        pair.Value.Contains(
                                            position)))
                        .ToList();

                if (goalCandidates.Count == 0)
                {
                    return null;
                }

                int bestCompatibility =
                    int.MaxValue;

                List<Point> bestGoals =
                    new List<Point>();

                foreach (Point goal in goalCandidates)
                {
                    int compatibility =
                        crateReachableGoals.Count(
                            pair =>
                                pair.Value.Contains(
                                    goal));

                    if (compatibility <
                        bestCompatibility)
                    {
                        bestCompatibility =
                            compatibility;

                        bestGoals.Clear();

                        bestGoals.Add(
                            goal);
                    }
                    else if (compatibility ==
                             bestCompatibility)
                    {
                        bestGoals.Add(
                            goal);
                    }
                }

                Point selectedGoal =
                    bestGoals[
                        random.Next(
                            bestGoals.Count)];

                assignedGoals.Add(
                    selectedGoal);

                level.Goals.Add(
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

        private int GetCrateDistance(
    NumberPushDifficulty difficulty,
    int crateIndex)
        {
            return difficulty.MinimumCrateDistance;
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
            List<Point> availableCells =
                GetAvailableCells(
                    level);

            if (availableCells.Count == 0)
            {
                return false;
            }

            HashSet<Point> visited =
                new HashSet<Point>();

            Queue<Point> queue =
                new Queue<Point>();

            Point start =
                availableCells[0];

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

                    if (IsWall(
                        level,
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