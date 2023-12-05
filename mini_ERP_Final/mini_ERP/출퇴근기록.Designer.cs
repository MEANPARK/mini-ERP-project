namespace TeamProject_test_v1
{
    partial class 출퇴근기록
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
            additional_allowance_dataGridView = new DataGridView();
            additional_allowance_button = new Button();
            commute_label = new Label();
            ((System.ComponentModel.ISupportInitialize)additional_allowance_dataGridView).BeginInit();
            SuspendLayout();
            // 
            // additional_allowance_dataGridView
            // 
            additional_allowance_dataGridView.AllowUserToAddRows = false;
            additional_allowance_dataGridView.AllowUserToDeleteRows = false;
            additional_allowance_dataGridView.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            additional_allowance_dataGridView.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            additional_allowance_dataGridView.Location = new Point(14, 88);
            additional_allowance_dataGridView.Margin = new Padding(4);
            additional_allowance_dataGridView.Name = "additional_allowance_dataGridView";
            additional_allowance_dataGridView.ReadOnly = true;
            additional_allowance_dataGridView.RowHeadersVisible = false;
            additional_allowance_dataGridView.RowHeadersWidth = 51;
            additional_allowance_dataGridView.RowTemplate.Height = 25;
            additional_allowance_dataGridView.Size = new Size(1253, 619);
            additional_allowance_dataGridView.TabIndex = 0;
            // 
            // additional_allowance_button
            // 
            additional_allowance_button.BackColor = Color.SkyBlue;
            additional_allowance_button.FlatStyle = FlatStyle.Popup;
            additional_allowance_button.ForeColor = Color.White;
            additional_allowance_button.Location = new Point(1126, 32);
            additional_allowance_button.Margin = new Padding(4);
            additional_allowance_button.Name = "additional_allowance_button";
            additional_allowance_button.Size = new Size(141, 47);
            additional_allowance_button.TabIndex = 1;
            additional_allowance_button.Text = "추가 수당 신청";
            additional_allowance_button.UseVisualStyleBackColor = false;
            additional_allowance_button.Click += button1_Click;
            // 
            // commute_label
            // 
            commute_label.AutoSize = true;
            commute_label.Font = new Font("맑은 고딕", 12F, FontStyle.Bold, GraphicsUnit.Point);
            commute_label.Location = new Point(14, 38);
            commute_label.Name = "commute_label";
            commute_label.Size = new Size(186, 28);
            commute_label.TabIndex = 2;
            commute_label.Text = "나의 출퇴근 기록부";
            // 
            // 출퇴근기록
            // 
            AutoScaleDimensions = new SizeF(9F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1280, 720);
            Controls.Add(commute_label);
            Controls.Add(additional_allowance_button);
            Controls.Add(additional_allowance_dataGridView);
            FormBorderStyle = FormBorderStyle.None;
            Margin = new Padding(4);
            Name = "출퇴근기록";
            Text = "Form1";
            Load += 출퇴근기록_Load;
            ((System.ComponentModel.ISupportInitialize)additional_allowance_dataGridView).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private DataGridView additional_allowance_dataGridView;
        private Button additional_allowance_button;
        private Label commute_label;
    }
}