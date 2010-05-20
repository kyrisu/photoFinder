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

namespace PhotoFinder
{
    public partial class PhotoFinderForm : Form
    {
        /// <summary>
        /// Bitmap object to be later passed to descriptor calculating methods.
        /// </summary>
        private Bitmap _QueryBitmap = null;

        private string _SourceFolderPath;
        public PhotoFinderForm()
        {
            InitializeComponent();


        }

        private void PopulateGallery(string path)
        {
            try
            {
                foreach (var folder in (new DirectoryInfo(path)).GetDirectories())
                {
                    PopulateGallery(folder.FullName);
                }
                foreach (var file in (new DirectoryInfo(path)).GetFiles())
                {
                    if (file.IsImage())
                    {
                        Gallery.ImageListViewItem ilvi = new Gallery.ImageListViewItem();
                        ilvi.FileName = file.FullName;
                        ilvGallery.Items.Add(ilvi);
                    }
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
                SearchInFolder(_SourceFolderPath, DescriptorTools.PoliczEHD(_QueryBitmap), desc);

        }

        /// <summary>
        /// Searches in specifide in _SourceFolderPath directory based on SCD
        /// </summary>
        /// <param name="path"></param>
        /// <param name="query">SCD descriptor from the query image as doublep[]</param>
        private void SearchInFolder(string path, double[] query, Descriptor desc)
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

                            if ((desc & Descriptor.SCD) == Descriptor.SCD)
                            {
                                double distance = DescriptorTools.CalculateSCDDistance(query, DescriptorTools.CalculateSCDHistogram(file));
                                if (distance >= 0)
                                {
                                    GalleryEntryBuilder(file, ref ilvi, "SCD diff: " + distance.ToString());
                                }
                            }

                            if ((desc & Descriptor.CLD) == Descriptor.CLD)
                            {
                                throw new NotImplementedException();
                            }

                            if ((desc & Descriptor.DCD) == Descriptor.DCD)
                            {
                                throw new NotImplementedException();
                            }

                            if ((desc & Descriptor.EHD) == Descriptor.EHD)
                            {
                                double distance = DescriptorTools.PoliczOdlegloscEHD(query, DescriptorTools.PoliczEHD(file));
                                if (distance >= 0)
                                {
                                    GalleryEntryBuilder(file, ref ilvi, "EHD diff: " + distance.ToString());
                                }
                            }

                            if ((desc & Descriptor.FCTH) == Descriptor.FCTH)
                            {
                                throw new NotImplementedException();
                            }

                            if ((desc & Descriptor.CEDD) == Descriptor.CEDD)
                            {
                                // my implementation of CEDD goes here :D
                                throw new NotImplementedException();
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
            if (ilvi == null) ilvi = new Gallery.ImageListViewItem();
            ilvi.FileName = file.FullName;
            ilvi.Text = caption + Environment.NewLine;
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

        }
    }
}