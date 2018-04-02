using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Utility.Events
{
    public interface IEvent<T>
    {
        Action<T> Action { get; set; }
    }
}
