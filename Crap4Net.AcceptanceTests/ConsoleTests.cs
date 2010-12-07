using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Diagnostics;

namespace Crap4Net.AcceptanceTests
{

    
    [TestClass]
    public class ConsoleTests
    {
        string ErrorMessageUsage = "Please supply a valid Configuration File";
        
        [TestMethod]
        [DeploymentItem(@"Crap4Net-Console\App.Config")]
        public void SimpleActivations_GenerateAReport()
        {
            //copy necessary files
            System.IO.File.Copy("App.config", "Crap4Net-Console.config");

            Process target = new Process();
            target.StartInfo.FileName = "Crap4Net-Console.exe";
 
            target.Start();
            target.WaitForExit();
            Assert.AreEqual(0, target.ExitCode);

        }
    }
}
