using NovaWrightNumberPush;
using System.Text;

namespace NovaWright.NumberPush.LevelGenerator
{
    public class NumberPushSolver
    {
        private readonly NumberPushLevel level;

        private readonly int rows;
        private readonly int columns;

        private readonly HashSet<Point> wallPositions;
        private readonly HashSet<Point> goalPositions;

        // Reusable arrays for player reachability.
        private readonly int[] reachableVisit;
        private readonly int[] reachableQueue;

        // Reusable array for crate occupancy.
        private readonly int[] crateOccupancyVisit;

        private int crateOccupancyVisitId;

        private int reachableVisitId;

        private int legalPushesForState;

        public int StatesExplored { get; private set; }

        public long TotalLegalPushes { get; private set; }

        public int MaximumLegalPushes { get; private set; }

        public int ZeroLegalPushStates { get; private set; }

        public long DuplicateStates { get; private set; }

        public Dictionary<int, long> GoalProgressStates { get; } =
            new Dictionary<int, long>();

        public long GoalProgressIncreases { get; private set; }

        public long GoalProgressDecreases { get; private set; }

        public long GoalProgressUnchanged { get; private set; }

        public long DuplicateCrateConfigurations { get; private set; }

        public NumberPushSolver(NumberPushLevel level)
        {
            this.level = level;

            rows = level.Rows;
            columns = level.Columns;

            wallPositions =
                level.Walls
                    .Select(
                        wall =>
                            new Point(
                                wall.X,
                                wall.Y))
                    .ToHashSet();

            goalPositions =
                level.Goals.ToHashSet();

            int cellCount =
                rows * columns;

            reachableVisit =
                new int[cellCount];

            reachableQueue =
                new int[cellCount];

            crateOccupancyVisit =
                new int[cellCount];

            reachableVisitId = 0;

            crateOccupancyVisitId = 0;
        }

        /// <summary>
        /// Finds the minimum-push solution for the level.
        ///
        /// Returns:
        ///     A NumberPushSolution with IsSolved = true
        ///     when the level is solvable.
        ///
        ///     A NumberPushSolution with IsSolved = false
        ///     when the level is unsolvable.
        /// </summary>
        public NumberPushSolution FindSolution()
        {
            Point[] crateStartPositions =
                level.Crates
                    .Select(crate => crate.Position)
                    .ToArray();

            List<int> crateDistances =
                level.Crates
                    .Select(crate => crate.Distance)
                    .ToList();

            Queue<SolverState> queue = new();

            HashSet<SolverStateKey> visited =
                new();

            //TODO: Next experiment Temporarily remove the diagnostic-only work:

            //HashSet<string> visitedCrateConfigurations =
            //    new();

            StatesExplored = 0;

            TotalLegalPushes = 0;

            MaximumLegalPushes = 0;

            ZeroLegalPushStates = 0;

            GoalProgressStates.Clear();

            GoalProgressIncreases = 0;

            GoalProgressDecreases = 0;

            GoalProgressUnchanged = 0;

            DuplicateCrateConfigurations = 0;

            SolverState startState =
                new SolverState(
                    level.PlayerStart,
                    crateStartPositions,
                    0,
                    null,
                    null);

            queue.Enqueue(startState);

            visited.Add(
                CreateStateKey(
                    level.PlayerStart,
                    crateStartPositions));

            //TODO: Next experiment Temporarily remove the diagnostic-only work:
            //visitedCrateConfigurations.Add(
            //    CreateCrateConfigurationKey(
            //        crateStartPositions));

            while (queue.Count > 0)
            {
                SolverState state =
                    queue.Dequeue();

                StatesExplored++;

                int cratesOnGoals =
                    CountCratesOnGoals(
                        state.CratePositions);

                if (GoalProgressStates.ContainsKey(
                    cratesOnGoals))
                {
                    GoalProgressStates[cratesOnGoals]++;
                }
                else
                {
                    GoalProgressStates[cratesOnGoals] = 1;
                }

                legalPushesForState = 0;

                if (IsComplete(state.CratePositions))
                {
                    return BuildSolution(state);
                }

                // Build the reusable crate occupancy map for this state.
                BuildCrateOccupancy(
                    state.CratePositions);

                // Mark every position the player can reach without
                // moving any crates.
                int currentReachableVisitId =
                    MarkReachableCells(
                        state.PlayerPosition);

                for (int crateIndex = 0;
                     crateIndex < state.CratePositions.Length;
                     crateIndex++)
                {
                    int distance =
                        crateDistances[crateIndex];

                    TryPush(
                        state,
                        crateIndex,
                        distance,
                        new Point(0, -1),
                        currentReachableVisitId,
                        queue,
                        visited);

                    TryPush(
                        state,
                        crateIndex,
                        distance,
                        new Point(0, 1),
                        currentReachableVisitId,
                        queue,
                        visited);

                    TryPush(
                        state,
                        crateIndex,
                        distance,
                        new Point(-1, 0),
                        currentReachableVisitId,
                        queue,
                        visited);

                    TryPush(
                        state,
                        crateIndex,
                        distance,
                        new Point(1, 0),
                        currentReachableVisitId,
                        queue,
                        visited);
                }

                TotalLegalPushes +=
                    legalPushesForState;

                MaximumLegalPushes =
                    Math.Max(
                        MaximumLegalPushes,
                        legalPushesForState);

                if (legalPushesForState == 0)
                {
                    ZeroLegalPushStates++;
                }
            }

            return new NumberPushSolution
            {
                IsSolved = false,
                MinimumPushes = -1
            };
        }

