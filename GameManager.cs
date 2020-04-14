using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{

    public int wave;

    TextMeshProUGUI waveText;

    private void Awake() {
        DontDestroyOnLoad(this.gameObject);
    } 

    // Update is called once per frame
    void Update()
    {
        waveText = GameObject.Find("Wave Text").GetComponent<TextMeshProUGUI>();
        waveText.text = "Wave: " + wave.ToString();
    }
}
