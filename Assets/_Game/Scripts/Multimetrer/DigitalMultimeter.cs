using _Game.Scripts.Enums;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
namespace _Game.Scripts.Multimetrer {
    public class DigitalMultimeter : MonoBehaviour {
        [SerializeField] private TMP_Text displayTmpText;
        [SerializeField] private float resistanceValue = 1000;
        [SerializeField] private float powerValue = 400;
        [SerializeField] private WorkType currentWorkType;
        [SerializeField] private OutlineSelectCollider outlineSelectCollider;
        public UnityAction<WorkType, float> WortTypeAndValue;

        private void OnEnable() {
            if (outlineSelectCollider != null) outlineSelectCollider.OnWorkTypeChanged += OnWorkTypeChanged;
        }
        private void OnDisable() {
            if (outlineSelectCollider != null) outlineSelectCollider.OnWorkTypeChanged -= OnWorkTypeChanged;
        }

        private void OnWorkTypeChanged(WorkType workType) {
            currentWorkType = workType;
            float value = CalculateGetValue(workType);
            WortTypeAndValue?.Invoke(currentWorkType, value);
        }

        private float CalculateGetValue(WorkType workType) {
            float value = 0f;
            switch (workType) {
                // default:
                case WorkType.Off: {
                    displayTmpText.text = value.ToString("F");
                    break;
                }
                case WorkType.Resistance: {
                    displayTmpText.text = resistanceValue.ToString("0000");
                    value = resistanceValue;
                    break;
                }
                case WorkType.Strength: {
                    float strength = Mathf.Sqrt(powerValue / resistanceValue);
                    displayTmpText.text = strength.ToString("F");
                    value = strength;
                    break;
                }
                case WorkType.ACv: {
                    value = 0.01f;
                    displayTmpText.text = value.ToString("F");
                    break;
                }
                case WorkType.DCv: {
                    float dcV = Mathf.Sqrt(powerValue * resistanceValue);
                    displayTmpText.text = dcV.ToString("0000");
                    value = dcV;
                    break;
                }
            }
            return value;
        }
    }
}