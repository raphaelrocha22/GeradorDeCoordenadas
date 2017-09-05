using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Device.Location;
using System.Net;
using System.Xml.Linq;

namespace Gerador_de_Coordenadas
{
    class Program
    {
        static void Main(string[] args)
        {
            var p = new Program();
            p.ObterCoordenadas();
        }

        public void ObterCoordenadas()
        {
            var rep = new Repositorio();
            List<ClienteEntidade> lista = rep.ObterEndereco();
            int contador = 0, acerto = 0, naoEncontrado = 0, erro = 0, total = lista.Count;

            foreach (var item in lista)
            {
                contador++;

                try
                {
                    var c = new ClienteEntidade();
                    c.Cod_Cliente = item.Cod_Cliente;

                    var requestUri = string.Format("https://maps.googleapis.com/maps/api/geocode/xml?address={0}&key=API", Uri.EscapeDataString(item.EnderecoCompleto));

                    var request = WebRequest.Create(requestUri);
                    var response = request.GetResponse();
                    var xdoc = XDocument.Load(response.GetResponseStream());

                    string status = xdoc.Element("GeocodeResponse").Element("status").Value;

                    if (status == "OK")
                    {
                        var result = xdoc.Element("GeocodeResponse").Element("result");
                        var locationElement = result.Element("geometry").Element("location");
                        c.Latitude = locationElement.Element("lat").Value;
                        c.Longitude = locationElement.Element("lng").Value;
                        
                        rep.AtualizarTabela(c);
                        acerto++;
                    }
                    else
                    {
                        naoEncontrado++;
                    }
                                                            
                    Console.WriteLine($"{contador} de {total} / Cod_Cliente: {c.Cod_Cliente} / Latitude: {c.Latitude} / Longitude: {c.Longitude}");

                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                    erro++;
                    continue;
                }
            }

            Console.WriteLine("-----FIM-----");
            Console.WriteLine($"Acertos: {acerto} / Não Encontrado: {naoEncontrado} / Erros: {erro} / Total: {total}");
            Console.ReadKey();
        }
    }
}
