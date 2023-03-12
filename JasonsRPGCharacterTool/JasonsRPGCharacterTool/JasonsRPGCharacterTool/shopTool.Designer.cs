namespace JasonsRPGCharacterTool
{
    partial class shopTool
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(shopTool));
            this.RPGShopToolText = new System.Windows.Forms.Label();
            this.shopPanel = new System.Windows.Forms.Panel();
            this.versionText = new System.Windows.Forms.Label();
            this.shopPriceText = new System.Windows.Forms.Label();
            this.shopAmountText = new System.Windows.Forms.Label();
            this.shopNameText = new System.Windows.Forms.Label();
            this.shopTable = new System.Windows.Forms.TableLayoutPanel();
            this.shopTESSaveButton = new System.Windows.Forms.Button();
            this.exitButton = new System.Windows.Forms.Button();
            this.backButton = new System.Windows.Forms.Button();
            this.shopAsteriusSaveButton = new System.Windows.Forms.Button();
            this.shopLoadSaveButton = new System.Windows.Forms.Button();
            this.shopPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // RPGShopToolText
            // 
            this.RPGShopToolText.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(137)))), ((int)(((byte)(239)))));
            this.RPGShopToolText.Font = new System.Drawing.Font("Segoe UI", 27.75F);
            this.RPGShopToolText.Image = global::JasonsRPGCharacterTool.Properties.Resources.Background;
            this.RPGShopToolText.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.RPGShopToolText.Location = new System.Drawing.Point(0, 0);
            this.RPGShopToolText.Name = "RPGShopToolText";
            this.RPGShopToolText.Size = new System.Drawing.Size(1000, 56);
            this.RPGShopToolText.TabIndex = 11;
            this.RPGShopToolText.Text = "Jason\'s RPG Shop Tool";
            this.RPGShopToolText.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.RPGShopToolText.Click += new System.EventHandler(this.RPGToolText_Click);
            this.RPGShopToolText.MouseDown += new System.Windows.Forms.MouseEventHandler(this.RPGToolText_MouseDown);
            // 
            // shopPanel
            // 
            this.shopPanel.Controls.Add(this.versionText);
            this.shopPanel.Controls.Add(this.shopPriceText);
            this.shopPanel.Controls.Add(this.shopAmountText);
            this.shopPanel.Controls.Add(this.shopNameText);
            this.shopPanel.Controls.Add(this.shopTable);
            this.shopPanel.Location = new System.Drawing.Point(9, 60);
            this.shopPanel.Name = "shopPanel";
            this.shopPanel.Size = new System.Drawing.Size(963, 499);
            this.shopPanel.TabIndex = 13;
            // 
            // versionText
            // 
            this.versionText.Font = new System.Drawing.Font("Segoe UI Light", 6.75F, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.versionText.Location = new System.Drawing.Point(798, 0);
            this.versionText.Name = "versionText";
            this.versionText.Size = new System.Drawing.Size(165, 18);
            this.versionText.TabIndex = 4;
            this.versionText.Tag = "";
            this.versionText.Text = "version";
            this.versionText.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // shopPriceText
            // 
            this.shopPriceText.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.shopPriceText.Location = new System.Drawing.Point(708, 9);
            this.shopPriceText.Name = "shopPriceText";
            this.shopPriceText.Size = new System.Drawing.Size(229, 23);
            this.shopPriceText.TabIndex = 3;
            this.shopPriceText.Text = "shopPrice";
            // 
            // shopAmountText
            // 
            this.shopAmountText.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.shopAmountText.Location = new System.Drawing.Point(476, 9);
            this.shopAmountText.Name = "shopAmountText";
            this.shopAmountText.Size = new System.Drawing.Size(229, 23);
            this.shopAmountText.TabIndex = 2;
            this.shopAmountText.Text = "shopAmount";
            // 
            // shopNameText
            // 
            this.shopNameText.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.shopNameText.Location = new System.Drawing.Point(9, 9);
            this.shopNameText.Name = "shopNameText";
            this.shopNameText.Size = new System.Drawing.Size(471, 23);
            this.shopNameText.TabIndex = 1;
            this.shopNameText.Text = "shopName";
            // 
            // shopTable
            // 
            this.shopTable.AutoScroll = true;
            this.shopTable.AutoScrollMinSize = new System.Drawing.Size(0, 33);
            this.shopTable.CellBorderStyle = System.Windows.Forms.TableLayoutPanelCellBorderStyle.Single;
            this.shopTable.ColumnCount = 3;
            this.shopTable.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.shopTable.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.shopTable.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.shopTable.Location = new System.Drawing.Point(11, 33);
            this.shopTable.Name = "shopTable";
            this.shopTable.RowCount = 14;
            this.shopTable.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 33F));
            this.shopTable.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 33F));
            this.shopTable.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 33F));
            this.shopTable.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 33F));
            this.shopTable.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 33F));
            this.shopTable.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 33F));
            this.shopTable.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 33F));
            this.shopTable.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 33F));
            this.shopTable.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 33F));
            this.shopTable.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 33F));
            this.shopTable.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 33F));
            this.shopTable.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 33F));
            this.shopTable.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 33F));
            this.shopTable.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 33F));
            this.shopTable.Size = new System.Drawing.Size(949, 462);
            this.shopTable.TabIndex = 0;
            // 
            // shopTESSaveButton
            // 
            this.shopTESSaveButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(138)))), ((int)(((byte)(0)))));
            this.shopTESSaveButton.FlatAppearance.BorderSize = 0;
            this.shopTESSaveButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.shopTESSaveButton.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.shopTESSaveButton.Location = new System.Drawing.Point(566, 0);
            this.shopTESSaveButton.Name = "shopTESSaveButton";
            this.shopTESSaveButton.Size = new System.Drawing.Size(75, 56);
            this.shopTESSaveButton.TabIndex = 14;
            this.shopTESSaveButton.Text = "shopTESSave";
            this.shopTESSaveButton.UseVisualStyleBackColor = false;
            this.shopTESSaveButton.Click += new System.EventHandler(this.shopTESSaveButton_Click);
            // 
            // exitButton
            // 
            this.exitButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(231)))), ((int)(((byte)(78)))), ((int)(((byte)(61)))));
            this.exitButton.FlatAppearance.BorderSize = 0;
            this.exitButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.exitButton.Font = new System.Drawing.Font("Segoe UI", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.exitButton.Location = new System.Drawing.Point(882, 0);
            this.exitButton.Name = "exitButton";
            this.exitButton.Size = new System.Drawing.Size(102, 56);
            this.exitButton.TabIndex = 15;
            this.exitButton.Text = "exitButton";
            this.exitButton.UseVisualStyleBackColor = false;
            this.exitButton.Click += new System.EventHandler(this.exitButton_Click);
            // 
            // backButton
            // 
            this.backButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(53)))), ((int)(((byte)(153)))), ((int)(((byte)(219)))));
            this.backButton.FlatAppearance.BorderSize = 0;
            this.backButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.backButton.Font = new System.Drawing.Font("Segoe UI", 27.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.backButton.Location = new System.Drawing.Point(830, 0);
            this.backButton.Name = "backButton";
            this.backButton.Size = new System.Drawing.Size(55, 56);
            this.backButton.TabIndex = 141;
            this.backButton.Text = "↩";
            this.backButton.UseVisualStyleBackColor = false;
            this.backButton.Click += new System.EventHandler(this.backButton_Click);
            // 
            // shopAsteriusSaveButton
            // 
            this.shopAsteriusSaveButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(250)))), ((int)(((byte)(104)))), ((int)(((byte)(0)))));
            this.shopAsteriusSaveButton.FlatAppearance.BorderSize = 0;
            this.shopAsteriusSaveButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.shopAsteriusSaveButton.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.shopAsteriusSaveButton.Location = new System.Drawing.Point(641, 0);
            this.shopAsteriusSaveButton.Name = "shopAsteriusSaveButton";
            this.shopAsteriusSaveButton.Size = new System.Drawing.Size(75, 56);
            this.shopAsteriusSaveButton.TabIndex = 142;
            this.shopAsteriusSaveButton.Text = "shopAsteriusSave";
            this.shopAsteriusSaveButton.UseVisualStyleBackColor = false;
            this.shopAsteriusSaveButton.Click += new System.EventHandler(this.shopAsteriusSaveButton_Click);
            // 
            // shopLoadSaveButton
            // 
            this.shopLoadSaveButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(80)))), ((int)(((byte)(239)))));
            this.shopLoadSaveButton.FlatAppearance.BorderSize = 0;
            this.shopLoadSaveButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.shopLoadSaveButton.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.shopLoadSaveButton.Location = new System.Drawing.Point(491, 0);
            this.shopLoadSaveButton.Name = "shopLoadSaveButton";
            this.shopLoadSaveButton.Size = new System.Drawing.Size(75, 56);
            this.shopLoadSaveButton.TabIndex = 143;
            this.shopLoadSaveButton.Text = "shopLoadSave";
            this.shopLoadSaveButton.UseVisualStyleBackColor = false;
            this.shopLoadSaveButton.Click += new System.EventHandler(this.shopLoadSaveButton_Click);
            // 
            // shopTool
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(984, 571);
            this.ControlBox = false;
            this.Controls.Add(this.shopLoadSaveButton);
            this.Controls.Add(this.shopAsteriusSaveButton);
            this.Controls.Add(this.backButton);
            this.Controls.Add(this.shopTESSaveButton);
            this.Controls.Add(this.shopPanel);
            this.Controls.Add(this.exitButton);
            this.Controls.Add(this.RPGShopToolText);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "shopTool";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "shopTool";
            this.shopPanel.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Label RPGShopToolText;
        private System.Windows.Forms.Panel shopPanel;
        private System.Windows.Forms.Label shopNameText;
        private System.Windows.Forms.TableLayoutPanel shopTable;
        private System.Windows.Forms.Label shopPriceText;
        private System.Windows.Forms.Label shopAmountText;
        private System.Windows.Forms.Button shopTESSaveButton;
        private System.Windows.Forms.Button exitButton;
        private System.Windows.Forms.Button backButton;
        private System.Windows.Forms.Button shopAsteriusSaveButton;
        private System.Windows.Forms.Button shopLoadSaveButton;
        private System.Windows.Forms.Label versionText;
    }
}