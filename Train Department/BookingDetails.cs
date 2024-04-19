using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Train_Department
{
    
    public partial class BookingDetails : Form
     {
        int Id;
        public BookingDetails()
        {
            InitializeComponent();
        }

        private void dgvBookings_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 0)
            {

                Id = Convert.ToInt32(dgvBookings.Rows[e.RowIndex].Cells[1].Value.ToString());

                string url = "https://localhost:7125/api/Booking/" + Id;

                HttpClient client = new HttpClient();
                DialogResult result = MessageBox.Show("Are you sure you want to delete",
                    "Confirm Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (result == DialogResult.Yes)
                {
                    var res = client.DeleteAsync(url).Result;
                    if (res.IsSuccessStatusCode)
                    {
                        LoadUser();
                    }
                }
            }

        }

        public async Task LoadUser()
        {
            string url = "https://localhost:7125/api/Booking";
            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
                using (var response = await client.GetAsync(url))
                {
                    if (response.IsSuccessStatusCode)
                    {
                        string json = await response.Content.ReadAsStringAsync();
                        dgvBookings.DataSource = null;
                        dgvBookings.DataSource = JsonConvert.DeserializeObject<List<Bookings>>(json);
                        dgvBookings.Columns["TrainId"].Visible = false;
                    }
                    else
                    {
                        MessageBox.Show("Failed to retrieve train data. Status code: " + response.StatusCode);
                    }
                }
            }
        }

        public class Bookings
        {
            public int BookingId { get; set; }

            public int TrainId { get; set; }

            public string NIC { get; set; }

            public string StartStation { get; set; }

            public string DestinationStation { get; set; }

            public int SeatCapacity { get; set; }

            public String DepartureTime { get; set; }

            public String ArrivalTime { get; set; }

            public String Date { get; set; }
        }

        private void BookingDetails_Load(object sender, EventArgs e)
        {
            LoadUser();
        }

        public class BookingCreateDTO
        {
            public int TrainId { get; set; }
            public string NIC { get; set; }
            public int SeatCapacity { get; set; }
            public string StartStation { get; set; }
            public string DestinationStation { get; set; }
            public string DepartureTime { get; set; }
            public string ArrivalTime { get; set; }
            public string Date { get; set; }
        }

    }
}
