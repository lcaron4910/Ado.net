using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MySql.Data.MySqlClient;

namespace JoyeuxAnniversaire
{
    static class ConnectionJoyeuxAnniversaire
    {

        static private MySqlConnection cnx;
        static private string sConnection;

        static ConnectionJoyeuxAnniversaire()
        {
            ConnectionJoyeuxAnniversaire.sConnection = "HOST=localhost; DATABASE=anniversaire; USER=root; PASSWORD=siojjr";
            ConnectionJoyeuxAnniversaire.cnx = new MySqlConnection(sConnection);
        }

        static public MySqlConnection GetConnection(){
            return ConnectionJoyeuxAnniversaire.cnx;
        }
    }
}
