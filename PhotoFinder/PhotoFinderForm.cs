using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using Gallery = Manina.Windows.Forms;
using FlickrNet;
using System.Net;
using System.Text.RegularExpressions;
using System.Threading;
using Manina.Windows.Forms;
using System.Runtime.InteropServices;


namespace PhotoFinder
{
    public partial class PhotoFinderForm : Form
    {
        /// <summary>
        /// Bitmap object to be later passed to descriptor calculating methods.
        /// </summary>
        private Bitmap _QueryBitmap = null;
        private List<string> _PhotoIdList = new List<string>();
        private Flickr _Flickr;
        private string _FolderPath;
        bool _IsIndexing = false;
        bool _IsSearching = false;
        bool _RefreshGallery = false;
        bool _FolderSearch = false;
        string _SourceFolderPath;

        public PhotoFinderForm()
        {
            InitializeComponent();
            _Flickr = new Flickr("135794b7b378a7ecbd10186748d28fb0");
        }

        private delegate void PopulateGalleryCallback(Manina.Windows.Forms.ImageListViewItem ilvi);

        [DllImport("wininet.dll")]
        private extern static bool InternetGetConnectedState(out int Description, int ReservedValue);

        private void PopulateGallery(string path, FlickrNet.Photo photo)
        {
            try
            {
                FileInfo file = new FileInfo(path);
                if (file.IsImage())
                {
                    Gallery.ImageListViewItem ilvi = new Gallery.ImageListViewItem();
                    ilvi.FileName = file.FullName;
                    ilvi.Tag = new ImageInfo
                    {
                        PhotoID = photo.PhotoId
                    };
                    if (ilvGallery.InvokeRequired)
                    {
                        PopulateGalleryCallback d = new PopulateGalleryCallback(PopulateGallery);
                        this.Invoke(d, new object[] { ilvi });
                    }
                    else
                    {
                        ilvGallery.Items.Add(ilvi);
                    }
                }
            }
            catch (UnauthorizedAccessException)
            {
                // so what ... go go go!!! :P
            }
        }

        private void PopulateGallery(ImageListViewItem ilvi)
        {
            ilvGallery.Items.Add(ilvi);
        }

