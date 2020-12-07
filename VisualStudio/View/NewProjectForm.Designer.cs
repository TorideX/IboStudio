namespace VisualStudio.View
{
    partial class NewProjectForm
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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.checkBox_CreateSubDir = new System.Windows.Forms.CheckBox();
            this.Button_ChooseFolder = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.textBox_Folder = new System.Windows.Forms.TextBox();
            this.textBox_Name = new System.Windows.Forms.TextBox();
            this.Button_Cancel = new System.Windows.Forms.Button();
            this.Button_OK = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.checkBox_CreateSubDir);
            this.groupBox1.Controls.Add(this.Button_ChooseFolder);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.textBox_Folder);
            this.groupBox1.Controls.Add(this.textBox_Name);
            this.groupBox1.Location = new System.Drawing.Point(12, 22);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(227, 193);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "New Project";
            // 
            // checkBox_CreateSubDir
            // 
            this.checkBox_CreateSubDir.AutoSize = true;
            this.checkBox_CreateSubDir.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.checkBox_CreateSubDir.Location = new System.Drawing.Point(6, 157);
            this.checkBox_CreateSubDir.Name = "checkBox_CreateSubDir";
            this.checkBox_CreateSubDir.Size = new System.Drawing.Size(178, 19);
            this.checkBox_CreateSubDir.TabIndex = 5;
            this.checkBox_CreateSubDir.Text = "Create subdirectory \"Proj_1\"";
            this.checkBox_CreateSubDir.UseVisualStyleBackColor = true;
            this.checkBox_CreateSubDir.CheckedChanged += new System.EventHandler(this.checkBox_CreateSubDir_CheckedChanged);
            // 
            // Button_ChooseFolder
            // 
            this.Button_ChooseFolder.Location = new System.Drawing.Point(3, 119);
            this.Button_ChooseFolder.Name = "Button_ChooseFolder";
            this.Button_ChooseFolder.Size = new System.Drawing.Size(215, 23);
            this.Button_ChooseFolder.TabIndex = 4;
            this.Button_ChooseFolder.Text = "Choose Folder";
            this.Button_ChooseFolder.UseVisualStyleBackColor = true;
            this.Button_ChooseFolder.Click += new System.EventHandler(this.Button_ChooseFolder_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(3, 77);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(36, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "Folder";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 29);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(35, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Name";
            // 
            // textBox_Folder
            // 
            this.textBox_Folder.Enabled = false;
            this.textBox_Folder.Location = new System.Drawing.Point(3, 93);
            this.textBox_Folder.Name = "textBox_Folder";
            this.textBox_Folder.Size = new System.Drawing.Size(215, 20);
            this.textBox_Folder.TabIndex = 1;
            this.textBox_Folder.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // textBox_Name
            // 
            this.textBox_Name.Location = new System.Drawing.Point(6, 45);
            this.textBox_Name.Name = "textBox_Name";
            this.textBox_Name.Size = new System.Drawing.Size(215, 20);
            this.textBox_Name.TabIndex = 0;
            this.textBox_Name.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.textBox_Name.TextChanged += new System.EventHandler(this.textBox_Name_TextChanged);
            // 
            // Button_Cancel
            // 
            this.Button_Cancel.Location = new System.Drawing.Point(12, 230);
            this.Button_Cancel.Name = "Button_Cancel";
            this.Button_Cancel.Size = new System.Drawing.Size(105, 23);
            this.Button_Cancel.TabIndex = 1;
            this.Button_Cancel.Text = "Cancel";
            this.Button_Cancel.UseVisualStyleBackColor = true;
            this.Button_Cancel.Click += new System.EventHandler(this.Button_Cancel_Click);
            // 
            // Button_OK
            // 
            this.Button_OK.Enabled = false;
            this.Button_OK.Location = new System.Drawing.Point(134, 230);
            this.Button_OK.Name = "Button_OK";
            this.Button_OK.Size = new System.Drawing.Size(105, 23);
            this.Button_OK.TabIndex = 2;
            this.Button_OK.Text = "OK";
            this.Button_OK.UseVisualStyleBackColor = true;
            this.Button_OK.Click += new System.EventHandler(this.Button_OK_Click);
            // 
            // NewProjectForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(251, 267);
            this.Controls.Add(this.Button_OK);
            this.Controls.Add(this.Button_Cancel);
            this.Controls.Add(this.groupBox1);
            this.MaximumSize = new System.Drawing.Size(267, 306);
            this.MinimumSize = new System.Drawing.Size(267, 306);
            this.Name = "NewProjectForm";
            this.Text = "New Project";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button Button_Cancel;
        private System.Windows.Forms.Button Button_OK;
        private System.Windows.Forms.CheckBox checkBox_CreateSubDir;
        private System.Windows.Forms.Button Button_ChooseFolder;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textBox_Folder;
        private System.Windows.Forms.TextBox textBox_Name;
    }
}