using System;
using System.Collections.Generic;
using System.Text;

namespace UnitTestingSamples
{
    public class StringSample
    {
        private string _init;
        public StringSample(string init)
        {
            if (init is null) throw new ArgumentException(nameof(init));
            _init = init;
        }

        public string GetStringDemo(string first, string second)
        {
            if (string.IsNullOrEmpty(first)) throw new ArgumentException(nameof(first));
            if (second is null) throw new ArgumentException(nameof(second));
            if (second.Length > first.Length) throw new ArgumentException("second argument must not be larger then first");
            int startIndex = first.IndexOf(second);
            if (startIndex == -1) return $"{second} not found in {first}";
            if(startIndex < 5)
            {
                string result = first.Remove(startIndex, second.Length);
                return $"removed {second} from {first}: {result}";
            }
            return _init.ToUpperInvariant();
        }
    }
}
