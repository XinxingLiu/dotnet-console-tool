using System;
using System.Reflection;
using Microsoft.Extensions.CommandLineUtils;

namespace DataProcessor
{
    class Program
    {
        static void Main(string[] args)
        {
            var app = new CommandLineApplication();

            app.Name = "DataProcessor";
            app.Description = "Demo dotnet console tool to process data.";

            app.HelpOption("-?|-h|--help");
            app.VersionOption("-v|--version", () => {
                return string.Format("Version {0}", Assembly.GetEntryAssembly().GetCustomAttribute<AssemblyInformationalVersionAttribute>().InformationalVersion);
            });

            app.OnExecute(() =>
            {
                Console.Write(app.GetHelpText());
                return 0;
            });

            app.Command("validate", (command) =>
            {
                command.Description = "Description for 'validate' sub-command.";
                command.ExtendedHelpText = "Extended help text for 'validate' sub-command.";
                command.HelpOption("-?|-h|--help");

                var inputPathsOption = command.Option("-i|--input-path",
                    "Specify multiple input data sources. ",
                    CommandOptionType.MultipleValue);

                var outputPathOption = command.Option("-o|--output-path",
                    "Path to the directory for the validation reports.",
                    CommandOptionType.SingleValue);

                command.OnExecute(() =>
                {
                    var inputPaths = inputPathsOption.Values;
                    var outputPath = outputPathOption.Value();

                    Console.WriteLine($"Start validating. Data sources: ");
                    foreach (var path in inputPaths)
                    {
                        Console.WriteLine($"\t{path}");
                    }

                    Console.WriteLine("Validating data.");
                    
                    Console.WriteLine($"Finish Validation. See validation reports under {outputPath}.");
                    return 0;
                });
            });

            app.Command("process", (command) =>
            {
                command.Description = "Description for 'process' sub-command.";
                command.ExtendedHelpText = "Extended help text for 'process' sub-command.";
                command.HelpOption("-?|-h|--help");

                var inputPathsOption = command.Option("-i|--input-path",
                    "Specify multiple input data sources. ",
                    CommandOptionType.MultipleValue);

                var outputPathOption = command.Option("-o|--output-path",
                    "Path to the directory for the validation reports.",
                    CommandOptionType.SingleValue);

                var skipValidationOption = command.Option("-s|--skip-validation",
                    "Skip validation or not.",
                    CommandOptionType.NoValue);

                command.OnExecute(() =>
                {
                    var inputPaths = inputPathsOption.Values;
                    var outputPath = outputPathOption.Value();
                    var skipValidation = skipValidationOption.HasValue();

                    Console.WriteLine($"Start validating. Data sources: ");
                    foreach (var path in inputPaths)
                    {
                        Console.WriteLine($"\t{path}");
                    }

                    if (skipValidation)
                    {
                        Console.WriteLine("Skip validation.");
                    }
                    else
                    {
                        Console.WriteLine("Validating data.");
                    }

                    Console.WriteLine("Processing data.");

                    Console.WriteLine($"Finish processing. See output under {outputPath}.");
                    return 0;
                });
            });

            try
            {
                app.Execute(args);
            }
            catch (CommandParsingException ex)
            {
                Console.WriteLine(ex.Message);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Unable to execute application: {0}", ex.Message);
            }
        }
    }
}
