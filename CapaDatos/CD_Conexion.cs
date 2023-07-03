using System.Data.SqlClient;

namespace CapaDatos
{
    public class CD_Conexion
    {
        private static readonly CD_Conexion instancia = new CD_Conexion();
        private SqlConnection conexion;

        private CD_Conexion()
        {
            // Configurar la cadena de conexión a la base de datos
            string connectionString = "Server=DESKTOP-O5L8NSD\\IHECAST;Database=puntoVenta;Integrated Security=true";
            conexion = new SqlConnection(connectionString);
        }

        public static CD_Conexion Instancia
        {
            get { return instancia; }
        }

        public SqlConnection AbrirConexion()
        {
            if (conexion.State == System.Data.ConnectionState.Closed)
            {
                conexion.Open();
            }
            return conexion;
        }

        public SqlConnection CerrarConexion()
        {
            if (conexion.State == System.Data.ConnectionState.Open)
            {
                conexion.Close();
            }
            return conexion;
        }
    }
}
