using NovaWrightNumberPush;
using System.Diagnostics;

namespace NovaWright.NumberPush.LevelGenerator
{
    public class NumberPushGenerationService
    {
        private readonly NumberPushLevelGenerator generator;

        private readonly bool reportDiagnostics;

        public int Seed { get; }

        public NumberPushGenerationService(
            int seed,
            bool reportDiagnostics)
        {
            Seed =
                seed;

            this.reportDiagnostics =
                reportDiagnostics;

            generator =
                new NumberPushLevelGenerator(
                    seed);
        }

        public NumberPushGenerationResult Generate(
            int levelNumber)
        {
            NumberPushDifficulty difficulty =
                new NumberPushDifficulty(
                    levelNumber);

            NumberPushGenerationDiagnostics? diagnostics =
                reportDiagnostics
                    ? new NumberPushGenerationDiagnostics()
                    : null;

            Stopwatch generationTimer =
                Stopwatch.StartNew();

            NumberPushLevel? level =
                generator.Generate(
                    levelNumber,
                    difficulty,
                    diagnostics);

            generationTimer.Stop();

            if (diagnostics != null &&
                level != null)
            {
                diagnostics.FinalRows =
                    level.Rows;

                diagnostics.FinalColumns =
                    level.Columns;
            }

            NumberPushGenerationResult result =
                new NumberPushGenerationResult
                {
                    Level =
                        level,

                    Difficulty =
                        difficulty,

                    Diagnostics =
                        diagnostics,

                    GenerationMilliseconds =
                        generationTimer.ElapsedMilliseconds
                };

            if (level == null)
            {
                return result;
            }

            Stopwatch solutionTimer =
                Stopwatch.StartNew();

            NumberPushSolver solver =
                new NumberPushSolver(
                    level);

            NumberPushSolution solution =
                solver.FindSolution();

            solutionTimer.Stop();

            result.Solution =
                solution;

            result.SolutionMilliseconds =
                solutionTimer.ElapsedMilliseconds;

            if (diagnostics != null)
            {
                diagnostics.TotalMilliseconds =
                    generationTimer.ElapsedMilliseconds +
                    solutionTimer.ElapsedMilliseconds;
            }

            return result;
        }

        public List<NumberPushGenerationResult> GenerateRange(
            int firstLevel,
            int lastLevel,
            IProgress<int>? progress = null)
        {
            if (firstLevel <= 0)
            {
                throw new ArgumentException(
                    "First level must be greater than zero.",
                    nameof(firstLevel));
            }

            if (lastLevel < firstLevel)
            {
                throw new ArgumentException(
                    "Last level cannot be smaller than first level.",
                    nameof(lastLevel));
            }

            List<NumberPushGenerationResult> results =
                new List<NumberPushGenerationResult>();

            int levelCount =
                lastLevel - firstLevel + 1;

            int completedLevels =
                0;

            for (int levelNumber = firstLevel;
                 levelNumber <= lastLevel;
                 levelNumber++)
            {
                results.Add(
                    Generate(
                        levelNumber));

                completedLevels++;

                progress?.Report(
                    completedLevels);
            }

            return results;
        }
    }
}
