﻿namespace Chess
{
    partial class GameForm
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
            GamePanel = new Panel();
            SuspendLayout();
            // 
            // GamePanel
            // 
            GamePanel.Location = new Point(160, 15);
            GamePanel.Name = "GamePanel";
            GamePanel.Size = new Size(480, 480);
            GamePanel.TabIndex = 0;
            // 
            // GameForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(784, 511);
            Controls.Add(GamePanel);
            Name = "GameForm";
            Text = "Шахматы";
            ResumeLayout(false);
        }

        #endregion

        private Panel GamePanel;
    }
}