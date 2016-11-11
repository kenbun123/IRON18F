using UnityEngine;
using System.Collections;

public class GhostEffect : MonoBehaviour {

    private GhostMain main;
    private float alpha;

    public float DashAlpha;

    public SkinnedMeshRenderer head;
    public SkinnedMeshRenderer body;
    public GameObject SmokeParticle;

    // Use this for initialization
    void Start () {

        main = CheckComponentNull<GhostMain>.CheckConmponentNull(gameObject, GetType().FullName + ": Don't Get GhostMain");

        if (main == null)
        {
            enabled = false;
        }
        alpha = 1.0f;
    }
	
	// Update is called once per frame
	void Update () {

        if (main.IsLocalPlayer) return;

        if (main.CanView)
        {
            SmokeParticle.SetActive(true);
            AddAlpha();

        }else{

            SmokeParticle.SetActive(false);
            SubAlpha();

        }

        SetRenderAlpha(alpha);
    }

    void SetRenderAlpha(float value) {
        head.material.color = new Color(1.0f, 1.0f, 1.0f, value);
        body.material.color = new Color(1.0f, 1.0f, 1.0f, value);
    }

    void AddAlpha()
    {

        if (alpha < 1.0f)
        {
            alpha += 0.05f;

        }
        if(alpha >= 1.0f)
        {
            alpha = 1f;
        }
    }
    void SubAlpha()
    {
        if (alpha > 0)
        {
            alpha -= 0.05f;
        }
        if (alpha <= 0)
        {
            alpha = 0;
        }
    }


}
