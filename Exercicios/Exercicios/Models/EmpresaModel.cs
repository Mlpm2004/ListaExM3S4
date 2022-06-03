using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace Exercicios.Models
{
    [Table("Empresa")]
    public class EmpresaModel
    {
        [Key]
        [Column("Id")]
        [Display(Name = "ID da empresa")]
        public int Id { get; set; }


        [MaxLength(50, ErrorMessage = "Erro na desrição com quantiade de caracteres")]
        [Display(Name = "Descrição da empresa")]
        public string Descricao { get; set; }

        public DateTime DataCadastro { get; set; }
        public bool Ativo { get; set; }
    }
}
