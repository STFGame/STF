using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Character.Instrument.Profiles
{
    public class PlayStation4Profile : Instrument
    {
        public string[] Name = { "Wireless Controller" };

        public PlayStation4Profile(Instrument obj) : base(obj)
        {
            foreach (var name in obj.GetType().GetFields())
            {
                this.GetType().GetProperty(name.Name).SetValue(this, name.GetValue(obj), null);
            }
        }
    }
}
