namespace ObjetosTridimencionais
{
    partial class frmMain
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
            this.pbCanvas = new System.Windows.Forms.PictureBox();
            this.ofdAbrir = new System.Windows.Forms.OpenFileDialog();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.arquivoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.abrirToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.perspectivasToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.paralelaToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.perspectivaToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ortoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.oblícuaToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.pontoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.eixoZToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.eixoYToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.eixoXToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.cabinetToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.cavaleiraToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            ((System.ComponentModel.ISupportInitialize)(this.pbCanvas)).BeginInit();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // pbCanvas
            // 
            this.pbCanvas.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(40)))), ((int)(((byte)(45)))));
            this.pbCanvas.Dock = System.Windows.Forms.DockStyle.Left;
            this.pbCanvas.Location = new System.Drawing.Point(0, 24);
            this.pbCanvas.Name = "pbCanvas";
            this.pbCanvas.Size = new System.Drawing.Size(598, 725);
            this.pbCanvas.TabIndex = 0;
            this.pbCanvas.TabStop = false;
            this.pbCanvas.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.pbCanvas_MouseDoubleClick);
            this.pbCanvas.MouseDown += new System.Windows.Forms.MouseEventHandler(this.pbCanvas_MouseDown);
            this.pbCanvas.MouseMove += new System.Windows.Forms.MouseEventHandler(this.pbCanvas_MouseMove);
            this.pbCanvas.MouseUp += new System.Windows.Forms.MouseEventHandler(this.pbCanvas_MouseUp);
            // 
            // ofdAbrir
            // 
            this.ofdAbrir.FileName = "openFileDialog1";
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.arquivoToolStripMenuItem,
            this.perspectivasToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(1370, 24);
            this.menuStrip1.TabIndex = 1;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // arquivoToolStripMenuItem
            // 
            this.arquivoToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.abrirToolStripMenuItem});
            this.arquivoToolStripMenuItem.Name = "arquivoToolStripMenuItem";
            this.arquivoToolStripMenuItem.Size = new System.Drawing.Size(61, 20);
            this.arquivoToolStripMenuItem.Text = "Arquivo";
            // 
            // abrirToolStripMenuItem
            // 
            this.abrirToolStripMenuItem.Name = "abrirToolStripMenuItem";
            this.abrirToolStripMenuItem.Size = new System.Drawing.Size(100, 22);
            this.abrirToolStripMenuItem.Text = "Abrir";
            this.abrirToolStripMenuItem.Click += new System.EventHandler(this.AbrirToolStripMenuItem_Click);
            // 
            // perspectivasToolStripMenuItem
            // 
            this.perspectivasToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.paralelaToolStripMenuItem,
            this.perspectivaToolStripMenuItem});
            this.perspectivasToolStripMenuItem.Name = "perspectivasToolStripMenuItem";
            this.perspectivasToolStripMenuItem.Size = new System.Drawing.Size(70, 20);
            this.perspectivasToolStripMenuItem.Text = "Projeções";
            // 
            // paralelaToolStripMenuItem
            // 
            this.paralelaToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ortoToolStripMenuItem,
            this.oblícuaToolStripMenuItem});
            this.paralelaToolStripMenuItem.Name = "paralelaToolStripMenuItem";
            this.paralelaToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.paralelaToolStripMenuItem.Text = "Paralela";
            // 
            // perspectivaToolStripMenuItem
            // 
            this.perspectivaToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.pontoToolStripMenuItem});
            this.perspectivaToolStripMenuItem.Name = "perspectivaToolStripMenuItem";
            this.perspectivaToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.perspectivaToolStripMenuItem.Text = "Perspectiva";
            // 
            // ortoToolStripMenuItem
            // 
            this.ortoToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.eixoZToolStripMenuItem,
            this.eixoYToolStripMenuItem,
            this.eixoXToolStripMenuItem});
            this.ortoToolStripMenuItem.Name = "ortoToolStripMenuItem";
            this.ortoToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.ortoToolStripMenuItem.Text = "Ortográfica";
            // 
            // oblícuaToolStripMenuItem
            // 
            this.oblícuaToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.cabinetToolStripMenuItem,
            this.cavaleiraToolStripMenuItem});
            this.oblícuaToolStripMenuItem.Name = "oblícuaToolStripMenuItem";
            this.oblícuaToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.oblícuaToolStripMenuItem.Text = "Oblíqua";
            // 
            // pontoToolStripMenuItem
            // 
            this.pontoToolStripMenuItem.Name = "pontoToolStripMenuItem";
            this.pontoToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.pontoToolStripMenuItem.Text = "1 Ponto";
            // 
            // eixoZToolStripMenuItem
            // 
            this.eixoZToolStripMenuItem.Name = "eixoZToolStripMenuItem";
            this.eixoZToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.eixoZToolStripMenuItem.Text = "Eixo Z";
            this.eixoZToolStripMenuItem.Click += new System.EventHandler(this.eixoZToolStripMenuItem_Click);
            // 
            // eixoYToolStripMenuItem
            // 
            this.eixoYToolStripMenuItem.Name = "eixoYToolStripMenuItem";
            this.eixoYToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.eixoYToolStripMenuItem.Text = "Eixo Y";
            this.eixoYToolStripMenuItem.Click += new System.EventHandler(this.eixoYToolStripMenuItem_Click);
            // 
            // eixoXToolStripMenuItem
            // 
            this.eixoXToolStripMenuItem.Name = "eixoXToolStripMenuItem";
            this.eixoXToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.eixoXToolStripMenuItem.Text = "Eixo X";
            this.eixoXToolStripMenuItem.Click += new System.EventHandler(this.eixoXToolStripMenuItem_Click);
            // 
            // cabinetToolStripMenuItem
            // 
            this.cabinetToolStripMenuItem.Name = "cabinetToolStripMenuItem";
            this.cabinetToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.cabinetToolStripMenuItem.Text = "Cabinet";
            this.cabinetToolStripMenuItem.Click += new System.EventHandler(this.cabinetToolStripMenuItem_Click);
            // 
            // cavaleiraToolStripMenuItem
            // 
            this.cavaleiraToolStripMenuItem.Name = "cavaleiraToolStripMenuItem";
            this.cavaleiraToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.cavaleiraToolStripMenuItem.Text = "Cavaleira";
            this.cavaleiraToolStripMenuItem.Click += new System.EventHandler(this.cavaleiraToolStripMenuItem_Click);
            // 
            // frmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(30)))), ((int)(((byte)(35)))));
            this.ClientSize = new System.Drawing.Size(1370, 749);
            this.Controls.Add(this.pbCanvas);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "frmMain";
            this.Text = "Objetos Tridimensionais";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.frmMain_KeyDown);
            this.KeyUp += new System.Windows.Forms.KeyEventHandler(this.frmMain_KeyUp);
            ((System.ComponentModel.ISupportInitialize)(this.pbCanvas)).EndInit();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox pbCanvas;
        private System.Windows.Forms.OpenFileDialog ofdAbrir;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem arquivoToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem abrirToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem perspectivasToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem paralelaToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem ortoToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem eixoZToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem eixoYToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem eixoXToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem oblícuaToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem cabinetToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem cavaleiraToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem perspectivaToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem pontoToolStripMenuItem;
    }
}

