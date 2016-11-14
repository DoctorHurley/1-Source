/* TestPanel.Designer.cs
   Author: Mike Hurley
   This file handles higher level form components
   within the WPF API.
*/

namespace AmalgIm
{
    partial class TestPanel
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
            this.DropDown = new System.Windows.Forms.ComboBox();
            this.SuspendLayout();
            // 
            // DropDown
            // 
            this.DropDown.FormattingEnabled = true;
            this.DropDown.Items.AddRange(new object[] {
            "Test 1",
            "Test 2",
            "Test 3",
            "Nutshackk"});
            this.DropDown.Location = new System.Drawing.Point(36, 12);
            this.DropDown.Name = "DropDown";
            this.DropDown.Size = new System.Drawing.Size(193, 24);
            this.DropDown.TabIndex = 0;
            this.DropDown.SelectedIndexChanged += new System.EventHandler(this.comboBox1_SelectedIndexChanged);
            // 
            // TestPanel
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(282, 253);
            this.Controls.Add(this.DropDown);
            this.Name = "TestPanel";
            this.Text = "AmalgIm";
            this.Load += new System.EventHandler(this.TestPanel_Load);
            this.ResumeLayout(false);

        }

        #endregion

        public System.Windows.Forms.ComboBox DropDown;
    }
}

