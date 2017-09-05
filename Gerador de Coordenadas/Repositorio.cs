using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace Gerador_de_Coordenadas
{
    public class Repositorio:Conexao
    {
        public List<ClienteEntidade> ObterEndereco()
        {
            AbrirConexao();

            string query = "select cod_cliente, endereco, numero, bairro, cidade, uf, cep from DIM_CLIENTE where LATITUDE is null and ENDERECO is not null";
            cmd = new SqlCommand(query, con);
            dr = cmd.ExecuteReader();

            var lista = new List<ClienteEntidade>();

            while (dr.Read())
            {
                var c = new ClienteEntidade();
                c.Cod_Cliente = (int)dr["COD_CLIENTE"];
                string endereco = dr["Endereco"].ToString();
                string numero = dr["Numero"].ToString();
                string bairro = dr["Bairro"].ToString();
                string cidade = dr["Cidade"].ToString();
                string UF = dr["UF"].ToString();
                string CEP = dr["CEP"].ToString();

                c.EnderecoCompleto = $"{endereco}, {numero} - {bairro}, {cidade} - {UF}, {CEP}";

                lista.Add(c);
            }

            FecharConexao();
            return lista;
        }

        public void AtualizarTabela(ClienteEntidade c)
        {
            AbrirConexao();

            string query = "update dim_cliente set Latitude = @Latitude, Longitude = @Longitude " +
                "where Cod_Cliente = @Cod_Cliente";

            cmd = new SqlCommand(query, con);
            cmd.Parameters.AddWithValue("@Latitude", c.Latitude);
            cmd.Parameters.AddWithValue("@Longitude", c.Longitude);
            cmd.Parameters.AddWithValue("@Cod_Cliente", c.Cod_Cliente);
            cmd.ExecuteNonQuery();

            FecharConexao();
        }
    }
}
