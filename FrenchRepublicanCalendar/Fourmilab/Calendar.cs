using System;

namespace FrenchRepublicanCalendar.Fourmilab
{
    // https://www.fourmilab.ch/documents/calendar/calendar.js
    // the file above is also embedded beside this one, in case their source code would change (so we could compare)

    public static class Calendar
    {
        /*  EQUATIONOFTIME  --  Compute equation of time for a given moment.
                        Returns the equation of time as a fraction of
                        a day.  */

        public static double equinoxe_a_paris(double year)
        {
            double equJED, equJD, equAPP, equParis, dtParis;

            //  September equinox in dynamical time
            equJED = Astronomic.equinox(year, 2);

            //  Correct for delta T to obtain Universal time
            equJD = equJED - (Astronomic.deltat(year) / (24 * 60 * 60));

            //  Apply the equation of time to yield the apparent time at Greenwich
            equAPP = equJD + Astronomic.equationOfTime(equJED);

            /*  Finally, we must correct for the constant difference between
                the Greenwich meridian and that of Paris, 2°20'15" to the
                East.  */

            dtParis = (2 + (20 / 60.0) + (15 / (60 * 60.0))) / 360;
            equParis = equAPP + dtParis;

            return equParis;
        }

        /*  PARIS_EQUINOXE_JD  --  Calculate Julian day during which the
                                   September equinox, reckoned from the Paris
                                   meridian, occurred for a given Gregorian
                                   year.  */

        public static double paris_equinoxe_jd(double year)
        {
            double ep, epg;

            ep = equinoxe_a_paris(year);
            epg = Math.Floor(ep - 0.5) + 0.5;

            return epg;
        }

        /*  ANNEE_DE_LA_REVOLUTION  --  Determine the year in the French
                                        revolutionary calendar in which a
                                        given Julian day falls.  Returns an
                                        array of two elements:

                                            [0]  Année de la Révolution
                                            [1]  Julian day number containing
                                                 equinox for this year.
        */

        public const double FRENCH_REVOLUTIONARY_EPOCH = 2375839.5;

        public static double[] annee_da_la_revolution(double jd)
        {
            double guess = jd_to_gregorian(jd)[0] - 2,
                lasteq, nexteq, adr;

            lasteq = paris_equinoxe_jd(guess);
            while (lasteq > jd)
            {
                guess--;
                lasteq = paris_equinoxe_jd(guess);
            }
            nexteq = lasteq - 1;
            while (!((lasteq <= jd) && (jd < nexteq)))
            {
                lasteq = nexteq;
                guess++;
                nexteq = paris_equinoxe_jd(guess);
            }
            adr = Math.Round((lasteq - FRENCH_REVOLUTIONARY_EPOCH) / Astronomic.TropicalYear) + 1;

            return new double[] { adr, lasteq };
        }

        /*  JD_TO_FRENCH_REVOLUTIONARY  --  Calculate date in the French Revolutionary
                                            calendar from Julian day.  The five or six
                                            "sansculottides" are considered a thirteenth
                                            month in the results of this function.  */

        public static double[] jd_to_french_revolutionary(double jd)
        {
            double an, mois, decade, jour;
            double[] adr;
            double equinoxe;

            jd = Math.Floor(jd) + 0.5;
            adr = annee_da_la_revolution(jd);
            an = adr[0];
            equinoxe = adr[1];
            mois = Math.Floor((jd - equinoxe) / 30) + 1;
            jour = (jd - equinoxe) % 30;
            decade = Math.Floor(jour / 10) + 1;
            jour = (jour % 10) + 1;

            return new double[]{an, mois, decade, jour};
        }

        /*  FRENCH_REVOLUTIONARY_TO_JD  --  Obtain Julian day from a given French
                                            Revolutionary calendar date.  */

        public static double french_revolutionary_to_jd(double an, double mois, double decade, double jour)
        {
            double[] adr;
            double equinoxe, guess, jd;

            guess = FRENCH_REVOLUTIONARY_EPOCH + (Astronomic.TropicalYear * ((an - 1) - 1));
            adr = new [] {an - 1, 0};

            while (adr[0] < an)
            {
                adr = annee_da_la_revolution(guess);
                guess = adr[1] + (Astronomic.TropicalYear + 2);
            }
            equinoxe = adr[1];

            jd = equinoxe + (30 * (mois - 1)) + (10 * (decade - 1)) + (jour - 1);
            return jd;
        }

        //  LEAP_GREGORIAN  --  Is a given year in the Gregorian calendar a leap year ?

        public static bool leap_gregorian(double year)
        {
            return ((year % 4) == 0) &&
                   (!(((year % 100) == 0) && ((year % 400) != 0)));
        }

        //  GREGORIAN_TO_JD  --  Determine Julian day number from Gregorian calendar date

        public static double GREGORIAN_EPOCH = 1721425.5;

        public static double gregorian_to_jd(double year, double month, double day)
        {
            return (GREGORIAN_EPOCH - 1) +
                   (365 * (year - 1)) +
                   Math.Floor((year - 1) / 4) +
                   (-Math.Floor((year - 1) / 100)) +
                   Math.Floor((year - 1) / 400) +
                   Math.Floor((((367 * month) - 362) / 12) +
                              ((month <= 2) ? 0 :
                                  (leap_gregorian(year) ? -1 : -2)
                              ) +
                              day);
        }


        //  JD_TO_GREGORIAN  --  Calculate Gregorian calendar date from Julian day

        public static double[] jd_to_gregorian(double jd)
        {
            double wjd, depoch, quadricent, dqc, cent, dcent, quad, dquad,
                yindex, dyindex, year, yearday, leapadj;

            wjd = Math.Floor(jd - 0.5) + 0.5;
            depoch = wjd - GREGORIAN_EPOCH;
            quadricent = Math.Floor(depoch / 146097);
            dqc = Astronomic.mod(depoch, 146097);
            cent = Math.Floor(dqc / 36524);
            dcent = Astronomic.mod(dqc, 36524);
            quad = Math.Floor(dcent / 1461);
            dquad = Astronomic.mod(dcent, 1461);
            yindex = Math.Floor(dquad / 365);
            year = (quadricent * 400) + (cent * 100) + (quad * 4) + yindex;
            if (!((cent == 4) || (yindex == 4)))
            {
                year++;
            }
            yearday = wjd - gregorian_to_jd(year, 1, 1);
            leapadj = ((wjd < gregorian_to_jd(year, 3, 1)) ? 0
                    :
                    (leap_gregorian(year) ? 1 : 2)
                );
            var month = Math.Floor((((yearday + leapadj) * 12) + 373) / 367);
            var day = (wjd - gregorian_to_jd(year, month, 1)) + 1;

            return new double[]{year, month, day};
        }
    }
}
