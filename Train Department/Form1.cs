using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Web.Script.Serialization;
using System.Net;
using System.Net.Http;
using Newtonsoft.Json;

namespace Train_Department
{
    public partial class Form1 : Form
    {
        int Id;
        public Form1()
        {
            InitializeComponent();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            string url = "https://localhost:7125/api/Train";
            HttpClient client = new HttpClient();
            MyTrain train = new MyTrain();
            train.Name = txtName.Text;
            train.StartStation = txtStartStation.Text;
            train.DestinationStation = txtDestinationStation.Text;
            train.Capacity = Convert.ToInt32(txtCapacity.Text);
            train.DepartureTime = txtDepature.Text;
            train.ArrivalTime = txtArival.Text;
            train.Date = dtpDate.Value.Date.ToString("yyyy-MM-dd");

            string data = JsonConvert.SerializeObject(train);
            var content = new StringContent(data, UnicodeEncoding.UTF8, "application/json");
            var res = client.PostAsync(url, content).Result;
            if (res.IsSuccessStatusCode)
            {
                MessageBox.Show("Train Details Added");
                LoadTrains();

            }
            else
            {
                MessageBox.Show("Failed to add train details");
            }
        }

        public void LoadTrains()
        {
            string url = "https://localhost:7125/api/Train";
            WebClient client = new WebClient();
            client.Headers["content-type"] = "application/json";
            client.Encoding = Encoding.UTF8;

            string json = client.DownloadString(url);
            dgv.DataSource = null;
            dgv.DataSource = JsonConvert.DeserializeObject<List<MyTrain>>(json);
        }

        private void Form1_Load(object sender, EventArgs e)             
        {
            LoadTrains();
        }

        private void dgv_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 0)
            {
                txtName.Text = dgv.Rows[e.RowIndex].Cells[3].Value.ToString();
                txtStartStation.Text = dgv.Rows[e.RowIndex].Cells[4].Value.ToString();
                txtDestinationStation.Text = dgv.Rows[e.RowIndex].Cells[5].Value.ToString();
                txtCapacity.Text = dgv.Rows[e.RowIndex].Cells[6].Value.ToString();
                txtDepature.Text = dgv.Rows[e.RowIndex].Cells[7].Value.ToString();
                txtArival.Text = dgv.Rows[e.RowIndex].Cells[8].Value.ToString();
                dtpDate.Text = dgv.Rows[e.RowIndex].Cells[9].Value.ToString();

                Id = Convert.ToInt32(dgv.Rows[e.RowIndex].Cells[2].Value.ToString());
            }
            else if (e.ColumnIndex == 1)
            {

                Id = Convert.ToInt32(dgv.Rows[e.RowIndex].Cells[2].Value.ToString());
                string url = "https://localhost:7125/api/Train/" + Id;
                HttpClient client = new HttpClient();
                DialogResult result = MessageBox.Show("Are you want to delete?",
                    "Confirm Delete",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question);
                if (result == DialogResult.Yes)
                {
                    var res = client.DeleteAsync(url).Result;
                    if (res.IsSuccessStatusCode)
                    {
                        LoadTrains();
                    }
                }
            }
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            {
                string url = "https://localhost:7125/api/Train/" + Id;
                HttpClient client = new HttpClient();
                MyTrain train = new MyTrain();
                train.TrainId = Id;
                train.Name = txtName.Text;
                train.StartStation = txtStartStation.Text;
                train.DestinationStation = txtDestinationStation.Text;
                train.Capacity = Convert.ToInt32(txtCapacity.Text);
                train.DepartureTime = txtDepature.Text;
                train.ArrivalTime = txtArival.Text;
                train.Date = dtpDate.Text;
                string data = JsonConvert.SerializeObject(train);
                var content = new StringContent(data, UnicodeEncoding.UTF8, "application/json");
                var res = client.PutAsync(url, content).Result;
                if (res.IsSuccessStatusCode)
                {
                    MessageBox.Show("Train Updated");
                    LoadTrains();
                }
                else
                {
                    MessageBox.Show("Failed to update train");
                }
            }
        }

        public class MyTrain
        {
            public int TrainId { get; set; }
            public string Name { get; set; }
            public string StartStation { get; set; }
            public string DestinationStation { get; set; }
            public int Capacity { get; set; }
            public string DepartureTime { get; set; }
            public string ArrivalTime { get; set; }
            public string Date { get; set; }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            BookingDetails bookingDetails = new BookingDetails();
            bookingDetails.Show();
            this.Hide();
        }

       
    }
}
