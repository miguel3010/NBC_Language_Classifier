namespace Capa_Dominio.Motor_Inferencia
{ 
    using Domain_Layer;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    public class Normalizador
    {
        private List<Tendencia> logits;
        public void BayesNormalizer()
        {
            if (logits != null && logits.Any())
            {
                List<Tendencia> log = new List<Tendencia>();
                int i = 0; 
                double sum = 0;
                while (i < logits.Count)
                {
                    sum += logits[i].Logit;
                    i++;
                }
                i = 0;
                while (i < logits.Count)
                {
                    Tendencia current = logits[i];
                    current.Logit /= sum;
                    current.Logit *= 100;
                    log.Add(current);
                    i++;
                }
                if (log.Any())
                {
                    logits = log;
                }
            }
        }
        public void SoftmaxNormalizer()
        {
            if (logits != null && logits.Any())
            {
                int i = 0;                
                double sum = getSumatoria(logits);
                List<Tendencia> log = new List<Tendencia>();
                while (i < logits.Count)
                {
                    Tendencia current = logits[i];
                    current.Logit = (double)Math.Pow(Math.E, (double)current.Logit) / sum;
                    log.Add(current);
                    i++;
                }
                if (log.Any())
                {
                    this.logits = log;
                }
            }
        }
        private double getSumatoria(List<Tendencia> y)
        {
            int i = 0;
            double g = 0;
            while (i < y.Count)
            {
                g += (double)Math.Pow(Math.E, (double)y[i].Logit);
                i++;
            }
            return g;
        }
        public void SetTendencias(List<Tendencia> L)
        {
            if (L != null && L.Any())
            {
                this.logits = L;
            }
        }
        public List<Tendencia> getConclusiones()
        {
            return logits;
        }
    }
}

