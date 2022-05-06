using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BomberGame
{
    public class MovableWall : Wall, IMovableWall
    {
        [SerializeField] private CircleCaster _raycaster;
        private bool isMoving = false;
        private void Start()
        {
            if(_raycaster == null)
                _raycaster = GetComponent<CircleCaster>();
        }
        
        public bool Move(Vector3 dir,float distance, float time)
        {
            if (isMoving == true)
                return false;
            _raycaster.Distance = distance;
            RaycastHit2D other = _raycaster.RaycastAll(transform.position, dir).Find(t => t.collider.gameObject != gameObject);
            if (other == true)
            {
                return false;
            }
            if(isMoving == false)
            {
                StartCoroutine(MovingToPoint(transform.position + dir.normalized * distance, time));
                return true;
            }
            return false;
        }

        private IEnumerator MovingToPoint(Vector3 position, float time)
        {
            isMoving = true;
            float elapsed = 0f;
            Vector3 startPos = transform.position;
            while(elapsed <= time)
            {
                transform.position = Vector3.Lerp(startPos, position, elapsed / time);
                elapsed += Time.deltaTime;
                yield return null;
            }
            transform.position = position;
            isMoving = false;
        }
        
    }
}