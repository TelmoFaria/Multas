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
        private MultasDb db = new MultasDb();

        // GET: Agentes
        public ActionResult Index()
        {
            //db.Agentes.ToList() -> em sql : SELECT * FROM Agentes;
            //enviar pa view uma lista de todos os agentes
            //obter a lista de todos os agentes
            //em SQL slect * from agentes order by nome
            var listaDeAgentes = db.Agentes.ToList().OrderBy(a=>a.Nome);
            return View(listaDeAgentes);
        }

        // GET: Agentes/Details/5
        //int? id = id pode ser nulo ou seja pode-se nao fornecer o valor do ID e nao da erro
        public ActionResult Details(int? id)
        {
            //protecao para o caso de nao ter sido fornecido um ID valido
            if (id == null)
            {
                //instrução original
                //devolve erro qd nao ha ID
                //logo, não é possivel pesquisar por um agente
                // return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

                //redirecionar para uma pagina que nos controlamos
                return RedirectToAction("Index");
            }
            //procura na bd o agente cujo id foi fornecido
            Agentes agentes = db.Agentes.Find(id);
            if (agentes == null)
            {
                //O AGENTE NAO FOI ENCONTRADO LOGO GERA-SE UMA MENSAGEM DE ERRO 
                //return HttpNotFound();
                return RedirectToAction("Index");
            }
            //entrega a view os dados do agente encontrado
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
        //anotador para proteçao por roubo de identidade
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Nome,Esquadra")] Agentes agente, HttpPostedFileBase uploadFotografia){
            //escrever os dados de um novo agente na BD

            //especificar o id do novo agente
            //testar se ha registos na tabela dos agentes
            //if (db.Agentes.Count() != 0){}
            //ou entao usar a instruçao Try Catch
            int id = 0;
            try { id = db.Agentes.Max(a => a.ID) + 1; }
            catch (Exception) {
                id = 1;
            }


            //guardar o id do novo agente
            agente.ID = id;
            //especificar o nome do ficheiro
            string nomeImagem = "Agente_"+id+".jpg";
            //var. auxiliar
            string path = "";
            //validar se imagem foi fornecida
            if(uploadFotografia != null){
                //o ficheiro foi fornecido
                //validar se o ficheiro que foi fornecido é imagem
                //formatar o tamanho da imagem

                //criar o caminho completo ate ao sitio onde o ficheiro sera guardado
                path = Path.Combine(Server.MapPath("~/imagens/"), nomeImagem);

                //guardar o nome do ficheiro na BD
                agente.Fotografia = nomeImagem;
            }
            else{
                //nao foi fornecido qq ficheiro
                ModelState.AddModelError("", "Não foi fornecida uma imagem...");
                //devolver o controlo a view
                return View(agente);
            }
            //guardar o nome escolhido na BD
            //escrever um file com a foto
            //no disco rigido na pasta 'imagens'
            if (ModelState.IsValid){
                try
                {
                    db.Agentes.Add(agente);
                    //faz commit as alteracoes
                    db.SaveChanges();
                    //escrever o ficheiro com a foto no disco rigido, na pasta
                    uploadFotografia.SaveAs(path);
                    //se tudo correr bem volta ao index
                    return RedirectToAction("Index");
                }
                catch (Exception)
                {

                    ModelState.AddModelError("", "Houve um erro na criação do novo agente...");
                }
                //adiciona um novo agente a bd
                
            }

            return View(agente);
        }

        // GET: Agentes/Edit/5
        public ActionResult Edit(int? id)
        {
            //protecao para o caso de nao ter sido fornecido um ID valido
            if (id == null)
            {
                //instrução original
                //devolve erro qd nao ha ID
                //logo, não é possivel pesquisar por um agente
                // return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

                //redirecionar para uma pagina que nos controlamos
                return RedirectToAction("Index");
            }
            //procura na bd o agente cujo id foi fornecido
            Agentes agentes = db.Agentes.Find(id);
            if (agentes == null)
            {
                //O AGENTE NAO FOI ENCONTRADO LOGO GERA-SE UMA MENSAGEM DE ERRO 
                //return HttpNotFound();
                return RedirectToAction("Index");
            }
            //entrega a view os dados do agente encontrado
            return View(agentes);
        }

        // POST: Agentes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Nome,Esquadra,Fotografia")] Agentes agentes)
        {
            if (ModelState.IsValid)
            {
                db.Entry(agentes).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(agentes);
        }

        // GET: Agentes/Delete/5
        /// <summary>
        /// apresenta na view os dados de um agente com vista à sua eventual, eliminaçao
        /// </summary>
        /// <param name="id">identificador do agente a apagar</param>
        /// <returns></returns>
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("Index");
            }
            Agentes agentes = db.Agentes.Find(id);
            if (agentes == null)
            {
                //o Agente nao existe
                //Redirecionar para a pagina inicial    
                return RedirectToAction("Index");
            }
            return View(agentes);
        }

        // POST: Agentes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id){
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
