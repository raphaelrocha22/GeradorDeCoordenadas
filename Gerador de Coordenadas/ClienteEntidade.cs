using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gerador_de_Coordenadas
{
    public class ClienteEntidade
    {
        public int Cod_Cliente { get; set; }
        public string CEP { get; set; }
        public string EnderecoCompleto { get; set; }
        public string Latitude { get; set; }
        public string Longitude { get; set; }
    }
}