        private string CreateCrateConfigurationKey(
            List<Point> cratePositions)
        {
            List<(int Distance, int Position)> crateKeys =
                new List<(int Distance, int Position)>(
                    cratePositions.Count);

            for (int i = 0;
                 i < cratePositions.Count;
                 i++)
            {
                Point position =
                    cratePositions[i];

                int cellIndex =
                    position.Y * columns +
                    position.X;

                int distance =
                    level.Crates[i].Distance;

                crateKeys.Add(
                    (distance, cellIndex));
            }

            crateKeys.Sort(
                (left, right) =>
                {
                    int distanceComparison =
                        left.Distance.CompareTo(
                            right.Distance);

                    if (distanceComparison != 0)
                    {
                        return distanceComparison;
                    }

                    return left.Position.CompareTo(
                        right.Position);
                });

            StringBuilder key =
                new StringBuilder(
                    crateKeys.Count * 8);

            for (int i = 0;
                 i < crateKeys.Count;
                 i++)
            {
                if (i > 0)
                {
                    key.Append(',');
                }

                key.Append(
                    crateKeys[i].Distance);

                key.Append(':');

                key.Append(
                    crateKeys[i].Position);
            }

            return key.ToString();
        }

        // Creates the unique key used to determine whether a solver state
        // has already been visited.
        //
        // This version keeps the exact same state semantics as the baseline:
        // - The exact player position remains part of the key.
        // - Crate positions are sorted so equivalent crate configurations
        //   produce the same key.
        // - No crate identity or distance information is added.
        //
        // The optimization removes LINQ allocations from this method.
        // This method is called millions of times during the solver search,
        // so avoiding Select(), OrderBy(), ToList(), and string.Join()
        // allocations is important.
        private SolverStateKey CreateStateKey(
            Point playerPosition,
            IReadOnlyList<Point> cratePositions)
        {
            int[] crateIndexes =
                new int[cratePositions.Count];

            for (
                int crateIndex = 0;
                crateIndex < cratePositions.Count;
                crateIndex++)
            {
                Point position =
                    cratePositions[crateIndex];

                crateIndexes[crateIndex] =
                    position.Y * columns +
                    position.X;
            }

            // Preserve the exact canonical ordering used by
            // the previous string-based state key.
            Array.Sort(
                crateIndexes);

            int playerIndex =
                playerPosition.Y * columns +
                playerPosition.X;

            return new SolverStateKey(
                playerIndex,
                crateIndexes);
        }

