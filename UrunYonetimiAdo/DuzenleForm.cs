using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using UrunYonetimiAdo.Models;

namespace UrunYonetimiAdo
{
    public partial class DuzenleForm : Form
    {
        private readonly Urun orjinalUrun;

        public event EventHandler<UrunDuzenlendiEventArgs> UrunDuzenlendi;

        public DuzenleForm(Urun urun)
        {
            orjinalUrun = urun;
            InitializeComponent();
            txtUrunAd.Text = orjinalUrun.UrunAd;
            nudBirimFiyat.Value = orjinalUrun.BirimFiyat;
        }

        protected virtual void UrunDuzenlendiginde(UrunDuzenlendiEventArgs args)
        {
            if (UrunDuzenlendi != null)
            {
                UrunDuzenlendi(this, args);
            }
        }

        private void btnIptal_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btnKaydet_Click(object sender, EventArgs e)
        {
            string urunAd = txtUrunAd.Text.Trim();
            decimal birimFiyat = nudBirimFiyat.Value;

            if (urunAd == "")
            {
                MessageBox.Show("Ürün adı girmelisiniz.");
                return;
            }

            var args = new UrunDuzenlendiEventArgs();
            args.EskiUrun = orjinalUrun;
            args.YeniUrun = new Urun()
            {
                Id = orjinalUrun.Id,
                UrunAd = urunAd,
                BirimFiyat = birimFiyat
            };

            UrunDuzenlendiginde(args);
            Close();
        }

        private void DuzenleForm_Load(object sender, EventArgs e)
        {
            CenterToParent();
        }
    }
}
