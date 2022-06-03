using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace Exercicios.Models
{
    [Table("Localizacao")]
    public class LocalizacaoModel
    {
        [Key]
        [Column("Id")]
        [Display(Name = "ID de Localização")]
        public int Id { get; set; }
        [MaxLength(200, ErrorMessage = "Erro na desrição com quantiade de caracteres")]
        [Display(Name = "Logradouro")]
        public string Logradouro { get; set; }
        [MaxLength(100, ErrorMessage = "Erro na desrição com quantiade de caracteres")]
        [Display(Name = "Bairro")]
        public string Bairro { get; set; }
        [MaxLength(100, ErrorMessage = "Erro na desrição com quantiade de caracteres")]
        [Display(Name = "Cidade")]
        public string Cidade { get; set; }
        [MaxLength(100, ErrorMessage = "Erro na desrição com quantiade de caracteres")]
        [Display(Name = "UF")] public string UF { get; set; }
        public DateTime DataCadastro { get; set; }
        public bool Ativo { get; set; }
        [ForeignKey("Empresa-Id")]
        public EmpresaModel EmpresaId { get; set; }
    }
}
