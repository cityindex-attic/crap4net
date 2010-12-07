using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Xml.Linq;

namespace Crap4Net
{
    public abstract class BaseCoverageParser : ICoverageParser
    {
        #region ICoverageParser Members
        public IList<CoverageDataEntry> Data
        {
            get
            {
                if (_data == null)
                    throw new ApplicationException("Data was not loaded");
                return _data;
            }
            private set
            {
                _data = value;
            }
        }

        public void LoadData(string reportFileName)
        {
            if (!File.Exists(reportFileName))
            {
                throw new ArgumentException("Coverage report file was not found:" + reportFileName);
            }
            Data = new List<CoverageDataEntry>();
            ParseDataFromXml(reportFileName);
        }
        protected abstract void ParseDataFromXml(string reportFileName);

        private IList<CoverageDataEntry> _data;
        #endregion

    }
}
