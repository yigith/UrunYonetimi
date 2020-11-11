using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using UrunYonetimi.Models;

namespace UrunYonetimi
{
    public partial class DuzenleForm : Form
    {
        public event EventHandler UrunDuzenlendi;

        private readonly Urun urun;

        public DuzenleForm(Urun urun)
        {
            this.urun = urun;
            InitializeComponent();
            txtUrunAd.Text = urun.UrunAd;
            nudBirimFiyat.Value = urun.BirimFiyat;
            Text = urun.ToString();
        }

        private void btnIptal_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btnKaydet_Click(object sender, EventArgs e)
        {
            var urunAd = txtUrunAd.Text.Trim();

            if (urunAd == "")
            {
                MessageBox.Show("Ürün adı girmediniz!");
                return;
            }

            urun.UrunAd = urunAd;
            urun.BirimFiyat = nudBirimFiyat.Value;

            // UrunDuzenlendi event'ini tetikle
            UrunDuzenlendiginde(EventArgs.Empty);

            Close();
        }

        protected virtual void UrunDuzenlendiginde(EventArgs args)
        {
            if (UrunDuzenlendi != null)
            {
                UrunDuzenlendi(this, args);
            }
        }
    }
}
