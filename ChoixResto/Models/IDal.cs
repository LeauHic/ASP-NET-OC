using Microsoft.Ajax.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChoixResto.Models
{
    public interface IDal : IDisposable
    {
        void CreerRestaurant(string nom, string telephone);
        void ModifierRestaurant(int id, string nom, string telephone);
        bool RestaurantExiste(string nomResto);
        int AjouterUtilisateur(string prenomUser, string motDePasse);
        Utilisateur Authentifier(string prenomUser, string motDePasse);
        Utilisateur ObtenirUtilisateur(int idUser);
        List<Resto> ObtientTouslesRestaurants();
        int CreerUnSondage();
        void AjouterVote(int idSondage, int vote, int idUtilisateur);
        bool ADejaVote(int idSondage, string IdUser);
        List<Resultats> ObtenirLesResultats(int idSondage);
    }
}
