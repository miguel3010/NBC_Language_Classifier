
namespace Capa_Dominio.Motor_Inferencia
{
    using DataAccessLayer.DataAccessObjects;
    using Domain_Layer;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    public class Clasificador
    {
        List<Patron> patrones;
        List<Tendencia> tendencias;
        private static int beta = 10; //Feature Scaling

        /// <summary>
        /// Inicializacion de las tendendencias
        /// </summary>
        public Clasificador()
        {
            tendencias = new List<Tendencia>();
        }


        public void setData(List<Patron> patrones)
        {
            this.patrones = patrones;
        }



        /// <summary>
        /// Proceso de Classificacion 
        /// </summary>
        public void Run()
        {
            if (patrones != null && patrones.Any())
            {
                foreach (var item in patrones)
                {
                    int totalTendency = getTotalTendency(item);
                    foreach (var tendencia in item.tendencias)
                    {
                        calculoBayesiano(tendencia, totalTendency);
                    }
                }
                filterTendencias(tendencias);

            }
        }

        /// <summary>
        /// Proceso de Classificacion mediante el algoritmo Naive Bayes
        /// </summary>
        private void calculoBayesiano(Tendencia tendencia, int totalTendency)
        {
            double pre_logit = ((double)tendencia.getmedida()) / ((double)totalTendency);
            pre_logit = pre_logit * ((double)beta);

            Tendencia t = getTendenciaPorCategoria(tendencia.categoria);
            if (t == null)
            {
                t = registerTendencia(tendencia.categoria);
            }
            if (t.Logit > 0)
            {
                t.Logit = t.Logit * pre_logit;
            }
            else
            {
                t.Logit = pre_logit;
            }
        }

        /// <summary>
        /// Sumatoria de todos los datos del poligono de frecuencias para un patron en particular
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        private int getTotalTendency(Patron item)
        {
            int i = 0;

            if (item != null && item.tendencias != null && item.tendencias.Any())
            {
                foreach (var tendencia in item.tendencias)
                {
                    i += tendencia.getmedida();
                }
            }

            return i;
        }

        /// <summary>
        /// Filtra las tendencias que estén en 0
        /// </summary>
        /// <param name="tendencia"></param>
        private void filterTendencias(List<Tendencia> tendencia)
        {
            if (tendencia != null && tendencia.Any())
            {
                int i = 0;
                while (i < tendencia.Count)
                {
                    if (tendencia[i].Logit <= 0)
                    {
                        tendencia.RemoveAt(i);
                        i--;
                    }
                    i++;
                }
            }
        }

        /// <summary>
        /// Agrega una nueva tendencia a la lista de posibles conclusiones
        /// </summary>
        /// <param name="conclusion"></param>
        /// <returns></returns>
        private Tendencia registerTendencia(Categoria conclusion)
        {
            Tendencia t = new Tendencia();
            t.setCategoria_id(conclusion.getid());
            t.categoria = conclusion;
            tendencias.Add(t);
            return t;
        }

        /// <summary>
        /// Busca una Tendencia en las posibles conclusiones dada una categoria c
        /// </summary>
        /// <param name="c"></param>
        /// <returns></returns>
        private Tendencia getTendenciaPorCategoria(Categoria c)
        {
            Tendencia tendencia = null;
            foreach (var item in tendencias)
            {
                if (item.getCategoria_id() == c.getid())
                {
                    tendencia = item;
                    break;
                }
            }
            return tendencia;
        }

        /// <summary>
        /// Obtiene la lista de Tendencias computadas por el clasificador
        /// </summary>
        /// <returns>Lista de tendencias a conclusiones</returns>
        public List<Tendencia> getTendencias()
        {
            return tendencias;
        }

    }
}

