using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    [SerializeField] private LayerMask layer;
    private Camera mainCam;
    private bool canSelect;

    public void Init()
    {
        mainCam = FindObjectOfType<CamManager>().MainCam;
        ActionManager.Updater += OnUpdate;
        canSelect = true;
    }

    public void DeInit()
    {
        ActionManager.Updater -= OnUpdate;
    }

    private void OnUpdate(float deltaTime)
    {
        if (Input.GetMouseButtonDown(0) && canSelect)
        {

            if (!canSelect) return;

            Ray ray = mainCam.ScreenPointToRay(Input.mousePosition);
            RaycastHit hitInfo;

            if (Physics.Raycast(ray, out hitInfo, Mathf.Infinity, layer))
            {
                if (hitInfo.collider.transform.parent.TryGetComponent<GridElement>(out GridElement grid))
                {
                    grid.OnGridClicked();
                    return;
                }
            }
        }
    }
}
