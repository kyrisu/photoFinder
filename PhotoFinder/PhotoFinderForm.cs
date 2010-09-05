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

        public PhotoFinderForm()
        {
            InitializeComponent();
            _Flickr = new Flickr("135794b7b378a7ecbd10186748d28fb0");
        }

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
                    ilvGallery.Items.Add(ilvi);
                    Application.DoEvents();
                }

            }
            catch (UnauthorizedAccessException)
            {
                // so what ... go go go!!! :P
            }
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
            _IsSearching = true;
            btnSearch.Text = "Searching...";
            if (_QueryBitmap == null)
            {
                MessageBox.Show("No query defined!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

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
            else
                SearchInDatabase(GetQuery(_QueryBitmap, desc), desc);
            btnSearch.Text = "Search";
            _IsSearching = false;
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
                cbxSort.Items.Clear();
                foreach (Descriptor descriptor in Enum.GetValues(typeof(Descriptor)))
                {
                    if (descriptor != Descriptor.NONE && (desc & descriptor) == descriptor)
                        cbxSort.Items.Add(descriptor.ToString());
                }
                
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

            _Flickr.PhotosSearchAsync(searchOptions, SearchAsyncFinished);
        }

        private void SearchAsyncFinished(FlickrResult<FlickrNet.PhotoCollection> result)
        {
            if (result.HasError)
            {
                //TODO: exception handling
            }
            else
            {
                PhotoCollection photoCollection = result.Result;
                _PhotoIdList.Clear();
                WebClient client = new WebClient();
                PhotosEntities photoEntities = new PhotosEntities();
                foreach (FlickrNet.Photo photo in photoCollection)
                {
                    _PhotoIdList.Add(photo.PhotoId);
                    //string tempFile = TempManager.GetTempFileName();
                    string tempFile = Path.Combine(_FolderPath, photo.PhotoId + ".jpg");
                    // check if the photo was indexed before
                    if (photoEntities.PhotoSet.Count(p => p.PhotoID == photo.PhotoId) < 1)
                    {
                        client.DownloadFile(photo.MediumUrl, tempFile);
                        // insert record to db
                        FileInfo tmpFileInfo = new FileInfo(tempFile);
                        Photo dbPhotoEntry = new Photo { PhotoID = photo.PhotoId, Title = photo.Title, UrlThumbnail = photo.ThumbnailUrl, UrlMedium = photo.MediumUrl, UrlLarge = photo.LargeUrl, ImagePath = tempFile, SCD = DescriptorTools.CalculateDescriptor(tmpFileInfo, Descriptor.SCD).BSerialize(), CLD = DescriptorTools.CalculateDescriptor(tmpFileInfo, Descriptor.CLD).BSerialize(), EHD = DescriptorTools.CalculateDescriptor(tmpFileInfo, Descriptor.EHD).BSerialize(), CEDD = DescriptorTools.CalculateDescriptor(tmpFileInfo, Descriptor.CEDD).BSerialize(), FCTH = DescriptorTools.CalculateDescriptor(tmpFileInfo, Descriptor.FCTH).BSerialize() };
                        photoEntities.PhotoSet.AddObject(dbPhotoEntry);
                        photoEntities.SaveChanges();
                    }
                    PopulateGallery(tempFile, photo);
                    if (!_IsIndexing)
                        return;
                }
                btnSearchFlickr.Text = "Index Flickr";
            }
        }

        private void tbFlickrQuery_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                btnSearch_Click(this, null);
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
                FlickrNet.PhotoInfo photo = _Flickr.PhotosGetInfo((string)ilvGallery.SelectedItems[0].Tag);
                String path = (new Uri(photo.MediumUrl)).LocalPath;
                saveFileDialog.FileName = Path.GetFileName(path);
                saveFileDialog.DefaultExt = Path.GetExtension(path);
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
                    client.DownloadFile(photo.MediumUrl, saveFileDialog.FileName);
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
            Manina.Windows.Forms.ImageListViewItem[] sortedItemList;
            if(rbAsc.Checked)
                sortedItemList = ilvGallery.Items.OrderBy(ilv => 
                ((ImageInfo)ilv.Tag).GetDescriptorDistance((Descriptor)Enum.Parse(typeof(Descriptor), cbxSort.SelectedItem.ToString(), true))
                ).ToArray();
            else
                sortedItemList = ilvGallery.Items.OrderByDescending(ilv =>
                ((ImageInfo)ilv.Tag).GetDescriptorDistance((Descriptor)Enum.Parse(typeof(Descriptor), cbxSort.SelectedItem.ToString(), true))
                ).ToArray();
            ilvGallery.Items.Clear();
            ilvGallery.Items.AddRange(sortedItemList);
        }
    }
}