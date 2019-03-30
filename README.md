# Ado.net

Présentation du projet: le but est de faire une connection à la base de donnée , Exécuter des requêtes de base puis remplir un DataSet a partir de la requête et répercuter les changements dans la base de données (en mode déconnecté) et pour finir lire de manière séquentielle les lignes d'une table(en mode connecté).

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
