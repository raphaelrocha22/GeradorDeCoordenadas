using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace Gerador_de_Coordenadas
{
    public class Conexao
    {
        protected SqlConnection con;
        protected SqlCommand cmd;
        protected SqlDataReader dr;

        protected void AbrirConexao()
        {
            con = new SqlConnection("SuaConectionString");
            con.Open();
        }

        protected void FecharConexao()
        {
            con.Close();
        }
    }
}
