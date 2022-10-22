using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SetSwapsText : MonoBehaviour
{
    private TextMeshProUGUI swapsText;

    void Start()
    {
        swapsText = GetComponent<TextMeshProUGUI>();
        SetSwaps();
    }

    private void SetSwaps()
    {
        swapsText.text = string.Format("{0}", PlayerManager.playerManager.GetSwapCounter());
    }
}
