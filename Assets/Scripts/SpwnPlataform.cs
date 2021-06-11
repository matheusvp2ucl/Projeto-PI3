using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpwnPlataform : MonoBehaviour
{

    public List<GameObject> platforms = new List<GameObject>();
    public List<Transform> currentPlatforms = new List<Transform>();


    public float speed;
    public int offset;
    public int offsetReset;
    private Transform player;
    private GameObject objPlayer;
    private Transform currentPlatformPoint;
    private int platformIndex;
    


    void Start()
    {
        objPlayer = GameObject.FindGameObjectWithTag("Player");
        speed = objPlayer.GetComponent<Player>().speed;

        player = objPlayer.transform;

        for ( int i = 0; i < platforms.Count; i++ ) {
            Transform p = Instantiate( platforms[i], new Vector3( 0, 0, i * 86 ), transform.rotation ).transform;
            p.SetParent(this.transform);
            currentPlatforms.Add( p );
            offset += 86;
        }

        currentPlatformPoint = currentPlatforms[platformIndex].GetComponent<Platform>().point;

    }

    void Update()
    {

        if ( !objPlayer.GetComponent<Player>().isGameOver() ) {
            AttEsteiras();

            float distance = player.position.z - currentPlatformPoint.position.z;

            if (distance >= 5) {
                Recycle(currentPlatforms[platformIndex].gameObject);
                platformIndex++;
                if (platformIndex > currentPlatforms.Count - 1) {
                    platformIndex = 0;
                }
                currentPlatformPoint = currentPlatforms[platformIndex].GetComponent<Platform>().point;
            }
        }
        
    }

    public void Recycle( GameObject platform ) {
        platform.transform.position = new Vector3( 0, 0, offsetReset);
    }

    public void AttEsteiras() {
        for (int i = 0; i < currentPlatforms.Count; i++ ) {
            currentPlatforms[i].Translate(new Vector3(0, 0, -speed * Time.fixedDeltaTime));
        }
    }


}
