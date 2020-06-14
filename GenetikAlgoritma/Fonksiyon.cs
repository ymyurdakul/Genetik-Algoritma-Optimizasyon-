using System;
using System.Collections.Generic;
using System.Text;

namespace GenetikAlgoritma
{
    class Fonksiyon
    {
     
        public double Altsinir { get; set; }
        public double Ustsinir{get;set;}
  
        public Fonksiyon(double altsinir, double ustsinir)
        {
            this.Altsinir = altsinir;
            this.Ustsinir = ustsinir;
        
        }
        public double hesapla(List<Gen> x) {
            //d 30 olması Gerekir 
            int d = x.Count;
            double sum = 0;
            for (int i = 0; i < d-1; i++)
            {
                double xi = x[i].deger;
                double xnext = x[i + 1].deger;
                double c= 100 *Math.Pow( (xnext - Math.Pow(xi, 2)),2) + Math.Pow((xi - 1), 2);
                sum += c;
            }
            return sum;
        }
    }
}
