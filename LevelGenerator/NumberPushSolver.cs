using System.Diagnostics;
using System.Text;
using NovaWrightNumberPush;

namespace NovaWright.NumberPush.LevelGenerator
{
    public class NumberPushSolver
    {
        private readonly NumberPushLevel level;

        private readonly int rows;
        private readonly int columns;

        private readonly HashSet<Point> wallPositions;
        private readonly HashSet<Point> goalPositions;

        private readonly bool[] blockedCells;

        private readonly bool[] successorBlockedCells;

        // Reusable arrays for current-state player reachability.
        private readonly int[] reachableVisit;
        private readonly int[] reachableQueue;

        // Reusable arrays for successor-state player reachability.
        private readonly int[] successorReachableVisit;
        private readonly int[] successorReachableQueue;

        // Reusable array for crate occupancy.
        private readonly int[] crateOccupancyVisit;

        private int crateOccupancyVisitId;

        private int reachableVisitId;

        private int successorReachableVisitId;

        private int legalPushesForState;

        public int StatesExplored { get; private set; }

        public long TotalLegalPushes { get; private set; }

        public int MaximumLegalPushes { get; private set; }

        public int ZeroLegalPushStates { get; private set; }

        public int MinimumZeroLegalPushDepth { get; private set; }

        public int LegalPushesBeforeMinimumZeroPushDepth { get; private set; }

        public int FirstZeroPushPlayerAccessBlockedCrates { get; private set; }

        public int FirstZeroPushWallBlockedCrates { get; private set; }

        public int FirstZeroPushCrateBlockedCrates { get; private set; }

        public int FirstZeroPushCornerDeadlockedCrates { get; private set; }

        public int FirstZeroPushUpBlocked { get; private set; }

        public int FirstZeroPushDownBlocked { get; private set; }

        public int FirstZeroPushLeftBlocked { get; private set; }

        public int FirstZeroPushRightBlocked { get; private set; }

        public int FirstZeroPushLastCratePlayerAccessBlocked { get; private set; }

        public int FirstZeroPushLastCrateWallBlocked { get; private set; }

        public int FirstZeroPushLastCrateCrateBlocked { get; private set; }

        public int FirstZeroPushLastCrateCornerDeadlocked { get; private set; }

        public int FirstZeroPushLastCrateLegalPushes { get; private set; }

        public int InitialTryPushLegalPushes { get; private set; }

        public long DuplicateStates { get; private set; }

        public long ExactParentReversalStates { get; private set; }

        public long HashSetDuplicateStates { get; private set; }

        public long UniqueSuccessorStates { get; private set; }

        public long OccupancyMilliseconds { get; private set; }

        public long CurrentReachabilityMilliseconds { get; private set; }

        public long SuccessorReachabilityMilliseconds { get; private set; }

        public long PushValidationMilliseconds { get; private set; }

        public long ArrayCopyMilliseconds { get; private set; }

        public long StateConstructionMilliseconds { get; private set; }

        public long HashSetLookupMilliseconds { get; private set; }

        public long StaticDeadlockMilliseconds { get; private set; }

        public Dictionary<int, long> GoalProgressStates { get; } = new Dictionary<int, long>();

        public long GoalProgressIncreases { get; private set; }

        public long GoalProgressDecreases { get; private set; }

        public long GoalProgressUnchanged { get; private set; }

        public long DuplicateCrateConfigurations { get; private set; }

