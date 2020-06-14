using System;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Security.Cryptography;
using System.Text;

namespace GenetikAlgoritma
{
    class KromozomSecici
    {
        public  Kromozom kromozom_sec(String method,List<Kromozom>kromozomlar)
        {
            Kromozom kromozom = null;
            switch (method)
            {
                case "Random":
                    kromozom= Random(kromozomlar);
                    break;
                case "Turnuva":
                    kromozom= Turnuva(kromozomlar);
                    break;
         
            }
            return kromozom;
        }
        private Kromozom Random(List<Kromozom> kromozomlar) {
            Random rnd = new Random();
            int x = rnd.Next(0,kromozomlar.Count);
            return kromozomlar[x];
        }
        private Kromozom Turnuva(List<Kromozom> kromozomlar)
        {
            Random rnd = new Random();
            int birinciSayı = rnd.Next(0, kromozomlar.Count);
            int ikinciSayı = rnd.Next(0, kromozomlar.Count);
            if (ikinciSayı <= birinciSayı)
                return kromozomlar[ikinciSayı];
            else
                return kromozomlar[birinciSayı];
        }
    }
}
