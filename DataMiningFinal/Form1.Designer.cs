﻿namespace DataMiningFinal
{
    partial class Form1
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
            this.button1 = new System.Windows.Forms.Button();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.textBox3 = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.textBox4 = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(650, 147);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(80, 93);
            this.button1.TabIndex = 0;
            this.button1.Text = "Calculate";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(554, 147);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(80, 27);
            this.textBox1.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(255, 147);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(289, 20);
            this.label1.TabIndex = 2;
            this.label1.Text = "How many clusters do you want to create?";
            // 
            // textBox2
            // 
            this.textBox2.Location = new System.Drawing.Point(554, 207);
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new System.Drawing.Size(80, 27);
            this.textBox2.TabIndex = 3;
            // 
            // textBox3
            // 
            this.textBox3.Location = new System.Drawing.Point(684, 254);
            this.textBox3.Name = "textBox3";
            this.textBox3.Size = new System.Drawing.Size(80, 27);
            this.textBox3.TabIndex = 4;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(246, 210);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(272, 20);
            this.label2.TabIndex = 5;
            this.label2.Text = "Do you want to visualize the data? (y/n)";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(30, 261);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(648, 20);
            this.label3.TabIndex = 6;
            this.label3.Text = "(if don\'t want to visualize PLEASE DONT WRİTE!)Which column do you want to use fo" +
    "r the x axis?";
            // 
            // textBox4
            // 
            this.textBox4.Location = new System.Drawing.Point(684, 306);
            this.textBox4.Name = "textBox4";
            this.textBox4.Size = new System.Drawing.Size(80, 27);
            this.textBox4.TabIndex = 7;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(30, 306);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(648, 20);
            this.label4.TabIndex = 8;
            this.label4.Text = "(if don\'t want to visualize PLEASE DONT WRİTE!)Which column do you want to use fo" +
    "r the y axis?";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.textBox4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.textBox3);
            this.Controls.Add(this.textBox2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.button1);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Button button1;
        private TextBox textBox1;
        private Label label1;
        private TextBox textBox2;
        private TextBox textBox3;
        private Label label2;
        private Label label3;
        private TextBox textBox4;
        private Label label4;
    }
}