        public NumberPushSolver(NumberPushLevel level)
        {
            this.level = level;

            rows = level.Rows;

            columns = level.Columns;

            int cellCount = rows * columns;

            wallPositions = level.Walls.Select(wall => new Point(wall.X, wall.Y)).ToHashSet();

            goalPositions = level.Goals.ToHashSet();

            blockedCells = new bool[cellCount];

            for (int index = 0; index < cellCount; index++)
            {
                int x = index % columns;

                int y = index / columns;
            }

            foreach (Rectangle wall in level.Walls)
            {
                if (wall.X < 0 || wall.X >= columns || wall.Y < 0 || wall.Y >= rows)
                {
                    continue;
                }

                blockedCells[wall.Y * columns + wall.X] = true;
            }

            reachableVisit = new int[cellCount];

            reachableQueue = new int[cellCount];

            successorReachableVisit = new int[cellCount];

            successorReachableQueue = new int[cellCount];

            crateOccupancyVisit = new int[cellCount];

            reachableVisitId = 0;

            successorReachableVisitId = 0;

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
            Point[] crateStartPositions = level.Crates.Select(crate => crate.Position).ToArray();

            List<int> crateDistances = level.Crates.Select(crate => crate.Distance).ToList();

            Queue<SolverState> queue = new();

            HashSet<SolverState> visited = new(new SolverStateComparer());

            StatesExplored = 0;

            TotalLegalPushes = 0;

            MaximumLegalPushes = 0;

            ZeroLegalPushStates = 0;

            MinimumZeroLegalPushDepth = -1;

            LegalPushesBeforeMinimumZeroPushDepth = -1;

            FirstZeroPushPlayerAccessBlockedCrates = 0;

            FirstZeroPushWallBlockedCrates = 0;

            FirstZeroPushCrateBlockedCrates = 0;

            FirstZeroPushCornerDeadlockedCrates = 0;

            FirstZeroPushUpBlocked = 0;

            FirstZeroPushDownBlocked = 0;

            FirstZeroPushLeftBlocked = 0;

            FirstZeroPushRightBlocked = 0;

            FirstZeroPushLastCratePlayerAccessBlocked = 0;

            FirstZeroPushLastCrateWallBlocked = 0;

            FirstZeroPushLastCrateCrateBlocked = 0;

            FirstZeroPushLastCrateCornerDeadlocked = 0;

            FirstZeroPushLastCrateLegalPushes = 0;

            DuplicateStates = 0;

            ExactParentReversalStates = 0;

            HashSetDuplicateStates = 0;

            UniqueSuccessorStates = 0;

            OccupancyMilliseconds = 0;

            CurrentReachabilityMilliseconds = 0;

            SuccessorReachabilityMilliseconds = 0;

            PushValidationMilliseconds = 0;

            ArrayCopyMilliseconds = 0;

            StateConstructionMilliseconds = 0;

            HashSetLookupMilliseconds = 0;

            StaticDeadlockMilliseconds = 0;

            GoalProgressStates.Clear();

            GoalProgressIncreases = 0;

            GoalProgressDecreases = 0;

            GoalProgressUnchanged = 0;

            DuplicateCrateConfigurations = 0;

            InitialTryPushLegalPushes = 0;

            int startPositionHashSum = 0;

            int startPositionHashSquareSum = 0;

            foreach (Point position in crateStartPositions)
            {
                int positionHash = HashCode.Combine(position.X, position.Y);

                startPositionHashSum += positionHash;

                startPositionHashSquareSum += positionHash * positionHash;
            }

            long[] startCanonicalCrateKeys = CreateCanonicalCrateKeys(crateStartPositions);

            SolverState startState = new SolverState(
                level.PlayerStart,
                crateStartPositions,
                0,
                null,
                null,
                startPositionHashSum,
                startPositionHashSquareSum,
                startCanonicalCrateKeys
            );

            queue.Enqueue(startState);

            // Build the initial player's reachable region using
            // the initial crate configuration.
            BuildCrateOccupancy(crateStartPositions);

            int startReachableVisitId = MarkReachableCells(level.PlayerStart);

            int startPlayerRegion = GetPlayerRegionKey(startReachableVisitId);

            startState.PlayerRegion = startPlayerRegion;

            visited.Add(startState);

            while (queue.Count > 0)
            {
                SolverState state = queue.Dequeue();

                StatesExplored++;

                int cratesOnGoals = CountCratesOnGoals(state.CratePositions);

                if (GoalProgressStates.ContainsKey(cratesOnGoals))
                {
                    GoalProgressStates[cratesOnGoals]++;
                }
                else
                {
                    GoalProgressStates[cratesOnGoals] = 1;
                }

                legalPushesForState = 0;

                if (StatesExplored == 1)
                {
                    InitialTryPushLegalPushes = 0;
                }

                if (IsComplete(state.CratePositions))
                {
                    return BuildSolution(state);
                }

                // Build the reusable crate occupancy map for this state.
                long timingStart = Stopwatch.GetTimestamp();

                BuildCrateOccupancy(state.CratePositions);

                OccupancyMilliseconds += Stopwatch.GetElapsedTime(timingStart).Ticks;

                // Mark every position the player can reach without
                // moving any crates.
                timingStart = Stopwatch.GetTimestamp();

                int currentReachableVisitId = MarkReachableCells(state.PlayerPosition);

                CurrentReachabilityMilliseconds += Stopwatch.GetElapsedTime(timingStart).Ticks;

                for (int crateIndex = 0; crateIndex < state.CratePositions.Length; crateIndex++)
                {
                    int distance = crateDistances[crateIndex];

                    TryPush(
                        state,
                        crateIndex,
                        distance,
                        new Point(0, -1),
                        currentReachableVisitId,
                        queue,
                        visited
                    );

                    TryPush(
                        state,
                        crateIndex,
                        distance,
                        new Point(0, 1),
                        currentReachableVisitId,
                        queue,
                        visited
                    );

                    TryPush(
                        state,
                        crateIndex,
                        distance,
                        new Point(-1, 0),
                        currentReachableVisitId,
                        queue,
                        visited
                    );

                    TryPush(
                        state,
                        crateIndex,
                        distance,
                        new Point(1, 0),
                        currentReachableVisitId,
                        queue,
                        visited
                    );
                }

                if (StatesExplored == 1)
                {
                    InitialTryPushLegalPushes = legalPushesForState;
                }

                TotalLegalPushes += legalPushesForState;

                MaximumLegalPushes = Math.Max(MaximumLegalPushes, legalPushesForState);

                if (legalPushesForState == 0)
                {
                    ZeroLegalPushStates++;

                    if (MinimumZeroLegalPushDepth == -1)
                    {
                        MinimumZeroLegalPushDepth = state.Pushes;

                        if (state.Parent == null)
                        {
                            LegalPushesBeforeMinimumZeroPushDepth = 0;
                        }
                        else
                        {
                            List<Point> parentCratePositions = state.Parent.CratePositions.ToList();

                            LegalPushesBeforeMinimumZeroPushDepth = GetLegalPushes(
                                state.Parent.PlayerPosition,
                                parentCratePositions
                            ).Count;
                        }

                        AnalyzeFirstZeroPushState(state);
                    }
                }
            }

            return new NumberPushSolution { IsSolved = false, MinimumPushes = -1 };
        }

