using System;
using System.Collections.Generic;
using System.Text;

namespace GenetikAlgoritma
{
    class Populasyon
    {
        public List<Kromozom> kromozomlar { get; set; }
        public Populasyon()
        {
            kromozomlar = new List<Kromozom>();
        }
        public void KromozomEkle(Kromozom kromozom)
        {
            kromozomlar.Add(kromozom);
        }
        public override string ToString()
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("Populasyon Başlangıcı\n");
            for (int i = 0; i < kromozomlar.Count; i++)
            {
                builder.Append(kromozomlar[i].ToString());
            }
            builder.Append("\nPopulasyon Sonu\n");


            return builder.ToString();
        }




    }
}
