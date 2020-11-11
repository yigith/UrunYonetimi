using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using UrunYonetimi.Models;

namespace UrunYonetimi
{
    public partial class Form1 : Form
    {
        BindingList<Urun> blUrunler = new BindingList<Urun>();

        public Form1()
        {
            VerileriOku();
            InitializeComponent();
            dgvUrunler.AutoGenerateColumns = false;
            dgvUrunler.DataSource = blUrunler;
        }

        private void OrnekVerileriYukle()
        {
            blUrunler.Add(new Urun { UrunAd = "Kola", BirimFiyat = 4m });
            blUrunler.Add(new Urun { UrunAd = "Ayran", BirimFiyat = 3.5m });
        }

        private void btnDuzenle_Click(object sender, EventArgs e)
        {
            if (dgvUrunler.SelectedRows.Count == 0)
                return;

            Urun seciliUrun = (Urun)dgvUrunler.SelectedRows[0].DataBoundItem;
            DuzenleForm frmDuzenle = new DuzenleForm(seciliUrun);
            frmDuzenle.UrunDuzenlendi += FrmDuzenle_UrunDuzenlendi;
            frmDuzenle.Show();
        }

        private void FrmDuzenle_UrunDuzenlendi(object sender, EventArgs e)
        {
            blUrunler.ResetBindings();
        }

        private void btnEkle_Click(object sender, EventArgs e)
        {
            var urunAd = txtUrunAd.Text.Trim();

            if (urunAd == "")
            {
                MessageBox.Show("Ürün adı girmediniz!");
                return;
            }

            blUrunler.Add(new Urun { UrunAd = urunAd, BirimFiyat = nudBirimFiyat.Value });
        }

        private void btnSil_Click(object sender, EventArgs e)
        {
            if (dgvUrunler.SelectedRows.Count == 0)
                return;

            Urun seciliUrun = (Urun)dgvUrunler.SelectedRows[0].DataBoundItem;
            blUrunler.Remove(seciliUrun);
        }

        private void dgvUrunler_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex != -1)
            {
                btnDuzenle.PerformClick();
            }
        }

        private void dgvUrunler_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvUrunler.SelectedRows.Count > 0)
            {
                btnDuzenle.Enabled = btnSil.Enabled = true;
            }
            else
            {
                btnDuzenle.Enabled = btnSil.Enabled = false;
            }
        }

        private void dgvUrunler_KeyDown(object sender, KeyEventArgs e)
        {
            if (dgvUrunler.SelectedRows.Count > 0 && e.KeyCode == Keys.Delete)
            {
                btnSil.PerformClick();
            }
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            VerileriKaydet();
        }

        private void VerileriKaydet()
        {
            string json= JsonConvert.SerializeObject(blUrunler);
            File.WriteAllText("veri.json", json);
        }

        private void VerileriOku()
        {
            try
            {
                string json = File.ReadAllText("veri.json");
                blUrunler = JsonConvert.DeserializeObject<BindingList<Urun>>(json);
            }
            catch (Exception)
            {
                OrnekVerileriYukle();
            }
        }
    }
}
