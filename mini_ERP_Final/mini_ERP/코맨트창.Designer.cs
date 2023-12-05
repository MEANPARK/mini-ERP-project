namespace TeamProject_test_v1
{
    partial class 코맨트창
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
            this.헤드라벨 = new System.Windows.Forms.Label();
            this.코맨트_텍스트박스 = new System.Windows.Forms.TextBox();
            this.취소_버튼 = new System.Windows.Forms.Button();
            this.확인_버튼 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // 헤드라벨
            // 
            this.헤드라벨.AutoSize = true;
            this.헤드라벨.Font = new System.Drawing.Font("굴림", 11F);
            this.헤드라벨.Location = new System.Drawing.Point(11, 14);
            this.헤드라벨.Name = "헤드라벨";
            this.헤드라벨.Size = new System.Drawing.Size(177, 15);
            this.헤드라벨.TabIndex = 7;
            this.헤드라벨.Text = "반려사유를 입력해주세요";
            // 
            // 코맨트_텍스트박스
            // 
            this.코맨트_텍스트박스.Location = new System.Drawing.Point(12, 41);
            this.코맨트_텍스트박스.Multiline = true;
            this.코맨트_텍스트박스.Name = "코맨트_텍스트박스";
            this.코맨트_텍스트박스.Size = new System.Drawing.Size(266, 135);
            this.코맨트_텍스트박스.TabIndex = 6;
            // 
            // 취소_버튼
            // 
            this.취소_버튼.Location = new System.Drawing.Point(150, 182);
            this.취소_버튼.Name = "취소_버튼";
            this.취소_버튼.Size = new System.Drawing.Size(128, 27);
            this.취소_버튼.TabIndex = 5;
            this.취소_버튼.Text = "취소";
            this.취소_버튼.UseVisualStyleBackColor = true;
            this.취소_버튼.Click += new System.EventHandler(this.취소_버튼_Click);
            // 
            // 확인_버튼
            // 
            this.확인_버튼.Location = new System.Drawing.Point(11, 182);
            this.확인_버튼.Name = "확인_버튼";
            this.확인_버튼.Size = new System.Drawing.Size(133, 27);
            this.확인_버튼.TabIndex = 4;
            this.확인_버튼.Text = "확인";
            this.확인_버튼.UseVisualStyleBackColor = true;
            this.확인_버튼.Click += new System.EventHandler(this.확인_버튼_Click);
            // 
            // 코맨트창
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(287, 219);
            this.Controls.Add(this.헤드라벨);
            this.Controls.Add(this.코맨트_텍스트박스);
            this.Controls.Add(this.취소_버튼);
            this.Controls.Add(this.확인_버튼);
            this.Name = "코맨트창";
            this.Text = "코맨트창";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label 헤드라벨;
        private System.Windows.Forms.TextBox 코맨트_텍스트박스;
        private System.Windows.Forms.Button 취소_버튼;
        private System.Windows.Forms.Button 확인_버튼;
    }
}