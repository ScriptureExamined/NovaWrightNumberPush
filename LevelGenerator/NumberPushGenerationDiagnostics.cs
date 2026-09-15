namespace NovaWright.NumberPush.LevelGenerator
{
    public class NumberPushGenerationDiagnostics
    {
        public int LevelNumber { get; set; }

        public int TargetMinimumPushes { get; set; }

        public int TargetMaximumPushes { get; set; }

        public int FinalRows { get; set; }

        public int FinalColumns { get; set; }

        public int TotalAttempts { get; set; }

        public int WallGenerationFailures { get; set; }

        public int TotalSolverCalls { get; set; }

        public int PreSolverRejectedCandidates { get; set; }

        public int PreSolverNoReachableGoals { get; set; }

        public int PreSolverFailedCrateDistance1 { get; set; }

        public int PreSolverFailedCrateDistance2 { get; set; }

        public int PreSolverFailedCrateDistance3 { get; set; }

        public int PreSolverFailedCrateDistance4Plus { get; set; }

        public int PreSolverNoDistinctGoalMatching { get; set; }

        public int UnsolvableCandidates { get; set; }

        public long UnsolvableStatesExplored { get; set; }

        public long UnsolvableZeroLegalPushStates { get; set; }

        public long UnsolvableMaximumLegalPushes { get; set; }

        public long UnsolvableDuplicateStates { get; set; }

        public int BelowTargetCandidates { get; set; }

        public int AboveTargetCandidates { get; set; }

        public int AcceptedCandidates { get; set; }

        public int MinimumPushes { get; set; }

        public int CratesMoved { get; set; }

        public int CratePushOrderChanges { get; set; }

        public int MaximumConsecutivePushes { get; set; }

        public Dictionary<int, int> CratePushCounts { get; set; } =
            new();

        public Dictionary<int, int> CrateFirstPushNumbers { get; set; } =
            new();

        public Dictionary<int, int> CrateLastPushNumbers { get; set; } =
            new();

        public Dictionary<int, Point> CrateGoalPositions { get; set; } =
    new();

        public Dictionary<int, Point> CrateStartPositions { get; set; } =
    new();

        public Dictionary<int, int> CrateDistances { get; set; } =
            new();

        public Dictionary<int, HashSet<Point>> CrateReachableGoals { get; set; } =
    new();

        public Dictionary<int, HashSet<Point>> CrateReachablePositions { get; set; } =
    new();

        public Dictionary<int, Dictionary<Point, int>> CrateGoalPushDistances { get; set; } =
    new();

        public int CrateInteractionPairs { get; set; }

        public int CrateInteractionBlocks { get; set; }

        public long CandidateGenerationMilliseconds { get; set; }

        public long SolverMilliseconds { get; set; }

        public long TotalMilliseconds { get; set; }

        public double AverageSolverMilliseconds
        {
            get
            {
                if (TotalSolverCalls == 0)
                {
                    return 0;
                }

                return
                    (double)SolverMilliseconds /
                    TotalSolverCalls;
            }
        }

        public string GetReportText()
        {
            TimeSpan totalTime =
                TimeSpan.FromMilliseconds(
                    TotalMilliseconds);

            string report =
                $"LEVEL {LevelNumber} DIAGNOSTICS\r\n" +
                $"Target pushes: {TargetMinimumPushes}-{TargetMaximumPushes}\r\n" +
                $"Final board: {FinalRows}x{FinalColumns}\r\n" +
                "\r\n" +
                "CANDIDATES\r\n" +
                $"Total attempts: {TotalAttempts}\r\n" +
                $"Wall generation failures: {WallGenerationFailures}\r\n" +
                $"Solver calls: {TotalSolverCalls}\r\n" +
                $"Pre-solver rejected: {PreSolverRejectedCandidates}\r\n" +
                $"Pre-solver no reachable goals: {PreSolverNoReachableGoals}\r\n" +
                $"Pre-solver failed crate distance 1: {PreSolverFailedCrateDistance1}\r\n" +
                $"Pre-solver failed crate distance 2: {PreSolverFailedCrateDistance2}\r\n" +
                $"Pre-solver failed crate distance 3: {PreSolverFailedCrateDistance3}\r\n" +
                $"Pre-solver failed crate distance 4+: {PreSolverFailedCrateDistance4Plus}\r\n" +
                $"Pre-solver no distinct goal matching: {PreSolverNoDistinctGoalMatching}\r\n" +
                $"Unsolvable: {UnsolvableCandidates}\r\n" +
                $"  States explored: {UnsolvableStatesExplored}\r\n" +
                $"  Zero-legal-push states: {UnsolvableZeroLegalPushStates}\r\n" +
                $"  Maximum legal pushes: {UnsolvableMaximumLegalPushes}\r\n" +
                $"  Duplicate states: {UnsolvableDuplicateStates}\r\n" +
                $"Below target: {BelowTargetCandidates}\r\n" +
                $"Above target: {AboveTargetCandidates}\r\n" +
                $"Accepted: {AcceptedCandidates}\r\n" +
                "\r\n" +
                "SOLUTION\r\n" +
                $"Minimum pushes: {MinimumPushes}\r\n" +
                $"Crates moved: {CratesMoved}\r\n";

            report +=
                $"Crate push order changes: {CratePushOrderChanges}\r\n" +
                $"Maximum consecutive pushes: {MaximumConsecutivePushes}\r\n";

            foreach (int crateIndex in CratePushCounts.Keys.OrderBy(
                         index => index))
            {
                int crateNumber =
                    crateIndex + 1;

                report +=
                    "\r\n" +
                    $"Crate {crateNumber}:\r\n" +
                    $"  Pushes: {CratePushCounts[crateIndex]}\r\n" +
                    $"  First push: {CrateFirstPushNumbers[crateIndex]}\r\n" +
                    $"  Last push: {CrateLastPushNumbers[crateIndex]}\r\n";
            }

            report +=
    "\r\n" +
    "CRATE GEOMETRY\r\n";

            foreach (int crateIndex in
                     CrateStartPositions.Keys.OrderBy(
                         index => index))
            {
                int crateNumber =
                    crateIndex + 1;

                Point startPosition =
                    CrateStartPositions[crateIndex];

                int distance =
                    CrateDistances[crateIndex];

                HashSet<Point> reachableGoals =
    CrateReachableGoals[crateIndex];

                Dictionary<Point, int> goalPushDistances =
    CrateGoalPushDistances[crateIndex];

                HashSet<Point> reachablePositions =
    CrateReachablePositions[crateIndex];

                report +=
    $"Crate {crateNumber}: " +
    $"start ({startPosition.X},{startPosition.Y}), " +
    $"distance {distance}, " +
    $"reachable goals {reachableGoals.Count}\r\n";

                report +=
    $"  Reachable positions: " +
    $"{reachablePositions.Count}\r\n";

                foreach (Point position in
                         reachablePositions.OrderBy(
                             point => point.Y)
                             .ThenBy(
                                 point => point.X))
                {
                    report +=
                        $"    ({position.X},{position.Y})\r\n";
                }

                foreach (Point goal in
         reachableGoals.OrderBy(
             point => point.Y)
             .ThenBy(
                 point => point.X))
                {
                    report +=
                        $"  Goal: ({goal.X},{goal.Y}), " +
                        $"minimum pushes {goalPushDistances[goal]}\r\n";
                }
            }

            report +=
    "\r\n" +
    "GOALS\r\n";

            foreach (int crateIndex in
                     CrateGoalPositions.Keys.OrderBy(
                         index => index))
            {
                int crateNumber =
                    crateIndex + 1;

                Point goalPosition =
                    CrateGoalPositions[crateIndex];

                report +=
                    $"Crate {crateNumber} goal: " +
                    $"({goalPosition.X},{goalPosition.Y})\r\n";
            }

            report +=
                "\r\n" +
                "TIMING\r\n" +
                $"Candidate generation: {CandidateGenerationMilliseconds} ms\r\n" +
                $"Solver: {SolverMilliseconds} ms\r\n" +
                $"Average solver call: {AverageSolverMilliseconds:F2} ms\r\n" +
                $"Total time: {totalTime:hh\\:mm\\:ss}";

            report +=
    $"Crate interaction pairs: {CrateInteractionPairs}\r\n" +
    $"Crate interaction blocks: {CrateInteractionBlocks}\r\n";

            return report;
        }
    }
}