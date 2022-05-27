using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Grill {
public class GrillMachine : MonoBehaviour {

    public static GrillMachine Instance;

    [SerializeField] private HeatBar heatBar;
    [SerializeField] private CompletionBar completionBar;

    [SerializeField] private GameObject grillMochiPrefab;
    [SerializeField] private int nbMochis;
    private List<GrillMochi> mochis = new List<GrillMochi>();

    void Awake() {
        Instance = this;
    }
    
    void Start() {
        for (int i = 0; i < nbMochis; i++) {
            float radiansOfSeparation = Mathf.PI * 2 / nbMochis;
            float x = Mathf.Sin(radiansOfSeparation * i) * 3.5f;
            float y = Mathf.Cos(radiansOfSeparation * i) * 2.25f;

            Vector3 mochiPosition = transform.position + new Vector3(x, y, 0);
            GrillMochi mochi = Instantiate(grillMochiPrefab, mochiPosition, Quaternion.identity).GetComponent<GrillMochi>();
            mochis.Add(mochi);
        }

        StartCoroutine(Grill());
    }

    private IEnumerator Grill() {
        while (true) {
            foreach (GrillMochi gm in mochis) gm.SetGrillStrenght(heatBar.heatValue/100);
            if (mochis.All(gm => gm.mochiState == 7f)) {
                MinigameManager.Instance.EndMinigame(false);
            }
            yield return new WaitForSeconds(1);
        }
    }

    public void GetMochi(GrillMochi grillMochi) {
        mochis.Remove(grillMochi);
        int mochiStep = (int)Mathf.Round(grillMochi.mochiState);

        if (mochiStep == 2 || mochiStep == 6) completionBar.FillBar(10);
        if (mochiStep == 3 || mochiStep == 5) completionBar.FillBar(20);
        if (mochiStep == 4) completionBar.FillBar(25);

        if (mochis.Count == 0) MinigameManager.Instance.EndMinigame(false);
    }
}
}

