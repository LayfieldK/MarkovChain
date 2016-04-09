namespace MarkovChain
{
    partial class MarkovForm
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
            this.AnalyzeButton = new System.Windows.Forms.Button();
            this.InputLabel = new System.Windows.Forms.Label();
            this.InputBox = new System.Windows.Forms.TextBox();
            this.OutputBox = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // AnalyzeButton
            // 
            this.AnalyzeButton.Location = new System.Drawing.Point(271, 305);
            this.AnalyzeButton.Name = "AnalyzeButton";
            this.AnalyzeButton.Size = new System.Drawing.Size(147, 23);
            this.AnalyzeButton.TabIndex = 0;
            this.AnalyzeButton.Text = "Analyze Markov Chain";
            this.AnalyzeButton.UseVisualStyleBackColor = true;
            this.AnalyzeButton.Click += new System.EventHandler(this.AnalyzeButton_Click);
            // 
            // InputLabel
            // 
            this.InputLabel.AutoSize = true;
            this.InputLabel.Location = new System.Drawing.Point(324, 13);
            this.InputLabel.Name = "InputLabel";
            this.InputLabel.Size = new System.Drawing.Size(31, 13);
            this.InputLabel.TabIndex = 1;
            this.InputLabel.Text = "Input";
            // 
            // InputBox
            // 
            this.InputBox.Location = new System.Drawing.Point(60, 29);
            this.InputBox.Multiline = true;
            this.InputBox.Name = "InputBox";
            this.InputBox.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.InputBox.Size = new System.Drawing.Size(594, 235);
            this.InputBox.TabIndex = 2;
            // 
            // OutputBox
            // 
            this.OutputBox.Location = new System.Drawing.Point(60, 374);
            this.OutputBox.Multiline = true;
            this.OutputBox.Name = "OutputBox";
            this.OutputBox.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.OutputBox.Size = new System.Drawing.Size(594, 151);
            this.OutputBox.TabIndex = 3;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(324, 352);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(39, 13);
            this.label2.TabIndex = 4;
            this.label2.Text = "Output";
            // 
            // MarkovForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(707, 537);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.OutputBox);
            this.Controls.Add(this.InputBox);
            this.Controls.Add(this.InputLabel);
            this.Controls.Add(this.AnalyzeButton);
            this.Name = "MarkovForm";
            this.Text = "Markov Chain Analysis";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button AnalyzeButton;
        private System.Windows.Forms.Label InputLabel;
        private System.Windows.Forms.TextBox InputBox;
        private System.Windows.Forms.TextBox OutputBox;
        private System.Windows.Forms.Label label2;
    }
}

