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

        public int TotalAttempts { get; set; }

        public int WallGenerationFailures { get; set; }
        public int GeometricFailures { get; set; }
        public int WallReachabilityFailures { get; set; }

        public int TotalSolverCalls { get; set; }

        public int UnsolvableCandidates { get; set; }

        public int BelowTargetCandidates { get; set; }

        public int AboveTargetCandidates { get; set; }

        public int AcceptedCandidates { get; set; }

        public long CandidateGenerationMilliseconds { get; set; }

        public long SolverMilliseconds { get; set; }

        public long UnsolvableSolverMilliseconds { get; set; }

        public long BelowTargetSolverMilliseconds { get; set; }

        public long AboveTargetSolverMilliseconds { get; set; }

        public long AcceptedSolverMilliseconds { get; set; }

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
"\r\n" +
"CANDIDATES\r\n" +
                $"Total attempts: {TotalAttempts}\r\n" +
                $"Wall generation failures: {WallGenerationFailures}\r\n" +
                $"Geometric failures: {GeometricFailures}\r\n" +
                $"Wall reachability failures: {WallReachabilityFailures}\r\n" +
                $"Solver calls: {TotalSolverCalls}\r\n" +
                $"Unsolvable: {UnsolvableCandidates}\r\n" +
                $"Below target: {BelowTargetCandidates}\r\n" +
                $"Above target: {AboveTargetCandidates}\r\n" +
                $"Accepted: {AcceptedCandidates}\r\n" +
                "\r\n" +
                "TIMING\r\n" +
$"Candidate generation: {CandidateGenerationMilliseconds} ms\r\n" +
$"Solver: {SolverMilliseconds} ms\r\n" +
$"Average solver call: {AverageSolverMilliseconds:F2} ms\r\n" +
$"Unsolvable solver time: {UnsolvableSolverMilliseconds} ms\r\n" +
$"Below target solver time: {BelowTargetSolverMilliseconds} ms\r\n" +
$"Above target solver time: {AboveTargetSolverMilliseconds} ms\r\n" +
$"Accepted solver time: {AcceptedSolverMilliseconds} ms\r\n" +
$"Total: {TotalMilliseconds} ms";
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