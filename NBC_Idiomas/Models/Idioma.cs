using Domain_Layer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NBC_Idiomas.Models
{
    public class Idioma
    {
        public int id;
        public String nombre;

        public Idioma()
        {
        }
        public Idioma(Categoria cat)
        {
            if (cat != null)
            {
                this.id = cat.getid();
                this.nombre = cat.getnombre();
            }
        }

        public static List<Idioma> toIdiomaList(List<Categoria> cat)
        {
            List<Idioma> idiomas = null;
            if (cat != null && cat.Any())
            {
                idiomas = new List<Idioma>();
                foreach (var item in cat)
                {
                    idiomas.Add(new Idioma(item));
                }
            }
            return idiomas;
        }

    }
}