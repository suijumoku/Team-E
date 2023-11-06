using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BlinkingScript : MonoBehaviour
{
    [SerializeField] private GameObject player = default!;
    [SerializeField] Image truelife = default!;
    [SerializeField] Image falselife = default!;
    [SerializeField] Material[] material = default!;
    [SerializeField] float duration = default!;
    [SerializeField] float[] timeDelay = default!;

    private float time = 0.0f;             // åoâﬂéûä‘Çäiî[Ç∑ÇÈïœêî    
    private bool changeF1 = false;
    private bool changeF2 = false;

    void Awake()
    {
        truelife.GetComponent<Image>().enabled = true;
        falselife.GetComponent<Image>().enabled = false;
    }
    void Update()
    {
        
    }
    private void ChangeImg(Image off, Image on)
    {
        off.enabled = false;
        on.enabled = true;
    }
    private IEnumerator Die()
    {
        changeF1 = false;
        changeF2 = false;
        yield return new WaitForSeconds(0.1f);

        while (true)
        {
            if (Mathf.Approximately(time, 0.0f))
            {
                ChangeImg(truelife, falselife);
                player.gameObject.GetComponent<Renderer>().material = material[1];
            }

            time += Time.deltaTime;

            if (time >= duration * timeDelay[0] && !changeF1)
            {
                ChangeImg(falselife, truelife);
                player.gameObject.GetComponent<Renderer>().material = material[0];

                changeF1 = true;
            }
            else if (time >= duration * timeDelay[1] && !changeF2)
            {
                ChangeImg(truelife, falselife);
                player.gameObject.GetComponent<Renderer>().material = material[1];

                changeF2 = true;
            }
            else if (time >= duration * timeDelay[2] && changeF2)
            {
                player.gameObject.GetComponent<Renderer>().material = material[0];

            }

            yield return null;
        }
    }
}
