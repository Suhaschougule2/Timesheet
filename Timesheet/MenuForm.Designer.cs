namespace Timesheet
{
    partial class MenuForm
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
            this.tsOpenButton = new System.Windows.Forms.Button();
            this.recordsOpenButton = new System.Windows.Forms.Button();
            this.tsPanel = new System.Windows.Forms.Panel();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.delete_button = new System.Windows.Forms.Button();
            this.update_button = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.save_button = new System.Windows.Forms.Button();
            this.textBox = new System.Windows.Forms.TextBox();
            this.comboBox = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.datePicker = new System.Windows.Forms.DateTimePicker();
            this.label1 = new System.Windows.Forms.Label();
            this.rsPanel = new System.Windows.Forms.Panel();
            this.treeView = new System.Windows.Forms.TreeView();
            this.errorProvider = new System.Windows.Forms.ErrorProvider(this.components);
            this.tsPanel.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.rsPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider)).BeginInit();
            this.SuspendLayout();
            // 
            // tsOpenButton
            // 
            this.tsOpenButton.Font = new System.Drawing.Font("Arial", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tsOpenButton.Location = new System.Drawing.Point(27, 12);
            this.tsOpenButton.Name = "tsOpenButton";
            this.tsOpenButton.Size = new System.Drawing.Size(119, 30);
            this.tsOpenButton.TabIndex = 0;
            this.tsOpenButton.Text = "Timesheet";
            this.tsOpenButton.UseVisualStyleBackColor = true;
            this.tsOpenButton.Click += new System.EventHandler(this.tsOpenButton_Click);
            // 
            // recordsOpenButton
            // 
            this.recordsOpenButton.Font = new System.Drawing.Font("Arial", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.recordsOpenButton.Location = new System.Drawing.Point(152, 12);
            this.recordsOpenButton.Name = "recordsOpenButton";
            this.recordsOpenButton.Size = new System.Drawing.Size(118, 30);
            this.recordsOpenButton.TabIndex = 1;
            this.recordsOpenButton.Text = "Records";
            this.recordsOpenButton.UseVisualStyleBackColor = true;
            this.recordsOpenButton.Click += new System.EventHandler(this.recordsOpenButton_Click);
            // 
            // tsPanel
            // 
            this.tsPanel.Controls.Add(this.groupBox1);
            this.tsPanel.Controls.Add(this.rsPanel);
            this.tsPanel.Location = new System.Drawing.Point(27, 48);
            this.tsPanel.Name = "tsPanel";
            this.tsPanel.Size = new System.Drawing.Size(721, 501);
            this.tsPanel.TabIndex = 2;
            this.tsPanel.Paint += new System.Windows.Forms.PaintEventHandler(this.tsPanel_Paint);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.delete_button);
            this.groupBox1.Controls.Add(this.update_button);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.save_button);
            this.groupBox1.Controls.Add(this.textBox);
            this.groupBox1.Controls.Add(this.comboBox);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.datePicker);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Font = new System.Drawing.Font("Arial", 10.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox1.Location = new System.Drawing.Point(13, 13);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(672, 473);
            this.groupBox1.TabIndex = 7;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "TimeSheet";
            // 
            // delete_button
            // 
            this.delete_button.Font = new System.Drawing.Font("Arial", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.delete_button.Location = new System.Drawing.Point(194, 388);
            this.delete_button.Name = "delete_button";
            this.delete_button.Size = new System.Drawing.Size(110, 30);
            this.delete_button.TabIndex = 8;
            this.delete_button.Text = "Delete";
            this.delete_button.UseVisualStyleBackColor = true;
            this.delete_button.Click += new System.EventHandler(this.delete_button_Click);
            // 
            // update_button
            // 
            this.update_button.Font = new System.Drawing.Font("Arial", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.update_button.Location = new System.Drawing.Point(357, 388);
            this.update_button.Name = "update_button";
            this.update_button.Size = new System.Drawing.Size(110, 30);
            this.update_button.TabIndex = 7;
            this.update_button.Text = "Update";
            this.update_button.UseVisualStyleBackColor = true;
            this.update_button.Click += new System.EventHandler(this.update_button_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Arial", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(18, 178);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(116, 19);
            this.label3.TabIndex = 6;
            this.label3.Text = "Work Details:";
            // 
            // save_button
            // 
            this.save_button.Font = new System.Drawing.Font("Arial", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.save_button.Location = new System.Drawing.Point(520, 388);
            this.save_button.Name = "save_button";
            this.save_button.Size = new System.Drawing.Size(110, 30);
            this.save_button.TabIndex = 5;
            this.save_button.Text = "Save";
            this.save_button.UseVisualStyleBackColor = true;
            this.save_button.Click += new System.EventHandler(this.save_button_Click);
            // 
            // textBox
            // 
            this.textBox.Font = new System.Drawing.Font("Arial", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBox.Location = new System.Drawing.Point(165, 178);
            this.textBox.MaxLength = 10000;
            this.textBox.Multiline = true;
            this.textBox.Name = "textBox";
            this.textBox.Size = new System.Drawing.Size(467, 168);
            this.textBox.TabIndex = 4;
            this.textBox.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textBox_KeyPress);
            this.textBox.Validating += new System.ComponentModel.CancelEventHandler(this.textBox_Validating);
            // 
            // comboBox
            // 
            this.comboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox.Font = new System.Drawing.Font("Arial", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.comboBox.FormattingEnabled = true;
            this.comboBox.Items.AddRange(new object[] {
            "DCS",
            "EA",
            "EAE",
            "EDGE",
            "ET",
            "GDP",
            "ML",
            "SET",
            "SOLUTION PORTAL",
            "OCS"});
            this.comboBox.Location = new System.Drawing.Point(165, 116);
            this.comboBox.Name = "comboBox";
            this.comboBox.Size = new System.Drawing.Size(467, 27);
            this.comboBox.TabIndex = 3;
            this.comboBox.Validating += new System.ComponentModel.CancelEventHandler(this.comboBox_Validating);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Arial", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(18, 116);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(56, 19);
            this.label2.TabIndex = 2;
            this.label2.Text = "Team:";
            // 
            // datePicker
            // 
            this.datePicker.Font = new System.Drawing.Font("Arial", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.datePicker.Location = new System.Drawing.Point(165, 55);
            this.datePicker.MaxDate = new System.DateTime(3001, 12, 31, 0, 0, 0, 0);
            this.datePicker.MinDate = new System.DateTime(1900, 1, 1, 0, 0, 0, 0);
            this.datePicker.Name = "datePicker";
            this.datePicker.Size = new System.Drawing.Size(467, 27);
            this.datePicker.TabIndex = 1;
            this.datePicker.Value = new System.DateTime(2023, 10, 8, 0, 0, 0, 0);
            this.datePicker.Validating += new System.ComponentModel.CancelEventHandler(this.datePicker_Validating);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Arial", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(18, 61);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(51, 19);
            this.label1.TabIndex = 0;
            this.label1.Text = "Date:";
            // 
            // rsPanel
            // 
            this.rsPanel.Controls.Add(this.treeView);
            this.rsPanel.Location = new System.Drawing.Point(13, 13);
            this.rsPanel.Name = "rsPanel";
            this.rsPanel.Size = new System.Drawing.Size(689, 473);
            this.rsPanel.TabIndex = 8;
            this.rsPanel.Paint += new System.Windows.Forms.PaintEventHandler(this.rsPanel_Paint);
            // 
            // treeView
            // 
            this.treeView.Location = new System.Drawing.Point(6, 0);
            this.treeView.Name = "treeView";
            this.treeView.Size = new System.Drawing.Size(479, 473);
            this.treeView.TabIndex = 1;
            // 
            // errorProvider
            // 
            this.errorProvider.ContainerControl = this;
            // 
            // MenuForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(769, 570);
            this.Controls.Add(this.tsPanel);
            this.Controls.Add(this.recordsOpenButton);
            this.Controls.Add(this.tsOpenButton);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "MenuForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "MenuForm";
            this.Load += new System.EventHandler(this.MenuForm_Load);
            this.tsPanel.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.rsPanel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button tsOpenButton;
        private System.Windows.Forms.Button recordsOpenButton;
        private System.Windows.Forms.Panel tsPanel;
        private System.Windows.Forms.Panel rsPanel;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button delete_button;
        private System.Windows.Forms.Button update_button;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button save_button;
        private System.Windows.Forms.TextBox textBox;
        private System.Windows.Forms.ComboBox comboBox;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.DateTimePicker datePicker;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TreeView treeView;
        private System.Windows.Forms.ErrorProvider errorProvider;
    }
}