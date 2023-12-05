namespace TeamProject_test_v1
{
    partial class 부서목록
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
            DataGridViewCellStyle dataGridViewCellStyle1 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle2 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle3 = new DataGridViewCellStyle();
            groupBox1 = new GroupBox();
            delete_button = new Button();
            edit_button = new Button();
            make_button = new Button();
            dataGridView1 = new DataGridView();
            new_member_button = new Button();
            groupBox3 = new GroupBox();
            label1 = new Label();
            member_combobox = new ComboBox();
            newmember_textBox = new TextBox();
            groupBox2 = new GroupBox();
            newname_textBox = new TextBox();
            groupBox4 = new GroupBox();
            newcode_textBox = new TextBox();
            new_groupBox = new GroupBox();
            cancel_button1 = new Button();
            edit_groupbox = new GroupBox();
            cancel_button2 = new Button();
            edit_member_button = new Button();
            groupBox7 = new GroupBox();
            label2 = new Label();
            editmember_combobox = new ComboBox();
            editmember_textBox = new TextBox();
            groupBox8 = new GroupBox();
            editname_textBox = new TextBox();
            groupBox9 = new GroupBox();
            editcode_textBox = new TextBox();
            groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dataGridView1).BeginInit();
            groupBox3.SuspendLayout();
            groupBox2.SuspendLayout();
            groupBox4.SuspendLayout();
            new_groupBox.SuspendLayout();
            edit_groupbox.SuspendLayout();
            groupBox7.SuspendLayout();
            groupBox8.SuspendLayout();
            groupBox9.SuspendLayout();
            SuspendLayout();
            // 
            // groupBox1
            // 
            groupBox1.Controls.Add(delete_button);
            groupBox1.Controls.Add(edit_button);
            groupBox1.Controls.Add(make_button);
            groupBox1.Controls.Add(dataGridView1);
            groupBox1.Font = new Font("맑은 고딕", 20F, FontStyle.Bold, GraphicsUnit.World);
            groupBox1.Location = new Point(12, 5);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new Size(442, 703);
            groupBox1.TabIndex = 0;
            groupBox1.TabStop = false;
            groupBox1.Text = "부서 목록";
            // 
            // delete_button
            // 
            delete_button.Font = new Font("맑은 고딕", 10F, FontStyle.Regular, GraphicsUnit.Point);
            delete_button.Location = new Point(303, 647);
            delete_button.Name = "delete_button";
            delete_button.Size = new Size(133, 29);
            delete_button.TabIndex = 2;
            delete_button.Tag = "삭제";
            delete_button.Text = "삭제";
            delete_button.UseVisualStyleBackColor = true;
            delete_button.Click += operate_button_click;
            // 
            // edit_button
            // 
            edit_button.Font = new Font("맑은 고딕", 10F, FontStyle.Regular, GraphicsUnit.Point);
            edit_button.Location = new Point(156, 646);
            edit_button.Name = "edit_button";
            edit_button.Size = new Size(141, 29);
            edit_button.TabIndex = 2;
            edit_button.Tag = "수정";
            edit_button.Text = "수정";
            edit_button.UseVisualStyleBackColor = true;
            edit_button.Click += operate_button_click;
            // 
            // make_button
            // 
            make_button.BackColor = SystemColors.ControlLight;
            make_button.Font = new Font("맑은 고딕", 11F, FontStyle.Regular, GraphicsUnit.Point);
            make_button.Location = new Point(6, 646);
            make_button.Name = "make_button";
            make_button.Size = new Size(144, 29);
            make_button.TabIndex = 1;
            make_button.Tag = "등록";
            make_button.Text = "등록";
            make_button.UseVisualStyleBackColor = false;
            make_button.Click += operate_button_click;
            // 
            // dataGridView1
            // 
            dataGridView1.AllowUserToAddRows = false;
            dataGridView1.AllowUserToDeleteRows = false;
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dataGridViewCellStyle1.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.BackColor = SystemColors.Control;
            dataGridViewCellStyle1.Font = new Font("맑은 고딕", 9F, FontStyle.Regular, GraphicsUnit.Point);
            dataGridViewCellStyle1.ForeColor = SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = DataGridViewTriState.True;
            dataGridView1.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            dataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle2.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle2.BackColor = SystemColors.Window;
            dataGridViewCellStyle2.Font = new Font("맑은 고딕", 9F, FontStyle.Regular, GraphicsUnit.Point);
            dataGridViewCellStyle2.ForeColor = SystemColors.ControlText;
            dataGridViewCellStyle2.SelectionBackColor = SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = DataGridViewTriState.False;
            dataGridView1.DefaultCellStyle = dataGridViewCellStyle2;
            dataGridView1.Location = new Point(6, 33);
            dataGridView1.Name = "dataGridView1";
            dataGridView1.ReadOnly = true;
            dataGridViewCellStyle3.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle3.BackColor = SystemColors.Control;
            dataGridViewCellStyle3.Font = new Font("맑은 고딕", 20F, FontStyle.Bold, GraphicsUnit.World);
            dataGridViewCellStyle3.ForeColor = SystemColors.WindowText;
            dataGridViewCellStyle3.SelectionBackColor = SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = DataGridViewTriState.True;
            dataGridView1.RowHeadersDefaultCellStyle = dataGridViewCellStyle3;
            dataGridView1.RowHeadersVisible = false;
            dataGridView1.RowHeadersWidth = 51;
            dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridView1.Size = new Size(430, 594);
            dataGridView1.TabIndex = 0;
            // 
            // new_member_button
            // 
            new_member_button.Location = new Point(269, 314);
            new_member_button.Name = "new_member_button";
            new_member_button.Size = new Size(99, 39);
            new_member_button.TabIndex = 8;
            new_member_button.Tag = "추가";
            new_member_button.Text = "추가";
            new_member_button.UseVisualStyleBackColor = true;
            new_member_button.Click += operate_button_click;
            // 
            // groupBox3
            // 
            groupBox3.Controls.Add(label1);
            groupBox3.Controls.Add(member_combobox);
            groupBox3.Controls.Add(newmember_textBox);
            groupBox3.Location = new Point(6, 222);
            groupBox3.Name = "groupBox3";
            groupBox3.Size = new Size(362, 86);
            groupBox3.TabIndex = 7;
            groupBox3.TabStop = false;
            groupBox3.Text = "부서장 선택";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("맑은 고딕", 6F, FontStyle.Regular, GraphicsUnit.Point);
            label1.ForeColor = Color.Gray;
            label1.Location = new Point(3, 56);
            label1.Name = "label1";
            label1.Size = new Size(147, 12);
            label1.TabIndex = 11;
            label1.Text = "※ 사원명(사원번호 8자리) 형식";
            // 
            // member_combobox
            // 
            member_combobox.DropDownStyle = ComboBoxStyle.DropDownList;
            member_combobox.FormattingEnabled = true;
            member_combobox.IntegralHeight = false;
            member_combobox.Location = new Point(179, 25);
            member_combobox.Name = "member_combobox";
            member_combobox.Size = new Size(177, 28);
            member_combobox.TabIndex = 4;
            member_combobox.SelectedIndexChanged += member_combobox_SelectedIndexChanged;
            // 
            // newmember_textBox
            // 
            newmember_textBox.Enabled = false;
            newmember_textBox.Location = new Point(0, 26);
            newmember_textBox.Name = "newmember_textBox";
            newmember_textBox.Size = new Size(173, 27);
            newmember_textBox.TabIndex = 0;
            // 
            // groupBox2
            // 
            groupBox2.Controls.Add(newname_textBox);
            groupBox2.Location = new Point(6, 137);
            groupBox2.Name = "groupBox2";
            groupBox2.Size = new Size(362, 69);
            groupBox2.TabIndex = 6;
            groupBox2.TabStop = false;
            groupBox2.Text = "부서명 작성";
            // 
            // newname_textBox
            // 
            newname_textBox.Location = new Point(0, 26);
            newname_textBox.Name = "newname_textBox";
            newname_textBox.PlaceholderText = "창설부서명";
            newname_textBox.Size = new Size(356, 27);
            newname_textBox.TabIndex = 0;
            // 
            // groupBox4
            // 
            groupBox4.Controls.Add(newcode_textBox);
            groupBox4.Location = new Point(6, 54);
            groupBox4.Name = "groupBox4";
            groupBox4.Size = new Size(362, 69);
            groupBox4.TabIndex = 5;
            groupBox4.TabStop = false;
            groupBox4.Text = "부서코드";
            // 
            // newcode_textBox
            // 
            newcode_textBox.Enabled = false;
            newcode_textBox.Location = new Point(0, 26);
            newcode_textBox.Name = "newcode_textBox";
            newcode_textBox.ReadOnly = true;
            newcode_textBox.Size = new Size(356, 27);
            newcode_textBox.TabIndex = 0;
            // 
            // new_groupBox
            // 
            new_groupBox.Controls.Add(cancel_button1);
            new_groupBox.Controls.Add(groupBox4);
            new_groupBox.Controls.Add(new_member_button);
            new_groupBox.Controls.Add(groupBox2);
            new_groupBox.Controls.Add(groupBox3);
            new_groupBox.Location = new Point(488, 12);
            new_groupBox.Name = "new_groupBox";
            new_groupBox.Size = new Size(374, 369);
            new_groupBox.TabIndex = 9;
            new_groupBox.TabStop = false;
            new_groupBox.Text = "신규부서등록";
            new_groupBox.Visible = false;
            // 
            // cancel_button1
            // 
            cancel_button1.Location = new Point(162, 314);
            cancel_button1.Name = "cancel_button1";
            cancel_button1.Size = new Size(101, 39);
            cancel_button1.TabIndex = 5;
            cancel_button1.Tag = "취소";
            cancel_button1.Text = "취소";
            cancel_button1.UseVisualStyleBackColor = true;
            cancel_button1.Click += operate_button_click;
            // 
            // edit_groupbox
            // 
            edit_groupbox.Controls.Add(cancel_button2);
            edit_groupbox.Controls.Add(edit_member_button);
            edit_groupbox.Controls.Add(groupBox7);
            edit_groupbox.Controls.Add(groupBox8);
            edit_groupbox.Controls.Add(groupBox9);
            edit_groupbox.Location = new Point(892, 12);
            edit_groupbox.Name = "edit_groupbox";
            edit_groupbox.Size = new Size(358, 369);
            edit_groupbox.TabIndex = 10;
            edit_groupbox.TabStop = false;
            edit_groupbox.Text = "부서수정";
            edit_groupbox.Visible = false;
            // 
            // cancel_button2
            // 
            cancel_button2.Location = new Point(145, 314);
            cancel_button2.Name = "cancel_button2";
            cancel_button2.Size = new Size(101, 39);
            cancel_button2.TabIndex = 5;
            cancel_button2.Tag = "취소";
            cancel_button2.Text = "취소";
            cancel_button2.UseVisualStyleBackColor = true;
            cancel_button2.Click += operate_button_click;
            // 
            // edit_member_button
            // 
            edit_member_button.Location = new Point(252, 314);
            edit_member_button.Name = "edit_member_button";
            edit_member_button.Size = new Size(94, 39);
            edit_member_button.TabIndex = 12;
            edit_member_button.Tag = "수정완료";
            edit_member_button.Text = "수정";
            edit_member_button.UseVisualStyleBackColor = true;
            edit_member_button.Click += operate_button_click;
            // 
            // groupBox7
            // 
            groupBox7.Controls.Add(label2);
            groupBox7.Controls.Add(editmember_combobox);
            groupBox7.Controls.Add(editmember_textBox);
            groupBox7.Location = new Point(9, 222);
            groupBox7.Name = "groupBox7";
            groupBox7.Size = new Size(337, 86);
            groupBox7.TabIndex = 11;
            groupBox7.TabStop = false;
            groupBox7.Text = "부서장";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("맑은 고딕", 6F, FontStyle.Regular, GraphicsUnit.Point);
            label2.ForeColor = Color.Gray;
            label2.Location = new Point(4, 56);
            label2.Name = "label2";
            label2.Size = new Size(147, 12);
            label2.TabIndex = 12;
            label2.Text = "※ 사원명(사원번호 8자리) 형식";
            // 
            // editmember_combobox
            // 
            editmember_combobox.DropDownStyle = ComboBoxStyle.DropDownList;
            editmember_combobox.FormattingEnabled = true;
            editmember_combobox.IntegralHeight = false;
            editmember_combobox.Location = new Point(170, 26);
            editmember_combobox.Name = "editmember_combobox";
            editmember_combobox.Size = new Size(161, 28);
            editmember_combobox.TabIndex = 4;
            editmember_combobox.SelectedIndexChanged += editmember_combobox_SelectedIndexChanged;
            // 
            // editmember_textBox
            // 
            editmember_textBox.Enabled = false;
            editmember_textBox.Location = new Point(6, 26);
            editmember_textBox.Name = "editmember_textBox";
            editmember_textBox.Size = new Size(158, 27);
            editmember_textBox.TabIndex = 0;
            // 
            // groupBox8
            // 
            groupBox8.Controls.Add(editname_textBox);
            groupBox8.Location = new Point(9, 137);
            groupBox8.Name = "groupBox8";
            groupBox8.Size = new Size(337, 69);
            groupBox8.TabIndex = 10;
            groupBox8.TabStop = false;
            groupBox8.Text = "부서명";
            // 
            // editname_textBox
            // 
            editname_textBox.Location = new Point(0, 26);
            editname_textBox.Name = "editname_textBox";
            editname_textBox.Size = new Size(337, 27);
            editname_textBox.TabIndex = 0;
            // 
            // groupBox9
            // 
            groupBox9.Controls.Add(editcode_textBox);
            groupBox9.Location = new Point(9, 54);
            groupBox9.Name = "groupBox9";
            groupBox9.Size = new Size(337, 69);
            groupBox9.TabIndex = 9;
            groupBox9.TabStop = false;
            groupBox9.Text = "부서코드";
            // 
            // editcode_textBox
            // 
            editcode_textBox.Location = new Point(0, 26);
            editcode_textBox.Name = "editcode_textBox";
            editcode_textBox.Size = new Size(337, 27);
            editcode_textBox.TabIndex = 0;
            // 
            // 부서목록
            // 
            AutoScaleDimensions = new SizeF(9F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            AutoSizeMode = AutoSizeMode.GrowAndShrink;
            ClientSize = new Size(1280, 720);
            Controls.Add(edit_groupbox);
            Controls.Add(new_groupBox);
            Controls.Add(groupBox1);
            FormBorderStyle = FormBorderStyle.None;
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "부서목록";
            Text = "부서목록";
            Load += Form2_Load;
            groupBox1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)dataGridView1).EndInit();
            groupBox3.ResumeLayout(false);
            groupBox3.PerformLayout();
            groupBox2.ResumeLayout(false);
            groupBox2.PerformLayout();
            groupBox4.ResumeLayout(false);
            groupBox4.PerformLayout();
            new_groupBox.ResumeLayout(false);
            edit_groupbox.ResumeLayout(false);
            groupBox7.ResumeLayout(false);
            groupBox7.PerformLayout();
            groupBox8.ResumeLayout(false);
            groupBox8.PerformLayout();
            groupBox9.ResumeLayout(false);
            groupBox9.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private GroupBox groupBox1;
        private Button make_button;
        private DataGridView dataGridView1;
        private Button delete_button;
        private Button edit_button;
        private Button new_member_button;
        private GroupBox groupBox3;
        private ComboBox member_combobox;
        private TextBox newmember_textBox;
        private GroupBox groupBox2;
        private TextBox newname_textBox;
        private GroupBox groupBox4;
        private TextBox newcode_textBox;
        private GroupBox new_groupBox;
        private GroupBox edit_groupbox;
        private Button edit_member_button;
        private GroupBox groupBox7;
        private ComboBox editmember_combobox;
        private TextBox editmember_textBox;
        private GroupBox groupBox8;
        private TextBox editname_textBox;
        private GroupBox groupBox9;
        private TextBox editcode_textBox;
        private Button cancel_button1;
        private Button cancel_button2;
        private Label label1;
        private Label label2;
    }
}