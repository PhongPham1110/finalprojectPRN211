using Models;
using User_MusicStore;

namespace ProjectPRN211
{
    public partial class AdminManageSongs : Form
    {
        private readonly PrnProjectCuoiKy2Context _context;
        public AdminManageSongs()
        {
            InitializeComponent();
            _context = new PrnProjectCuoiKy2Context();
            //cbGenre.DataSource = _context.Songs.ToList();
            //cbGenre.DisplayMember = ""
            dataGridView1.DataSource = _context.Songs.ToList();
            dataGridView1.Columns["AuthorNavigation"].Visible = false;
            dataGridView1.Columns["ListSongs"].Visible = false;
            int count = dataGridView1.Columns.Count;
            DataGridViewButtonColumn btnDetail = new DataGridViewButtonColumn
            {
                Text = "Hide",
                Name = "Hide",
                UseColumnTextForButtonValue = true
            };

            DataGridViewButtonColumn btnDelete = new DataGridViewButtonColumn
            {
                Text = "Delete",
                Name = "Delete",
                UseColumnTextForButtonValue = true
            };
            dataGridView1.Columns.Insert(count, btnDelete);
            dataGridView1.Columns.Insert(count, btnDetail);

        }
        void bindGrid()
        {
            dataGridView1.DataSource = _context.Songs.ToList();
        }
        void bindGirdSearch(string name)
        {

            var songs = _context.Songs.Where(a => a.Name.Contains(name)).ToList();
            dataGridView1.DataSource = songs;
        }
        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == dataGridView1.Columns["Hide"].Index)
            {
                int songNumber = (int)dataGridView1.Rows[e.RowIndex].Cells["SongNumber"].Value;
                Song song = _context.Songs.Find(songNumber);
                if (song.IsHide == true)
                {
                    song.IsHide = false;
                }
                else
                {
                    song.IsHide = true;
                }
                _context.Songs.Update(song);
                _context.SaveChanges();
                bindGrid();
            }
            else if (e.ColumnIndex == dataGridView1.Columns["Delete"].Index)
            {
                int songNumber = (int)dataGridView1.Rows[e.RowIndex].Cells["SongNumber"].Value;
                Song song = _context.Songs.Find(songNumber);
                _context.Songs.Remove(song);
                _context.SaveChanges();
                if (_context.SaveChanges() > 0)
                {
                    MessageBox.Show("Delete success");
                }
                bindGrid();
            }
        }
        private void btnSearch_Click(object sender, EventArgs e)
        {
            string name = txtName.Text;
            bindGirdSearch(name);

        }
        private void button2_Click(object sender, EventArgs e)
        {
            AddSongs form3 = new AddSongs();
            DialogResult dr = form3.ShowDialog();
            if (dr == DialogResult.OK)
                bindGrid();
        }
        private void AdminManageSongs_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            int songNumber = (int)dataGridView1.Rows[e.RowIndex].Cells["SongNumber"].Value;
            EditAddMusic update = new EditAddMusic(songNumber);
            DialogResult dr = update.ShowDialog();
            if (dr == DialogResult.OK)
                bindGrid();
        }
    }
}