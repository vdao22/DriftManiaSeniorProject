using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FinishLine : MonoBehaviour
{
    
    public Text lapText;
    private float startTime;
    private bool finished = false;
    int lap;

    public GameObject finishedMenu;
    // Start is called before the first frame update
    void Start()
    {
        finishedMenu.SetActive(false);
        lap = 0;
    }

    // Update is called once per frame
    void Update()
    {
        string lapString = "Lap: " + lap.ToString();
        lapText.text = lapString;
    }

    public void Finish()
    {
        finished = true;
        finishedMenu.SetActive(true);
    }

    private void OnTriggerEnter(Collider col)
    {
        if (col.TryGetComponent<CarController>(out CarController player))
        {
            if (lap == 3)
            {
                Finish();
            }
            else if (lap < 3)
            {
                lap++;
            }
        }
    }
}
