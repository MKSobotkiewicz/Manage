using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Manage.Units
{
    public static class UnitShaderController
    {
        public static Color Player = new Color(0, 1, 0);
        public static Color Enemy = new Color(1, 0, 0);
        public static Color Neutral = new Color(1, 1, 0);

        public static void SetBlood(Unit unit,float value)
        {
            var meshRenderers = unit.GetComponentsInChildren<MeshRenderer>();
            var skinnedMeshRenderers = unit.GetComponentsInChildren<SkinnedMeshRenderer>();
            foreach (var skinnedMeshRenderer in skinnedMeshRenderers)
            {
                foreach (var material in skinnedMeshRenderer.materials)
                {
                    material.SetFloat("_BloodLevel", value);
                }
            }
            foreach (var meshRenderer in meshRenderers)
            {
                foreach (var material in meshRenderer.materials)
                {
                    material.SetFloat("_BloodLevel", value);
                }
            }
        }

        public static void SetColor(Unit unit, Player.Player player)
        {
            var meshRenderers = unit.GetComponentsInChildren<MeshRenderer>();
            var skinnedMeshRenderers = unit.GetComponentsInChildren<SkinnedMeshRenderer>();
            if (Equals(unit.Character.Organization, player.Organization))
            {
                UnityEngine.Debug.Log("Setting Color To Player");
                SetColor(meshRenderers, skinnedMeshRenderers, Player);
                return;
            }
            if (player.Organization.Enemies.Contains(unit.Character.Organization)|| 
                unit.Character.Organization.Enemies.Contains(player.Organization))
            {
                UnityEngine.Debug.Log("Setting Color To Enemy");
                SetColor(meshRenderers, skinnedMeshRenderers, Enemy);
                return;
            }
            UnityEngine.Debug.Log("Setting Color To Neutral");
            SetColor(meshRenderers, skinnedMeshRenderers, Neutral);
        }

        public static void SetSelected(Unit unit)
        {
            var meshRenderers = unit.GetComponentsInChildren<MeshRenderer>();
            var skinnedMeshRenderers = unit.GetComponentsInChildren<SkinnedMeshRenderer>();
            SetDistance(meshRenderers, skinnedMeshRenderers, 0.1f);
        }

        public static void SetUnselected(Unit unit)
        {
            var meshRenderers = unit.GetComponentsInChildren<MeshRenderer>();
            var skinnedMeshRenderers = unit.GetComponentsInChildren<SkinnedMeshRenderer>();
            SetDistance(meshRenderers, skinnedMeshRenderers, 0f);
        }

        private static void SetDistance(MeshRenderer[] meshRenderers, SkinnedMeshRenderer[] skinnedMeshRenderers,float distance)
        {
            foreach (var skinnedMeshRenderer in skinnedMeshRenderers)
            {
                foreach (var material in skinnedMeshRenderer.materials)
                {
                    material.SetFloat("_Dist", distance);
                }
            }
            foreach (var meshRenderer in meshRenderers)
            {
                foreach (var material in meshRenderer.materials)
                {
                    material.SetFloat("_Dist", distance/100);
                }
            }
        }

        private static void SetColor(MeshRenderer[] meshRenderers, SkinnedMeshRenderer[] skinnedMeshRenderers, Color color)
        {
            foreach (var meshRenderer in meshRenderers)
            {
                foreach (var material in meshRenderer.materials)
                {
                    material.SetColor("_MainColor", color);
                }
            }
            foreach (var skinnedMeshRenderer in skinnedMeshRenderers)
            {
                foreach (var material in skinnedMeshRenderer.materials)
                {
                    material.SetColor("_MainColor", color);
                }
            }
        }
    }

}
