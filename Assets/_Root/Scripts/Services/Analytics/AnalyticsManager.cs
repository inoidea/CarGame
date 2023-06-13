using Services.Analytics.UnityAnalytics;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Services.Analytics
{
    internal interface IAnalyticsManager
    {
        void SendLevelStartEvent();
    }

    internal class AnalyticsManager : MonoBehaviour, IAnalyticsManager
    {
        private IAnalyticsService[] _services;

        private void Awake()
        {
            _services = new IAnalyticsService[]
            {
                new UnityAnalyticsService()
            };
        }

        public void SendMainMenuOpenedEvent() =>
            SendEvent("MainMenuOpened");

        public void SendLevelStartEvent() =>
            SendEvent("LevelStart");

        private void SendEvent(string eventName)
        {
            foreach (IAnalyticsService service in _services)
                service.SendEvent(eventName);
        }

        private void SendEvent(string eventName, Dictionary<string, object> eventData)
        {
            foreach (IAnalyticsService service in _services)
                service.SendEvent(eventName, eventData);
        }
    }
}
