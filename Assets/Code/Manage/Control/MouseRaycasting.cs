using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Manage.Units;
using Manage.Camera;
using Manage.UI;

namespace Manage.Control
{
    public class MouseRaycasting : MonoBehaviour
    {
        public Player.Player Player;
        public Canvas MainCanvas;
        public RectTransform SelectionSquare;
        public ParticleSystem UnitSelection;
        public ParticleSystem MoveOrder;
        public ParticleSystem AttackOrder;
        public ChestPopup ChestPopupPrefab;

        public HashSet<Unit> SelectedUnits;
        public bool beganSelecting;

        private List<ParticleSystem> UnitSelectionParticles;
        private CameraBehaviour cameraBehaviour;
        private bool beganOrdering;
        private readonly Vector3[] corners = new Vector3[2];

        private ChestPopup chestPopup;

        void Start()
        {
            SelectedUnits = new HashSet<Unit>();
            UnitSelectionParticles = new List<ParticleSystem>();
            cameraBehaviour = UnityEngine.Camera.main.GetComponent<CameraBehaviour>();
            beganSelecting = false;
            beganOrdering = false;
        }

        void Update()
        {
            if (Input.GetAxis("Select") > 0)
            {
                SelectTrue();
            }
            if (Input.GetAxis("Select") <= 0)
            {
                SelectFalse();
            }
            if (Input.GetAxis("Order") > 0)
            {
                OrderTrue();
            }
            if (Input.GetAxis("Order") <= 0)
            {
                OrderFalse();
            }
            CheckForChest();
        }

