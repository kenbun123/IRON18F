using UnityEngine;
using System.Collections;

public class CandleMain : MonoBehaviour {


    public enum CandleStatus {
        NORMAL,
        FIRE,
        ALTUR

    };

    private MeshRenderer mesh;

    [SerializeField]
    private CandleStatus nowStatus;
    private GameObject fireObject;

    

    public GameObject FirePrefab;
    void Start()
    {
        CapsuleCollider tmp = new CapsuleCollider() ;
        mesh = CheckComponentNull<MeshRenderer>.CheckConmponentNull(this, "Class CandleMain : Don't　Get MeshRenderer");
        switch (nowStatus)
        {
            case CandleStatus.FIRE:

                fireObject = Instantiate(FirePrefab);
                fireObject.transform.position = transform.position;
                fireObject.transform.parent = transform;

                tmp = CheckComponentNull<CapsuleCollider>.CheckConmponentNull(this, "Class CandleMain : Don't　Get CapsuleCollider");

                if (tmp != null)
                {
                    tmp.enabled = false;

                }
                break;
            case CandleStatus.NORMAL:

                mesh.material.color = new Color(1, 1, 1, 0);
                break;
            case CandleStatus.ALTUR:

                tmp = CheckComponentNull<CapsuleCollider>.CheckConmponentNull(this, "Class CandleMain : Don't　Get CapsuleCollider");
                if (tmp != null)
                {
                    tmp.enabled = false;

                }
                break;
        }
    }

    void Update()
    {
        switch (nowStatus)
        {
            case CandleStatus.FIRE:

                if (fireObject.GetComponent<CandleFire>().IsEnd != true) return;

                mesh.material.color -= new Color(0, 0, 0, 0.005f);

                if (mesh.material.color.a <= 0.0f) Destroy(this.gameObject);

                break;
            case CandleStatus.NORMAL:
                if (mesh.material.color.a > 1.0f) return;
                
                 mesh.material.color += new Color(0, 0, 0, 0.05f);

                if (mesh.material.color.a > 1.0f) {
                    mesh.material.color = new Color(1, 1, 1, 1.0f);
                }

                
                break;
        }

        
    }

    public void SetStatus(CandleStatus value)
    {
        nowStatus = value;
        
    }
}
