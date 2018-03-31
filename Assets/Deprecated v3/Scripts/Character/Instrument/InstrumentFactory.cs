using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Character.Instrument
{
    public class InstrumentFactory
    {
        private static Dictionary<string, Type> _dictionary = new Dictionary<string, Type>();

        public static void Register<InstrumentType>(string name) where InstrumentType : Instrument
        {
            _dictionary.Add(name, typeof(InstrumentType));
        }

        public static Instrument Create(string name)
        {
            return (Instrument)Activator.CreateInstance(_dictionary[name]);
        }
    }
}
