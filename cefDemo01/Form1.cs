using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using CefSharp;
using CefSharp.WinForms;
using System.Runtime.InteropServices;

namespace cefDemo01
{
    public partial class Form1 : Form
    {
        public ChromiumWebBrowser browser { get; set; }
        public ChromiumWebBrowser chromeBrowser;
        public Form1()
        {
            InitializeComponent();
            
            //browser = new ChromiumWebBrowser("www.baidu.com");
            //this.Controls.Add(browser);
            InitializeChromium();
            //需要添加此句代码，否则下面执行会报错
            CefSharpSettings.LegacyJavascriptBindingEnabled = true;

            //注册usbjs对象
            //chromeBrowser.RegisterJsObject("usbKey", new UsbKeyBound());
            chromeBrowser.RegisterJsObject("cefCust", new CefCustomObject(chromeBrowser,this));

            //注意在js函数里面只驼峰写法开头，如果要使用C#写法就使用下面的注册方式。
            //BindingOptions bo = new BindingOptions();
            //bo.CamelCaseJavascriptNames = false;
            //browser.RegisterJsObject("usbKey", new UsbKeyBound(), bo);

        }

        public void InitializeChromium()
        {
            CefSettings settings = new CefSettings();

            // Note that if you get an error or a white screen, you may be doing something wrong !
            // Try to load a local file that you're sure that exists and give the complete path instead to test
            // for example, replace page with a direct path instead :
            // String page = @"C:\Users\SDkCarlos\Desktop\afolder\index.html";

            String page = string.Format(@"{0}\html_resources\html\index.html", Application.StartupPath);
            //String page = @"C:\Users\SDkCarlos\Desktop\artyom-HOMEPAGE\index.html";

            if (!File.Exists(page))
            {
                MessageBox.Show("Error The html file doesn't exists : " + page);
            }

            // Initialize cef with the provided settings
            Cef.Initialize(settings);
            // Create a browser component
            chromeBrowser = new ChromiumWebBrowser(page);

            // Add it to the form and fill it to the form window.
            this.Controls.Add(chromeBrowser);
            chromeBrowser.Dock = DockStyle.Fill;

            // Allow the use of local resources in the browser
            BrowserSettings browserSettings = new BrowserSettings();
            browserSettings.FileAccessFromFileUrls = CefState.Enabled;
            browserSettings.UniversalAccessFromFileUrls = CefState.Enabled;
            chromeBrowser.BrowserSettings = browserSettings;
        }


        private void button1_Click(object sender, EventArgs e)
        {
            Cef.Shutdown();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            //此句代码执行有错，官网示例的跑不起来
            //chromeBrowser.ShowDevTools();
        }
    }
}
