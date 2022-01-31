using System.Collections.Generic;
using Features;
using UnityEngine;

public class ParticlesManager : MonoBehaviour
{
  #region Serialized Fields
  [SerializeField] private List<ObjectPool> attackParticlesPools = null;
  #endregion

  #region Private
  private List<ParticleSystem> m_attackParticles = null;
  #endregion

  #region Unity Methods
  private void Start()
  {
    m_attackParticles = new List<ParticleSystem>();
    
    attackParticlesPools.ForEach(pool => 
    { pool.PooledObjects.ForEach(obj => 
      m_attackParticles.Add(obj.GetComponent<ParticleSystem>())); });
  }
  #endregion

  #region Public

  public void PlayAttackParticle()
  {
    var particle = m_attackParticles[Random.Range(0, m_attackParticles.Count)];
    ChangeParticlePos(particle.transform);
    particle.Play();
  }

  #endregion

  #region Private

  private void ChangeParticlePos(Transform t)
  {
    var rot = t.rotation.eulerAngles;
    t.rotation = Quaternion.Euler(rot.x, transform.rotation.eulerAngles.y, rot.z);
    t.transform.position = transform.forward * 2 + new Vector3(0, 5, 0);
  }
  #endregion
}
