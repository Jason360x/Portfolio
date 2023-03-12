namespace JasonsRPGCharacterTool
{
    partial class rpgChoice
    {
        /// <summary>
        /// Erforderliche Designervariable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Verwendete Ressourcen bereinigen.
        /// </summary>
        /// <param name="disposing">True, wenn verwaltete Ressourcen gelöscht werden sollen; andernfalls False.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Vom Windows Form-Designer generierter Code

        /// <summary>
        /// Erforderliche Methode für die Designerunterstützung.
        /// Der Inhalt der Methode darf nicht mit dem Code-Editor geändert werden.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(rpgChoice));
            this.rpgChoiceText = new System.Windows.Forms.Label();
            this.RPGToolText = new System.Windows.Forms.Label();
            this.tesPic = new System.Windows.Forms.PictureBox();
            this.asteriusPic = new System.Windows.Forms.PictureBox();
            this.tesLabelUp = new System.Windows.Forms.Label();
            this.tesLabelCred = new System.Windows.Forms.Label();
            this.asteriusLabelCred = new System.Windows.Forms.Label();
            this.asteriusLabelUp = new System.Windows.Forms.Label();
            this.exitText = new System.Windows.Forms.Label();
            this.versionText = new System.Windows.Forms.Label();
            this.shopPic = new System.Windows.Forms.PictureBox();
            this.shopToolLabel = new System.Windows.Forms.Label();
            this.shopToolDescript = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.tesPic)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.asteriusPic)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.shopPic)).BeginInit();
            this.SuspendLayout();
            // 
            // rpgChoiceText
            // 
            resources.ApplyResources(this.rpgChoiceText, "rpgChoiceText");
            this.rpgChoiceText.Name = "rpgChoiceText";
            // 
            // RPGToolText
            // 
            this.RPGToolText.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(137)))), ((int)(((byte)(239)))));
            resources.ApplyResources(this.RPGToolText, "RPGToolText");
            this.RPGToolText.Image = global::JasonsRPGCharacterTool.Properties.Resources.Background;
            this.RPGToolText.Name = "RPGToolText";
            this.RPGToolText.Tag = "KeepColor";
            this.RPGToolText.Click += new System.EventHandler(this.RPGToolText_Click);
            this.RPGToolText.MouseDown += new System.Windows.Forms.MouseEventHandler(this.RPGToolText_MouseDown);
            // 
            // tesPic
            // 
            this.tesPic.Image = global::JasonsRPGCharacterTool.Properties.Resources.TESPnPLogoAlternative1;
            resources.ApplyResources(this.tesPic, "tesPic");
            this.tesPic.Name = "tesPic";
            this.tesPic.TabStop = false;
            this.tesPic.Click += new System.EventHandler(this.tesPic_Click);
            // 
            // asteriusPic
            // 
            resources.ApplyResources(this.asteriusPic, "asteriusPic");
            this.asteriusPic.Image = global::JasonsRPGCharacterTool.Properties.Resources.AsteriusPnPLogoFULLSIZE;
            this.asteriusPic.Name = "asteriusPic";
            this.asteriusPic.TabStop = false;
            this.asteriusPic.Click += new System.EventHandler(this.asteriusPic_Click);
            // 
            // tesLabelUp
            // 
            resources.ApplyResources(this.tesLabelUp, "tesLabelUp");
            this.tesLabelUp.Name = "tesLabelUp";
            this.tesLabelUp.Tag = "IsBold";
            // 
            // tesLabelCred
            // 
            resources.ApplyResources(this.tesLabelCred, "tesLabelCred");
            this.tesLabelCred.Name = "tesLabelCred";
            // 
            // asteriusLabelCred
            // 
            resources.ApplyResources(this.asteriusLabelCred, "asteriusLabelCred");
            this.asteriusLabelCred.Name = "asteriusLabelCred";
            // 
            // asteriusLabelUp
            // 
            resources.ApplyResources(this.asteriusLabelUp, "asteriusLabelUp");
            this.asteriusLabelUp.Name = "asteriusLabelUp";
            this.asteriusLabelUp.Tag = "IsBold";
            // 
            // exitText
            // 
            this.exitText.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(231)))), ((int)(((byte)(78)))), ((int)(((byte)(61)))));
            resources.ApplyResources(this.exitText, "exitText");
            this.exitText.Name = "exitText";
            this.exitText.Tag = "KeepColor";
            this.exitText.Click += new System.EventHandler(this.exitText_Click);
            this.exitText.MouseEnter += new System.EventHandler(this.exitText_MouseEnter);
            this.exitText.MouseLeave += new System.EventHandler(this.exitText_MouseLeave);
            // 
            // versionText
            // 
            resources.ApplyResources(this.versionText, "versionText");
            this.versionText.Name = "versionText";
            this.versionText.Tag = "KeepColor";
            // 
            // shopPic
            // 
            this.shopPic.Image = global::JasonsRPGCharacterTool.Properties.Resources.shopToolALT2;
            resources.ApplyResources(this.shopPic, "shopPic");
            this.shopPic.Name = "shopPic";
            this.shopPic.TabStop = false;
            this.shopPic.Click += new System.EventHandler(this.shopPic_Click);
            // 
            // shopToolLabel
            // 
            resources.ApplyResources(this.shopToolLabel, "shopToolLabel");
            this.shopToolLabel.Name = "shopToolLabel";
            this.shopToolLabel.Tag = "IsBold";
            // 
            // shopToolDescript
            // 
            resources.ApplyResources(this.shopToolDescript, "shopToolDescript");
            this.shopToolDescript.Name = "shopToolDescript";
            // 
            // rpgChoice
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ControlBox = false;
            this.Controls.Add(this.shopToolDescript);
            this.Controls.Add(this.shopToolLabel);
            this.Controls.Add(this.shopPic);
            this.Controls.Add(this.versionText);
            this.Controls.Add(this.exitText);
            this.Controls.Add(this.asteriusLabelCred);
            this.Controls.Add(this.asteriusLabelUp);
            this.Controls.Add(this.tesLabelCred);
            this.Controls.Add(this.tesLabelUp);
            this.Controls.Add(this.asteriusPic);
            this.Controls.Add(this.tesPic);
            this.Controls.Add(this.RPGToolText);
            this.Controls.Add(this.rpgChoiceText);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "rpgChoice";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            ((System.ComponentModel.ISupportInitialize)(this.tesPic)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.asteriusPic)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.shopPic)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label rpgChoiceText;
        private System.Windows.Forms.Label RPGToolText;
        private System.Windows.Forms.PictureBox tesPic;
        private System.Windows.Forms.PictureBox asteriusPic;
        private System.Windows.Forms.Label tesLabelUp;
        private System.Windows.Forms.Label tesLabelCred;
        private System.Windows.Forms.Label asteriusLabelCred;
        private System.Windows.Forms.Label asteriusLabelUp;
        private System.Windows.Forms.Label exitText;
        private System.Windows.Forms.Label versionText;
        private System.Windows.Forms.PictureBox shopPic;
        private System.Windows.Forms.Label shopToolLabel;
        private System.Windows.Forms.Label shopToolDescript;
    }
}

