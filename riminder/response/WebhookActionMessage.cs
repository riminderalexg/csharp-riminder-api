using System;
using System.Collections.Generic;

namespace riminder.response
{
    public class WebhookActionStage : IWebhookMessage
    {
        public string type;
        public string EventName
        {
            get { return type; }
        }
        public string message;
        public WebhookProfile profile;
        public WebhookFilter filter;
        string stage;
    }

    public class WebhookActionRating : IWebhookMessage
    {
        public string type;
        public string EventName
        {
            get {return type;}
        }
        public string message;
        public WebhookProfile profile;
        public WebhookFilter filter;
        int rating;
    }
}