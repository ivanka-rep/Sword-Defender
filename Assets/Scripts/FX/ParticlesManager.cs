using System.Collections.Generic;
using Features;
using UnityEngine;

public class ParticlesManager : MonoBehaviour
{
  #region Serialized Fields
  [SerializeField] private List<ParticleSystem> attackParticlesList = null;
  #endregion

  #region Public

  public void PlayAttackParticle()
  {
    var particle = attackParticlesList[Random.Range(0, attackParticlesList.Count)];
    ChangeParticlePos(particle.transform);
    
    if(!particle.gameObject.activeSelf)
        particle.gameObject.SetActive(true);
    
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
