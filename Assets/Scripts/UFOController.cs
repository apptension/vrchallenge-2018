
using GoogleARCore;
using UnityEngine;

#if UNITY_EDITOR
// Set up touch input propagation while using Instant Preview in the editor.
using Input = GoogleARCore.InstantPreviewInput;
#endif

public class UFOController : MonoBehaviour {

    private Pose? target = null;
    public GameObject UFOProjectorPrefab;

    [HideInInspector]
    public bool isCatching = false;

	private void FixedUpdate()
	{
        if (!isCatching)
        {
            float step = 0.15f * Time.deltaTime;
            transform.position = Vector3.MoveTowards(transform.position, PointerRaycast.GetInstance().CurrentTarget, step);
        }
	}

    public void CatchAnimal()
    {
        if (!isCatching) {
            isCatching = true;
            GameObject ufoProjector = Instantiate(UFOProjectorPrefab, transform);
            Destroy(ufoProjector, 5f);
            Invoke("OnDestroyUFOPRoject", 3.5f);
        }
    }

    private void OnDestroyUFOPRoject()
    {
        isCatching = false;
    }

    private void Start()
    {
        GameManager.instance.GameStarted += HandleGameStarted;
        gameObject.SetActive(false);
    }

    void HandleGameStarted(object sender, System.EventArgs e)
    {
        transform.parent = GameManager.instance.anchor.transform;
        transform.localPosition = new Vector3();
        gameObject.SetActive(true);
    }
}
