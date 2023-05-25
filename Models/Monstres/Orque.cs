using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HeroesVsMonster.Models.Monstres
{
    public class Orque : Monstre
    {
        //---------------ENDURANCE------------
        protected int _BonusRacialEndurance = 0;
        public new int End
        {
            get { return base.End + _BonusRacialEndurance; }

            protected set { _Endurance = value; }
        }
        public new int ModEnd
        {
            get { return getModificateur(End); }
        }

        //---------------FORCE------------
        protected int _BonusRacialForce = 1;
        public new int For
        {
            get { return base.For + _BonusRacialForce; }
            protected set { _Force = value; }
        }
        public new int ModFor
        {
            get { return getModificateur(For); }
        }

        public Orque() : base()
        {
            Pv = End + ModEnd;
            SonLoot = Or;
        }
        private string Or()
        {
            return "Or";
        }

        public override string ToString()
        {
            return $"Orque < PV : {Pv} | For {For} | End {End}>;";
        }
    }
}
