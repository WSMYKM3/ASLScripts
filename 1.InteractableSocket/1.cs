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
        // Only proceed with base interaction if it's the correct interactor
        if (interactor.transform.parent.gameObject == correctInteractor)
        {
            base.InteractorAdded(interactor);
            SetMeshColor(new Color(0, 1, 0, initialColor.a)); // Green with initial alpha
        }
        else
        {
            // For wrong interactors, just show red but don't allow interaction
            SetMeshColor(new Color(1, 0, 0, initialColor.a)); // Red with initial alpha
        }
    }

    protected override void InteractorRemoved(SnapInteractor interactor)
    {
        // Only process removal for correct interactor
        if (interactor.transform.parent.gameObject == correctInteractor)
        {
            base.InteractorRemoved(interactor);
        }
        SetMeshColor(new Color(1, 1, 1, initialColor.a)); // White with initial alpha
    }

    // Override to prevent wrong interactors from selecting
    protected override void SelectingInteractorAdded(SnapInteractor interactor)
    {
        if (interactor.transform.parent.gameObject == correctInteractor)
        {
            base.SelectingInteractorAdded(interactor);
        }
    }

    // Override to only allow correct interactor to unselect
    protected override void SelectingInteractorRemoved(SnapInteractor interactor)
    {
        if (interactor.transform.parent.gameObject == correctInteractor)
        {
            base.SelectingInteractorRemoved(interactor);
        }
    }

    private void SetMeshColor(Color color)
    {
        if (meshRenderer != null)
        {
            meshRenderer.material.color = color;
        }
    }
}
