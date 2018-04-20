using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
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
        public ActionResult Create([Bind(Include = "ID,Nome,Fotografia,Esquadra")] Agentes agente,
                                    HttpPostedFileBase carregaFotografia) {

            // gerar o ID do novo Agente
            int novoID = 0;
            if (db.Agentes.Count() != 0) {
                novoID = db.Agentes.Max(a => a.ID) + 1;
            } else {
                novoID = 1;
            }
            agente.ID = novoID; //atribuir o ID deste Agente

            // ***************************************************************************************************
            // outra hipótese de validar a atribuição de ID
            // try { }
            // catch(Exception ) { }
            // ***************************************************************************************************

            // var auxiliar
            string nomeFicheiro = "Agente_" + novoID + ".jpg";
            string caminho = "";

            /// primeiro que tudo, há que grarantir que existe imagem
             if (carregaFotografia!=null) {
                // a imagem existe
                agente.Fotografia = nomeFicheiro;
                // definir o nome do ficheiro e o seu caminho
                caminho = Path.Combine(Server.MapPath("~/imagens/"), nomeFicheiro);
            }
             else
            {
                //não foi submetida a imagem

                // gerar mensagem de erro, para elucidar o utilizador do erro
                ModelState.AddModelError("", "Não foi inserida uma imagem.");

                // redirecionar o utilizador para a View, para que ele corrija os dados
                return View(agente);
            }

             /// O que fazer para a imagem:
            /// escolher o nome da imagem
            /// formatar o tamanho da imagem ---> fazer em casa
            /// será que o ficheiro é uma imagem? ---> fazer em casa
            /// guardar a imagem no disco rígido do servidor


            // ModelState.IsValid ---> confronta os dados recebidos como o modelo, para verificar
            // se o que recebeu é o que deveria ter sido recebido
            if (ModelState.IsValid) {
                try
                {
                    // adiciona o Agente à estrutura de dados
                    db.Agentes.Add(agente);
                    // efetuam um COMMIT à BD
                    db.SaveChanges();
                    // guardar o ficheiro no disco rígido
                    carregaFotografia.SaveAs(caminho);

                    //redireciona o utilizador para a página do inicio
                    return RedirectToAction("Index");
                }
                catch (Exception) {
                    ModelState.AddModelError("", "Ocorreu um erro na criação do Agente.'" + agente.Nome + "'.");
                }
            }

            // se aqui cheguei, é porque alguma coisa correu mal...
            // devolvo na View os dados do Agente
            return View(agente);
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
