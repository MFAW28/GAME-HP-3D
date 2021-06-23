using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPosition : MonoBehaviour
{
    public GameObject chestPrefabs;

    public Vector3 PositionCenter;
    public Vector3 SizeBox;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SpawnChest()
    {
        Vector3 pos = PositionCenter + new Vector3(Random.Range(-SizeBox.x / 2, SizeBox.x / 2),
            Random.Range(-SizeBox.y / 2, SizeBox.y / 2),
            Random.Range(-SizeBox.z / 2, SizeBox.z / 2));
        if (!Physics.CheckBox(PositionCenter, SizeBox / 2))
        {
            Instantiate(chestPrefabs, pos, Quaternion.identity);
        }
    }
}
