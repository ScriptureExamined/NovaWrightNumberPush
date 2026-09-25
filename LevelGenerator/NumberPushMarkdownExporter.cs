using System.Text;
using NovaWrightNumberPush;

namespace NovaWright.NumberPush.LevelGenerator
{
    public static class NumberPushMarkdownExporter
    {
        public static string Export(
            NumberPushLevel level,
            NumberPushSolution solution,
            NumberPushDifficulty difficulty,
            int seed,
            string outputDirectory
        )
        {
            Directory.CreateDirectory(outputDirectory);

            string fileName = $"Level{level.LevelNumber:D3}-Solution.md";

            string filePath = Path.Combine(outputDirectory, fileName);

            StringBuilder markdown = new StringBuilder();

            markdown.AppendLine("---");
            markdown.AppendLine($"title: \"Number Push Level {level.LevelNumber}\"");
            markdown.AppendLine("layout: post");
            markdown.AppendLine($"level: {level.LevelNumber}");
            markdown.AppendLine($"complexity: {difficulty.Complexity}");
            markdown.AppendLine("---");
            markdown.AppendLine();

            markdown.AppendLine($"# Number Push Level {level.LevelNumber}");
            markdown.AppendLine();

            markdown.AppendLine("## Level Information");
            markdown.AppendLine();

            markdown.AppendLine($"- **Complexity:** {difficulty.Complexity}");
            markdown.AppendLine($"- **Board:** {level.Rows} × {level.Columns}");
            markdown.AppendLine($"- **Crates:** {level.Crates.Count}");
            markdown.AppendLine($"- **Interior Walls:** " + $"{GetInteriorWallCount(level)}");
            markdown.AppendLine($"- **Minimum Solution:** " + $"{solution.MinimumPushes} pushes");
            markdown.AppendLine($"- **Generator Seed:** {seed}");

            markdown.AppendLine();

            markdown.AppendLine("## Crates");
            markdown.AppendLine();

            for (int i = 0; i < level.Crates.Count; i++)
            {
                NumberPushCrate crate = level.Crates[i];

                markdown.AppendLine(
                    $"- **Crate {i + 1}:** "
                        + $"Position {FormatPoint(crate.Position)}, "
                        + $"Distance {crate.Distance}"
                );
            }

            markdown.AppendLine();

            markdown.AppendLine("## Goals");
            markdown.AppendLine();

            for (int i = 0; i < level.Goals.Count; i++)
            {
                markdown.AppendLine($"- **Goal {i + 1}:** " + $"{FormatPoint(level.Goals[i])}");
            }

            markdown.AppendLine();

            markdown.AppendLine("## Solution");
            markdown.AppendLine();

            if (!solution.IsSolved)
            {
                markdown.AppendLine("This level does not have a solution.");
                markdown.AppendLine();

                return WriteFile(filePath, markdown);
            }

            markdown.AppendLine($"**Minimum pushes:** " + $"{solution.MinimumPushes}");
            markdown.AppendLine();

            for (int i = 0; i < solution.Steps.Count; i++)
            {
                NumberPushSolutionStep step = solution.Steps[i];

                markdown.AppendLine($"### Push {step.PushNumber}");

                markdown.AppendLine();

                markdown.AppendLine($"- **Crate:** " + $"{step.CrateNumber}");

                markdown.AppendLine($"- **Direction:** " + $"{GetDirectionName(step.Direction)}");

                markdown.AppendLine($"- **Distance:** " + $"{step.Distance}");

                markdown.AppendLine($"- **Player Start:** " + $"{FormatPoint(step.PlayerStart)}");

                markdown.AppendLine(
                    $"- **Player Push Position:** " + $"{FormatPoint(step.PlayerPushPosition)}"
                );

                markdown.AppendLine($"- **Crate Start:** " + $"{FormatPoint(step.CrateStart)}");

                markdown.AppendLine($"- **Crate End:** " + $"{FormatPoint(step.CrateEnd)}");

                markdown.AppendLine($"- **Player Walk:** " + $"{FormatPath(step.PlayerPath)}");

                markdown.AppendLine();
            }

            markdown.AppendLine("## Generation Settings");
            markdown.AppendLine();

            markdown.AppendLine(
                $"- **Target Pushes:** "
                    + $"{difficulty.MinimumSolutionPushes}–"
                    + $"{difficulty.MaximumSolutionPushes}"
            );

            markdown.AppendLine(
                $"- **Crate Count Range:** "
                    + $"{difficulty.MinimumCrates}–"
                    + $"{difficulty.MaximumCrates}"
            );

            markdown.AppendLine(
                $"- **Crate Distance Range:** "
                    + $"{difficulty.MinimumCrateDistance}–"
                    + $"{difficulty.MaximumCrateDistance}"
            );

            markdown.AppendLine(
                $"- **Interior Wall Range:** "
                    + $"{difficulty.MinimumInteriorWalls}–"
                    + $"{difficulty.MaximumInteriorWalls}"
            );

            markdown.AppendLine();

            return WriteFile(filePath, markdown);
        }

        private static string WriteFile(string filePath, StringBuilder markdown)
        {
            File.WriteAllText(filePath, markdown.ToString(), Encoding.UTF8);

            return filePath;
        }

        private static int GetInteriorWallCount(NumberPushLevel level)
        {
            int outerWallCount = (level.Columns * 2) + ((level.Rows - 2) * 2);

            return level.Walls.Count - outerWallCount;
        }

        private static string GetDirectionName(Point direction)
        {
            if (direction.X == 0 && direction.Y == -1)
            {
                return "Up";
            }

            if (direction.X == 0 && direction.Y == 1)
            {
                return "Down";
            }

            if (direction.X == -1 && direction.Y == 0)
            {
                return "Left";
            }

            if (direction.X == 1 && direction.Y == 0)
            {
                return "Right";
            }

            return "Unknown";
        }

        private static string FormatPoint(Point point)
        {
            return $"({point.X}, {point.Y})";
        }

        private static string FormatPath(List<Point> path)
        {
            if (path.Count == 0)
            {
                return "None";
            }

            return string.Join(" → ", path.Select(FormatPoint));
        }
    }
}
