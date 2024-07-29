
namespace ClientLR4
{
    partial class Form1
    {
        /// <summary>
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.richTextBox1 = new System.Windows.Forms.RichTextBox();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.LBMIn = new System.Windows.Forms.Label();
            this.LBSec = new System.Windows.Forms.Label();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.label1 = new System.Windows.Forms.Label();
            this.WordsPerMIn = new System.Windows.Forms.Label();
            this.CharactersPerSec = new System.Windows.Forms.Label();
            this.resultWordsPerMin = new System.Windows.Forms.Label();
            this.resultCharPerSec = new System.Windows.Forms.Label();
            this.serchServBtn = new System.Windows.Forms.Button();
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.progressBar2 = new System.Windows.Forms.ProgressBar();
            this.progressBar3 = new System.Windows.Forms.ProgressBar();
            this.Player1 = new System.Windows.Forms.Label();
            this.Player3 = new System.Windows.Forms.Label();
            this.Player2 = new System.Windows.Forms.Label();
            this.resultLbl3 = new System.Windows.Forms.Label();
            this.resultLbl2 = new System.Windows.Forms.Label();
            this.resultLbl1 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // richTextBox1
            // 
            this.richTextBox1.BackColor = System.Drawing.SystemColors.Window;
            this.richTextBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.richTextBox1.Location = new System.Drawing.Point(12, 142);
            this.richTextBox1.Name = "richTextBox1";
            this.richTextBox1.ReadOnly = true;
            this.richTextBox1.Size = new System.Drawing.Size(792, 194);
            this.richTextBox1.TabIndex = 0;
            this.richTextBox1.Text = "";
            // 
            // textBox1
            // 
            this.textBox1.Enabled = false;
            this.textBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.textBox1.Location = new System.Drawing.Point(12, 342);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(533, 35);
            this.textBox1.TabIndex = 1;
            this.textBox1.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textBox1_KeyPress);
            // 
            // LBMIn
            // 
            this.LBMIn.AutoSize = true;
            this.LBMIn.Font = new System.Drawing.Font("Microsoft Sans Serif", 20F);
            this.LBMIn.Location = new System.Drawing.Point(682, 342);
            this.LBMIn.Name = "LBMIn";
            this.LBMIn.Size = new System.Drawing.Size(44, 31);
            this.LBMIn.TabIndex = 2;
            this.LBMIn.Text = "00";
            // 
            // LBSec
            // 
            this.LBSec.AutoSize = true;
            this.LBSec.Font = new System.Drawing.Font("Microsoft Sans Serif", 20F);
            this.LBSec.Location = new System.Drawing.Point(760, 342);
            this.LBSec.Name = "LBSec";
            this.LBSec.Size = new System.Drawing.Size(44, 31);
            this.LBSec.TabIndex = 3;
            this.LBSec.Text = "00";
            // 
            // timer1
            // 
            this.timer1.Interval = 1000;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 20F);
            this.label1.Location = new System.Drawing.Point(732, 342);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(22, 31);
            this.label1.TabIndex = 4;
            this.label1.Text = ":";
            // 
            // WordsPerMIn
            // 
            this.WordsPerMIn.AutoSize = true;
            this.WordsPerMIn.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F);
            this.WordsPerMIn.Location = new System.Drawing.Point(12, 397);
            this.WordsPerMIn.Name = "WordsPerMIn";
            this.WordsPerMIn.Size = new System.Drawing.Size(167, 26);
            this.WordsPerMIn.TabIndex = 5;
            this.WordsPerMIn.Text = "Слов в минуту:";
            // 
            // CharactersPerSec
            // 
            this.CharactersPerSec.AutoSize = true;
            this.CharactersPerSec.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F);
            this.CharactersPerSec.Location = new System.Drawing.Point(12, 436);
            this.CharactersPerSec.Name = "CharactersPerSec";
            this.CharactersPerSec.Size = new System.Drawing.Size(219, 26);
            this.CharactersPerSec.TabIndex = 6;
            this.CharactersPerSec.Text = "Символов в минуту:";
            // 
            // resultWordsPerMin
            // 
            this.resultWordsPerMin.AutoSize = true;
            this.resultWordsPerMin.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F);
            this.resultWordsPerMin.Location = new System.Drawing.Point(185, 397);
            this.resultWordsPerMin.Name = "resultWordsPerMin";
            this.resultWordsPerMin.Size = new System.Drawing.Size(0, 26);
            this.resultWordsPerMin.TabIndex = 7;
            // 
            // resultCharPerSec
            // 
            this.resultCharPerSec.AutoSize = true;
            this.resultCharPerSec.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F);
            this.resultCharPerSec.Location = new System.Drawing.Point(246, 436);
            this.resultCharPerSec.Name = "resultCharPerSec";
            this.resultCharPerSec.Size = new System.Drawing.Size(0, 26);
            this.resultCharPerSec.TabIndex = 8;
            // 
            // serchServBtn
            // 
            this.serchServBtn.Location = new System.Drawing.Point(699, 485);
            this.serchServBtn.Name = "serchServBtn";
            this.serchServBtn.Size = new System.Drawing.Size(105, 31);
            this.serchServBtn.TabIndex = 9;
            this.serchServBtn.Text = "Сервера";
            this.serchServBtn.UseVisualStyleBackColor = true;
            this.serchServBtn.Click += new System.EventHandler(this.serchServBtn_Click);
            // 
            // progressBar1
            // 
            this.progressBar1.Location = new System.Drawing.Point(98, 12);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(422, 23);
            this.progressBar1.TabIndex = 10;
            // 
            // progressBar2
            // 
            this.progressBar2.Location = new System.Drawing.Point(98, 55);
            this.progressBar2.Name = "progressBar2";
            this.progressBar2.Size = new System.Drawing.Size(422, 23);
            this.progressBar2.TabIndex = 11;
            // 
            // progressBar3
            // 
            this.progressBar3.Location = new System.Drawing.Point(98, 96);
            this.progressBar3.Name = "progressBar3";
            this.progressBar3.Size = new System.Drawing.Size(422, 23);
            this.progressBar3.TabIndex = 12;
            // 
            // Player1
            // 
            this.Player1.AutoSize = true;
            this.Player1.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F);
            this.Player1.Location = new System.Drawing.Point(14, 11);
            this.Player1.Name = "Player1";
            this.Player1.Size = new System.Drawing.Size(0, 24);
            this.Player1.TabIndex = 13;
            // 
            // Player3
            // 
            this.Player3.AutoSize = true;
            this.Player3.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F);
            this.Player3.Location = new System.Drawing.Point(14, 95);
            this.Player3.Name = "Player3";
            this.Player3.Size = new System.Drawing.Size(0, 24);
            this.Player3.TabIndex = 14;
            // 
            // Player2
            // 
            this.Player2.AutoSize = true;
            this.Player2.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F);
            this.Player2.Location = new System.Drawing.Point(14, 54);
            this.Player2.Name = "Player2";
            this.Player2.Size = new System.Drawing.Size(0, 24);
            this.Player2.TabIndex = 15;
            // 
            // resultLbl3
            // 
            this.resultLbl3.AutoSize = true;
            this.resultLbl3.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F);
            this.resultLbl3.Location = new System.Drawing.Point(526, 95);
            this.resultLbl3.Name = "resultLbl3";
            this.resultLbl3.Size = new System.Drawing.Size(0, 24);
            this.resultLbl3.TabIndex = 16;
            // 
            // resultLbl2
            // 
            this.resultLbl2.AutoSize = true;
            this.resultLbl2.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F);
            this.resultLbl2.Location = new System.Drawing.Point(526, 54);
            this.resultLbl2.Name = "resultLbl2";
            this.resultLbl2.Size = new System.Drawing.Size(0, 24);
            this.resultLbl2.TabIndex = 17;
            // 
            // resultLbl1
            // 
            this.resultLbl1.AutoSize = true;
            this.resultLbl1.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F);
            this.resultLbl1.Location = new System.Drawing.Point(526, 11);
            this.resultLbl1.Name = "resultLbl1";
            this.resultLbl1.Size = new System.Drawing.Size(0, 24);
            this.resultLbl1.TabIndex = 18;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(816, 528);
            this.Controls.Add(this.resultLbl1);
            this.Controls.Add(this.resultLbl2);
            this.Controls.Add(this.resultLbl3);
            this.Controls.Add(this.Player2);
            this.Controls.Add(this.Player3);
            this.Controls.Add(this.Player1);
            this.Controls.Add(this.progressBar3);
            this.Controls.Add(this.progressBar2);
            this.Controls.Add(this.progressBar1);
            this.Controls.Add(this.serchServBtn);
            this.Controls.Add(this.resultCharPerSec);
            this.Controls.Add(this.resultWordsPerMin);
            this.Controls.Add(this.CharactersPerSec);
            this.Controls.Add(this.WordsPerMIn);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.LBSec);
            this.Controls.Add(this.LBMIn);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.richTextBox1);
            this.Name = "Form1";
            this.Text = "Form1";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.Form1_FormClosed);
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.RichTextBox richTextBox1;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Label LBMIn;
        private System.Windows.Forms.Label LBSec;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label WordsPerMIn;
        private System.Windows.Forms.Label CharactersPerSec;
        private System.Windows.Forms.Label resultWordsPerMin;
        private System.Windows.Forms.Label resultCharPerSec;
        private System.Windows.Forms.Button serchServBtn;
        private System.Windows.Forms.ProgressBar progressBar1;
        private System.Windows.Forms.ProgressBar progressBar2;
        private System.Windows.Forms.ProgressBar progressBar3;
        private System.Windows.Forms.Label Player1;
        private System.Windows.Forms.Label Player3;
        private System.Windows.Forms.Label Player2;
        private System.Windows.Forms.Label resultLbl3;
        private System.Windows.Forms.Label resultLbl2;
        private System.Windows.Forms.Label resultLbl1;
    }
}

