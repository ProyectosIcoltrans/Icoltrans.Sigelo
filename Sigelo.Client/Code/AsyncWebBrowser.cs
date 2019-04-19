using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Navigation;

namespace Icoltrans.Sigelo.Win.Vehiculos.Code
{
    public class AsyncWebBrowser
    {
        protected WebBrowser m_WebBrowser;

        private ManualResetEvent m_MRE = new ManualResetEvent(false);

        internal event EventHandler<LoadCompletedEventHandler> CargaMapa;

        public void SetBrowser(WebBrowser browser, LoadCompletedEventHandler evento )
        {
            this.m_WebBrowser = browser;
            browser.LoadCompleted += evento; //new LoadCompletedEventHandler(WebBrowser_LoadCompleted);
        }

        public Task NavigateAsync(string url)
        {
            Navigate(url);

            return Task.Factory.StartNew((Action)(() =>
            {
                m_MRE.WaitOne();
                m_MRE.Reset();
            }));
        }

        public void Navigate(string url)
        {
            m_WebBrowser.Navigate(new Uri(url));
        }

        public Task InvokeScript(string placa)
        {
            m_WebBrowser.InvokeScript("showInMap", new object[] { placa, "flota" });
            return Task.Factory.StartNew((Action)(() =>
            {
                m_MRE.WaitOne();
                m_MRE.Reset();
            }));
        }

        void WebBrowser_LoadCompleted(object sender, NavigationEventArgs e)
        {
            m_MRE.Set();
        }
    }
}
