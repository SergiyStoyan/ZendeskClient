namespace Cliver.ZendeskClient
{
    partial class SettingsForm
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
            this.bOk = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.ProcessName = new System.Windows.Forms.TextBox();
            this.DumpRegex = new System.Windows.Forms.TextBox();
            this.EventUrl = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.CheckPeriodInSecs = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.bCancel = new System.Windows.Forms.Button();
            this.TicketKey = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.TicketModifierKey1 = new System.Windows.Forms.ComboBox();
            this.label6 = new System.Windows.Forms.Label();
            this.TicketModifierKey2 = new System.Windows.Forms.ComboBox();
            this.label7 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.DumpRegexIgnoreCase = new System.Windows.Forms.CheckBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.DumpRegexSingleLine = new System.Windows.Forms.CheckBox();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // bOk
            // 
            this.bOk.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.bOk.Location = new System.Drawing.Point(116, 337);
            this.bOk.Name = "bOk";
            this.bOk.Size = new System.Drawing.Size(75, 23);
            this.bOk.TabIndex = 0;
            this.bOk.Text = "OK";
            this.bOk.UseVisualStyleBackColor = true;
            this.bOk.Click += new System.EventHandler(this.bOk_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(9, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(208, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Process Name Pattern (wildcards allowed):";
            // 
            // ProcessName
            // 
            this.ProcessName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.ProcessName.Location = new System.Drawing.Point(12, 25);
            this.ProcessName.Name = "ProcessName";
            this.ProcessName.Size = new System.Drawing.Size(260, 20);
            this.ProcessName.TabIndex = 2;
            // 
            // DumpRegex
            // 
            this.DumpRegex.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.DumpRegex.Location = new System.Drawing.Point(10, 19);
            this.DumpRegex.Name = "DumpRegex";
            this.DumpRegex.Size = new System.Drawing.Size(238, 20);
            this.DumpRegex.TabIndex = 4;
            // 
            // EventUrl
            // 
            this.EventUrl.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.EventUrl.Location = new System.Drawing.Point(12, 159);
            this.EventUrl.Name = "EventUrl";
            this.EventUrl.Size = new System.Drawing.Size(260, 20);
            this.EventUrl.TabIndex = 6;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(9, 143);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(65, 13);
            this.label3.TabIndex = 5;
            this.label3.Text = "Channel Url:";
            // 
            // CheckPeriodInSecs
            // 
            this.CheckPeriodInSecs.Location = new System.Drawing.Point(12, 208);
            this.CheckPeriodInSecs.Name = "CheckPeriodInSecs";
            this.CheckPeriodInSecs.Size = new System.Drawing.Size(62, 20);
            this.CheckPeriodInSecs.TabIndex = 8;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(9, 192);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(105, 13);
            this.label4.TabIndex = 7;
            this.label4.Text = "Check Period (secs):";
            // 
            // bCancel
            // 
            this.bCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.bCancel.Location = new System.Drawing.Point(197, 337);
            this.bCancel.Name = "bCancel";
            this.bCancel.Size = new System.Drawing.Size(75, 23);
            this.bCancel.TabIndex = 9;
            this.bCancel.Text = "Cancel";
            this.bCancel.UseVisualStyleBackColor = true;
            this.bCancel.Click += new System.EventHandler(this.bCancel_Click);
            // 
            // TicketKey
            // 
            this.TicketKey.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.TicketKey.FormattingEnabled = true;
            this.TicketKey.Location = new System.Drawing.Point(10, 38);
            this.TicketKey.Name = "TicketKey";
            this.TicketKey.Size = new System.Drawing.Size(75, 21);
            this.TicketKey.TabIndex = 11;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(10, 22);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(28, 13);
            this.label5.TabIndex = 10;
            this.label5.Text = "Key:";
            // 
            // TicketModifierKey1
            // 
            this.TicketModifierKey1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.TicketModifierKey1.FormattingEnabled = true;
            this.TicketModifierKey1.Location = new System.Drawing.Point(91, 38);
            this.TicketModifierKey1.Name = "TicketModifierKey1";
            this.TicketModifierKey1.Size = new System.Drawing.Size(75, 21);
            this.TicketModifierKey1.TabIndex = 13;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(92, 22);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(74, 13);
            this.label6.TabIndex = 12;
            this.label6.Text = "Modifier Key1:";
            // 
            // TicketModifierKey2
            // 
            this.TicketModifierKey2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.TicketModifierKey2.FormattingEnabled = true;
            this.TicketModifierKey2.Location = new System.Drawing.Point(173, 38);
            this.TicketModifierKey2.Name = "TicketModifierKey2";
            this.TicketModifierKey2.Size = new System.Drawing.Size(75, 21);
            this.TicketModifierKey2.TabIndex = 15;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(174, 22);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(74, 13);
            this.label7.TabIndex = 14;
            this.label7.Text = "Modifier Key2:";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.TicketModifierKey1);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.TicketModifierKey2);
            this.groupBox1.Controls.Add(this.TicketKey);
            this.groupBox1.Controls.Add(this.label7);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Location = new System.Drawing.Point(12, 246);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(258, 69);
            this.groupBox1.TabIndex = 17;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Terminating Key Combination";
            // 
            // DumpRegexIgnoreCase
            // 
            this.DumpRegexIgnoreCase.AutoSize = true;
            this.DumpRegexIgnoreCase.Location = new System.Drawing.Point(10, 45);
            this.DumpRegexIgnoreCase.Name = "DumpRegexIgnoreCase";
            this.DumpRegexIgnoreCase.Size = new System.Drawing.Size(83, 17);
            this.DumpRegexIgnoreCase.TabIndex = 18;
            this.DumpRegexIgnoreCase.Text = "Ignore Case";
            this.DumpRegexIgnoreCase.UseVisualStyleBackColor = true;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.DumpRegexSingleLine);
            this.groupBox2.Controls.Add(this.DumpRegexIgnoreCase);
            this.groupBox2.Controls.Add(this.DumpRegex);
            this.groupBox2.Location = new System.Drawing.Point(12, 58);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(258, 72);
            this.groupBox2.TabIndex = 19;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Regex";
            // 
            // DumpRegexSingleLine
            // 
            this.DumpRegexSingleLine.AutoSize = true;
            this.DumpRegexSingleLine.Location = new System.Drawing.Point(95, 45);
            this.DumpRegexSingleLine.Name = "DumpRegexSingleLine";
            this.DumpRegexSingleLine.Size = new System.Drawing.Size(78, 17);
            this.DumpRegexSingleLine.TabIndex = 19;
            this.DumpRegexSingleLine.Text = "Single Line";
            this.DumpRegexSingleLine.UseVisualStyleBackColor = true;
            // 
            // SettingsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 372);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.bCancel);
            this.Controls.Add(this.CheckPeriodInSecs);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.EventUrl);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.ProcessName);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.bOk);
            this.Name = "SettingsForm";
            this.Text = "Settings";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button bOk;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox ProcessName;
        private System.Windows.Forms.TextBox DumpRegex;
        private System.Windows.Forms.TextBox EventUrl;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox CheckPeriodInSecs;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button bCancel;
        private System.Windows.Forms.ComboBox TicketKey;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ComboBox TicketModifierKey1;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.ComboBox TicketModifierKey2;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.CheckBox DumpRegexIgnoreCase;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.CheckBox DumpRegexSingleLine;
    }
}