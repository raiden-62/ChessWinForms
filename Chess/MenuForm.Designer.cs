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
            btnSelectFile = new Button();
            cmbSerialization = new ComboBox();
            txtFolderPath = new TextBox();
            txtSerializationFormat = new TextBox();
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
            // 
            // btnResumeGame
            // 
            btnResumeGame.Enabled = false;
            btnResumeGame.Location = new Point(300, 260);
            btnResumeGame.Name = "btnResumeGame";
            btnResumeGame.Size = new Size(200, 50);
            btnResumeGame.TabIndex = 1;
            btnResumeGame.Text = "Продолжить игру";
            btnResumeGame.UseVisualStyleBackColor = true;
            // 
            // btnSelectFile
            // 
            btnSelectFile.Location = new Point(430, 400);
            btnSelectFile.Name = "btnSelectFile";
            btnSelectFile.Size = new Size(150, 50);
            btnSelectFile.TabIndex = 2;
            btnSelectFile.Text = "Выбрать файл сохранения";
            btnSelectFile.UseVisualStyleBackColor = true;
            // 
            // cmbSerialization
            // 
            cmbSerialization.Location = new Point(623, 415);
            cmbSerialization.Name = "cmbSerialization";
            cmbSerialization.Size = new Size(82, 23);
            cmbSerialization.TabIndex = 3;
            // 
            // txtFolderPath
            // 
            txtFolderPath.Location = new Point(239, 456);
            txtFolderPath.Name = "txtFolderPath";
            txtFolderPath.ReadOnly = true;
            txtFolderPath.Size = new Size(533, 23);
            txtFolderPath.TabIndex = 4;
            txtFolderPath.Text = "Выбранный файл";
            txtFolderPath.TextAlign = HorizontalAlignment.Center;
            // 
            // txtSerializationFormat
            // 
            txtSerializationFormat.Location = new Point(623, 386);
            txtSerializationFormat.Name = "txtSerializationFormat";
            txtSerializationFormat.Size = new Size(134, 23);
            txtSerializationFormat.TabIndex = 5;
            txtSerializationFormat.Text = "Формат сериализации";
            // 
            // MenuForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(784, 511);
            Controls.Add(txtSerializationFormat);
            Controls.Add(txtFolderPath);
            Controls.Add(cmbSerialization);
            Controls.Add(btnSelectFile);
            Controls.Add(btnResumeGame);
            Controls.Add(btnNewGame);
            Name = "MenuForm";
            Text = "Шахматы";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button btnNewGame;
        private Button btnResumeGame;
        private Button btnSelectFile;
        private ComboBox cmbSerializeExtension;
        private ComboBox cmbSerialization;
        private TextBox txtFolderPath;
        private TextBox txtSerializationFormat;
    }
}
