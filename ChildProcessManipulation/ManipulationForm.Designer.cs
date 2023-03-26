namespace ChildProcessManipulation
{
    partial class ManipulationForm
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
            this.SelectAssemblies = new System.Windows.Forms.ListBox();
            this.StartedAssemblies = new System.Windows.Forms.ListBox();
            this.StartButton = new System.Windows.Forms.Button();
            this.StopButton = new System.Windows.Forms.Button();
            this.CloseWindowButton = new System.Windows.Forms.Button();
            this.RefreshButton = new System.Windows.Forms.Button();
            this.RunCalcButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // SelectAssemblies
            // 
            this.SelectAssemblies.FormattingEnabled = true;
            this.SelectAssemblies.Location = new System.Drawing.Point(30, 24);
            this.SelectAssemblies.Name = "SelectAssemblies";
            this.SelectAssemblies.Size = new System.Drawing.Size(258, 407);
            this.SelectAssemblies.TabIndex = 0;
            // 
            // StartedAssemblies
            // 
            this.StartedAssemblies.FormattingEnabled = true;
            this.StartedAssemblies.Location = new System.Drawing.Point(518, 24);
            this.StartedAssemblies.Name = "StartedAssemblies";
            this.StartedAssemblies.Size = new System.Drawing.Size(259, 407);
            this.StartedAssemblies.TabIndex = 0;
            // 
            // StartButton
            // 
            this.StartButton.Location = new System.Drawing.Point(347, 150);
            this.StartButton.Name = "StartButton";
            this.StartButton.Size = new System.Drawing.Size(111, 26);
            this.StartButton.TabIndex = 1;
            this.StartButton.Text = "Start";
            this.StartButton.UseVisualStyleBackColor = true;
            this.StartButton.Click += new System.EventHandler(this.StartButton_Click);
            // 
            // StopButton
            // 
            this.StopButton.Location = new System.Drawing.Point(347, 182);
            this.StopButton.Name = "StopButton";
            this.StopButton.Size = new System.Drawing.Size(111, 23);
            this.StopButton.TabIndex = 1;
            this.StopButton.Text = "Stop";
            this.StopButton.UseVisualStyleBackColor = true;
            this.StopButton.Click += new System.EventHandler(this.StopButton_Click);
            // 
            // CloseWindowButton
            // 
            this.CloseWindowButton.Location = new System.Drawing.Point(347, 211);
            this.CloseWindowButton.Name = "CloseWindowButton";
            this.CloseWindowButton.Size = new System.Drawing.Size(111, 23);
            this.CloseWindowButton.TabIndex = 1;
            this.CloseWindowButton.Text = "Close Window";
            this.CloseWindowButton.UseVisualStyleBackColor = true;
            // 
            // RefreshButton
            // 
            this.RefreshButton.Location = new System.Drawing.Point(347, 240);
            this.RefreshButton.Name = "RefreshButton";
            this.RefreshButton.Size = new System.Drawing.Size(111, 23);
            this.RefreshButton.TabIndex = 1;
            this.RefreshButton.Text = "Refresh";
            this.RefreshButton.UseVisualStyleBackColor = true;
            // 
            // RunCalcButton
            // 
            this.RunCalcButton.Location = new System.Drawing.Point(347, 269);
            this.RunCalcButton.Name = "RunCalcButton";
            this.RunCalcButton.Size = new System.Drawing.Size(111, 23);
            this.RunCalcButton.TabIndex = 1;
            this.RunCalcButton.Text = "Run Calc";
            this.RunCalcButton.UseVisualStyleBackColor = true;
            // 
            // ManipulationForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.RunCalcButton);
            this.Controls.Add(this.RefreshButton);
            this.Controls.Add(this.CloseWindowButton);
            this.Controls.Add(this.StopButton);
            this.Controls.Add(this.StartButton);
            this.Controls.Add(this.StartedAssemblies);
            this.Controls.Add(this.SelectAssemblies);
            this.Name = "ManipulationForm";
            this.Text = "Form1";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListBox SelectAssemblies;
        private System.Windows.Forms.ListBox StartedAssemblies;
        private System.Windows.Forms.Button StartButton;
        private System.Windows.Forms.Button StopButton;
        private System.Windows.Forms.Button CloseWindowButton;
        private System.Windows.Forms.Button RefreshButton;
        private System.Windows.Forms.Button RunCalcButton;
    }
}