        public List<(int CrateIndex, Point Direction)> GetLegalPushes(
            Point playerPosition,
            List<Point> cratePositions)
        {
            List<(int CrateIndex, Point Direction)> legalPushes =
                new();

            BuildCrateOccupancy(
                cratePositions);

            int currentReachableVisitId =
                MarkReachableCells(
                    playerPosition);

            Point[] directions =
            {
                new Point(0, -1),
                new Point(0, 1),
                new Point(-1, 0),
                new Point(1, 0)
            };

            for (int crateIndex = 0;
                 crateIndex < cratePositions.Count;
                 crateIndex++)
            {
                Point cratePosition =
                    cratePositions[crateIndex];

                int distance =
                    level.Crates[crateIndex].Distance;

                foreach (Point direction in directions)
                {
                    Point playerRequiredPosition =
                        new Point(
                            cratePosition.X - direction.X,
                            cratePosition.Y - direction.Y);

                    if (!IsReachable(
                        playerRequiredPosition,
                        currentReachableVisitId))
                    {
                        continue;
                    }

                    Point finalPosition =
                        cratePosition;

                    bool blocked =
                        false;

                    for (int step = 1;
                         step <= distance;
                         step++)
                    {
                        Point testPosition =
                            new Point(
                                cratePosition.X +
                                    direction.X * step,
                                cratePosition.Y +
                                    direction.Y * step);

                        if (IsWall(testPosition) ||
                            IsOccupiedByAnyCrate(
                                testPosition))
                        {
                            blocked = true;
                            break;
                        }

                        finalPosition =
                            testPosition;
                    }

                    if (blocked)
                    {
                        continue;
                    }

                    if (IsStaticCornerDeadlock(
                        finalPosition))
                    {
                        continue;
                    }

                    legalPushes.Add(
                        (crateIndex, direction));
                }
            }

            return legalPushes;
        }

        /// <summary>
        /// Maintains compatibility with the original solver API.
        /// </summary>
        public int FindMinimumPushes()
        {
            NumberPushSolution solution =
                FindSolution();

            return solution.MinimumPushes;
        }

        private int CountCratesOnGoals(
            IReadOnlyList<Point> cratePositions)
        {
            int count = 0;

            foreach (Point cratePosition in cratePositions)
            {
                if (level.Goals.Contains(
                    cratePosition))
                {
                    count++;
                }
            }

            return count;
        }

        private void TryPush(
            SolverState state,
            int crateIndex,
            int distance,
            Point direction,
            int currentReachableVisitId,
            Queue<SolverState> queue,
            HashSet<SolverStateKey> visited)
        {
            Point cratePosition =
                state.CratePositions[crateIndex];

            Point playerRequiredPosition =
                new Point(
                    cratePosition.X - direction.X,
                    cratePosition.Y - direction.Y);

            if (!IsReachable(
                playerRequiredPosition,
                currentReachableVisitId))
            {
                return;
            }

            Point finalPosition =
    cratePosition;

            for (int step = 1;
                 step <= distance;
                 step++)
            {
                int testX =
                    cratePosition.X +
                    direction.X * step;

                int testY =
                    cratePosition.Y +
                    direction.Y * step;

                if (testX < 0 ||
                    testX >= columns ||
                    testY < 0 ||
                    testY >= rows)
                {
                    return;
                }

                int testIndex =
                    testY * columns +
                    testX;

                if (wallPositions.Contains(
                    new Point(
                        testX,
                        testY)))
                {
                    return;
                }

                if (crateOccupancyVisit[testIndex] ==
                    crateOccupancyVisitId)
                {
                    return;
                }

                finalPosition =
                    new Point(
                        testX,
                        testY);
            }

            // Create an independent array snapshot for the successor state.
            // The array must not be reused because the queued state needs
            // its own immutable crate-position configuration.
            Point[] newCratePositions =
                new Point[
                    state.CratePositions.Length];

            Array.Copy(
                state.CratePositions,
                newCratePositions,
                state.CratePositions.Length);

            newCratePositions[crateIndex] =
                finalPosition;

            if (IsStaticCornerDeadlock(
                finalPosition))
            {
                return;
            }

            legalPushesForState++;

            Point newPlayerPosition =
                cratePosition;

            SolverStateKey newStateKey =
                CreateStateKey(
                    newPlayerPosition,
                    newCratePositions);

            if (!visited.Add(newStateKey))
            {
                DuplicateStates++;
                return;
            }

            // Only the pushed crate changes position, so we can determine
            // goal progress by comparing its old and new positions.
            //
            // This avoids scanning every crate with CountCratesOnGoals()
            // for every legal push.
            bool crateStartedOnGoal =
                goalPositions.Contains(
                    cratePosition);

            bool crateEndedOnGoal =
                goalPositions.Contains(
                    finalPosition);

            if (crateEndedOnGoal &&
                !crateStartedOnGoal)
            {
                GoalProgressIncreases++;
            }
            else if (!crateEndedOnGoal &&
                     crateStartedOnGoal)
            {
                GoalProgressDecreases++;
            }
            else
            {
                GoalProgressUnchanged++;
            }

            NumberPushSolutionStep stepData =
                new NumberPushSolutionStep
                {
                    CrateIndex = crateIndex,

                    PlayerStart =
                        state.PlayerPosition,

                    PlayerPushPosition =
                        playerRequiredPosition,

                    CrateStart =
                        cratePosition,

                    CrateEnd =
                        finalPosition,

                    Direction =
                        direction,

                    Distance =
                        distance,

                    PlayerPath =
                        new List<Point>()
                };

            SolverState newState =
                new SolverState(
                    newPlayerPosition,
                    newCratePositions,
                    state.Pushes + 1,
                    state,
                    stepData);

            queue.Enqueue(newState);
        }

