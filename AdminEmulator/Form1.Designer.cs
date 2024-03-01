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
            tableLayoutPanel1 = new TableLayoutPanel();
            panel1 = new Panel();
            ((System.ComponentModel.ISupportInitialize)imgBox).BeginInit();
            tableLayoutPanel1.SuspendLayout();
            panel1.SuspendLayout();
            SuspendLayout();
            // 
            // imgBox
            // 
            imgBox.Dock = DockStyle.Fill;
            imgBox.Location = new Point(3, 3);
            imgBox.Name = "imgBox";
            imgBox.Size = new Size(794, 353);
            imgBox.SizeMode = PictureBoxSizeMode.StretchImage;
            imgBox.TabIndex = 0;
            imgBox.TabStop = false;
            imgBox.MouseMove += imgBox_MouseMove;
            // 
            // CanselBtn
            // 
            CanselBtn.Location = new Point(172, 22);
            CanselBtn.Name = "CanselBtn";
            CanselBtn.Size = new Size(94, 29);
            CanselBtn.TabIndex = 2;
            CanselBtn.Text = "&";
            CanselBtn.UseVisualStyleBackColor = true;
            CanselBtn.Click += CanselBtn_Click;
            // 
            // SendBtn
            // 
            SendBtn.Location = new Point(24, 22);
            SendBtn.Name = "SendBtn";
            SendBtn.Size = new Size(94, 29);
            SendBtn.TabIndex = 3;
            SendBtn.Text = "Send";
            SendBtn.UseVisualStyleBackColor = true;
            SendBtn.Click += SendBtn_Click;
            // 
            // tableLayoutPanel1
            // 
            tableLayoutPanel1.ColumnCount = 1;
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 47F));
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 53F));
            tableLayoutPanel1.Controls.Add(imgBox, 0, 0);
            tableLayoutPanel1.Controls.Add(panel1, 0, 1);
            tableLayoutPanel1.Dock = DockStyle.Fill;
            tableLayoutPanel1.GrowStyle = TableLayoutPanelGrowStyle.AddColumns;
            tableLayoutPanel1.Location = new Point(0, 0);
            tableLayoutPanel1.Name = "tableLayoutPanel1";
            tableLayoutPanel1.RowCount = 2;
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 79.77778F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 20.2222214F));
            tableLayoutPanel1.Size = new Size(800, 450);
            tableLayoutPanel1.TabIndex = 4;
            // 
            // panel1
            // 
            panel1.Controls.Add(SendBtn);
            panel1.Controls.Add(CanselBtn);
            panel1.Dock = DockStyle.Fill;
            panel1.Location = new Point(3, 362);
            panel1.Name = "panel1";
            panel1.Size = new Size(794, 85);
            panel1.TabIndex = 4;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(tableLayoutPanel1);
            Name = "Form1";
            Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)imgBox).EndInit();
            tableLayoutPanel1.ResumeLayout(false);
            panel1.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private PictureBox imgBox;
        private Button CanselBtn;
        private Button SendBtn;
        private TableLayoutPanel tableLayoutPanel1;
        private Panel panel1;
    }
}
