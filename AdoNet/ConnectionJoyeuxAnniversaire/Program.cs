using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MySql.Data.MySqlClient;

namespace JoyeuxAnniversaire
{
    class Program
    {
        static void Main(string[] args)
        {
            TableAnniversaire ta = new TableAnniversaire();
            int comptage = ta.CompteOccurences();
            
            /*Console.WriteLine("Nombre d'occurence: {0}", comptage);
            Console.Write("Quelle année? ");
            int annee = Convert.ToInt32(Console.ReadLine());
            int comptageAnnee = ta.CompteOccurences(1912);
            Console.WriteLine("Occurences pour l'année {0}: {1}.", annee, comptageAnnee);

            Console.Write("Veuillez écrire les 3 premières lettres d'un nom: ");
            string lettreNom = Convert.ToString(Console.ReadLine());
            List<Anniversaire> anniv = new List<Anniversaire>(ta.LeNomPatronymiqueCommencePar(lettreNom));
            foreach (Anniversaire a in anniv)
            {
                Console.WriteLine(a.ToString());
            }

            Console.Write("Quel est le jour de naissance ?: ");
            int jour = Convert.ToInt32(Console.ReadLine());
            Console.Write("Quel est le jour de naissance ?: ");
            int mois = Convert.ToInt32(Console.ReadLine());
            List<Anniversaire> anniv2 = new List<Anniversaire>(ta.QuiEstNeCeJour(jour, mois));
            foreach (Anniversaire a in anniv2)
            {
                Console.WriteLine(a.ToString());
            }

            Console.Write("Quel âge ?: ");
            int age = Convert.ToInt32(Console.ReadLine());
            List<Anniversaire> anniv3 = new List<Anniversaire>(ta.PlusAgesQue(age));
            foreach (Anniversaire a in anniv3)
            {
                Console.WriteLine(a.ToString());
            }

            Console.WriteLine("INSERT ANNIVERSAIRE");
            Console.Write("Date Anniversaire: ");
            DateTime DtAnniv = Convert.ToDateTime(Console.ReadLine());
            Console.Write("Prénom ?: ");
            string prenom = Convert.ToString(Console.ReadLine());
            Console.Write("Nom ?: ");
            string nom = Convert.ToString(Console.ReadLine());
            Console.Write("Prénom pseudonyme ?: ");
            string prenomPseudonyme = Convert.ToString(Console.ReadLine());
            Console.Write("Nom pseudonyme ?: ");
            string nomPseudonyme = Convert.ToString(Console.ReadLine());
            Anniversaire annivFinal = new Anniversaire(DtAnniv, prenomPseudonyme, nomPseudonyme, prenom, nom);
            Console.WriteLine(ta.Insert(annivFinal));

            Console.WriteLine("UPDATE ANNIVERSAIRE");
            Console.WriteLine("Id de l'occurrence: ");
            int idOccurrenceUpdate = Convert.ToInt32(Console.ReadLine());
            Console.Write("Date Anniversaire: ");
            DateTime DtAnnivUpdate = Convert.ToDateTime(Console.ReadLine());
            Console.Write("Prénom ?: ");
            string prenomUpdate = Convert.ToString(Console.ReadLine());
            Console.Write("Nom ?: ");
            string nomUpdate = Convert.ToString(Console.ReadLine());
            Console.Write("Prénom pseudonyme ?: ");
            string prenomPseudonymeUpdate = Convert.ToString(Console.ReadLine());
            Console.Write("Nom pseudonyme ?: ");
            string nomPseudonymeUpdate = Convert.ToString(Console.ReadLine());
            Anniversaire annivFinalUpdate = new Anniversaire(idOccurrenceUpdate, DtAnnivUpdate, prenomPseudonymeUpdate, nomPseudonymeUpdate, prenomUpdate, nomUpdate);
            ta.Update(annivFinalUpdate);

            Console.WriteLine("DELETE ANNIVERSAIRE");
            Console.Write("Id de l'occurrence à supprimer: ");
            int idDelete = Convert.ToInt32(Console.ReadLine());
            ta.Delete(idDelete);*/

            Console.WriteLine("REPARTITION ANNIVERSAIRE (par mois): ");
            List<string> repartitionMois = new List<string>(ta.RepartitionAnniversaireParMois());
            foreach (string a in repartitionMois)
            {
                Console.WriteLine(a.ToString());
            }

            Console.WriteLine("REPARTITION ANNIVERSAIRE (par année): ");
            List<string> repartitionAnnee = new List<string>(ta.RepartitionAnniversaireParAnnee());
            foreach (string a in repartitionAnnee)
            {
                Console.WriteLine(a.ToString());
            }

            Console.ReadLine();
        }
    }
}
