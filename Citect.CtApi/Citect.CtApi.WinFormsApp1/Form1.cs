using Citect;

namespace WinFormsApp1
{
    public partial class Form1 : Form
    {
        private CtApi _ctApi = new CtApi();

        public Form1()
        {
            InitializeComponent();

            _ctApi.SetCtApiDirectory(@"C:\Program Files (x86)\AVEVA Plant SCADA\Bin\Bin (x64)");
            _ctApi.Open();
            //_ctApi.Open("127.0.0.1", "op", "op");
        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            _ctApi.Close();
        }

        private void btLogin_Click(object sender, EventArgs e)
        {
            var result = _ctApi.Login("op", "op");
        }

        private void btUserInfo_Click(object sender, EventArgs e)
        {
            var result = _ctApi.UserInfo(0);
            result = _ctApi.UserInfo(1);
            result = _ctApi.UserInfo(2);
            result = _ctApi.UserInfo(3);
            result = _ctApi.UserInfo(4);
            result = _ctApi.UserInfo(5);
            result = _ctApi.UserInfo(6);
        }

        private async void btGetPriv_Click(object sender, EventArgs e)
        {
            var result = await _ctApi.GetPrivAsync(1, 0);
        }
    }
}