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
            DifficultyProfile profile =
                DifficultyProgression.GetProfile(
                    levelNumber);

            DifficultySettings settings =
                DifficultyProgression.GetSettings(
                    levelNumber);

            Stopwatch generationTimer =
                Stopwatch.StartNew();

            NumberPushLevel? level =
                generator.Generate(
                    levelNumber,
                    settings);

            generationTimer.Stop();

            NumberPushGenerationResult result =
                new NumberPushGenerationResult
                {
                    Level = level,
                    Profile = profile,
                    Settings = settings,
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
    int lastLevel)
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

            for (int levelNumber = firstLevel;
                 levelNumber <= lastLevel;
                 levelNumber++)
            {
                results.Add(
                    Generate(
                        levelNumber));
            }

            return results;
        }
    }
}