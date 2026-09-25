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

        public long UnsolvableExactParentReversalStates { get; set; }

        public long UnsolvableHashSetDuplicateStates { get; set; }

        public long UniqueSuccessorStates { get; set; }

        public long OccupancyMilliseconds { get; set; }

        public long CurrentReachabilityMilliseconds { get; set; }

        public long SuccessorReachabilityMilliseconds { get; set; }

        public long PushValidationMilliseconds { get; set; }

        public long ArrayCopyMilliseconds { get; set; }

        public long StateConstructionMilliseconds { get; set; }

        public long HashSetLookupMilliseconds { get; set; }

        public long StaticDeadlockMilliseconds { get; set; }

        public int BelowTargetCandidates { get; set; }

        public int AboveTargetCandidates { get; set; }

        public int AcceptedCandidates { get; set; }

        public int InitialTryPushLegalPushes { get; set; }

        public Dictionary<int, int> SolvableCandidatePushCounts { get; set; } =
            new();

        public Dictionary<int, int> UnsolvableCandidateMaximumPushCounts { get; set; } =
            new();

        public Dictionary<int, int> InitialCrateMobilityDistribution { get; set; } =
            new();

        public Dictionary<int, int> InitialPlayerPushMobilityDistribution { get; set; } =
            new();

        public Dictionary<int, Dictionary<int, int>> InitialPlayerMobilityMaximumPushDistribution { get; set; } =
            new();

        public Dictionary<int, Dictionary<int, int>> InitialToAfterFirstPushMobilityDistribution { get; set; } =
            new();

        public Dictionary<int, int> UnsolvableCandidateMinimumZeroPushDepths { get; set; } =
            new();

        public Dictionary<int, int> UnsolvableCandidateLegalPushesBeforeZero { get; set; } =
            new();

        public int FirstZeroPushPlayerAccessBlockedCrates { get; set; }

        public int FirstZeroPushWallBlockedCrates { get; set; }

        public int FirstZeroPushCrateBlockedCrates { get; set; }

        public int FirstZeroPushCornerDeadlockedCrates { get; set; }

        public int FirstZeroPushUpBlocked { get; set; }

        public int FirstZeroPushDownBlocked { get; set; }

        public int FirstZeroPushLeftBlocked { get; set; }

        public int FirstZeroPushRightBlocked { get; set; }

        public int FirstZeroPushLastCratePlayerAccessBlocked { get; set; }

        public int FirstZeroPushLastCrateWallBlocked { get; set; }

        public int FirstZeroPushLastCrateCrateBlocked { get; set; }

        public int FirstZeroPushLastCrateCornerDeadlocked { get; set; }

        public int FirstZeroPushLastCrateLegalPushes { get; set; }

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

        public int SolutionOpportunityPairs { get; set; }

        public int SolutionOpportunityBlocks { get; set; }

        public Dictionary<int, HashSet<int>> SolutionOpportunities { get; set; } =
            new();

        public int SolutionRequiredDependencyPairs { get; set; }

        public int SolutionRequiredDependencyBlocks { get; set; }

        public Dictionary<int, HashSet<int>> SolutionRequiredDependencies { get; set; } =
            new();

        public int SolutionTemporaryDisplacementCrates { get; set; }

        public int SolutionTemporaryDisplacementMoves { get; set; }

        public List<string> SolutionPushSequence { get; set; } =
            new();

        public int SolutionPlayerAccessBlockPairs { get; set; }

        public int SolutionPlayerAccessBlockMoves { get; set; }

        public Dictionary<int, HashSet<int>> SolutionPlayerAccessBlocks { get; set; } =
            new();

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
                $"Initial try-push legal pushes: {InitialTryPushLegalPushes}\r\n" +
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
                $"    Exact parent reversals: {UnsolvableExactParentReversalStates}\r\n" +
                $"    HashSet duplicates: {UnsolvableHashSetDuplicateStates}\r\n" +
                $"Unique successor states: {UniqueSuccessorStates}\r\n" +
                $"Below target: {BelowTargetCandidates}\r\n" +
                $"Above target: {AboveTargetCandidates}\r\n" +
                $"Accepted: {AcceptedCandidates}\r\n";

            report +=
                "\r\n" +
                "SOLVABLE CANDIDATE PUSH DISTRIBUTION\r\n";

            foreach (int pushCount in
                     SolvableCandidatePushCounts.Keys.OrderBy(
                         pushes => pushes))
            {
                report +=
                    $"  {pushCount} pushes: " +
                    $"{SolvableCandidatePushCounts[pushCount]}\r\n";
            }

            report +=
                "\r\n" +
                "UNSOLVABLE CANDIDATE MAXIMUM PUSH DISTRIBUTION\r\n";

            foreach (int pushCount in
                     UnsolvableCandidateMaximumPushCounts.Keys.OrderBy(
                         pushes => pushes))
            {
                report +=
                    $"  {pushCount} maximum pushes: " +
                    $"{UnsolvableCandidateMaximumPushCounts[pushCount]}\r\n";
            }

            report +=
    "\r\n" +
    "UNSOLVABLE CANDIDATE MINIMUM ZERO-PUSH DEPTH DISTRIBUTION\r\n";

            foreach (int depth in
                     UnsolvableCandidateMinimumZeroPushDepths.Keys.OrderBy(
                         value => value))
            {
                report +=
                    $"  Depth {depth}: " +
                    $"{UnsolvableCandidateMinimumZeroPushDepths[depth]}\r\n";
            }

            report +=
    "\r\n" +
    "UNSOLVABLE CANDIDATE LEGAL PUSHES BEFORE FIRST ZERO-PUSH STATE\r\n";

            foreach (int legalPushes in
                     UnsolvableCandidateLegalPushesBeforeZero.Keys.OrderBy(
                         value => value))
            {
                report +=
                    $"  {legalPushes} legal pushes before zero: " +
                    $"{UnsolvableCandidateLegalPushesBeforeZero[legalPushes]}\r\n";
            }

            report +=
    "\r\n" +
    "FIRST ZERO-PUSH STATE CRATE BLOCKING REASONS\r\n" +
    "  Player access blocked: " +
    $"{FirstZeroPushPlayerAccessBlockedCrates}\r\n" +
    "  Wall blocked: " +
    $"{FirstZeroPushWallBlockedCrates}\r\n" +
    "  Another crate blocked: " +
    $"{FirstZeroPushCrateBlockedCrates}\r\n" +
    "  Static corner deadlock: " +
    $"{FirstZeroPushCornerDeadlockedCrates}\r\n";

            report +=
                "\r\n" +
                "FIRST ZERO-PUSH STATE PLAYER ACCESS BLOCKED DIRECTIONS\r\n" +
                "  Up: " +
                $"{FirstZeroPushUpBlocked}\r\n" +
                "  Down: " +
                $"{FirstZeroPushDownBlocked}\r\n" +
                "  Left: " +
                $"{FirstZeroPushLeftBlocked}\r\n" +
                "  Right: " +
                $"{FirstZeroPushRightBlocked}\r\n";

            report +=
    "\r\n" +
    "FIRST ZERO-PUSH STATE LAST-PUSHED CRATE FOUR-DIRECTION ANALYSIS\r\n" +
    "  Player access blocked: " +
    $"{FirstZeroPushLastCratePlayerAccessBlocked}\r\n" +
    "  Wall blocked: " +
    $"{FirstZeroPushLastCrateWallBlocked}\r\n" +
    "  Another crate blocked: " +
    $"{FirstZeroPushLastCrateCrateBlocked}\r\n" +
    "  Static corner deadlock: " +
    $"{FirstZeroPushLastCrateCornerDeadlocked}\r\n" +
    "  Legal pushes: " +
    $"{FirstZeroPushLastCrateLegalPushes}\r\n";

            report +=
                "\r\n" +
                "SOLUTION\r\n" +
                $"Minimum pushes: {MinimumPushes}\r\n" +
                $"Crates moved: {CratesMoved}\r\n" +
                $"Crate push order changes: {CratePushOrderChanges}\r\n" +
                $"Maximum consecutive pushes: {MaximumConsecutivePushes}\r\n";

            foreach (int crateIndex in
                     CratePushCounts.Keys.OrderBy(
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
                "INITIAL CRATE MOBILITY DISTRIBUTION\r\n";

            foreach (int mobility in
                     InitialCrateMobilityDistribution.Keys.OrderBy(
                         value => value))
            {
                report +=
                    $"  {mobility} legal pushes: " +
                    $"{InitialCrateMobilityDistribution[mobility]}\r\n";
            }

            report +=
                "\r\n" +
                "INITIAL PLAYER PUSH MOBILITY DISTRIBUTION\r\n";

            foreach (int mobility in
                     InitialPlayerPushMobilityDistribution.Keys.OrderBy(
                         value => value))
            {
                report +=
                    $"  {mobility} legal pushes: " +
                    $"{InitialPlayerPushMobilityDistribution[mobility]}\r\n";
            }

            report +=
                "\r\n" +
                "INITIAL PLAYER MOBILITY -> MAXIMUM PUSH DISTRIBUTION\r\n";

            foreach (int mobility in
                     InitialPlayerMobilityMaximumPushDistribution.Keys.OrderBy(
                         value => value))
            {
                report +=
                    $"  Initial {mobility} legal pushes:\r\n";

                foreach (int maximumPushes in
                         InitialPlayerMobilityMaximumPushDistribution[mobility]
                             .Keys
                             .OrderBy(
                                 value => value))
                {
                    report +=
                        $"    {maximumPushes} maximum pushes: " +
                        $"{InitialPlayerMobilityMaximumPushDistribution[mobility][maximumPushes]}\r\n";
                }
            }

            report +=
    "\r\n" +
    "INITIAL -> AFTER FIRST PUSH MOBILITY DISTRIBUTION\r\n";

            foreach (int initialMobility in
                     InitialToAfterFirstPushMobilityDistribution.Keys.OrderBy(
                         value => value))
            {
                report +=
                    $"  Initial {initialMobility} legal pushes:\r\n";

                Dictionary<int, int> afterFirstPushDistribution =
                    InitialToAfterFirstPushMobilityDistribution[
                        initialMobility];

                foreach (int afterFirstPushMobility in
                         afterFirstPushDistribution.Keys.OrderBy(
                             value => value))
                {
                    report +=
                        $"    {afterFirstPushMobility} legal pushes after first push: " +
                        $"{afterFirstPushDistribution[afterFirstPushMobility]}\r\n";
                }
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

                List<Point> orderedReachablePositions =
                    reachablePositions
                        .OrderBy(
                            point => point.Y)
                        .ThenBy(
                            point => point.X)
                        .ToList();

                for (int positionIndex = 0;
                     positionIndex < orderedReachablePositions.Count;
                     positionIndex += 15)
                {
                    List<string> positions =
                        orderedReachablePositions
                            .Skip(positionIndex)
                            .Take(15)
                            .Select(
                                point =>
                                    $"({point.X},{point.Y})")
                            .ToList();

                    report +=
                        "    " +
                        string.Join(
                            " ",
                            positions) +
                        "\r\n";
                }

                foreach (Point goal in
                         reachableGoals
                             .OrderBy(
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
                "SOLUTION OPPORTUNITIES\r\n" +
                $"Opportunity pairs: {SolutionOpportunityPairs}\r\n" +
                $"Opportunity blocks: {SolutionOpportunityBlocks}\r\n";

            report +=
                "\r\n" +
                "SOLUTION PLAYER ACCESS BLOCKS\r\n" +
                $"Player access block pairs: {SolutionPlayerAccessBlockPairs}\r\n" +
                $"Player access block moves: {SolutionPlayerAccessBlockMoves}\r\n";

            foreach (int crateIndex in
                     SolutionPlayerAccessBlocks.Keys.OrderBy(
                         index => index))
            {
                int crateNumber =
                    crateIndex + 1;

                report +=
                    $"Crate {crateNumber} blocks player access to: ";

                List<int> affectedCrates =
                    SolutionPlayerAccessBlocks[crateIndex]
                        .OrderBy(
                            index => index)
                        .Select(
                            index => index + 1)
                        .ToList();

                report +=
                    string.Join(
                        ", ",
                        affectedCrates) +
                    "\r\n";
            }

            report +=
                "\r\n" +
                "REQUIRED SOLUTION DEPENDENCIES\r\n" +
                $"Required dependency pairs: {SolutionRequiredDependencyPairs}\r\n" +
                $"Required dependency blocks: {SolutionRequiredDependencyBlocks}\r\n";

            foreach (int crateIndex in
                     SolutionRequiredDependencies.Keys.OrderBy(
                         index => index))
            {
                int crateNumber =
                    crateIndex + 1;

                report +=
                    $"Crate {crateNumber} must move before: ";

                List<int> affectedCrates =
                    SolutionRequiredDependencies[crateIndex]
                        .OrderBy(
                            index => index)
                        .Select(
                            index => index + 1)
                        .ToList();

                report +=
                    string.Join(
                        ", ",
                        affectedCrates) +
                    "\r\n";
            }

            foreach (int crateIndex in
                     SolutionOpportunities.Keys.OrderBy(
                         index => index))
            {
                int crateNumber =
                    crateIndex + 1;

                report +=
                    $"Crate {crateNumber} opens opportunities for: ";

                List<int> affectedCrates =
                    SolutionOpportunities[crateIndex]
                        .OrderBy(
                            index => index)
                        .Select(
                            index => index + 1)
                        .ToList();

                report +=
                    string.Join(
                        ", ",
                        affectedCrates) +
                    "\r\n";
            }

            report +=
                "\r\n" +
                "SOLUTION PUSH SEQUENCE\r\n";

            foreach (string push in
                     SolutionPushSequence)
            {
                report +=
                    push +
                    "\r\n";
            }

            report +=
                "\r\n" +
                "TIMING\r\n" +
                $"Candidate generation: {CandidateGenerationMilliseconds} ms\r\n" +
                $"Solver: {SolverMilliseconds} ms\r\n" +
                $"Average solver call: {AverageSolverMilliseconds:F2} ms\r\n" +
                $"  Occupancy: {OccupancyMilliseconds} ms\r\n" +
                $"  Current reachability: {CurrentReachabilityMilliseconds} ms\r\n" +
                $"  Successor reachability: {SuccessorReachabilityMilliseconds} ms\r\n" +
                $"  Push validation: {PushValidationMilliseconds} ms\r\n" +
                $"  Array copy: {ArrayCopyMilliseconds} ms\r\n" +
                $"  State construction: {StateConstructionMilliseconds} ms\r\n" +
                $"  HashSet lookup: {HashSetLookupMilliseconds} ms\r\n" +
                $"  Static deadlock: {StaticDeadlockMilliseconds} ms\r\n" +
                $"Total time: {totalTime:hh\\:mm\\:ss}\r\n" +
                $"Crate interaction pairs: {CrateInteractionPairs}\r\n" +
                $"Crate interaction blocks: {CrateInteractionBlocks}\r\n";

            return report;
        }
    }
}