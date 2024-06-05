namespace PracticDemoExam
{
    partial class UserForm
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
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.dBdemoExamDataSetBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.dBdemoExamDataSet = new PracticDemoExam.DBdemoExamDataSet();
            this.addRecordBtn = new System.Windows.Forms.Button();
            this.exitBtn = new System.Windows.Forms.Button();
            this.editRecordsBtn = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dBdemoExamDataSetBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dBdemoExamDataSet)).BeginInit();
            this.SuspendLayout();
            // 
            // dataGridView1
            // 
            this.dataGridView1.AutoGenerateColumns = false;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.DataSource = this.dBdemoExamDataSetBindingSource;
            this.dataGridView1.Location = new System.Drawing.Point(12, 28);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.Size = new System.Drawing.Size(1254, 232);
            this.dataGridView1.TabIndex = 0;
            // 
            // dBdemoExamDataSetBindingSource
            // 
            this.dBdemoExamDataSetBindingSource.DataSource = this.dBdemoExamDataSet;
            this.dBdemoExamDataSetBindingSource.Position = 0;
            // 
            // dBdemoExamDataSet
            // 
            this.dBdemoExamDataSet.DataSetName = "DBdemoExamDataSet";
            this.dBdemoExamDataSet.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // addRecordBtn
            // 
            this.addRecordBtn.Location = new System.Drawing.Point(22, 266);
            this.addRecordBtn.Name = "addRecordBtn";
            this.addRecordBtn.Size = new System.Drawing.Size(173, 75);
            this.addRecordBtn.TabIndex = 1;
            this.addRecordBtn.Text = "Добавить";
            this.addRecordBtn.UseVisualStyleBackColor = true;
            // 
            // exitBtn
            // 
            this.exitBtn.Location = new System.Drawing.Point(22, 394);
            this.exitBtn.Name = "exitBtn";
            this.exitBtn.Size = new System.Drawing.Size(142, 78);
            this.exitBtn.TabIndex = 2;
            this.exitBtn.Text = "Выйти";
            this.exitBtn.UseVisualStyleBackColor = true;
            this.exitBtn.Click += new System.EventHandler(this.exitBtn_Click);
            // 
            // editRecordsBtn
            // 
            this.editRecordsBtn.Location = new System.Drawing.Point(261, 266);
            this.editRecordsBtn.Name = "editRecordsBtn";
            this.editRecordsBtn.Size = new System.Drawing.Size(173, 75);
            this.editRecordsBtn.TabIndex = 3;
            this.editRecordsBtn.Text = "Изменить";
            this.editRecordsBtn.UseVisualStyleBackColor = true;
            // 
            // UserForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1278, 550);
            this.Controls.Add(this.editRecordsBtn);
            this.Controls.Add(this.exitBtn);
            this.Controls.Add(this.addRecordBtn);
            this.Controls.Add(this.dataGridView1);
            this.Name = "UserForm";
            this.Text = "UserForm";
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dBdemoExamDataSetBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dBdemoExamDataSet)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.BindingSource dBdemoExamDataSetBindingSource;
        private DBdemoExamDataSet dBdemoExamDataSet;
        private System.Windows.Forms.Button addRecordBtn;
        private System.Windows.Forms.Button exitBtn;
        private System.Windows.Forms.Button editRecordsBtn;
    }
}