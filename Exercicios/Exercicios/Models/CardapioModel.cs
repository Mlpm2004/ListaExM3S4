using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace Exercicios.Models
{
    [Table("Cardapio")]
    public class CardapioModel
    {
        [Key]
        [Column("Id")]
        [Display(Name = "ID do Cardápio")]
        public int Id { get; set; }
        [MaxLength(50, ErrorMessage = "Erro na desrição com quantiade de caracteres")]
        [Display(Name = "ID do Cardápio")]
        public string Descricao { get; set; }
        [Display(Name = "Valor do Cardápio")]
        public decimal Valor{ get; set; }
        public DateTime DataCadastro { get; set; }
        public bool Ativo { get; set; }
        [ForeignKey("EmpresaId")]
        public EmpresaModel Empresa { get; set; }
    }
}
