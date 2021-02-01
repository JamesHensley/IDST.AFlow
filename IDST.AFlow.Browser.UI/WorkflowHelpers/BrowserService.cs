using CefSharp.WinForms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IDST.AFlow.Browser.UI.WorkflowHelpers
{
    public static class BrowserService
    {
        private static List<ChromiumWebBrowser> browserList;

        public static void RegisterBrowser(ChromiumWebBrowser browser)
        {
            if (browserList == null) { browserList = new List<ChromiumWebBrowser>(); }

            browser.Disposed += (object sender, EventArgs o) => {
                UnRegisterBrowser(browser.Handle);
            };
            browserList.Add(browser);
        }

        public static void UnRegisterBrowser(IntPtr browserHandle)
        {
            var bObj = browserList.FirstOrDefault(o => o.Handle == browserHandle);
            if (bObj != null)
            {
                System.Diagnostics.Debug.WriteLine($"Removing browser with handle: {browserHandle.ToInt64()}");
                browserList.Remove(bObj);
            }
        }

        public static ChromiumWebBrowser BrowserById(IntPtr browserHandle) {
            return browserList.FirstOrDefault(o => o.Handle == browserHandle);
        }
    }
}
