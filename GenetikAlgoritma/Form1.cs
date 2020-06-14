using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GenetikAlgoritma
{
    public partial class Form1 : Form
    {
        private int populasyonboyutu = 0;
        private double caprazlamaOranı = 0.0;
        private double mutasyonOranı = 0.0;
        private double seckinlikOranı = 0.0;

        private String secilimTürü = "Random";
        private String çaprazlamaTürü = "Tek Nokta";
        private String mutasyonTürü = "Tek Nokta";
        private int iterasyonSayısı = 0;

        GenetikAlgoritmaCözücü genetikAlgoritma;
        public Form1()
        {
            InitializeComponent();
        }

        private void label8_Click(object sender, EventArgs e)
        {

        }

        private void chrtSonuc_Click(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {
           
        }

        private void groupBox4_Enter(object sender, EventArgs e)
        {

        }

        private void btnBaslaBitir_Click(object sender, EventArgs e)
        {
            btnSıfırla.Enabled = true;
            //caprazlama tek tip olduğu için alınmamıştır.
            parametreAta();
            genetikAlgoritma = new GenetikAlgoritmaCözücü(this.populasyonboyutu, secilimTürü, 
                                                            seckinlikOranı, mutasyonOranı, 
                                                            caprazlamaOranı, panelIterasyon,
                                                            mutasyonTürü,chrtSonuc,chrt_Ortalama,
                                                            chrtStandartSapma);
            genetikAlgoritma.PopulasyonuRastgeleDoldur();
            genetikAlgoritma.Sırala(genetikAlgoritma.AnaPopulasyon);
            genetikAlgoritma.Yakınsa(iterasyonSayısı);
        }
        public void parametreAta() {
            this.populasyonboyutu =(int) nudPopulasyonBoyutu.Value;
            this.caprazlamaOranı = (double)nudCaprazlamaOranı.Value;
            this.mutasyonOranı =(double) nudMutasyonOranı.Value;

            //elitis sayısı yerine oran kullandım
            this.seckinlikOranı = (double)nudSeckinlik.Value;
            this.iterasyonSayısı =(int) nudIterasyonSayısı.Value;

        }

      

        private void rbRandomSecim_CheckedChanged(object sender, EventArgs e)
        {
            this.secilimTürü = "Random";
        }

        private void rbTurnuvaSecimi_CheckedChanged(object sender, EventArgs e)
        {
            this.secilimTürü = "Turnuva";
        }

        private void rbTekNoktalıÇaprazlama_CheckedChanged(object sender, EventArgs e)
        {
            this.çaprazlamaTürü = "Tek Nokta";
        }

        private void rbTekNoktalıMutasyon_CheckedChanged(object sender, EventArgs e)
        {
            this.mutasyonTürü = "Tek Nokta";
        }

        private void rbCokNoktalıMutasyon_CheckedChanged(object sender, EventArgs e)
        {
            this.mutasyonTürü = "İki Nokta";
        }
        GenetikAlgoritmaCözücü temp;
        int i = 0;
        bool x = true;
        private void btnAdımAdım_Click(object sender, EventArgs e)
        {
            if (x == true) {
                tempSıfırla();
                btnSıfırla.Enabled = true;
                x = false;
                btnAdımAdım.Text = "İleri";
            }
                
            i++;
            temp.Yakınsa(i);
            
        }
        private void tempSıfırla() {
            x = true;
            i = 0;
            parametreAta();
            temp = new GenetikAlgoritmaCözücü(this.populasyonboyutu, secilimTürü,
                                                            seckinlikOranı, mutasyonOranı,
                                                            caprazlamaOranı, panelIterasyon,
                                                            mutasyonTürü, chrtSonuc, chrt_Ortalama,
                                                            chrtStandartSapma);
            temp.PopulasyonuRastgeleDoldur();
            temp.Sırala(temp.AnaPopulasyon);
        }

        private void btnSıfırla_Click(object sender, EventArgs e)
        {
            timer1.Stop();
            btnAdımAdım.Text = "Adım Adım";
            tempSıfırla();
            btnSıfırla.Enabled = false;
            temp.clear();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            btnAdımAdım_Click(sender,e);
        }

        private void btnYavas_Click(object sender, EventArgs e)
        {
            tempSıfırla();
            btnSıfırla.Enabled = true;
            timer1.Interval =(int) nudSaniye.Value*1000;
            timer1.Start();
        }

        private void nudSaniye_ValueChanged(object sender, EventArgs e)
        {
            timer1.Interval = (int)nudSaniye.Value * 1000;
        }
    }
}
