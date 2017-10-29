using Capa_Dominio.Motor_Inferencia;
using DataAccessLayer.DataAccessObjects;
using Domain_Layer;
using NBC_Idiomas.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Threading;
using System.Web.Http.Cors;

namespace NBC_Idiomas.Controllers
{
   //  [EnableCors(origins: "*", headers: "*", methods: "*")] //development
    public class ClassifierController : ApiController
    {
        [HttpPost]
        [Route("api/Classifier/Idioma/")]
        public IHttpActionResult Idioma(Idioma idioma)
        {
            if (idioma != null && !string.IsNullOrEmpty(idioma.nombre))
            {
                Categoria cat = new Categoria();
                cat.setnombre(idioma.nombre);
                CategoriaDAO.getInstance().create(cat);
                idioma.id = cat.getid();
                return Ok(idioma);
            }
            return Ok();
        }

        [HttpGet]
        [Route("api/Classifier/Idioma/")]
        public IHttpActionResult Idioma()
        {
            List<Categoria> categorias = CategoriaDAO.getInstance().readALL(null);
            List<Idioma> idiomas = NBC_Idiomas.Models.Idioma.toIdiomaList(categorias);
            if (categorias != null && categorias.Any())
            {
                return Ok(idiomas);
            }
            else
            {
                return NotFound();
            }
        }
        [Route("api/Classifier/Idioma/{id}")]
        [HttpGet]
        public IHttpActionResult Idioma(int id)
        {
            if (id > 0)
            {
                Categoria cat = CategoriaDAO.getInstance().read(id);
                Idioma idioma = new Idioma(cat);
                if (idioma != null)
                {
                    return Ok(idioma);
                }
            }
            return NotFound();
        }

        [Route("api/Classifier/classify/")]
        [HttpPost]
        public IHttpActionResult classify(String txt)
        {
            Motor_Inferencia clasifier = new Motor_Inferencia();
            clasifier.process(txt);
            List<Tendencia> tendencias = clasifier.getTendencias();
            if (tendencias != null && tendencias.Any())
            {
                List<TendenciaViewData> tendencia = new List<TendenciaViewData>();
                foreach (var item in tendencias)
                {
                    tendencia.Add(new TendenciaViewData()
                    {
                        idioma = new Models.Idioma(item.categoria),
                        logit = item.Logit
                    });
                }
                return Ok(tendencia);
            }
            return Content(HttpStatusCode.OK, new { message = "No se logró generar una clasificación objetiva." });

        }

        [HttpPost]
        [Route("api/Classifier/setKnowledge/{id_idioma}")]
        public IHttpActionResult setKnowledge(String txt, int id_idioma)
        {
            Categoria cat = CategoriaDAO.getInstance().read(id_idioma);
            if (cat != null)
            {               
                new Thread(() =>
                {
                    Thread.CurrentThread.IsBackground = true;
                    new Motor_Inferencia().setKnowledge(txt, cat);
                }).Start();               
            }
            return Ok();
        }
    } 
}