        private bool IsStaticCornerDeadlock(
            Point position)
        {
            if (goalPositions.Contains(position))
            {
                return false;
            }

            bool wallUp =
                IsWall(
                    new Point(
                        position.X,
                        position.Y - 1));

            bool wallDown =
                IsWall(
                    new Point(
                        position.X,
                        position.Y + 1));

            bool wallLeft =
                IsWall(
                    new Point(
                        position.X - 1,
                        position.Y));

            bool wallRight =
                IsWall(
                    new Point(
                        position.X + 1,
                        position.Y));

            return
                (wallUp && wallLeft) ||
                (wallUp && wallRight) ||
                (wallDown && wallLeft) ||
                (wallDown && wallRight);
        }

        private NumberPushSolution BuildSolution(
            SolverState finalState)
        {
            List<NumberPushSolutionStep> steps =
                new();

            SolverState? current =
                finalState;

            while (current != null &&
                   current.Parent != null)
            {
                if (current.Step != null)
                {
                    NumberPushSolutionStep step =
                        current.Step;

                    step.PlayerPath =
                        FindPlayerPath(
                            step.PlayerStart,
                            step.PlayerPushPosition,
                            current.Parent.CratePositions);

                    steps.Add(step);
                }

                current = current.Parent;
            }

            steps.Reverse();

            for (int i = 0;
                 i < steps.Count;
                 i++)
            {
                steps[i].PushNumber =
                    i + 1;
            }

            NumberPushSolution solution =
                new NumberPushSolution
                {
                    IsSolved = true,
                    MinimumPushes =
                        finalState.Pushes,
                    Steps = steps
                };

            foreach (NumberPushSolutionStep step in steps)
            {
                if (solution.CratePushCounts.ContainsKey(
                    step.CrateIndex))
                {
                    solution.CratePushCounts[step.CrateIndex]++;
                }
                else
                {
                    solution.CratePushCounts[step.CrateIndex] =
                        1;
                }

                if (!solution.CrateFirstPushNumbers.ContainsKey(
                    step.CrateIndex))
                {
                    solution.CrateFirstPushNumbers[step.CrateIndex] =
                        step.PushNumber;
                }

                solution.CrateLastPushNumbers[step.CrateIndex] =
                    step.PushNumber;

                solution.CrateGoalPositions[step.CrateIndex] =
                    step.CrateEnd;
            }

            return solution;
        }

