using System;
using UnityEngine;

namespace Controllers
{
    public sealed class PlayerInput : MonoBehaviour
    {
        [SerializeField] private LayerMask layerMask;
        private Camera cam;

        public event Action<Vector3> PlayerPressedField;
        
        private void Awake()
        {
            cam = Camera.main;
        }

        private void Update()
        {
            if (Input.GetMouseButtonDown(0))
            {
                var ray = cam.ScreenPointToRay(Input.mousePosition);

                if (Physics.Raycast(ray, out var hit,1000, layerMask)) InvokePlayerPressedField(hit.point);
            }
        }

        private void InvokePlayerPressedField(Vector3 pos)
        {
            PlayerPressedField?.Invoke(pos);
        }
    }
}