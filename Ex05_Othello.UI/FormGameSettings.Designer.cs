namespace Ex05_Othello.UI
{
    partial class FormGameSettings
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
            this.buttonPlayAgianstComputer = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.buttonPlayAgianstFriend = new System.Windows.Forms.Button();
            this.buttonChangeBoardSize = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // buttonPlayAgianstComputer
            // 
            this.buttonPlayAgianstComputer.Location = new System.Drawing.Point(9, 98);
            this.buttonPlayAgianstComputer.Margin = new System.Windows.Forms.Padding(2);
            this.buttonPlayAgianstComputer.Name = "buttonPlayAgianstComputer";
            this.buttonPlayAgianstComputer.Size = new System.Drawing.Size(154, 41);
            this.buttonPlayAgianstComputer.TabIndex = 0;
            this.buttonPlayAgianstComputer.Text = "Play agianst the computer";
            this.buttonPlayAgianstComputer.UseVisualStyleBackColor = true;
            this.buttonPlayAgianstComputer.Click += new System.EventHandler(this.buttonPlayAgianstComputer_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(364, 214);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 23);
            this.button2.TabIndex = 1;
            this.button2.Text = "button2";
            this.button2.UseVisualStyleBackColor = true;
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(127, 214);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(75, 23);
            this.button3.TabIndex = 2;
            this.button3.Text = "button3";
            this.button3.UseVisualStyleBackColor = true;
            // 
            // buttonPlayAgianstFriend
            // 
            this.buttonPlayAgianstFriend.Location = new System.Drawing.Point(179, 98);
            this.buttonPlayAgianstFriend.Margin = new System.Windows.Forms.Padding(2);
            this.buttonPlayAgianstFriend.Name = "buttonPlayAgianstFriend";
            this.buttonPlayAgianstFriend.Size = new System.Drawing.Size(154, 41);
            this.buttonPlayAgianstFriend.TabIndex = 3;
            this.buttonPlayAgianstFriend.Text = "Play agianst your friend";
            this.buttonPlayAgianstFriend.UseVisualStyleBackColor = true;
            this.buttonPlayAgianstFriend.Click += new System.EventHandler(this.buttonPlayAgianstFriend_Click);
            // 
            // buttonChangeBoardSize
            // 
            this.buttonChangeBoardSize.Location = new System.Drawing.Point(9, 20);
            this.buttonChangeBoardSize.Margin = new System.Windows.Forms.Padding(2);
            this.buttonChangeBoardSize.Name = "buttonChangeBoardSize";
            this.buttonChangeBoardSize.Size = new System.Drawing.Size(324, 41);
            this.buttonChangeBoardSize.TabIndex = 4;
            this.buttonChangeBoardSize.Text = "Board Size: 6x6 (click to increase)";
            this.buttonChangeBoardSize.UseVisualStyleBackColor = true;
            this.buttonChangeBoardSize.Click += new System.EventHandler(this.buttonChangeBoardSize_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.ClientSize = new System.Drawing.Size(344, 161);
            this.Controls.Add(this.buttonChangeBoardSize);
            this.Controls.Add(this.buttonPlayAgianstFriend);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.buttonPlayAgianstComputer);
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Othello - Game Settings";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button buttonPlayAgianstComputer;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Button buttonPlayAgianstFriend;
        private System.Windows.Forms.Button buttonChangeBoardSize;
    }
}

