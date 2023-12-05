namespace TeamProject_test_v1
{
    partial class MainForm
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
            treeViewMenu = new TreeView();
            panel1 = new Panel();
            안읽은쪽지여부_라벨 = new Label();
            button쪽지 = new Button();
            button급여 = new Button();
            panel4 = new Panel();
            label4 = new Label();
            panel2 = new Panel();
            mailNotification_Label = new Label();
            button퇴근 = new Button();
            buttonLogout = new Button();
            labelName = new Label();
            backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            panel1.SuspendLayout();
            panel4.SuspendLayout();
            panel2.SuspendLayout();
            SuspendLayout();
            // 
            // treeViewMenu
            // 
            treeViewMenu.Location = new Point(0, 104);
            treeViewMenu.Name = "treeViewMenu";
            treeViewMenu.Size = new Size(150, 670);
            treeViewMenu.TabIndex = 0;
            treeViewMenu.BeforeSelect += treeViewMenu_BeforeSelect;
            treeViewMenu.AfterSelect += treeViewMenu_AfterSelect;
            // 
            // panel1
            // 
            panel1.BackColor = Color.FromArgb(224, 224, 224);
            panel1.Controls.Add(안읽은쪽지여부_라벨);
            panel1.Controls.Add(button쪽지);
            panel1.Controls.Add(button급여);
            panel1.Controls.Add(panel4);
            panel1.Controls.Add(treeViewMenu);
            panel1.Location = new Point(0, 0);
            panel1.Name = "panel1";
            panel1.Size = new Size(150, 850);
            panel1.TabIndex = 1;
            // 
            // 안읽은쪽지여부_라벨
            // 
            안읽은쪽지여부_라벨.AutoSize = true;
            안읽은쪽지여부_라벨.BackColor = Color.FromArgb(128, 128, 255);
            안읽은쪽지여부_라벨.Font = new Font("맑은 고딕", 12F, FontStyle.Bold, GraphicsUnit.Point);
            안읽은쪽지여부_라벨.ForeColor = Color.Red;
            안읽은쪽지여부_라벨.Location = new Point(45, 618);
            안읽은쪽지여부_라벨.Name = "안읽은쪽지여부_라벨";
            안읽은쪽지여부_라벨.Size = new Size(59, 28);
            안읽은쪽지여부_라벨.TabIndex = 6;
            안읽은쪽지여부_라벨.Text = "NEW";
            안읽은쪽지여부_라벨.Visible = false;
            // 
            // button쪽지
            // 
            button쪽지.BackColor = Color.FromArgb(128, 128, 255);
            button쪽지.Font = new Font("맑은 고딕", 10.2F, FontStyle.Bold, GraphicsUnit.Point);
            button쪽지.ForeColor = Color.White;
            button쪽지.Location = new Point(0, 609);
            button쪽지.Name = "button쪽지";
            button쪽지.Size = new Size(150, 84);
            button쪽지.TabIndex = 5;
            button쪽지.Text = "쪽지";
            button쪽지.UseVisualStyleBackColor = false;
            button쪽지.Click += button쪽지_Click;
            // 
            // button급여
            // 
            button급여.BackColor = Color.FromArgb(128, 128, 255);
            button급여.Font = new Font("맑은 고딕", 10.2F, FontStyle.Bold, GraphicsUnit.Point);
            button급여.ForeColor = Color.White;
            button급여.Location = new Point(0, 690);
            button급여.Name = "button급여";
            button급여.Size = new Size(150, 84);
            button급여.TabIndex = 4;
            button급여.Text = "급여내역서";
            button급여.UseVisualStyleBackColor = false;
            button급여.Click += button급여_Click;
            // 
            // panel4
            // 
            panel4.BackColor = Color.FromArgb(192, 192, 255);
            panel4.Controls.Add(label4);
            panel4.Location = new Point(0, 49);
            panel4.Name = "panel4";
            panel4.Size = new Size(150, 55);
            panel4.TabIndex = 3;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Font = new Font("맑은 고딕", 12F, FontStyle.Bold, GraphicsUnit.Point);
            label4.ForeColor = SystemColors.ButtonHighlight;
            label4.Location = new Point(12, 9);
            label4.Name = "label4";
            label4.Size = new Size(73, 28);
            label4.TabIndex = 3;
            label4.Text = "MENU";
            // 
            // panel2
            // 
            panel2.BackColor = Color.FromArgb(128, 128, 255);
            panel2.BorderStyle = BorderStyle.FixedSingle;
            panel2.Controls.Add(mailNotification_Label);
            panel2.Controls.Add(button퇴근);
            panel2.Controls.Add(buttonLogout);
            panel2.Controls.Add(labelName);
            panel2.ForeColor = SystemColors.ControlText;
            panel2.Location = new Point(0, 0);
            panel2.Name = "panel2";
            panel2.Size = new Size(1429, 51);
            panel2.TabIndex = 2;
            // 
            // mailNotification_Label
            // 
            mailNotification_Label.AutoSize = true;
            mailNotification_Label.Location = new Point(465, 14);
            mailNotification_Label.Name = "mailNotification_Label";
            mailNotification_Label.Size = new Size(0, 20);
            mailNotification_Label.TabIndex = 12;
            // 
            // button퇴근
            // 
            button퇴근.BackColor = Color.FromArgb(255, 128, 128);
            button퇴근.Font = new Font("맑은 고딕", 9F, FontStyle.Bold, GraphicsUnit.Point);
            button퇴근.ForeColor = Color.White;
            button퇴근.Location = new Point(992, 8);
            button퇴근.Name = "button퇴근";
            button퇴근.Size = new Size(50, 29);
            button퇴근.TabIndex = 11;
            button퇴근.Text = "퇴근";
            button퇴근.UseVisualStyleBackColor = false;
            button퇴근.Click += button퇴근_Click;
            // 
            // buttonLogout
            // 
            buttonLogout.BackColor = Color.Gray;
            buttonLogout.FlatStyle = FlatStyle.System;
            buttonLogout.Font = new Font("맑은 고딕", 9F, FontStyle.Bold, GraphicsUnit.Point);
            buttonLogout.Location = new Point(1308, 8);
            buttonLogout.Name = "buttonLogout";
            buttonLogout.Size = new Size(94, 29);
            buttonLogout.TabIndex = 10;
            buttonLogout.Text = "LOGOUT";
            buttonLogout.UseVisualStyleBackColor = false;
            buttonLogout.Click += buttonLogout_Click;
            // 
            // labelName
            // 
            labelName.Font = new Font("맑은 고딕", 9F, FontStyle.Bold, GraphicsUnit.Point);
            labelName.ForeColor = Color.White;
            labelName.Location = new Point(964, 10);
            labelName.Name = "labelName";
            labelName.RightToLeft = RightToLeft.No;
            labelName.Size = new Size(338, 25);
            labelName.TabIndex = 8;
            labelName.TextAlign = ContentAlignment.MiddleRight;
            // 
            // MainForm
            // 
            AutoScaleDimensions = new SizeF(9F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            AutoSizeMode = AutoSizeMode.GrowAndShrink;
            ClientSize = new Size(1431, 774);
            Controls.Add(panel2);
            Controls.Add(panel1);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            IsMdiContainer = true;
            Name = "MainForm";
            Text = "메인화면";
            FormClosing += Form1_FormClosing;
            Load += MainForm_Load;
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            panel4.ResumeLayout(false);
            panel4.PerformLayout();
            panel2.ResumeLayout(false);
            panel2.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private TreeView treeViewMenu;
        private Panel panel1;
        private Panel panel2;
        private Panel panel4;
        private Label label4;
        private System.ComponentModel.BackgroundWorker backgroundWorker1;
        private Button buttonLogout;
        private Label labelName;
        private Button button급여;
        private Button button쪽지;
        private Button button퇴근;
        private Label mailNotification_Label;
        private Label 안읽은쪽지여부_라벨;
    }
}