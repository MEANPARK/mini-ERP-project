namespace TeamProject_test_v1
{
    partial class 출퇴근현황
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
            check_work_dataGridView = new DataGridView();
            check_work_label = new Label();
            check_work_button = new Button();
            ((System.ComponentModel.ISupportInitialize)check_work_dataGridView).BeginInit();
            SuspendLayout();
            // 
            // check_work_dataGridView
            // 
            check_work_dataGridView.AllowUserToAddRows = false;
            check_work_dataGridView.AllowUserToDeleteRows = false;
            check_work_dataGridView.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            check_work_dataGridView.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            check_work_dataGridView.Location = new Point(15, 90);
            check_work_dataGridView.Margin = new Padding(4, 4, 4, 4);
            check_work_dataGridView.Name = "check_work_dataGridView";
            check_work_dataGridView.ReadOnly = true;
            check_work_dataGridView.RowHeadersVisible = false;
            check_work_dataGridView.RowHeadersWidth = 51;
            check_work_dataGridView.RowTemplate.Height = 25;
            check_work_dataGridView.Size = new Size(1252, 617);
            check_work_dataGridView.TabIndex = 0;
            // 
            // check_work_label
            // 
            check_work_label.AutoSize = true;
            check_work_label.Font = new Font("맑은 고딕", 12F, FontStyle.Bold, GraphicsUnit.Point);
            check_work_label.Location = new Point(15, 47);
            check_work_label.Margin = new Padding(4, 0, 4, 0);
            check_work_label.Name = "check_work_label";
            check_work_label.Size = new Size(132, 28);
            check_work_label.TabIndex = 1;
            check_work_label.Text = "출퇴근기록부";
            // 
            // check_work_button
            // 
            check_work_button.BackColor = Color.SkyBlue;
            check_work_button.Font = new Font("맑은 고딕", 10.2F, FontStyle.Bold, GraphicsUnit.Point);
            check_work_button.ForeColor = Color.White;
            check_work_button.Location = new Point(1126, 35);
            check_work_button.Margin = new Padding(4, 4, 4, 4);
            check_work_button.Name = "check_work_button";
            check_work_button.Size = new Size(141, 47);
            check_work_button.TabIndex = 3;
            check_work_button.Text = "조회";
            check_work_button.UseVisualStyleBackColor = false;
            check_work_button.Click += check_work_button_Click;
            // 
            // 출퇴근현황
            // 
            AutoScaleDimensions = new SizeF(9F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1280, 720);
            Controls.Add(check_work_button);
            Controls.Add(check_work_label);
            Controls.Add(check_work_dataGridView);
            FormBorderStyle = FormBorderStyle.None;
            Margin = new Padding(4, 4, 4, 4);
            Name = "출퇴근현황";
            Text = "출퇴근현황";
            ((System.ComponentModel.ISupportInitialize)check_work_dataGridView).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private DataGridView check_work_dataGridView;
        private Label check_work_label;
        private Button check_work_button;
    }
}