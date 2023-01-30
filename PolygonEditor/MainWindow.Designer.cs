namespace PolygonEditor
{
    partial class MainWindow
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainWindow));
            this.canvas = new System.Windows.Forms.PictureBox();
            this.addGroupBox = new System.Windows.Forms.GroupBox();
            this.AddRelationGivenLengthbutton = new System.Windows.Forms.Button();
            this.AddVertexButton = new System.Windows.Forms.Button();
            this.AddPolygonButton = new System.Windows.Forms.Button();
            this.AddRelationParallelButton = new System.Windows.Forms.Button();
            this.removeGroupBox = new System.Windows.Forms.GroupBox();
            this.RemoveRelationGivenLengthbutton = new System.Windows.Forms.Button();
            this.RemoveRelationParallelButton = new System.Windows.Forms.Button();
            this.RemoveVertexButton = new System.Windows.Forms.Button();
            this.RemovePolygonButton = new System.Windows.Forms.Button();
            this.BresenhamRadioButton = new System.Windows.Forms.RadioButton();
            this.algorithmGroupBox = new System.Windows.Forms.GroupBox();
            this.LibraryRadioButton = new System.Windows.Forms.RadioButton();
            this.ResetButton = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.canvas)).BeginInit();
            this.addGroupBox.SuspendLayout();
            this.removeGroupBox.SuspendLayout();
            this.algorithmGroupBox.SuspendLayout();
            this.SuspendLayout();
            // 
            // canvas
            // 
            this.canvas.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.canvas.Location = new System.Drawing.Point(12, 12);
            this.canvas.Name = "canvas";
            this.canvas.Size = new System.Drawing.Size(611, 537);
            this.canvas.TabIndex = 0;
            this.canvas.TabStop = false;
            this.canvas.Paint += new System.Windows.Forms.PaintEventHandler(this.Canvas_Paint);
            this.canvas.MouseClick += new System.Windows.Forms.MouseEventHandler(this.Canvas_MouseClick);
            this.canvas.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Canvas_MouseDown);
            this.canvas.MouseMove += new System.Windows.Forms.MouseEventHandler(this.Canvas_MouseMove);
            this.canvas.MouseUp += new System.Windows.Forms.MouseEventHandler(this.Canvas_MouseUp);
            // 
            // addGroupBox
            // 
            this.addGroupBox.Controls.Add(this.AddRelationGivenLengthbutton);
            this.addGroupBox.Controls.Add(this.AddVertexButton);
            this.addGroupBox.Controls.Add(this.AddPolygonButton);
            this.addGroupBox.Controls.Add(this.AddRelationParallelButton);
            this.addGroupBox.Location = new System.Drawing.Point(639, 12);
            this.addGroupBox.Name = "addGroupBox";
            this.addGroupBox.Size = new System.Drawing.Size(233, 169);
            this.addGroupBox.TabIndex = 1;
            this.addGroupBox.TabStop = false;
            this.addGroupBox.Text = "Add";
            // 
            // AddRelationGivenLengthbutton
            // 
            this.AddRelationGivenLengthbutton.BackColor = System.Drawing.Color.Lime;
            this.AddRelationGivenLengthbutton.Location = new System.Drawing.Point(132, 99);
            this.AddRelationGivenLengthbutton.Name = "AddRelationGivenLengthbutton";
            this.AddRelationGivenLengthbutton.Size = new System.Drawing.Size(95, 62);
            this.AddRelationGivenLengthbutton.TabIndex = 3;
            this.AddRelationGivenLengthbutton.Text = "Given length";
            this.AddRelationGivenLengthbutton.UseVisualStyleBackColor = false;
            this.AddRelationGivenLengthbutton.Click += new System.EventHandler(this.AddRelationGivenLengthbutton_Click);
            // 
            // AddVertexButton
            // 
            this.AddVertexButton.BackColor = System.Drawing.Color.Lime;
            this.AddVertexButton.Location = new System.Drawing.Point(132, 22);
            this.AddVertexButton.Name = "AddVertexButton";
            this.AddVertexButton.Size = new System.Drawing.Size(95, 62);
            this.AddVertexButton.TabIndex = 1;
            this.AddVertexButton.Text = "Vertex";
            this.AddVertexButton.UseVisualStyleBackColor = false;
            this.AddVertexButton.Click += new System.EventHandler(this.AddVertexButton_Click);
            // 
            // AddPolygonButton
            // 
            this.AddPolygonButton.BackColor = System.Drawing.Color.Lime;
            this.AddPolygonButton.Location = new System.Drawing.Point(6, 22);
            this.AddPolygonButton.Name = "AddPolygonButton";
            this.AddPolygonButton.Size = new System.Drawing.Size(95, 62);
            this.AddPolygonButton.TabIndex = 0;
            this.AddPolygonButton.Text = "Polygon";
            this.AddPolygonButton.UseVisualStyleBackColor = false;
            this.AddPolygonButton.Click += new System.EventHandler(this.AddPolygonButton_Click);
            // 
            // AddRelationParallelButton
            // 
            this.AddRelationParallelButton.BackColor = System.Drawing.Color.Lime;
            this.AddRelationParallelButton.Location = new System.Drawing.Point(6, 99);
            this.AddRelationParallelButton.Name = "AddRelationParallelButton";
            this.AddRelationParallelButton.Size = new System.Drawing.Size(95, 62);
            this.AddRelationParallelButton.TabIndex = 2;
            this.AddRelationParallelButton.Text = "Parallel edges";
            this.AddRelationParallelButton.UseVisualStyleBackColor = false;
            this.AddRelationParallelButton.Click += new System.EventHandler(this.AddRelationParallelButton_Click);
            // 
            // removeGroupBox
            // 
            this.removeGroupBox.Controls.Add(this.RemoveRelationGivenLengthbutton);
            this.removeGroupBox.Controls.Add(this.RemoveRelationParallelButton);
            this.removeGroupBox.Controls.Add(this.RemoveVertexButton);
            this.removeGroupBox.Controls.Add(this.RemovePolygonButton);
            this.removeGroupBox.Location = new System.Drawing.Point(639, 187);
            this.removeGroupBox.Name = "removeGroupBox";
            this.removeGroupBox.Size = new System.Drawing.Size(233, 166);
            this.removeGroupBox.TabIndex = 2;
            this.removeGroupBox.TabStop = false;
            this.removeGroupBox.Text = "Remove";
            // 
            // RemoveRelationGivenLengthbutton
            // 
            this.RemoveRelationGivenLengthbutton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.RemoveRelationGivenLengthbutton.Location = new System.Drawing.Point(132, 98);
            this.RemoveRelationGivenLengthbutton.Name = "RemoveRelationGivenLengthbutton";
            this.RemoveRelationGivenLengthbutton.Size = new System.Drawing.Size(95, 62);
            this.RemoveRelationGivenLengthbutton.TabIndex = 5;
            this.RemoveRelationGivenLengthbutton.Text = "Given length";
            this.RemoveRelationGivenLengthbutton.UseVisualStyleBackColor = false;
            this.RemoveRelationGivenLengthbutton.Click += new System.EventHandler(this.RemoveRelationGivenLengthbutton_Click);
            // 
            // RemoveRelationParallelButton
            // 
            this.RemoveRelationParallelButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.RemoveRelationParallelButton.Location = new System.Drawing.Point(6, 98);
            this.RemoveRelationParallelButton.Name = "RemoveRelationParallelButton";
            this.RemoveRelationParallelButton.Size = new System.Drawing.Size(95, 62);
            this.RemoveRelationParallelButton.TabIndex = 4;
            this.RemoveRelationParallelButton.Text = "Parallel edges";
            this.RemoveRelationParallelButton.UseVisualStyleBackColor = false;
            this.RemoveRelationParallelButton.Click += new System.EventHandler(this.RemoveRelationParallelButton_Click);
            // 
            // RemoveVertexButton
            // 
            this.RemoveVertexButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.RemoveVertexButton.Location = new System.Drawing.Point(132, 22);
            this.RemoveVertexButton.Name = "RemoveVertexButton";
            this.RemoveVertexButton.Size = new System.Drawing.Size(95, 62);
            this.RemoveVertexButton.TabIndex = 3;
            this.RemoveVertexButton.Text = "Vertex";
            this.RemoveVertexButton.UseVisualStyleBackColor = false;
            this.RemoveVertexButton.Click += new System.EventHandler(this.RemoveVertexButton_Click);
            // 
            // RemovePolygonButton
            // 
            this.RemovePolygonButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.RemovePolygonButton.Location = new System.Drawing.Point(6, 22);
            this.RemovePolygonButton.Name = "RemovePolygonButton";
            this.RemovePolygonButton.Size = new System.Drawing.Size(95, 62);
            this.RemovePolygonButton.TabIndex = 2;
            this.RemovePolygonButton.Text = "Polygon";
            this.RemovePolygonButton.UseVisualStyleBackColor = false;
            this.RemovePolygonButton.Click += new System.EventHandler(this.RemovePolygonButton_Click);
            // 
            // BresenhamRadioButton
            // 
            this.BresenhamRadioButton.AutoSize = true;
            this.BresenhamRadioButton.Location = new System.Drawing.Point(17, 31);
            this.BresenhamRadioButton.Name = "BresenhamRadioButton";
            this.BresenhamRadioButton.Size = new System.Drawing.Size(84, 19);
            this.BresenhamRadioButton.TabIndex = 1;
            this.BresenhamRadioButton.Text = "Bresenham";
            this.BresenhamRadioButton.UseVisualStyleBackColor = true;
            this.BresenhamRadioButton.Click += new System.EventHandler(this.BresenhamRadioButton_Click);
            // 
            // algorithmGroupBox
            // 
            this.algorithmGroupBox.Controls.Add(this.LibraryRadioButton);
            this.algorithmGroupBox.Controls.Add(this.BresenhamRadioButton);
            this.algorithmGroupBox.Location = new System.Drawing.Point(639, 372);
            this.algorithmGroupBox.Name = "algorithmGroupBox";
            this.algorithmGroupBox.Size = new System.Drawing.Size(233, 100);
            this.algorithmGroupBox.TabIndex = 6;
            this.algorithmGroupBox.TabStop = false;
            this.algorithmGroupBox.Text = "Draw Algorithm";
            // 
            // LibraryRadioButton
            // 
            this.LibraryRadioButton.AutoSize = true;
            this.LibraryRadioButton.Checked = true;
            this.LibraryRadioButton.Location = new System.Drawing.Point(17, 65);
            this.LibraryRadioButton.Name = "LibraryRadioButton";
            this.LibraryRadioButton.Size = new System.Drawing.Size(61, 19);
            this.LibraryRadioButton.TabIndex = 2;
            this.LibraryRadioButton.TabStop = true;
            this.LibraryRadioButton.Text = "Library";
            this.LibraryRadioButton.UseVisualStyleBackColor = true;
            this.LibraryRadioButton.Click += new System.EventHandler(this.LibraryRadioButton_Click);
            // 
            // ResetButton
            // 
            this.ResetButton.BackColor = System.Drawing.Color.Cyan;
            this.ResetButton.ForeColor = System.Drawing.SystemColors.ControlText;
            this.ResetButton.Location = new System.Drawing.Point(713, 493);
            this.ResetButton.Name = "ResetButton";
            this.ResetButton.Size = new System.Drawing.Size(89, 56);
            this.ResetButton.TabIndex = 0;
            this.ResetButton.Text = "Reset";
            this.ResetButton.UseVisualStyleBackColor = false;
            this.ResetButton.Click += new System.EventHandler(this.ResetButton_Click);
            // 
            // MainWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(884, 561);
            this.Controls.Add(this.ResetButton);
            this.Controls.Add(this.algorithmGroupBox);
            this.Controls.Add(this.removeGroupBox);
            this.Controls.Add(this.addGroupBox);
            this.Controls.Add(this.canvas);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(900, 600);
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(900, 600);
            this.Name = "MainWindow";
            this.Text = "Polygon Editor";
            this.Load += new System.EventHandler(this.MainWindow_Load);
            ((System.ComponentModel.ISupportInitialize)(this.canvas)).EndInit();
            this.addGroupBox.ResumeLayout(false);
            this.removeGroupBox.ResumeLayout(false);
            this.algorithmGroupBox.ResumeLayout(false);
            this.algorithmGroupBox.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private PictureBox canvas;
        private GroupBox addGroupBox;
        private GroupBox removeGroupBox;
        private Button AddVertexButton;
        private Button AddPolygonButton;
        private Button RemoveVertexButton;
        private Button RemovePolygonButton;
        private Button AddRelationGivenLengthbutton;
        private Button AddRelationParallelButton;
        private RadioButton BresenhamRadioButton;
        private GroupBox algorithmGroupBox;
        private RadioButton LibraryRadioButton;
        private Button ResetButton;
        private Button RemoveRelationGivenLengthbutton;
        private Button RemoveRelationParallelButton;
    }
}