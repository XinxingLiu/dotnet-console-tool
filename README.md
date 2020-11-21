# Dotnet console tool demo

This project is to demo how to create a console app in dotnet core, with package manager NuGet to pack and distribute the tool. See the code for how to define a dotnet tool NuGet package and how to add sub-command and options to the tool.

## Build and Release

0. enter project folder:
```bash
cd DataProcessor/
```

1. clean up:

```bash
dotnet clean -c Release
```

2. build and pack:

```bash
dotnet pack -c Release
```

Local path to DataProcessor NuGet package will be `nupkg\DataProcessor.0.0.1.nupkg`.

3. [optional] push to your NuGet server as needed.

## Install

To install the console app as dotnet tool on a machine, make sure you have dotnet sdk 3.1 installed, and run the following command:

```bash
package_source="nupkg"
dotnet tool install --add-source $package_source DataProcessor -g
```

If you want to use a package on your NuGet server, point `package_source` to your server. For example, if you use Azure DevOps artifact, `https://pkgs.dev.azure.com/<your org>/_packaging/<your project>/nuget/v3/index.json`


## Run

### See the help information with a `-h` or `--help` flag

```bash
DataProcessor --help
```

Console output:
```bash
 Version 0.0.1

Usage: DataProcessor [options] [command]

Options:
  -?|-h|--help  Show help information
  -v|--version  Show version information

Commands:
  process   Description for 'process' sub-command.
  validate  Description for 'validate' sub-command.

Use "DataProcessor [command] --help" for more information about a command.
```

### Tool version

```bash
DataProcessor --version
```

Console output:
```bash
 Version 0.0.1
```

### Help information for sub-commands

```bash
DataProcessor validate -h
```

Console output:
```bash
Usage: DataProcessor validate [options]

Options:
  -?|-h|--help      Show help information
  -i|--input-path   Specify multiple input data sources. 
  -o|--output-path  Path to the directory for the validation reports.
Extended help text for 'validate' sub-command.
```

```bash
DataProcessor process -h
```

Console output:
```bash
Usage: DataProcessor process [options]

Options:
  -?|-h|--help          Show help information
  -i|--input-path       Specify multiple input data sources. 
  -o|--output-path      Path to the directory for the validation reports.
  -s|--skip-validation  Skip validation or not.
Extended help text for 'process' sub-command.
```

### Call `validate` sub-command

```bash
DataProcessor validate -i /home/user/data-folder1 -i /home/user/data-folder2 -o /home/user/output
```
Console output:
```bash
Start validating. Data sources: 
        /home/user/data-folder1
        /home/user/data-folder2
Validating data.
Finish validation. See validation reports under /home/user/output.
```

### Call `process` sub-command

```bash
DataProcessor process -i /home/user/data-folder1 -i /home/user/data-folder2 -o /home/user/output -s
```

Console output:

```bash
Start validating. Data sources:
        /home/user/data-folder1
        /home/user/data-folder2
Skip validation.
Processing data.
Finish processing. See output under /home/user/output.
```