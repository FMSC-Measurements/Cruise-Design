﻿using Bogus;
using CruiseDesign.Services;
using Microsoft.Extensions.Logging;
using NSubstitute;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Runtime.CompilerServices;
using Xunit.Abstractions;

namespace CruiseDesign.Test
{
    public abstract class TestBase
    {
        protected ILogger MakeMockLogger() => Substitute.For<ILogger>();

        protected IDialogService MakeMockDialogService() => Substitute.For<IDialogService>();


        protected readonly ITestOutputHelper Output;
        private string _testTempPath;
        private List<string> FilesToBeDeleted { get; } = new List<string>();
        protected DbProviderFactory DbProvider { get; private set; }
        protected Stopwatch _stopwatch;
        private Randomizer _rand;

        protected bool CleanUpTestFiles { get; set; }
        protected Randomizer Rand => _rand ??= new Randomizer();

        public TestBase(ITestOutputHelper output)
        {
            Output = output;
            CleanUpTestFiles = false;

            var testTempPath = TestTempPath;
            if (!Directory.Exists(testTempPath))
            {
                Directory.CreateDirectory(testTempPath);
            }

            DbProvider = Microsoft.Data.Sqlite.SqliteFactory.Instance;
        }

        ~TestBase()
        {
            if (CleanUpTestFiles)
            {
                foreach (var file in FilesToBeDeleted)
                {
                    try
                    {
                        File.Delete(file);
                    }
                    catch
                    {
                        // do nothing
                    }
                }
            }
        }

        public string TestExecutionDirectory
        {
            get
            {
                var codeBase = new Uri(Assembly.GetExecutingAssembly().CodeBase).LocalPath;
                return Path.GetDirectoryName(codeBase);
            }
        }

        public string TestTempPath => _testTempPath ??= Path.Combine(Path.GetTempPath(), "TestTemp", this.GetType().FullName);
        public string TestFilesDirectory => Path.Combine(TestExecutionDirectory, "TestFiles");
        public string ResourceDirectory => Path.Combine(TestExecutionDirectory, "Resources");

        public string GetTempFilePathWithExt(string extention, [CallerMemberName] string testName = null)
        {
            var fileName = testName + "_" + Rand.Int().ToString("x") + "_" + extention;
            return GetTempFilePath(fileName);
        }

        public string GetTempFilePath(string fileName)
        {
            var filePath = Path.Combine(TestTempPath, fileName);
            Output.WriteLine("Generated Temp File Path: " + filePath);
            return filePath;
        }

        public string GetTestFile(string fileName) => InitializeTestFile(fileName);

        public string InitializeTestFile(string fileName)
        {
            var sourcePath = Path.Combine(TestFilesDirectory, fileName);
            if (File.Exists(sourcePath) == false) { throw new FileNotFoundException(sourcePath); }

            var targetPath = Path.Combine(TestTempPath, fileName);

            RegesterFileForCleanUp(targetPath);
            File.Copy(sourcePath, targetPath, true);
            return targetPath;
        }

        public void RegesterFileForCleanUp(string path)
        {
            FilesToBeDeleted.Add(path);
        }

        public void WriteDictionary<tKey, tValue>(IDictionary<tKey, tValue> dict)
        {
            Output.WriteLine("{");
            foreach (var entry in dict)
            {
                Output.WriteLine($"{{{entry.Key.ToString()} : {entry.Value.ToString()} }}");
            }
            Output.WriteLine("}");
        }

        public void StartTimer()
        {
            _stopwatch = new Stopwatch();
            Output.WriteLine("Stopwatch Started");
            _stopwatch.Start();
        }

        public void EndTimer()
        {
            _stopwatch.Stop();
            Output.WriteLine("Stopwatch Ended:" + _stopwatch.ElapsedMilliseconds.ToString() + "ms");
        }

        //public static async Task<int> RunProcessAsync(string fileName, string args)
        //{
        //    using (var process = new Process
        //    {
        //        StartInfo =
        //{
        //    FileName = fileName, Arguments = args,
        //    UseShellExecute = false, CreateNoWindow = true,
        //    RedirectStandardOutput = true, RedirectStandardError = true
        //},
        //        EnableRaisingEvents = true
        //    })
        //    {
        //        return await RunProcessAsync(process).ConfigureAwait(false);
        //    }
        //}

        //private static Task<int> RunProcessAsync(Process process)
        //{
        //    var tcs = new TaskCompletionSource<int>();

        //    process.Exited += (s, ea) => tcs.SetResult(process.ExitCode);
        //    process.OutputDataReceived += (s, ea) => Console.WriteLine(ea.Data);
        //    process.ErrorDataReceived += (s, ea) => Console.WriteLine("ERR: " + ea.Data);

        //    bool started = process.Start();
        //    if (!started)
        //    {
        //        //you may allow for the process to be re-used (started = false)
        //        //but I'm not sure about the guarantees of the Exited event in such a case
        //        throw new InvalidOperationException("Could not start process: " + process);
        //    }

        //    process.BeginOutputReadLine();
        //    process.BeginErrorReadLine();

        //    return tcs.Task;
        //}
    }
}