using ASI.Console;
using ASI.Console.Commands;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;

namespace CompanyProfile.Console.Commands
{
    [Command("install")]
    public class CompanyProfileInstallCommand : ICompanyProfileCommand
    {
        public void Execute(CompanyProfileContext context)
        {
            var websitesPath = @"D:\Websites";
            var baseAppName = "CompanyProfile";
            var newAppName = context.Args.GetOrDefault(0, "TestAppClone");
            var targetPath = Path.Combine(websitesPath, newAppName);


            void GitPull()
            {
                var appTemplatePath = Path.Combine(websitesPath, targetPath);
                var githubPath = "https://github.com/asi/ApplicationTemplate.git";
                var gitCommand = $"git clone {githubPath} {appTemplatePath}";
                var p = new Process();
                p.StartInfo.FileName = "cmd.exe";
                p.StartInfo.Arguments = $"/c {gitCommand}";
                p.StartInfo.RedirectStandardOutput = true;
                p.StartInfo.UseShellExecute = false;
                //p.StartInfo.CreateNoWindow = true;
                p.Start();
                p.WaitForExit();
            }


            if (Directory.Exists(targetPath))
            {
                if (Directory.EnumerateDirectories(targetPath).Any() || Directory.EnumerateFiles(targetPath).Any())
                {
                    Terminal.Red($"Target path {targetPath} must be empty");
                    return;
                }
            }

            GitPull();

            Terminal.Green($"Renaming folders");

            var srcFolder = Path.Combine(targetPath, "src");
            //changing folder name
            foreach (var dir in Directory.EnumerateDirectories(srcFolder)
                .Where(x => x.Contains(baseAppName)).ToList())
            {
                var newDir = dir.Replace(baseAppName, newAppName);
                Terminal.Yellow($"Moving '{dir}' -> '{newDir}'");
                Directory.Move(dir, newDir);
            }


            Terminal.Green($"Rename project files");
            foreach (var file in Directory.EnumerateFiles(targetPath, $"*{baseAppName}*.csproj", SearchOption.AllDirectories).ToList())
            {
                if (!File.Exists(file))
                    continue;

                var newFile = file.Replace(baseAppName, newAppName);
                Terminal.Yellow($"Moving '{file}' -> '{newFile}'");
                File.Move(file, newFile);
            }

            foreach (var dir in Directory.EnumerateDirectories(srcFolder))
            {
                Terminal.Green($"Find and replace files in {dir}");
                ReplaceTextInFiles(baseAppName, newAppName, dir, "*");
            }



            var specificFilesToMoveAndFix = new[]
            {
                "Jenkinsfile",
                $"{baseAppName}.sln",
                $"{baseAppName}.sln.startup.json",
            };

            foreach (var spec in specificFilesToMoveAndFix)
            {
                var fileName = Path.Combine(targetPath, spec);
                ReplaceTestInFile(baseAppName, newAppName, fileName);
                var newFile = fileName.Replace(baseAppName, newAppName);
                Terminal.Yellow($"Moving '{spec}' -> '{newFile}'");
                File.Move(fileName, newFile);
            }
            Terminal.Green("Done!");
        }

        private static void ReplaceTextInFiles(string baseAppName, string newAppName, string targetPath, string pattern)
        {
            foreach (var file in Directory.EnumerateFiles(targetPath, pattern, SearchOption.AllDirectories).ToList())
            {

                ReplaceTestInFile(baseAppName, newAppName, file);
            }
        }

        private static void ReplaceTestInFile(string baseAppName, string newAppName, string file)
        {
            if (!File.Exists(file))
                return;

            var text = File.ReadAllText(file);
            if (text.IndexOf(baseAppName) < 0)
                return;

            if (!text.Contains(baseAppName))
                return;

            var newText = text.Replace(baseAppName, newAppName);
            Terminal.Yellow($"Fixing text in '{file}'");
            File.WriteAllText(file, newText);
        }
    }
}
