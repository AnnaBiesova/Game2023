using System.Collections.Generic;
using System.Linq;
using Cinemachine;
using DG.Tweening;
using UnityEngine;

public class CinemachineTransparentObjects : CinemachineExtension
{
	[SerializeField] private string canBeTransparentTag = "CanBeTransparent";
    [SerializeField] private Transform target;
    [SerializeField] private LayerMask testLayers;
    [SerializeField] private Color transparentColor = new Color(1, 1, 1, 0.6f);
    
    private List<Material> transparentMaterials = new List<Material>();
    private float maxDistance;
    
    private static readonly int Color1 = Shader.PropertyToID("_Color");

    protected override void Awake()
    {
	    base.Awake();

	    maxDistance = Vector3.Distance(VirtualCamera.transform.position, target.position);
    }

    protected override void PostPipelineStageCallback(CinemachineVirtualCameraBase vcam, CinemachineCore.Stage stage, ref CameraState state, float deltaTime)
    {
	    if(Application.isPlaying == false) return;
	    
	    transparentMaterials.Clear();

	    Vector3 targetPos = target.position;
	    Vector3 vCamPos = vcam.transform.position;
	    
	    CheckRaycastForTransparencyInDirection(targetPos + Vector3.up - vCamPos);
	    CheckRaycastForTransparencyInDirection(targetPos + Vector3.up + Vector3.right * 2f - vCamPos);
	    CheckRaycastForTransparencyInDirection(targetPos + Vector3.up + Vector3.left * 2f - vCamPos);
	    
	    
        if (transparentMaterials.Count > 0)
        {
	        Color currentTransparentColor = Color.Lerp(transparentMaterials[0].GetColor(Color1), transparentColor,
		        Time.deltaTime * 0.5f);

	        foreach (Material transparentMaterial in transparentMaterials)
	        {
		        transparentMaterial.SetColor(Color1, currentTransparentColor);
	        }
        }

        
        void CheckRaycastForTransparencyInDirection(Vector3 direction)
        {
	        if (Physics.Raycast(vcam.transform.position, direction, out var hit, maxDistance, testLayers))
	        {
		        if (hit.collider.CompareTag(canBeTransparentTag) == false) return;
		        AddTransparentMaterialsFromRaycastHit(hit);
	        }
        }
        
        void AddTransparentMaterialsFromRaycastHit(RaycastHit hit)
        {
	        Transform transparentParent = FindParentByTag(hit.collider.transform, canBeTransparentTag, 3);

	        foreach (Renderer childRenderer in transparentParent.GetComponentsInChildren<Renderer>())
	        {
		        transparentMaterials.AddRange(childRenderer.materials);
	        }
        }
        
        Transform FindParentByTag(Transform child, string tag, int depth)
        {
	        Transform currentParentInDepth = child.parent;
	        int currentDepth = 0;

	        Transform returnParent = child;
	        
	        while (currentParentInDepth != null && currentDepth < depth)
	        {
		        if (currentParentInDepth.CompareTag(tag))
		        {
			        returnParent = currentParentInDepth;
		        }

		        currentParentInDepth = currentParentInDepth.parent;
		        currentDepth++;
	        }

	        return returnParent;
        }
    }
}
