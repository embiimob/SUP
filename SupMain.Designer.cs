﻿namespace SUP
{
    partial class SupMain
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
            this.components = new System.ComponentModel.Container();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.splitContainer2 = new System.Windows.Forms.SplitContainer();
            this.btnConnections = new System.Windows.Forms.Button();
            this.btnLive = new System.Windows.Forms.Button();
            this.btnWorkBench = new System.Windows.Forms.Button();
            this.flowFollow = new System.Windows.Forms.FlowLayoutPanel();
            this.btnMint = new System.Windows.Forms.Button();
            this.btnVideoSearch = new System.Windows.Forms.Button();
            this.btnSkipAudio = new System.Windows.Forms.Button();
            this.btnJukeBox = new System.Windows.Forms.Button();
            this.btnHome = new System.Windows.Forms.Button();
            this.profileURN = new System.Windows.Forms.LinkLabel();
            this.lblProcessHeight = new System.Windows.Forms.Label();
            this.btnMute = new System.Windows.Forms.Button();
            this.refreshFriendFeed = new System.Windows.Forms.Button();
            this.btnFollow = new System.Windows.Forms.Button();
            this.profileOwner = new System.Windows.Forms.PictureBox();
            this.btnBlock = new System.Windows.Forms.Button();
            this.supFlow = new System.Windows.Forms.FlowLayoutPanel();
            this.lblAdultsOnly = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.profileCreatedDate = new System.Windows.Forms.Label();
            this.profileBIO = new System.Windows.Forms.Label();
            this.profileIMG = new System.Windows.Forms.PictureBox();
            this.btnDisco = new System.Windows.Forms.Button();
            this.btnPrivateMessage = new System.Windows.Forms.Button();
            this.btnPublicMessage = new System.Windows.Forms.Button();
            this.tmrSearchMemoryPool = new System.Windows.Forms.Timer(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).BeginInit();
            this.splitContainer2.Panel1.SuspendLayout();
            this.splitContainer2.Panel2.SuspendLayout();
            this.splitContainer2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.profileOwner)).BeginInit();
            this.supFlow.SuspendLayout();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.profileIMG)).BeginInit();
            this.SuspendLayout();
            // 
            // splitContainer1
            // 
            this.splitContainer1.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.splitContainer1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.splitContainer2);
            this.splitContainer1.Panel1MinSize = 666;
            this.splitContainer1.Panel2MinSize = 280;
            this.splitContainer1.Size = new System.Drawing.Size(991, 735);
            this.splitContainer1.SplitterDistance = 666;
            this.splitContainer1.SplitterWidth = 10;
            this.splitContainer1.TabIndex = 0;
            this.splitContainer1.DoubleClick += new System.EventHandler(this.splitContainer1_DoubleClick);
            // 
            // splitContainer2
            // 
            this.splitContainer2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer2.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.splitContainer2.IsSplitterFixed = true;
            this.splitContainer2.Location = new System.Drawing.Point(0, 0);
            this.splitContainer2.Name = "splitContainer2";
            // 
            // splitContainer2.Panel1
            // 
            this.splitContainer2.Panel1.Controls.Add(this.btnConnections);
            this.splitContainer2.Panel1.Controls.Add(this.btnLive);
            this.splitContainer2.Panel1.Controls.Add(this.btnWorkBench);
            this.splitContainer2.Panel1.Controls.Add(this.flowFollow);
            this.splitContainer2.Panel1.Controls.Add(this.btnMint);
            this.splitContainer2.Panel1MinSize = 67;
            // 
            // splitContainer2.Panel2
            // 
            this.splitContainer2.Panel2.Controls.Add(this.btnVideoSearch);
            this.splitContainer2.Panel2.Controls.Add(this.btnSkipAudio);
            this.splitContainer2.Panel2.Controls.Add(this.btnJukeBox);
            this.splitContainer2.Panel2.Controls.Add(this.btnHome);
            this.splitContainer2.Panel2.Controls.Add(this.profileURN);
            this.splitContainer2.Panel2.Controls.Add(this.lblProcessHeight);
            this.splitContainer2.Panel2.Controls.Add(this.btnMute);
            this.splitContainer2.Panel2.Controls.Add(this.refreshFriendFeed);
            this.splitContainer2.Panel2.Controls.Add(this.btnFollow);
            this.splitContainer2.Panel2.Controls.Add(this.profileOwner);
            this.splitContainer2.Panel2.Controls.Add(this.btnBlock);
            this.splitContainer2.Panel2.Controls.Add(this.supFlow);
            this.splitContainer2.Panel2.Controls.Add(this.panel1);
            this.splitContainer2.Panel2.Controls.Add(this.btnDisco);
            this.splitContainer2.Panel2.Controls.Add(this.btnPrivateMessage);
            this.splitContainer2.Panel2.Controls.Add(this.btnPublicMessage);
            this.splitContainer2.Size = new System.Drawing.Size(664, 733);
            this.splitContainer2.SplitterDistance = 67;
            this.splitContainer2.SplitterWidth = 1;
            this.splitContainer2.TabIndex = 0;
            this.splitContainer2.TabStop = false;
            // 
            // btnConnections
            // 
            this.btnConnections.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnConnections.Font = new System.Drawing.Font("Segoe UI Emoji", 13.8F);
            this.btnConnections.Location = new System.Drawing.Point(8, 592);
            this.btnConnections.Name = "btnConnections";
            this.btnConnections.Size = new System.Drawing.Size(56, 40);
            this.btnConnections.TabIndex = 82;
            this.btnConnections.Text = "🗝️";
            this.btnConnections.UseVisualStyleBackColor = true;
            this.btnConnections.Click += new System.EventHandler(this.ButtonLoadConnections);
            // 
            // btnLive
            // 
            this.btnLive.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnLive.BackColor = System.Drawing.Color.White;
            this.btnLive.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.btnLive.Font = new System.Drawing.Font("Segoe UI Emoji", 13.8F);
            this.btnLive.ForeColor = System.Drawing.Color.Black;
            this.btnLive.Location = new System.Drawing.Point(8, 544);
            this.btnLive.Name = "btnLive";
            this.btnLive.Size = new System.Drawing.Size(56, 40);
            this.btnLive.TabIndex = 83;
            this.btnLive.Text = "🧿";
            this.btnLive.UseVisualStyleBackColor = false;
            this.btnLive.Click += new System.EventHandler(this.btnLive_Click);
            // 
            // btnWorkBench
            // 
            this.btnWorkBench.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnWorkBench.Font = new System.Drawing.Font("Segoe UI Emoji", 13.8F);
            this.btnWorkBench.Location = new System.Drawing.Point(8, 638);
            this.btnWorkBench.Name = "btnWorkBench";
            this.btnWorkBench.Size = new System.Drawing.Size(56, 40);
            this.btnWorkBench.TabIndex = 81;
            this.btnWorkBench.Text = "⚙️";
            this.btnWorkBench.UseVisualStyleBackColor = true;
            this.btnWorkBench.Click += new System.EventHandler(this.ButtonLoadWorkBench);
            // 
            // flowFollow
            // 
            this.flowFollow.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.flowFollow.AutoScroll = true;
            this.flowFollow.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.flowFollow.Location = new System.Drawing.Point(7, 7);
            this.flowFollow.Name = "flowFollow";
            this.flowFollow.Size = new System.Drawing.Size(58, 523);
            this.flowFollow.TabIndex = 94;
            // 
            // btnMint
            // 
            this.btnMint.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnMint.Font = new System.Drawing.Font("Segoe UI Emoji", 13.8F);
            this.btnMint.Location = new System.Drawing.Point(8, 686);
            this.btnMint.Name = "btnMint";
            this.btnMint.Size = new System.Drawing.Size(56, 40);
            this.btnMint.TabIndex = 80;
            this.btnMint.Text = "💎";
            this.btnMint.UseVisualStyleBackColor = true;
            this.btnMint.Click += new System.EventHandler(this.btnMint_Click);
            // 
            // btnVideoSearch
            // 
            this.btnVideoSearch.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnVideoSearch.Font = new System.Drawing.Font("Segoe UI Emoji", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnVideoSearch.Location = new System.Drawing.Point(183, 686);
            this.btnVideoSearch.Name = "btnVideoSearch";
            this.btnVideoSearch.Size = new System.Drawing.Size(56, 40);
            this.btnVideoSearch.TabIndex = 100;
            this.btnVideoSearch.Text = "🎬";
            this.btnVideoSearch.UseVisualStyleBackColor = true;
            this.btnVideoSearch.Click += new System.EventHandler(this.btnVideoSearch_Click);
            // 
            // btnSkipAudio
            // 
            this.btnSkipAudio.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnSkipAudio.Font = new System.Drawing.Font("Segoe UI Emoji", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSkipAudio.Location = new System.Drawing.Point(288, 686);
            this.btnSkipAudio.Name = "btnSkipAudio";
            this.btnSkipAudio.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.btnSkipAudio.Size = new System.Drawing.Size(56, 40);
            this.btnSkipAudio.TabIndex = 99;
            this.btnSkipAudio.Text = "⏩";
            this.btnSkipAudio.UseVisualStyleBackColor = true;
            this.btnSkipAudio.Click += new System.EventHandler(this.btnSkipAudio_Click);
            // 
            // btnJukeBox
            // 
            this.btnJukeBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnJukeBox.Font = new System.Drawing.Font("Segoe UI Emoji", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnJukeBox.Location = new System.Drawing.Point(121, 686);
            this.btnJukeBox.Name = "btnJukeBox";
            this.btnJukeBox.Size = new System.Drawing.Size(56, 40);
            this.btnJukeBox.TabIndex = 98;
            this.btnJukeBox.Text = "🎵";
            this.btnJukeBox.UseVisualStyleBackColor = true;
            this.btnJukeBox.Click += new System.EventHandler(this.button1_Click_1);
            // 
            // btnHome
            // 
            this.btnHome.Location = new System.Drawing.Point(8, 141);
            this.btnHome.Name = "btnHome";
            this.btnHome.Size = new System.Drawing.Size(56, 23);
            this.btnHome.TabIndex = 97;
            this.btnHome.Text = "home";
            this.btnHome.UseVisualStyleBackColor = true;
            this.btnHome.Click += new System.EventHandler(this.button1_Click);
            // 
            // profileURN
            // 
            this.profileURN.AutoSize = true;
            this.profileURN.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.profileURN.LinkColor = System.Drawing.SystemColors.GradientActiveCaption;
            this.profileURN.Location = new System.Drawing.Point(4, 114);
            this.profileURN.Name = "profileURN";
            this.profileURN.Size = new System.Drawing.Size(45, 20);
            this.profileURN.TabIndex = 11;
            this.profileURN.TabStop = true;
            this.profileURN.Text = "anon";
            this.profileURN.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.profileURN_LinkClicked);
            // 
            // lblProcessHeight
            // 
            this.lblProcessHeight.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblProcessHeight.AutoSize = true;
            this.lblProcessHeight.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblProcessHeight.ForeColor = System.Drawing.Color.White;
            this.lblProcessHeight.Location = new System.Drawing.Point(421, 117);
            this.lblProcessHeight.Name = "lblProcessHeight";
            this.lblProcessHeight.Size = new System.Drawing.Size(28, 16);
            this.lblProcessHeight.TabIndex = 96;
            this.lblProcessHeight.Text = "ph: ";
            this.lblProcessHeight.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // btnMute
            // 
            this.btnMute.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnMute.BackColor = System.Drawing.Color.White;
            this.btnMute.Location = new System.Drawing.Point(462, 141);
            this.btnMute.Name = "btnMute";
            this.btnMute.Size = new System.Drawing.Size(56, 23);
            this.btnMute.TabIndex = 8;
            this.btnMute.Text = "mute";
            this.btnMute.UseVisualStyleBackColor = false;
            this.btnMute.Click += new System.EventHandler(this.btnMute_Click);
            // 
            // refreshFriendFeed
            // 
            this.refreshFriendFeed.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.refreshFriendFeed.Font = new System.Drawing.Font("Segoe UI Emoji", 13.8F);
            this.refreshFriendFeed.Location = new System.Drawing.Point(54, 686);
            this.refreshFriendFeed.Name = "refreshFriendFeed";
            this.refreshFriendFeed.Size = new System.Drawing.Size(55, 40);
            this.refreshFriendFeed.TabIndex = 96;
            this.refreshFriendFeed.Text = "🌆";
            this.refreshFriendFeed.UseVisualStyleBackColor = true;
            this.refreshFriendFeed.Click += new System.EventHandler(this.refreshFriendFeed_Click);
            // 
            // btnFollow
            // 
            this.btnFollow.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnFollow.Location = new System.Drawing.Point(400, 141);
            this.btnFollow.Name = "btnFollow";
            this.btnFollow.Size = new System.Drawing.Size(56, 23);
            this.btnFollow.TabIndex = 10;
            this.btnFollow.Text = "follow";
            this.btnFollow.UseVisualStyleBackColor = true;
            this.btnFollow.Click += new System.EventHandler(this.btnFollow_Click);
            // 
            // profileOwner
            // 
            this.profileOwner.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.profileOwner.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.profileOwner.Location = new System.Drawing.Point(8, 686);
            this.profileOwner.Name = "profileOwner";
            this.profileOwner.Size = new System.Drawing.Size(40, 40);
            this.profileOwner.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.profileOwner.TabIndex = 95;
            this.profileOwner.TabStop = false;
            // 
            // btnBlock
            // 
            this.btnBlock.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnBlock.Location = new System.Drawing.Point(524, 141);
            this.btnBlock.Name = "btnBlock";
            this.btnBlock.Size = new System.Drawing.Size(56, 23);
            this.btnBlock.TabIndex = 9;
            this.btnBlock.Text = "block";
            this.btnBlock.UseVisualStyleBackColor = true;
            this.btnBlock.Click += new System.EventHandler(this.btnBlock_Click);
            // 
            // supFlow
            // 
            this.supFlow.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.supFlow.AutoScroll = true;
            this.supFlow.Controls.Add(this.lblAdultsOnly);
            this.supFlow.Location = new System.Drawing.Point(9, 170);
            this.supFlow.Name = "supFlow";
            this.supFlow.Padding = new System.Windows.Forms.Padding(0, 3, 0, 0);
            this.supFlow.Size = new System.Drawing.Size(591, 510);
            this.supFlow.TabIndex = 86;
            // 
            // lblAdultsOnly
            // 
            this.lblAdultsOnly.Font = new System.Drawing.Font("Dubai", 24F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblAdultsOnly.ForeColor = System.Drawing.Color.White;
            this.lblAdultsOnly.Location = new System.Drawing.Point(3, 3);
            this.lblAdultsOnly.Name = "lblAdultsOnly";
            this.lblAdultsOnly.Size = new System.Drawing.Size(576, 479);
            this.lblAdultsOnly.TabIndex = 86;
            this.lblAdultsOnly.Text = "greetings teamworld\r\nclick the 🗝️ to begin\r\nmade for adults only";
            this.lblAdultsOnly.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.profileCreatedDate);
            this.panel1.Controls.Add(this.profileBIO);
            this.panel1.Controls.Add(this.profileIMG);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(596, 110);
            this.panel1.TabIndex = 0;
            // 
            // profileCreatedDate
            // 
            this.profileCreatedDate.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.profileCreatedDate.ForeColor = System.Drawing.Color.White;
            this.profileCreatedDate.Location = new System.Drawing.Point(110, 95);
            this.profileCreatedDate.Name = "profileCreatedDate";
            this.profileCreatedDate.Size = new System.Drawing.Size(112, 16);
            this.profileCreatedDate.TabIndex = 3;
            // 
            // profileBIO
            // 
            this.profileBIO.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.profileBIO.ForeColor = System.Drawing.Color.White;
            this.profileBIO.Location = new System.Drawing.Point(110, 9);
            this.profileBIO.Name = "profileBIO";
            this.profileBIO.Size = new System.Drawing.Size(478, 85);
            this.profileBIO.TabIndex = 2;
            // 
            // profileIMG
            // 
            this.profileIMG.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.profileIMG.Location = new System.Drawing.Point(4, 9);
            this.profileIMG.Name = "profileIMG";
            this.profileIMG.Size = new System.Drawing.Size(100, 100);
            this.profileIMG.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.profileIMG.TabIndex = 0;
            this.profileIMG.TabStop = false;
            // 
            // btnDisco
            // 
            this.btnDisco.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnDisco.Font = new System.Drawing.Font("Segoe UI Emoji", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnDisco.Location = new System.Drawing.Point(393, 686);
            this.btnDisco.Name = "btnDisco";
            this.btnDisco.Size = new System.Drawing.Size(56, 40);
            this.btnDisco.TabIndex = 85;
            this.btnDisco.Text = "📣";
            this.btnDisco.UseVisualStyleBackColor = true;
            this.btnDisco.Click += new System.EventHandler(this.btnRefresh_Click);
            // 
            // btnPrivateMessage
            // 
            this.btnPrivateMessage.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnPrivateMessage.BackColor = System.Drawing.Color.White;
            this.btnPrivateMessage.Font = new System.Drawing.Font("Segoe UI Emoji", 13.8F);
            this.btnPrivateMessage.ForeColor = System.Drawing.Color.Black;
            this.btnPrivateMessage.Location = new System.Drawing.Point(524, 686);
            this.btnPrivateMessage.Name = "btnPrivateMessage";
            this.btnPrivateMessage.Size = new System.Drawing.Size(56, 40);
            this.btnPrivateMessage.TabIndex = 84;
            this.btnPrivateMessage.Text = "🤐";
            this.btnPrivateMessage.UseVisualStyleBackColor = false;
            this.btnPrivateMessage.Click += new System.EventHandler(this.btnPrivateMessage_Click);
            // 
            // btnPublicMessage
            // 
            this.btnPublicMessage.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnPublicMessage.BackColor = System.Drawing.Color.White;
            this.btnPublicMessage.Font = new System.Drawing.Font("Segoe UI Emoji", 13.8F);
            this.btnPublicMessage.ForeColor = System.Drawing.Color.Black;
            this.btnPublicMessage.Location = new System.Drawing.Point(462, 686);
            this.btnPublicMessage.Name = "btnPublicMessage";
            this.btnPublicMessage.Size = new System.Drawing.Size(56, 40);
            this.btnPublicMessage.TabIndex = 83;
            this.btnPublicMessage.Text = "😍";
            this.btnPublicMessage.UseVisualStyleBackColor = false;
            this.btnPublicMessage.Click += new System.EventHandler(this.btnPublicMessage_Click);
            // 
            // tmrSearchMemoryPool
            // 
            this.tmrSearchMemoryPool.Interval = 5000;
            this.tmrSearchMemoryPool.Tick += new System.EventHandler(this.tmrSearchMemoryPool_Tick);
            // 
            // SupMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(991, 735);
            this.Controls.Add(this.splitContainer1);
            this.MinimumSize = new System.Drawing.Size(1007, 774);
            this.Name = "SupMain";
            this.ShowIcon = false;
            this.Text = "Sup!?";
            this.Load += new System.EventHandler(this.SupMaincs_Load);
            this.splitContainer1.Panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.splitContainer2.Panel1.ResumeLayout(false);
            this.splitContainer2.Panel2.ResumeLayout(false);
            this.splitContainer2.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).EndInit();
            this.splitContainer2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.profileOwner)).EndInit();
            this.supFlow.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.profileIMG)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.SplitContainer splitContainer2;
        private System.Windows.Forms.Button btnWorkBench;
        private System.Windows.Forms.Button btnConnections;
        private System.Windows.Forms.Button btnPublicMessage;
        private System.Windows.Forms.Button btnMint;
        private System.Windows.Forms.Button btnLive;
        private System.Windows.Forms.Button btnPrivateMessage;
        private System.Windows.Forms.Timer tmrSearchMemoryPool;
        private System.Windows.Forms.Button btnDisco;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button btnFollow;
        private System.Windows.Forms.Button btnBlock;
        private System.Windows.Forms.Button btnMute;
        private System.Windows.Forms.Label profileCreatedDate;
        private System.Windows.Forms.Label profileBIO;
        private System.Windows.Forms.PictureBox profileIMG;
        private System.Windows.Forms.FlowLayoutPanel flowFollow;
        private System.Windows.Forms.LinkLabel profileURN;
        private System.Windows.Forms.Label lblAdultsOnly;
        private System.Windows.Forms.FlowLayoutPanel supFlow;
        private System.Windows.Forms.PictureBox profileOwner;
        private System.Windows.Forms.Button refreshFriendFeed;
        private System.Windows.Forms.Label lblProcessHeight;
        private System.Windows.Forms.Button btnHome;
        private System.Windows.Forms.Button btnJukeBox;
        private System.Windows.Forms.Button btnSkipAudio;
        private System.Windows.Forms.Button btnVideoSearch;
    }
}