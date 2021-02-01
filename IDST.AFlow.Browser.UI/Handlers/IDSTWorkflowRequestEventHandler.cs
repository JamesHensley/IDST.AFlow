using CefSharp;
using CefSharp.Handler;
using System;
using System.Collections.Generic;
using System.Text;

namespace IDST.AFlow.Browser.UI.Handlers
{
    public class IDSTWorkflowRequestEventHandler : RequestHandler
    {
        private readonly Action<string, int?> openNewTab;

        public IDSTWorkflowRequestEventHandler(Action<string, int?> openNewTab)
        {
            this.openNewTab = openNewTab;
        }

        protected override IResourceRequestHandler GetResourceRequestHandler(IWebBrowser chromiumWebBrowser, IBrowser browser, IFrame frame, IRequest request, bool isNavigation, bool isDownload, string requestInitiator, ref bool disableDefaultHandling)
        {
            return new IDSTWorkflowResourceRequestHandler();
        }
    }
}
