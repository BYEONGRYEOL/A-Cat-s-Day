using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Isometric
{
    public class LayerSorter : MonoBehaviour
    {
        private SpriteRenderer parentRenderer;
        private List<Obstacle> obstacleList = new List<Obstacle>();
        // Update is called once per frame
        void Start()
        {
            parentRenderer = transform.parent.GetComponent<SpriteRenderer>();
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.CompareTag("Obstacle"))
            {
                Obstacle obstacle = collision.GetComponent<Obstacle>();
                SpriteRenderer obstacleSpriteRenderer = obstacle.individualSpriteRenderer;

                obstacle.FadeOut();

                if(obstacleList.Count ==0 || obstacleSpriteRenderer.sortingOrder -1 < parentRenderer.sortingOrder)
                {
                    parentRenderer.sortingOrder = obstacleSpriteRenderer.sortingOrder - 1;
                }

                obstacleList.Add(obstacle);
            }
        }

        private void OnTriggerExit2D(Collider2D collision)
        {
            if (collision.CompareTag("Obstacle"))
            {
                Obstacle obstacle = collision.GetComponent<Obstacle>();
                obstacle.FadeIn();
                obstacleList.Remove(obstacle);

                if(obstacleList.Count == 0)
                {
                    parentRenderer.sortingOrder = 200;
                }

                else
                {
                    obstacleList.Sort();
                    parentRenderer.sortingOrder = obstacleList[0].individualSpriteRenderer.sortingOrder - 1;
                }
            }
        }
    }

}