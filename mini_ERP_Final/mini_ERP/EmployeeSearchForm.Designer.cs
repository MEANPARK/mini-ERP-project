namespace TeamProject_test_v1
{
    partial class EmployeeSearchForm
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
            comboBoxDepartment = new ComboBox();
            textBoxAge = new TextBox();
            textBoxName = new TextBox();
            buttonSearch = new Button();
            label1 = new Label();
            label2 = new Label();
            label3 = new Label();
            dataGridViewEmployee = new DataGridView();
            buttonUpdate = new Button();
            buttonDelete = new Button();
            label4 = new Label();
            buttonReset = new Button();
            ((System.ComponentModel.ISupportInitialize)dataGridViewEmployee).BeginInit();
            SuspendLayout();
            // 
            // comboBoxDepartment
            // 
            comboBoxDepartment.DropDownStyle = ComboBoxStyle.DropDownList;
            comboBoxDepartment.FormattingEnabled = true;
            comboBoxDepartment.Location = new Point(66, 90);
            comboBoxDepartment.Name = "comboBoxDepartment";
            comboBoxDepartment.Size = new Size(151, 28);
            comboBoxDepartment.TabIndex = 0;
            // 
            // textBoxAge
            // 
            textBoxAge.Location = new Point(398, 90);
            textBoxAge.Name = "textBoxAge";
            textBoxAge.PlaceholderText = "나이";
            textBoxAge.Size = new Size(125, 27);
            textBoxAge.TabIndex = 1;
            // 
            // textBoxName
            // 
            textBoxName.Location = new Point(242, 90);
            textBoxName.Name = "textBoxName";
            textBoxName.PlaceholderText = "이름";
            textBoxName.Size = new Size(125, 27);
            textBoxName.TabIndex = 2;
            // 
            // buttonSearch
            // 
            buttonSearch.Location = new Point(1138, 90);
            buttonSearch.Name = "buttonSearch";
            buttonSearch.Size = new Size(94, 29);
            buttonSearch.TabIndex = 3;
            buttonSearch.Text = "검색하기";
            buttonSearch.UseVisualStyleBackColor = true;
            buttonSearch.Click += buttonSearch_Click;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(66, 67);
            label1.Name = "label1";
            label1.Size = new Size(54, 20);
            label1.TabIndex = 4;
            label1.Text = "부서명";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(242, 67);
            label2.Name = "label2";
            label2.Size = new Size(74, 20);
            label2.TabIndex = 5;
            label2.Text = "사원 이름";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(398, 67);
            label3.Name = "label3";
            label3.Size = new Size(39, 20);
            label3.TabIndex = 6;
            label3.Text = "나이";
            // 
            // dataGridViewEmployee
            // 
            dataGridViewEmployee.AllowUserToAddRows = false;
            dataGridViewEmployee.AllowUserToDeleteRows = false;
            dataGridViewEmployee.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dataGridViewCellStyle1.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.BackColor = SystemColors.Control;
            dataGridViewCellStyle1.Font = new Font("맑은 고딕", 9F, FontStyle.Regular, GraphicsUnit.Point);
            dataGridViewCellStyle1.ForeColor = SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = DataGridViewTriState.True;
            dataGridViewEmployee.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            dataGridViewEmployee.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewEmployee.Location = new Point(66, 137);
            dataGridViewEmployee.Name = "dataGridViewEmployee";
            dataGridViewEmployee.RowHeadersVisible = false;
            dataGridViewEmployee.RowHeadersWidth = 51;
            dataGridViewEmployee.RowTemplate.Height = 29;
            dataGridViewEmployee.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridViewEmployee.Size = new Size(1166, 454);
            dataGridViewEmployee.TabIndex = 7;
            // 
            // buttonUpdate
            // 
            buttonUpdate.Location = new Point(1027, 611);
            buttonUpdate.Name = "buttonUpdate";
            buttonUpdate.Size = new Size(94, 29);
            buttonUpdate.TabIndex = 8;
            buttonUpdate.Text = "수정";
            buttonUpdate.UseVisualStyleBackColor = true;
            buttonUpdate.Click += buttonUpdate_Click;
            // 
            // buttonDelete
            // 
            buttonDelete.Location = new Point(1138, 611);
            buttonDelete.Name = "buttonDelete";
            buttonDelete.Size = new Size(94, 29);
            buttonDelete.TabIndex = 9;
            buttonDelete.Text = "삭제";
            buttonDelete.UseVisualStyleBackColor = true;
            buttonDelete.Click += buttonDelete_Click;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Font = new Font("맑은 고딕", 10.2F, FontStyle.Bold, GraphicsUnit.Point);
            label4.Location = new Point(25, 18);
            label4.Name = "label4";
            label4.Size = new Size(78, 23);
            label4.TabIndex = 10;
            label4.Text = "사원검색";
            // 
            // buttonReset
            // 
            buttonReset.Location = new Point(540, 90);
            buttonReset.Name = "buttonReset";
            buttonReset.Size = new Size(70, 28);
            buttonReset.TabIndex = 11;
            buttonReset.Text = "초기화";
            buttonReset.UseVisualStyleBackColor = true;
            buttonReset.Click += buttonReset_Click;
            // 
            // EmployeeSearchForm
            // 
            AutoScaleDimensions = new SizeF(9F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1280, 720);
            Controls.Add(buttonReset);
            Controls.Add(label4);
            Controls.Add(buttonDelete);
            Controls.Add(buttonUpdate);
            Controls.Add(dataGridViewEmployee);
            Controls.Add(label3);
            Controls.Add(label2);
            Controls.Add(label1);
            Controls.Add(buttonSearch);
            Controls.Add(textBoxName);
            Controls.Add(textBoxAge);
            Controls.Add(comboBoxDepartment);
            FormBorderStyle = FormBorderStyle.None;
            Name = "EmployeeSearchForm";
            Text = "ChildForm1";
            ((System.ComponentModel.ISupportInitialize)dataGridViewEmployee).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private ComboBox comboBoxDepartment;
        private TextBox textBoxAge;
        private TextBox textBoxName;
        private Button buttonSearch;
        private Label label1;
        private Label label2;
        private Label label3;
        private DataGridView dataGridViewEmployee;
        private Button buttonUpdate;
        private Button buttonDelete;
        private Label label4;
        private Button buttonReset;
    }
}