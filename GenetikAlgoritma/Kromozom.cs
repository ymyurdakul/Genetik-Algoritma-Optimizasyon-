using System;
using System.Collections.Generic;
using System.Text;

namespace GenetikAlgoritma
{
    class Kromozom:IComparable
    {
        public List<Gen> genler { get; set; }
        public double fitness { get; set; }

        public Kromozom()
        {
            genler = new List<Gen>();
            
        }
        public void genEkle(Gen gen)
        {
            genler.Add(gen);
        }
        public override string ToString()
        {

         
            StringBuilder builder = new StringBuilder();
            builder.Append("----------------------------------------------------------------\n");
            for (int i = 0; i < this.genler.Count; i++)
            {
                if (i % 5==0)
                    builder.Append("\n");
                builder.Append("( x"+(i+1)+":"+genler[i].deger.ToString()+")");
            }
            builder.Append("\n").Append("Uygunluk Değeri:"+this.fitness+"\n");
            builder.Append("----------------------------------------------------------------");
            return builder.ToString();
        }
       
        public int CompareTo(object obj)
        {
            Kromozom kr = (Kromozom)obj;
            if (this.fitness < kr.fitness)
            {
                return 1;
            }
            else if (this.fitness == kr.fitness)
            {
                return 0;
            }
            else return -1;
        }
    }
}
