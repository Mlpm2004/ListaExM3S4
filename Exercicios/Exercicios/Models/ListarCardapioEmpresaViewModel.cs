using System.ComponentModel;
namespace Exercicios.Models
{
    public class ListarCardapioEmpresaViewModel
    {
        public ListarCardapioEmpresaViewModel()
        {
            DescricaoEmpresa = String.Empty;   
            
        }
        [DisplayName("Id do Cardápio")]
        public int Id { get; set; }

        [DisplayName("Descrição")]
        public string Descricao { get; set; }
        [DisplayName("Valor")]
        public decimal Valor { get; set; }
        [DisplayName("Ativo")]
        public bool Ativo { get; set; }
        [DisplayName("Data de Cadastro")]
        public DateTime DataCadastro { get; set; } 
        [DisplayName("Empresa Id")]
        public int EmpresaId { get; set; }

        [DisplayName("Descrição")]
        public string DescricaoEmpresa { get; set; }

        public static implicit operator CardapioModel(ListarCardapioEmpresaViewModel model)
        {
            CardapioModel cardapioModel = new()
            {
                Id = model.Id,
                Descricao = model.Descricao,
                DataCadastro = model.DataCadastro,
                Valor = model.Valor,
                Ativo = model.Ativo,
                EmpresaId = new EmpresaModel { Id = model.EmpresaId }
              

            };

            return cardapioModel;
        }
      
    }
}
