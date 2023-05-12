using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class LapsManager : MonoBehaviour
{
    public Text lapsText;

    private Player_Character_controller player_;
    public int maxDisplayNumber;
    // Start is called before the first frame update
    void Start()
    {
        player_ = GameObject.FindGameObjectWithTag("Player").GetComponent<Player_Character_controller>();

    }

    // Update is called once per frame
    void Update()
    {
        maxDisplayNumber = player_.maxLapNumber - 1;
        lapsText.text = "Lap: " + player_.lapNumber.ToString() + " / " + maxDisplayNumber.ToString();
    }
}
