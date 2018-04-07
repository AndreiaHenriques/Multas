using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Multas.Models;

namespace Multas.Controllers
{
    public class AgentesController : Controller
    {
        // cria um objeto privado que 'referencia' a BD
        private MultasDb db = new MultasDb();

        // GET: Agentes
        public ActionResult Index()
        {
            // (LINQ) db.Agentes.ToList() ---> em SQL: SELECT * FROM Agentes
            // lista de Agentes, presentes na BD
            return View(db.Agentes.ToList());
        }

        // GET: Agentes/Details/5
        /// <summary>
        /// Apresenta uma listagem dos dados de um Agente
        /// </summary>
        /// <param name="id">Identifica o nº do Agente a pesquisar </param>
        /// <returns></returns>
        public ActionResult Details(int? id)

            // int? id ---> O '?' informa que o parâmetro é de preenchimento facultativo
        {
            // caso não haja ID, nada é feito
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            // pesquisa os dados do Agente, cujo ID foi fornecido
            Agentes agentes = db.Agentes.Find(id);

            // valida se foi encontrado algum Agente
            if (agentes == null)
            {
                return HttpNotFound();
            }

            // apresenta na View os dados de um Agente
            return View(agentes);
        }

        // GET: Agentes/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Agentes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,Nome,Fotografia,Esquadra")] Agentes agentes)
        {
            // ModelState.IsValid ---> confronta os dados recebidos como o modelo, para verificar
            // se o que recebeu é o que deveria ter sido recebido
            if (ModelState.IsValid)
            {
                // adiciona o Agente à estrutura de dados
                db.Agentes.Add(agentes);
                // efetuam um COMMIT à BD
                db.SaveChanges();

                //redireciona o utilizador para a página do inicio
                return RedirectToAction("Index");
            }

            // se aqui cheguei, é porque alguma coisa correu mal...
            // devolvo na View os dados do Agente
            return View(agentes);
        }

        // GET: Agentes/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Agentes agentes = db.Agentes.Find(id);
            if (agentes == null)
            {
                return HttpNotFound();
            }
            return View(agentes);
        }

        // POST: Agentes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Nome,Fotografia,Esquadra")] Agentes agentes)
        {
            if (ModelState.IsValid)
            {
                // update
                db.Entry(agentes).State = EntityState.Modified;
                // COMMIT
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(agentes);
        }

        // GET: Agentes/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Agentes agentes = db.Agentes.Find(id);
            if (agentes == null)
            {
                return HttpNotFound();
            }
            return View(agentes);
        }

        // POST: Agentes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Agentes agentes = db.Agentes.Find(id);
            db.Agentes.Remove(agentes);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
