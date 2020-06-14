using System;
using System.Collections.Generic;
using System.Text;

namespace GenetikAlgoritma
{
    class Caprazlama
    {
        List<Kromozom> adaylar;
        Random rnd;
        public Caprazlama()
        {
            adaylar = new List<Kromozom>();
            rnd = new Random();
        }
        public List<Kromozom> Caprazla(Kromozom k1, Kromozom k2)
        {
            adaylar.Clear();
            int rastgelesayi = rnd.Next(0, k1.genler.Count);
            for (int i = 0; i < 2; i++)
            {
                BirbirineKopyala(i, k1, k2, rastgelesayi);
            }

            return adaylar;
        }
        private void BirbirineKopyala(int i, Kromozom k1, Kromozom k2, int rastgelesayi)
        {
            Kromozom kromozom = new Kromozom();
            for (int j = 0; j < k1.genler.Count; j++)
            {

                kontrolEt(i, j, k1, k2, rastgelesayi, kromozom);
            }

            adaylar.Add(kromozom);
        }
        private void kontrolEt(int i, int j, Kromozom k1, Kromozom k2, int rastgelesayi, Kromozom kromozom)
        {
            if (i == 0)
            {
                if (rastgelesayi == j)
                {
                    kromozom.genEkle(k2.genler[rastgelesayi]);
                }
                else
                {
                    kromozom.genEkle(k1.genler[j]);
                }
            }
            else
            {
                if (rastgelesayi == j)
                {
                    kromozom.genEkle(k1.genler[rastgelesayi]);
                }
                else
                {
                    kromozom.genEkle(k2.genler[j]);
                }
            }
        }
    }
}