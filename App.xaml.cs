using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Text.Json;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using System.Windows;

namespace WrathModdingHelper
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private static string? _DevEnv = null;
        private static string? _MSBuild = null;
        public static string DevEnv => _DevEnv ??= GetDevEnv();
        public static bool DevEnvValid => DevEnv.Length > 0;

        public static bool FindGameFolder(out string result)
        {
            string? maybeWrathPath = Environment.GetEnvironmentVariable("WrathPathDebug");
            if (maybeWrathPath != null && Directory.Exists(maybeWrathPath))
            {
                result = maybeWrathPath;
                return true;
            }

            maybeWrathPath = Environment.GetEnvironmentVariable("WrathPath");
            if (maybeWrathPath != null && Directory.Exists(maybeWrathPath))
            {
                result = maybeWrathPath;
                return true;
            }

            result = "";
            string gameFolder = "Pathfinder Second Adventure";
            string[] disks = new string[] { @"C:\", @"D:\", @"E:\", @"F:\" };
            string[] roots = new string[] { "Games", "Program files", "Program files (x86)", "" };
            string[] folders = new string[] { @"Steam\SteamApps\common", @"GoG Galaxy\Games", "" };

            foreach (var disk in disks)
            {
                foreach (var root in roots)
                {
                    foreach (var folder in folders)
                    {
                        var path = Path.Combine(disk, root);
                        path = Path.Combine(path, folder);
                        path = Path.Combine(path, gameFolder);
                        if (Directory.Exists(path))
                        {
                            result = path;
                            Environment.SetEnvironmentVariable("WrathPath", path, EnvironmentVariableTarget.User);
                            return true;
                        }
                    }
                }
            }
            return false;
        }
        private static string GetDevEnv()
        {
            var findDevEnv = VsWhere(start =>
            {
                start.ArgumentList.Add("-format");
                start.ArgumentList.Add("json");
                start.ArgumentList.Add("-requires");
                start.ArgumentList.Add("Microsoft.Net.Component.4.7.2.TargetingPack");
                start.ArgumentList.Add("-latest");
            });

            var vsInstalls = JsonDocument.Parse(findDevEnv).RootElement;

            if (vsInstalls.ValueKind != JsonValueKind.Array || vsInstalls.GetArrayLength() == 0)
            {
                Debug.WriteLine("Could not find valid visual studio install");
                return "";
            }

            var vsInstall = vsInstalls.EnumerateArray().First();
            bool ok = vsInstall.TryGetProperty("productPath", out var devenvJ);
            if (!ok)
            {
                Debug.WriteLine("Could not find devenv");
                return "";
            }

            {
                var findMsBuild = VsWhere(start =>
                {
                    start.ArgumentList.Add("-requires");
                    start.ArgumentList.Add("Microsoft.Component.MSBuild");
                    start.ArgumentList.Add("-latest");
                    start.ArgumentList.Add("-find");
                    start.ArgumentList.Add(@"MSBuild\**\Bin\MSBuild.exe");
                });
                _MSBuild = findMsBuild.Trim();
            }

            return devenvJ.GetString()!;
        }

        public static string MSBuild(params string[] args)
        {
            var msbuild = new Process();
            msbuild.StartInfo.FileName = _MSBuild!;
            msbuild.StartInfo.RedirectStandardOutput = true;
            msbuild.StartInfo.UseShellExecute = false;
            foreach (var arg in args)
                msbuild.StartInfo.ArgumentList.Add(arg);

            msbuild.Start();
            string output = msbuild.StandardOutput.ReadToEnd();
            msbuild.WaitForExit();
            return output;
        }

        private static string VsWhere(Action<ProcessStartInfo> custom)
        {
            var wd = System.AppDomain.CurrentDomain.BaseDirectory;

            var vswhere = new Process();
            vswhere.StartInfo.FileName = Path.Combine(wd, "vswhere.exe");
            vswhere.StartInfo.RedirectStandardOutput = true;
            vswhere.StartInfo.UseShellExecute = false;
            custom(vswhere.StartInfo);
            vswhere.Start();
            string output = vswhere.StandardOutput.ReadToEnd();
            vswhere.WaitForExit();
            return output;
        }

        protected override void OnActivated(EventArgs e)
        {
            base.OnActivated(e);

        }
    }
}