        private void tpImgSearch_DoubleClick(object sender, EventArgs e)
        {
            OpenFileDialog ofDialog = new OpenFileDialog();
            if (ofDialog.ShowDialog() == DialogResult.OK && !String.IsNullOrEmpty(ofDialog.FileName))
            {
                try
                {
                    _QueryBitmap = (Bitmap)Image.FromFile(ofDialog.FileName);
                }
                catch (System.IO.FileNotFoundException)
                {
                    MessageBox.Show("File was not found!!!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

                tpImgSearch.BackgroundImage = _QueryBitmap;
                //hide DragImageHere label
                lbDIH1.Visible = false;
            }
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            if (_IsSearching == true)
            {
                _IsSearching = false;
                btnSearch.Text = "Search";
                return;
            }
            
            if (_QueryBitmap == null)
            {
                MessageBox.Show("No query defined!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            _IsSearching = true;
            _RefreshGallery = true;
            btnSearch.Text = "Searching...";
            //check which descriptors are checked
            Descriptor desc = Descriptor.NONE;
            desc |= (clbDescriptorsList.GetItemChecked(0) ? Descriptor.SCD : Descriptor.NONE);
            desc |= (clbDescriptorsList.GetItemChecked(1) ? Descriptor.CLD : Descriptor.NONE);
            desc |= (clbDescriptorsList.GetItemChecked(2) ? Descriptor.EHD : Descriptor.NONE);
            desc |= (clbDescriptorsList.GetItemChecked(3) ? Descriptor.CEDD : Descriptor.NONE);
            desc |= (clbDescriptorsList.GetItemChecked(4) ? Descriptor.FCTH : Descriptor.NONE);
            //desc |= (clbDescriptorsList.GetItemChecked(2) ? Descriptor.DCD : Descriptor.NONE);

            ilvGallery.Items.Clear();
            if (desc == Descriptor.NONE)
                MessageBox.Show("You need to choose the descriptor first!", "Warning!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            else if (_FolderSearch)
                ThreadPool.QueueUserWorkItem(SearchInFolder, desc);
            else
                SearchInDatabase(GetQuery(_QueryBitmap, desc), desc);
            btnSearch.Text = "Search";
            _IsSearching = false;

            // if we have choosen sorting order before, lets sort the results again
            if (cbxSort.SelectedItem != null)
                cbxSort_SelectedIndexChanged(null, null);
        }

        /// <summary>
        /// Calculates selected descriptors for query bitmap.
        /// </summary>
        /// <param name="_QueryBitmap"></param>
        /// <param name="desc"></param>
        /// <returns></returns>
        private Dictionary<Descriptor, object> GetQuery(Bitmap _QueryBitmap, Descriptor desc)
        {
            Dictionary<Descriptor, object> result = new Dictionary<Descriptor, object>();
            foreach (Descriptor descriptor in Enum.GetValues(typeof(Descriptor)))
            {
                if (descriptor != Descriptor.NONE && (desc & descriptor) == descriptor)
                {
                    result.Add(descriptor, DescriptorTools.CalculateDescriptor(_QueryBitmap, descriptor));
                }
            }

            return result;
        }

        private void SearchInDatabase(Dictionary<Descriptor, object> query, Descriptor desc)
        {
            if (query != null)
            {

                try
                {
                    PhotosEntities photoEntities = new PhotosEntities();

                    foreach (var photoId in _PhotoIdList)
                    {
                        bool comply = false;
                        Photo photo = photoEntities.PhotoSet.Single(p => p.PhotoID == photoId);
                        Gallery.ImageListViewItem ilvi = new Gallery.ImageListViewItem(photo.ImagePath);
                        //we need to clear the filename form the text property, and set it to picture title
                        ilvi.Text = "Photo title: " + photo.Title + Environment.NewLine;
                        ilvi.Tag = new ImageInfo
                        {
                            PhotoID = photo.PhotoID
                        };
                        foreach (Descriptor descriptor in Enum.GetValues(typeof(Descriptor)))
                        {
                            if (descriptor != Descriptor.NONE && (desc & descriptor) == descriptor)
                            {
                                double distance = DescriptorTools.CalculateDescriptorDistance(query[descriptor], photo.GetByIndex(descriptor.ToString()).BDeserialize(), descriptor);
                                //TODO: distance needs to be set per descriptor
                                if (distance >= 0)
                                {
                                    comply = true;
                                    //string tmpFileName = TempManager.GetTempFileName();
                                    string tmpFileName = Path.Combine(_FolderPath, photo.PhotoID + ".jpg");
                                    ((ImageInfo)ilvi.Tag).SetDescriptorDistance(descriptor, distance);
                                    GalleryEntryBuilder(new FileInfo(tmpFileName), ref ilvi, descriptor.ToString() + ": " + distance.ToString("F"));
                                }
                            }
                        }

                        // after all descriptors touched the file it goes to the gallery if it complies with the query parameters
                        if (comply)
                        {
                            ilvGallery.Items.Add(ilvi);
                        }
                    }
                }
                catch (UnauthorizedAccessException)
                {
                    // again.. so what.. go go go!
                }
            }
        }

        /// <summary>
        /// Searches in _SourceFolderPath directory
        /// </summary>
        /// <param name="_SourceFolderPath"></param>
        /// <param name="query"></param>
        private void SearchInFolder(object odesc)
        {
            Descriptor desc = (Descriptor)odesc;
            var query = GetQuery(_QueryBitmap, desc);
            if (!String.IsNullOrEmpty(_SourceFolderPath) && query != null)
            {
                try
                {
                    foreach (var file in (new DirectoryInfo(_SourceFolderPath)).GetFiles())
                    {
                        bool comply = false;
                        Gallery.ImageListViewItem ilvi = new Gallery.ImageListViewItem(file.FullName);
                        //we need to clear the filename form the text property, and set it to picture title
                        ilvi.Text = "Photo title: " + file.Name + Environment.NewLine;
                        ilvi.Tag = new ImageInfo
                        {
                            PhotoID = file.FullName
                        };
                        if (file.IsImage())
                        {

                            foreach (Descriptor descriptor in Enum.GetValues(typeof(Descriptor)))
                            {
                                if (descriptor != Descriptor.NONE && (desc & descriptor) == descriptor)
                                {
                                    double distance = DescriptorTools.CalculateDescriptorDistance(query[descriptor], DescriptorTools.CalculateDescriptor(file, descriptor), descriptor);
                                    //TODO: distance needs to be set per descriptor
                                    if (distance >= 0)
                                    {
                                        comply = true;
                                        //string tmpFileName = TempManager.GetTempFileName();
                                        string tmpFileName = file.FullName;
                                        ((ImageInfo)ilvi.Tag).SetDescriptorDistance(descriptor, distance);
                                        GalleryEntryBuilder(new FileInfo(tmpFileName), ref ilvi, descriptor.ToString() + ": " + distance.ToString("F"));
                                    }
                                }
                            }

                            // after all descriptors touched the file it goes to the gallery if it complies with the query parameters
                            if (comply)
                            {
                                if (ilvGallery.InvokeRequired)
                                {
                                    PopulateGalleryCallback d = new PopulateGalleryCallback(PopulateGallery);
                                    this.Invoke(d, new object[] { ilvi });
                                }
                                else
                                {
                                    ilvGallery.Items.Add(ilvi);
                                }
                            }
                        }
                    }
                }
                catch (UnauthorizedAccessException)
                {
                    // again.. so what.. go go go!
                }
            }

        }

        /// <summary>
        /// Builds or modifies image to be added to the gallery.
        /// </summary>
        /// <param name="file">File to be added</param>
        /// <param name="ilvi"></param>
        /// <param name="caption">Text to put in the image description</param>
        private static void GalleryEntryBuilder(FileInfo file, ref Gallery.ImageListViewItem ilvi, string caption)
        {
            // check if ilvi is initialized, if not initialize it
            if (ilvi == null) ilvi = new Gallery.ImageListViewItem { FileName = file.FullName };
            ilvi.Text += caption + " | ";
        }

        private void tpImgSearch_DragDrop(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop, false))
            {
                _QueryBitmap = new Bitmap(Image.FromFile(((string[])e.Data.GetData(DataFormats.FileDrop, false))[0]));
                tpImgSearch.BackgroundImage = _QueryBitmap;
                lbDIH1.Visible = false;
            }
        }

        private void tpImgSearch_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop, false))
                e.Effect = DragDropEffects.All;
            else
                e.Effect = DragDropEffects.None;
        }

