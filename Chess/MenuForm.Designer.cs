namespace Chess
{
    partial class MenuForm
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
            btnNewGame = new Button();
            btnResumeGame = new Button();
            btnSelectFolder = new Button();
            cmbSerialization = new ComboBox();
            txtFolderPath = new TextBox();
            SuspendLayout();
            // 
            // btnNewGame
            // 
            btnNewGame.Location = new Point(300, 180);
            btnNewGame.Name = "btnNewGame";
            btnNewGame.Size = new Size(200, 50);
            btnNewGame.TabIndex = 0;
            btnNewGame.Text = "Новая игра";
            btnNewGame.UseVisualStyleBackColor = true;
            btnNewGame.Click += StartNewGame;
            // 
            // btnResumeGame
            // 
            btnResumeGame.Location = new Point(300, 260);
            btnResumeGame.Name = "btnResumeGame";
            btnResumeGame.Size = new Size(200, 50);
            btnResumeGame.TabIndex = 1;
            btnResumeGame.Text = "Продолжить игру";
            btnResumeGame.UseVisualStyleBackColor = true;
            btnResumeGame.Click += ResumeGame;
            // 
            // btnSelectFolder
            // 
            btnSelectFolder.Location = new Point(430, 400);
            btnSelectFolder.Name = "btnSelectFolder";
            btnSelectFolder.Size = new Size(150, 50);
            btnSelectFolder.TabIndex = 2;
            btnSelectFolder.Text = "Выбрать папку";
            btnSelectFolder.UseVisualStyleBackColor = true;
            // 
            // cmbSerialization
            // 
            cmbSerialization.Location = new Point(623, 415);
            cmbSerialization.Name = "cmbSerialization";
            cmbSerialization.Size = new Size(121, 23);
            cmbSerialization.TabIndex = 3;
            // 
            // txtFolderPath
            // 
            txtFolderPath.Location = new Point(375, 456);
            txtFolderPath.Name = "txtFolderPath";
            txtFolderPath.Size = new Size(257, 23);
            txtFolderPath.TabIndex = 4;
            // 
            // MenuForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(784, 511);
            Controls.Add(txtFolderPath);
            Controls.Add(cmbSerialization);
            Controls.Add(btnSelectFolder);
            Controls.Add(btnResumeGame);
            Controls.Add(btnNewGame);
            Name = "MenuForm";
            Text = "Chess Menu";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button btnNewGame;
        private Button btnResumeGame;
        private Button btnSelectFolder;
        private ComboBox cmbSerializeExtension;
        private ComboBox cmbSerialization;
        private TextBox txtFolderPath;
    }
}
