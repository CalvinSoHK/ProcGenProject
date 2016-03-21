using UnityEngine;
using System.Collections;

public class Pathmaker : MonoBehaviour {

    private int counter = 0;
    private PathmakerManager manager;
    public float turnChance = 0.25f;
    public float spawnChance = 0.01f;
    public Transform floorPrefab1;
    public Transform floorPrefab2;
    public Transform floorPrefab3;
    public Transform pathmakerPrefab;
    Transform spawn;


	// Use this for initialization
	void Start () {
        manager = GameObject.Find("Main Camera").GetComponent<PathmakerManager>();
        spawn = floorPrefab1;
	}
	
	// Update is called once per frame
	void Update () {
        if (counter < 50)
        {
            //Code for random direction
            float randomNumber = Random.value;
            if (randomNumber < turnChance)
            {
                //transform.rotation = Quaternion.Euler(transform.eulerAngles + new Vector3(0, 90, 0));
                transform.Rotate(new Vector3(0, 90, 0));
            }
            else if (randomNumber >= turnChance && randomNumber < turnChance * 2)
            {
                //transform.rotation = Quaternion.Euler(transform.eulerAngles - new Vector3(0, 90, 0));
                transform.Rotate(new Vector3(0, -90, 0));
            }
            else if (randomNumber >= (1.0f - spawnChance) && randomNumber <= 1.0f)
            {
                if (manager.count < manager.max)
                {
                    Object temp = Instantiate(pathmakerPrefab, transform.position, Quaternion.identity);
                    Transform obj = temp as Transform;
                    Pathmaker script = obj.GetComponent<Pathmaker>();
                    script.turnChance = turnChance;
                    script.spawnChance = spawnChance;
                    manager.count++;
                }
            }

            //Code for random floor tile
            randomNumber = Random.value;
            if(randomNumber < 0.17f)
            {
                spawn = floorPrefab3;
            }
            else if(randomNumber < 0.34f)
            {
                spawn = floorPrefab2;
            }
            else if(randomNumber < 0.5f)
            {
                spawn = floorPrefab1;
            }

            //Code for random floor height
            randomNumber = Random.value;
            float heightChange;
            if(randomNumber < 0.5f)
            {
                heightChange = 0;
            }
            else if(randomNumber < 0.8f)
            {
                heightChange = 3;
            }
            else
            {
                heightChange = 6;
            }

            Object spawnTemp = Instantiate(spawn, transform.position, Quaternion.identity);
            Transform trans = spawnTemp as Transform;
            trans.localScale += new Vector3(0, 3 + heightChange, 0);
            trans.position -= new Vector3(0, 1 - heightChange / 2, 0);
            transform.position += transform.forward * 5;
            counter++;
        }

        else
        {
            Destroy(this);
        }

    }
}
