namespace AdminEmulator
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
            imgBox = new PictureBox();
            CanselBtn = new Button();
            SendBtn = new Button();
            ((System.ComponentModel.ISupportInitialize)imgBox).BeginInit();
            SuspendLayout();
            // 
            // imgBox
            // 
            imgBox.Location = new Point(0, 0);
            imgBox.Name = "imgBox";
            imgBox.Size = new Size(478, 229);
            imgBox.TabIndex = 0;
            imgBox.TabStop = false;
            // 
            // CanselBtn
            // 
            CanselBtn.Location = new Point(583, 91);
            CanselBtn.Name = "CanselBtn";
            CanselBtn.Size = new Size(94, 29);
            CanselBtn.TabIndex = 2;
            CanselBtn.Text = "&";
            CanselBtn.UseVisualStyleBackColor = true;
            CanselBtn.Click += CanselBtn_Click;
            // 
            // SendBtn
            // 
            SendBtn.Location = new Point(583, 36);
            SendBtn.Name = "SendBtn";
            SendBtn.Size = new Size(94, 29);
            SendBtn.TabIndex = 3;
            SendBtn.Text = "Send";
            SendBtn.UseVisualStyleBackColor = true;
            SendBtn.Click += SendBtn_Click;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(SendBtn);
            Controls.Add(CanselBtn);
            Controls.Add(imgBox);
            Name = "Form1";
            Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)imgBox).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private PictureBox imgBox;
        private Button CanselBtn;
        private Button SendBtn;
    }
}
