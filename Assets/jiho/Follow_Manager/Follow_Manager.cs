using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Follow_Manager : MonoBehaviour
{
    public static Follow_Manager instance;

    public List<Unit> all_sponed_Unit;
    private void Awake()
    {
        instance = this;
    }


    // Update is called once per frame
    void Update()
    {
        
    }

    public void add_list_Unit(Unit u)
    {
        all_sponed_Unit.Add(u);
    
        for(int i=0; i< all_sponed_Unit.Count; i++)
        {
            if(i==0)
            {
                all_sponed_Unit[i].isMain = true;
                continue;
            }

            all_sponed_Unit[i].follow_Unit = all_sponed_Unit[i - 1].transform;
              
        }
    }
}
