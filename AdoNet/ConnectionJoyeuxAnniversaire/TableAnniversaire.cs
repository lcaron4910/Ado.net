using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MySql.Data.MySqlClient;

namespace JoyeuxAnniversaire
{
    class TableAnniversaire
    {
        private MySqlConnection cnx;
        private MySqlCommand selectAnniversaire;

        public TableAnniversaire()
        {
            this.cnx = ConnectionJoyeuxAnniversaire.GetConnection();
        }

        public int CompteOccurences()
        {
            int comptage = 0;
            this.cnx.Open();
            selectAnniversaire = new MySqlCommand("Select COUNT(*) from anniversaire", this.cnx);
            comptage = Convert.ToInt32(selectAnniversaire.ExecuteScalar());
            this.cnx.Close();
            return comptage;
        }

        public int CompteOccurences(int annee)
        {
            int comptage = 0;
            this.cnx.Open();
            MySqlCommand cmdSql = new MySqlCommand("Select COUNT(*) from anniversaire where YEAR(dateAnniversaire)=@annee", this.cnx);
            cmdSql.Parameters.Add(new MySqlParameter("@annee", MySqlDbType.Int32));
            cmdSql.Parameters["@annee"].Value = annee;
            comptage = Convert.ToInt32(cmdSql.ExecuteScalar());
            this.cnx.Close();
            return comptage;
        }

        public void Delete(int id)
        {
            this.cnx.Open();
            MySqlCommand cmdSql = new MySqlCommand("delete from anniversaire where id=@id;", this.cnx);

            cmdSql.Parameters.Add(new MySqlParameter("@id", MySqlDbType.Int32));
            cmdSql.Parameters["@id"].Value = id;

            cmdSql.ExecuteNonQuery();
            this.cnx.Close();
        }

        public int Insert(Anniversaire nouvelAnniversaire)
        {
            DateTime dateAnniv = nouvelAnniversaire.DateAnniversaire;
            string prenom = nouvelAnniversaire.Prenom;
            string nom = nouvelAnniversaire.Nom;
            string prenomPseudonyme = nouvelAnniversaire.PrenomPseudonyme;
            string nomPseudonyme = nouvelAnniversaire.NomPseudonyme;
            string sdate = dateAnniv.ToString("yyyy-MM-dd HH:mm:ss.fff");

            this.cnx.Open();
            MySqlCommand cmdSql = new MySqlCommand("Insert into Anniversaire(dateAnniversaire, nomPseudonyme, prenomPseudonyme, nom, prenom) values (@sdate, @nomPseudonyme, @prenomPseudonyme, @nom, @prenom)", this.cnx);

            cmdSql.Parameters.Add(new MySqlParameter("@prenom", MySqlDbType.String));
            cmdSql.Parameters["@prenom"].Value = prenom;
            cmdSql.Parameters.Add(new MySqlParameter("@nom", MySqlDbType.String));
            cmdSql.Parameters["@nom"].Value = nom;
            cmdSql.Parameters.Add(new MySqlParameter("@prenomPseudonyme", MySqlDbType.String));
            cmdSql.Parameters["@prenomPseudonyme"].Value = prenomPseudonyme;
            cmdSql.Parameters.Add(new MySqlParameter("@nomPseudonyme", MySqlDbType.String));
            cmdSql.Parameters["@nomPseudonyme"].Value = nomPseudonyme;
            cmdSql.Parameters.Add(new MySqlParameter("@sdate", MySqlDbType.String));
            cmdSql.Parameters["@sdate"].Value = sdate;

            MySqlDataReader reader = cmdSql.ExecuteReader();
            long longLastId = cmdSql.LastInsertedId;
            int LastId = Convert.ToInt32(longLastId);
            this.cnx.Close();
            return LastId;
        }

