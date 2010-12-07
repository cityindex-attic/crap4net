using System;
using System.IO;
using System.Reflection;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace MSBuild.Tasks.AcceptanceTests
{
    [TestClass]
    public class CalculateCrap4JsMetricsTests
    {
        [TestMethod]
        public void CanGenerateJSComplexity()
        {
            StringAssert.Contains(RunMSBuild("jsbuild.xml /t:CalculateCrap4JSMetrics"),"0 Error(s)");
        }

        private static string RunMSBuild(string args)
        {
            var msbuild = new System.Diagnostics.Process();
            var currentDirectory = Path.GetDirectoryName(Assembly.GetAssembly(typeof(CalculateCrap4JsMetricsTests)).Location);
            msbuild.StartInfo = new System.Diagnostics.ProcessStartInfo { 
                                                                            Arguments = args, FileName = "msbuild.exe",
                                                                            WorkingDirectory = currentDirectory,
                                                                            CreateNoWindow=true, UseShellExecute=false, RedirectStandardError = true, RedirectStandardOutput = true };

            Console.WriteLine("====\nExecuting '{0} {1}' in '{2}'\n====\n", msbuild.StartInfo.FileName, msbuild.StartInfo.Arguments, msbuild.StartInfo.WorkingDirectory);
            msbuild.Start();

            var output = msbuild.StandardOutput.ReadToEnd() + msbuild.StandardError.ReadToEnd();
            Console.WriteLine(output);
            return output;
        }
    }
}