        private void ilvGallery_ItemClick(object sender, Gallery.ItemClickEventArgs e)
        {
            lblDescriptorsDetails.Text = e.Item.Text;
        }

        private void btnSearchFlickr_Click(object sender, EventArgs e)
        {
            if (_IsIndexing)
            {
                _IsIndexing = false;
                btnSearchFlickr.Text = "Index Flickr";
                return;
            }
            int Desc ;
            if (!InternetGetConnectedState(out Desc, 0))
            {
                MessageBox.Show("Indexing Flickr needs working internet connection. Unfortunately no working connection was detected.",
                                "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            ilvGallery.Items.Clear();
            _IsIndexing = true;
            btnSearchFlickr.Text = "Stop Indexing";
            if (String.IsNullOrWhiteSpace(tbFlickrQuery.Text))
            {
                MessageBox.Show("The query you entered is invalid");
                return;
            }
            PhotoSearchOptions searchOptions = new PhotoSearchOptions();
            searchOptions.Tags = tbFlickrQuery.Text;
            searchOptions.PerPage = 100;
            searchOptions.SortOrder = PhotoSearchSortOrder.Relevance;
            // there is a function called PhotoSearchAsync - unfortunately it blocked the gui for some reason.
            // using ThreadPool makes the app more responsive
            ThreadPool.QueueUserWorkItem(FlickrPhotoSearch, searchOptions);
        }

        private void FlickrPhotoSearch(object searchOptions)
        {
            PhotoCollection photoCollection = _Flickr.PhotosSearch((PhotoSearchOptions)searchOptions);
            _PhotoIdList.Clear();
            WebClient client = new WebClient();
            PhotosEntities photoEntities = new PhotosEntities();
            foreach (FlickrNet.Photo photo in photoCollection)
            {
                _PhotoIdList.Add(photo.PhotoId);
                string tempFile = Path.Combine(_FolderPath, photo.PhotoId + ".jpg");
                // check if the photo was not indexed before
                if (photoEntities.PhotoSet.Count(p => p.PhotoID == photo.PhotoId) < 1)
                {
                    if (!File.Exists(tempFile))
                        client.DownloadFile(photo.MediumUrl, tempFile);
                    // insert record to db
                    FileInfo tmpFileInfo = new FileInfo(tempFile);
                    // it happened one and got the app crashed .. so just in case
                    if (tmpFileInfo.Length == 0) continue;
                    Photo dbPhotoEntry = new Photo { 
                        PhotoID = photo.PhotoId, 
                        Title = photo.Title, 
                        UrlThumbnail = photo.ThumbnailUrl, 
                        UrlMedium = photo.MediumUrl, 
                        UrlLarge = photo.LargeUrl, 
                        ImagePath = tempFile, 
                        SCD = DescriptorTools.CalculateDescriptor(tmpFileInfo, Descriptor.SCD).BSerialize(), 
                        CLD = DescriptorTools.CalculateDescriptor(tmpFileInfo, Descriptor.CLD).BSerialize(), 
                        EHD = DescriptorTools.CalculateDescriptor(tmpFileInfo, Descriptor.EHD).BSerialize(), 
                        CEDD = DescriptorTools.CalculateDescriptor(tmpFileInfo, Descriptor.CEDD).BSerialize(), 
                        FCTH = DescriptorTools.CalculateDescriptor(tmpFileInfo, Descriptor.FCTH).BSerialize() };
                    photoEntities.PhotoSet.AddObject(dbPhotoEntry);
                }
                else
                {
                    if (!File.Exists(tempFile))
                        client.DownloadFile(photo.MediumUrl, tempFile);
                }
                PopulateGallery(tempFile, photo);
                if (!_IsIndexing)
                {
                    photoEntities.SaveChanges();
                    return;
                }
                   
            }
            if (btnSearchFlickr.InvokeRequired)
            {
                this.Invoke((Action)(() => btnSearchFlickr.Text = "Index Flickr"));
            }
            else
            {
                btnSearchFlickr.Text = "Index Flickr";
            }
            _IsIndexing = false;
            photoEntities.SaveChanges();
        }

        private void tbFlickrQuery_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                btnSearchFlickr_Click(this, null);
        }

        private void PhotoFinderForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            ilvGallery.Items.Clear();
            TempManager.Dispose();
        }

