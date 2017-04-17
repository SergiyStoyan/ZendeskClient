namespace Cliver.ZendeskClient
{
    partial class TicketForm
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
            this.subject = new System.Windows.Forms.ComboBox();
            this.description = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.cancel = new System.Windows.Forms.Button();
            this.ok = new System.Windows.Forms.Button();
            this.add_attachment = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.attachments = new System.Windows.Forms.Panel();
            this.IncludeScreenshot = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // subject
            // 
            this.subject.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.subject.FormattingEnabled = true;
            this.subject.Location = new System.Drawing.Point(12, 25);
            this.subject.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.subject.Name = "subject";
            this.subject.Size = new System.Drawing.Size(393, 21);
            this.subject.TabIndex = 0;
            // 
            // description
            // 
            this.description.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.description.Location = new System.Drawing.Point(12, 65);
            this.description.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.description.Multiline = true;
            this.description.Name = "description";
            this.description.Size = new System.Drawing.Size(393, 112);
            this.description.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(46, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Subject:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 49);
            this.label2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(63, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "Description:";
            // 
            // cancel
            // 
            this.cancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.cancel.Location = new System.Drawing.Point(330, 338);
            this.cancel.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.cancel.Name = "cancel";
            this.cancel.Size = new System.Drawing.Size(74, 23);
            this.cancel.TabIndex = 4;
            this.cancel.Text = "Cancel";
            this.cancel.UseVisualStyleBackColor = true;
            this.cancel.Click += new System.EventHandler(this.cancel_Click);
            // 
            // ok
            // 
            this.ok.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.ok.Location = new System.Drawing.Point(248, 338);
            this.ok.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.ok.Name = "ok";
            this.ok.Size = new System.Drawing.Size(74, 23);
            this.ok.TabIndex = 5;
            this.ok.Text = "Send";
            this.ok.UseVisualStyleBackColor = true;
            this.ok.Click += new System.EventHandler(this.ok_Click);
            // 
            // add_attachment
            // 
            this.add_attachment.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.add_attachment.Location = new System.Drawing.Point(92, 207);
            this.add_attachment.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.add_attachment.Name = "add_attachment";
            this.add_attachment.Size = new System.Drawing.Size(40, 23);
            this.add_attachment.TabIndex = 6;
            this.add_attachment.Text = "+";
            this.add_attachment.UseVisualStyleBackColor = true;
            this.add_attachment.Click += new System.EventHandler(this.add_attachment_Click);
            // 
            // label3
            // 
            this.label3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 214);
            this.label3.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(69, 13);
            this.label3.TabIndex = 8;
            this.label3.Text = "Attachments:";
            // 
            // attachments
            // 
            this.attachments.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.attachments.AutoScroll = true;
            this.attachments.Location = new System.Drawing.Point(14, 230);
            this.attachments.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.attachments.Name = "attachments";
            this.attachments.Size = new System.Drawing.Size(390, 94);
            this.attachments.TabIndex = 9;
            // 
            // IncludeScreenshot
            // 
            this.IncludeScreenshot.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.IncludeScreenshot.AutoSize = true;
            this.IncludeScreenshot.Checked = true;
            this.IncludeScreenshot.CheckState = System.Windows.Forms.CheckState.Checked;
            this.IncludeScreenshot.Location = new System.Drawing.Point(14, 183);
            this.IncludeScreenshot.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.IncludeScreenshot.Name = "IncludeScreenshot";
            this.IncludeScreenshot.Size = new System.Drawing.Size(118, 17);
            this.IncludeScreenshot.TabIndex = 10;
            this.IncludeScreenshot.Text = "Include Screenshot";
            this.IncludeScreenshot.UseVisualStyleBackColor = true;
            // 
            // TicketForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.ClientSize = new System.Drawing.Size(416, 373);
            this.Controls.Add(this.IncludeScreenshot);
            this.Controls.Add(this.attachments);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.add_attachment);
            this.Controls.Add(this.ok);
            this.Controls.Add(this.cancel);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.description);
            this.Controls.Add(this.subject);
            this.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "TicketForm";
            this.Text = "New Ticket";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox subject;
        private System.Windows.Forms.TextBox description;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button cancel;
        private System.Windows.Forms.Button ok;
        private System.Windows.Forms.Button add_attachment;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Panel attachments;
        private System.Windows.Forms.CheckBox IncludeScreenshot;
    }
}