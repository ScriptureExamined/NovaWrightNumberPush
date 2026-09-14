namespace NovaWright.NumberPush.LevelGenerator
{
    public class NumberPushGenerationDiagnostics
    {
        public int LevelNumber { get; set; }

        public int TargetMinimumPushes { get; set; }

        public int TargetMaximumPushes { get; set; }

        public int FinalRows { get; set; }

        public int FinalColumns { get; set; }

        public Dictionary<string, int> BoardSizeAttempts { get; } =
    new Dictionary<string, int>();

        public Dictionary<string, int> BoardSizeSolverCalls { get; } =
            new Dictionary<string, int>();

        public Dictionary<string, long> BoardSizeMilliseconds { get; } =
    new Dictionary<string, long>();

        public Dictionary<string, long> BoardSizeStatesExplored { get; } =
    new Dictionary<string, long>();

        public Dictionary<string, long> BoardSizeUnsolvableStates { get; } =
            new Dictionary<string, long>();

        public int TotalAttempts { get; set; }

        public int WallGenerationFailures { get; set; }
        public int GeometricFailures { get; set; }
        public int WallReachabilityFailures { get; set; }

        public int CrateReachabilityPasses { get; set; }

        public int CrateReachabilityFailures { get; set; }

        public int TotalSolverCalls { get; set; }

        public long StatesExplored { get; set; }

        public long UnsolvableStates { get; set; }

        public long MaximumStatesExplored { get; set; }

        public long MaximumUnsolvableStates { get; set; }

        public long AcceptedStates { get; set; }

        public long TotalLegalPushes { get; set; }

        public long MaximumLegalPushes { get; set; }

        public long ZeroLegalPushStates { get; set; }

        public double AverageLegalPushes
        {
            get
            {
                if (StatesExplored == 0)
                {
                    return 0;
                }

                return
                    (double)TotalLegalPushes /
                    StatesExplored;
            }
        }

        public int UnsolvableCandidates { get; set; }

        public int BelowTargetCandidates { get; set; }

        public int AboveTargetCandidates { get; set; }

        public Dictionary<int, int> SolvablePushCounts { get; } =
    new Dictionary<int, int>();

        public int AcceptedCandidates { get; set; }

        public long CandidateGenerationMilliseconds { get; set; }

        public long SolverMilliseconds { get; set; }

        public long UnsolvableSolverMilliseconds { get; set; }

        public long BelowTargetSolverMilliseconds { get; set; }

        public long AboveTargetSolverMilliseconds { get; set; }

        public long AcceptedSolverMilliseconds { get; set; }

        public long TotalMilliseconds { get; set; }

        public double AverageStatesExplored
        {
            get
            {
                if (TotalSolverCalls == 0)
                {
                    return 0;
                }

                return
                    (double)StatesExplored /
                    TotalSolverCalls;
            }
        }

        public double AverageUnsolvableStates
        {
            get
            {
                if (UnsolvableCandidates == 0)
                {
                    return 0;
                }

                return
                    (double)UnsolvableStates /
                    UnsolvableCandidates;
            }
        }

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
            return
                $"LEVEL {LevelNumber} DIAGNOSTICS\r\n" +
                $"Target pushes: {TargetMinimumPushes}-{TargetMaximumPushes}\r\n" +
                $"Final board: {FinalRows}x{FinalColumns}\r\n" +
                "\r\n" +
                "BOARD SIZE ATTEMPTS\r\n" +
string.Join(
    "\r\n",
    BoardSizeAttempts.Select(
        entry =>
            $"  {entry.Key}: {entry.Value}")) +
"\r\n" +
"\r\n" +
"BOARD SIZE SOLVER CALLS\r\n" +
string.Join(
    "\r\n",
    BoardSizeSolverCalls.Select(
        entry =>
            $"  {entry.Key}: {entry.Value}")) +
"\r\n" +

"BOARD SIZE TIME\r\n" +
string.Join(
    "\r\n",
    BoardSizeMilliseconds.Select(
        entry =>
            $"  {entry.Key}: {TimeSpan.FromMilliseconds(entry.Value):hh\\:mm\\:ss}")) +
"\r\n" +
"\r\n" +
"BOARD SIZE STATES EXPLORED\r\n" +
string.Join(
    "\r\n",
    BoardSizeStatesExplored.Select(
        entry =>
            $"  {entry.Key}: {entry.Value:N0}")) +
"\r\n" +
"\r\n" +
"BOARD SIZE UNSOLVABLE STATES\r\n" +
string.Join(
    "\r\n",
    BoardSizeUnsolvableStates.Select(
        entry =>
            $"  {entry.Key}: {entry.Value:N0}")) +
"\r\n" +
"\r\n" +
"CANDIDATES\r\n" +
                $"Total attempts: {TotalAttempts}\r\n" +
                $"Wall generation failures: {WallGenerationFailures}\r\n" +
                $"Geometric failures: {GeometricFailures}\r\n" +
                $"Wall reachability failures: {WallReachabilityFailures}\r\n" +
                $"Crate reachability passes: {CrateReachabilityPasses}\r\n" +
                $"Crate reachability failures: {CrateReachabilityFailures}\r\n" +
                $"Solver calls: {TotalSolverCalls}\r\n" +
                $"States explored: {StatesExplored:N0}\r\n" +
                $"Average states per solver call: {AverageStatesExplored:N0}\r\n" +
                $"Maximum states in a solver call: {MaximumStatesExplored:N0}\r\n" +
                $"Unsolvable: {UnsolvableCandidates}\r\n" +
                $"Unsolvable states: {UnsolvableStates:N0}\r\n" +
                $"Average unsolvable states: {AverageUnsolvableStates:N0}\r\n" +
                $"Maximum unsolvable states: {MaximumUnsolvableStates:N0}\r\n" +
                $"Accepted states: {AcceptedStates:N0}\r\n" +
                $"Total legal pushes: {TotalLegalPushes:N0}\r\n" +
                $"Average legal pushes per state: {AverageLegalPushes:F2}\r\n" +
                $"Maximum legal pushes in a state: {MaximumLegalPushes:N0}\r\n" +
                $"States with zero legal pushes: {ZeroLegalPushStates:N0}\r\n" +
                $"Below target: {BelowTargetCandidates}\r\n" +
                $"Above target: {AboveTargetCandidates}\r\n" +
                $"Accepted: {AcceptedCandidates}\r\n" +
                "\r\n" +
                "SOLVABLE PUSH DISTRIBUTION\r\n" +
                string.Join(
                    "\r\n",
                    SolvablePushCounts
                        .OrderBy(
                            entry =>
                                entry.Key)
                        .Select(
                            entry =>
                                $"  {entry.Key} pushes: {entry.Value}")) +
                "\r\n" +
                "\r\n" +
                "TIMING\r\n" +
                $"Candidate generation: {CandidateGenerationMilliseconds} ms\r\n" +
                $"Solver: {SolverMilliseconds} ms\r\n" +
                $"Average solver call: {AverageSolverMilliseconds:F2} ms\r\n" +
                $"Unsolvable solver time: {UnsolvableSolverMilliseconds} ms\r\n" +
                $"Below target solver time: {BelowTargetSolverMilliseconds} ms\r\n" +
                $"Above target solver time: {AboveTargetSolverMilliseconds} ms\r\n" +
                $"Accepted solver time: {AcceptedSolverMilliseconds} ms\r\n" +
                $"Total time: {TimeSpan.FromMilliseconds(TotalMilliseconds):hh\\:mm\\:ss}";
        }

        public void PrintReport()
        {
            Console.WriteLine();
            Console.WriteLine(
                "========================================");

            Console.WriteLine(
                $"LEVEL {LevelNumber} DIAGNOSTICS");

            Console.WriteLine(
                "========================================");

            Console.WriteLine(
                $"Target pushes: " +
                $"{TargetMinimumPushes}-{TargetMaximumPushes}");

            Console.WriteLine(
                $"Final board: " +
                $"{FinalRows}x{FinalColumns}");

            Console.WriteLine(
    "Attempts by board size:");

            Console.WriteLine();

            Console.WriteLine(
                "CANDIDATES");

            Console.WriteLine(
                $"Total attempts: {TotalAttempts}");

            Console.WriteLine(
                $"Wall generation failures: " +
                $"{WallGenerationFailures}");

            Console.WriteLine(
                $"Geometric failures: " +
                $"{GeometricFailures}");

            Console.WriteLine(
                $"Solver calls: {TotalSolverCalls}");

            Console.WriteLine(
                $"Unsolvable: {UnsolvableCandidates}");

            Console.WriteLine(
                $"Below target: {BelowTargetCandidates}");

            Console.WriteLine(
                $"Above target: {AboveTargetCandidates}");

            Console.WriteLine(
                $"Accepted: {AcceptedCandidates}");

            Console.WriteLine();

            Console.WriteLine(
                "TIMING");

            Console.WriteLine(
                $"Candidate generation: " +
                $"{CandidateGenerationMilliseconds} ms");

            Console.WriteLine(
                $"Solver: " +
                $"{SolverMilliseconds} ms");

            Console.WriteLine(
                $"Average solver call: " +
                $"{AverageSolverMilliseconds:F2} ms");

            Console.WriteLine(
                $"Total: " +
                $"{TotalMilliseconds} ms");

            Console.WriteLine();

            if (TotalMilliseconds > 0)
            {
                double solverPercentage =
                    SolverMilliseconds * 100.0 /
                    TotalMilliseconds;

                double generationPercentage =
                    CandidateGenerationMilliseconds * 100.0 /
                    TotalMilliseconds;

                Console.WriteLine(
                    $"Solver percentage: " +
                    $"{solverPercentage:F1}%");

                Console.WriteLine(
                    $"Generation percentage: " +
                    $"{generationPercentage:F1}%");
            }

            Console.WriteLine(
                "========================================");

            Console.WriteLine();
        }
    }
}