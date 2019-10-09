using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Umbraco.Core;
using Umbraco.Core.Composing;
using Umbraco.Core.Logging;
using Umbraco.Core.Services.Implement;

namespace UmbracoExtendingEvents.Web.Components //.Web.Components is merely to show that it is a component. Works without it too.
{
    [RuntimeLevel(MinLevel = RuntimeLevel.Run)]
    public class LogWhenPublishedComposer : ComponentComposer<LogWhenPublishedComponent>
    {
        //Nothing needed here - simply there to compose the component.
    }

    public class LogWhenPublishedComponent: IComponent
    {
        private readonly ILogger _logger;
        public LogWhenPublishedComponent(ILogger logger)
        {
            _logger = logger;
        }

        // Run once when Umbraco starts.
        public void Initialize()
        {
            //Do something whenever Umbraco starts up 
            // - ie. subscribe to an event.
            ContentService.Published += ContentService_Published;
            ContentService.Publishing += ContentService_Publishing;
        }

        private void ContentService_Publishing(Umbraco.Core.Services.IContentService sender, Umbraco.Core.Events.ContentPublishingEventArgs e)
        {
            //e.Cancel = true; --------- Wold make it impossible to publish content
            foreach (var PublishedItem in e.PublishedEntities)
            {
                _logger.Info<LogWhenPublishedComponent>(PublishedItem.Name + " Is about to be published.");
            }

        }

        private void ContentService_Published(Umbraco.Core.Services.IContentService sender, Umbraco.Core.Events.ContentPublishedEventArgs e)
        {
            //Code that will be executed whenever content is published.
            foreach (var PublishedItem in e.PublishedEntities)
            {
                _logger.Info<LogWhenPublishedComponent>(PublishedItem.Name + " was published.");
            }
        }
                
        //Runs once when Umbraco stops
        public void Terminate()
        {
            //Do something when Umbraco Terminates.
        }
    }
}