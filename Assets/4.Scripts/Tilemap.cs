using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
namespace Isometric
{

    public class Tilemap : MonoBehaviour
    {
        
        private Vector3 tilemapCell = Vector3.zero;
        public Tilemap tileMap;
        private Player player;
        private Transform playerLocation;
        void Start()
        {
            playerLocation = FindObjectOfType<Player>().transform;
        }

        // Update is called once per frame
        void Update()
        {
            
        }

        private void Test()
        {
            
                Ray ray = Camera.main.ScreenPointToRay(playerLocation.position);
                RaycastHit2D hit = Physics2D.Raycast(ray.origin, Vector3.zero);

                if(this.tileMap = hit.transform.GetComponent<Tilemap>())
                {
                    
                    

                    

                }
            
            
        }
    }

}