        private long[] CreateCanonicalCrateKeys(IReadOnlyList<Point> cratePositions)
        {
            long[] keys = new long[cratePositions.Count];

            for (int index = 0; index < cratePositions.Count; index++)
            {
                Point position = cratePositions[index];

                int cellIndex = position.Y * columns + position.X;

                int distance = level.Crates[index].Distance;

                keys[index] = ((long)distance << 32) | (uint)cellIndex;
            }

            Array.Sort(keys);

            return keys;
        }

        private long[] CreateSuccessorCanonicalCrateKeys(
            SolverState state,
            int crateIndex,
            Point cratePosition,
            Point finalPosition,
            int distance
        )
        {
            long oldKey =
                ((long)distance << 32) | (uint)(cratePosition.Y * columns + cratePosition.X);

            long newKey =
                ((long)distance << 32) | (uint)(finalPosition.Y * columns + finalPosition.X);

            long[] keys = new long[state.CanonicalCrateKeys.Length];

            Array.Copy(state.CanonicalCrateKeys, keys, state.CanonicalCrateKeys.Length);

            int oldIndex = Array.BinarySearch(keys, oldKey);

            if (oldIndex < 0)
            {
                throw new InvalidOperationException("The old canonical crate key was not found.");
            }

            for (int index = oldIndex; index < keys.Length - 1; index++)
            {
                keys[index] = keys[index + 1];
            }

            keys[keys.Length - 1] = 0;

            int newIndex = Array.BinarySearch(keys, 0, keys.Length - 1, newKey);

            if (newIndex < 0)
            {
                newIndex = ~newIndex;
            }

            for (int index = keys.Length - 1; index > newIndex; index--)
            {
                keys[index] = keys[index - 1];
            }

            keys[newIndex] = newKey;

            return keys;
        }

        private string CreateCrateConfigurationKey(List<Point> cratePositions)
        {
            List<(int Distance, int Position)> crateKeys = new List<(int Distance, int Position)>(
                cratePositions.Count
            );

            for (int i = 0; i < cratePositions.Count; i++)
            {
                Point position = cratePositions[i];

                int cellIndex = position.Y * columns + position.X;

                int distance = level.Crates[i].Distance;

                crateKeys.Add((distance, cellIndex));
            }

            crateKeys.Sort(
                (left, right) =>
                {
                    int distanceComparison = left.Distance.CompareTo(right.Distance);

                    if (distanceComparison != 0)
                    {
                        return distanceComparison;
                    }

                    return left.Position.CompareTo(right.Position);
                }
            );

            StringBuilder key = new StringBuilder(crateKeys.Count * 8);

            for (int i = 0; i < crateKeys.Count; i++)
            {
                if (i > 0)
                {
                    key.Append(',');
                }

                key.Append(crateKeys[i].Distance);

                key.Append(':');

                key.Append(crateKeys[i].Position);
            }

            return key.ToString();
        }

        public List<(int CrateIndex, Point Direction)> GetLegalPushes(
            Point playerPosition,
            List<Point> cratePositions
        )
        {
            List<(int CrateIndex, Point Direction)> legalPushes = new();

            BuildCrateOccupancy(cratePositions);

            int currentReachableVisitId = MarkReachableCells(playerPosition);

            Point[] directions =
            {
                new Point(0, -1),
                new Point(0, 1),
                new Point(-1, 0),
                new Point(1, 0),
            };

            for (int crateIndex = 0; crateIndex < cratePositions.Count; crateIndex++)
            {
                Point cratePosition = cratePositions[crateIndex];

                int distance = level.Crates[crateIndex].Distance;

                foreach (Point direction in directions)
                {
                    Point playerRequiredPosition = new Point(
                        cratePosition.X - direction.X,
                        cratePosition.Y - direction.Y
                    );

                    if (!IsReachable(playerRequiredPosition, currentReachableVisitId))
                    {
                        continue;
                    }

                    Point finalPosition = cratePosition;

                    bool blocked = false;

                    for (int step = 1; step <= distance; step++)
                    {
                        Point testPosition = new Point(
                            cratePosition.X + direction.X * step,
                            cratePosition.Y + direction.Y * step
                        );

                        if (IsWall(testPosition) || IsOccupiedByAnyCrate(testPosition))
                        {
                            blocked = true;
                            break;
                        }

                        finalPosition = testPosition;
                    }

                    if (blocked)
                    {
                        continue;
                    }

                    if (IsStaticCornerDeadlock(finalPosition))
                    {
                        continue;
                    }

                    legalPushes.Add((crateIndex, direction));
                }
            }

            return legalPushes;
        }

        /// <summary>
        /// Maintains compatibility with the original solver API.
        /// </summary>
        public int FindMinimumPushes()
        {
            NumberPushSolution solution = FindSolution();

            return solution.MinimumPushes;
        }

