using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using _6;
using System.Xml.Linq;
using Models;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using ProjectPRN211;

namespace User_MusicStore
{
    public partial class EditAddMusic : Form
    {
        PrnProjectCuoiKy2Context _context = new();
        public Song tmp { get; set; }
        public EditAddMusic(int songNumber)
        {
            InitializeComponent();
            _context = new PrnProjectCuoiKy2Context();
            if (songNumber != -1)
            {
                Song? song = _context.Songs.Find(songNumber);
                textBox2.Text = songNumber.ToString();
                textBox1.Text = song.Name;
                textBox5.Text = UserNow.thisUser.UserName;
                textBox5.ReadOnly = true;
                songText.Text = song.AudioPath;
                textBox4.Text = song.Lyric;
                imageText.Text = song.ImgPath;
                label7.Text = "Please, you must edit image and song";
            }
            else
            {
                label6.Enabled = false;
                textBox5.Text = UserNow.thisUser.UserName;
                textBox5.ReadOnly = true;
                textBox2.Text = "-1";
                textBox2.Visible = false;
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
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                if (imageText.Text.Length > 0)
                    File.Copy(imageText.Text, Application.StartupPath + @"\Image\" + Path.GetFileName(imageText.Text));
                if (songText.Text.Length > 0)
                    File.Copy(songText.Text, Application.StartupPath + @"\song\" + Path.GetFileName(songText.Text));
                if (imageText.Text.Length > 0 && songText.Text.Length > 0 && textBox1.Text.Length > 0)
                {
                    Song? song = _context.Songs.Find(int.Parse(textBox2.Text));
                    {
                        song.Author = textBox5.Text;
                        song.Name = textBox1.Text;
                        song.Lyric = textBox4.Text;
                        song.AudioPath = Path.GetFileName(songText.Text);
                        song.ImgPath = Path.GetFileName(imageText.Text);
                        song.IsHide = false;
                    };
                    _context.Songs.Update(song);
                    _context.SaveChanges();
                    MessageBox.Show("Song Edited!");
                    this.Close();
                }
                else
                    MessageBox.Show("Missing Fields");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnUploadImg_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Image Files(*.jpg;*jpeg;*.gif;) | *.jpg;*.jpeg;*.gif;";
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                imageText.Text = openFileDialog.FileName;
                pictureBox1.Image = new Bitmap(openFileDialog.FileName);
                pictureBox1.ImageLocation = openFileDialog.FileName;
                pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
            }
        }
        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void EditAddMusic_FormClosing(object sender, FormClosingEventArgs e)
        {

        }
    }
}
