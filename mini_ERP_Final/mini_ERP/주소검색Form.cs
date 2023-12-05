using Microsoft.Web.WebView2.Core;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TeamProject_test_v1
{
    public partial class 주소검색Form : Form
    {
        /// <summary>
        /// 반환할 우편코드와 주소
        /// </summary>
        public string gstrZipCode = "";
        public string gstrAddress1 = "";

        public 주소검색Form()
        {
            InitializeComponent();
        }

        private void 주소검색Form_Load(object sender, EventArgs e)
        {
            InitBrowser();
        }
        private async Task Initizated()
        {
            await webView21.EnsureCoreWebView2Async(null);
        }

        public async void InitBrowser()
        {
            await Initizated();
            webView21.CoreWebView2.Navigate("https://pkminsu.github.io/testtesttest/addresstest.html");
        }

        private int count = 0;

        private void webView21_WebMessageReceived(object sender, CoreWebView2WebMessageReceivedEventArgs e)
        {
            string address1 = e.TryGetWebMessageAsString();
            gstrAddress1 = address1.Substring(0, address1.Length - 5);
            gstrZipCode = address1.Substring(address1.Length - 5);
            this.Close();
        }
    }
}
