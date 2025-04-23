//this script now can connect with ShowConfettiS3.cs, only the correct word put into right socket, the confetti will play

using Oculus.Interaction;
using UnityEngine;

public class MyCustomMetaSnapInteractable : SnapInteractable
{
    public GameObject correctInteractor;
    private MeshRenderer meshRenderer;
    private Color initialColor;
    private ShowConfettiS3 confettiManager;
    private bool isCorrectlySnapped = false;

    // Add socket name for matching
    private string socketName;
    
    protected override void Start()
    {
        base.Start();
        meshRenderer = GetComponent<MeshRenderer>();
        if (meshRenderer != null)
        {
            initialColor = meshRenderer.material.color;
        }
        // Find the ShowConfettiS3 script in the scene
        confettiManager = FindObjectOfType<ShowConfettiS3>();
        
        // Get the socket name from this object's name
        socketName = gameObject.name.ToUpper();
    }

    private bool IsCorrectMatch(SnapInteractor interactor)
    {
        // Get the text object's name
        string interactorName = interactor.transform.parent.name.ToUpper();
        
        // Check if the text object exactly matches this socket
        // Text_I should only match with socket I
        // Text_CAN should only match with socket Can
        // Text_SIGN should only match with socket Sign
        if (interactorName.StartsWith("TEXT_"))
        {
            string textType = interactorName.Replace("TEXT_", "");
            // Exact match only
            return textType == socketName;
        }
        return false;
    }

    protected override void InteractorAdded(SnapInteractor interactor)
    {
        if (IsCorrectMatch(interactor))
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
        if (IsCorrectMatch(interactor))
        {
            base.InteractorRemoved(interactor);
            if (isCorrectlySnapped)
            {
                isCorrectlySnapped = false;
                if (confettiManager != null)
                {
                    confettiManager.OnCorrectObjectRemoved();
                }
            }
        }
        SetMeshColor(new Color(1, 1, 1, initialColor.a)); // White with initial alpha
    }

    // Override to prevent wrong interactors from selecting
    protected override void SelectingInteractorAdded(SnapInteractor interactor)
    {
        if (IsCorrectMatch(interactor))
        {
            base.SelectingInteractorAdded(interactor);
            isCorrectlySnapped = true;
            if (confettiManager != null)
            {
                confettiManager.OnCorrectObjectSnapped();
            }
        }
    }

    // Override to only allow correct interactor to unselect
    protected override void SelectingInteractorRemoved(SnapInteractor interactor)
    {
        if (IsCorrectMatch(interactor))
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
