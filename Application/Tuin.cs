﻿namespace Application
{
    public class Tuin
    {
        private LiggingTuin liggingTuin;

        private TuinOppervlakteMetersSquared tuinOppervlakte;

        public Tuin()
        {
            liggingTuin = LiggingTuin.All;
            tuinOppervlakte = TuinOppervlakteMetersSquared.All;
        }
    }
}