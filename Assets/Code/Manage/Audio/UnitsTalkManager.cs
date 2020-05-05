using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Manage.Units;
namespace Manage.Audio
{
    public class UnitsTalkManager : MonoBehaviour
    {
        public List<AudioClip> MaleAttacked = new List<AudioClip>();
        public List<AudioClip> FemaleAttacked = new List<AudioClip>();
        public List<AudioClip> MaleAttacking = new List<AudioClip>();
        public List<AudioClip> FemaleAttacking = new List<AudioClip>();
        public List<AudioClip> MaleMove = new List<AudioClip>();
        public List<AudioClip> FemaleMove = new List<AudioClip>();
        public List<AudioClip> MaleEnemyKilled = new List<AudioClip>();
        public List<AudioClip> FemaleEnemyKilled = new List<AudioClip>();
        public List<AudioClip> MaleReloading = new List<AudioClip>();
        public List<AudioClip> FemaleReloading = new List<AudioClip>();

        private AudioSource audioSource;

        private static System.Random random = new System.Random();

        void Awake()
        {
            audioSource = GetComponent<AudioSource>();
        }

        public void PlayAttacked(Unit unit)
        {
            if (unit.Character != null)
            {
                if (audioSource.isPlaying)
                {
                    return;
                }
                switch (unit.Character.Gender)
                {
                    case Characters.EGender.Other:
                    case Characters.EGender.Female:
                        PlayFemaleAttacked();
                        break;

                    case Characters.EGender.Male:
                        PlayMaleAttacked();
                        break;
                }
            }
        }

        public void PlayAttacking(Unit unit)
        {
            if (unit.Character != null)
            {
                if (audioSource.isPlaying)
                {
                    return;
                }
                switch (unit.Character.Gender)
                {
                    case Characters.EGender.Other:
                    case Characters.EGender.Female:
                        PlayFemaleAttacking();
                        break;

                    case Characters.EGender.Male:
                        PlayMaleAttacking();
                        break;
                }
            }
        }

        public void PlayMove(Unit unit)
        {
            if (unit.Character != null)
            {
                if (audioSource.isPlaying)
                {
                    return;
                }
                switch (unit.Character.Gender)
                {
                    case Characters.EGender.Other:
                    case Characters.EGender.Female:
                        PlayFemaleMove();
                        break;

                    case Characters.EGender.Male:
                        PlayMaleMove();
                        break;
                }
            }
        }

        public void PlayEnemyKilled(Unit unit)
        {
            if (unit.Character != null)
            {
                if (audioSource.isPlaying)
                {
                    return;
                }
                switch (unit.Character.Gender)
                {
                    case Characters.EGender.Other:
                    case Characters.EGender.Female:
                        PlayFemaleEnemyKilled();
                        break;

                    case Characters.EGender.Male:
                        PlayMaleEnemyKilled();
                        break;
                }
            }
        }

        public void PlayReloading(Unit unit)
        {
            if (unit.Character != null)
            {
                if (audioSource.isPlaying)
                {
                    return;
                }
                switch (unit.Character.Gender)
                {
                    case Characters.EGender.Other:
                    case Characters.EGender.Female:
                        PlayFemaleReloading();
                        break;

                    case Characters.EGender.Male:
                        PlayMaleReloading();
                        break;
                }
            }
        }

        private void PlayFemaleAttacked()
        {
            if (FemaleAttacked.Count <= 0)
            {
                return;
            }
            audioSource.PlayOneShot(FemaleAttacked[random.Next(0, FemaleAttacked.Count - 1)]);
        }

        private void PlayMaleAttacked()
        {
            if (MaleAttacked.Count <= 0)
            {
                return;
            }
            audioSource.PlayOneShot(MaleAttacked[random.Next(0, MaleAttacked.Count - 1)]);
        }

        private void PlayFemaleAttacking()
        {
            if (FemaleAttacking.Count <= 0)
            {
                return;
            }
            audioSource.PlayOneShot(FemaleAttacking[random.Next(0, FemaleAttacking.Count - 1)]);
        }

        private void PlayMaleAttacking()
        {
            if (MaleAttacking.Count <= 0)
            {
                return;
            }
            audioSource.PlayOneShot(MaleAttacking[random.Next(0, MaleAttacking.Count - 1)]);
        }

        private void PlayFemaleMove()
        {
            if (FemaleMove.Count <= 0)
            {
                return;
            }
            audioSource.PlayOneShot(FemaleMove[random.Next(0, FemaleMove.Count - 1)]);
        }

        private void PlayMaleMove()
        {
            if (MaleMove.Count <= 0)
            {
                return;
            }
            audioSource.PlayOneShot(MaleMove[random.Next(0, MaleMove.Count - 1)]);
        }

        private void PlayFemaleEnemyKilled()
        {
            if (FemaleEnemyKilled.Count <= 0)
            {
                return;
            }
            audioSource.PlayOneShot(FemaleEnemyKilled[random.Next(0, FemaleEnemyKilled.Count - 1)]);
        }

        private void PlayMaleEnemyKilled()
        {
            if (MaleEnemyKilled.Count <= 0)
            {
                return;
            }
            audioSource.PlayOneShot(MaleEnemyKilled[random.Next(0, MaleEnemyKilled.Count - 1)]);
        }

        private void PlayFemaleReloading()
        {
            if (FemaleReloading.Count <= 0)
            {
                return;
            }
            audioSource.PlayOneShot(FemaleReloading[random.Next(0, FemaleReloading.Count - 1)]);
        }

        private void PlayMaleReloading()
        {
            if (MaleReloading.Count <= 0)
            {
                return;
            }
            audioSource.PlayOneShot(MaleReloading[random.Next(0, MaleReloading.Count - 1)]);
        }
    }
}
