using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Features;
using SwordDefender.CharacterControl;
using UnityEngine;

namespace SwordDefender.Game
{
    //Using object-pooling method
    public class EnemySpawner : MonoBehaviour
    {
        #region Serialized Fields
        [SerializeField] private GameObject objectToPool;
        [SerializeField] private int amountToPool;
        [SerializeField] private Transform playerT = null;
        [SerializeField] private List<Transform> positionsList = null;
        #endregion

        #region Private
        private readonly List<EnemyController> m_enemyCtrlList = new List<EnemyController>();
        
        private Transform m_parent = null;
        private int m_enemiesCount = 0;
        #endregion
        
        #region Unity Methods
        private void Awake()
        {
            m_parent = transform;
            for (int i = 0; i < amountToPool; i++)
            {
                GameObject tmp = Instantiate(objectToPool, m_parent);
                m_enemyCtrlList.Add(tmp.GetComponent<EnemyController>());
                tmp.SetActive(false);
            }
        }
        #endregion
        
        #region Public Methods
        public void StartAction(int enemiesAmount) =>
            StartCoroutine(SpawnRoutine(enemiesAmount));
        //
        // public void StopAction()
        // {
        //     StopAllCoroutines();
        //     m_enemyCtrlList.ForEach(enemy => {enemy.StopAllActions();});
        // }
        
        #endregion
        
        #region Coroutines

        private IEnumerator SpawnRoutine(int enemiesAmount)
        {
            var enemyCtrl = m_enemyCtrlList.Find(enemy => !enemy.gameObject.activeSelf);
            enemyCtrl.transform.position = positionsList[Random.Range(0, positionsList.Count)].position;
            enemyCtrl.transform.LookAt(playerT);
            enemyCtrl.gameObject.SetActive(true);
            enemyCtrl.StartMoving(playerT);

            m_enemiesCount++;
            if (m_enemiesCount == enemiesAmount) yield break;
            
            yield return new WaitForSeconds(3f);
            yield return SpawnRoutine(enemiesAmount);
        }
        
        #endregion
    }
}