using System;
using System.IO;
using System.Reflection;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace MSBuild.Tasks.AcceptanceTests
{
    /// <summary>
    /// Runs some checks to ensure the test run environment is setup correctly
    /// </summary>
    [TestClass]
    public class TestRunnerSetupChecks
    {
        [AssemblyInitialize]
        public static void TestAssembliesMustRunFromCompiledLocation(TestContext testContext)
        {
            var currentDirectory = Path.GetDirectoryName(Assembly.GetAssembly(typeof(TestRunnerSetupChecks)).Location);
            Assert.IsTrue(Directory.GetFiles(currentDirectory).Contains(Path.Combine(currentDirectory,"jsbuild.xml")), "The test assemblies must be run from the same folder that contains jsbuild.xml.  Check that Resharper Unit test option 'Shadow copy assemblies being tested' is NOT checked");
        }
    }
}
