namespace TRPJHF
{
    partial class WorksheetRegistrationForm
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
            this.panelMainContainer = new System.Windows.Forms.Panel();
            this.panelWorks = new System.Windows.Forms.Panel();
            this.panelTotals = new System.Windows.Forms.Panel();
            this.panelMainContainer.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelMainContainer
            // 
            this.panelMainContainer.Controls.Add(this.panelWorks);
            this.panelMainContainer.Controls.Add(this.panelTotals);
            this.panelMainContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelMainContainer.Location = new System.Drawing.Point(0, 0);
            this.panelMainContainer.Name = "panelMainContainer";
            this.panelMainContainer.Size = new System.Drawing.Size(800, 450);
            this.panelMainContainer.TabIndex = 0;
            // 
            // panelWorks
            // 
            this.panelWorks.AutoScroll = true;
            this.panelWorks.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelWorks.Location = new System.Drawing.Point(0, 0);
            this.panelWorks.Name = "panelWorks";
            this.panelWorks.Size = new System.Drawing.Size(800, 350);
            this.panelWorks.TabIndex = 1;
            // 
            // panelTotals
            // 
            this.panelTotals.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelTotals.Location = new System.Drawing.Point(0, 350);
            this.panelTotals.Name = "panelTotals";
            this.panelTotals.Size = new System.Drawing.Size(800, 100);
            this.panelTotals.TabIndex = 0;
            // 
            // WorksheetRegistrationForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.panelMainContainer);
            this.Name = "WorksheetRegistrationForm";
            this.Text = "Worksheet Registration";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.WorksheetRegistrationForm_FormClosing);
            this.panelMainContainer.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Panel panelMainContainer;
        private System.Windows.Forms.Panel panelWorks;
        private System.Windows.Forms.Panel panelTotals;
    }
}