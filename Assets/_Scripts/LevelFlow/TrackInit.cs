using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BomberGame
{
    public class TrackInit : MonoBehaviour
    {
        [SerializeField] private List<GameObject> _backgrounds = new List<GameObject>();
        [SerializeField] private List<GameObject> _borders = new List<GameObject>();
        [SerializeField] private List<GameObject> _walls = new List<GameObject>();

        [SerializeField] private float _startDelay = 0.15f;
        [SerializeField] private float _bigDelay = 0.22f;
        [SerializeField] private float _smallDelay = 0.075f;
        public IEnumerator InitTrack()
        {

            yield return new WaitForSeconds(_startDelay);
            foreach(GameObject g in _borders)
            {
                if(g)
                    g.SetActive(true);
            }
            yield return new WaitForSeconds(_bigDelay);

            foreach(GameObject g in _walls)
            {
                if (g)
                {
                    g.SetActive(true);
                    yield return new WaitForSeconds(_smallDelay);
                }
            }
            yield return new WaitForSeconds(_bigDelay);

            foreach (GameObject g in _backgrounds)
            {
                if (g)
                    g.SetActive(true);
            }
        }


        public void HideLevel()
        {
            foreach (GameObject g in _borders)
            {
                if (g)
                    g.SetActive(false);
            }
            foreach (GameObject g in _walls)
            {
                if (g)
                {
                    g.SetActive(false);
                }
            }
            foreach (GameObject g in _backgrounds)
            {
                if (g)
                    g.SetActive(false);
            }
        }
        public void ShowLevel()
        {
            foreach (GameObject g in _borders)
            {
                if (g)
                    g.SetActive(true);
            }
            foreach (GameObject g in _walls)
            {
                if (g)
                {
                    g.SetActive(true);
                }
            }
            foreach (GameObject g in _backgrounds)
            {
                if (g)
                    g.SetActive(true);
            }
        }


    }

}