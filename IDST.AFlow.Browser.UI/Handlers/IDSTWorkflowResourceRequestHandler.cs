using CefSharp;
using CefSharp.Handler;
using CefSharp.ResponseFilter;
using IDST.AFlow.Browser.UI.WorkflowHelpers;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace IDST.AFlow.Browser.UI.Handlers
{
    public class IDSTWorkflowResourceRequestHandler : ResourceRequestHandler
    {
        private MemoryStream memoryStream;


        protected override IResponseFilter GetResourceResponseFilter(IWebBrowser chromiumWebBrowser, IBrowser browser, IFrame frame, IRequest request, IResponse response)
        {
            memoryStream = new MemoryStream();
            return new StreamResponseFilter(memoryStream);
        }


        protected override void OnResourceLoadComplete(IWebBrowser chromiumWebBrowser, IBrowser browser, IFrame frame, IRequest request, IResponse response, UrlRequestStatus status, long receivedContentLength)
        {
            var url = new Uri(request.Url);
            if (memoryStream != null)
            {
                var data = memoryStream.ToArray();
                var dataLength = data.Length;
                //NOTE: You may need to use a different encoding depending on the request
                var dataAsUtf8String = Encoding.UTF8.GetString(data);

                try
                {
                    BrowserService.DataLoaded(chromiumWebBrowser.GetMainFrame(), url.AbsoluteUri, dataAsUtf8String.Substring(0, (dataLength > 50 ? 50 : dataLength)));
                }
                catch (System.Exception ex)
                {
                    //IF object is disposed, ignore
                    System.Diagnostics.Debug.WriteLine($"Explosion: {ex.Message}");
                }
            }
        }


        protected override void Dispose()
        {
            memoryStream?.Dispose();
            memoryStream = null;

            base.Dispose();
        }
    }
}
