using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IdleConquest1._5
{
    public class HostileEncounter
    {
        public  int min, max, loci, locj, encounterarmy;
        // public  float encounterlifemin, encounterlifemax, speed, encounteroffensemin, encounteroffensemax, encounterdefensemin, encounterdefensemax, encounterdamagemin, encounterdamagemax,encounterNOAmin, encounterNOAmax, encountercritrangemin, encountercritrangemax, encountercritmodmin, encountercritmodmax, temp, 
        public float encounterlife, encounteroffense, encounterdefense,encounterdamage, encounterNOA, encountercritrange, encountercritmod,temp;
        Random random = new Random();
       public void createlocation(float Encounterlifemin, float Encounterlifemax, float Encounteroffensemin, float Encounteroffensemax, float Encounterdefensemin, float Encounterdefensemax,
            float Encounterdamagemin, float Encounterdamagemax, float EncounterNOAmin, float EncounterNOAmax, float Encountercritrangemin, float Encountercritrangemax, float Encountercritmodmin,
            float Encountercritmodmax, int LocI, int LocJ, int Encounterarmy)
        {
            NextFloat(random, Encounterlifemin, Encounterlifemax);
            encounterlife = temp;
            NextFloat(random, Encounteroffensemin, Encounteroffensemax);
            encounteroffense = temp;
            NextFloat(random, Encounterdefensemin, Encounterdefensemax);
            encounterdefense = temp;
            NextFloat(random, Encounterdamagemin, Encounterdamagemax);
            encounterdamage = temp;
            NextFloat(random, EncounterNOAmin, EncounterNOAmax);
            encounterNOA = temp;
            NextFloat(random, Encountercritrangemin, Encountercritrangemax);
            encountercritrange = temp;
            NextFloat(random, Encountercritmodmin, Encountercritmodmax);
            encountercritmod = temp;
            encounterarmy = Encounterarmy;
        }
        public int LocI
        {
            get { return loci; }
            set { loci = value; }
        }
        public int LocJ
        {
            get { return locj; }
            set { locj = value; }
        }
        public float Encounterlife
        {
            get { return encounterlife; }
            set { encounterlife = value; }
        }
        public float Encounteroffense
        {
            get { return encounteroffense; }
            set { encounteroffense = value; }
        }
        public float Encounterdefense
        {
            get { return encounterdefense; }
            set { encounterdefense = value; }
        }
        public float Encounterdamage
        {
            get { return encounterdamage; }
            set { encounterdamage = value; }
        }
        public float EncounterNOA
        {
            get { return encounterNOA; }
            set { encounterNOA = value; }
        }
        public float Encountercritrange
        {
            get { return encountercritrange; }
            set { encountercritrange = value; }
        }

        public float Encountercritmod
        {
            get { return encountercritmod; }
            set { encountercritmod = value; }
        }
        public int Encounterarmy
        {
            get { return encounterarmy; }
            set { encounterarmy = value; }
        }
        
      
        public void NextFloat(Random random, float min,float max)
        {
            
            temp = (float)Math.Round(random.NextDouble() * (max - min) + min, 2);

        }
    }
}
