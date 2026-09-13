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

        public int UnsolvableCandidates { get; set; }

        public int BelowTargetCandidates { get; set; }

        public int AboveTargetCandidates { get; set; }

        public int AcceptedCandidates { get; set; }

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
            return
                $"LEVEL {LevelNumber} DIAGNOSTICS\r\n" +
                $"Target pushes: {TargetMinimumPushes}-{TargetMaximumPushes}\r\n" +
                $"Final board: {FinalRows}x{FinalColumns}\r\n" +
                "\r\n" +
                "CANDIDATES\r\n" +
                $"Total attempts: {TotalAttempts}\r\n" +
                $"Wall generation failures: {WallGenerationFailures}\r\n" +
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

            Console.WriteLine();

            Console.WriteLine(
                "CANDIDATES");

            Console.WriteLine(
                $"Total attempts: {TotalAttempts}");

            Console.WriteLine(
                $"Wall generation failures: " +
                $"{WallGenerationFailures}");

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