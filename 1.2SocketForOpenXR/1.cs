using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Filtering;
using UnityEngine.XR.Interaction.Toolkit.Interactables;

public class NewSocket : UnityEngine.XR.Interaction.Toolkit.Interactors.XRSocketInteractor
{
    private MeshRenderer meshRenderer;
    private Color initialColor;
    private ShowConfettiS3 confettiManager;
    private bool isCorrectlySnapped = false;
    private string socketName;

    protected override void Awake()
    {
        base.Awake();
        meshRenderer = GetComponent<MeshRenderer>();
        if (meshRenderer != null)
        {
            initialColor = meshRenderer.material.color;
        }
        confettiManager = FindObjectOfType<ShowConfettiS3>();
        socketName = gameObject.name.ToUpper();
    }

    private bool IsCorrectMatch(IXRHoverInteractable interactable)
    {
        if (interactable == null) return false;
        
        string interactableName = interactable.transform.name.ToUpper();
        if (interactableName.StartsWith("TEXT_"))
        {
            string textType = interactableName.Replace("TEXT_", "");
            return textType == socketName;
        }
        return false;
    }

    protected override void OnHoverEntered(HoverEnterEventArgs args)
    {
        base.OnHoverEntered(args);
        
        if (IsCorrectMatch(args.interactableObject))
        {
            SetMeshColor(new Color(0, 1, 0, initialColor.a)); // Green
        }
        else
        {
            SetMeshColor(new Color(1, 0, 0, initialColor.a)); // Red
        }
    }

    protected override void OnHoverExited(HoverExitEventArgs args)
    {
        base.OnHoverExited(args);
        if (!isCorrectlySnapped)
        {
            SetMeshColor(new Color(1, 1, 1, initialColor.a)); // White
        }
    }

    protected override void OnSelectEntered(SelectEnterEventArgs args)
    {
        base.OnSelectEntered(args);
        
        if (IsCorrectMatch(args.interactableObject as IXRHoverInteractable))
        {
            isCorrectlySnapped = true;
            if (confettiManager != null)
            {
                confettiManager.OnCorrectObjectSnapped();
            }
        }
    }

    protected override void OnSelectExited(SelectExitEventArgs args)
    {
        base.OnSelectExited(args);
        
        if (IsCorrectMatch(args.interactableObject as IXRHoverInteractable))
        {
            isCorrectlySnapped = false;
            if (confettiManager != null)
            {
                confettiManager.OnCorrectObjectRemoved();
            }
        }
        SetMeshColor(new Color(1, 1, 1, initialColor.a)); // Reset to white
    }

    private void SetMeshColor(Color color)
    {
        if (meshRenderer != null)
        {
            meshRenderer.material.color = color;
        }
    }
}