        private List<Point> FindPlayerPath(
            Point startPosition,
            Point targetPosition,
            IReadOnlyList<Point> cratePositions)
        {
            if (startPosition == targetPosition)
            {
                return new List<Point>
                {
                    startPosition
                };
            }

            Queue<Point> queue = new();

            HashSet<Point> visited = new();

            Dictionary<Point, Point> parents =
                new();

            queue.Enqueue(startPosition);
            visited.Add(startPosition);

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

                    if (IsWall(next))
                    {
                        continue;
                    }

                    if (IsOccupiedByAnyCrate(
                        next,
                        cratePositions))
                    {
                        continue;
                    }

                    if (!visited.Add(next))
                    {
                        continue;
                    }

                    parents[next] =
                        current;

                    if (next == targetPosition)
                    {
                        return ReconstructPlayerPath(
                            startPosition,
                            targetPosition,
                            parents);
                    }

                    queue.Enqueue(next);
                }
            }

            return new List<Point>();
        }

        private List<Point> ReconstructPlayerPath(
            Point startPosition,
            Point targetPosition,
            Dictionary<Point, Point> parents)
        {
            List<Point> path =
                new();

            Point current =
                targetPosition;

            path.Add(current);

            while (current != startPosition)
            {
                if (!parents.TryGetValue(
                    current,
                    out Point parent))
                {
                    return new List<Point>();
                }

                current = parent;
                path.Add(current);
            }

            path.Reverse();

            return path;
        }

        /// <summary>
        /// Builds a reusable occupancy map for the current crate
        /// configuration.
        /// </summary>
        private void BuildCrateOccupancy(
            IReadOnlyList<Point> cratePositions)
        {
            crateOccupancyVisitId++;

            if (crateOccupancyVisitId == int.MaxValue)
            {
                Array.Clear(
                    crateOccupancyVisit,
                    0,
                    crateOccupancyVisit.Length);

                crateOccupancyVisitId = 1;
            }

            foreach (Point cratePosition in cratePositions)
            {
                if (cratePosition.X < 0 ||
                    cratePosition.X >= columns ||
                    cratePosition.Y < 0 ||
                    cratePosition.Y >= rows)
                {
                    continue;
                }

                int index =
                    GetCellIndex(cratePosition);

                crateOccupancyVisit[index] =
                    crateOccupancyVisitId;
            }
        }

        /// <summary>
        /// Marks every cell the player can currently reach without
        /// moving any crates.
        ///
        /// The visitation array and queue are reused between calls.
        /// </summary>
        private int MarkReachableCells(
            Point startPosition)
        {
            reachableVisitId++;

            if (reachableVisitId == int.MaxValue)
            {
                Array.Clear(
                    reachableVisit,
                    0,
                    reachableVisit.Length);

                reachableVisitId = 1;
            }

            if (IsWall(startPosition) ||
                IsOccupiedByAnyCrate(
                    startPosition))
            {
                return reachableVisitId;
            }

            int startIndex =
                GetCellIndex(startPosition);

            int head = 0;
            int tail = 0;

            reachableQueue[tail++] =
                startIndex;

            reachableVisit[startIndex] =
                reachableVisitId;

            while (head < tail)
            {
                int currentIndex =
                    reachableQueue[head++];

                int currentX =
                    currentIndex % columns;

                int currentY =
                    currentIndex / columns;

                MarkReachableNeighbor(
                    currentX,
                    currentY - 1,
                    ref tail);

                MarkReachableNeighbor(
                    currentX,
                    currentY + 1,
                    ref tail);

                MarkReachableNeighbor(
                    currentX - 1,
                    currentY,
                    ref tail);

                MarkReachableNeighbor(
                    currentX + 1,
                    currentY,
                    ref tail);
            }

            return reachableVisitId;
        }

        private void MarkReachableNeighbor(
            int x,
            int y,
            ref int tail)
        {
            if (x < 0 ||
                x >= columns ||
                y < 0 ||
                y >= rows)
            {
                return;
            }

            int index =
                y * columns + x;

            if (reachableVisit[index] ==
                reachableVisitId)
            {
                return;
            }

            Point position =
                new Point(x, y);

            if (IsWall(position))
            {
                return;
            }

            if (crateOccupancyVisit[index] ==
                crateOccupancyVisitId)
            {
                return;
            }

            reachableVisit[index] =
                reachableVisitId;

            reachableQueue[tail++] =
                index;
        }

        private bool IsReachable(
            Point position,
            int currentReachableVisitId)
        {
            if (position.X < 0 ||
                position.X >= columns ||
                position.Y < 0 ||
                position.Y >= rows)
            {
                return false;
            }

            int index =
                GetCellIndex(position);

            return
                reachableVisit[index] ==
                currentReachableVisitId;
        }

        private int GetCellIndex(
            Point position)
        {
            return
                position.Y * columns +
                position.X;
        }

        private bool IsComplete(
            IReadOnlyList<Point> cratePositions)
        {
            foreach (Point cratePosition in cratePositions)
            {
                if (!goalPositions.Contains(cratePosition))
                {
                    return false;
                }
            }

            return true;
        }

        private bool IsWall(Point position)
        {
            if (position.X < 0 ||
                position.X >= columns ||
                position.Y < 0 ||
                position.Y >= rows)
            {
                return true;
            }

            return wallPositions.Contains(position);
        }

        /// <summary>
        /// Checks the reusable occupancy map.
        /// Used during player reachability calculations.
        /// </summary>
        private bool IsOccupiedByAnyCrate(
            Point position)
        {
            if (position.X < 0 ||
                position.X >= columns ||
                position.Y < 0 ||
                position.Y >= rows)
            {
                return false;
            }

            int index =
                GetCellIndex(position);

            return
                crateOccupancyVisit[index] ==
                crateOccupancyVisitId;
        }

        /// <summary>
        /// Checks the supplied crate configuration directly.
        /// Used when reconstructing player paths for a solution.
        /// </summary>
        private bool IsOccupiedByAnyCrate(
            Point position,
            IReadOnlyList<Point> cratePositions)
        {
            return cratePositions.Contains(position);
        }

        private bool IsOccupiedByAnotherCrate(
            Point position,
            Point[] cratePositions,
            int movingCrateIndex)
        {
            for (int i = 0;
                 i < cratePositions.Length;
                 i++)
            {
                if (i == movingCrateIndex)
                {
                    continue;
                }

                if (cratePositions[i] == position)
                {
                    return true;
                }
            }

            return false;
        }

        private class SolverState
        {
            public Point PlayerPosition { get; }

            public Point[] CratePositions { get; }

            public int Pushes { get; }

            public SolverState? Parent { get; }

            public NumberPushSolutionStep? Step { get; }

            public SolverState(
                Point playerPosition,
                Point[] cratePositions,
                int pushes,
                SolverState? parent,
                NumberPushSolutionStep? step)
            {
                PlayerPosition =
                    playerPosition;

                CratePositions =
                    cratePositions;

                Pushes =
                    pushes;

                Parent =
                    parent;

                Step =
                    step;
            }
        }

        private readonly struct SolverStateKey :
            IEquatable<SolverStateKey>
        {
            private readonly int playerPosition;

            private readonly int[] cratePositions;

            public SolverStateKey(
                int playerPosition,
                int[] cratePositions)
            {
                this.playerPosition =
                    playerPosition;

                this.cratePositions =
                    cratePositions;
            }

            public bool Equals(
                SolverStateKey other)
            {
                if (playerPosition !=
                    other.playerPosition)
                {
                    return false;
                }

                if (cratePositions.Length !=
                    other.cratePositions.Length)
                {
                    return false;
                }

                for (
                    int index = 0;
                    index < cratePositions.Length;
                    index++)
                {
                    if (cratePositions[index] !=
                        other.cratePositions[index])
                    {
                        return false;
                    }
                }

                return true;
            }

            public override bool Equals(
                object? obj)
            {
                return obj is SolverStateKey other &&
                       Equals(other);
            }

            public override int GetHashCode()
            {
                HashCode hash =
                    new HashCode();

                hash.Add(
                    playerPosition);

                for (
                    int index = 0;
                    index < cratePositions.Length;
                    index++)
                {
                    hash.Add(
                        cratePositions[index]);
                }

                return hash.ToHashCode();
            }
        }
    }
}