        private void CheckForChest()
        {
            RaycastHit hit;
            var ray = UnityEngine.Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit, 200))
            {
                var go = hit.transform.gameObject;
                if (go is null)
                {
                    if (!(chestPopup is null))
                    {
                        if (chestPopup.Deleting is false)
                        {
                            chestPopup.Delete();
                            chestPopup = null;
                        }
                    }
                    return;
                }
                var chest = go.GetComponent<Chest>();
                if (chest is null)
                {
                    if (!(chestPopup is null))
                    {
                        if (chestPopup.Deleting is false)
                        {
                            chestPopup.Delete();
                            chestPopup = null;
                        }
                    }
                    return;
                }
                UnityEngine.Debug.Log("Chest");
                if (chestPopup is null)
                {
                    chestPopup = Instantiate(ChestPopupPrefab, MainCanvas.transform);
                    chestPopup.Info = chest.ToString();
                    chestPopup.GetComponent<RectTransform>().anchoredPosition = Input.mousePosition;
                    return;
                }
            }
        }

        private void SelectTrue()
        {
            UnityEngine.Debug.Log("Select");

            if (!beganSelecting)
            {
                beganSelecting = true;
                corners[0] = Input.mousePosition;
            }
            else
            {
                SelectionSquare.gameObject.SetActive(true);
                corners[1] = Input.mousePosition;
                SelectionSquare.anchoredPosition = new Vector2(Mathf.Min(corners[0].x, corners[1].x), Mathf.Min(corners[0].y, corners[1].y));
                SelectionSquare.sizeDelta = new Vector2(Mathf.Max(corners[0].x, corners[1].x) - Mathf.Min(corners[0].x, corners[1].x),
                                                      Mathf.Max(corners[0].y, corners[1].y) - Mathf.Min(corners[0].y, corners[1].y));
            }
        }

        private void SelectFalse()
        {
            if (beganSelecting != false)
            {
                beganSelecting = false;
                SelectionSquare.gameObject.SetActive(false);
                foreach (var unit in SelectedUnits)
                {
                    unit.Unselect();
                }
                foreach (var unitSelectionParticle in UnitSelectionParticles)
                {
                    UnityEngine.Debug.Log("Destroyed");
                    Destroy(unitSelectionParticle);
                }
                UnitSelectionParticles.Clear();
                SelectedUnits.Clear();
                RaycastHit hit;
                UnityEngine.Debug.Log("Unselect");
                var minX = Mathf.Min((int)corners[0].x, (int)corners[1].x);
                var maxX = Mathf.Max((int)corners[0].x, (int)corners[1].x);
                var minY = Mathf.Min((int)corners[0].y, (int)corners[1].y);
                var maxY = Mathf.Max((int)corners[0].y, (int)corners[1].y);
                for (int i = minX; i <= maxX; i += 5)
                {
                    for (int j = minY; j <= maxY; j += 3)
                    {
                        var ray = UnityEngine.Camera.main.ScreenPointToRay(new Vector3(i, j, 0));
                        if (Physics.Raycast(ray, out hit, 200))
                        {
                            var unit = hit.transform.gameObject.GetComponentInParent<Unit>();
                            if (unit != null)
                            {
                                if (Equals(unit.Character.Organization, Player.Organization))
                                {
                                    SelectedUnits.Add(unit);
                                }
                            }
                        }
                    }
                }
                foreach (var unit in SelectedUnits)
                {
                    UnityEngine.Debug.Log("Selected");
                    unit.Select();
                    var selection = Instantiate(UnitSelection);
                    UnitSelectionParticles.Add(selection);
                    selection.transform.parent = unit.transform;
                    selection.transform.localPosition = new Vector3(0, 0.05f, 0);
                }
            }
        }

        private void OrderTrue()
        {
            UnityEngine.Debug.Log("Order");
            var ray = UnityEngine.Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, 200))
            {
                var go = hit.transform.gameObject;
                var point = hit.point;
                var hitUnit = go.GetComponentInParent<Unit>();
                if (hitUnit != null)
                {
                    if (SelectedUnits.Last().Character.Organization.Enemies.Contains(hitUnit.Character.Organization) ||
                        hitUnit.Character.Organization.Enemies.Contains(SelectedUnits.Last().Character.Organization))
                    {
                        foreach (var unit in SelectedUnits)
                        {
                            if (beganOrdering == false)
                            {
                                beganOrdering = true;
                                Instantiate(AttackOrder, point + new Vector3(0, 1, 0), new Quaternion());
                            }
                            unit.Stop();
                            unit.Attack(hitUnit);
                        }
                    }
                    else
                    {
                        foreach (var unit in SelectedUnits)
                        {
                            if (beganOrdering == false)
                            {
                                beganOrdering = true;
                                Instantiate(MoveOrder, point + new Vector3(0, 1, 0), new Quaternion());
                            }
                            unit.Stop();
                            unit.Move(hitUnit.transform.position);
                            if (!Equals(hitUnit.DialogManager, null))
                            {
                                hitUnit.DialogManager.PoolDialog(unit);
                            }
                        }
                    }
                }
                else
                {
                    var hitChest = go.GetComponentInParent<Chest>();
                    if (hitChest != null)
                    {
                        hitChest.SetPlayer(Player);
                    }
                    var currentPositionOfUnits = new List<Vector3>();
                    foreach (var unit in SelectedUnits)
                    {
                        currentPositionOfUnits.Add(unit.Position());
                    }
                    var orderPositionOfUnits = MoveOrderPositions.GetPositions(currentPositionOfUnits, point,5);
                    int i = 0;
                    foreach (var unit in SelectedUnits)
                    {
                        unit.Move(orderPositionOfUnits[i]);
                        i++;
                    }
                    i = 0;
                    if (beganOrdering == false)
                    {
                        beganOrdering = true;
                        foreach (var unit in SelectedUnits)
                        {
                            Instantiate(MoveOrder, orderPositionOfUnits[i] + new Vector3(0, 1, 0), new Quaternion());
                            i++;
                        }
                    }
                }
            }
        }

        private void OrderFalse()
        {
            if (beganOrdering == true)
            {
                beganOrdering = false;
            }
        }
    }
}