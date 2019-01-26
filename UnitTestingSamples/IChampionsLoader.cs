using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Linq;

namespace UnitTestingSamples
{
    public interface IChampionsLoader
    {
        XElement LoadChampions();
    }
}
