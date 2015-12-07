namespace Spell_Checker
{
    partial class User_Interface
    {

        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region UI design

        private void InitializeComponent()
        {
            this.btn_Check = new System.Windows.Forms.Button();
            this.txt_1 = new System.Windows.Forms.TextBox();
            this.txt_2 = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.lbl_similarity = new System.Windows.Forms.Label();
            this.btn_Importword = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btn_Check
            // 
            this.btn_Check.Location = new System.Drawing.Point(59, 196);
            this.btn_Check.Name = "btn_Check";
            this.btn_Check.Size = new System.Drawing.Size(75, 23);
            this.btn_Check.TabIndex = 0;
            this.btn_Check.Text = "Check";
            this.btn_Check.UseVisualStyleBackColor = true;
            this.btn_Check.Click += new System.EventHandler(this.btn_check_Click);
            // 
            // txt_1
            // 
            this.txt_1.Location = new System.Drawing.Point(107, 81);
            this.txt_1.Name = "txt_1";
            this.txt_1.Size = new System.Drawing.Size(115, 21);
            this.txt_1.TabIndex = 1;
            // 
            // txt_2
            // 
            this.txt_2.Location = new System.Drawing.Point(107, 108);
            this.txt_2.Name = "txt_2";
            this.txt_2.Size = new System.Drawing.Size(115, 21);
            this.txt_2.TabIndex = 2;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(20, 90);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(59, 12);
            this.label1.TabIndex = 3;
            this.label1.Text = "String 1：";
            this.label1.Click += new System.EventHandler(this.label1_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(20, 117);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(59, 12);
            this.label2.TabIndex = 4;
            this.label2.Text = "String 2：";
            // 
            // lbl_similarity
            // 
            this.lbl_similarity.AutoSize = true;
            this.lbl_similarity.Location = new System.Drawing.Point(48, 157);
            this.lbl_similarity.Name = "lbl_similarity";
            this.lbl_similarity.Size = new System.Drawing.Size(53, 12);
            this.lbl_similarity.TabIndex = 5;
            this.lbl_similarity.Text = "Similarity Rate：";
            // 
            // btn_Importword
            // 
            this.btn_Importword.Location = new System.Drawing.Point(160, 196);
            this.btn_Importword.Name = "btn_Importword";
            this.btn_Importword.Size = new System.Drawing.Size(75, 23);
            this.btn_Importword.TabIndex = 6;
            this.btn_Importword.Text = "Import Word";
            this.btn_Importword.UseVisualStyleBackColor = true;
            this.btn_Importword.Click += new System.EventHandler(this.btn_importword_Click);
            // 
            // User_Interface
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 262);
            this.Controls.Add(this.btn_Importword);
            this.Controls.Add(this.lbl_similarity);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txt_2);
            this.Controls.Add(this.txt_1);
            this.Controls.Add(this.btn_Check);
            this.Name = "User_Interface";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "User_Interface";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btn_Check;
        private System.Windows.Forms.TextBox txt_1;
        private System.Windows.Forms.TextBox txt_2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label lbl_similarity;
        private System.Windows.Forms.Button btn_Importword;
    }
}

