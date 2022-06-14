using Mvc_DBImagem.Models;
using System.IO;
using System.Linq;
using System.Net;
using System.Web.Mvc;

namespace Mvc_DBImagem.Controllers
{
    public class ProdutosController : Controller
    {
        ProdutoDbContext db;
        public ProdutosController()
        {
            db = new ProdutoDbContext();
        }
        // GET: Produtos
        public ActionResult Index()
        {
            var produtos = db.Produtos.ToList();
            return View(produtos);
        }

        public ActionResult Create()
        {
            ViewBag.Categorias = db.Categorias;
            var model = new ProdutoViewModel();
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(ProdutoViewModel model)
        {
            var imageTypes = new string[]{
                    "image/gif",
                    "image/jpeg",
                    "image/pjpeg",
                    "image/png"
                };
            if (model.ImageUpload == null || model.ImageUpload.ContentLength == 0)
            {
                ModelState.AddModelError("ImageUpload", "Este campo é obrigatório");
            }
            else if (!imageTypes.Contains(model.ImageUpload.ContentType))
            {
                ModelState.AddModelError("ImageUpload", "Escolha uma iamgem GIF, JPG ou PNG.");
            }

            if (ModelState.IsValid)
            {
                var produto = new Produto();
                produto.Nome = model.Nome;
                produto.Preco = model.Preco;
                produto.Estoque = model.Estoque;
                produto.Descricao = model.Descricao;
                produto.CategoriaId = model.CategoriaId;

                // Salvar a imagem para a pasta e pega o caminho
                using (var binaryReader = new BinaryReader(model.ImageUpload.InputStream))
                    produto.Imagem = binaryReader.ReadBytes(model.ImageUpload.ContentLength);

                db.Produtos.Add(produto);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            // Se ocorrer um erro retorna para pagina
            ViewBag.Categories = db.Categorias;
            return View(model);
        }

        // GET: Produtos/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Produto produto = db.Produtos.Find(id);
            if (produto == null)
            {
                return HttpNotFound();
            }

            ViewBag.Categorias = db.Categorias;
            return View(produto);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ProdutoId,Nome,Preco,CategoriaId")] Produto model)
        {
            if (ModelState.IsValid)
            {
                var produto = db.Produtos.Find(model.ProdutoId);
                if (produto == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                produto.Nome = model.Nome;
                produto.Preco = model.Preco;
                produto.CategoriaId = model.CategoriaId;
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Categorias = db.Categorias;
            return View(model);
        }

        // GET: Produtos/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Produto produto = db.Produtos.Find(id);
            if (produto == null)
            {
                return HttpNotFound();
            }

            ViewBag.Categoria = db.Categorias.Find(produto.CategoriaId).CategoriaNome;
            return View(produto);
        }

        // GET: Produtos/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            Produto produto = db.Produtos.Find(id);
            if (produto == null)
            {
                return HttpNotFound();
            }
            ViewBag.Categoria = db.Categorias.Find(produto.CategoriaId).CategoriaNome;
            return View(produto);
        }

        // POST: Produtos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Produto produto = db.Produtos.Find(id);
            db.Produtos.Remove(produto);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}