///
/// Copyright (C) Miguel Ángel Campos - All Rights Reserved
/// Unauthorized copying of this file, via any medium is strictly prohibited
/// Proprietary and confidential
/// Written by Miguel Ángel Campos <camposmiguelangel3010@gmail.com>  twitter: @Miguel_Angel_30,
/// January 2017
/// 
namespace Domain_Layer
{
    using DataAccessLayer.DataAccessObjects.Dependencies;
    public class Tendencia : DTO
    {
        //Atributes
        private int id;
        private int Categoria_id;
        public Categoria categoria;
        private int Patron_id;
        public Patron patron;
        private int medida;

        public double Logit { get; internal set; }

        //Methods
        public bool isValid()
        {
            if (Categoria_id != -1 &&
             Patron_id != -1)
            {
                return true;
            }
            return false;
        }
        /// <summary>
        ///
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public void setid(int id)
        {
            this.id = id;
        }
        /// <summary>
        ///
        /// </summary>
        /// <returns></returns>
        public int getid()
        {
            return this.id;
        }
        /// <summary>
        ///
        /// </summary>
        /// <returns></returns>
        public int getCategoria_id()
        {
            return this.Categoria_id;
        }
        /// <summary>
        ///
        /// </summary>
        /// <param name="Categoria_id"></param>
        /// <returns></returns>
        public void setCategoria_id(int Categoria_id)
        {
            this.Categoria_id = Categoria_id;
        }
        /// <summary>
        ///
        /// </summary>
        /// <returns></returns>
        public int getPatron_id()
        {
            return this.Patron_id;
        }
        /// <summary>
        ///
        /// </summary>
        /// <param name="Patron_id"></param>
        /// <returns></returns>
        public void setPatron_id(int Patron_id)
        {
            this.Patron_id = Patron_id;
        }
        /// <summary>
        ///
        /// </summary>
        /// <param name="medida"></param>
        /// <returns></returns>
        public void setmedida(int medida)
        {
            this.medida = medida;
        }
        /// <summary>
        ///
        /// </summary>
        /// <returns></returns>
        public int getmedida()
        {
            return this.medida;
        }
    }
}