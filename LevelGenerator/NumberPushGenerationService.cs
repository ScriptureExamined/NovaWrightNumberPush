using NovaWrightNumberPush;
using System.Diagnostics;

namespace NovaWright.NumberPush.LevelGenerator
{
    public class NumberPushGenerationService
    {
        private readonly NumberPushLevelGenerator generator;

        public int Seed { get; }

        public NumberPushGenerationService(
            int seed)
        {
            Seed =
                seed;

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

            Stopwatch generationTimer =
                Stopwatch.StartNew();

            NumberPushLevel? level =
                generator.Generate(
                    levelNumber,
                    difficulty);

            generationTimer.Stop();

            NumberPushGenerationResult result =
                new NumberPushGenerationResult
                {
                    Level =
                        level,

                    Difficulty =
    difficulty,

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

            int completedLevels = 0;

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