
namespace ClientLR4
{
    partial class SearchServers
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
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.searchServBtn = new System.Windows.Forms.Button();
            this.ipChosenServ = new System.Windows.Forms.Label();
            this.ipServ = new System.Windows.Forms.Label();
            this.portChoosenServ = new System.Windows.Forms.Label();
            this.portServ = new System.Windows.Forms.Label();
            this.connectBtn = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // dataGridView1
            // 
            this.dataGridView1.BackgroundColor = System.Drawing.SystemColors.ControlLight;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(12, 12);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.ReadOnly = true;
            this.dataGridView1.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridView1.Size = new System.Drawing.Size(641, 264);
            this.dataGridView1.TabIndex = 0;
            this.dataGridView1.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellClick);
            // 
            // searchServBtn
            // 
            this.searchServBtn.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.searchServBtn.Location = new System.Drawing.Point(659, 12);
            this.searchServBtn.Name = "searchServBtn";
            this.searchServBtn.Size = new System.Drawing.Size(129, 54);
            this.searchServBtn.TabIndex = 2;
            this.searchServBtn.Text = "Поиск серверов";
            this.searchServBtn.UseVisualStyleBackColor = true;
            this.searchServBtn.Click += new System.EventHandler(this.searchServBtn_Click);
            // 
            // ipChosenServ
            // 
            this.ipChosenServ.AutoSize = true;
            this.ipChosenServ.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F);
            this.ipChosenServ.Location = new System.Drawing.Point(12, 292);
            this.ipChosenServ.Name = "ipChosenServ";
            this.ipChosenServ.Size = new System.Drawing.Size(255, 26);
            this.ipChosenServ.TabIndex = 3;
            this.ipChosenServ.Text = "IP выбранного сервера:";
            // 
            // ipServ
            // 
            this.ipServ.AutoSize = true;
            this.ipServ.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F);
            this.ipServ.Location = new System.Drawing.Point(264, 292);
            this.ipServ.Name = "ipServ";
            this.ipServ.Size = new System.Drawing.Size(0, 26);
            this.ipServ.TabIndex = 4;
            // 
            // portChoosenServ
            // 
            this.portChoosenServ.AutoSize = true;
            this.portChoosenServ.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F);
            this.portChoosenServ.Location = new System.Drawing.Point(12, 329);
            this.portChoosenServ.Name = "portChoosenServ";
            this.portChoosenServ.Size = new System.Drawing.Size(284, 26);
            this.portChoosenServ.TabIndex = 5;
            this.portChoosenServ.Text = "Порт выбранного сервера:";
            // 
            // portServ
            // 
            this.portServ.AutoSize = true;
            this.portServ.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F);
            this.portServ.Location = new System.Drawing.Point(302, 329);
            this.portServ.Name = "portServ";
            this.portServ.Size = new System.Drawing.Size(0, 26);
            this.portServ.TabIndex = 6;
            // 
            // connectBtn
            // 
            this.connectBtn.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.connectBtn.Location = new System.Drawing.Point(12, 368);
            this.connectBtn.Name = "connectBtn";
            this.connectBtn.Size = new System.Drawing.Size(147, 40);
            this.connectBtn.TabIndex = 7;
            this.connectBtn.Text = "Подключиться";
            this.connectBtn.UseVisualStyleBackColor = true;
            this.connectBtn.Click += new System.EventHandler(this.connectBtn_Click);
            // 
            // SearchServers
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.connectBtn);
            this.Controls.Add(this.portServ);
            this.Controls.Add(this.portChoosenServ);
            this.Controls.Add(this.ipServ);
            this.Controls.Add(this.ipChosenServ);
            this.Controls.Add(this.searchServBtn);
            this.Controls.Add(this.dataGridView1);
            this.Name = "SearchServers";
            this.Text = "SearchServers";
            this.Load += new System.EventHandler(this.SearchServers_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Button searchServBtn;
        private System.Windows.Forms.Label ipChosenServ;
        private System.Windows.Forms.Label ipServ;
        private System.Windows.Forms.Label portChoosenServ;
        private System.Windows.Forms.Label portServ;
        private System.Windows.Forms.Button connectBtn;
    }
}