using _Game.Scripts.Enums;
using TMPro;
using UnityEngine;
namespace _Game.Scripts.Multimetrer {
    public class DubbedBottomLeftPanel : MonoBehaviour {
        [SerializeField] private TMP_Text dcvValueTmp;
        [SerializeField] private TMP_Text aValueTmp;
        [SerializeField] private TMP_Text acvValueTmp;
        [SerializeField] private TMP_Text omValueTmp;
        [SerializeField] private DigitalMultimeter digitalMultimeter;

        private void OnEnable() {
            digitalMultimeter.WortTypeAndValue += WortTypeAndValue;
        }

        private void OnDisable() {
            digitalMultimeter.WortTypeAndValue -= WortTypeAndValue;
        }

        private void WortTypeAndValue(WorkType workType, float value) {
            dcvValueTmp.text = "0";
            aValueTmp.text = "0";
            acvValueTmp.text = "0";
            omValueTmp.text = "0";
            if (workType == WorkType.DCv) dcvValueTmp.text = value.ToString("0000");
            else if (workType == WorkType.Strength) aValueTmp.text = value.ToString("F");
            else if (workType == WorkType.ACv) acvValueTmp.text = value.ToString("F");
            else if (workType == WorkType.Resistance) omValueTmp.text = value.ToString("0000");
        }
    }
}