using UnityEngine;
using System;
using System.Collections.Generic;
namespace BomberGame
{
    public class AttackStart_CrossTargetPath
    {
        private int MaxAttackDistance;
        private float _step;
        private MapHelpers _mapHelper;
        private INodeMap<Vector2> _nodeMap;
        public AttackStart_CrossTargetPath(int maxAttackDistance, INodeMap<Vector2> nodeMap)
        {
            _nodeMap = nodeMap;
            MaxAttackDistance = maxAttackDistance;

            _step = _nodeMap.GetGridSize();
            _mapHelper = new MapHelpers(_nodeMap.GetNodes());
        }

        private bool DoDBG = false;
        private void DBG(string message)
        {
            if (DoDBG)
                Debug.Log(message);
        }

        public List<Vector2> GetCrossPath(Vector2 myPos, Vector2 enemyPos)
        {
            var directions = MapHelpers.GetDirectionVectors(myPos, enemyPos);
            Vector2 dir_x = directions.Item1;
            Vector2 dir_y = directions.Item2;
            var path_x = CanCross(dir_x, myPos, enemyPos);
            var path_y = CanCross(dir_y, myPos, enemyPos);
            // check if enemy can be reached from the end of the cross path
            EnemyReachRes reach_1 = CanReach(path_x, dir_y, enemyPos);
            EnemyReachRes reach_2 = CanReach(path_y, dir_x, enemyPos);

            var shortest = FindShortestPath(reach_1, reach_2);
            if (shortest.CrossPath == null)
            {
                Debug.Log("Shortest path is null");
                return null;
            }
            var finalPath = CorrectPath(shortest.CrossPath, shortest.ReachPath, enemyPos);
            return finalPath;
        }

        private EnemyReachRes FindShortestPath(EnemyReachRes path_1, EnemyReachRes path_2)
        {

            if (path_1.CrossPath == null && path_2.CrossPath == null)
                return new EnemyReachRes(false, null, null);
            if (path_1.CrossPath == null)
                return path_2;
            if (path_2.CrossPath == null)
                return path_1;
            if (path_1.CrossPath.Count < path_2.CrossPath.Count)
                return path_1;
            else
                return path_2;
        }

        private List<Vector2> CanCross(Vector2 dir, Vector2 myPos, Vector2 enemyPos)
        {
            try
            {
                DBG($"<color=red>Checing {dir} cross direction </color>");
                var path = _mapHelper.CanMove(dir, _step, myPos, enemyPos);
                return path;
            }
            catch (Exception ex)
            {
                DBG($"Caught: {ex.Message}");
                return null;
            }
        }

        /// <summary>
        ///  1 is up or right
        ///  -1 is left or left
        /// </summary>
        /// <param name="my"></param>
        /// <param name="other"></param>
        /// <returns></returns>
        private int GetCrossDir(float my, float other)
        {
            return (int)Mathf.Sign(other - my);
        }

        public struct EnemyReachRes
        {
            public bool Can;
            public List<Vector2> CrossPath;
            public List<Vector2> ReachPath;
            public EnemyReachRes(bool can, List<Vector2> crossPath, List<Vector2> reachPath)
            {
                Can = can;
                CrossPath = crossPath;
                ReachPath = reachPath;
            }
        }

        private EnemyReachRes CanReach(List<Vector2> crossPath, Vector2 reachDir, Vector2 enemyPos)
        {
            if (crossPath == null)
                return new EnemyReachRes(false, null, null);
            try
            {
                var edge = crossPath[crossPath.Count - 1];
                var reachPath = _mapHelper.CanMove(reachDir, _step, edge, enemyPos);
                
                return new EnemyReachRes(true, crossPath, reachPath);
            }
            catch(System.Exception ex)
            {
                DBG($"Caught {ex.Message}");
                return new EnemyReachRes(false, null, null);
            }
        }

        private List<Vector2> CorrectPath(List<Vector2> crossPath, List<Vector2> reachPath, Vector2 enemyPos)
        {
            if (reachPath == null)
                return crossPath;
            int currentLength = reachPath.Count;
            int diff = currentLength - MaxAttackDistance;
            if (diff > 0)
            {
                DBG("too far, will get closer");
                for (int i = 0; i < diff; i++)
                {
                    crossPath.Add(reachPath[i]);
                }
            }
            return crossPath;
        }


    }

}