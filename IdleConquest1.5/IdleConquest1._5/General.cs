using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IdleConquest1._5
{
    public class General
    {
        public string generalname, classprof;
        public float speed, offense, defense, damage, noa, critrange, critmod,modmin,modmid,modmax,life;
        public int army,encounter,encounterarmy;
        public Texture2D spritename, spritename32;
        public Vector2 spritePosition;
        public float destinationx , destinationy ;
        public bool unitalive = false;
        public  bool ismoving;


        public General(string Generalname, string Classprof, Texture2D Spritename, Texture2D Spritename32, float Speed, float Offense, float Defense, float Damage, float Noa, float Critrange, float Critmod, float Life, int Army, Vector2 SpritePosition,int Encounter,float Modmin,float Modmid,float Modmax,float Destinationx, float Destinationy, bool Unitalive, int Encounterarmy, bool Ismoving)
        {
            generalname = Generalname;
            classprof = Classprof;
            speed = Speed;
            offense = Offense;
            defense = Defense;
            damage = Damage;
            noa = Noa;
            critrange = Critrange;
            critmod = Critmod;
            life = Life;
            spritename = Spritename;
            spritename32 = Spritename32;
            army = Army;
            spritePosition = SpritePosition;
            modmin = Modmin;
            modmid = Modmid;
            modmax = Modmax;
            destinationx = Destinationx;
            destinationy = Destinationy;
            unitalive = Unitalive;
            encounterarmy = Encounterarmy;
            ismoving = Ismoving;
        }
    }
}
