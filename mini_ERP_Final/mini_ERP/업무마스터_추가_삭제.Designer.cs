namespace TeamProject_test_v1
{
    partial class 업무마스터_추가_삭제
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
            this.label1 = new System.Windows.Forms.Label();
            this.big_textbox = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.mid_textbox = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.small_textbox = new System.Windows.Forms.TextBox();
            this.delete_button = new System.Windows.Forms.Button();
            this.save_button = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("맑은 고딕", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.label1.Location = new System.Drawing.Point(32, 41);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(69, 25);
            this.label1.TabIndex = 0;
            this.label1.Text = "대분류";
            // 
            // big_textbox
            // 
            this.big_textbox.Font = new System.Drawing.Font("맑은 고딕", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.big_textbox.Location = new System.Drawing.Point(121, 39);
            this.big_textbox.Name = "big_textbox";
            this.big_textbox.Size = new System.Drawing.Size(170, 32);
            this.big_textbox.TabIndex = 1;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("맑은 고딕", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.label2.Location = new System.Drawing.Point(331, 42);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(69, 25);
            this.label2.TabIndex = 2;
            this.label2.Text = "중분류";
            // 
            // mid_textbox
            // 
            this.mid_textbox.Font = new System.Drawing.Font("맑은 고딕", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.mid_textbox.Location = new System.Drawing.Point(421, 41);
            this.mid_textbox.Name = "mid_textbox";
            this.mid_textbox.Size = new System.Drawing.Size(170, 32);
            this.mid_textbox.TabIndex = 3;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("맑은 고딕", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.label3.Location = new System.Drawing.Point(632, 42);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(69, 25);
            this.label3.TabIndex = 4;
            this.label3.Text = "소분류";
            // 
            // small_textbox
            // 
            this.small_textbox.Font = new System.Drawing.Font("맑은 고딕", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.small_textbox.Location = new System.Drawing.Point(719, 39);
            this.small_textbox.Name = "small_textbox";
            this.small_textbox.Size = new System.Drawing.Size(170, 32);
            this.small_textbox.TabIndex = 5;
            // 
            // delete_button
            // 
            this.delete_button.Location = new System.Drawing.Point(497, 129);
            this.delete_button.Name = "delete_button";
            this.delete_button.Size = new System.Drawing.Size(94, 36);
            this.delete_button.TabIndex = 6;
            this.delete_button.Text = "삭제하기";
            this.delete_button.UseVisualStyleBackColor = true;
            this.delete_button.Click += new System.EventHandler(this.delete_button_Click);
            // 
            // save_button
            // 
            this.save_button.Location = new System.Drawing.Point(364, 129);
            this.save_button.Name = "save_button";
            this.save_button.Size = new System.Drawing.Size(94, 36);
            this.save_button.TabIndex = 7;
            this.save_button.Text = "저장하기";
            this.save_button.UseVisualStyleBackColor = true;
            this.save_button.Click += new System.EventHandler(this.save_button_Click);
            // 
            // 업무마스터_관리
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(973, 200);
            this.Controls.Add(this.save_button);
            this.Controls.Add(this.delete_button);
            this.Controls.Add(this.small_textbox);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.mid_textbox);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.big_textbox);
            this.Controls.Add(this.label1);
            this.Name = "업무마스터_관리";
            this.Text = "업무마스터_관리";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Label label1;
        private TextBox big_textbox;
        private Label label2;
        private TextBox mid_textbox;
        private Label label3;
        private TextBox small_textbox;
        private Button delete_button;
        private Button save_button;
    }
}