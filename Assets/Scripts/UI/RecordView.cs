using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class RecordView : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _recordText;

    public void ShowRecord(float recordScore)
    {
        _recordText.text = $"{_recordText.text} {recordScore}";
    }
}
