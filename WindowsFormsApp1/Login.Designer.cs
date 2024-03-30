namespace WindowsFormsApp1
{
    partial class Login
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
            this.loginLabel = new System.Windows.Forms.Label();
            this.usernameLabel = new System.Windows.Forms.Label();
            this.parolaLabel = new System.Windows.Forms.Label();
            this.usernameField = new System.Windows.Forms.TextBox();
            this.parolaField = new System.Windows.Forms.TextBox();
            this.loginButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // loginLabel
            // 
            this.loginLabel.Location = new System.Drawing.Point(173, 84);
            this.loginLabel.Name = "loginLabel";
            this.loginLabel.Size = new System.Drawing.Size(151, 30);
            this.loginLabel.TabIndex = 0;
            this.loginLabel.Text = "Login :";
            // 
            // usernameLabel
            // 
            this.usernameLabel.Location = new System.Drawing.Point(110, 177);
            this.usernameLabel.Name = "usernameLabel";
            this.usernameLabel.Size = new System.Drawing.Size(100, 23);
            this.usernameLabel.TabIndex = 1;
            this.usernameLabel.Text = "username :";
            // 
            // parolaLabel
            // 
            this.parolaLabel.Location = new System.Drawing.Point(110, 250);
            this.parolaLabel.Name = "parolaLabel";
            this.parolaLabel.Size = new System.Drawing.Size(100, 23);
            this.parolaLabel.TabIndex = 2;
            this.parolaLabel.Text = "parola :";
            // 
            // usernameField
            // 
            this.usernameField.Location = new System.Drawing.Point(267, 174);
            this.usernameField.Name = "usernameField";
            this.usernameField.Size = new System.Drawing.Size(181, 26);
            this.usernameField.TabIndex = 3;
            // 
            // parolaField
            // 
            this.parolaField.Location = new System.Drawing.Point(267, 247);
            this.parolaField.Name = "parolaField";
            this.parolaField.Size = new System.Drawing.Size(181, 26);
            this.parolaField.TabIndex = 4;
            // 
            // loginButton
            // 
            this.loginButton.Location = new System.Drawing.Point(372, 355);
            this.loginButton.Name = "loginButton";
            this.loginButton.Size = new System.Drawing.Size(75, 31);
            this.loginButton.TabIndex = 5;
            this.loginButton.Text = "Login";
            this.loginButton.UseVisualStyleBackColor = true;
            this.loginButton.Click += new System.EventHandler(this.loginButton_Click);
            // 
            // Login
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.ClientSize = new System.Drawing.Size(545, 446);
            this.Controls.Add(this.loginButton);
            this.Controls.Add(this.parolaField);
            this.Controls.Add(this.usernameField);
            this.Controls.Add(this.parolaLabel);
            this.Controls.Add(this.usernameLabel);
            this.Controls.Add(this.loginLabel);
            this.Location = new System.Drawing.Point(15, 15);
            this.Name = "Login";
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        private System.Windows.Forms.TextBox usernameField;
        private System.Windows.Forms.TextBox parolaField;
        private System.Windows.Forms.Button loginButton;

        private System.Windows.Forms.Label parolaLabel;

        private System.Windows.Forms.Label usernameLabel;

        private System.Windows.Forms.Label loginLabel;

        #endregion
    }
}