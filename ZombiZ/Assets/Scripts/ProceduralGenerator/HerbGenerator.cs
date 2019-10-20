using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HerbGenerator : MonoBehaviour
{

	public int nbBrinsXZ;
	public GameObject herb1Prefab;
	public GameObject herb2Prefab;
	public Vector3 posPlateforme;
	public Vector3 dimPlateforme;
    // Start is called before the first frame update
    void Start()
    {
        for(int x = 1; x < nbBrinsXZ; x++)
        {
        	for(int z = 1; z < nbBrinsXZ; z++)
        	{
        		int dice = Random.Range(0, 3);
        		GameObject herbToInstantiate;
        		if (dice == 0 || dice == 1) herbToInstantiate = herb1Prefab;
        		else herbToInstantiate = herb2Prefab;
        		GameObject herb = Instantiate<GameObject>(herbToInstantiate, new Vector3(+Random.Range(-0.5f,0.5f)*(dimPlateforme.x/nbBrinsXZ) -dimPlateforme.x/2 + posPlateforme.x + x * (dimPlateforme.x/nbBrinsXZ), Random.Range(0f,1.5f), +Random.Range(-0.5f,0.5f)*(dimPlateforme.z/nbBrinsXZ) -dimPlateforme.z/2 + posPlateforme.z + z * (dimPlateforme.z/nbBrinsXZ)), Quaternion.identity);
                herb.transform.parent = gameObject.transform;
        	}
        }
    }
}