        private int CountCratesOnGoals(IReadOnlyList<Point> cratePositions)
        {
            int count = 0;

            foreach (Point cratePosition in cratePositions)
            {
                if (level.Goals.Contains(cratePosition))
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
            HashSet<SolverState> visited
        )
        {
            Point cratePosition = state.CratePositions[crateIndex];

            Point playerRequiredPosition = new Point(
                cratePosition.X - direction.X,
                cratePosition.Y - direction.Y
            );

            long timingStart = Stopwatch.GetTimestamp();

            if (!IsReachable(playerRequiredPosition, currentReachableVisitId))
            {
                PushValidationMilliseconds += Stopwatch.GetElapsedTime(timingStart).Ticks;

                return;
            }

            Point finalPosition = cratePosition;

            for (int step = 1; step <= distance; step++)
            {
                int testX = cratePosition.X + direction.X * step;

                int testY = cratePosition.Y + direction.Y * step;

                if (testX < 0 || testX >= columns || testY < 0 || testY >= rows)
                {
                    PushValidationMilliseconds += Stopwatch.GetElapsedTime(timingStart).Ticks;

                    return;
                }

                int testIndex = testY * columns + testX;

                if (wallPositions.Contains(new Point(testX, testY)))
                {
                    PushValidationMilliseconds += Stopwatch.GetElapsedTime(timingStart).Ticks;

                    return;
                }

                if (crateOccupancyVisit[testIndex] == crateOccupancyVisitId)
                {
                    PushValidationMilliseconds += Stopwatch.GetElapsedTime(timingStart).Ticks;

                    return;
                }

                finalPosition = new Point(testX, testY);
            }

            PushValidationMilliseconds += Stopwatch.GetElapsedTime(timingStart).Ticks;

            timingStart = Stopwatch.GetTimestamp();

            Point[] newCratePositions = new Point[state.CratePositions.Length];

            Array.Copy(state.CratePositions, newCratePositions, state.CratePositions.Length);

            newCratePositions[crateIndex] = finalPosition;

            ArrayCopyMilliseconds += Stopwatch.GetElapsedTime(timingStart).Ticks;

            timingStart = Stopwatch.GetTimestamp();

            if (IsStaticCornerDeadlock(finalPosition))
            {
                StaticDeadlockMilliseconds += Stopwatch.GetElapsedTime(timingStart).Ticks;

                return;
            }

            StaticDeadlockMilliseconds += Stopwatch.GetElapsedTime(timingStart).Ticks;

            legalPushesForState++;

            Point newPlayerPosition = cratePosition;

            timingStart = Stopwatch.GetTimestamp();

            BuildCrateOccupancy(newCratePositions);

            OccupancyMilliseconds += Stopwatch.GetElapsedTime(timingStart).Ticks;

            timingStart = Stopwatch.GetTimestamp();

            int newReachableVisitId = MarkSuccessorReachableCells(newPlayerPosition);

            SuccessorReachabilityMilliseconds += Stopwatch.GetElapsedTime(timingStart).Ticks;

            int newPlayerRegion = GetSuccessorPlayerRegionKey(newReachableVisitId);

            if (
                state.Parent != null
                && state.Step != null
                && state.Step.CrateIndex == crateIndex
                && state.Step.Direction.X == -direction.X
                && state.Step.Direction.Y == -direction.Y
                && cratePosition == state.Step.CrateEnd
                && finalPosition == state.Step.CrateStart
                && newPlayerRegion == state.Parent.PlayerRegion
            )
            {
                ExactParentReversalStates++;

                DuplicateStates++;

                return;
            }

            timingStart = Stopwatch.GetTimestamp();

            BuildCrateOccupancy(state.CratePositions);

            OccupancyMilliseconds += Stopwatch.GetElapsedTime(timingStart).Ticks;

            int oldPositionHash = HashCode.Combine(cratePosition.X, cratePosition.Y);

            int newPositionHash = HashCode.Combine(finalPosition.X, finalPosition.Y);

            int newPositionHashSum = state.PositionHashSum - oldPositionHash + newPositionHash;

            int newPositionHashSquareSum =
                state.PositionHashSquareSum
                - oldPositionHash * oldPositionHash
                + newPositionHash * newPositionHash;

            StateConstructionMilliseconds += Stopwatch.GetElapsedTime(timingStart).Ticks;

            timingStart = Stopwatch.GetTimestamp();

            long[] newCanonicalCrateKeys = CreateSuccessorCanonicalCrateKeys(
                state,
                crateIndex,
                cratePosition,
                finalPosition,
                distance
            );

            SolverState newState = new SolverState(
                newPlayerPosition,
                newCratePositions,
                state.Pushes + 1,
                state,
                null,
                newPositionHashSum,
                newPositionHashSquareSum,
                newCanonicalCrateKeys
            )
            {
                PlayerRegion = newPlayerRegion,
            };

            StateConstructionMilliseconds += Stopwatch.GetElapsedTime(timingStart).Ticks;

            timingStart = Stopwatch.GetTimestamp();

            if (!visited.Add(newState))
            {
                HashSetLookupMilliseconds += Stopwatch.GetElapsedTime(timingStart).Ticks;

                HashSetDuplicateStates++;

                DuplicateStates++;

                return;
            }

            HashSetLookupMilliseconds += Stopwatch.GetElapsedTime(timingStart).Ticks;

            UniqueSuccessorStates++;

            // Only the pushed crate changes position, so we can determine
            // goal progress by comparing its old and new positions.
            bool crateStartedOnGoal = goalPositions.Contains(cratePosition);

            bool crateEndedOnGoal = goalPositions.Contains(finalPosition);

            if (crateEndedOnGoal && !crateStartedOnGoal)
            {
                GoalProgressIncreases++;
            }
            else if (!crateEndedOnGoal && crateStartedOnGoal)
            {
                GoalProgressDecreases++;
            }
            else
            {
                GoalProgressUnchanged++;
            }

            NumberPushSolutionStep stepData = new NumberPushSolutionStep
            {
                CrateIndex = crateIndex,

                PlayerStart = state.PlayerPosition,

                PlayerPushPosition = playerRequiredPosition,

                CrateStart = cratePosition,

                CrateEnd = finalPosition,

                Direction = direction,

                Distance = distance,

                PlayerPath = new List<Point>(),
            };

            newState.Step = stepData;

            queue.Enqueue(newState);
        }

        private void AnalyzeFirstZeroPushState(SolverState state)
        {
            Point[] directions =
            {
                new Point(0, -1),
                new Point(0, 1),
                new Point(-1, 0),
                new Point(1, 0),
            };

            BuildCrateOccupancy(state.CratePositions);

            int currentReachableVisitId = MarkReachableCells(state.PlayerPosition);

            for (int crateIndex = 0; crateIndex < state.CratePositions.Length; crateIndex++)
            {
                Point cratePosition = state.CratePositions[crateIndex];

                bool playerAccessBlocked = false;

                bool wallBlocked = false;

                bool crateBlocked = false;

                bool cornerDeadlocked = false;

                foreach (Point direction in directions)
                {
                    Point playerRequiredPosition = new Point(
                        cratePosition.X - direction.X,
                        cratePosition.Y - direction.Y
                    );

                    if (!IsReachable(playerRequiredPosition, currentReachableVisitId))
                    {
                        playerAccessBlocked = true;

                        if (direction == new Point(0, -1))
                        {
                            FirstZeroPushUpBlocked++;
                        }
                        else if (direction == new Point(0, 1))
                        {
                            FirstZeroPushDownBlocked++;
                        }
                        else if (direction == new Point(-1, 0))
                        {
                            FirstZeroPushLeftBlocked++;
                        }
                        else if (direction == new Point(1, 0))
                        {
                            FirstZeroPushRightBlocked++;
                        }

                        continue;
                    }

                    Point finalPosition = cratePosition;

                    bool pathBlocked = false;

                    for (int step = 1; step <= level.Crates[crateIndex].Distance; step++)
                    {
                        int testX = cratePosition.X + direction.X * step;

                        int testY = cratePosition.Y + direction.Y * step;

                        Point testPosition = new Point(testX, testY);

                        if (IsWall(testPosition))
                        {
                            wallBlocked = true;
                            pathBlocked = true;
                            break;
                        }

                        if (IsOccupiedByAnyCrate(testPosition))
                        {
                            crateBlocked = true;
                            pathBlocked = true;
                            break;
                        }

                        finalPosition = testPosition;
                    }

                    if (pathBlocked)
                    {
                        continue;
                    }

                    if (IsStaticCornerDeadlock(finalPosition))
                    {
                        cornerDeadlocked = true;
                    }
                }

                if (playerAccessBlocked)
                {
                    FirstZeroPushPlayerAccessBlockedCrates++;
                }

                if (wallBlocked)
                {
                    FirstZeroPushWallBlockedCrates++;
                }

                if (crateBlocked)
                {
                    FirstZeroPushCrateBlockedCrates++;
                }

                if (cornerDeadlocked)
                {
                    FirstZeroPushCornerDeadlockedCrates++;
                }
            }

            AnalyzeLastPushedCrate(state);
        }

        private void AnalyzeLastPushedCrate(SolverState state)
        {
            if (state.Step == null)
            {
                return;
            }

            int crateIndex = state.Step.CrateIndex;

            if (crateIndex < 0 || crateIndex >= state.CratePositions.Length)
            {
                return;
            }

            Point cratePosition = state.CratePositions[crateIndex];

            Point[] directions =
            {
                new Point(0, -1),
                new Point(0, 1),
                new Point(-1, 0),
                new Point(1, 0),
            };

            foreach (Point direction in directions)
            {
                Point playerRequiredPosition = new Point(
                    cratePosition.X - direction.X,
                    cratePosition.Y - direction.Y
                );

                int currentReachableVisitId = MarkReachableCells(state.PlayerPosition);

                if (!IsReachable(playerRequiredPosition, currentReachableVisitId))
                {
                    FirstZeroPushLastCratePlayerAccessBlocked++;
                    continue;
                }

                Point finalPosition = cratePosition;

                bool wallBlocked = false;

                bool crateBlocked = false;

                for (int step = 1; step <= level.Crates[crateIndex].Distance; step++)
                {
                    int testX = cratePosition.X + direction.X * step;

                    int testY = cratePosition.Y + direction.Y * step;

                    Point testPosition = new Point(testX, testY);

                    if (IsWall(testPosition))
                    {
                        wallBlocked = true;
                        break;
                    }

                    if (IsOccupiedByAnyCrate(testPosition))
                    {
                        crateBlocked = true;
                        break;
                    }

                    finalPosition = testPosition;
                }

                if (wallBlocked)
                {
                    FirstZeroPushLastCrateWallBlocked++;
                    continue;
                }

                if (crateBlocked)
                {
                    FirstZeroPushLastCrateCrateBlocked++;
                    continue;
                }

                if (IsStaticCornerDeadlock(finalPosition))
                {
                    FirstZeroPushLastCrateCornerDeadlocked++;
                    continue;
                }

                FirstZeroPushLastCrateLegalPushes++;
            }
        }

        /// <summary>
        /// Returns a stable identifier for the player's current reachable
        /// region.
        ///
        /// Every cell in one connected reachable region has the same
        /// identifier: the lowest cell index in that region.
        ///
        /// This lets the solver treat different player positions in the
        /// same walkable region as the same state.
        /// </summary>
        private int GetPlayerRegionKey(int currentReachableVisitId)
        {
            int regionKey = int.MaxValue;

            for (int index = 0; index < reachableVisit.Length; index++)
            {
                if (reachableVisit[index] == currentReachableVisitId)
                {
                    regionKey = index;

                    break;
                }
            }

            return regionKey;
        }

        private int GetSuccessorPlayerRegionKey(int currentReachableVisitId)
        {
            int regionKey = int.MaxValue;

            for (int index = 0; index < successorReachableVisit.Length; index++)
            {
                if (successorReachableVisit[index] == currentReachableVisitId)
                {
                    regionKey = index;

                    break;
                }
            }

            return regionKey;
        }

        private bool IsStaticCornerDeadlock(Point position)
        {
            if (goalPositions.Contains(position))
            {
                return false;
            }

            bool wallUp = IsWall(new Point(position.X, position.Y - 1));

            bool wallDown = IsWall(new Point(position.X, position.Y + 1));

            bool wallLeft = IsWall(new Point(position.X - 1, position.Y));

            bool wallRight = IsWall(new Point(position.X + 1, position.Y));

            return (wallUp && wallLeft)
                || (wallUp && wallRight)
                || (wallDown && wallLeft)
                || (wallDown && wallRight);
        }

        private NumberPushSolution BuildSolution(SolverState finalState)
        {
            List<NumberPushSolutionStep> steps = new();

            SolverState? current = finalState;

            while (current != null && current.Parent != null)
            {
                if (current.Step != null)
                {
                    NumberPushSolutionStep step = current.Step;

                    step.PlayerPath = FindPlayerPath(
                        step.PlayerStart,
                        step.PlayerPushPosition,
                        current.Parent.CratePositions
                    );

                    steps.Add(step);
                }

                current = current.Parent;
            }

            steps.Reverse();

            for (int i = 0; i < steps.Count; i++)
            {
                steps[i].PushNumber = i + 1;
            }

            NumberPushSolution solution = new NumberPushSolution
            {
                IsSolved = true,
                MinimumPushes = finalState.Pushes,
                Steps = steps,
            };

            foreach (NumberPushSolutionStep step in steps)
            {
                if (solution.CratePushCounts.ContainsKey(step.CrateIndex))
                {
                    solution.CratePushCounts[step.CrateIndex]++;
                }
                else
                {
                    solution.CratePushCounts[step.CrateIndex] = 1;
                }

                if (!solution.CrateFirstPushNumbers.ContainsKey(step.CrateIndex))
                {
                    solution.CrateFirstPushNumbers[step.CrateIndex] = step.PushNumber;
                }

                solution.CrateLastPushNumbers[step.CrateIndex] = step.PushNumber;

                solution.CrateGoalPositions[step.CrateIndex] = step.CrateEnd;
            }

            return solution;
        }

        private List<Point> FindPlayerPath(
            Point startPosition,
            Point targetPosition,
            IReadOnlyList<Point> cratePositions
        )
        {
            if (startPosition == targetPosition)
            {
                return new List<Point> { startPosition };
            }

            Queue<Point> queue = new();

            HashSet<Point> visited = new();

            Dictionary<Point, Point> parents = new();

            queue.Enqueue(startPosition);

            visited.Add(startPosition);

            Point[] directions =
            {
                new Point(0, -1),
                new Point(0, 1),
                new Point(-1, 0),
                new Point(1, 0),
            };

            while (queue.Count > 0)
            {
                Point current = queue.Dequeue();

                foreach (Point direction in directions)
                {
                    Point next = new Point(current.X + direction.X, current.Y + direction.Y);

                    if (IsWall(next))
                    {
                        continue;
                    }

                    if (IsOccupiedByAnyCrate(next, cratePositions))
                    {
                        continue;
                    }

                    if (!visited.Add(next))
                    {
                        continue;
                    }

                    parents[next] = current;

                    if (next == targetPosition)
                    {
                        return ReconstructPlayerPath(startPosition, targetPosition, parents);
                    }

                    queue.Enqueue(next);
                }
            }

            return new List<Point>();
        }

        private List<Point> ReconstructPlayerPath(
            Point startPosition,
            Point targetPosition,
            Dictionary<Point, Point> parents
        )
        {
            List<Point> path = new();

            Point current = targetPosition;

            path.Add(current);

            while (current != startPosition)
            {
                if (!parents.TryGetValue(current, out Point parent))
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
        private void BuildCrateOccupancy(IReadOnlyList<Point> cratePositions)
        {
            crateOccupancyVisitId++;

            if (crateOccupancyVisitId == int.MaxValue)
            {
                Array.Clear(crateOccupancyVisit, 0, crateOccupancyVisit.Length);

                crateOccupancyVisitId = 1;
            }

            foreach (Point cratePosition in cratePositions)
            {
                if (
                    cratePosition.X < 0
                    || cratePosition.X >= columns
                    || cratePosition.Y < 0
                    || cratePosition.Y >= rows
                )
                {
                    continue;
                }

                int index = GetCellIndex(cratePosition);

                crateOccupancyVisit[index] = crateOccupancyVisitId;
            }
        }

        /// <summary>
        /// Marks every cell the player can currently reach without
        /// moving any crates.
        ///
        /// The visitation array and queue are reused between calls.
        /// </summary>
        private int MarkReachableCells(Point startPosition)
        {
            reachableVisitId++;

            if (reachableVisitId == int.MaxValue)
            {
                Array.Clear(reachableVisit, 0, reachableVisit.Length);

                reachableVisitId = 1;
            }

            if (IsWall(startPosition) || IsOccupiedByAnyCrate(startPosition))
            {
                return reachableVisitId;
            }

            int startIndex = GetCellIndex(startPosition);

            int head = 0;

            int tail = 0;

            reachableQueue[tail++] = startIndex;

            reachableVisit[startIndex] = reachableVisitId;

            while (head < tail)
            {
                int currentIndex = reachableQueue[head++];

                int currentX = currentIndex % columns;

                if (currentX > 0)
                {
                    int index = currentIndex - 1;

                    if (
                        reachableVisit[index] != reachableVisitId
                        && !blockedCells[index]
                        && crateOccupancyVisit[index] != crateOccupancyVisitId
                    )
                    {
                        reachableVisit[index] = reachableVisitId;

                        reachableQueue[tail++] = index;
                    }
                }

                if (currentX < columns - 1)
                {
                    int index = currentIndex + 1;

                    if (
                        reachableVisit[index] != reachableVisitId
                        && !blockedCells[index]
                        && crateOccupancyVisit[index] != crateOccupancyVisitId
                    )
                    {
                        reachableVisit[index] = reachableVisitId;

                        reachableQueue[tail++] = index;
                    }
                }

                if (currentIndex >= columns)
                {
                    int index = currentIndex - columns;

                    if (
                        reachableVisit[index] != reachableVisitId
                        && !blockedCells[index]
                        && crateOccupancyVisit[index] != crateOccupancyVisitId
                    )
                    {
                        reachableVisit[index] = reachableVisitId;

                        reachableQueue[tail++] = index;
                    }
                }

                if (currentIndex < reachableVisit.Length - columns)
                {
                    int index = currentIndex + columns;

                    if (
                        reachableVisit[index] != reachableVisitId
                        && !blockedCells[index]
                        && crateOccupancyVisit[index] != crateOccupancyVisitId
                    )
                    {
                        reachableVisit[index] = reachableVisitId;

                        reachableQueue[tail++] = index;
                    }
                }
            }

            return reachableVisitId;
        }

        private int MarkSuccessorReachableCells(Point startPosition)
        {
            successorReachableVisitId++;

            if (successorReachableVisitId == int.MaxValue)
            {
                Array.Clear(successorReachableVisit, 0, successorReachableVisit.Length);

                successorReachableVisitId = 1;
            }

            if (IsWall(startPosition) || IsOccupiedByAnyCrate(startPosition))
            {
                return successorReachableVisitId;
            }

            int startIndex = GetCellIndex(startPosition);

            int head = 0;

            int tail = 0;

            successorReachableQueue[tail++] = startIndex;

            successorReachableVisit[startIndex] = successorReachableVisitId;

            while (head < tail)
            {
                int currentIndex = successorReachableQueue[head++];

                int currentX = currentIndex % columns;

                if (currentX > 0)
                {
                    int index = currentIndex - 1;

                    if (
                        successorReachableVisit[index] != successorReachableVisitId
                        && !blockedCells[index]
                        && crateOccupancyVisit[index] != crateOccupancyVisitId
                    )
                    {
                        successorReachableVisit[index] = successorReachableVisitId;

                        successorReachableQueue[tail++] = index;
                    }
                }

                if (currentX < columns - 1)
                {
                    int index = currentIndex + 1;

                    if (
                        successorReachableVisit[index] != successorReachableVisitId
                        && !blockedCells[index]
                        && crateOccupancyVisit[index] != crateOccupancyVisitId
                    )
                    {
                        successorReachableVisit[index] = successorReachableVisitId;

                        successorReachableQueue[tail++] = index;
                    }
                }

                if (currentIndex >= columns)
                {
                    int index = currentIndex - columns;

                    if (
                        successorReachableVisit[index] != successorReachableVisitId
                        && !blockedCells[index]
                        && crateOccupancyVisit[index] != crateOccupancyVisitId
                    )
                    {
                        successorReachableVisit[index] = successorReachableVisitId;

                        successorReachableQueue[tail++] = index;
                    }
                }

                if (currentIndex < successorReachableVisit.Length - columns)
                {
                    int index = currentIndex + columns;

                    if (
                        successorReachableVisit[index] != successorReachableVisitId
                        && !blockedCells[index]
                        && crateOccupancyVisit[index] != crateOccupancyVisitId
                    )
                    {
                        successorReachableVisit[index] = successorReachableVisitId;

                        successorReachableQueue[tail++] = index;
                    }
                }
            }

            return successorReachableVisitId;
        }

        private void MarkReachableNeighbor(int x, int y, ref int tail)
        {
            if (x < 0 || x >= columns || y < 0 || y >= rows)
            {
                return;
            }

            int index = y * columns + x;

            if (reachableVisit[index] == reachableVisitId)
            {
                return;
            }

            Point position = new Point(x, y);

            if (IsWall(position))
            {
                return;
            }

            if (crateOccupancyVisit[index] == crateOccupancyVisitId)
            {
                return;
            }

            reachableVisit[index] = reachableVisitId;

            reachableQueue[tail++] = index;
        }

        private void MarkSuccessorReachableNeighbor(int x, int y, ref int tail)
        {
            if (x < 0 || x >= columns || y < 0 || y >= rows)
            {
                return;
            }

            int index = y * columns + x;

            if (successorReachableVisit[index] == successorReachableVisitId)
            {
                return;
            }

            if (blockedCells[index])
            {
                return;
            }

            if (crateOccupancyVisit[index] == crateOccupancyVisitId)
            {
                return;
            }

            successorReachableVisit[index] = successorReachableVisitId;

            successorReachableQueue[tail++] = index;
        }

        private bool IsReachable(Point position, int currentReachableVisitId)
        {
            if (position.X < 0 || position.X >= columns || position.Y < 0 || position.Y >= rows)
            {
                return false;
            }

            int index = GetCellIndex(position);

            return reachableVisit[index] == currentReachableVisitId;
        }

        private int GetCellIndex(Point position)
        {
            return position.Y * columns + position.X;
        }

        private bool IsComplete(IReadOnlyList<Point> cratePositions)
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
            if (position.X < 0 || position.X >= columns || position.Y < 0 || position.Y >= rows)
            {
                return true;
            }

            return wallPositions.Contains(position);
        }

        /// <summary>
        /// Checks the reusable occupancy map.
        /// Used during player reachability calculations.
        /// </summary>
        private bool IsOccupiedByAnyCrate(Point position)
        {
            if (position.X < 0 || position.X >= columns || position.Y < 0 || position.Y >= rows)
            {
                return false;
            }

            int index = GetCellIndex(position);

            return crateOccupancyVisit[index] == crateOccupancyVisitId;
        }

        /// <summary>
        /// Checks the supplied crate configuration directly.
        /// Used when reconstructing player paths for a solution.
        /// </summary>
        private bool IsOccupiedByAnyCrate(Point position, IReadOnlyList<Point> cratePositions)
        {
            return cratePositions.Contains(position);
        }

        private bool IsOccupiedByAnotherCrate(
            Point position,
            Point[] cratePositions,
            int movingCrateIndex
        )
        {
            for (int i = 0; i < cratePositions.Length; i++)
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

        private static long ToMilliseconds(long timeSpanTicks)
        {
            return timeSpanTicks / TimeSpan.TicksPerMillisecond;
        }

        private class SolverState
        {
            public Point PlayerPosition { get; }

            public Point[] CratePositions { get; }

            public long[] CanonicalCrateKeys { get; }

            public int Pushes { get; }

            public SolverState? Parent { get; }

            public int PlayerRegion { get; set; }

            public NumberPushSolutionStep? Step { get; set; }

            public int PositionHashSum { get; }

            public int PositionHashSquareSum { get; }

            public SolverState(
                Point playerPosition,
                Point[] cratePositions,
                int pushes,
                SolverState? parent,
                NumberPushSolutionStep? step,
                int positionHashSum,
                int positionHashSquareSum,
                long[] canonicalCrateKeys
            )
            {
                PlayerPosition = playerPosition;

                CratePositions = cratePositions;

                Pushes = pushes;

                Parent = parent;

                Step = step;

                PositionHashSum = positionHashSum;

                PositionHashSquareSum = positionHashSquareSum;

                CanonicalCrateKeys = canonicalCrateKeys;
            }
        }

        public long OccupancyMillisecondsValue => ToMilliseconds(OccupancyMilliseconds);

        public long CurrentReachabilityMillisecondsValue =>
            ToMilliseconds(CurrentReachabilityMilliseconds);

        public long SuccessorReachabilityMillisecondsValue =>
            ToMilliseconds(SuccessorReachabilityMilliseconds);

        public long PushValidationMillisecondsValue => ToMilliseconds(PushValidationMilliseconds);

        public long ArrayCopyMillisecondsValue => ToMilliseconds(ArrayCopyMilliseconds);

        public long StateConstructionMillisecondsValue =>
            ToMilliseconds(StateConstructionMilliseconds);

        public long HashSetLookupMillisecondsValue => ToMilliseconds(HashSetLookupMilliseconds);

        public long StaticDeadlockMillisecondsValue => ToMilliseconds(StaticDeadlockMilliseconds);

        private sealed class SolverStateComparer : IEqualityComparer<SolverState>
        {
            public bool Equals(SolverState? left, SolverState? right)
            {
                if (ReferenceEquals(left, right))
                {
                    return true;
                }

                if (left == null || right == null)
                {
                    return false;
                }

                if (left.PlayerRegion != right.PlayerRegion)
                {
                    return false;
                }

                if (left.CanonicalCrateKeys.Length != right.CanonicalCrateKeys.Length)
                {
                    return false;
                }

                for (int index = 0; index < left.CanonicalCrateKeys.Length; index++)
                {
                    if (left.CanonicalCrateKeys[index] != right.CanonicalCrateKeys[index])
                    {
                        return false;
                    }
                }

                return true;
            }

            public int GetHashCode(SolverState state)
            {
                return HashCode.Combine(
                    state.PlayerRegion,
                    state.PositionHashSum,
                    state.PositionHashSquareSum
                );
            }
        }
    }
}
