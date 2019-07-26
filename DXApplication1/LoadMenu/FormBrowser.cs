using System;
using System.Windows.Forms;
using DevExpress.XtraEditors;

namespace LoadMenu
{
    public partial class FormBrowser : XtraForm
    {
        public FormBrowser()
        {
            InitializeComponent();
        }

        public string Url { get; set; }

        private void FormBrowser_Load(object sender,
                                      EventArgs e)
        {
            webBrowser1.Navigate(Url);
        }

        private void FormBrowser_FormClosing(object sender,
                                             FormClosingEventArgs e)
        {
            webBrowser1.Stop();
        }

        public void Stop()
        {
            webBrowser1.Stop();
        }
    }
}
