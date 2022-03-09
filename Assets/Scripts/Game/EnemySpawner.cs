using System.Collections;
using System.Collections.Generic;
using System.Linq;
using SwordDefender.CharacterControl;
using UnityEngine;

namespace SwordDefender.Game
{
    //Using object-pooling method
    public class EnemySpawner : MonoBehaviour
    {
        #region Serialized Fields
        [SerializeField] private bool isEnabled = true;
        [SerializeField] private GameObject objectToPool;
        [SerializeField] private int amountToPool;
        [SerializeField] private Transform playerT = null;
        [SerializeField] private List<Transform> positionsList = null;
        #endregion

        #region Private
        private readonly List<EnemyController> m_enemyCtrlList = new List<EnemyController>();
        
        private Transform m_parent = null;
        private int m_enemiesToSpawn = 0;
        private int m_enemiesCount = 0;
        private int m_enemiesLeft = 0;
        #endregion
        
        #region Unity Methods
        private void Awake()
        {
            if (!isEnabled) return;

            m_enemiesToSpawn = GameManager.Instance.Config.EnemyParams.EnemiesAmount;
            GameEventManager.OnGameProcessStarted.AddListener(StartAction);
            GameEventManager.OnGameProcessEnded.AddListener(StopAction);
            GameEventManager.OnEnemyKilled.AddListener(OnEnemyKilled);
            
            m_parent = transform;
            for (int i = 0; i < amountToPool; i++)
            {
                GameObject tmp = Instantiate(objectToPool, m_parent);
                m_enemyCtrlList.Add(tmp.GetComponent<EnemyController>());
                tmp.SetActive(false);
            }
        }
        #endregion

        #region Private Methods

        private void StartAction()
        {
            //m_enemiesToSpawn = задавать новое значение с каждой итерацией (система уровней)
            m_enemiesCount = 0;
            m_enemiesLeft = m_enemiesToSpawn;
            
            foreach (var enemy in m_enemyCtrlList.Where(enemy => enemy.gameObject.activeSelf))
                enemy.gameObject.SetActive(false);
            
            StartCoroutine(SpawnRoutine());
        }

        private void StopAction() =>
            StopAllCoroutines();

        private void OnEnemyKilled()
        {
            m_enemiesLeft--;
            if (m_enemiesLeft == 0)
                GameEventManager.SendGameProcessEnded();
        }
        
        private EnemyController GetEnemyObject()
        {
            var enemyCtrl = m_enemyCtrlList.Find(enemy => !enemy.gameObject.activeSelf);
            
            if (!enemyCtrl)
            {
                var obj = Instantiate(objectToPool, m_parent);
                enemyCtrl = obj.GetComponent<EnemyController>();
                m_enemyCtrlList.Add(enemyCtrl);
            }

            return enemyCtrl;
        }

        #endregion
        
        #region Coroutines
        private IEnumerator SpawnRoutine()
        {
            if (!isEnabled || m_enemiesCount == m_enemiesToSpawn) yield break;
            
            var enemyCtrl = GetEnemyObject();
            enemyCtrl.transform.position = positionsList[Random.Range(0, positionsList.Count)].position;
            enemyCtrl.transform.LookAt(playerT);
            enemyCtrl.gameObject.SetActive(true);
            enemyCtrl.StartMoving(playerT);

            m_enemiesCount++;
            
            yield return new WaitForSeconds(3f);
            yield return SpawnRoutine();
        }
        #endregion
    }
}