        private void tpSaveBox_DragDrop(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop, false))
            {
                WebClient client = new WebClient();
                SaveFileDialog saveFileDialog = new SaveFileDialog();
                FlickrNet.PhotoInfo photo = _Flickr.PhotosGetInfo(((ImageInfo)ilvGallery.SelectedItems[0].Tag).PhotoID);
                String path = (new Uri(photo.LargeUrl)).LocalPath;
                saveFileDialog.FileName = Path.GetFileName(path);
                saveFileDialog.DefaultExt = Path.GetExtension(path);

                // this switch was designed to support downloading images using original image url
                // which may point to a file with extension different thatn .jpg
                switch (saveFileDialog.DefaultExt)
                {
                    case "jpg":
                        saveFileDialog.Filter = "JPG Files (*.jpg)|*.jpg";
                        break;
                    case "jpeg":
                        saveFileDialog.Filter = "JPEG Files (*.jpeg)|*.jpeg";
                        break;
                    case "gif":
                        saveFileDialog.Filter = "GIF Files (*.gif)|*.gif";
                        break;
                    case "png":
                        saveFileDialog.Filter = "PNG Files (*.png)|*.png";
                        break;
                }
                saveFileDialog.Filter += "|All Files (*.*)|*.*";
                if (saveFileDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    client.DownloadFile(photo.LargeUrl, saveFileDialog.FileName);
                }
            }
        }

        private void tpSaveBox_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop, false))
                e.Effect = DragDropEffects.All;
            else
                e.Effect = DragDropEffects.None;
        }

        private void PhotoFinderForm_Load(object sender, EventArgs e)
        {
            tpSaveBox.AllowDrop = true;

            // find or create image folder
            _FolderPath = Path.Combine(Directory.GetCurrentDirectory(), "Images");
            if (!Directory.Exists(_FolderPath))
                Directory.CreateDirectory(_FolderPath);

            // check sort order to ascending
            rbAsc.Select();
        }

        /// <summary>
        /// Function sorts Gallery items by Descriptor values stored in Tag property of every element.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cbxSort_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!_RefreshGallery)
            {
                return;
            }
            Descriptor d = (Descriptor)Enum.Parse(typeof(Descriptor), cbxSort.SelectedItem.ToString(), true);
            Manina.Windows.Forms.ImageListViewItem[] sortedItemList;
            if (rbAsc.Checked)
                sortedItemList = ilvGallery.Items.OrderBy(ilv =>
                ((ImageInfo)ilv.Tag).GetDescriptorDistance(d)
                ).ToArray();
            else
                sortedItemList = ilvGallery.Items.OrderByDescending(ilv =>
                ((ImageInfo)ilv.Tag).GetDescriptorDistance(d)
                ).ToArray();
            ilvGallery.Items.Clear();
            ilvGallery.Items.AddRange(sortedItemList);
        }

        private void clbDescriptorsList_SelectedIndexChanged(object sender, EventArgs e)
        {
            cbxSort.Items.Clear();
            foreach (var item in clbDescriptorsList.CheckedItems)
            {
                cbxSort.Items.Add(Regex.Match((string)item, @"([A-Z]{1,4})").Groups[0].ToString());
            }
            // we need to prevent reloading of the gallery after select, we will use bool trigger for this
            _RefreshGallery = false;
            if(clbDescriptorsList.GetItemChecked(clbDescriptorsList.SelectedIndex))
                cbxSort.SelectedItem = Regex.Match((string)clbDescriptorsList.SelectedItem, @"([A-Z]{1,4})").Groups[0].ToString();
            else
                cbxSort.SelectedItem = Regex.Match((string)clbDescriptorsList.CheckedItems[0], @"([A-Z]{1,4})").Groups[0].ToString();
        }

        private void rbAsc_CheckedChanged(object sender, EventArgs e)
        {
            cbxSort_SelectedIndexChanged(null, null);
        }

        private void btnFlickr_Click(object sender, EventArgs e)
        {
            _FolderSearch = false;
            tbFlickrQuery.Visible = true;
            btnSearchFlickr.Visible = true;
        }

        private void btnFolder_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog fbd = new FolderBrowserDialog();
            if (fbd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                _SourceFolderPath = fbd.SelectedPath;
                _FolderSearch = true;
                tbFlickrQuery.Visible = false;
                btnSearchFlickr.Visible = false;
            }
        }
    }
}