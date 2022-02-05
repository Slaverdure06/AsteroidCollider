using System.Collections;
using UnityEngine;

public class RayGun : MonoBehaviour
{
    public GameObject m_shotPrefab;

     public void shootRay()
    {
        GameObject laser = GameObject.Instantiate(m_shotPrefab, transform.position, transform.rotation) as GameObject;
        Destroy(laser, 1f);
    }

}
