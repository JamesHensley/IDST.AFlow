using CefSharp.WinForms;
using IDST.AFlow.Browser.UI.Forms;
using System;
using System.Collections.Generic;
using System.Text;

namespace IDST.AFlow.Browser.UI.WorkflowHelpers
{
    public class BrowserRecord
    {
        public BrowserTabUserControl BrowserForm;
        public ChromiumWebBrowser BrowserControl;
        public IntPtr BrowserHandle;
        public List<KeyValuePair<string,string>> pages;

        public BrowserRecord()
        {
            pages = new List<KeyValuePair<string, string>>();
        }
    }
}
