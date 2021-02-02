using CefSharp;
using CefSharp.WinForms;
using IDST.AFlow.Browser.UI.Forms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IDST.AFlow.Browser.UI.WorkflowHelpers
{
    public static class BrowserService
    {
        private static readonly List<BrowserRecord> browserList = new List<BrowserRecord>();

        public static void RegisterBrowser(BrowserRecord record)
        {
            browserList.Add(record);
            record.BrowserForm.Disposed += UnRegisterBrowser;
        }

        public static void UnRegisterBrowser(object sender, EventArgs o)
        {
            browserList.Clear();
            //var browserHandle = (sender as ChromiumWebBrowser).Handle;
            //browserList.RemoveAll(o => o.Key == browserHandle);
        }

        public static BrowserRecord BrowserById(IntPtr browserHandle)
        {
            return browserList.FirstOrDefault(o => o.BrowserHandle == browserHandle);
        }

        public static void DataLoaded(ResourceType resType, IFrame frame, string url, object data)
        {
            //System.Diagnostics.Debug.WriteLine($"BrowserService.DataLoaded: {resType} {frame.Url} {url}");
        }
    }
}
