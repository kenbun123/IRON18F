using UnityEngine;
using System.Collections;

public class CandleFire : MonoBehaviour
{
    // ===== 定数 =====
    /* 計算補正定数 */
    private const float CORRECTION = 6.5F / 160.0F;    // 光の範囲とコライダーの範囲の補正値
    private const float MIN_ANGLE = 50.0F;

    /* タグ */
    private const string MODEL_NAME = "Model";
    /* コンポーネントネーム */
    private const string SPOT_LIGHT_NAME = "SpotLight";
    private const string POINT_LIGHT_NAME = "PointLight";
    private const string COLLIDER_NAME = "Collider";
    private const string PARTICLE_NAME = "FireParticle";
    private const string SMOKE_NAME = "SmokeParticle";
    private const string SHINE_NAME = "ShineParticle";
    /* シーンネーム */
    private const string TITLE_SCENE = "Title";
    /* メタデータ */
    private const float LIGHT_UP_AREA = 50.0F;            // 光の強さのメタデータ
    private const float SUB_LIGHT = 3.0F;             // 光の減少量のメタデータ



    // ===== メンバ変数 =====
    /* Candle用変数 */
    public float LightUpArea;        // 光の強さ->寿命(単位不明)
    public float SubLight;           // 光の減少量(単位不明)
    private float maxLightUp;         // 光の強さの最大
    private float maxRadius;          // 最大の半径

    /* コンポーネント */
    private Light spotLight;  // ライトコンポーネント
    private Light pointLight; // ポイントライト
    private SphereCollider lightColl; // コリジョンコンポーネント
    private GameObject fire;
    //private CapsuleCollider modelColl;    // モデルのコリジョン

    public GameObject FirePrefab;      // 火のパーティクル
    public GameObject Smoke;     // 鎮火パーティクル

    public bool IsEnd;           //終了したのかどうか

    void Init()
    {
        // コンポーネント取得
        spotLight = gameObject.transform.FindChild(SPOT_LIGHT_NAME).GetComponent<Light>();
        pointLight = gameObject.transform.FindChild(POINT_LIGHT_NAME).GetComponent<Light>();
        lightColl = gameObject.GetComponent<SphereCollider>();

        // 初期化
        spotLight.spotAngle = LightUpArea;
        maxRadius = lightColl.radius;
        lightColl.radius = (LightUpArea - MIN_ANGLE) / (maxLightUp - MIN_ANGLE) * 0.25F * 10.0F + 0.5F;
        IsEnd = false;
        
    }
    // Use this for initialization
    void Start()
    {
        Init();
        SpawnFire();

    }

    // Update is called once per frame
    void Update()
    {
        AbateCandle();
    }



    void AbateCandle()
    {

        //End処理
        bool isLife = MIN_ANGLE >= LightUpArea;
        if (isLife)
        {
            EndCandle();
            return;
        }

        // 光の強さを弱める
        LightUpArea -= SubLight * Time.deltaTime;
        /* 各コンポーネントへ値の反映 */
        spotLight.spotAngle = LightUpArea;

        lightColl.radius = (LightUpArea - MIN_ANGLE) / (maxLightUp - MIN_ANGLE) * 0.25F * 10.0F + 0.5F;
    }

    void EndCandle()
    {
        if (IsEnd) return;
        Destroy(fire);

        var tmpParticle = Instantiate(Smoke);
        tmpParticle.transform.position = transform.position;
        //  tmpParticle.transform.position += new Vector3(0,0.7f,0);
        tmpParticle.transform.rotation = Quaternion.Euler(-90, 0, 0);
        tmpParticle.transform.parent = transform;
        IsEnd = true;
    }

    void SpawnFire()
    {
        fire = Instantiate(FirePrefab);
        fire.transform.position = transform.position;
        fire.transform.rotation = Quaternion.Euler(-90, 0, 0);
        fire.transform.localScale = new Vector3(0.9f, 0.9f, 0.9f);
        fire.transform.parent = transform;
    }
}