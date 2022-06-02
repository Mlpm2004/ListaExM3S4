using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace Exercicios.Models
{
    [Table("Cardapio")]
    public class CardapioModel
    {
        [Column("Id")]
        public int Id { get; set; }
        [MaxLength(50)]
        public string Descricao { get; set; }
        [MaxLength(100)]
        public decimal Valor{ get; set; }
        public DateTime DataCadastro { get; set; }
        public bool Ativo { get; set; }
        [ForeignKey("FK_Cardapio_Empresa")]
        public EmpresaModel Id_Empresa { get; set; }
    }
}