        public List<Anniversaire> LeNomPatronymiqueCommencePar(string debutNom)
        {
            List<Anniversaire> annivList = new List<Anniversaire>();
            this.cnx.Open();
            MySqlCommand cmdSql = new MySqlCommand("select Year(dateAnniversaire), month(dateAnniversaire), day(dateAnniversaire), id, prenomPseudonyme, nomPseudonyme, prenom, nom from anniversaire where nom like @lettreNom", this.cnx);
            cmdSql.Parameters.Add(new MySqlParameter("@lettreNom", MySqlDbType.String));
            cmdSql.Parameters["@lettreNom"].Value = debutNom + "%";
            MySqlDataReader reader = cmdSql.ExecuteReader();
            while(reader.Read()){
                DateTime dateAnniv = new DateTime(Convert.ToInt32(reader.GetValue(0)),Convert.ToInt32(reader.GetValue(1)),Convert.ToInt32(reader.GetValue(2)));
                Anniversaire anniv = new Anniversaire(Convert.ToInt32(reader.GetValue(3)), dateAnniv, Convert.ToString(reader.GetValue(4)), Convert.ToString(reader.GetValue(5)), Convert.ToString(reader.GetValue(6)), Convert.ToString(reader.GetValue(7)));
                annivList.Add(anniv);
            }
            this.cnx.Close();
            return annivList;
        }

        public List<Anniversaire> PlusAgesQue(int age)
        {
            List<Anniversaire> annivList = new List<Anniversaire>();
            this.cnx.Open();
            age = DateTime.Now.Year - age;

            MySqlCommand cmdSql = new MySqlCommand("select Year(dateAnniversaire), month(dateAnniversaire), day(dateAnniversaire), id, prenomPseudonyme, nomPseudonyme, prenom, nom from anniversaire where YEAR(dateAnniversaire)<@age", this.cnx);
            cmdSql.Parameters.Add(new MySqlParameter("@age", MySqlDbType.Int32));
            cmdSql.Parameters["@age"].Value = age;
            MySqlDataReader reader = cmdSql.ExecuteReader();
            while (reader.Read())
            {
                DateTime dateAnniv = new DateTime(Convert.ToInt32(reader.GetValue(0)), Convert.ToInt32(reader.GetValue(1)), Convert.ToInt32(reader.GetValue(2)));
                Anniversaire anniv = new Anniversaire(Convert.ToInt32(reader.GetValue(3)), dateAnniv, Convert.ToString(reader.GetValue(4)), Convert.ToString(reader.GetValue(5)), Convert.ToString(reader.GetValue(6)), Convert.ToString(reader.GetValue(7)));
                annivList.Add(anniv);
            }
            this.cnx.Close();
            return annivList;
        }

        public List<Anniversaire> QuiEstNeCeJour(int jour, int mois)
        {
            List<Anniversaire> annivList = new List<Anniversaire>();
            this.cnx.Open();
            MySqlCommand cmdSql = new MySqlCommand("select Year(dateAnniversaire), month(dateAnniversaire), day(dateAnniversaire), id, prenomPseudonyme, nomPseudonyme, prenom, nom from anniversaire where day(dateAnniversaire)=@jour and month(dateAnniversaire)=@mois", this.cnx);
            cmdSql.Parameters.Add(new MySqlParameter("@jour", MySqlDbType.Int32));
            cmdSql.Parameters["@jour"].Value = jour;
            cmdSql.Parameters.Add(new MySqlParameter("@mois", MySqlDbType.Int32));
            cmdSql.Parameters["@mois"].Value = mois;
            MySqlDataReader reader = cmdSql.ExecuteReader();
            while (reader.Read())
            {
                DateTime dateAnniv = new DateTime(Convert.ToInt32(reader.GetValue(0)), Convert.ToInt32(reader.GetValue(1)), Convert.ToInt32(reader.GetValue(2)));
                Anniversaire anniv = new Anniversaire(Convert.ToInt32(reader.GetValue(3)), dateAnniv, Convert.ToString(reader.GetValue(4)), Convert.ToString(reader.GetValue(5)), Convert.ToString(reader.GetValue(6)), Convert.ToString(reader.GetValue(7)));
                annivList.Add(anniv);
            }
            this.cnx.Close();
            return annivList;
        }

