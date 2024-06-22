using Citect;

namespace WinFormsApp1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void btLogin_Click(object sender, EventArgs e)
        {
            using (var ctapi = new CtApi())
            {
                ctapi.Open("127.0.0.1", "engineer", "Citect");
                var result = ctapi.Login("engineer", "Citect");
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            using (var ctapi = new CtApi())
            {
                ctapi.Open("127.0.0.1", "engineer", "Citect");
                var result = ctapi.UserInfo(0);
                result = ctapi.UserInfo(1);
                result = ctapi.UserInfo(2);
                result = ctapi.UserInfo(3);
                result = ctapi.UserInfo(4);
                result = ctapi.UserInfo(5);
                result = ctapi.UserInfo(6);
            }
        }

        private async void btGetPriv_Click(object sender, EventArgs e)
        {
            try
            {
                using (var ctapi = new CtApi())
                {
                    //ctapi.Open("127.0.0.1", "engineer", "Citect");
                    //ctapi.Open("127.0.0.1", "AlarmViewer", "AlarmViewer");
                    await ctapi.OpenAsync();
                    var result = await ctapi.GetPrivAsync(1, 0);
                }
            }
            catch (Exception)
            {
            }
        }

        private async void button2_Click(object sender, EventArgs e)
        {
            try
            {
                using (var ctapi = new CtApi())
                {
                    ctapi.Open(null, null, null);
                    var result = await ctapi.GetPrivAsync(1, 0);
                }
            }
            catch (Exception)
            {
            }
        }
    }
}