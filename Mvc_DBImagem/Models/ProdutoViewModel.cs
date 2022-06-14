using System;
using System.ComponentModel.DataAnnotations;
using System.Web;

namespace Mvc_DBImagem.Models
{
    public class ProdutoViewModel
    {
        public Int32 ProdutoId { get; set; }

        [Required(ErrorMessage = "O nome do produto é obrigatório", AllowEmptyStrings = false)]
        [Display(Name = "Nome do Produto")]
        public string Nome { get; set; }

        [Required(ErrorMessage = "A descrição do produto é obrigatória", AllowEmptyStrings = false)]
        [Display(Name = "Descrição do Produto")]
        public string Descricao { get; set; }

        [Required(ErrorMessage = "Informe o preço do produto", AllowEmptyStrings = false)]
        [DisplayFormat(DataFormatString = "{0:C2}", ApplyFormatInEditMode = true)]
        [Display(Name = "Preço")]
        public decimal Preco { get; set; }

        [Required(ErrorMessage = "Informe o estoque do produto")]
        public int Estoque { get; set; }

        [Required(ErrorMessage = "Selecione uma categoria", AllowEmptyStrings = false)]
        [Display(Name = "Categoria")]
        public int CategoriaId { get; set; }

        [Required]
        [DataType(DataType.Upload)]
        [Display(Name = "Imagem")]
        public HttpPostedFileBase ImageUpload { get; set; }
    }
}