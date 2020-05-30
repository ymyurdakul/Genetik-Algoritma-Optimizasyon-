using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace GenetikAlgoritma
{
    class GenetikAlgoritmaCözücü
    {
        /*
         * Değişken isimleri çok hoş olmamış olabilir 
         * */

        //Gen sayısı alt sınır ve ust sınır sabit olduğu için dışarıya kapattım.
        private const int GEN_SAYISI = 30;
        private const int X_ALT_DEGER = -30;
        private const int X_UST_DEGER = 30;
 
        public Panel Panel_Populasyon_ { get; set; }
        public double Elit_Oran { get; set; }
        public String Kromozom_Secim_Method { get; set; }
        public int Kromozom_Sayısı { get; set; }
        public Populasyon AnaPopulasyon { get; set; }
        public KromozomSecici KromozomSecimi { get; set; }
        public Caprazlama Caprazlayıcı { get; set; }
        public Chart Chart_EnBasarılıKromozom { get; set; }

        public Chart Chart_Ortalama_Uygunluk { get; set; }
        public Mutasyon Mutasyonlayıcı { get; set; }
        Fonksiyon Fitness { get; set; }
        public double Mutasyon_Oranı { get; set; }
        public double Çaprazlama_Oranı { get; set; }
        public String Mutasyon_Türü { get; set; }

        public Chart Chart_Standart_Sapma { get; set; }
        public GenetikAlgoritmaCözücü(int kromozomSayısı,String kromozom_Secim_Method,
                                            double elitOran,double m_oran,double c_oran,
                                            Panel panel,String mutasyontürü,
                                        Chart chartEniyi,Chart chartOrtalama,Chart chartStandartSapma)
        {
            this.Elit_Oran = elitOran;
            this.Kromozom_Secim_Method = kromozom_Secim_Method;
            this.Kromozom_Sayısı =kromozomSayısı;
            this.Mutasyon_Oranı = m_oran;
            this.Çaprazlama_Oranı = c_oran;
            this.Mutasyon_Türü = mutasyontürü;
            AnaPopulasyon = new Populasyon();
            KromozomSecimi = new KromozomSecici();
            Caprazlayıcı = new Caprazlama();
            Mutasyonlayıcı = new Mutasyon();
            Fitness= new Fonksiyon(X_ALT_DEGER, X_UST_DEGER);
            this.Panel_Populasyon_ = panel;

            this.Chart_EnBasarılıKromozom = chartEniyi;
            this.Chart_Ortalama_Uygunluk = chartOrtalama;
            this.Chart_Standart_Sapma = chartStandartSapma;
        }
        public Kromozom KromozomSec(Populasyon pop)
        {
           return KromozomSecimi.kromozom_sec(Kromozom_Secim_Method,pop.kromozomlar);
        }
        public void Sırala(Populasyon pop)
        {

            pop.kromozomlar.Sort();
        }
        public void PopulasyonuRastgeleDoldur()
        {
            Random rnd = new Random();
            for (int x = 0; x < Kromozom_Sayısı; x++)
            {
                Kromozom kromozom = new Kromozom();
                for (int i = 0; i < GEN_SAYISI; i++)
                {
                    kromozom.genEkle(new Gen(rnd.NextDouble() * rnd.Next(X_ALT_DEGER, X_UST_DEGER)));
                }
                kromozom.fitness = Fitness.hesapla(kromozom.genler);
                AnaPopulasyon.KromozomEkle(kromozom);
            }
        }
        public  string PopulasyonuStringOlarakAl()
        {

            StringBuilder builder = new StringBuilder();
            builder.Append("Populasyon Başlangıcı\n");
            for (int i = 0; i < Kromozom_Sayısı; i++)
            {
                builder.Append(AnaPopulasyon.kromozomlar[i].ToString());
            }
            builder.Append("\nPopulasyon Sonu\n");


            return builder.ToString();
        }
        public string PopulasyonGenelBilgi(int nesil, Populasyon pop)
        {
            

            StringBuilder builder = new StringBuilder();
            builder.Append("Nesil : " + nesil + "\n");
            builder.Append("Nesil Ortalama Başarı:" + PopulasyonBasarıOrtalamasıHesapla(pop) + "\n");
            builder.Append("Neslin en iyi kromozomu\n");
            builder.Append(EnBasarılıKromozom(pop).ToString()+"\n");

            RichTextBox textBox = new RichTextBox();
            textBox.Text = builder.ToString();
            textBox.Parent = Panel_Populasyon_;
            textBox.Dock = DockStyle.Top;
            textBox.ReadOnly = true;
            Panel_Populasyon_.Controls.Add(textBox);
            return builder.ToString();
        }
        public List<Kromozom> Caprazla(Kromozom k1,Kromozom k2)
        {
           List<Kromozom>cocuklar= Caprazlayıcı.Caprazla(k1,k2);
            cocuklar.ForEach(cocuk=>cocuk.fitness=Fitness.hesapla(cocuk.genler));
            return cocuklar;
        }
        public Kromozom MutasyonaUgrat(Kromozom kr)
        {
            if(Mutasyon_Türü=="Tek Nokta")
            {
                Kromozom cocuk = Mutasyonlayıcı.TeknoktaMutasyonla(kr, -30, 30);
                cocuk.fitness = Fitness.hesapla(cocuk.genler);
                return cocuk;

            }
            else
            {
                Kromozom cocuk = Mutasyonlayıcı.IkıNoktaMutasyonla(kr, -30, 30);
                cocuk.fitness = Fitness.hesapla(cocuk.genler);
                return cocuk;
            }
          
        }

        public Populasyon JenerasyonOluştur(Populasyon pop)
        {
            List<Kromozom> yeniKromozomlar = new List<Kromozom>();
            Sırala(pop);

            int elit_sayisi  =(int) (AnaPopulasyon.kromozomlar.Count  * Elit_Oran);
            int mutasyon_sayisi=(int)(AnaPopulasyon.kromozomlar.Count * Mutasyon_Oranı);
            int caprazlama_sayisi=(int )(AnaPopulasyon.kromozomlar.Count * Çaprazlama_Oranı);
            //Elit olanlar dan sonrakiler değiştirilecek ve elitlerde seçime dahil edilecek.
            for (int i = elit_sayisi; i <caprazlama_sayisi; i++)
            {
                Kromozom k1 = KromozomSecimi.kromozom_sec(Kromozom_Secim_Method,pop.kromozomlar);
                Kromozom k2 = KromozomSecimi.kromozom_sec(Kromozom_Secim_Method,pop.kromozomlar);
                List<Kromozom>cocuklar=Caprazlayıcı.Caprazla(k1, k2);
                cocuklar.ForEach(cocuk=> {
                    cocuk.fitness = Fitness.hesapla(cocuk.genler);
                    yeniKromozomlar.Add(cocuk);
                    });

            }
            for (int i = elit_sayisi; i < caprazlama_sayisi; i++)
            {
                pop.kromozomlar[i] = yeniKromozomlar[i];
            }

            Sırala(pop);
            for (int i = 0; i < mutasyon_sayisi; i++)
            {
                Random rnd = new Random();
                int x = rnd.Next(elit_sayisi,Kromozom_Sayısı);
                MutasyonaUgrat(pop.kromozomlar[x]);
            }
            
            return pop;


        }
        public Kromozom EnBasarılıKromozom(Populasyon pop)
        {
            Sırala(pop);
            return pop.kromozomlar[0];
        }
        public double PopulasyonBasarıOrtalamasıHesapla(Populasyon pop)
        {
            double sum = 0;
            pop.kromozomlar.ForEach(x => sum += x.fitness);
            return sum / 30;
        }
        public void Yakınsa(int iterasyonSayısı) {
            clear();
            for (int i = 0; i < iterasyonSayısı; i++)
            {
               
                Populasyon pop = JenerasyonOluştur(AnaPopulasyon);
                PopulasyonGenelBilgi(i+1,pop);
                Chart_EnBasarılıKromozom.Series["EN_UYGUN_KROMOZOM"].Points.Add(EnBasarılıKromozom(pop).fitness);
                Chart_Ortalama_Uygunluk.Series["ORTALAMA_UYGUNLUK_DEGERLERİ"].Points.Add(PopulasyonBasarıOrtalamasıHesapla(pop));
                List<double> degerler = new List<double>();
                pop.kromozomlar.ForEach(x=>degerler.Add(x.fitness));
                double s=StandartSapma(degerler);
                Chart_Standart_Sapma.Series["POPULASYON_STANDART_SAPMA"].Points.Add(s);
                 
            }

            

        }

        public void clear()
        {
            Panel_Populasyon_.Controls.Clear();
            Chart_EnBasarılıKromozom.Series["EN_UYGUN_KROMOZOM"].Points.Clear();
            Chart_Ortalama_Uygunluk.Series["ORTALAMA_UYGUNLUK_DEGERLERİ"].Points.Clear();
            Chart_Standart_Sapma.Series["POPULASYON_STANDART_SAPMA"].Points.Clear();
        }

        public double StandartSapma(IEnumerable<double> values)
        {
            double avg = values.Average();
            return Math.Sqrt(values.Average(v => Math.Pow(v - avg, 2)));
        }
    }
}
