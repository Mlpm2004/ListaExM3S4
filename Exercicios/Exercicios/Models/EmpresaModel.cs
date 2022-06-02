using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace Exercicios.Models
{
    [System.ComponentModel.DataAnnotations.Schema.Table("Empresa")]
    public class EmpresaModel
    {
        [Column("Id")]
        public int Id { get; set; }
        [MaxLength(50)]
        public string Descricao { get; set; }
        public DateTime DataCadastro { get; set; }
        public bool Ativo { get; set; }
    }
}