        public List<string> RepartitionAnniversaireParMois()
        {
            List<string> repartition = new List<string>();
            
            MySqlCommand cmdSql1 = new MySqlCommand("select Count(DateAnniversaire) as total from anniversaire", this.cnx);
            this.cnx.Open();
            MySqlDataReader reader1 = cmdSql1.ExecuteReader();
            reader1.Read();
            int total =Convert.ToInt32(reader1["total"]);
            this.cnx.Close();
            MySqlCommand cmdSql2 = new MySqlCommand("select Count(DateAnniversaire), month(dateAnniversaire) from anniversaire group by month(dateAnniversaire)", this.cnx);
            this.cnx.Open();
            MySqlDataReader reader2 = cmdSql2.ExecuteReader();
            while (reader2.Read())
            {
                double calcul = (Convert.ToDouble(reader2.GetValue(0)) / total) * 100;
                string contenu = Convert.ToString(reader2.GetValue(1))+ " - " + Convert.ToString(calcul) + "%";
                repartition.Add(contenu);
            }
            string fin = "Total - 100,00%";
            repartition.Add(fin);
            this.cnx.Close();
            return repartition;
        }

        public List<string> RepartitionAnniversaireParAnnee()
        {
            List<string> repartition = new List<string>();

            MySqlCommand cmdSql1 = new MySqlCommand("select Count(DateAnniversaire) as total from anniversaire", this.cnx);
            this.cnx.Open();
            MySqlDataReader reader1 = cmdSql1.ExecuteReader();
            reader1.Read();
            int total = Convert.ToInt32(reader1["total"]);
            cnx.Close();
            MySqlCommand cmdSql2 = new MySqlCommand("select Count(DateAnniversaire), YEAR(dateAnniversaire) from anniversaire group by YEAR(dateAnniversaire)", this.cnx);
            cnx.Open();
            MySqlDataReader reader2 = cmdSql2.ExecuteReader();
            while (reader2.Read())
            {
                double calcul = (Convert.ToDouble(reader2.GetValue(0)) / total) * 100;
                string contenu = Convert.ToString(reader2.GetValue(1)) + " - " + Convert.ToString(calcul) + "%";
                repartition.Add(contenu);
            }
            string fin = "Total - 100,00%";
            repartition.Add(fin);
            this.cnx.Close();
            return repartition;
        }

        public void Update(Anniversaire unAnniversaire)
        {
            int idOccurence = unAnniversaire.Id;
            DateTime dateAnniv = unAnniversaire.DateAnniversaire;
            string prenom = unAnniversaire.Prenom;
            string nom = unAnniversaire.Nom;
            string prenomPseudonyme = unAnniversaire.PrenomPseudonyme;
            string nomPseudonyme = unAnniversaire.NomPseudonyme;
            string sdate = dateAnniv.ToString("yyyy-MM-dd HH:mm:ss.fff");

            this.cnx.Open();
            MySqlCommand cmdSql = new MySqlCommand("Update anniversaire set dateAnniversaire=@sdate, nomPseudonyme=@nomPseudonyme, prenomPseudonyme=@prenomPseudonyme, nom=@nom, prenom=@prenom where id=@idOccurrence", this.cnx);

            cmdSql.Parameters.Add(new MySqlParameter("@idOccurrence", MySqlDbType.Int32));
            cmdSql.Parameters["@idOccurrence"].Value = idOccurence;
            cmdSql.Parameters.Add(new MySqlParameter("@prenom", MySqlDbType.String));
            cmdSql.Parameters["@prenom"].Value = prenom;
            cmdSql.Parameters.Add(new MySqlParameter("@nom", MySqlDbType.String));
            cmdSql.Parameters["@nom"].Value = nom;
            cmdSql.Parameters.Add(new MySqlParameter("@prenomPseudonyme", MySqlDbType.String));
            cmdSql.Parameters["@prenomPseudonyme"].Value = prenomPseudonyme;
            cmdSql.Parameters.Add(new MySqlParameter("@nomPseudonyme", MySqlDbType.String));
            cmdSql.Parameters["@nomPseudonyme"].Value = nomPseudonyme;
            cmdSql.Parameters.Add(new MySqlParameter("@sdate", MySqlDbType.String));
            cmdSql.Parameters["@sdate"].Value = sdate;

            cmdSql.ExecuteNonQuery();
            this.cnx.Close();
        }
    }
}
