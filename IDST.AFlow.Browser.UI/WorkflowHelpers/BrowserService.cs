using CefSharp;
using CefSharp.WinForms;
using IDST.AFlow.Browser.UI.Forms;
using System;
using System.Buffers.Text;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;

namespace IDST.AFlow.Browser.UI.WorkflowHelpers
{
    public static class BrowserService
    {
        private static BinaryWriter _myWriter;
        private static BinaryFormatter _formatter;
        private static MemoryStream _memStream;

        private static BinaryWriter myWriter {
            get {
                //Media chunk
                if (_myWriter == null)
                {
                    _myWriter = new BinaryWriter(File.Open(".\\chunk.dat", FileMode.Create, FileAccess.Write, FileShare.None));
                    _formatter = new BinaryFormatter();
                    _memStream = new MemoryStream();
                }
                return _myWriter;
            }
        }

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
            if (url.ToLower().EndsWith(".ts"))
            {
                var valueBytes = Convert.FromBase64String(data.ToString());
                myWriter.Write(valueBytes);
            }
            else if (url.ToLower().Contains("googlevideo.com/videoplayback?")) {
                //myWriter.Write(ObjectToByteArray(data));
            }
        }

        private static byte[] ObjectToByteArray(object obj)
        {
            _formatter.Serialize(_memStream, obj);
            return _memStream.ToArray();
        }
    }
}
