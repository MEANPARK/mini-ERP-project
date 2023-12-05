namespace TeamProject_test_v1
{
    partial class EmployeeUpdateForm
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
            buttonConfirm = new Button();
            buttonCencel = new Button();
            textBoxNum = new TextBox();
            labelNum = new Label();
            textBoxPhone = new TextBox();
            labelPhone = new Label();
            comboBoxDepartment = new ComboBox();
            labelDepartment = new Label();
            labelPosition = new Label();
            comboBoxPosition = new ComboBox();
            labelAddress = new Label();
            textBoxAddress1 = new TextBox();
            labelName = new Label();
            textBoxName = new TextBox();
            labelEmail = new Label();
            textBoxEmail = new TextBox();
            labelAge = new Label();
            textBoxAge = new TextBox();
            labelAccount = new Label();
            textBoxAccount = new TextBox();
            labelGender = new Label();
            buttonAdrSearch = new Button();
            dateTimePicker1 = new DateTimePicker();
            label1 = new Label();
            label11 = new Label();
            textBoxAddress3 = new TextBox();
            textBoxAddress2 = new TextBox();
            label2 = new Label();
            label9 = new Label();
            label3 = new Label();
            label4 = new Label();
            label5 = new Label();
            label6 = new Label();
            label7 = new Label();
            label8 = new Label();
            comboBoxGender = new ComboBox();
            SuspendLayout();
            // 
            // buttonConfirm
            // 
            buttonConfirm.Location = new Point(751, 464);
            buttonConfirm.Name = "buttonConfirm";
            buttonConfirm.Size = new Size(116, 29);
            buttonConfirm.TabIndex = 0;
            buttonConfirm.Text = "확인";
            buttonConfirm.UseVisualStyleBackColor = true;
            buttonConfirm.Click += buttonConfirm_Click;
            // 
            // buttonCencel
            // 
            buttonCencel.Location = new Point(883, 464);
            buttonCencel.Name = "buttonCencel";
            buttonCencel.Size = new Size(116, 29);
            buttonCencel.TabIndex = 1;
            buttonCencel.Text = "취소";
            buttonCencel.UseVisualStyleBackColor = true;
            buttonCencel.Click += buttonCencel_Click;
            // 
            // textBoxNum
            // 
            textBoxNum.Location = new Point(132, 82);
            textBoxNum.Name = "textBoxNum";
            textBoxNum.ReadOnly = true;
            textBoxNum.Size = new Size(197, 27);
            textBoxNum.TabIndex = 2;
            textBoxNum.TextAlign = HorizontalAlignment.Center;
            // 
            // labelNum
            // 
            labelNum.AutoSize = true;
            labelNum.Location = new Point(57, 85);
            labelNum.Name = "labelNum";
            labelNum.Size = new Size(69, 20);
            labelNum.TabIndex = 3;
            labelNum.Text = "사원번호";
            // 
            // textBoxPhone
            // 
            textBoxPhone.Location = new Point(456, 86);
            textBoxPhone.Name = "textBoxPhone";
            textBoxPhone.Size = new Size(197, 27);
            textBoxPhone.TabIndex = 4;
            textBoxPhone.TextAlign = HorizontalAlignment.Center;
            // 
            // labelPhone
            // 
            labelPhone.AutoSize = true;
            labelPhone.Location = new Point(381, 89);
            labelPhone.Name = "labelPhone";
            labelPhone.Size = new Size(69, 20);
            labelPhone.TabIndex = 5;
            labelPhone.Text = "전화번호";
            // 
            // comboBoxDepartment
            // 
            comboBoxDepartment.DropDownStyle = ComboBoxStyle.DropDownList;
            comboBoxDepartment.FormattingEnabled = true;
            comboBoxDepartment.Location = new Point(804, 166);
            comboBoxDepartment.Name = "comboBoxDepartment";
            comboBoxDepartment.Size = new Size(195, 28);
            comboBoxDepartment.TabIndex = 6;
            // 
            // labelDepartment
            // 
            labelDepartment.AutoSize = true;
            labelDepartment.Location = new Point(759, 170);
            labelDepartment.Name = "labelDepartment";
            labelDepartment.Size = new Size(39, 20);
            labelDepartment.TabIndex = 7;
            labelDepartment.Text = "부서";
            // 
            // labelPosition
            // 
            labelPosition.AutoSize = true;
            labelPosition.Location = new Point(759, 261);
            labelPosition.Name = "labelPosition";
            labelPosition.Size = new Size(39, 20);
            labelPosition.TabIndex = 9;
            labelPosition.Text = "직급";
            // 
            // comboBoxPosition
            // 
            comboBoxPosition.DropDownStyle = ComboBoxStyle.DropDownList;
            comboBoxPosition.FormattingEnabled = true;
            comboBoxPosition.Location = new Point(804, 258);
            comboBoxPosition.Name = "comboBoxPosition";
            comboBoxPosition.Size = new Size(195, 28);
            comboBoxPosition.TabIndex = 8;
            // 
            // labelAddress
            // 
            labelAddress.AutoSize = true;
            labelAddress.Location = new Point(381, 351);
            labelAddress.Name = "labelAddress";
            labelAddress.Size = new Size(69, 20);
            labelAddress.TabIndex = 13;
            labelAddress.Text = "우편번호";
            // 
            // textBoxAddress1
            // 
            textBoxAddress1.Location = new Point(456, 347);
            textBoxAddress1.Name = "textBoxAddress1";
            textBoxAddress1.ReadOnly = true;
            textBoxAddress1.Size = new Size(117, 27);
            textBoxAddress1.TabIndex = 12;
            textBoxAddress1.TextAlign = HorizontalAlignment.Center;
            // 
            // labelName
            // 
            labelName.AutoSize = true;
            labelName.Location = new Point(87, 169);
            labelName.Name = "labelName";
            labelName.Size = new Size(39, 20);
            labelName.TabIndex = 11;
            labelName.Text = "이름";
            // 
            // textBoxName
            // 
            textBoxName.Location = new Point(132, 170);
            textBoxName.Name = "textBoxName";
            textBoxName.ReadOnly = true;
            textBoxName.Size = new Size(197, 27);
            textBoxName.TabIndex = 10;
            textBoxName.TextAlign = HorizontalAlignment.Center;
            // 
            // labelEmail
            // 
            labelEmail.AutoSize = true;
            labelEmail.Location = new Point(396, 173);
            labelEmail.Name = "labelEmail";
            labelEmail.Size = new Size(54, 20);
            labelEmail.TabIndex = 17;
            labelEmail.Text = "이메일";
            // 
            // textBoxEmail
            // 
            textBoxEmail.Location = new Point(456, 170);
            textBoxEmail.Name = "textBoxEmail";
            textBoxEmail.Size = new Size(197, 27);
            textBoxEmail.TabIndex = 16;
            textBoxEmail.TextAlign = HorizontalAlignment.Center;
            // 
            // labelAge
            // 
            labelAge.AutoSize = true;
            labelAge.Location = new Point(57, 261);
            labelAge.Name = "labelAge";
            labelAge.Size = new Size(69, 20);
            labelAge.TabIndex = 15;
            labelAge.Text = "생년월일";
            // 
            // textBoxAge
            // 
            textBoxAge.Location = new Point(132, 258);
            textBoxAge.Name = "textBoxAge";
            textBoxAge.ReadOnly = true;
            textBoxAge.Size = new Size(197, 27);
            textBoxAge.TabIndex = 14;
            textBoxAge.TextAlign = HorizontalAlignment.Center;
            // 
            // labelAccount
            // 
            labelAccount.AutoSize = true;
            labelAccount.Location = new Point(381, 262);
            labelAccount.Name = "labelAccount";
            labelAccount.Size = new Size(69, 20);
            labelAccount.TabIndex = 21;
            labelAccount.Text = "계좌번호";
            // 
            // textBoxAccount
            // 
            textBoxAccount.Location = new Point(456, 259);
            textBoxAccount.Name = "textBoxAccount";
            textBoxAccount.Size = new Size(197, 27);
            textBoxAccount.TabIndex = 20;
            textBoxAccount.TextAlign = HorizontalAlignment.Center;
            // 
            // labelGender
            // 
            labelGender.AutoSize = true;
            labelGender.Location = new Point(87, 350);
            labelGender.Name = "labelGender";
            labelGender.Size = new Size(39, 20);
            labelGender.TabIndex = 19;
            labelGender.Text = "성별";
            // 
            // buttonAdrSearch
            // 
            buttonAdrSearch.Location = new Point(581, 347);
            buttonAdrSearch.Name = "buttonAdrSearch";
            buttonAdrSearch.Size = new Size(72, 29);
            buttonAdrSearch.TabIndex = 22;
            buttonAdrSearch.Text = "검색";
            buttonAdrSearch.UseVisualStyleBackColor = true;
            buttonAdrSearch.Click += buttonAdrSearch_Click;
            // 
            // dateTimePicker1
            // 
            dateTimePicker1.Location = new Point(132, 225);
            dateTimePicker1.Name = "dateTimePicker1";
            dateTimePicker1.Size = new Size(197, 27);
            dateTimePicker1.TabIndex = 25;
            dateTimePicker1.ValueChanged += dateTimePicker1_ValueChanged;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("맑은 고딕", 7.20000029F, FontStyle.Regular, GraphicsUnit.Point);
            label1.ForeColor = SystemColors.ControlDarkDark;
            label1.Location = new Point(456, 66);
            label1.Name = "label1";
            label1.Size = new Size(134, 17);
            label1.TabIndex = 24;
            label1.Text = "※ 010-0000-000 형식";
            // 
            // label11
            // 
            label11.AutoSize = true;
            label11.Location = new Point(381, 416);
            label11.Name = "label11";
            label11.Size = new Size(69, 20);
            label11.TabIndex = 66;
            label11.Text = "상세주소";
            // 
            // textBoxAddress3
            // 
            textBoxAddress3.Location = new Point(456, 413);
            textBoxAddress3.Name = "textBoxAddress3";
            textBoxAddress3.Size = new Size(418, 27);
            textBoxAddress3.TabIndex = 65;
            // 
            // textBoxAddress2
            // 
            textBoxAddress2.Location = new Point(456, 380);
            textBoxAddress2.Name = "textBoxAddress2";
            textBoxAddress2.ReadOnly = true;
            textBoxAddress2.RightToLeft = RightToLeft.No;
            textBoxAddress2.Size = new Size(418, 27);
            textBoxAddress2.TabIndex = 64;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(411, 383);
            label2.Name = "label2";
            label2.Size = new Size(39, 20);
            label2.TabIndex = 63;
            label2.Text = "주소";
            // 
            // label9
            // 
            label9.AutoSize = true;
            label9.Font = new Font("맑은 고딕", 7.8F, FontStyle.Regular, GraphicsUnit.Point);
            label9.ForeColor = Color.Red;
            label9.Location = new Point(25, 24);
            label9.Name = "label9";
            label9.Size = new Size(76, 17);
            label9.TabIndex = 67;
            label9.Text = "* 필수 입력";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Font = new Font("맑은 고딕", 9F, FontStyle.Bold, GraphicsUnit.Point);
            label3.ForeColor = Color.Red;
            label3.Location = new Point(75, 166);
            label3.Name = "label3";
            label3.Size = new Size(16, 20);
            label3.TabIndex = 68;
            label3.Text = "*";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Font = new Font("맑은 고딕", 9F, FontStyle.Bold, GraphicsUnit.Point);
            label4.ForeColor = Color.Red;
            label4.Location = new Point(46, 258);
            label4.Name = "label4";
            label4.Size = new Size(16, 20);
            label4.TabIndex = 69;
            label4.Text = "*";
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Font = new Font("맑은 고딕", 9F, FontStyle.Bold, GraphicsUnit.Point);
            label5.ForeColor = Color.Red;
            label5.Location = new Point(75, 347);
            label5.Name = "label5";
            label5.Size = new Size(16, 20);
            label5.TabIndex = 70;
            label5.Text = "*";
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Font = new Font("맑은 고딕", 9F, FontStyle.Bold, GraphicsUnit.Point);
            label6.ForeColor = Color.Red;
            label6.Location = new Point(368, 86);
            label6.Name = "label6";
            label6.Size = new Size(16, 20);
            label6.TabIndex = 71;
            label6.Text = "*";
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Font = new Font("맑은 고딕", 9F, FontStyle.Bold, GraphicsUnit.Point);
            label7.ForeColor = Color.Red;
            label7.Location = new Point(748, 166);
            label7.Name = "label7";
            label7.Size = new Size(16, 20);
            label7.TabIndex = 72;
            label7.Text = "*";
            // 
            // label8
            // 
            label8.AutoSize = true;
            label8.Font = new Font("맑은 고딕", 9F, FontStyle.Bold, GraphicsUnit.Point);
            label8.ForeColor = Color.Red;
            label8.Location = new Point(748, 258);
            label8.Name = "label8";
            label8.Size = new Size(16, 20);
            label8.TabIndex = 73;
            label8.Text = "*";
            // 
            // comboBoxGender
            // 
            comboBoxGender.DropDownStyle = ComboBoxStyle.DropDownList;
            comboBoxGender.FormattingEnabled = true;
            comboBoxGender.Items.AddRange(new object[] { "남성", "여성" });
            comboBoxGender.Location = new Point(132, 347);
            comboBoxGender.Name = "comboBoxGender";
            comboBoxGender.Size = new Size(197, 28);
            comboBoxGender.TabIndex = 74;
            // 
            // EmployeeUpdateForm
            // 
            AutoScaleDimensions = new SizeF(9F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1060, 518);
            Controls.Add(comboBoxGender);
            Controls.Add(label8);
            Controls.Add(label7);
            Controls.Add(label6);
            Controls.Add(label5);
            Controls.Add(label4);
            Controls.Add(label3);
            Controls.Add(label9);
            Controls.Add(label11);
            Controls.Add(textBoxAddress3);
            Controls.Add(textBoxAddress2);
            Controls.Add(label2);
            Controls.Add(dateTimePicker1);
            Controls.Add(label1);
            Controls.Add(buttonAdrSearch);
            Controls.Add(labelAccount);
            Controls.Add(textBoxAccount);
            Controls.Add(labelGender);
            Controls.Add(labelEmail);
            Controls.Add(textBoxEmail);
            Controls.Add(labelAge);
            Controls.Add(textBoxAge);
            Controls.Add(labelAddress);
            Controls.Add(textBoxAddress1);
            Controls.Add(labelName);
            Controls.Add(textBoxName);
            Controls.Add(labelPosition);
            Controls.Add(comboBoxPosition);
            Controls.Add(labelDepartment);
            Controls.Add(comboBoxDepartment);
            Controls.Add(labelPhone);
            Controls.Add(textBoxPhone);
            Controls.Add(labelNum);
            Controls.Add(textBoxNum);
            Controls.Add(buttonCencel);
            Controls.Add(buttonConfirm);
            Name = "EmployeeUpdateForm";
            Text = "사원 수정";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button buttonConfirm;
        private Button buttonCencel;
        private TextBox textBoxNum;
        private Label labelNum;
        private TextBox textBoxPhone;
        private Label labelPhone;
        private ComboBox comboBoxDepartment;
        private Label labelDepartment;
        private Label labelPosition;
        private ComboBox comboBoxPosition;
        private Label labelAddress;
        private TextBox textBoxAddress1;
        private Label labelName;
        private TextBox textBoxName;
        private Label labelEmail;
        private TextBox textBoxEmail;
        private Label labelAge;
        private TextBox textBoxAge;
        private Label labelAccount;
        private TextBox textBoxAccount;
        private Label labelGender;
        private Button buttonAdrSearch;
        private DateTimePicker dateTimePicker1;
        private Label label1;
        private Label label11;
        private TextBox textBoxAddress3;
        private TextBox textBoxAddress2;
        private Label label2;
        private Label label9;
        private Label label3;
        private Label label4;
        private Label label5;
        private Label label6;
        private Label label7;
        private Label label8;
        private ComboBox comboBoxGender;
    }
}