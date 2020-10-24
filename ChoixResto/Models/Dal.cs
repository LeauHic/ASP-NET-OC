using Antlr.Runtime;
using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Linq;
using System.Web;

namespace ChoixResto.Models
{
    public class Dal : IDal
    {
        private BddContext bdd;

        public Dal()
        {
            bdd = new BddContext();
        }

        public List<Resto> ObtientTouslesRestaurants()
        {
            return bdd.Restos.ToList();
        }

        public void Dispose()
        {
            bdd.Dispose();
        }

        public void ModifierRestaurant(int id, string nom, string telephone)
        {
            Resto restoTrouve = bdd.Restos.FirstOrDefault(resto => resto.Id == id);
            if (restoTrouve != null)
            {
                restoTrouve.Nom = nom;
                restoTrouve.Telephone = telephone;
                bdd.SaveChanges();
            }
        }

        public void CreerRestaurant(string nom, string telephone)
        {
            bdd.Restos.Add(new Resto { Nom = nom, Telephone = telephone });
            bdd.SaveChanges();
        }

        public bool RestaurantExiste(string nomResto)
        {
            Resto restoTrouve = bdd.Restos.FirstOrDefault(resto => resto.Nom == nomResto);
            if(restoTrouve !=null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public void CreerUtilisateur(string prenom)
        {
            bdd.Utilisateurs.Add(new Utilisateur { Prenom = prenom});
            bdd.SaveChanges();
        }

        public Utilisateur ObtenirUtilisateur(int idUser)
        {
            Utilisateur userTrouve = bdd.Utilisateurs.FirstOrDefault(user => user.Id == idUser);
            if(userTrouve !=null)
            {
                return userTrouve;
            }
            else
            {
                return null;
            }
        }
        public Utilisateur ObtenirUtilisateur(string idUserstr)
        {
            int.TryParse(idUserstr, out int idUser);
            return ObtenirUtilisateur(idUser);
        }
        
        public int AjouterUtilisateur(string prenomUser, string MotDePasseUser)
        {
            Utilisateur newUser = new Utilisateur { Prenom = prenomUser, motDePasse = MotDePasseUser };
            bdd.Utilisateurs.Add(newUser);
            bdd.SaveChanges();
            return newUser.Id; 
        }
      
        public Utilisateur Authentifier(string prenomUser, string motDePasse)
        {
            Utilisateur userTrouve = bdd.Utilisateurs.FirstOrDefault(user => user.Prenom == prenomUser);
            if (userTrouve != null)
            {
                if(motDePasse == userTrouve.motDePasse)
                {
                    return userTrouve;
                }
            }
            return null;
        }
       
        public int CreerUnSondage()
        {
            Sondage newSondage = new Sondage {Date = DateTime.Now, Votes = new List<Vote> { } };
            bdd.Sondages.Add(newSondage);
            bdd.SaveChanges();
            return newSondage.Id;
        }
       
        public void AjouterVote(int idSondage, int vote, int idUtilisateur)
        {
            Sondage sondageExistant = bdd.Sondages.FirstOrDefault(recupSondage => recupSondage.Id == idSondage);
            
            Resto RestoVote = bdd.Restos.FirstOrDefault(resto => resto.Id == vote);
            Utilisateur UserVotant = bdd.Utilisateurs.FirstOrDefault(user => user.Id == idUtilisateur);

            Vote newvote = new Vote { Resto = RestoVote, Utilisateur = UserVotant };

            if (sondageExistant == null){sondageExistant.Votes = new List<Vote>();}
            sondageExistant.Votes.Add(newvote);
            bdd.SaveChanges();
        }
      
        public bool ADejaVote(int idSondage, string idUser)
        {
            Sondage sondageExistant = bdd.Sondages.FirstOrDefault(Sondage => Sondage.Id == idSondage);
            if(sondageExistant != null)
            {
                foreach(Vote votes in sondageExistant.Votes)
                {
                    if(votes.Utilisateur.Id == int.Parse(idUser)){return true;}
                }
            }
            return false;
        }

        public List<Resultats> ObtenirLesResultats(int idSondage)
        {
            List<Resto> restaurants = ObtientTouslesRestaurants();
            List<Resultats> resultats = new List<Resultats>();
            Sondage sondage = bdd.Sondages.First(s => s.Id == idSondage);
            foreach (IGrouping<int, Vote> grouping in sondage.Votes.GroupBy(v => v.Resto.Id))
            {
                int idRestaurant = grouping.Key;
                Resto resto = restaurants.First(r => r.Id == idRestaurant);
                int nombreDeVotes = grouping.Count();
                resultats.Add(new Resultats { Nom = resto.Nom, Telephone = resto.Telephone, NombreDeVotes = nombreDeVotes });
            }
            return resultats;
        }
    }
}