using System.ComponentModel;
namespace Exercicios.Models
{
    public class ListarLocalizacaoEmpresaViewModel
    {
        [DisplayName("Id da Localização")]
        public int Id { get; set; }

        [DisplayName("Logradouro")]
        public string Logradouro { get; set; }

        [DisplayName("Bairro")]
        public string Bairro { get; set; }

        [DisplayName("Cidade")]
        public string Cidade { get; set; }

        [DisplayName("UF")]
        public string UF { get; set; }
        [DisplayName("Empresa Id")]
        public int Empresa { get; set; }

        [DisplayName("Descrição")]
        public string DescricaoEmpresa { get; set; }

        public static implicit operator LocalizacaoModel(ListarLocalizacaoEmpresaViewModel model)
        {
            LocalizacaoModel localizacaoModel = new()
            {
                Id = model.Id,
                Logradouro = model.Logradouro,
                Cidade = model.Cidade,
                UF = model.UF,
                Ativo = true,
                Bairro = model.Bairro,
                Empresa = new EmpresaModel { Id = model.Empresa },
                
            };

            return localizacaoModel;
        }
    }
}
