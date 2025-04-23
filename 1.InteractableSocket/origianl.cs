using Oculus.Interaction;
using UnityEngine;

public class MyCustomMetaSnapInteractable : SnapInteractable
{
    public GameObject correctInteractor;
    private MeshRenderer meshRenderer;
    private Color initialColor;

    protected override void Start()
    {
        base.Start();
        meshRenderer = GetComponent<MeshRenderer>();
        if (meshRenderer != null)
        {
            initialColor = meshRenderer.material.color;
        }
    }

    protected override void InteractorAdded(SnapInteractor interactor)
    {
        base.InteractorAdded(interactor);

        if (interactor.transform.parent.gameObject == correctInteractor)
        {
            SetMeshColor(new Color(0, 1, 0, initialColor.a)); // Green with initial alpha
        }
        else
        {
            SetMeshColor(new Color(1, 0, 0, initialColor.a)); // Red with initial alpha
            // disable interactor so it can't snap to wrong object
            //interactor.enabled = false;
        }
    }

    protected override void InteractorRemoved(SnapInteractor interactor)
    {
        base.InteractorRemoved(interactor);
        SetMeshColor(new Color(1, 1, 1, initialColor.a)); // White with initial alpha
        // need to re-enable interactor (was disabled above in else if wrong) so that it can snap elsewhere
        //interactor.enabled = true;
    }

    private void SetMeshColor(Color color)
    {
        if (meshRenderer != null)
        {
            meshRenderer.material.color = color;
        }
    }
}