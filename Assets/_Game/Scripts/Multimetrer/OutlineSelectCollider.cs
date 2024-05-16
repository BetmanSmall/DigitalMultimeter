using System;
using System.Collections.Generic;
using _Game.Scripts.Enums;
using _Game.Scripts.Utils;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
namespace _Game.Scripts.Multimetrer {
    public class OutlineSelectCollider : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler {
        [Serializable]
        private class WorkTypeTransform {
            public WorkType WorkType;
            public Transform Transform;
        }
        // [SerializeField] private Dictionary<WorkType, Transform> positionsTransforms;
        [SerializeField] private List<WorkTypeTransform> transformsList;
        [SerializeField] private NavigationList<WorkTypeTransform> transformsNavList;
        [SerializeField] private Outline outline;
        private bool _active;
        public UnityAction<WorkType> OnWorkTypeChanged; 

        private void Start() {
            transformsNavList = new NavigationList<WorkTypeTransform>();
            // transformsList.CopyTo(transformsNavList.ToArray());
            foreach (var transform1 in transformsList) {
                transformsNavList.Add(transform1);
            }
        }

        public void OnPointerEnter(PointerEventData eventData) {
            // Debug.Log("OutlineSelectCollider::OnPointerEnter(); -- eventData:" + eventData, gameObject);
            outline.enabled = true;
            _active = true;
        }

        public void OnPointerExit(PointerEventData eventData) {
            // Debug.Log("OutlineSelectCollider::OnPointerExit(); -- eventData:" + eventData, gameObject);
            outline.enabled = false;
            _active = false;
        }

        private void OnMouseOver() {
            if (_active) {
                float mouseScrollWheel = Input.GetAxis("Mouse ScrollWheel");
                // Debug.Log("OutlineSelectCollider::OnMouseOver(); -- transformsNavList.CurrentIndex:" + transformsNavList.CurrentIndex, gameObject);
                if (Mathf.Abs(mouseScrollWheel) > 0f) {
                    if (mouseScrollWheel > 0f) {
                        transform.LookAt(transformsNavList.MoveNext.Transform.position, Vector3.forward);
                    } else if (mouseScrollWheel < 0f) {
                        transform.LookAt(transformsNavList.MovePrevious.Transform.position, Vector3.forward);
                    }
                    OnWorkTypeChanged?.Invoke(transformsNavList.Current.WorkType);
                }
            }
        }

        private void OnMouseEnter() {
            // Debug.Log("OutlineSelectCollider::OnMouseEnter(); -- gameObject:" + gameObject, gameObject);
            outline.enabled = true;
            _active = true;
        }

        private void OnMouseExit() {
            // Debug.Log("OutlineSelectCollider::OnMouseExit(); -- gameObject:" + gameObject, gameObject);
            outline.enabled = false;
            _active = false;
        }
    }
}