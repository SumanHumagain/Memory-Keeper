using Mixpanel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Memory_App.Services
{

    public class AnalyticsService
    {
        private readonly MixpanelClient _mixpanelClient;

        public AnalyticsService(string apiKey)
        {
            _mixpanelClient = new MixpanelClient("be5b08d35d1dcfa6dd657f9e7e896ba6");
        }

        public void TrackEvent(string eventName, object properties)
        {
            _mixpanelClient.TrackAsync(eventName, properties);
        }
    }
}