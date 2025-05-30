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
            button3 = new Button();
            cmbSerializeExtension = new ComboBox();
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
            // button3
            // 
            button3.Location = new Point(430, 400);
            button3.Name = "button3";
            button3.Size = new Size(150, 50);
            button3.TabIndex = 2;
            button3.Text = "Выбрать папку";
            button3.UseVisualStyleBackColor = true;
            // 
            // cmbSerializeExtension
            // 
            cmbSerializeExtension.FormattingEnabled = true;
            cmbSerializeExtension.Items.AddRange(new object[] { "JSON", "XML" });
            cmbSerializeExtension.Location = new Point(600, 415);
            cmbSerializeExtension.Name = "cmbSerializeExtension";
            cmbSerializeExtension.Size = new Size(151, 23);
            cmbSerializeExtension.TabIndex = 3;
            cmbSerializeExtension.Text = "Формат сериализации ";
            // 
            // MenuForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(784, 511);
            Controls.Add(cmbSerializeExtension);
            Controls.Add(button3);
            Controls.Add(btnResumeGame);
            Controls.Add(btnNewGame);
            Name = "MenuForm";
            Text = "Form1";
            ResumeLayout(false);
        }

        #endregion

        private Button btnNewGame;
        private Button btnResumeGame;
        private Button button3;
        private ComboBox cmbSerializeExtension;
    }
}
