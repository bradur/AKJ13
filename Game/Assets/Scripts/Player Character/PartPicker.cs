using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class PartPicker : MonoBehaviour
{
    private int maxParts = 999;
    private int partsCollected = 0;
    private float totalValue = 0;

    private List<MinigameInfo> factoryInfos = new List<MinigameInfo>();

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public bool CollectPart(float value)
    {
        if (partsCollected >= maxParts)
        {
            return false;
        }
        else
        {
            ShowFactoryInfo();
            partsCollected++;
            UIRoboParts.main.SetValue(partsCollected, true);
            totalValue += value;
            return true;
        }

    }

    private void ShowFactoryInfo()
    {
        if (factoryInfos.Count == 0)
        {
            foreach (GameObject factory in GameObject.FindGameObjectsWithTag("Factory"))
            {
                Transform target = factory.GetComponentInChildren<UnloadParts>().transform;
                MinigameInfo info = WorldUI.main.GetMinigameInfo("Bring parts here!", target.position, target);
                factoryInfos.Add(info);
            };
        }
        // Debug.Log($"Show infos: {factoryInfos.Count}");
        foreach (MinigameInfo info in factoryInfos)
        {
            if (!info.IsShown)
            {
                info.Show();
            }
        }
    }

    public void HideFactoryInfo()
    {
        foreach (MinigameInfo info in factoryInfos)
        {
            if (info.IsShown)
            {
                info.Hide();
            }
        }
    }

    public float UnloadParts(int partsNeeded)
    {
        float value = totalValue;
        totalValue -= System.Math.Min(partsNeeded, partsCollected);
        partsCollected -= System.Math.Min(partsNeeded, partsCollected);
        UIRoboParts.main.SetValue(partsCollected, true);
        HideFactoryInfo();
        return value;
    }
}
