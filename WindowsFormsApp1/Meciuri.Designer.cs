using System.ComponentModel;

namespace WindowsFormsApp1;

partial class Meciuri
{
    /// <summary>
    /// Required designer variable.
    /// </summary>
    private IContainer components = null;

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
        this.meciuriTable = new System.Windows.Forms.DataGridView();
        this.numeColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
        this.pretBiletColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
        this.dataColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
        this.nrLocuriDisponibileColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
        this.LogoutButton = new System.Windows.Forms.Button();
        this.buttonFiltru = new System.Windows.Forms.Button();
        this.label1 = new System.Windows.Forms.Label();
        this.label2 = new System.Windows.Forms.Label();
        this.nrLocuriSelector = new System.Windows.Forms.NumericUpDown();
        this.numeClientField = new System.Windows.Forms.TextBox();
        this.buttonVanzare = new System.Windows.Forms.Button();
        ((System.ComponentModel.ISupportInitialize)(this.meciuriTable)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(this.nrLocuriSelector)).BeginInit();
        this.SuspendLayout();
        // 
        // meciuriTable
        // 
        this.meciuriTable.AllowUserToDeleteRows = false;
        this.meciuriTable.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
        this.meciuriTable.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] { this.numeColumn, this.pretBiletColumn, this.dataColumn, this.nrLocuriDisponibileColumn });
        this.meciuriTable.Location = new System.Drawing.Point(65, 59);
        this.meciuriTable.Name = "meciuriTable";
        this.meciuriTable.ReadOnly = true;
        this.meciuriTable.RowTemplate.Height = 28;
        this.meciuriTable.Size = new System.Drawing.Size(668, 252);
        this.meciuriTable.TabIndex = 0;
        // 
        // numeColumn
        // 
        this.numeColumn.DataPropertyName = "numeColumn";
        this.numeColumn.HeaderText = "Nume";
        this.numeColumn.Name = "numeColumn";
        this.numeColumn.ReadOnly = true;
        // 
        // pretBiletColumn
        // 
        this.pretBiletColumn.DataPropertyName = "pretBiletColumn";
        this.pretBiletColumn.HeaderText = "Pret Bilet";
        this.pretBiletColumn.Name = "pretBiletColumn";
        this.pretBiletColumn.ReadOnly = true;
        // 
        // dataColumn
        // 
        this.dataColumn.DataPropertyName = "dataColumn";
        this.dataColumn.HeaderText = "Data";
        this.dataColumn.Name = "dataColumn";
        this.dataColumn.ReadOnly = true;
        // 
        // nrLocuriDisponibileColumn
        // 
        this.nrLocuriDisponibileColumn.DataPropertyName = "nrLocuriDisponibileColumn";
        this.nrLocuriDisponibileColumn.HeaderText = "Locuri disponibile";
        this.nrLocuriDisponibileColumn.Name = "nrLocuriDisponibileColumn";
        this.nrLocuriDisponibileColumn.ReadOnly = true;
        // 
        // LogoutButton
        // 
        this.LogoutButton.Location = new System.Drawing.Point(725, 0);
        this.LogoutButton.Name = "LogoutButton";
        this.LogoutButton.Size = new System.Drawing.Size(75, 37);
        this.LogoutButton.TabIndex = 1;
        this.LogoutButton.Text = "Logout";
        this.LogoutButton.UseVisualStyleBackColor = true;
        this.LogoutButton.Click += new System.EventHandler(this.LogoutButton_Click);
        // 
        // buttonFiltru
        // 
        this.buttonFiltru.Location = new System.Drawing.Point(65, 348);
        this.buttonFiltru.Name = "buttonFiltru";
        this.buttonFiltru.Size = new System.Drawing.Size(329, 32);
        this.buttonFiltru.TabIndex = 2;
        this.buttonFiltru.Text = "Doar meciurile la care mai sunt bilete\r\n";
        this.buttonFiltru.UseVisualStyleBackColor = true;
        this.buttonFiltru.Click += new System.EventHandler(this.buttonFiltru_Click);
        // 
        // label1
        // 
        this.label1.Location = new System.Drawing.Point(489, 351);
        this.label1.Name = "label1";
        this.label1.Size = new System.Drawing.Size(100, 23);
        this.label1.TabIndex = 3;
        this.label1.Text = "Nr. bilete : ";
        // 
        // label2
        // 
        this.label2.Location = new System.Drawing.Point(489, 403);
        this.label2.Name = "label2";
        this.label2.Size = new System.Drawing.Size(100, 23);
        this.label2.TabIndex = 4;
        this.label2.Text = "Nume client :\r\n";
        // 
        // nrLocuriSelector
        // 
        this.nrLocuriSelector.Location = new System.Drawing.Point(613, 348);
        this.nrLocuriSelector.Name = "nrLocuriSelector";
        this.nrLocuriSelector.Size = new System.Drawing.Size(120, 26);
        this.nrLocuriSelector.TabIndex = 5;
        // 
        // numeClientField
        // 
        this.numeClientField.Location = new System.Drawing.Point(613, 400);
        this.numeClientField.Name = "numeClientField";
        this.numeClientField.Size = new System.Drawing.Size(120, 26);
        this.numeClientField.TabIndex = 6;
        // 
        // buttonVanzare
        // 
        this.buttonVanzare.Location = new System.Drawing.Point(613, 439);
        this.buttonVanzare.Name = "buttonVanzare";
        this.buttonVanzare.Size = new System.Drawing.Size(120, 29);
        this.buttonVanzare.TabIndex = 7;
        this.buttonVanzare.Text = "Vinde bilet";
        this.buttonVanzare.UseVisualStyleBackColor = true;
        this.buttonVanzare.Click += new System.EventHandler(this.buttonVanzare_Click);
        // 
        // Meciuri
        // 
        this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
        this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
        this.BackColor = System.Drawing.SystemColors.Control;
        this.ClientSize = new System.Drawing.Size(800, 480);
        this.Controls.Add(this.buttonVanzare);
        this.Controls.Add(this.numeClientField);
        this.Controls.Add(this.nrLocuriSelector);
        this.Controls.Add(this.label2);
        this.Controls.Add(this.label1);
        this.Controls.Add(this.buttonFiltru);
        this.Controls.Add(this.LogoutButton);
        this.Controls.Add(this.meciuriTable);
        this.Location = new System.Drawing.Point(15, 15);
        this.Name = "Meciuri";
        this.Text = "Meciuri";
        ((System.ComponentModel.ISupportInitialize)(this.meciuriTable)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(this.nrLocuriSelector)).EndInit();
        this.ResumeLayout(false);
        this.PerformLayout();
    }

    private System.Windows.Forms.Button buttonVanzare;

    private System.Windows.Forms.NumericUpDown nrLocuriSelector;
    private System.Windows.Forms.TextBox numeClientField;

    private System.Windows.Forms.Label label1;
    private System.Windows.Forms.Label label2;

    private System.Windows.Forms.Button buttonFiltru;

    private System.Windows.Forms.Button LogoutButton;

    private System.Windows.Forms.DataGridViewTextBoxColumn numeColumn;
    private System.Windows.Forms.DataGridViewTextBoxColumn pretBiletColumn;
    private System.Windows.Forms.DataGridViewTextBoxColumn dataColumn;
    private System.Windows.Forms.DataGridViewTextBoxColumn nrLocuriDisponibileColumn;

    private System.Windows.Forms.DataGridView meciuriTable;

    #endregion
}