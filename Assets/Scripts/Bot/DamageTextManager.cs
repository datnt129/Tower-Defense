using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DamageTextManager : MonoBehaviour
{
    private ObjectPooler _objectPooler;
    // Start is called before the first frame update
    void Start()
    {
        _objectPooler = GetComponent<ObjectPooler>();
    }

    // Update is called once per frame
    public void Onhit(Transform spawnPoint, float damage)
    {
        GameObject newInstance = _objectPooler.GetInstanceFromPool();

        TMP_Text damageText = newInstance.GetComponent<DamageText>().DmgText;
        damageText.text = damage.ToString();

        newInstance.transform.SetParent(spawnPoint);
        newInstance.transform.position = spawnPoint.position;
        newInstance.SetActive(true);
        StartCoroutine(RemoveDmgText(newInstance));
    }

    private IEnumerator RemoveDmgText(GameObject instance)
    {
        yield return new WaitForSeconds(1f);
        instance.SetActive(false);
        ObjectPooler.ReturnToPool(instance);
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
