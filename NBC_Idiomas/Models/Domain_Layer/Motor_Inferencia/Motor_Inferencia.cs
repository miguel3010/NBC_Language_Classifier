
namespace Capa_Dominio.Motor_Inferencia
{
    using DataAccessLayer.DataAccessObjects;
    using Domain_Layer;
    using NBC_Idiomas.Models.DataAccessLayer;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Text.RegularExpressions;

    public class Motor_Inferencia
    {

        private Clasificador clasificador;
        private Normalizador normalizador;
        private List<Patron> knownPatterns;
        private List<Patron> UnknownPatterns;
        private List<Tendencia> tendencias;

        public Motor_Inferencia()
        {
            this.clasificador = new Clasificador();
            this.normalizador = new Normalizador();
            knownPatterns = new List<Patron>();
            UnknownPatterns = new List<Patron>();
        }


        public void process(String txt)
        {
            txt = preprocessString(txt);
            if (!String.IsNullOrEmpty(txt))
            {
                char[] whitespace = new char[] { ' ', '\t' };
                String[] splittedTxt = txt.Split(whitespace);
                setDataRaw(splittedTxt);

                /// Clasification Process
                clasificador.setData(knownPatterns);
                clasificador.Run();
                tendencias = clasificador.getTendencias();
                /// Normalization Process
                this.normalizador.SetTendencias(tendencias);
                this.normalizador.BayesNormalizer();
                tendencias = this.normalizador.getConclusiones(); 
                /// Proactive Learning
                tendencias = ordenarTendencias(tendencias);
               // autoaprendizaje(tendencias, knownPatterns, UnknownPatterns);
            }

        }

        private void autoaprendizaje(List<Tendencia> tendencias, List<Patron> knownPatterns, List<Patron> unknownPatterns)
        {
            if (tendencias != null)
            {
                foreach (var tendencia in tendencias)
                {
                    if (knownPatterns != null && knownPatterns.Any())
                    {
                        foreach (var pattern in knownPatterns)
                        {
                            setKnowledge(pattern, tendencia.categoria);
                        }
                    }
                    if (unknownPatterns != null && unknownPatterns.Any())
                    {
                        foreach (var pattern in unknownPatterns)
                        {
                            setKnowledge(pattern, tendencia.categoria);
                        }
                    }
                }
            }
        } 

        public void setKnowledge(String txt, Categoria categoria)
        {
            txt = preprocessString(txt);
            if (!String.IsNullOrEmpty(txt))
            {
                char[] whitespace = new char[] { ' ', '\t' };
                String[] splittedTxt = txt.Split(whitespace);
                foreach (var item in splittedTxt)
                {
                    Patron patron = new Patron();
                    patron.setnombre(item);
                    setKnowledge(patron, categoria);
                }

            }

        }
        private void setKnowledge(Patron patron, Categoria categoria)
        {
            new Transactions().setKnowledge(patron, categoria);
        }

        private void setDataRaw(string[] splittedTxt)
        {
            if (splittedTxt.Any())
            {
                foreach (var item in splittedTxt)
                {
                    Patron p = PatronDAO.getInstance().read(item);
                    if (p != null)
                    {
                        setKnownPattern(p);
                    }
                    else
                    {
                        p = new Patron();
                        p.setnombre(item);
                        UnknownPatterns.Add(p);
                    }
                }
            }
        }

        private void setKnownPattern(Patron p)
        {
            readPatronDependencies(p);
            knownPatterns.Add(p);
        }

        private void readPatronDependencies(Patron p)
        {
            p.tendencias = TendenciaDAO.getInstance().readALL(p);
            if (p.tendencias != null)
            {
                foreach (var item in p.tendencias)
                {
                    item.categoria = CategoriaDAO.getInstance().read(item);
                }
            }
        }

        public List<Tendencia> getTendencias()
        {
            return tendencias;
        }

        private string preprocessString(string txt)
        {
            txt = Regex.Replace(txt, "[0-9]+", "");
            txt = txt.Replace(";", String.Empty);
            txt = txt.Replace("-", String.Empty);
            txt = txt.Replace(".", String.Empty);
            txt = txt.Replace(":", String.Empty);
            txt = txt.Replace("/", String.Empty);
            txt = txt.Replace(",", String.Empty);
            txt = txt.Replace(",", String.Empty);
            txt = txt.Replace(",", String.Empty);
            txt = txt.Replace(")", String.Empty);
            txt = txt.Replace("(", String.Empty);
            txt = txt.Replace("\"", String.Empty);
            txt = txt.Replace("_", String.Empty);
            txt = txt.Replace("*", String.Empty);
            txt = txt.Replace("/", String.Empty);
            txt = txt.Replace("#", String.Empty);
            txt = txt.Replace("$", String.Empty);
            txt = txt.Replace("%", String.Empty);
            txt = txt.Replace("&", String.Empty);
            txt = txt.Replace("=", String.Empty);
            txt = txt.Replace("?", String.Empty);
            txt = txt.Replace("¡", String.Empty);
            txt = txt.Replace("¿", String.Empty);
            txt = txt.Replace("'", String.Empty);
            txt = txt.Replace("|", String.Empty);
            txt = txt.Replace("°", String.Empty);
            txt = txt.Replace("¬", String.Empty);
            txt = txt.Replace("[", String.Empty);
            txt = txt.Replace("]", String.Empty);
            txt = txt.Replace("{", String.Empty);
            txt = txt.Replace("}", String.Empty);
            txt = txt.Replace("^", String.Empty);
            txt = txt.Replace("`", String.Empty);
            txt = txt.Replace("+", String.Empty);
            txt = txt.Replace("~", String.Empty);
            txt = txt.Replace("<", String.Empty);
            txt = txt.Replace(">", String.Empty);
            txt = txt.Replace("╩", String.Empty);
            return txt;
        }

        private List<Tendencia> ordenarTendencias(List<Tendencia> tendencias)
        {
            if (tendencias != null && tendencias.Any())
            {
                int h = 1;
                int inner, outer;
                int n = tendencias.Count;
                Tendencia temp;

                while (h <= n / 3)
                {
                    h = h * 3 + 1;
                }
                while (h > 0)
                {
                    outer = h;
                    while (outer < n)
                    {
                        temp = tendencias[outer];
                        inner = outer;
                        while (inner > (h - 1) && (float)tendencias[inner - h].Logit >= temp.Logit)
                        {
                            tendencias[inner] = tendencias[inner - h];
                            inner -= h;
                        }
                        tendencias[inner] = temp;
                        outer++;
                    }
                    h = (h - 1) / 3;
                }
                tendencias = reverse(tendencias);
                return tendencias;
            }
            return null;
        }

        private List<Tendencia> reverse(List<Tendencia> data)
        {
            List<Tendencia> ndata = new List<Tendencia>();
            int i = data.Count - 1;

            while (i >= 0)
            {
                ndata.Add(data[i]);
                i--;
            }
            return ndata;

        }
    }
}

