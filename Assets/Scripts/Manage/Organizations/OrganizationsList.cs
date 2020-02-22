using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Manage.Organizations
{
    public static class OrganizationsList
    {
        public static Organization Bandits = new Organization("Bandits", "", FlagLoader.LoadFlag("Bandits"),Color.gray);
        public static Organization Empire = new Organization("Empire", "", FlagLoader.LoadFlag("Empire"), Color.gray);
        public static Organization Rebels = new Organization("Rebels", "", FlagLoader.LoadFlag("Rebels"), Color.gray);
        public static Organization Republic = new Organization("Republic", "", FlagLoader.LoadFlag("Republic"), Color.gray);

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
