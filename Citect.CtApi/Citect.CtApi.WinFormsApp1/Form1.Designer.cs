namespace WinFormsApp1
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            btLogin = new Button();
            btUserInfo = new Button();
            btGetPriv = new Button();
            backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            SuspendLayout();
            // 
            // btLogin
            // 
            btLogin.Location = new Point(12, 12);
            btLogin.Name = "btLogin";
            btLogin.Size = new Size(75, 23);
            btLogin.TabIndex = 0;
            btLogin.Text = "Login";
            btLogin.UseVisualStyleBackColor = true;
            btLogin.Click += btLogin_Click;
            // 
            // btUserInfo
            // 
            btUserInfo.Location = new Point(12, 41);
            btUserInfo.Name = "btUserInfo";
            btUserInfo.Size = new Size(75, 23);
            btUserInfo.TabIndex = 1;
            btUserInfo.Text = "UserInfo";
            btUserInfo.UseVisualStyleBackColor = true;
            btUserInfo.Click += btUserInfo_Click;
            // 
            // btGetPriv
            // 
            btGetPriv.Location = new Point(12, 70);
            btGetPriv.Name = "btGetPriv";
            btGetPriv.Size = new Size(75, 23);
            btGetPriv.TabIndex = 2;
            btGetPriv.Text = "GetPriv";
            btGetPriv.UseVisualStyleBackColor = true;
            btGetPriv.Click += btGetPriv_Click;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(btGetPriv);
            Controls.Add(btUserInfo);
            Controls.Add(btLogin);
            Name = "Form1";
            Text = "Form1";
            FormClosed += Form1_FormClosed;
            ResumeLayout(false);
        }

        #endregion

        private Button btLogin;
        private Button btUserInfo;
        private Button btGetPriv;
        private System.ComponentModel.BackgroundWorker backgroundWorker1;
    }
}