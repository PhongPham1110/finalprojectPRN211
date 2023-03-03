using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using Models;
using _6;

namespace ProjectPRN211
{
    public partial class AddSongs : Form
    {

        private readonly PrnProjectCuoiKy2Context _context;
        public AddSongs()
        {
            InitializeComponent();
            _context = new PrnProjectCuoiKy2Context();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                if (imageText.Text.Length > 0)
                    File.Copy(imageText.Text, Application.StartupPath + @"\Image\" + Path.GetFileName(imageText.Text));
                if (songText.Text.Length > 0)
                    File.Copy(songText.Text, Application.StartupPath + @"\song\" + Path.GetFileName(songText.Text));
                if (imageText.Text.Length >0 && songText.Text.Length >0 && txtName.Text.Length >0)
                {
                    Song song = new()
                    {
                        Author = UserNow.thisUser.UserName,
                        Name = txtName.Text,
                        AudioPath = Path.GetFileName(songText.Text),
                        Lyric = txtLyrics.Text,
                        ImgPath = Path.GetFileName(imageText.Text),
                        IsHide = false,
                    };
                    _context.Songs.Add(song);
                    _context.SaveChanges();
                    MessageBox.Show("add thanh cong bai hat moi");
                }
                else
                    MessageBox.Show("Missing Fields");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnUploadSong_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog1 = new OpenFileDialog();
            openFileDialog1.Filter = "Video (.wmv)|*.wmv|Music (.mp3)|*.mp3|ALL Files (*.*)|*.*";
            openFileDialog1.FilterIndex = 2;
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                songText.Text = openFileDialog1.FileName;
                txtSongPath.Text = openFileDialog1.FileName;
            }
        }

        private void btnUploadImg_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Image Files(*.png;*.jpg;*jpeg;*.gif;) | *.png;*.jpg;*.jpeg;*.gif;";
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                imageText.Text = openFileDialog.FileName;
                txtImagePath.Text = openFileDialog.FileName;
                pictureBox1.Image = new Bitmap(openFileDialog.FileName);
                pictureBox1.ImageLocation = openFileDialog.FileName;
                pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
            }

        }

        private void AddSongs_FormClosing(object sender, FormClosingEventArgs e)
        {
        }
    }
}
