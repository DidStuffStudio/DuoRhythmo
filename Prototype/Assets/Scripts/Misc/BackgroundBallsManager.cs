using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Managers
{
    public class BackgroundBallsManager : MonoBehaviour
    {
        public float spread = 10.0f;
        public float minSize = 10, maxSize = 80, minSpeed = 1.0f, maxSpeed = 20.0f;
        public int numberBalls;
        public List<Color> colours;
        [SerializeField] private GameObject ball;

        void OnDrawGizmosSelected()
        {
            Gizmos.color = new Color(1, 0, 0, 0.5f);
            Gizmos.DrawCube(transform.position, new Vector3(spread, spread, spread));
        }

        private void Start()
        {
            for (int i = 0; i < numberBalls; i++)
            {
                var pos = transform.position;
                var point = new Vector3(
                    Random.Range(pos.x-spread/2,pos.x+spread/2),
                    Random.Range(pos.y-spread/2,pos.y+spread/2),
                    Random.Range(pos.z-spread/2,pos.z+spread/2));
                var uniBall = Instantiate(ball,point, Quaternion.identity,transform);
                uniBall.GetComponent<BackgroundBalls>().manager = this;
            }
        }
    }
}