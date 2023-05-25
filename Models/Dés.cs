using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HeroesVsMonster.Models
{
    public static class Dés
    {
        public const int MINIMUM = 1;
        public enum Faces
        {
            Quatre = 4,
            Six = 6
        }

        //Méthode pour lancer un seul dé
        /// <summary>
        /// "Lance" un dé à 4 faces
        /// </summary>
        /// <returns>int entre 1 et 4 compris</returns>
        public static int LancerDé4()
        {
            return LancerDé((int)Faces.Quatre);
        }
        /// <summary>
        /// "Lance" un dé à 6 faces
        /// </summary>
        /// <returns>int entre 1 et 6 compris</returns>
        public static int LancerDé6()
        {
            return LancerDé((int)Faces.Six);
        }
        /// <summary>
        /// "Lance" un dé générique avec le nombre de face démandé.
        /// Pour augmente le nombre de possibilité de de "dés" à lancer, modifier l'enum Faces
        /// </summary>
        /// <returns>int entre 1 et nombre de face demandé + 1 compris</returns>
        public static int LancerDé(Faces nombreDeFace)
        {
            return LancerDé((int)nombreDeFace);
        }
        /// <summary>
        /// Version ultra générique du lancer de dé
        /// </summary>
        /// <param name="nombreDeFace">valeur entière du nombre de face</param>
        /// <returns>résultat du lancer de dé</returns>
        private static int LancerDé(int nombreDeFace)
        {
            Random leHasard = new Random();
            return leHasard.Next(MINIMUM, nombreDeFace + 1);
        }


        //méthode pour lancer plusieurs dés
        /// <summary>
        /// Méthode pour le lancer de plusieurs dés de différentes face
        /// TODO : penser à comment généraliser sur le enum
        /// </summary>
        /// <param name="nbDé4">Nombre de dés à 4 faces</param>
        /// <param name="nbDé6">nombre de dés à 6 faces</param>
        /// <param name="détail">indique si nous souhaitons un retour écrit de toute les valeurs des dés (désactivé par défaut)</param>
        /// <returns>La somme des valeurs des dés</returns>
        public static int LancerDés(int nbDé4, int nbDé6, bool détail = false)
        {
            int result = 0;
            string log = "";

            if (nbDé4 > 0) result += LancerDésPartial(nbDé4, Faces.Quatre, ref log);
            if (nbDé6 > 0) result += LancerDésPartial(nbDé6, Faces.Six, ref log);

            if(détail) Console.WriteLine(log);
            return result;
        }
        /// <summary>
        /// Méthode interne permet un lancer générique tant de la Face du dé existe dans l'enum
        /// </summary>
        /// <param name="nbDé">Donne le nombre de dé à lancer</param>
        /// <param name="face">Reprends la valeur dans l'enum du type de dé choisi</param>
        /// <param name="log">Ressort un texte des résultats</param>
        /// <returns>La somme des valeurs des dés</returns>
        private static int LancerDésPartial(int nbDé, Faces face,ref string log)
        {
            int result = 0;
            if (nbDé > 0)
            {
                string message4 = nbDé>1?$"les {nbDé} dé {(int)face} ont donnés : " : $"le dé {(int)face} a donné : ";
                for (int i = 0; i < nbDé; i++)
                {
                    int value = LancerDé4();
                    message4 += value + " ";
                    result += value;
                }
                log += message4 + "\n";
            }
            return result;
        }

        /// <summary>
        /// Lancer de dés générique avec limitation du nombre de valeurs gardés
        /// </summary>
        /// <param name="nbDé"></param>
        /// <param name="face"></param>
        /// <param name="nbGardé"></param>
        /// <param name="meilleur">indique si on veut garder les meilleur valeurs de notre lancé (activé par défaut)</param>
        /// <param name="détail">indique si nous souhaitons un retour écrit de toute les valeurs des dés (désactivé par défaut)</param>
        /// <returns></returns>
        public static int LancerDésTronqué(int nbDé, Faces face, int nbGardé, bool meilleur = true,bool détail = false)
        {
            int result = 0;
            if (nbDé <= 0)
            {
                if(détail) Console.WriteLine("Aucun dé n'a été lancé");
                return result;
            }
            if (nbGardé <= 0)
            {
                if (détail) Console.WriteLine("Aucun dé n'a été gardé");
                return result;
            }
            List<int> lesValeurs = new List<int>();
            for(int i = 0; i< nbDé;i++)
            {
                lesValeurs.Add(LancerDé(face));
            }
            int[] valeursGardés = new int[nbGardé];
            if (nbGardé < nbDé)
            {
                List<int> indexGardé = new List<int>();
                for (int i = 0; i < nbGardé; i++)
                {
                    indexGardé.Add(-1);
                    if (meilleur)
                        valeursGardés[i] = 0;
                    else
                        valeursGardés[i] = (int)face + 1;
                }
                for (int i = 0; i < valeursGardés.Length; i++)
                {
                    for (int j = 0; j < lesValeurs.Count; j++)
                    {
                        if (!indexGardé.Contains(j) && ( 
                            (meilleur && valeursGardés[i] < lesValeurs[j]) || 
                            (!meilleur && valeursGardés[i] > lesValeurs[j]) ))
                        {
                            indexGardé[i] = j;
                            valeursGardés[i] = lesValeurs[j];
                        }
                    }
                }
            }

            if(détail)
            {
                string message = nbDé > 1 ? $"Les {nbDé} dés {(int)face} ont donnés : " : $"Le dé {(int)face} a donné : ";
                foreach (int value in lesValeurs)
                    message += value + " ";
                if(nbGardé <= nbDé)
                {
                    message += $" et on garde {nbGardé} ";
                    message += meilleur ? "meilleur(s) : " : "moins bon :"; 
                    if(nbGardé < nbDé)
                        foreach (int value in valeursGardés)
                            message += value + " ";
                    else
                        foreach (int value in lesValeurs)
                            message += value + " ";
                }
                Console.WriteLine(message);
            }
            if(nbGardé>nbDé)
                foreach (int value in valeursGardés)
                    result += value;
            else
                foreach (int value in lesValeurs)
                    result += value;
            return result;
        }
    }
}
