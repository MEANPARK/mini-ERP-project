namespace TeamProject_test_v1
{
    partial class LoginForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
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
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            panel1 = new Panel();
            panelLogin = new Panel();
            checkBoxRemember = new CheckBox();
            buttonLogin = new Button();
            labelMain = new Label();
            textBoxPW = new TextBox();
            textBoxID = new TextBox();
            panel1.SuspendLayout();
            panelLogin.SuspendLayout();
            SuspendLayout();
            // 
            // panel1
            // 
            panel1.BackColor = Color.FromArgb(128, 128, 255);
            panel1.Controls.Add(panelLogin);
            panel1.Dock = DockStyle.Fill;
            panel1.Location = new Point(0, 0);
            panel1.Name = "panel1";
            panel1.Size = new Size(800, 502);
            panel1.TabIndex = 0;
            // 
            // panelLogin
            // 
            panelLogin.BackColor = Color.White;
            panelLogin.Controls.Add(checkBoxRemember);
            panelLogin.Controls.Add(buttonLogin);
            panelLogin.Controls.Add(labelMain);
            panelLogin.Controls.Add(textBoxPW);
            panelLogin.Controls.Add(textBoxID);
            panelLogin.Location = new Point(199, 93);
            panelLogin.Name = "panelLogin";
            panelLogin.Size = new Size(402, 321);
            panelLogin.TabIndex = 0;
            // 
            // checkBoxRemember
            // 
            checkBoxRemember.AutoSize = true;
            checkBoxRemember.Font = new Font("맑은 고딕", 7.8F, FontStyle.Regular, GraphicsUnit.Point);
            checkBoxRemember.Location = new Point(80, 161);
            checkBoxRemember.Name = "checkBoxRemember";
            checkBoxRemember.Size = new Size(131, 21);
            checkBoxRemember.TabIndex = 1;
            checkBoxRemember.Text = "로그인 정보 기억";
            checkBoxRemember.UseVisualStyleBackColor = true;
            checkBoxRemember.CheckedChanged += checkBoxRemember_CheckedChanged;
            // 
            // buttonLogin
            // 
            buttonLogin.BackColor = Color.FromArgb(192, 192, 255);
            buttonLogin.FlatStyle = FlatStyle.Flat;
            buttonLogin.Font = new Font("맑은 고딕", 10.2F, FontStyle.Bold, GraphicsUnit.Point);
            buttonLogin.ForeColor = Color.White;
            buttonLogin.Location = new Point(116, 231);
            buttonLogin.Name = "buttonLogin";
            buttonLogin.Size = new Size(171, 34);
            buttonLogin.TabIndex = 1;
            buttonLogin.Text = "LOGIN";
            buttonLogin.UseVisualStyleBackColor = false;
            buttonLogin.Click += buttonLogin_Click;
            // 
            // labelMain
            // 
            labelMain.AutoSize = true;
            labelMain.Font = new Font("문체부 궁체 흘림체", 18F, FontStyle.Regular, GraphicsUnit.Point);
            labelMain.Location = new Point(168, 24);
            labelMain.Name = "labelMain";
            labelMain.Size = new Size(66, 30);
            labelMain.TabIndex = 9;
            labelMain.Text = "Login";
            // 
            // textBoxPW
            // 
            textBoxPW.BackColor = SystemColors.Window;
            textBoxPW.Location = new Point(80, 123);
            textBoxPW.Name = "textBoxPW";
            textBoxPW.PasswordChar = '*';
            textBoxPW.PlaceholderText = "***********";
            textBoxPW.Size = new Size(244, 27);
            textBoxPW.TabIndex = 8;
            // 
            // textBoxID
            // 
            textBoxID.BackColor = SystemColors.Window;
            textBoxID.Location = new Point(80, 83);
            textBoxID.Name = "textBoxID";
            textBoxID.PlaceholderText = "Username";
            textBoxID.Size = new Size(244, 27);
            textBoxID.TabIndex = 7;
            // 
            // LoginForm
            // 
            AutoScaleDimensions = new SizeF(9F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 502);
            Controls.Add(panel1);
            Name = "LoginForm";
            Text = "LoginForm";
            FormClosed += LoginForm_FormClosed;
            panel1.ResumeLayout(false);
            panelLogin.ResumeLayout(false);
            panelLogin.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private Panel panel1;
        private Panel panelLogin;
        private TextBox textBoxPW;
        private TextBox textBoxID;
        private Button buttonLogin;
        private Label labelMain;
        private CheckBox checkBoxRemember;
    }
}