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

        private void tpImgSearch_DoubleClick(object sender, EventArgs e)
        {
            OpenFileDialog ofDialog = new OpenFileDialog();
            if (ofDialog.ShowDialog() == DialogResult.OK && !String.IsNullOrEmpty(ofDialog.FileName))
            {
                try
                {
                    _QueryBitmap = new Bitmap(ofDialog.FileName);
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
           
            SearchInFolder(CalculateSCDHistogram(_QueryBitmap));

        }

        /// <summary>
        /// Searches in specifide in _SourceFolderPath directory based on SCD
        /// </summary>
        /// <param name="query">SCD descriptor from the query image as doublep[]</param>
        private void SearchInFolder(double[] query)
        {
            ilvGallery.Items.Clear();
            if (!String.IsNullOrEmpty(_SourceFolderPath))
            {
                foreach (var file in (new DirectoryInfo(_SourceFolderPath)).GetFiles())
                {
                    if (file.IsImage())
                    {
                        double distance = CalculateSCDDistance(query, CalculateSCDHistogram(file));
                        if(distance >= 0)
                        {
                            Gallery.ImageListViewItem ilvi = new Gallery.ImageListViewItem();
                            ilvi.FileName = file.FullName;
                            ilvi.Text = distance.ToString();
                            ilvGallery.Items.Add(ilvi);
                        }
                    }
                }
            }
            ilvGallery.Items.OrderBy(i => i.Text);
            ilvGallery.Refresh();
        }

        private double[] CalculateSCDHistogram(FileInfo image)
        {
            Bitmap bitmap = new Bitmap(image.FullName);
            return CalculateSCDHistogram(bitmap);
        }

        private double[] CalculateSCDHistogram(Bitmap bitmap)
        {
            SCD_Descriptor SCD = new SCD_Descriptor();
            SCD.Apply(bitmap, 64, 0);
            return SCD.Norm4BitHistogram;
        }

        private double CalculateSCDDistance(double[] histogramA, double[] histogramB)
        {
            double result = 0;

            for (int i = 0; i < 64; i++)
            {
                result += Math.Abs(histogramA[i] - histogramB[i]);

            }
            return result;
        }

        private void tpImgSearch_DragDrop(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop, false))
            {
                _QueryBitmap = new Bitmap(((string[])e.Data.GetData(DataFormats.FileDrop))[0]);
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
                PopulateGallery(fbd.SelectedPath);

                _SourceFolderPath = fbd.SelectedPath;
            }
        }
    }
}