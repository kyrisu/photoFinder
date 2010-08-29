namespace PhotoFinder
{
    partial class PhotoFinderForm
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
            Manina.Windows.Forms.ImageListViewColor imageListViewColor1 = new Manina.Windows.Forms.ImageListViewColor();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PhotoFinderForm));
            this.tcSearch = new System.Windows.Forms.TabControl();
            this.tpImgSearch = new System.Windows.Forms.TabPage();
            this.lbDIH1 = new System.Windows.Forms.Label();
            this.tpParmSearch = new System.Windows.Forms.TabPage();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label1 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.ilvGallery = new Manina.Windows.Forms.ImageListView();
            this.lvGallery = new System.Windows.Forms.ListView();
            this.clbDescriptorsList = new System.Windows.Forms.CheckedListBox();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.btnSearch = new System.Windows.Forms.Button();
            this.gbImgDetails = new System.Windows.Forms.GroupBox();
            this.lblDescriptorsDetails = new System.Windows.Forms.Label();
            this.tbFlickrQuery = new System.Windows.Forms.TextBox();
            this.btnSearchFlickr = new System.Windows.Forms.Button();
            this.tcSearch.SuspendLayout();
            this.tpImgSearch.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.panel1.SuspendLayout();
            this.gbImgDetails.SuspendLayout();
            this.SuspendLayout();
            // 
            // tcSearch
            // 
            this.tcSearch.Controls.Add(this.tpImgSearch);
            this.tcSearch.Controls.Add(this.tpParmSearch);
            this.tcSearch.Location = new System.Drawing.Point(12, 34);
            this.tcSearch.Name = "tcSearch";
            this.tcSearch.SelectedIndex = 0;
            this.tcSearch.Size = new System.Drawing.Size(225, 188);
            this.tcSearch.TabIndex = 0;
            // 
            // tpImgSearch
            // 
            this.tpImgSearch.AllowDrop = true;
            this.tpImgSearch.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.tpImgSearch.Controls.Add(this.lbDIH1);
            this.tpImgSearch.Location = new System.Drawing.Point(4, 22);
            this.tpImgSearch.Name = "tpImgSearch";
            this.tpImgSearch.Padding = new System.Windows.Forms.Padding(3);
            this.tpImgSearch.Size = new System.Drawing.Size(217, 162);
            this.tpImgSearch.TabIndex = 0;
            this.tpImgSearch.Text = "Image Search";
            this.tpImgSearch.UseVisualStyleBackColor = true;
            this.tpImgSearch.DragDrop += new System.Windows.Forms.DragEventHandler(this.tpImgSearch_DragDrop);
            this.tpImgSearch.DragEnter += new System.Windows.Forms.DragEventHandler(this.tpImgSearch_DragEnter);
            this.tpImgSearch.DoubleClick += new System.EventHandler(this.tpImgSearch_DoubleClick);
            // 
            // lbDIH1
            // 
            this.lbDIH1.AutoSize = true;
            this.lbDIH1.Location = new System.Drawing.Point(61, 73);
            this.lbDIH1.Name = "lbDIH1";
            this.lbDIH1.Size = new System.Drawing.Size(91, 13);
            this.lbDIH1.TabIndex = 0;
            this.lbDIH1.Text = "Drag Image Here!";
            // 
            // tpParmSearch
            // 
            this.tpParmSearch.Location = new System.Drawing.Point(4, 22);
            this.tpParmSearch.Name = "tpParmSearch";
            this.tpParmSearch.Padding = new System.Windows.Forms.Padding(3);
            this.tpParmSearch.Size = new System.Drawing.Size(217, 162);
            this.tpParmSearch.TabIndex = 1;
            this.tpParmSearch.Text = "Parametric Search";
            this.tpParmSearch.UseVisualStyleBackColor = true;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Location = new System.Drawing.Point(16, 344);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(217, 124);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(37, 60);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(136, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Drag Images Here to Save!";
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.Controls.Add(this.ilvGallery);
            this.panel1.Controls.Add(this.lvGallery);
            this.panel1.Location = new System.Drawing.Point(243, 34);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(790, 358);
            this.panel1.TabIndex = 2;
            // 
            // ilvGallery
            // 
            this.ilvGallery.AllowDrag = true;
            imageListViewColor1.BackColor = System.Drawing.SystemColors.Window;
            imageListViewColor1.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(109)))), ((int)(((byte)(109)))), ((int)(((byte)(109)))));
            imageListViewColor1.CellForeColor = System.Drawing.SystemColors.ControlText;
            imageListViewColor1.ColumnHeaderBackColor1 = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            imageListViewColor1.ColumnHeaderBackColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(196)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            imageListViewColor1.ColumnHeaderForeColor = System.Drawing.SystemColors.WindowText;
            imageListViewColor1.ColumnHeaderHoverColor1 = System.Drawing.Color.FromArgb(((int)(((byte)(16)))), ((int)(((byte)(51)))), ((int)(((byte)(153)))), ((int)(((byte)(255)))));
            imageListViewColor1.ColumnHeaderHoverColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(51)))), ((int)(((byte)(153)))), ((int)(((byte)(255)))));
            imageListViewColor1.ColumnSelectColor = System.Drawing.Color.FromArgb(((int)(((byte)(16)))), ((int)(((byte)(109)))), ((int)(((byte)(109)))), ((int)(((byte)(109)))));
            imageListViewColor1.ColumnSeparatorColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(109)))), ((int)(((byte)(109)))), ((int)(((byte)(109)))));
            imageListViewColor1.ControlBackColor = System.Drawing.SystemColors.Window;
            imageListViewColor1.ForeColor = System.Drawing.SystemColors.ControlText;
            imageListViewColor1.HoverBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(51)))), ((int)(((byte)(153)))), ((int)(((byte)(255)))));
            imageListViewColor1.HoverColor1 = System.Drawing.Color.FromArgb(((int)(((byte)(8)))), ((int)(((byte)(51)))), ((int)(((byte)(153)))), ((int)(((byte)(255)))));
            imageListViewColor1.HoverColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(51)))), ((int)(((byte)(153)))), ((int)(((byte)(255)))));
            imageListViewColor1.ImageInnerBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            imageListViewColor1.ImageOuterBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            imageListViewColor1.InsertionCaretColor = System.Drawing.SystemColors.Highlight;
            imageListViewColor1.PaneBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(16)))), ((int)(((byte)(109)))), ((int)(((byte)(109)))), ((int)(((byte)(109)))));
            imageListViewColor1.PaneLabelColor = System.Drawing.SystemColors.GrayText;
            imageListViewColor1.PaneSeparatorColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(109)))), ((int)(((byte)(109)))), ((int)(((byte)(109)))));
            imageListViewColor1.SelectedBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(51)))), ((int)(((byte)(153)))), ((int)(((byte)(255)))));
            imageListViewColor1.SelectedColor1 = System.Drawing.Color.FromArgb(((int)(((byte)(16)))), ((int)(((byte)(51)))), ((int)(((byte)(153)))), ((int)(((byte)(255)))));
            imageListViewColor1.SelectedColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(51)))), ((int)(((byte)(153)))), ((int)(((byte)(255)))));
            imageListViewColor1.SelectionRectangleBorderColor = System.Drawing.SystemColors.Highlight;
            imageListViewColor1.SelectionRectangleColor1 = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(51)))), ((int)(((byte)(153)))), ((int)(((byte)(255)))));
            imageListViewColor1.SelectionRectangleColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(51)))), ((int)(((byte)(153)))), ((int)(((byte)(255)))));
            imageListViewColor1.UnFocusedBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(109)))), ((int)(((byte)(109)))), ((int)(((byte)(109)))));
            imageListViewColor1.UnFocusedColor1 = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(109)))), ((int)(((byte)(109)))), ((int)(((byte)(109)))));
            imageListViewColor1.UnFocusedColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(109)))), ((int)(((byte)(109)))), ((int)(((byte)(109)))));
            this.ilvGallery.Colors = imageListViewColor1;
            this.ilvGallery.DefaultImage = ((System.Drawing.Image)(resources.GetObject("ilvGallery.DefaultImage")));
            this.ilvGallery.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ilvGallery.ErrorImage = ((System.Drawing.Image)(resources.GetObject("ilvGallery.ErrorImage")));
            this.ilvGallery.HeaderFont = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.ilvGallery.Location = new System.Drawing.Point(0, 0);
            this.ilvGallery.Name = "ilvGallery";
            this.ilvGallery.Size = new System.Drawing.Size(790, 358);
            this.ilvGallery.TabIndex = 1;
            this.ilvGallery.Text = "";
            this.ilvGallery.ItemClick += new Manina.Windows.Forms.ItemClickEventHandler(this.ilvGallery_ItemClick);
            // 
            // lvGallery
            // 
            this.lvGallery.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lvGallery.Location = new System.Drawing.Point(0, 0);
            this.lvGallery.Name = "lvGallery";
            this.lvGallery.Size = new System.Drawing.Size(790, 358);
            this.lvGallery.TabIndex = 0;
            this.lvGallery.UseCompatibleStateImageBehavior = false;
            // 
            // clbDescriptorsList
            // 
            this.clbDescriptorsList.BackColor = System.Drawing.Color.WhiteSmoke;
            this.clbDescriptorsList.CheckOnClick = true;
            this.clbDescriptorsList.FormattingEnabled = true;
            this.clbDescriptorsList.Items.AddRange(new object[] {
            "(SCD) Scalable Color",
            "(CLD) Color Layout",
            "(DCD) Dominant Color",
            "(EHD) Edge Histogram",
            "(CEDD) Color and Edge Directivity Desc.",
            "(FCTH) Fuzzy Color and Texture Hist."});
            this.clbDescriptorsList.Location = new System.Drawing.Point(12, 228);
            this.clbDescriptorsList.Name = "clbDescriptorsList";
            this.clbDescriptorsList.Size = new System.Drawing.Size(225, 94);
            this.clbDescriptorsList.TabIndex = 3;
            // 
            // menuStrip1
            // 
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(1045, 24);
            this.menuStrip1.TabIndex = 4;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // btnSearch
            // 
            this.btnSearch.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSearch.Location = new System.Drawing.Point(958, 404);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(75, 23);
            this.btnSearch.TabIndex = 5;
            this.btnSearch.Text = "Search!";
            this.btnSearch.UseVisualStyleBackColor = true;
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
            // 
            // gbImgDetails
            // 
            this.gbImgDetails.Controls.Add(this.lblDescriptorsDetails);
            this.gbImgDetails.Location = new System.Drawing.Point(243, 398);
            this.gbImgDetails.Name = "gbImgDetails";
            this.gbImgDetails.Size = new System.Drawing.Size(709, 79);
            this.gbImgDetails.TabIndex = 6;
            this.gbImgDetails.TabStop = false;
            this.gbImgDetails.Text = "Image Details";
            // 
            // lblDescriptorsDetails
            // 
            this.lblDescriptorsDetails.AutoSize = true;
            this.lblDescriptorsDetails.Location = new System.Drawing.Point(7, 20);
            this.lblDescriptorsDetails.Name = "lblDescriptorsDetails";
            this.lblDescriptorsDetails.Size = new System.Drawing.Size(0, 13);
            this.lblDescriptorsDetails.TabIndex = 0;
            // 
            // tbFlickrQuery
            // 
            this.tbFlickrQuery.Location = new System.Drawing.Point(243, 4);
            this.tbFlickrQuery.Name = "tbFlickrQuery";
            this.tbFlickrQuery.Size = new System.Drawing.Size(242, 20);
            this.tbFlickrQuery.TabIndex = 7;
            this.tbFlickrQuery.KeyUp += new System.Windows.Forms.KeyEventHandler(this.tbFlickrQuery_KeyUp);
            // 
            // btnSearchFlickr
            // 
            this.btnSearchFlickr.Location = new System.Drawing.Point(491, 1);
            this.btnSearchFlickr.Name = "btnSearchFlickr";
            this.btnSearchFlickr.Size = new System.Drawing.Size(75, 23);
            this.btnSearchFlickr.TabIndex = 8;
            this.btnSearchFlickr.Text = "Index Flickr";
            this.btnSearchFlickr.UseVisualStyleBackColor = true;
            this.btnSearchFlickr.Click += new System.EventHandler(this.btnSearchFlickr_Click);
            // 
            // PhotoFinderForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1045, 480);
            this.Controls.Add(this.btnSearchFlickr);
            this.Controls.Add(this.tbFlickrQuery);
            this.Controls.Add(this.gbImgDetails);
            this.Controls.Add(this.btnSearch);
            this.Controls.Add(this.clbDescriptorsList);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.tcSearch);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "PhotoFinderForm";
            this.Text = "PhotoFinder";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.PhotoFinderForm_FormClosing);
            this.tcSearch.ResumeLayout(false);
            this.tpImgSearch.ResumeLayout(false);
            this.tpImgSearch.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.gbImgDetails.ResumeLayout(false);
            this.gbImgDetails.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TabControl tcSearch;
        private System.Windows.Forms.TabPage tpImgSearch;
        private System.Windows.Forms.TabPage tpParmSearch;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label lbDIH1;
        private System.Windows.Forms.ListView lvGallery;
        private System.Windows.Forms.CheckedListBox clbDescriptorsList;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.Button btnSearch;
        private Manina.Windows.Forms.ImageListView ilvGallery;
        private System.Windows.Forms.GroupBox gbImgDetails;
        private System.Windows.Forms.Label lblDescriptorsDetails;
        private System.Windows.Forms.TextBox tbFlickrQuery;
        private System.Windows.Forms.Button btnSearchFlickr;
    }
}

