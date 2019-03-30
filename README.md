# Ado.net #

Présentation du projet: le but est de faire une connection à la base de donnée , Exécuter des requêtes de base puis remplir un DataSet a partir de la requête et répercuter les changements dans la base de données (en mode déconnecté) et pour finir lire de manière séquentielle les lignes d'une table (en mode connecté).

le développement  tourne autour de 3 grandes parties:

1. La connexion
2. La commande
3. Exécution de la commande
4. Exploitation du résultat de la commande
5. Fermeture de la connexion

Les outils mis en oeuvre :

* git.
* Visual studio.
* Sql

## connection ##
```cs
// chaîne de caractères de connexion
string sCnx = // chaîne de caractères de connexion
string sCnx =
"server=localhost;uid=root;database=tennis;port=3306;pwd=igjjr";
//création d'un objet connexion
MySqlConnection Cnx = new MySqlConnection(sCnx);
//ouverture de la connexion
try {
Cnx.Open();
Console.WriteLine("connexion réussie");
}
catch (Exception e) {
Console.WriteLine("erreur connexion " + e.Message.ToString());
}
```

## COMMANDE ##
```cs
/* déclarer la requête */
string requete = "select * from joueur";
/* créer la commande */
MySqlCommand Cmd = new
MySqlCommand(requete, Cnx);
```
## Execution de le requête ##
```cs
//création d'un data reader pour executer la requête
//et obtenir le jeu d'enregistrements
string req = "select * from joueur";
MySqlCommand Cmd = new
MySqlCommand(requete, Cnx);
MySqlDataReader Rdr ;
Rdr = Cmd.ExecuteReader();
// requêtes de mise à jour
req = "delete from joueur where NumLicence = 1";
Cmd.ExecuteNonQuery();
//requêtes ne renvoyant qu’un seul résultat
req = " select count(*) from joueur ";
Cmd.ExecuteScalar();
```


## Parcourir un jeu d'enregistrements ##
```cs
/* parcourir le jeu d'enregistrements, affichage de la 2ème et 3ème colonnes de la table joueur */
Console.WriteLine();
while (Rdr.Read()) {
// avec le numéro de la colonne
Console.WriteLine(Rdr.GetString(1) + " " +
Rdr.GetString(2));
// avec le nom de la colonne
Console.WriteLine(Rdr[« NomJoueur"].ToString() + " " + Rdr[« Prenom"].ToString()); }
```
## Fermeture du reader et connection ##
```cs
// fermeture du reader
Rdr.Close();
/* fermer la connexion */
Cnx.Close();
```


## Utilisation de paramètre dans une requête ##
```cs
sCmd = "INSERT INTO joueur (NumLicence,NomJoueur, Prenom) VALUES (@numL,@nomJ, @prenom)";
SqlConnection Cnx = new SqlConnection(sCnx);
Cnx.Open();
SqlCommand Cmd = new SqlCommand();
Cmd.Connection = Cnx;
Cmd.CommandText = sCmd;
Cmd.Parameters.Add("@numL");
Cmd.Parameters.Add("@nomJ");
Cmd.Parameters["@numL"]=11;
Cmd.Parameters["@nomJ"]= "Nadal";
nLignesAffectées = (int)Cmd.ExecuteNonQuery();
Cnx.Close();
```

## exemple pour illustrer ##
```cs
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
```
Resultat:
![AdoNet.png](http://image.noelshack.com/fichiers/2019/13/6/1553956476-capture.png)
