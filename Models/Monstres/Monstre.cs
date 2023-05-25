using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HeroesVsMonster.Models.Monstres
{
    public delegate string Loot();
    public abstract class Monstre : Personnage
    {
        public Loot SonLoot = null;
    }
}
