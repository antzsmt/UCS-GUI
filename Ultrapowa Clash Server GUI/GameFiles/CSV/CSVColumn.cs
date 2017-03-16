using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace UCS.GameFiles
{
    class CSVColumn
    {
        private List<string> m_vValues;

        public CSVColumn()
        {
            this.m_vValues = new List<string>();
        }

        public void Add(string value)
        {
            // if (value == string.Empty)
            // m_vValues.Add(m_vValues.Last());
            // else
            this.m_vValues.Add(value);
        }

        public string Get(int row)
        {
            return this.m_vValues[row];
        }

        public int GetSize()
        {
            return this.m_vValues.Count;
        }

        public int GetArraySize(int currentOffset, int nextOffset)
        {
            return nextOffset - currentOffset;
        }
    }

}
