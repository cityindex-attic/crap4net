using System;
using Microsoft.VisualStudio.CodeCoverage;
using System.Collections.Generic;

namespace Crap4Net.CoverageParsers
{
    public class VSCoverageParser : BaseCoverageParser
    {

        protected override void ParseDataFromXml(string reportFileName)
        {
            String CoveragePath = System.IO.Path.GetDirectoryName(reportFileName);
            CoverageInfoManager.ExePath = CoveragePath;
            CoverageInfoManager.SymPath = CoveragePath;
            CoverageInfo data = CoverageInfoManager.CreateInfoFromFile(reportFileName);
            CoverageDS ds = data.BuildDataSet(null);
            
            foreach (CoverageDSPriv.NamespaceTableRow namespaceRow in ds.NamespaceTable)
            {
                foreach (CoverageDSPriv.ClassRow classRow in namespaceRow.GetClassRows())
                {
                    foreach (CoverageDSPriv.MethodRow methodRow in classRow.GetMethodRows())
                    {
                        string methodName = CleanMethodName(methodRow.MethodName);
                        string typeName = classRow.ClassName;
                        int covargeValue = CalculateCoverageValue(methodRow);
                        var method = new CoverageDataEntry(typeName, methodName,covargeValue);
                        Data.Add(method);
                    }
                }
            }
        }
        private string CleanMethodName(string methodName)
        {
            return methodName.Split(new char[] { '(' })[0];
        }

        private static int CalculateCoverageValue(CoverageDSPriv.MethodRow methodRow)
        {
            return (100 * (int)methodRow.BlocksCovered) / (int)(methodRow.BlocksCovered + methodRow.BlocksNotCovered);
        }
    }
}
