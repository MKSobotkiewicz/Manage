using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Manage.Organizations
{
    public static class OrganizationTypes
    {
        public enum EOrganization
        {
            Neutral,
            Bandits,
            Empire,
            Rebels,
            Republic,
            RedRats
        }

        public static Organization ToOrganization(EOrganization organization)
        {
            switch (organization)
            {
                case EOrganization.Neutral:
                    return Neutral;
                case EOrganization.Bandits:
                    return Bandits;
                case EOrganization.Empire:
                    return Empire;
                case EOrganization.Rebels:
                    return Rebels;
                case EOrganization.Republic:
                    return Republic;
                case EOrganization.RedRats:
                    return RedRats;
                default:
                    return null;
            }
        }

        public static Organization Neutral = new Organization(
            "Neutral",
            "",
            FlagLoader.LoadFlag("Black"),
            Color.green,
            new HashSet<Organization> { });

        public static Organization Bandits = new Organization(
            "Bandits",
            "", 
            FlagLoader.LoadFlag("Bandits"),
            Color.gray,
            new HashSet<Organization> { Empire, Rebels , Republic, RedRats });

        public static Organization Empire = new Organization(
            "Empire",
            "", 
            FlagLoader.LoadFlag("Empire"),
            Color.gray,
            new HashSet<Organization> { Bandits, Rebels, Republic, RedRats });

        public static Organization Rebels = new Organization(
            "Rebels", 
            "", 
            FlagLoader.LoadFlag("Rebels"),
            Color.gray,
            new HashSet<Organization> { Empire, Bandits, Republic, RedRats });

        public static Organization Republic = new Organization(
            "Republic", 
            "",
            FlagLoader.LoadFlag("Republic"), 
            Color.gray,
            new HashSet<Organization> { Empire, Rebels, Bandits, RedRats });

        public static Organization RedRats = new Organization(
            "Red Rats",
            "",
            FlagLoader.LoadFlag("RedRats"),
            Color.red,
            new HashSet<Organization> { Empire, Rebels, Republic, Bandits });

        private static System.Random random = new System.Random();

        public static Organization GetRandomOrganization()
        {
            switch (random.Next(0,4))
            {
                case 0:
                    return Bandits;
                case 1:
                    return Empire;
                case 2:
                    return Rebels;
                case 3:
                    return Republic;
            }
            return null;
        }
    }
}
