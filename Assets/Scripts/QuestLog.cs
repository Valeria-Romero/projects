using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class QuestLog : MonoBehaviour
{
    public GameObject panel;
    private List<string> mission;
    public GameObject questLogItem;
    // Start is called before the first frame update
    void Start()
    {
        mission = new List<string>();
        AddMission("placeholder");
        AddMission("placeholder1");
        AddMission("placeholder2");
    }

    void AddMission(string quest)
    {
        mission.Add(quest);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            if (panel.activeSelf)
            {
                for(int count = 0; count < panel.transform.childCount; count++)
                {
                    Destroy(panel.transform.GetChild(count).gameObject);
                }
                panel.SetActive(false);
            }
            else
            {
                panel.SetActive(true);
                foreach (string item in mission)
                {
                    Instantiate(questLogItem, panel.transform);
                }
            }
        }
            
    }
}
