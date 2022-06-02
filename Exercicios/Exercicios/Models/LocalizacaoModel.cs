using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace Exercicios.Models
{
    [Table("Localizacao")]
    public class LocalizacaoModel
    {
        [Column("Id")]
        public int Id { get; set; }
        [MaxLength(200)]
        public string Logradouro { get; set; }
        [MaxLength(100)]
        public string Bairro { get; set; }
        [MaxLength(100)]
        public string Cidade { get; set; }
        [MaxLength(100)]
        public string UF { get; set; }
        public DateTime DataCadastro { get; set; }
        public bool Ativo { get; set; }
        [ForeignKey("FK_Localizacao_Empresa")]
        public EmpresaModel Id_Empresa { get; set; }
    }
}
