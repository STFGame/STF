using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Alerts
{
    /// <summary>
    /// Interface that alerts behaviours of relevant states in other classes.
    /// </summary>
    public interface IAlert : IEventSystemHandler
    {
        void Inform(AlertValue alert);
    }
}