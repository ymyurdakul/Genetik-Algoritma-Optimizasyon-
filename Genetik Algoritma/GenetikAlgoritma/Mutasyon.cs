using System;
using System.Collections.Generic;
using System.Text;

namespace GenetikAlgoritma
{
    class Mutasyon
    {
        public Mutasyon() { 
        
        }
        public Kromozom TeknoktaMutasyonla(Kromozom kromozom, double x_min, double xmax)
        {
            //Tek nokta mutasyonlayıcı
            int i = new Random().Next(0,30);
                 
                    kromozom.genler[i].deger = kromozom.genler[i].deger-2;
                    if (kromozom.genler[i].deger > xmax)
                    {
                        kromozom.genler[i].deger = xmax;
                    }
                    if (kromozom.genler[i].deger < x_min)
                    {
                        kromozom.genler[i].deger = x_min;
                    }
                
            
            return kromozom;
        }

        //Cift  nokta mutasyonlayıcı
        public Kromozom IkıNoktaMutasyonla(Kromozom kromozom, double x_min, double xmax)
        {
            for (int x = 0; x < 2; x++)
            {
                
                int i = new Random().Next(0, 30);

                kromozom.genler[i].deger = kromozom.genler[i].deger - 2;
                if (kromozom.genler[i].deger > xmax)
                {
                    kromozom.genler[i].deger = xmax;
                }
                if (kromozom.genler[i].deger < x_min)
                {
                    kromozom.genler[i].deger = x_min;
                }

            }


            return kromozom;
        }
    }
}
