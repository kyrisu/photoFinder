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
            Manina.Windows.Forms.ImageListViewColor imageListViewColor2 = new Manina.Windows.Forms.ImageListViewColor();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PhotoFinderForm));
            this.tcSearch = new System.Windows.Forms.TabControl();
            this.tpImgSearch = new System.Windows.Forms.TabPage();
            this.lbDIH1 = new System.Windows.Forms.Label();
            this.tpSaveBox = new System.Windows.Forms.GroupBox();
            this.label1 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.ilvGallery = new Manina.Windows.Forms.ImageListView();
            this.lvGallery = new System.Windows.Forms.ListView();
            this.clbDescriptorsList = new System.Windows.Forms.CheckedListBox();
            this.btnSearch = new System.Windows.Forms.Button();
            this.gbImgDetails = new System.Windows.Forms.GroupBox();
            this.lblDescriptorsDetails = new System.Windows.Forms.Label();
            this.tbFlickrQuery = new System.Windows.Forms.TextBox();
            this.btnSearchFlickr = new System.Windows.Forms.Button();
            this.cbxSort = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.pSort = new System.Windows.Forms.Panel();
            this.rbDesc = new System.Windows.Forms.RadioButton();
            this.rbAsc = new System.Windows.Forms.RadioButton();
            this.btnFolder = new System.Windows.Forms.Button();
            this.btnFlickr = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.tcSearch.SuspendLayout();
            this.tpImgSearch.SuspendLayout();
            this.tpSaveBox.SuspendLayout();
            this.panel1.SuspendLayout();
            this.gbImgDetails.SuspendLayout();
            this.pSort.SuspendLayout();
            this.SuspendLayout();
            // 
            // tcSearch
            // 
            this.tcSearch.Controls.Add(this.tpImgSearch);
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
            this.tpImgSearch.Text = "Image Query";
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
            // tpSaveBox
            // 
            this.tpSaveBox.Controls.Add(this.label1);
            this.tpSaveBox.Location = new System.Drawing.Point(16, 353);
            this.tpSaveBox.Name = "tpSaveBox";
            this.tpSaveBox.Size = new System.Drawing.Size(217, 124);
            this.tpSaveBox.TabIndex = 1;
            this.tpSaveBox.TabStop = false;
            this.tpSaveBox.DragDrop += new System.Windows.Forms.DragEventHandler(this.tpSaveBox_DragDrop);
            this.tpSaveBox.DragEnter += new System.Windows.Forms.DragEventHandler(this.tpSaveBox_DragEnter);
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
            imageListViewColor2.BackColor = System.Drawing.SystemColors.Window;
            imageListViewColor2.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(109)))), ((int)(((byte)(109)))), ((int)(((byte)(109)))));
            imageListViewColor2.CellForeColor = System.Drawing.SystemColors.ControlText;
            imageListViewColor2.ColumnHeaderBackColor1 = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            imageListViewColor2.ColumnHeaderBackColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(196)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            imageListViewColor2.ColumnHeaderForeColor = System.Drawing.SystemColors.WindowText;
            imageListViewColor2.ColumnHeaderHoverColor1 = System.Drawing.Color.FromArgb(((int)(((byte)(16)))), ((int)(((byte)(51)))), ((int)(((byte)(153)))), ((int)(((byte)(255)))));
            imageListViewColor2.ColumnHeaderHoverColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(51)))), ((int)(((byte)(153)))), ((int)(((byte)(255)))));
            imageListViewColor2.ColumnSelectColor = System.Drawing.Color.FromArgb(((int)(((byte)(16)))), ((int)(((byte)(109)))), ((int)(((byte)(109)))), ((int)(((byte)(109)))));
            imageListViewColor2.ColumnSeparatorColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(109)))), ((int)(((byte)(109)))), ((int)(((byte)(109)))));
            imageListViewColor2.ControlBackColor = System.Drawing.SystemColors.Window;
            imageListViewColor2.ForeColor = System.Drawing.SystemColors.ControlText;
            imageListViewColor2.HoverBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(51)))), ((int)(((byte)(153)))), ((int)(((byte)(255)))));
            imageListViewColor2.HoverColor1 = System.Drawing.Color.FromArgb(((int)(((byte)(8)))), ((int)(((byte)(51)))), ((int)(((byte)(153)))), ((int)(((byte)(255)))));
            imageListViewColor2.HoverColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(51)))), ((int)(((byte)(153)))), ((int)(((byte)(255)))));
            imageListViewColor2.ImageInnerBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            imageListViewColor2.ImageOuterBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            imageListViewColor2.InsertionCaretColor = System.Drawing.SystemColors.Highlight;
            imageListViewColor2.PaneBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(16)))), ((int)(((byte)(109)))), ((int)(((byte)(109)))), ((int)(((byte)(109)))));
            imageListViewColor2.PaneLabelColor = System.Drawing.SystemColors.GrayText;
            imageListViewColor2.PaneSeparatorColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(109)))), ((int)(((byte)(109)))), ((int)(((byte)(109)))));
            imageListViewColor2.SelectedBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(51)))), ((int)(((byte)(153)))), ((int)(((byte)(255)))));
            imageListViewColor2.SelectedColor1 = System.Drawing.Color.FromArgb(((int)(((byte)(16)))), ((int)(((byte)(51)))), ((int)(((byte)(153)))), ((int)(((byte)(255)))));
            imageListViewColor2.SelectedColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(51)))), ((int)(((byte)(153)))), ((int)(((byte)(255)))));
            imageListViewColor2.SelectionRectangleBorderColor = System.Drawing.SystemColors.Highlight;
            imageListViewColor2.SelectionRectangleColor1 = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(51)))), ((int)(((byte)(153)))), ((int)(((byte)(255)))));
            imageListViewColor2.SelectionRectangleColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(51)))), ((int)(((byte)(153)))), ((int)(((byte)(255)))));
            imageListViewColor2.UnFocusedBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(109)))), ((int)(((byte)(109)))), ((int)(((byte)(109)))));
            imageListViewColor2.UnFocusedColor1 = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(109)))), ((int)(((byte)(109)))), ((int)(((byte)(109)))));
            imageListViewColor2.UnFocusedColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(109)))), ((int)(((byte)(109)))), ((int)(((byte)(109)))));
            this.ilvGallery.Colors = imageListViewColor2;
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
            "(EHD) Edge Histogram",
            "(CEDD) Color and Edge Directivity Desc.",
            "(FCTH) Fuzzy Color and Texture Hist."});
            this.clbDescriptorsList.Location = new System.Drawing.Point(12, 228);
            this.clbDescriptorsList.Name = "clbDescriptorsList";
            this.clbDescriptorsList.Size = new System.Drawing.Size(225, 94);
            this.clbDescriptorsList.TabIndex = 3;
            this.clbDescriptorsList.SelectedIndexChanged += new System.EventHandler(this.clbDescriptorsList_SelectedIndexChanged);
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
            this.gbImgDetails.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
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
            this.tbFlickrQuery.Visible = false;
            this.tbFlickrQuery.KeyUp += new System.Windows.Forms.KeyEventHandler(this.tbFlickrQuery_KeyUp);
            // 
            // btnSearchFlickr
            // 
            this.btnSearchFlickr.Location = new System.Drawing.Point(491, 1);
            this.btnSearchFlickr.Name = "btnSearchFlickr";
            this.btnSearchFlickr.Size = new System.Drawing.Size(101, 23);
            this.btnSearchFlickr.TabIndex = 8;
            this.btnSearchFlickr.Text = "Index Flickr";
            this.btnSearchFlickr.UseVisualStyleBackColor = true;
            this.btnSearchFlickr.Visible = false;
            this.btnSearchFlickr.Click += new System.EventHandler(this.btnSearchFlickr_Click);
            // 
            // cbxSort
            // 
            this.cbxSort.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.cbxSort.FormattingEnabled = true;
            this.cbxSort.Location = new System.Drawing.Point(817, 3);
            this.cbxSort.Name = "cbxSort";
            this.cbxSort.Size = new System.Drawing.Size(99, 21);
            this.cbxSort.Sorted = true;
            this.cbxSort.TabIndex = 9;
            this.cbxSort.SelectedIndexChanged += new System.EventHandler(this.cbxSort_SelectedIndexChanged);
            // 
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(767, 6);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(44, 13);
            this.label2.TabIndex = 10;
            this.label2.Text = "Sort By:";
            // 
            // pSort
            // 
            this.pSort.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.pSort.Controls.Add(this.rbDesc);
            this.pSort.Controls.Add(this.rbAsc);
            this.pSort.Location = new System.Drawing.Point(922, 4);
            this.pSort.Name = "pSort";
            this.pSort.Size = new System.Drawing.Size(111, 20);
            this.pSort.TabIndex = 11;
            // 
            // rbDesc
            // 
            this.rbDesc.AutoSize = true;
            this.rbDesc.Location = new System.Drawing.Point(54, 1);
            this.rbDesc.Name = "rbDesc";
            this.rbDesc.Size = new System.Drawing.Size(50, 17);
            this.rbDesc.TabIndex = 1;
            this.rbDesc.TabStop = true;
            this.rbDesc.Text = "Desc";
            this.rbDesc.UseVisualStyleBackColor = true;
            // 
            // rbAsc
            // 
            this.rbAsc.AutoSize = true;
            this.rbAsc.Location = new System.Drawing.Point(4, 1);
            this.rbAsc.Name = "rbAsc";
            this.rbAsc.Size = new System.Drawing.Size(43, 17);
            this.rbAsc.TabIndex = 0;
            this.rbAsc.TabStop = true;
            this.rbAsc.Text = "Asc";
            this.rbAsc.UseVisualStyleBackColor = true;
            this.rbAsc.CheckedChanged += new System.EventHandler(this.rbAsc_CheckedChanged);
            // 
            // btnFolder
            // 
            this.btnFolder.Location = new System.Drawing.Point(76, 1);
            this.btnFolder.Name = "btnFolder";
            this.btnFolder.Size = new System.Drawing.Size(75, 23);
            this.btnFolder.TabIndex = 12;
            this.btnFolder.Text = "Folder";
            this.btnFolder.UseVisualStyleBackColor = true;
            this.btnFolder.Click += new System.EventHandler(this.btnFolder_Click);
            // 
            // btnFlickr
            // 
            this.btnFlickr.Location = new System.Drawing.Point(157, 1);
            this.btnFlickr.Name = "btnFlickr";
            this.btnFlickr.Size = new System.Drawing.Size(75, 23);
            this.btnFlickr.TabIndex = 13;
            this.btnFlickr.Text = "Flickr";
            this.btnFlickr.UseVisualStyleBackColor = true;
            this.btnFlickr.Click += new System.EventHandler(this.btnFlickr_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(13, 6);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(58, 13);
            this.label3.TabIndex = 14;
            this.label3.Text = "Search in: ";
            // 
            // PhotoFinderForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1045, 480);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.btnFlickr);
            this.Controls.Add(this.btnFolder);
            this.Controls.Add(this.pSort);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.cbxSort);
            this.Controls.Add(this.btnSearchFlickr);
            this.Controls.Add(this.tbFlickrQuery);
            this.Controls.Add(this.gbImgDetails);
            this.Controls.Add(this.btnSearch);
            this.Controls.Add(this.clbDescriptorsList);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.tpSaveBox);
            this.Controls.Add(this.tcSearch);
            this.Name = "PhotoFinderForm";
            this.Text = "PhotoFinder";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.PhotoFinderForm_FormClosing);
            this.Load += new System.EventHandler(this.PhotoFinderForm_Load);
            this.tcSearch.ResumeLayout(false);
            this.tpImgSearch.ResumeLayout(false);
            this.tpImgSearch.PerformLayout();
            this.tpSaveBox.ResumeLayout(false);
            this.tpSaveBox.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.gbImgDetails.ResumeLayout(false);
            this.gbImgDetails.PerformLayout();
            this.pSort.ResumeLayout(false);
            this.pSort.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TabControl tcSearch;
        private System.Windows.Forms.TabPage tpImgSearch;
        private System.Windows.Forms.GroupBox tpSaveBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label lbDIH1;
        private System.Windows.Forms.ListView lvGallery;
        private System.Windows.Forms.CheckedListBox clbDescriptorsList;
        private System.Windows.Forms.Button btnSearch;
        private Manina.Windows.Forms.ImageListView ilvGallery;
        private System.Windows.Forms.GroupBox gbImgDetails;
        private System.Windows.Forms.Label lblDescriptorsDetails;
        private System.Windows.Forms.TextBox tbFlickrQuery;
        private System.Windows.Forms.Button btnSearchFlickr;
        private System.Windows.Forms.ComboBox cbxSort;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Panel pSort;
        private System.Windows.Forms.RadioButton rbDesc;
        private System.Windows.Forms.RadioButton rbAsc;
        private System.Windows.Forms.Button btnFolder;
        private System.Windows.Forms.Button btnFlickr;
        private System.Windows.Forms.Label label3;
    }
}

