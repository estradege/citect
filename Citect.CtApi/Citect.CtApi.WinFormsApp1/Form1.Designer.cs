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
            this.btLogin = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.btGetPriv = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btLogin
            // 
            this.btLogin.Location = new System.Drawing.Point(12, 12);
            this.btLogin.Name = "btLogin";
            this.btLogin.Size = new System.Drawing.Size(75, 23);
            this.btLogin.TabIndex = 0;
            this.btLogin.Text = "Login";
            this.btLogin.UseVisualStyleBackColor = true;
            this.btLogin.Click += new System.EventHandler(this.btLogin_Click);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(12, 41);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 1;
            this.button1.Text = "UserInfo";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // btGetPriv
            // 
            this.btGetPriv.Location = new System.Drawing.Point(12, 70);
            this.btGetPriv.Name = "btGetPriv";
            this.btGetPriv.Size = new System.Drawing.Size(75, 23);
            this.btGetPriv.TabIndex = 2;
            this.btGetPriv.Text = "GetPriv";
            this.btGetPriv.UseVisualStyleBackColor = true;
            this.btGetPriv.Click += new System.EventHandler(this.btGetPriv_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(93, 70);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 23);
            this.button2.TabIndex = 3;
            this.button2.Text = "button2";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.btGetPriv);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.btLogin);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);

        }

        #endregion

        private Button btLogin;
        private Button button1;
        private Button btGetPriv;
        private Button button2;
    }
}