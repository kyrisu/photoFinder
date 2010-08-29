﻿using System;
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

        private Flickr _Flickr;
        private string _SourceFolderPath;
        bool _IsIndexing = false;

        public PhotoFinderForm()
        {
            InitializeComponent();
        }

        private void PopulateGallery(string path)
        {
            try
            {
                FileInfo file = new FileInfo(path);
                if (file.IsImage())
                {
                    Gallery.ImageListViewItem ilvi = new Gallery.ImageListViewItem();
                    ilvi.FileName = file.FullName;
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
            if (_QueryBitmap == null)
            {
                MessageBox.Show("No query defined!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            //check which descriptors are checked
            Descriptor desc = Descriptor.NONE;
            desc |= (clbDescriptorsList.GetItemChecked(0) ? Descriptor.SCD : Descriptor.NONE);
            desc |= (clbDescriptorsList.GetItemChecked(1) ? Descriptor.CLD : Descriptor.NONE);
            desc |= (clbDescriptorsList.GetItemChecked(2) ? Descriptor.DCD : Descriptor.NONE);
            desc |= (clbDescriptorsList.GetItemChecked(3) ? Descriptor.EHD : Descriptor.NONE);
            desc |= (clbDescriptorsList.GetItemChecked(4) ? Descriptor.CEDD : Descriptor.NONE);
            desc |= (clbDescriptorsList.GetItemChecked(5) ? Descriptor.FCTH : Descriptor.NONE);

            ilvGallery.Items.Clear();
            if (desc == Descriptor.NONE)
                MessageBox.Show("You need to choose the descriptor first!", "Warning!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            else
                //TODO: set multiple descriptors per query
                //SearchInFolder(_SourceFolderPath, GetQuery(_QueryBitmap, desc), desc);
                SearchInDatabase(GetQuery(_QueryBitmap, desc), desc);

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
                    
                    foreach (var entry in photoEntities.PhotoSet)
                    {
                            Gallery.ImageListViewItem ilvi = null;

                            foreach (Descriptor descriptor in Enum.GetValues(typeof(Descriptor)))
                            {
                                if (descriptor != Descriptor.NONE && (desc & descriptor) == descriptor)
                                {
                                    double distance = DescriptorTools.CalculateDescriptorDistance(query[descriptor], entry.GetByIndex(descriptor.ToString()).BDeserialize(), descriptor);
                                    if (distance >= 0)
                                    {
                                        string tmpFileName = Path.GetTempFileName();
                                        WebClient client = new WebClient();
                                        client.DownloadFile(entry.Url, tmpFileName);
                                        GalleryEntryBuilder(new FileInfo(tmpFileName), ref ilvi, descriptor.ToString() + ": " + distance.ToString("F"));
                                    }
                                }
                            }

                            // after all descriptors touched the file it goes to the gallery
                            if (ilvi != null) ilvGallery.Items.Add(ilvi);
                    }
                }
                catch (UnauthorizedAccessException)
                {
                    // again.. so what.. go go go!
                }
            }
        }

        /// <summary>
        /// Searches in specifide in _SourceFolderPath directory based on SCD
        /// </summary>
        /// <param name="path"></param>
        /// <param name="query">SCD descriptor from the query image as double[]</param>
        private void SearchInFolder(string path, Dictionary<Descriptor, object> query, Descriptor desc)
        {

            if (!String.IsNullOrEmpty(path))
            {
                try
                {
                    foreach (var folder in (new DirectoryInfo(path)).GetDirectories())
                    {
                        SearchInFolder(folder.FullName, query, desc);
                    }
                    foreach (var file in (new DirectoryInfo(path)).GetFiles())
                    {
                        if (file.IsImage())
                        {
                            Gallery.ImageListViewItem ilvi = null;

                            foreach (Descriptor descriptor in Enum.GetValues(typeof(Descriptor)))
                            {
                                if (descriptor != Descriptor.NONE && (desc & descriptor) == descriptor)
                                {
                                    double distance = DescriptorTools.CalculateDescriptorDistance(query[descriptor], DescriptorTools.CalculateDescriptor(file, descriptor), descriptor);
                                    if (distance >= 0)
                                    {
                                        GalleryEntryBuilder(file, ref ilvi, descriptor.ToString() + ": " + distance.ToString("F"));
                                    }
                                }
                            }

                            // after all descriptors touched the file it goes to the gallery
                            if (ilvi != null) ilvGallery.Items.Add(ilvi);
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
            ilvi.Text += caption + Environment.NewLine;
        }

        private void tpImgSearch_DragDrop(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop, false))
            {
                _QueryBitmap = new Bitmap(Image.FromFile(((string[])e.Data.GetData(DataFormats.FileDrop))[0]));
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

        private void sourceToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog fbd = new FolderBrowserDialog();
            if (fbd.ShowDialog() == System.Windows.Forms.DialogResult.OK && !String.IsNullOrEmpty(fbd.SelectedPath))
            {
                ilvGallery.Items.Clear();
                PopulateGallery(fbd.SelectedPath);

                _SourceFolderPath = fbd.SelectedPath;
            }
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

            _IsIndexing = true;
            btnSearchFlickr.Text = "Stop Indexing";
            if (String.IsNullOrWhiteSpace(tbFlickrQuery.Text))
            {
                MessageBox.Show("The query you entered is invalid");
                return;
            }
            PhotoSearchOptions searchOptions = new PhotoSearchOptions();
            searchOptions.Tags = tbFlickrQuery.Text;

            PhotoCollection photoCollection;
            _Flickr.PhotosSearchAsync(searchOptions, result =>
            {
                if (result.HasError)
                {
                    //TODO: exception handling
                }
                else
                {
                    photoCollection = result.Result;
                    WebClient client = new WebClient();
                    PhotosEntities photoEntities = new PhotosEntities();
                    foreach (FlickrNet.Photo photo in photoCollection)
                    {
                        string tempFile = Path.GetTempFileName();
                        client.DownloadFile(photo.MediumUrl, tempFile);
                        // insert record to db
                        FileInfo tmpFileInfo = new FileInfo(tempFile);
                        Photo dbPhotoEntry = new Photo
                        {
                            PhotoID = photo.PhotoId,
                            Title = photo.Title,
                            Url = photo.OriginalUrl,
                            SCD = DescriptorTools.CalculateDescriptor(tmpFileInfo, Descriptor.SCD).BSerialize(),
                            CLD = DescriptorTools.CalculateDescriptor(tmpFileInfo, Descriptor.CLD).BSerialize(),
                            //DCD = DescriptorTools.CalculateDescriptor(fi, Descriptor.DCD).BSerialize(),
                            EHD = DescriptorTools.CalculateDescriptor(tmpFileInfo, Descriptor.EHD).BSerialize(),
                            CEDD = DescriptorTools.CalculateDescriptor(tmpFileInfo, Descriptor.CEDD).BSerialize(),
                            FCTH = DescriptorTools.CalculateDescriptor(tmpFileInfo, Descriptor.FCTH).BSerialize()
                        };
                        photoEntities.PhotoSet.AddObject(dbPhotoEntry);
                        photoEntities.SaveChanges();
                        PopulateGallery(tempFile);
                        if (!_IsIndexing) return;
                    }
                    btnSearchFlickr.Text = "Index Flickr";
                }
            });
        }

        private void PhotoFinderForm_Load(object sender, EventArgs e)
        {
            _Flickr = new Flickr("135794b7b378a7ecbd10186748d28fb0");
        }
    }
}