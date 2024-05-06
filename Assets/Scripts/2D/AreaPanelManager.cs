using Services;
using Services.ObjectPools;
using System.Collections.Generic;
using UnityEngine;

public class AreaPanelManager : MonoBehaviour
{
    private IObjectManager objectManager;
    private List<AreaPanel> panels = new List<AreaPanel>();
    private readonly string[] numbers = { "0101", "0100", "0110", "0001", "0000", "0010", "1001", "1000", "1010" };

    private void Awake()
    {
        objectManager = ServiceLocator.Get<IObjectManager>();
        for (int i = 0; i < 9; i++) 
        {
            AreaPanel panel = objectManager.Activate("AreaPanel", Vector3.zero, Vector3.zero, transform).Transform.GetComponent<AreaPanel>();
            panel.number = numbers[i];
            panels.Add(panel);
        }
    }

    public void GenerateAreas(int[] xs,int[] ys)
    {
        int count = 0;
        for (int j = 0; j < 3; j++)
        {
            for (int i = 0; i < 3; i++)
            {
          
                Vector3 min = GridGenerator2D.IndexToWorld(xs[i], ys[j]);
                Vector3 max = GridGenerator2D.IndexToWorld(xs[i + 1], ys[j + 1]);
                panels[count].SetRange(min, max);
                count++;
            }
        }
    }
}
