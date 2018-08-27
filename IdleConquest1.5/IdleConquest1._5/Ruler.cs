using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IdleConquest1._5
{
    class Ruler
    {
        public string rname, kingdomName;
        public float commerce, military, education, workforce, social, temp;
        public float land, gold;

        Random random = new Random();
        // Character constructor
        public Ruler(string RName, string KingdomName, float Commerce, float Military, float Education, float Workforce, float Social, float Land)
        {


            RName = rname;
            kingdomName = KingdomName;
            commerce = Commerce;
            military = Military;
            education = Education;
            workforce = Workforce;
            social = Social;
            land = Land;

        }
        public void assignnumbers()
        {

            NextFloat(random);
            commerce = temp;
            NextFloat(random);
            military = temp;
            NextFloat(random);
            education = temp;
            NextFloat(random);
            workforce = temp;
            NextFloat(random);
            social = temp;

        }
        public void NextFloat(Random random)
        {

            temp = (float)Math.Round(random.NextDouble() * (50 - 10) + 10, 2);

        }
        public void cash()
        {

        }
        public void setland()
        {
            land = Convert.ToInt32(commerce + military + education + workforce + social / (float)Math.Round(random.NextDouble() * (50 - 10) + 10, 0));
            gold = Convert.ToInt32((commerce + education + workforce + (social / 2) / (float)Math.Round(random.NextDouble() * (50 - 10) + 10, 0)));
        }
    }
}
