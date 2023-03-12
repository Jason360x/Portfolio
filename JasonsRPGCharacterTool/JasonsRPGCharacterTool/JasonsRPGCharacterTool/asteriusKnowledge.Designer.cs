namespace JasonsRPGCharacterTool
{
    partial class asteriusKnowledge
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
            this.knowledgeFlowPanel = new System.Windows.Forms.FlowLayoutPanel();
            this.charKnowledge = new System.Windows.Forms.Label();
            this.charKnowledgeGuns = new System.Windows.Forms.Label();
            this.charKnowledgeUnarmed = new System.Windows.Forms.Label();
            this.charKnowledgeExplosives = new System.Windows.Forms.Label();
            this.charKnowledgePolitics = new System.Windows.Forms.Label();
            this.charKnowledgePlants = new System.Windows.Forms.Label();
            this.charKnowledgeSpaceships = new System.Windows.Forms.Label();
            this.charKnowledgeTraps = new System.Windows.Forms.Label();
            this.charKnowledgeArchitecture = new System.Windows.Forms.Label();
            this.charKnowledgeMedicine = new System.Windows.Forms.Label();
            this.charKnowledgeHistory = new System.Windows.Forms.Label();
            this.charKnowledgeTech = new System.Windows.Forms.Label();
            this.charKnowledgeComputers = new System.Windows.Forms.Label();
            this.charKnowledgeBionic = new System.Windows.Forms.Label();
            this.charKnowledgeCriminality = new System.Windows.Forms.Label();
            this.charKnowledgeAstronomy = new System.Windows.Forms.Label();
            this.charKnowledgeSciences = new System.Windows.Forms.Label();
            this.charKnowledgeMilitary = new System.Windows.Forms.Label();
            this.languages = new System.Windows.Forms.Label();
            this.charLanguagesUSL = new System.Windows.Forms.Label();
            this.charLanguagesFornsh = new System.Windows.Forms.Label();
            this.charLanguagesMekish = new System.Windows.Forms.Label();
            this.charLanguagesLop = new System.Windows.Forms.Label();
            this.charLanguagesVulsh = new System.Windows.Forms.Label();
            this.knowledgeLanguageText = new System.Windows.Forms.Label();
            this.knowledgeFlowPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // knowledgeFlowPanel
            // 
            this.knowledgeFlowPanel.AutoScroll = true;
            this.knowledgeFlowPanel.BackColor = System.Drawing.SystemColors.ControlLight;
            this.knowledgeFlowPanel.Controls.Add(this.charKnowledge);
            this.knowledgeFlowPanel.Controls.Add(this.charKnowledgeGuns);
            this.knowledgeFlowPanel.Controls.Add(this.charKnowledgeUnarmed);
            this.knowledgeFlowPanel.Controls.Add(this.charKnowledgeExplosives);
            this.knowledgeFlowPanel.Controls.Add(this.charKnowledgePolitics);
            this.knowledgeFlowPanel.Controls.Add(this.charKnowledgePlants);
            this.knowledgeFlowPanel.Controls.Add(this.charKnowledgeSpaceships);
            this.knowledgeFlowPanel.Controls.Add(this.charKnowledgeTraps);
            this.knowledgeFlowPanel.Controls.Add(this.charKnowledgeArchitecture);
            this.knowledgeFlowPanel.Controls.Add(this.charKnowledgeMedicine);
            this.knowledgeFlowPanel.Controls.Add(this.charKnowledgeHistory);
            this.knowledgeFlowPanel.Controls.Add(this.charKnowledgeTech);
            this.knowledgeFlowPanel.Controls.Add(this.charKnowledgeComputers);
            this.knowledgeFlowPanel.Controls.Add(this.charKnowledgeBionic);
            this.knowledgeFlowPanel.Controls.Add(this.charKnowledgeCriminality);
            this.knowledgeFlowPanel.Controls.Add(this.charKnowledgeAstronomy);
            this.knowledgeFlowPanel.Controls.Add(this.charKnowledgeSciences);
            this.knowledgeFlowPanel.Controls.Add(this.charKnowledgeMilitary);
            this.knowledgeFlowPanel.Controls.Add(this.languages);
            this.knowledgeFlowPanel.Controls.Add(this.charLanguagesUSL);
            this.knowledgeFlowPanel.Controls.Add(this.charLanguagesFornsh);
            this.knowledgeFlowPanel.Controls.Add(this.charLanguagesMekish);
            this.knowledgeFlowPanel.Controls.Add(this.charLanguagesLop);
            this.knowledgeFlowPanel.Controls.Add(this.charLanguagesVulsh);
            this.knowledgeFlowPanel.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.knowledgeFlowPanel.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.knowledgeFlowPanel.Location = new System.Drawing.Point(0, 22);
            this.knowledgeFlowPanel.Name = "knowledgeFlowPanel";
            this.knowledgeFlowPanel.Size = new System.Drawing.Size(300, 378);
            this.knowledgeFlowPanel.TabIndex = 0;
            this.knowledgeFlowPanel.WrapContents = false;
            // 
            // charKnowledge
            // 
            this.charKnowledge.Font = new System.Drawing.Font("Segoe UI Semibold", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.charKnowledge.Location = new System.Drawing.Point(3, 0);
            this.charKnowledge.Name = "charKnowledge";
            this.charKnowledge.Size = new System.Drawing.Size(270, 20);
            this.charKnowledge.TabIndex = 167;
            this.charKnowledge.Tag = "IsBold";
            this.charKnowledge.Text = "knowledge";
            this.charKnowledge.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // charKnowledgeGuns
            // 
            this.charKnowledgeGuns.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.charKnowledgeGuns.Location = new System.Drawing.Point(3, 20);
            this.charKnowledgeGuns.Name = "charKnowledgeGuns";
            this.charKnowledgeGuns.Size = new System.Drawing.Size(270, 20);
            this.charKnowledgeGuns.TabIndex = 166;
            this.charKnowledgeGuns.Text = "charKnowledgeGuns";
            this.charKnowledgeGuns.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.charKnowledgeGuns.MouseClick += new System.Windows.Forms.MouseEventHandler(this.KnowledgeBonus);
            // 
            // charKnowledgeUnarmed
            // 
            this.charKnowledgeUnarmed.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.charKnowledgeUnarmed.Location = new System.Drawing.Point(3, 40);
            this.charKnowledgeUnarmed.Name = "charKnowledgeUnarmed";
            this.charKnowledgeUnarmed.Size = new System.Drawing.Size(270, 20);
            this.charKnowledgeUnarmed.TabIndex = 168;
            this.charKnowledgeUnarmed.Text = "charKnowledgeUnarmed";
            this.charKnowledgeUnarmed.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.charKnowledgeUnarmed.MouseClick += new System.Windows.Forms.MouseEventHandler(this.KnowledgeBonus);
            // 
            // charKnowledgeExplosives
            // 
            this.charKnowledgeExplosives.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.charKnowledgeExplosives.Location = new System.Drawing.Point(3, 60);
            this.charKnowledgeExplosives.Name = "charKnowledgeExplosives";
            this.charKnowledgeExplosives.Size = new System.Drawing.Size(270, 20);
            this.charKnowledgeExplosives.TabIndex = 169;
            this.charKnowledgeExplosives.Text = "charKnowledgeExplosives";
            this.charKnowledgeExplosives.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.charKnowledgeExplosives.MouseClick += new System.Windows.Forms.MouseEventHandler(this.KnowledgeBonus);
            // 
            // charKnowledgePolitics
            // 
            this.charKnowledgePolitics.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.charKnowledgePolitics.Location = new System.Drawing.Point(3, 80);
            this.charKnowledgePolitics.Name = "charKnowledgePolitics";
            this.charKnowledgePolitics.Size = new System.Drawing.Size(270, 20);
            this.charKnowledgePolitics.TabIndex = 170;
            this.charKnowledgePolitics.Text = "charKnowledgePolitics";
            this.charKnowledgePolitics.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.charKnowledgePolitics.MouseClick += new System.Windows.Forms.MouseEventHandler(this.KnowledgeBonus);
            // 
            // charKnowledgePlants
            // 
            this.charKnowledgePlants.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.charKnowledgePlants.Location = new System.Drawing.Point(3, 100);
            this.charKnowledgePlants.Name = "charKnowledgePlants";
            this.charKnowledgePlants.Size = new System.Drawing.Size(270, 20);
            this.charKnowledgePlants.TabIndex = 171;
            this.charKnowledgePlants.Text = "charKnowledgePlants";
            this.charKnowledgePlants.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.charKnowledgePlants.MouseClick += new System.Windows.Forms.MouseEventHandler(this.KnowledgeBonus);
            // 
            // charKnowledgeSpaceships
            // 
            this.charKnowledgeSpaceships.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.charKnowledgeSpaceships.Location = new System.Drawing.Point(3, 120);
            this.charKnowledgeSpaceships.Name = "charKnowledgeSpaceships";
            this.charKnowledgeSpaceships.Size = new System.Drawing.Size(270, 20);
            this.charKnowledgeSpaceships.TabIndex = 172;
            this.charKnowledgeSpaceships.Text = "charKnowledgeSpaceships";
            this.charKnowledgeSpaceships.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.charKnowledgeSpaceships.MouseClick += new System.Windows.Forms.MouseEventHandler(this.KnowledgeBonus);
            // 
            // charKnowledgeTraps
            // 
            this.charKnowledgeTraps.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.charKnowledgeTraps.Location = new System.Drawing.Point(3, 140);
            this.charKnowledgeTraps.Name = "charKnowledgeTraps";
            this.charKnowledgeTraps.Size = new System.Drawing.Size(270, 20);
            this.charKnowledgeTraps.TabIndex = 173;
            this.charKnowledgeTraps.Text = "charKnowledgeTraps";
            this.charKnowledgeTraps.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.charKnowledgeTraps.MouseClick += new System.Windows.Forms.MouseEventHandler(this.KnowledgeBonus);
            // 
            // charKnowledgeArchitecture
            // 
            this.charKnowledgeArchitecture.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.charKnowledgeArchitecture.Location = new System.Drawing.Point(3, 160);
            this.charKnowledgeArchitecture.Name = "charKnowledgeArchitecture";
            this.charKnowledgeArchitecture.Size = new System.Drawing.Size(270, 20);
            this.charKnowledgeArchitecture.TabIndex = 174;
            this.charKnowledgeArchitecture.Text = "charKnowledgeArchitecture";
            this.charKnowledgeArchitecture.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.charKnowledgeArchitecture.MouseClick += new System.Windows.Forms.MouseEventHandler(this.KnowledgeBonus);
            // 
            // charKnowledgeMedicine
            // 
            this.charKnowledgeMedicine.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.charKnowledgeMedicine.Location = new System.Drawing.Point(3, 180);
            this.charKnowledgeMedicine.Name = "charKnowledgeMedicine";
            this.charKnowledgeMedicine.Size = new System.Drawing.Size(270, 20);
            this.charKnowledgeMedicine.TabIndex = 175;
            this.charKnowledgeMedicine.Text = "charKnowledgeMedicine";
            this.charKnowledgeMedicine.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.charKnowledgeMedicine.MouseClick += new System.Windows.Forms.MouseEventHandler(this.KnowledgeBonus);
            // 
            // charKnowledgeHistory
            // 
            this.charKnowledgeHistory.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.charKnowledgeHistory.Location = new System.Drawing.Point(3, 200);
            this.charKnowledgeHistory.Name = "charKnowledgeHistory";
            this.charKnowledgeHistory.Size = new System.Drawing.Size(270, 20);
            this.charKnowledgeHistory.TabIndex = 176;
            this.charKnowledgeHistory.Text = "charKnowledgeHistory";
            this.charKnowledgeHistory.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.charKnowledgeHistory.MouseClick += new System.Windows.Forms.MouseEventHandler(this.KnowledgeBonus);
            // 
            // charKnowledgeTech
            // 
            this.charKnowledgeTech.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.charKnowledgeTech.Location = new System.Drawing.Point(3, 220);
            this.charKnowledgeTech.Name = "charKnowledgeTech";
            this.charKnowledgeTech.Size = new System.Drawing.Size(270, 20);
            this.charKnowledgeTech.TabIndex = 177;
            this.charKnowledgeTech.Text = "charKnowledgeTech";
            this.charKnowledgeTech.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.charKnowledgeTech.MouseClick += new System.Windows.Forms.MouseEventHandler(this.KnowledgeBonus);
            // 
            // charKnowledgeComputers
            // 
            this.charKnowledgeComputers.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.charKnowledgeComputers.Location = new System.Drawing.Point(3, 240);
            this.charKnowledgeComputers.Name = "charKnowledgeComputers";
            this.charKnowledgeComputers.Size = new System.Drawing.Size(270, 20);
            this.charKnowledgeComputers.TabIndex = 178;
            this.charKnowledgeComputers.Text = "charKnowledgeComputers";
            this.charKnowledgeComputers.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.charKnowledgeComputers.MouseClick += new System.Windows.Forms.MouseEventHandler(this.KnowledgeBonus);
            // 
            // charKnowledgeBionic
            // 
            this.charKnowledgeBionic.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.charKnowledgeBionic.Location = new System.Drawing.Point(3, 260);
            this.charKnowledgeBionic.Name = "charKnowledgeBionic";
            this.charKnowledgeBionic.Size = new System.Drawing.Size(270, 20);
            this.charKnowledgeBionic.TabIndex = 179;
            this.charKnowledgeBionic.Text = "charKnowledgeBionic";
            this.charKnowledgeBionic.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.charKnowledgeBionic.MouseClick += new System.Windows.Forms.MouseEventHandler(this.KnowledgeBonus);
            // 
            // charKnowledgeCriminality
            // 
            this.charKnowledgeCriminality.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.charKnowledgeCriminality.Location = new System.Drawing.Point(3, 280);
            this.charKnowledgeCriminality.Name = "charKnowledgeCriminality";
            this.charKnowledgeCriminality.Size = new System.Drawing.Size(270, 20);
            this.charKnowledgeCriminality.TabIndex = 180;
            this.charKnowledgeCriminality.Text = "charKnowledgeCriminality";
            this.charKnowledgeCriminality.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.charKnowledgeCriminality.MouseClick += new System.Windows.Forms.MouseEventHandler(this.KnowledgeBonus);
            // 
            // charKnowledgeAstronomy
            // 
            this.charKnowledgeAstronomy.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.charKnowledgeAstronomy.Location = new System.Drawing.Point(3, 300);
            this.charKnowledgeAstronomy.Name = "charKnowledgeAstronomy";
            this.charKnowledgeAstronomy.Size = new System.Drawing.Size(270, 20);
            this.charKnowledgeAstronomy.TabIndex = 181;
            this.charKnowledgeAstronomy.Text = "charKnowledgeAstronomy";
            this.charKnowledgeAstronomy.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.charKnowledgeAstronomy.MouseClick += new System.Windows.Forms.MouseEventHandler(this.KnowledgeBonus);
            // 
            // charKnowledgeSciences
            // 
            this.charKnowledgeSciences.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.charKnowledgeSciences.Location = new System.Drawing.Point(3, 320);
            this.charKnowledgeSciences.Name = "charKnowledgeSciences";
            this.charKnowledgeSciences.Size = new System.Drawing.Size(270, 20);
            this.charKnowledgeSciences.TabIndex = 182;
            this.charKnowledgeSciences.Text = "charKnowledgeSciences";
            this.charKnowledgeSciences.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.charKnowledgeSciences.MouseClick += new System.Windows.Forms.MouseEventHandler(this.KnowledgeBonus);
            // 
            // charKnowledgeMilitary
            // 
            this.charKnowledgeMilitary.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.charKnowledgeMilitary.Location = new System.Drawing.Point(3, 340);
            this.charKnowledgeMilitary.Name = "charKnowledgeMilitary";
            this.charKnowledgeMilitary.Size = new System.Drawing.Size(270, 20);
            this.charKnowledgeMilitary.TabIndex = 183;
            this.charKnowledgeMilitary.Text = "charKnowledgeMilitary";
            this.charKnowledgeMilitary.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.charKnowledgeMilitary.MouseClick += new System.Windows.Forms.MouseEventHandler(this.KnowledgeBonus);
            // 
            // languages
            // 
            this.languages.Font = new System.Drawing.Font("Segoe UI Semibold", 11.25F, System.Drawing.FontStyle.Bold);
            this.languages.Location = new System.Drawing.Point(3, 360);
            this.languages.Name = "languages";
            this.languages.Size = new System.Drawing.Size(270, 20);
            this.languages.TabIndex = 184;
            this.languages.Tag = "IsBold";
            this.languages.Text = "languages";
            this.languages.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // charLanguagesUSL
            // 
            this.charLanguagesUSL.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.charLanguagesUSL.Location = new System.Drawing.Point(3, 380);
            this.charLanguagesUSL.Name = "charLanguagesUSL";
            this.charLanguagesUSL.Size = new System.Drawing.Size(270, 20);
            this.charLanguagesUSL.TabIndex = 185;
            this.charLanguagesUSL.Text = "charLanguagesUSL";
            this.charLanguagesUSL.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.charLanguagesUSL.MouseClick += new System.Windows.Forms.MouseEventHandler(this.KnowledgeBonus);
            // 
            // charLanguagesFornsh
            // 
            this.charLanguagesFornsh.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.charLanguagesFornsh.Location = new System.Drawing.Point(3, 400);
            this.charLanguagesFornsh.Name = "charLanguagesFornsh";
            this.charLanguagesFornsh.Size = new System.Drawing.Size(270, 20);
            this.charLanguagesFornsh.TabIndex = 186;
            this.charLanguagesFornsh.Text = "charLanguagesFornsh";
            this.charLanguagesFornsh.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.charLanguagesFornsh.MouseClick += new System.Windows.Forms.MouseEventHandler(this.KnowledgeBonus);
            // 
            // charLanguagesMekish
            // 
            this.charLanguagesMekish.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.charLanguagesMekish.Location = new System.Drawing.Point(3, 420);
            this.charLanguagesMekish.Name = "charLanguagesMekish";
            this.charLanguagesMekish.Size = new System.Drawing.Size(270, 20);
            this.charLanguagesMekish.TabIndex = 187;
            this.charLanguagesMekish.Text = "charLanguagesMekish";
            this.charLanguagesMekish.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.charLanguagesMekish.MouseClick += new System.Windows.Forms.MouseEventHandler(this.KnowledgeBonus);
            // 
            // charLanguagesLop
            // 
            this.charLanguagesLop.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.charLanguagesLop.Location = new System.Drawing.Point(3, 440);
            this.charLanguagesLop.Name = "charLanguagesLop";
            this.charLanguagesLop.Size = new System.Drawing.Size(270, 20);
            this.charLanguagesLop.TabIndex = 188;
            this.charLanguagesLop.Text = "charLanguagesLop";
            this.charLanguagesLop.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.charLanguagesLop.MouseClick += new System.Windows.Forms.MouseEventHandler(this.KnowledgeBonus);
            // 
            // charLanguagesVulsh
            // 
            this.charLanguagesVulsh.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.charLanguagesVulsh.Location = new System.Drawing.Point(3, 460);
            this.charLanguagesVulsh.Name = "charLanguagesVulsh";
            this.charLanguagesVulsh.Size = new System.Drawing.Size(270, 20);
            this.charLanguagesVulsh.TabIndex = 189;
            this.charLanguagesVulsh.Text = "charLanguagesVulsh";
            this.charLanguagesVulsh.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.charLanguagesVulsh.MouseClick += new System.Windows.Forms.MouseEventHandler(this.KnowledgeBonus);
            // 
            // knowledgeLanguageText
            // 
            this.knowledgeLanguageText.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Bold);
            this.knowledgeLanguageText.Image = global::JasonsRPGCharacterTool.Properties.Resources.Background;
            this.knowledgeLanguageText.Location = new System.Drawing.Point(2, 0);
            this.knowledgeLanguageText.Name = "knowledgeLanguageText";
            this.knowledgeLanguageText.Size = new System.Drawing.Size(300, 19);
            this.knowledgeLanguageText.TabIndex = 168;
            this.knowledgeLanguageText.Tag = "IsBold";
            this.knowledgeLanguageText.Text = "knowledge & languages";
            this.knowledgeLanguageText.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // asteriusKnowledge
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(137)))), ((int)(((byte)(239)))));
            this.ClientSize = new System.Drawing.Size(300, 400);
            this.ControlBox = false;
            this.Controls.Add(this.knowledgeLanguageText);
            this.Controls.Add(this.knowledgeFlowPanel);
            this.DoubleBuffered = true;
            this.ForeColor = System.Drawing.SystemColors.ControlText;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "asteriusKnowledge";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "asteriusKnowledge";
            this.knowledgeFlowPanel.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.FlowLayoutPanel knowledgeFlowPanel;
        private System.Windows.Forms.Label charKnowledge;
        private System.Windows.Forms.Label charKnowledgeGuns;
        private System.Windows.Forms.Label charKnowledgeUnarmed;
        private System.Windows.Forms.Label charKnowledgeExplosives;
        private System.Windows.Forms.Label charKnowledgePolitics;
        private System.Windows.Forms.Label charKnowledgePlants;
        private System.Windows.Forms.Label charKnowledgeSpaceships;
        private System.Windows.Forms.Label charKnowledgeTraps;
        private System.Windows.Forms.Label charKnowledgeArchitecture;
        private System.Windows.Forms.Label charKnowledgeMedicine;
        private System.Windows.Forms.Label charKnowledgeHistory;
        private System.Windows.Forms.Label charKnowledgeTech;
        private System.Windows.Forms.Label charKnowledgeComputers;
        private System.Windows.Forms.Label charKnowledgeBionic;
        private System.Windows.Forms.Label charKnowledgeCriminality;
        private System.Windows.Forms.Label charKnowledgeAstronomy;
        private System.Windows.Forms.Label charKnowledgeSciences;
        private System.Windows.Forms.Label charKnowledgeMilitary;
        private System.Windows.Forms.Label languages;
        private System.Windows.Forms.Label charLanguagesUSL;
        private System.Windows.Forms.Label charLanguagesFornsh;
        private System.Windows.Forms.Label charLanguagesMekish;
        private System.Windows.Forms.Label charLanguagesLop;
        private System.Windows.Forms.Label charLanguagesVulsh;
        private System.Windows.Forms.Label knowledgeLanguageText;
    }
}