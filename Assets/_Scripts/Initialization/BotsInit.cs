using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace BomberGame
{
    [System.Serializable]
    public class BotInitData
    {
        public Transform Spawn;
        public GameObject Prefab;
        public BotSettings Settings;
    }
    public class BotsInit : MonoBehaviour
    {
        [SerializeField] private List<BotInitData> InitList = new List<BotInitData>();
        [SerializeField] private float _spawnDelay = 0.3f;
        List<EnemyBot> spawned = new List<EnemyBot>();

        public IEnumerator InitBots()
        {
            foreach(BotInitData data in InitList)
            {
                if (data != null)
                {
                    Vector3 pos = data.Spawn.position;
                    GameObject bot = Instantiate(data.Prefab);
                    
                    bot.transform.position = pos;
                    EnemyBot b = bot.GetComponent<EnemyBot>();
                    b.SelfInit = false;
                    b.Init(data.Settings);
                    b.SetState(BotStates.Idle);
                    spawned.Add(b);
                }
                yield return new WaitForSeconds(_spawnDelay);
            }
            
            yield return null;
        }

        public void StartBots()
        {
            foreach(EnemyBot b in spawned)
            {
                b.SetState(BotStates.Active);
            }
        }

    }
}