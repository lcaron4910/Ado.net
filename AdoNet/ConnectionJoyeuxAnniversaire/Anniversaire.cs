using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JoyeuxAnniversaire
{
    class Anniversaire
    {
        private DateTime dateAnniversaire;
        private int id;
        private string nom;
        private string nomPseudonyme;
        private string prenom;
        private string prenomPseudonyme;

        public Anniversaire(int id, DateTime dateAnniversaire, string prenomPseudonyme, string nomPseudonyme, string prenom, string nom)
        {
            this.id=id;
            this.dateAnniversaire = dateAnniversaire;
            this.prenomPseudonyme = prenomPseudonyme;
            this.nomPseudonyme = nomPseudonyme;
            this.prenom = prenom;
            this.nom = nom;
        }

        public Anniversaire(DateTime dateAnniversaire, string prenomPseudonyme, string nomPseudonyme, string prenom, string nom)
        {
            this.dateAnniversaire = dateAnniversaire;
            this.prenomPseudonyme = prenomPseudonyme;
            this.nomPseudonyme = nomPseudonyme;
            this.prenom = prenom;
            this.nom = nom;
        }

        public string ToString()
        {
            return string.Format("ID: {0}. Date Anniversaire: {1}. Prenom Pseudonyme: {2}. Nom Pseudonyme: {3}. Prenom: {4}. Nom: {5}. ", this.id, this.dateAnniversaire, this.prenomPseudonyme, this.nomPseudonyme, this.prenom, this.nom);
        }

        public int Id
        {
            get
            {
                return this.id;
            }
        }

        public DateTime DateAnniversaire 
        {
            get
            {
                return this.dateAnniversaire;
            }
        }

        public string Nom
        {
            get
            {
                return this.nom;
            }
        }

        public string Prenom
        {
            get
            {
                return this.prenom;
            }
        }

        public string PrenomPseudonyme
        {
            get
            {
                return this.prenomPseudonyme;
            }
        }

        public string NomPseudonyme
        {
            get
            {
                return this.nomPseudonyme;
            }
        }

        
    }
}
