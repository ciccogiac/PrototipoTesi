using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace Escape.Levels.Level2
{
    public class MonitorLevel2 : MethodListener
    {
        [SerializeField] private GameObject AttributeValueMonitorPrefab;
        [SerializeField] private GameObject AttributeBox;
        [SerializeField] private TMP_Text ClassNameText;
        [SerializeField] private GameObject ClassBox;
        [SerializeField] private TMP_Text ErrorText;

        public override void Getter(List<(string, string)> objectValue)
        {
            base.Getter(objectValue);
            foreach (Transform child in AttributeBox.transform)
            {
                Destroy(child.gameObject);
            }
            ClassBox.SetActive(true);
            AttributeBox.SetActive(true);
            ClassNameText.text = className;
            foreach (var attribute in objectAttributeValue)
            {
                var objInstantiated = Instantiate(AttributeValueMonitorPrefab, AttributeBox.transform.position,
                    Quaternion.identity);
                objInstantiated.GetComponent<AttributeValueMonitor>()
                    .SetAttributeValueMonitor(attribute.Item1, attribute.Item2);
                objInstantiated.transform.SetParent(AttributeBox.transform);
                objInstantiated.transform.rotation = new Quaternion(0f, 0f, 0f, 0f);
            }
        }
        public override void SetClass(string nameClass)
        {
            base.SetClass(nameClass);
            ClassBox.SetActive(true);
            AttributeBox.SetActive(true);
            ErrorText.gameObject.SetActive(false);
            ClassNameText.text = className;
        }

        public void SetError(string error)
        {
            foreach (Transform child in AttributeBox.transform)
            {
                Destroy(child.gameObject);
            }
            ErrorText.gameObject.SetActive(true);
            ErrorText.text = error;
        }
        public override void RemoveObject()
        {
            base.RemoveObject();
            foreach (Transform child in AttributeBox.transform)
            {
                Destroy(child.gameObject);
            }
            ClassNameText.text = "";
            ClassBox.SetActive(false);
            AttributeBox.SetActive(false);
        }
    }
}
