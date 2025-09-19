using UnityEngine;

public class BuildSystem : MonoBehaviour
{
    public Camera cam;
    public LayerMask groundMask;
    public GameObject turretPrefab;
    public int turretCost = 50;

    private void Update()
    {
        if (GameManager.I.CurrentPhase != GameManager.Phase.Build) return;

        if (Input.GetMouseButtonDown(0))
        {
            if (!CurrencySystem.I.CanAfford(turretCost)) return;

            Ray ray = cam.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit hit, 200f, groundMask))
            {
                Vector3 p = hit.point;
                // simple 1m snap
                p.x = Mathf.Round(p.x);
                p.z = Mathf.Round(p.z);
                p.y += 0.01f;

                if (PlaceIsClear(p))
                {
                    CurrencySystem.I.Spend(turretCost);
                    Instantiate(turretPrefab, p, Quaternion.identity);
                }
            }
        }
    }

    private bool PlaceIsClear(Vector3 position)
    {
        // basic overlap check to avoid stacking turrets
        return Physics.OverlapSphere(position, 0.45f, LayerMask.GetMask("Turrets")).Length == 0;
    }
}
