
namespace GameOfLife
{
    partial class ModalDialog
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
            this.buttonOK = new System.Windows.Forms.Button();
            this.buttonCancel = new System.Windows.Forms.Button();
            this.Timer = new System.Windows.Forms.NumericUpDown();
            this.label1 = new System.Windows.Forms.Label();
            this.HeightCounter = new System.Windows.Forms.NumericUpDown();
            this.WidthCounter = new System.Windows.Forms.NumericUpDown();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.Timer)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.HeightCounter)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.WidthCounter)).BeginInit();
            this.SuspendLayout();
            // 
            // buttonOK
            // 
            this.buttonOK.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.buttonOK.Location = new System.Drawing.Point(255, 336);
            this.buttonOK.Name = "buttonOK";
            this.buttonOK.Size = new System.Drawing.Size(75, 23);
            this.buttonOK.TabIndex = 0;
            this.buttonOK.Text = "OK";
            this.buttonOK.UseVisualStyleBackColor = true;
            // 
            // buttonCancel
            // 
            this.buttonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.buttonCancel.Location = new System.Drawing.Point(352, 336);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(75, 23);
            this.buttonCancel.TabIndex = 1;
            this.buttonCancel.Text = "Cancel";
            this.buttonCancel.UseVisualStyleBackColor = true;
            // 
            // Timer
            // 
            this.Timer.Location = new System.Drawing.Point(179, 80);
            this.Timer.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.Timer.Name = "Timer";
            this.Timer.Size = new System.Drawing.Size(120, 20);
            this.Timer.TabIndex = 2;
            this.Timer.ValueChanged += new System.EventHandler(this.Timer_ValueChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(17, 80);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(139, 13);
            this.label1.TabIndex = 3;
            this.label1.Text = "Timer Interval in miliseconds";
            this.label1.Click += new System.EventHandler(this.label1_Click);
            // 
            // HeightCounter
            // 
            this.HeightCounter.Location = new System.Drawing.Point(179, 157);
            this.HeightCounter.Name = "HeightCounter";
            this.HeightCounter.Size = new System.Drawing.Size(120, 20);
            this.HeightCounter.TabIndex = 4;
            this.HeightCounter.ValueChanged += new System.EventHandler(this.HeightCounter_ValueChanged);
            // 
            // WidthCounter
            // 
            this.WidthCounter.Location = new System.Drawing.Point(179, 115);
            this.WidthCounter.Name = "WidthCounter";
            this.WidthCounter.Size = new System.Drawing.Size(120, 20);
            this.WidthCounter.TabIndex = 5;
            this.WidthCounter.ValueChanged += new System.EventHandler(this.WidthCounter_ValueChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(17, 117);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(125, 13);
            this.label2.TabIndex = 6;
            this.label2.Text = "Width of universe in cells";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(17, 157);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(128, 13);
            this.label3.TabIndex = 7;
            this.label3.Text = "Height of universe in cells";
            // 
            // ModalDialog
            // 
            this.AcceptButton = this.buttonOK;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.buttonCancel;
            this.ClientSize = new System.Drawing.Size(427, 371);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.WidthCounter);
            this.Controls.Add(this.HeightCounter);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.Timer);
            this.Controls.Add(this.buttonCancel);
            this.Controls.Add(this.buttonOK);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ModalDialog";
            this.Text = "ModalDialog";
            ((System.ComponentModel.ISupportInitialize)(this.Timer)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.HeightCounter)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.WidthCounter)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button buttonOK;
        private System.Windows.Forms.Button buttonCancel;
        private System.Windows.Forms.NumericUpDown Timer;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.NumericUpDown HeightCounter;
        private System.Windows.Forms.NumericUpDown WidthCounter;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
    }
}