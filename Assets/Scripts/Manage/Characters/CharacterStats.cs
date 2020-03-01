﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Manage.Characters
{
    public class CharacterStats
    {
        public uint Points { get; set; }
        
        private uint stamina;
        private uint endurance;
        private uint marksmanship;
        private uint cunning;
        private uint charisma;

        private static Random random = new Random();

        public CharacterStats()
        {
            RandomStats(this,1);
            Points = 0;
        }

        public CharacterStats(uint level)
        {
            RandomStats(this, level);
            Points = 0;
        }

        public CharacterStats(uint _stamina, uint _endurance, uint _marksmanship,uint _cunning,uint _charisma)
        {
            stamina = _stamina;
            endurance = _endurance;
            marksmanship = _marksmanship;
            cunning = _cunning;
            charisma = _charisma;
            Points = 0;
        }

        public uint AddStamina()
        {
            stamina++;
            Points--;
            return stamina;
        }

        public uint AddEndurance()
        {
            endurance++;
            Points--;
            return endurance;
        }

        public uint AddMarksmanship()
        {
            marksmanship++;
            Points--;
            return marksmanship;
        }

        public uint AddCunning()
        {
            cunning++;
            Points--;
            return cunning;
        }

        public uint AddCharisma()
        {
            charisma++;
            Points--;
            return charisma;
        }

        public uint GetStamina()
        {
            return stamina;
        }

        public uint GetEndurance()
        {
            return endurance;
        }

        public uint GetMarksmanship()
        {
            return marksmanship;
        }

        public uint GetCunning()
        {
            return cunning;
        }

        public uint GetCharisma()
        {
            return charisma;
        }

        private static CharacterStats RandomStats(CharacterStats characterStats, uint level)
        {
            characterStats.stamina = 1;
            characterStats.endurance = 1;
            characterStats.marksmanship = 1;
            characterStats.cunning = 1;
            characterStats.charisma = 1;
            for (int i=0;i<4+level; i++)
            {
                var randValue = (uint)random.Next(5);
                switch (randValue)
                {
                    case 0:
                        characterStats.stamina++;
                        break;
                    case 1:
                        characterStats.endurance++;
                        break;
                    case 2:
                        characterStats.marksmanship++;
                        break;
                    case 3:
                        characterStats.cunning++;
                        break;
                    case 4:
                        characterStats.charisma++;
                        break;
                }
            }
            return characterStats;
        }
    }
}