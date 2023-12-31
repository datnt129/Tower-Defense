using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DamageTextManager : MonoBehaviour
{
    [SerializeField]
    GameObject textDmgPrefab;

    public void Onhit(Transform spawnPoint, float damage)
    {
        GameObject newInstance = Instantiate(textDmgPrefab, spawnPoint.position, Quaternion.identity);
        newInstance.transform.SetParent(spawnPoint);

        TMP_Text damageText = newInstance.GetComponent<DamageText>().DmgText;
        damageText.text = damage.ToString();

        Destroy(newInstance, 1);
    }

    private void OnEnable()
    {
        BotFX.onHit += Onhit;
    }

    private void OnDisable()
    {
        BotFX.onHit -= Onhit;
    }
}
