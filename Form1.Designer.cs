﻿namespace SUP
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
            this.btnPut = new System.Windows.Forms.Button();
            this.btnGet = new System.Windows.Forms.Button();
            this.btnDelete = new System.Windows.Forms.Button();
            this.txtPutKey = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.txtGetKey = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txtDeleteKey = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.txtPutValue = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.txtGetValue = new System.Windows.Forms.TextBox();
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
            this.btnGetTransactionId = new System.Windows.Forms.Button();
            this.label12 = new System.Windows.Forms.Label();
            this.txtTransactionId = new System.Windows.Forms.TextBox();
            this.transaction_id = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.is_signed = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.signed_by = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.signature = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.filecount = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.messagecount = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.keywordcount = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.total_bytes = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.block_time = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.confirmations = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.buildtime = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.dgTransactions)).BeginInit();
            this.SuspendLayout();
            // 
            // btnPut
            // 
            this.btnPut.Location = new System.Drawing.Point(982, 44);
            this.btnPut.Name = "btnPut";
            this.btnPut.Size = new System.Drawing.Size(75, 23);
            this.btnPut.TabIndex = 0;
            this.btnPut.Text = "Put";
            this.btnPut.UseVisualStyleBackColor = true;
            this.btnPut.Click += new System.EventHandler(this.btnPut_Click);
            // 
            // btnGet
            // 
            this.btnGet.Location = new System.Drawing.Point(982, 117);
            this.btnGet.Name = "btnGet";
            this.btnGet.Size = new System.Drawing.Size(75, 23);
            this.btnGet.TabIndex = 1;
            this.btnGet.Text = "Get";
            this.btnGet.UseVisualStyleBackColor = true;
            this.btnGet.Click += new System.EventHandler(this.btnGet_Click);
            // 
            // btnDelete
            // 
            this.btnDelete.Location = new System.Drawing.Point(982, 171);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(75, 23);
            this.btnDelete.TabIndex = 2;
            this.btnDelete.Text = "delete";
            this.btnDelete.UseVisualStyleBackColor = true;
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            // 
            // txtPutKey
            // 
            this.txtPutKey.Location = new System.Drawing.Point(379, 26);
            this.txtPutKey.Multiline = true;
            this.txtPutKey.Name = "txtPutKey";
            this.txtPutKey.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtPutKey.Size = new System.Drawing.Size(242, 41);
            this.txtPutKey.TabIndex = 3;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(376, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(65, 13);
            this.label1.TabIndex = 4;
            this.label1.Text = "levelDB Key";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(376, 81);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(65, 13);
            this.label2.TabIndex = 6;
            this.label2.Text = "levelDB Key";
            // 
            // txtGetKey
            // 
            this.txtGetKey.Location = new System.Drawing.Point(379, 97);
            this.txtGetKey.Multiline = true;
            this.txtGetKey.Name = "txtGetKey";
            this.txtGetKey.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtGetKey.Size = new System.Drawing.Size(242, 43);
            this.txtGetKey.TabIndex = 5;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(624, 148);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(65, 13);
            this.label3.TabIndex = 8;
            this.label3.Text = "levelDB Key";
            // 
            // txtDeleteKey
            // 
            this.txtDeleteKey.Location = new System.Drawing.Point(627, 174);
            this.txtDeleteKey.Name = "txtDeleteKey";
            this.txtDeleteKey.Size = new System.Drawing.Size(350, 20);
            this.txtDeleteKey.TabIndex = 7;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(624, 10);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(34, 13);
            this.label4.TabIndex = 10;
            this.label4.Text = "Value";
            // 
            // txtPutValue
            // 
            this.txtPutValue.Location = new System.Drawing.Point(627, 26);
            this.txtPutValue.Multiline = true;
            this.txtPutValue.Name = "txtPutValue";
            this.txtPutValue.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtPutValue.Size = new System.Drawing.Size(350, 41);
            this.txtPutValue.TabIndex = 9;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(624, 74);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(34, 13);
            this.label5.TabIndex = 12;
            this.label5.Text = "Value";
            // 
            // txtGetValue
            // 
            this.txtGetValue.Location = new System.Drawing.Point(627, 97);
            this.txtGetValue.Multiline = true;
            this.txtGetValue.Name = "txtGetValue";
            this.txtGetValue.ReadOnly = true;
            this.txtGetValue.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtGetValue.Size = new System.Drawing.Size(350, 43);
            this.txtGetValue.TabIndex = 11;
            // 
            // lbTableName
            // 
            this.lbTableName.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.lbTableName.FormattingEnabled = true;
            this.lbTableName.Items.AddRange(new object[] {
            "ROOT",
            "PRO",
            "COL",
            "OBJ",
            "LOGS"});
            this.lbTableName.Location = new System.Drawing.Point(535, 164);
            this.lbTableName.Name = "lbTableName";
            this.lbTableName.ScrollAlwaysVisible = true;
            this.lbTableName.Size = new System.Drawing.Size(83, 30);
            this.lbTableName.TabIndex = 13;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(532, 148);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(74, 13);
            this.label6.TabIndex = 14;
            this.label6.Text = "levelDB Table";
            // 
            // txtLogin
            // 
            this.txtLogin.Location = new System.Drawing.Point(12, 26);
            this.txtLogin.Name = "txtLogin";
            this.txtLogin.Size = new System.Drawing.Size(111, 20);
            this.txtLogin.TabIndex = 15;
            this.txtLogin.Text = "embiiuser";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(12, 9);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(29, 13);
            this.label7.TabIndex = 16;
            this.label7.Text = "login";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(139, 9);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(52, 13);
            this.label8.TabIndex = 18;
            this.label8.Text = "password";
            // 
            // txtPassword
            // 
            this.txtPassword.Location = new System.Drawing.Point(142, 26);
            this.txtPassword.Name = "txtPassword";
            this.txtPassword.Size = new System.Drawing.Size(108, 20);
            this.txtPassword.TabIndex = 17;
            this.txtPassword.Text = "embiipassword";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(13, 54);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(18, 13);
            this.label9.TabIndex = 20;
            this.label9.Text = "url";
            // 
            // txtUrl
            // 
            this.txtUrl.Location = new System.Drawing.Point(13, 71);
            this.txtUrl.Name = "txtUrl";
            this.txtUrl.Size = new System.Drawing.Size(218, 20);
            this.txtUrl.TabIndex = 19;
            this.txtUrl.Text = "http://127.0.0.1:18332";
            // 
            // btnTestConnection
            // 
            this.btnTestConnection.Location = new System.Drawing.Point(256, 24);
            this.btnTestConnection.Name = "btnTestConnection";
            this.btnTestConnection.Size = new System.Drawing.Size(97, 23);
            this.btnTestConnection.TabIndex = 21;
            this.btnTestConnection.Text = "Test Connection";
            this.btnTestConnection.UseVisualStyleBackColor = true;
            this.btnTestConnection.Click += new System.EventHandler(this.btnTestConnection_Click);
            // 
            // lblTotalBytes
            // 
            this.lblTotalBytes.AutoSize = true;
            this.lblTotalBytes.Location = new System.Drawing.Point(694, 206);
            this.lblTotalBytes.Name = "lblTotalBytes";
            this.lblTotalBytes.Size = new System.Drawing.Size(60, 13);
            this.lblTotalBytes.TabIndex = 23;
            this.lblTotalBytes.Text = "Total Bytes";
            // 
            // txtbalance
            // 
            this.txtbalance.Location = new System.Drawing.Point(288, 71);
            this.txtbalance.Name = "txtbalance";
            this.txtbalance.Size = new System.Drawing.Size(65, 20);
            this.txtbalance.TabIndex = 22;
            // 
            // txtSearchAddress
            // 
            this.txtSearchAddress.Location = new System.Drawing.Point(12, 135);
            this.txtSearchAddress.Name = "txtSearchAddress";
            this.txtSearchAddress.Size = new System.Drawing.Size(238, 20);
            this.txtSearchAddress.TabIndex = 25;
            this.txtSearchAddress.Text = "mzbQbFQeCx14eqAaGfFeZDbsCDonfxWncK";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(9, 120);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(65, 13);
            this.label11.TabIndex = 26;
            this.label11.Text = "Get Address";
            // 
            // btnSearch
            // 
            this.btnSearch.Location = new System.Drawing.Point(256, 133);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(58, 23);
            this.btnSearch.TabIndex = 27;
            this.btnSearch.Text = "Search";
            this.btnSearch.UseVisualStyleBackColor = true;
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
            // 
            // dgTransactions
            // 
            this.dgTransactions.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgTransactions.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.transaction_id,
            this.is_signed,
            this.signed_by,
            this.signature,
            this.filecount,
            this.messagecount,
            this.keywordcount,
            this.total_bytes,
            this.block_time,
            this.confirmations,
            this.buildtime});
            this.dgTransactions.Location = new System.Drawing.Point(12, 222);
            this.dgTransactions.Name = "dgTransactions";
            this.dgTransactions.Size = new System.Drawing.Size(1045, 370);
            this.dgTransactions.TabIndex = 30;
            // 
            // btnGetTransactionId
            // 
            this.btnGetTransactionId.Location = new System.Drawing.Point(409, 185);
            this.btnGetTransactionId.Name = "btnGetTransactionId";
            this.btnGetTransactionId.Size = new System.Drawing.Size(58, 23);
            this.btnGetTransactionId.TabIndex = 33;
            this.btnGetTransactionId.Text = "Search";
            this.btnGetTransactionId.UseVisualStyleBackColor = true;
            this.btnGetTransactionId.Click += new System.EventHandler(this.btnGetTransactionId_Click);
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(13, 171);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(95, 13);
            this.label12.TabIndex = 32;
            this.label12.Text = "Get Transaction Id";
            // 
            // txtTransactionId
            // 
            this.txtTransactionId.Location = new System.Drawing.Point(12, 187);
            this.txtTransactionId.Name = "txtTransactionId";
            this.txtTransactionId.Size = new System.Drawing.Size(391, 20);
            this.txtTransactionId.TabIndex = 31;
            this.txtTransactionId.Text = "a87d87b2b9821c4fc7d2c349cdda7542627b774d56db81e3a988503735b8a4f9";
            // 
            // transaction_id
            // 
            this.transaction_id.HeaderText = "Transaction ID";
            this.transaction_id.Name = "transaction_id";
            // 
            // is_signed
            // 
            this.is_signed.HeaderText = "Is Signed?";
            this.is_signed.Name = "is_signed";
            this.is_signed.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.is_signed.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            // 
            // signed_by
            // 
            this.signed_by.HeaderText = "Signed By";
            this.signed_by.Name = "signed_by";
            // 
            // signature
            // 
            this.signature.HeaderText = "Signature";
            this.signature.Name = "signature";
            // 
            // filecount
            // 
            this.filecount.HeaderText = "Files";
            this.filecount.Name = "filecount";
            // 
            // messagecount
            // 
            this.messagecount.HeaderText = "Messages";
            this.messagecount.Name = "messagecount";
            // 
            // keywordcount
            // 
            this.keywordcount.HeaderText = "Keywords";
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
            // confirmations
            // 
            this.confirmations.HeaderText = "Confirmations";
            this.confirmations.Name = "confirmations";
            // 
            // buildtime
            // 
            this.buildtime.HeaderText = "Build Time";
            this.buildtime.Name = "buildtime";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1069, 604);
            this.Controls.Add(this.btnGetTransactionId);
            this.Controls.Add(this.label12);
            this.Controls.Add(this.txtTransactionId);
            this.Controls.Add(this.dgTransactions);
            this.Controls.Add(this.btnSearch);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.txtSearchAddress);
            this.Controls.Add(this.lblTotalBytes);
            this.Controls.Add(this.txtbalance);
            this.Controls.Add(this.btnTestConnection);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.txtUrl);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.txtPassword);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.txtLogin);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.lbTableName);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.txtGetValue);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.txtPutValue);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.txtDeleteKey);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txtGetKey);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtPutKey);
            this.Controls.Add(this.btnDelete);
            this.Controls.Add(this.btnGet);
            this.Controls.Add(this.btnPut);
            this.MaximumSize = new System.Drawing.Size(1085, 643);
            this.MinimumSize = new System.Drawing.Size(1085, 643);
            this.Name = "Form1";
            this.Text = "Sup!? Just testing ";
            ((System.ComponentModel.ISupportInitialize)(this.dgTransactions)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnPut;
        private System.Windows.Forms.Button btnGet;
        private System.Windows.Forms.Button btnDelete;
        private System.Windows.Forms.TextBox txtPutKey;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtGetKey;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtDeleteKey;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtPutValue;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txtGetValue;
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
        private System.Windows.Forms.DataGridViewTextBoxColumn transaction_id;
        private System.Windows.Forms.DataGridViewCheckBoxColumn is_signed;
        private System.Windows.Forms.DataGridViewTextBoxColumn signed_by;
        private System.Windows.Forms.DataGridViewTextBoxColumn signature;
        private System.Windows.Forms.DataGridViewTextBoxColumn filecount;
        private System.Windows.Forms.DataGridViewTextBoxColumn messagecount;
        private System.Windows.Forms.DataGridViewTextBoxColumn keywordcount;
        private System.Windows.Forms.DataGridViewTextBoxColumn total_bytes;
        private System.Windows.Forms.DataGridViewTextBoxColumn block_time;
        private System.Windows.Forms.DataGridViewTextBoxColumn confirmations;
        private System.Windows.Forms.DataGridViewTextBoxColumn buildtime;
    }
}

