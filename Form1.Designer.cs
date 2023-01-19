﻿using System.Windows.Forms;

namespace SUP
{
    partial class Form1
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle17 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle19 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle20 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle18 = new System.Windows.Forms.DataGridViewCellStyle();
            this.btnGet = new System.Windows.Forms.Button();
            this.btnDelete = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.txtlevelDBKey = new System.Windows.Forms.TextBox();
            this.lbTableName = new System.Windows.Forms.ListBox();
            this.label6 = new System.Windows.Forms.Label();
            this.txtLogin = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.txtPassword = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.txtUrl = new System.Windows.Forms.TextBox();
            this.btnTestConnection = new System.Windows.Forms.Button();
            this.lblTotalBytes = new System.Windows.Forms.Label();
            this.txtbalance = new System.Windows.Forms.TextBox();
            this.txtSearchAddress = new System.Windows.Forms.TextBox();
            this.label11 = new System.Windows.Forms.Label();
            this.btnSearch = new System.Windows.Forms.Button();
            this.dgTransactions = new System.Windows.Forms.DataGridView();
            this.rootid = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.messagecount = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.is_signed = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.signed_by = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.filecount = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.totalfilebytes = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.keywordcount = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.total_bytes = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.block_time = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.transaction_id = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.signature = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.confirmations = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.buildtime = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.btnGetTransactionId = new System.Windows.Forms.Button();
            this.label12 = new System.Windows.Forms.Label();
            this.txtTransactionId = new System.Windows.Forms.TextBox();
            this.btnGetKeyword = new System.Windows.Forms.Button();
            this.label13 = new System.Windows.Forms.Label();
            this.txtVersionByte = new System.Windows.Forms.TextBox();
            this.label14 = new System.Windows.Forms.Label();
            this.lblTotalTime = new System.Windows.Forms.Label();
            this.lblKbs = new System.Windows.Forms.Label();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.txtGetValue = new System.Windows.Forms.RichTextBox();
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.button2 = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.btnGetCreated = new System.Windows.Forms.Button();
            this.btnGetOwned = new System.Windows.Forms.Button();
            this.lblTotal = new System.Windows.Forms.Label();
            this.btnPurge = new System.Windows.Forms.Button();
            this.btnGetObject = new System.Windows.Forms.Button();
            this.btnDecrypt = new System.Windows.Forms.Button();
            this.btnEncrypt = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dgTransactions)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnGet
            // 
            this.btnGet.Location = new System.Drawing.Point(291, 132);
            this.btnGet.Name = "btnGet";
            this.btnGet.Size = new System.Drawing.Size(42, 23);
            this.btnGet.TabIndex = 1;
            this.btnGet.Text = "get";
            this.btnGet.UseVisualStyleBackColor = true;
            this.btnGet.Click += new System.EventHandler(this.BtnGet_Click);
            // 
            // btnDelete
            // 
            this.btnDelete.Location = new System.Drawing.Point(289, 106);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(44, 23);
            this.btnDelete.TabIndex = 2;
            this.btnDelete.Text = "delete";
            this.btnDelete.UseVisualStyleBackColor = true;
            this.btnDelete.Click += new System.EventHandler(this.BtnDelete_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(14, 116);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(41, 13);
            this.label3.TabIndex = 8;
            this.label3.Text = "leveldb";
            // 
            // txtlevelDBKey
            // 
            this.txtlevelDBKey.Location = new System.Drawing.Point(9, 132);
            this.txtlevelDBKey.Name = "txtlevelDBKey";
            this.txtlevelDBKey.Size = new System.Drawing.Size(271, 20);
            this.txtlevelDBKey.TabIndex = 7;
            this.txtlevelDBKey.Text = "mt1ZyUkEjnjV6vLs4EQ3f2B1utF9Xqjp7j";
            // 
            // lbTableName
            // 
            this.lbTableName.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.lbTableName.FormattingEnabled = true;
            this.lbTableName.Items.AddRange(new object[] {
            "EVENT",
            "OBJ",
            "ROOT",
            "PRO",
            "COL"});
            this.lbTableName.Location = new System.Drawing.Point(349, 83);
            this.lbTableName.Name = "lbTableName";
            this.lbTableName.ScrollAlwaysVisible = true;
            this.lbTableName.Size = new System.Drawing.Size(102, 69);
            this.lbTableName.TabIndex = 13;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(339, 67);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(67, 13);
            this.label6.TabIndex = 14;
            this.label6.Text = "leveldb table";
            // 
            // txtLogin
            // 
            this.txtLogin.Location = new System.Drawing.Point(13, 34);
            this.txtLogin.Name = "txtLogin";
            this.txtLogin.Size = new System.Drawing.Size(111, 20);
            this.txtLogin.TabIndex = 15;
            this.txtLogin.Text = "good-user";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(13, 17);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(29, 13);
            this.label7.TabIndex = 16;
            this.label7.Text = "login";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(132, 17);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(52, 13);
            this.label8.TabIndex = 18;
            this.label8.Text = "password";
            // 
            // txtPassword
            // 
            this.txtPassword.Location = new System.Drawing.Point(130, 34);
            this.txtPassword.Name = "txtPassword";
            this.txtPassword.Size = new System.Drawing.Size(121, 20);
            this.txtPassword.TabIndex = 17;
            this.txtPassword.Text = "better-password";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(14, 67);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(18, 13);
            this.label9.TabIndex = 20;
            this.label9.Text = "url";
            // 
            // txtUrl
            // 
            this.txtUrl.Location = new System.Drawing.Point(13, 83);
            this.txtUrl.Name = "txtUrl";
            this.txtUrl.Size = new System.Drawing.Size(170, 20);
            this.txtUrl.TabIndex = 19;
            this.txtUrl.Text = "http://127.0.0.1:18332";
            // 
            // btnTestConnection
            // 
            this.btnTestConnection.Location = new System.Drawing.Point(257, 34);
            this.btnTestConnection.Name = "btnTestConnection";
            this.btnTestConnection.Size = new System.Drawing.Size(76, 23);
            this.btnTestConnection.TabIndex = 21;
            this.btnTestConnection.Text = "Test Connect";
            this.btnTestConnection.UseVisualStyleBackColor = true;
            this.btnTestConnection.Click += new System.EventHandler(this.BtnTestConnection_Click);
            // 
            // lblTotalBytes
            // 
            this.lblTotalBytes.AutoSize = true;
            this.lblTotalBytes.Location = new System.Drawing.Point(200, 359);
            this.lblTotalBytes.Name = "lblTotalBytes";
            this.lblTotalBytes.Size = new System.Drawing.Size(35, 13);
            this.lblTotalBytes.TabIndex = 23;
            this.lblTotalBytes.Text = "bytes:";
            // 
            // txtbalance
            // 
            this.txtbalance.Location = new System.Drawing.Point(257, 83);
            this.txtbalance.Name = "txtbalance";
            this.txtbalance.Size = new System.Drawing.Size(76, 20);
            this.txtbalance.TabIndex = 22;
            // 
            // txtSearchAddress
            // 
            this.txtSearchAddress.Location = new System.Drawing.Point(9, 265);
            this.txtSearchAddress.Name = "txtSearchAddress";
            this.txtSearchAddress.Size = new System.Drawing.Size(270, 20);
            this.txtSearchAddress.TabIndex = 25;
            this.txtSearchAddress.Text = "mt1ZyUkEjnjV6vLs4EQ3f2B1utF9Xqjp7j";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(6, 245);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(44, 13);
            this.label11.TabIndex = 26;
            this.label11.Text = "address";
            // 
            // btnSearch
            // 
            this.btnSearch.Location = new System.Drawing.Point(285, 263);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(76, 23);
            this.btnSearch.TabIndex = 27;
            this.btnSearch.Text = "get address";
            this.btnSearch.UseVisualStyleBackColor = true;
            this.btnSearch.Click += new System.EventHandler(this.BtnSearch_Click);
            // 
            // dgTransactions
            // 
            this.dgTransactions.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            dataGridViewCellStyle17.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle17.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle17.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle17.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle17.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle17.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle17.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgTransactions.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle17;
            this.dgTransactions.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgTransactions.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.rootid,
            this.messagecount,
            this.is_signed,
            this.signed_by,
            this.filecount,
            this.totalfilebytes,
            this.keywordcount,
            this.total_bytes,
            this.block_time,
            this.transaction_id,
            this.signature,
            this.confirmations,
            this.buildtime});
            dataGridViewCellStyle19.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle19.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle19.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle19.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle19.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle19.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle19.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgTransactions.DefaultCellStyle = dataGridViewCellStyle19;
            this.dgTransactions.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgTransactions.Location = new System.Drawing.Point(0, 0);
            this.dgTransactions.Name = "dgTransactions";
            dataGridViewCellStyle20.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle20.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle20.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle20.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle20.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle20.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle20.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgTransactions.RowHeadersDefaultCellStyle = dataGridViewCellStyle20;
            this.dgTransactions.Size = new System.Drawing.Size(1056, 267);
            this.dgTransactions.TabIndex = 30;
            this.dgTransactions.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgTransactions_CellClick);
            // 
            // rootid
            // 
            this.rootid.HeaderText = "Object ID";
            this.rootid.MinimumWidth = 60;
            this.rootid.Name = "rootid";
            // 
            // messagecount
            // 
            dataGridViewCellStyle18.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.messagecount.DefaultCellStyle = dataGridViewCellStyle18;
            this.messagecount.HeaderText = "Messages";
            this.messagecount.MaxInputLength = 100000000;
            this.messagecount.Name = "messagecount";
            // 
            // is_signed
            // 
            this.is_signed.HeaderText = "Signed";
            this.is_signed.MinimumWidth = 50;
            this.is_signed.Name = "is_signed";
            this.is_signed.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.is_signed.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            // 
            // signed_by
            // 
            this.signed_by.HeaderText = "Signed By";
            this.signed_by.Name = "signed_by";
            // 
            // filecount
            // 
            this.filecount.HeaderText = "Files";
            this.filecount.Name = "filecount";
            // 
            // totalfilebytes
            // 
            this.totalfilebytes.HeaderText = "File Bytes";
            this.totalfilebytes.Name = "totalfilebytes";
            this.totalfilebytes.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            // 
            // keywordcount
            // 
            this.keywordcount.HeaderText = "Keys";
            this.keywordcount.Name = "keywordcount";
            // 
            // total_bytes
            // 
            this.total_bytes.HeaderText = "Total Bytes";
            this.total_bytes.Name = "total_bytes";
            // 
            // block_time
            // 
            this.block_time.HeaderText = "Block Time";
            this.block_time.Name = "block_time";
            // 
            // transaction_id
            // 
            this.transaction_id.HeaderText = "TID";
            this.transaction_id.Name = "transaction_id";
            // 
            // signature
            // 
            this.signature.HeaderText = "SIG";
            this.signature.Name = "signature";
            // 
            // confirmations
            // 
            this.confirmations.HeaderText = "Confirms";
            this.confirmations.Name = "confirmations";
            // 
            // buildtime
            // 
            this.buildtime.HeaderText = "Build Time";
            this.buildtime.Name = "buildtime";
            // 
            // btnGetTransactionId
            // 
            this.btnGetTransactionId.Location = new System.Drawing.Point(408, 203);
            this.btnGetTransactionId.Name = "btnGetTransactionId";
            this.btnGetTransactionId.Size = new System.Drawing.Size(43, 23);
            this.btnGetTransactionId.TabIndex = 33;
            this.btnGetTransactionId.Text = "get";
            this.btnGetTransactionId.UseVisualStyleBackColor = true;
            this.btnGetTransactionId.Click += new System.EventHandler(this.BtnGetTransactionId_Click);
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(6, 190);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(23, 13);
            this.label12.TabIndex = 32;
            this.label12.Text = "txid";
            // 
            // txtTransactionId
            // 
            this.txtTransactionId.Location = new System.Drawing.Point(9, 206);
            this.txtTransactionId.Name = "txtTransactionId";
            this.txtTransactionId.Size = new System.Drawing.Size(393, 20);
            this.txtTransactionId.TabIndex = 31;
            this.txtTransactionId.Text = "7223070fbc3706856e90701b62a8bfb3f5a618dc13d64d8f029260cba187ca26";
            // 
            // btnGetKeyword
            // 
            this.btnGetKeyword.Location = new System.Drawing.Point(367, 262);
            this.btnGetKeyword.Name = "btnGetKeyword";
            this.btnGetKeyword.Size = new System.Drawing.Size(84, 23);
            this.btnGetKeyword.TabIndex = 36;
            this.btnGetKeyword.Text = "by keyword";
            this.btnGetKeyword.UseVisualStyleBackColor = true;
            this.btnGetKeyword.Click += new System.EventHandler(this.BtnGetKeyword_Click);
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(187, 67);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(27, 13);
            this.label13.TabIndex = 38;
            this.label13.Text = "byte";
            // 
            // txtVersionByte
            // 
            this.txtVersionByte.Location = new System.Drawing.Point(190, 83);
            this.txtVersionByte.Name = "txtVersionByte";
            this.txtVersionByte.Size = new System.Drawing.Size(61, 20);
            this.txtVersionByte.TabIndex = 37;
            this.txtVersionByte.Text = "111";
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(254, 67);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(45, 13);
            this.label14.TabIndex = 39;
            this.label14.Text = "balance";
            // 
            // lblTotalTime
            // 
            this.lblTotalTime.AutoSize = true;
            this.lblTotalTime.Location = new System.Drawing.Point(25, 359);
            this.lblTotalTime.Name = "lblTotalTime";
            this.lblTotalTime.Size = new System.Drawing.Size(29, 13);
            this.lblTotalTime.TabIndex = 40;
            this.lblTotalTime.Text = "time:";
            // 
            // lblKbs
            // 
            this.lblKbs.AutoSize = true;
            this.lblKbs.Location = new System.Drawing.Point(314, 359);
            this.lblKbs.Name = "lblKbs";
            this.lblKbs.Size = new System.Drawing.Size(32, 13);
            this.lblKbs.TabIndex = 41;
            this.lblKbs.Text = "kb/s:";
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.IsSplitterFixed = true;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.button3);
            this.splitContainer1.Panel1.Controls.Add(this.txtGetValue);
            this.splitContainer1.Panel1.Controls.Add(this.checkBox1);
            this.splitContainer1.Panel1.Controls.Add(this.button2);
            this.splitContainer1.Panel1.Controls.Add(this.button1);
            this.splitContainer1.Panel1.Controls.Add(this.btnGetCreated);
            this.splitContainer1.Panel1.Controls.Add(this.btnGetOwned);
            this.splitContainer1.Panel1.Controls.Add(this.lblTotal);
            this.splitContainer1.Panel1.Controls.Add(this.btnPurge);
            this.splitContainer1.Panel1.Controls.Add(this.btnGetObject);
            this.splitContainer1.Panel1.Controls.Add(this.btnDecrypt);
            this.splitContainer1.Panel1.Controls.Add(this.btnEncrypt);
            this.splitContainer1.Panel1.Controls.Add(this.lblKbs);
            this.splitContainer1.Panel1.Controls.Add(this.lblTotalTime);
            this.splitContainer1.Panel1.Controls.Add(this.btnGet);
            this.splitContainer1.Panel1.Controls.Add(this.label14);
            this.splitContainer1.Panel1.Controls.Add(this.btnDelete);
            this.splitContainer1.Panel1.Controls.Add(this.label13);
            this.splitContainer1.Panel1.Controls.Add(this.txtVersionByte);
            this.splitContainer1.Panel1.Controls.Add(this.btnGetKeyword);
            this.splitContainer1.Panel1.Controls.Add(this.txtlevelDBKey);
            this.splitContainer1.Panel1.Controls.Add(this.btnGetTransactionId);
            this.splitContainer1.Panel1.Controls.Add(this.label3);
            this.splitContainer1.Panel1.Controls.Add(this.label12);
            this.splitContainer1.Panel1.Controls.Add(this.txtTransactionId);
            this.splitContainer1.Panel1.Controls.Add(this.btnSearch);
            this.splitContainer1.Panel1.Controls.Add(this.label11);
            this.splitContainer1.Panel1.Controls.Add(this.lbTableName);
            this.splitContainer1.Panel1.Controls.Add(this.txtSearchAddress);
            this.splitContainer1.Panel1.Controls.Add(this.label6);
            this.splitContainer1.Panel1.Controls.Add(this.lblTotalBytes);
            this.splitContainer1.Panel1.Controls.Add(this.txtLogin);
            this.splitContainer1.Panel1.Controls.Add(this.txtbalance);
            this.splitContainer1.Panel1.Controls.Add(this.label7);
            this.splitContainer1.Panel1.Controls.Add(this.btnTestConnection);
            this.splitContainer1.Panel1.Controls.Add(this.txtPassword);
            this.splitContainer1.Panel1.Controls.Add(this.label9);
            this.splitContainer1.Panel1.Controls.Add(this.label8);
            this.splitContainer1.Panel1.Controls.Add(this.txtUrl);
            this.splitContainer1.Panel1MinSize = 380;
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.dgTransactions);
            this.splitContainer1.Size = new System.Drawing.Size(1056, 648);
            this.splitContainer1.SplitterDistance = 380;
            this.splitContainer1.SplitterWidth = 1;
            this.splitContainer1.TabIndex = 42;
            this.splitContainer1.Resize += new System.EventHandler(this.splitContainer1_Resize);
            // 
            // txtGetValue
            // 
            this.txtGetValue.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtGetValue.BackColor = System.Drawing.SystemColors.Control;
            this.txtGetValue.Location = new System.Drawing.Point(457, 12);
            this.txtGetValue.Name = "txtGetValue";
            this.txtGetValue.Size = new System.Drawing.Size(587, 360);
            this.txtGetValue.TabIndex = 56;
            this.txtGetValue.Text = "";
            // 
            // checkBox1
            // 
            this.checkBox1.AutoSize = true;
            this.checkBox1.Location = new System.Drawing.Point(17, 330);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(64, 17);
            this.checkBox1.TabIndex = 55;
            this.checkBox1.Text = "verbose";
            this.checkBox1.UseVisualStyleBackColor = true;
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(97, 329);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(79, 23);
            this.button2.TabIndex = 53;
            this.button2.Text = "by keyword";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click_1);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(97, 300);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(79, 23);
            this.button1.TabIndex = 52;
            this.button1.Text = "get objects";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click_1);
            // 
            // btnGetCreated
            // 
            this.btnGetCreated.Location = new System.Drawing.Point(267, 300);
            this.btnGetCreated.Name = "btnGetCreated";
            this.btnGetCreated.Size = new System.Drawing.Size(79, 23);
            this.btnGetCreated.TabIndex = 51;
            this.btnGetCreated.Text = "get created";
            this.btnGetCreated.UseVisualStyleBackColor = true;
            this.btnGetCreated.Click += new System.EventHandler(this.btnGetCreated_Click);
            // 
            // btnGetOwned
            // 
            this.btnGetOwned.Location = new System.Drawing.Point(182, 300);
            this.btnGetOwned.Name = "btnGetOwned";
            this.btnGetOwned.Size = new System.Drawing.Size(79, 23);
            this.btnGetOwned.TabIndex = 50;
            this.btnGetOwned.Text = "get owned";
            this.btnGetOwned.UseVisualStyleBackColor = true;
            this.btnGetOwned.Click += new System.EventHandler(this.btnGetOwned_Click);
            // 
            // lblTotal
            // 
            this.lblTotal.AutoSize = true;
            this.lblTotal.Location = new System.Drawing.Point(107, 359);
            this.lblTotal.Name = "lblTotal";
            this.lblTotal.Size = new System.Drawing.Size(30, 13);
            this.lblTotal.TabIndex = 49;
            this.lblTotal.Text = "total:";
            // 
            // btnPurge
            // 
            this.btnPurge.Location = new System.Drawing.Point(364, 300);
            this.btnPurge.Name = "btnPurge";
            this.btnPurge.Size = new System.Drawing.Size(87, 52);
            this.btnPurge.TabIndex = 45;
            this.btnPurge.Text = "purge";
            this.btnPurge.UseVisualStyleBackColor = true;
            this.btnPurge.Click += new System.EventHandler(this.btnPurge_Click);
            // 
            // btnGetObject
            // 
            this.btnGetObject.Location = new System.Drawing.Point(12, 300);
            this.btnGetObject.Name = "btnGetObject";
            this.btnGetObject.Size = new System.Drawing.Size(79, 23);
            this.btnGetObject.TabIndex = 44;
            this.btnGetObject.Text = "get object";
            this.btnGetObject.UseVisualStyleBackColor = true;
            this.btnGetObject.Click += new System.EventHandler(this.button3_Click);
            // 
            // btnDecrypt
            // 
            this.btnDecrypt.Location = new System.Drawing.Point(285, 174);
            this.btnDecrypt.Name = "btnDecrypt";
            this.btnDecrypt.Size = new System.Drawing.Size(76, 23);
            this.btnDecrypt.TabIndex = 43;
            this.btnDecrypt.Text = "decrypt";
            this.btnDecrypt.UseVisualStyleBackColor = true;
            this.btnDecrypt.Click += new System.EventHandler(this.button2_Click);
            // 
            // btnEncrypt
            // 
            this.btnEncrypt.Location = new System.Drawing.Point(367, 174);
            this.btnEncrypt.Name = "btnEncrypt";
            this.btnEncrypt.Size = new System.Drawing.Size(84, 23);
            this.btnEncrypt.TabIndex = 42;
            this.btnEncrypt.Text = "encrypt";
            this.btnEncrypt.UseVisualStyleBackColor = true;
            this.btnEncrypt.Click += new System.EventHandler(this.Button1_Click);
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(184, 330);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(77, 23);
            this.button3.TabIndex = 57;
            this.button3.Text = "urn register";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click_1);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1056, 648);
            this.Controls.Add(this.splitContainer1);
            this.MinimumSize = new System.Drawing.Size(1072, 687);
            this.Name = "Form1";
            this.Text = "Sup!? Just testing ";
            ((System.ComponentModel.ISupportInitialize)(this.dgTransactions)).EndInit();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel1.PerformLayout();
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Button btnGet;
        private System.Windows.Forms.Button btnDelete;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtlevelDBKey;
        private System.Windows.Forms.ListBox lbTableName;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox txtLogin;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox txtPassword;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox txtUrl;
        private System.Windows.Forms.Button btnTestConnection;
        private System.Windows.Forms.Label lblTotalBytes;
        private System.Windows.Forms.TextBox txtbalance;
        private System.Windows.Forms.TextBox txtSearchAddress;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Button btnSearch;
        private System.Windows.Forms.DataGridView dgTransactions;
        private System.Windows.Forms.Button btnGetTransactionId;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.TextBox txtTransactionId;
        private System.Windows.Forms.Button btnGetKeyword;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.TextBox txtVersionByte;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Label lblTotalTime;
        private System.Windows.Forms.Label lblKbs;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private DataGridViewTextBoxColumn rootid;
        private DataGridViewTextBoxColumn messagecount;
        private DataGridViewCheckBoxColumn is_signed;
        private DataGridViewTextBoxColumn signed_by;
        private DataGridViewTextBoxColumn filecount;
        private DataGridViewTextBoxColumn totalfilebytes;
        private DataGridViewTextBoxColumn keywordcount;
        private DataGridViewTextBoxColumn total_bytes;
        private DataGridViewTextBoxColumn block_time;
        private DataGridViewTextBoxColumn transaction_id;
        private DataGridViewTextBoxColumn signature;
        private DataGridViewTextBoxColumn confirmations;
        private DataGridViewTextBoxColumn buildtime;
        private Button btnEncrypt;
        private Button btnDecrypt;
        private Button btnGetObject;
        private Button btnPurge;
        private Label lblTotal;
        private Button btnGetCreated;
        private Button btnGetOwned;
        private Button button1;
        private Button button2;
        private CheckBox checkBox1;
        private RichTextBox txtGetValue;
        private Button button3;
    }
}

