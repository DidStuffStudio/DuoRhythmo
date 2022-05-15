using System;
using System.Collections;
using System.Collections.Generic;
using ctsalidis;
using Custom_Buttons.Did_Stuff_Buttons;
using Managers;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;
using UnityEngine.VFX;

public class DidStuffNode : AbstractDidStuffButton
{

    public int nodeIndex = 0;
    //private VisualEffect _vfx;
    public DrumType drumType;
    public NodeManager nodeManager;
    private static readonly float AngleWindow = 10.0f;
    public List<Image> subNodes = new List<Image>();
    private RectTransform _rectT;
    private bool _recentlyPlayed = false;
    [SerializeField] private ctsalidis.NodeSync _nodeSync;
    private float _angle = 0.0f;
    public bool nodeInitialised = false;

    public void InitialiseSubNodes()
    {
        for (var i = 0; i < subNodes.Count; i++)
        {
            var sub = subNodes[i];
            nodeManager.SetSubNode(nodeIndex, _isActive, i);
        }
        
    }
    public void ToggleState() => ButtonClicked();
    public bool IsActive => _isActive;
    

    public float GetAngle()
    {
        _rectT = transform.parent.GetComponent<RectTransform>();
        var rt = _rectT.anchoredPosition;
        var x = rt.x;
        var y = rt.y;
        _angle = (float)Angle();
        _rectT = GetComponent<RectTransform>();
        return _angle;
    }

    public void SetNodeSync(NodeSync nodeSync) => this._nodeSync = nodeSync; 
    public void SetActiveFromServer() => ChangeToActiveState();
    public void SetInactiveFromServer() => ChangeToInactiveState();
    
    private double Angle()
    {
        var spacing = 360 / nodeManager.NumberOfNodes;
        var angle = spacing *nodeIndex;
        angle += (spacing/2);
        return angle;
    }

    protected override void ButtonClicked()
    {
        base.ButtonClicked();
        if(_nodeSync) _nodeSync.Toggle(_isActive);
        MasterManager.Instance.UpdateSubNodes(nodeIndex, _isActive, (int)nodeManager.TypeOfDrum);
    }

    public override void ActivateButton() {
        base.ActivateButton();
        if(_nodeSync) _nodeSync.Toggle(_isActive);
    }

    public override void DeactivateButton() {
        base.DeactivateButton();
        if(_nodeSync) _nodeSync.Toggle(_isActive);
    }

    public void PlayDrum() {
        if (!_isActive || _recentlyPlayed) return;
        var localPosition = _rectT.localPosition;
        localPosition = new Vector3(localPosition.x, localPosition.y, -11.0f);
        _rectT.localPosition = localPosition;
        StartCoroutine(LerpBack());
       // StartCoroutine(AudioVFX());
        StartCoroutine(RecentlyPlayedDrum());
        nodeManager.PlayDrum((int)drumType);
    }

    private void FixedUpdate()
    {
        if (!nodeInitialised) return;
        if(Mathf.Abs(Mathf.Abs(nodeManager.currentRotation) - Mathf.Abs(_angle)) < AngleWindow) PlayDrum();
    }


    private IEnumerator RecentlyPlayedDrum()
    {
        _recentlyPlayed = true;
        yield return new WaitForSeconds(0.2f);
        _recentlyPlayed = false;
    }

    /*private IEnumerator AudioVFX() {
        _vfx.SetFloat("SphereSize", _vfx.GetFloat("SphereSize") + 1.0f);
        while (_vfx.GetFloat("SphereSize") > 1.1f) {
            yield return new WaitForSeconds(Time.fixedDeltaTime);
            _vfx.SetFloat("SphereSize", _vfx.GetFloat("SphereSize") - 0.1f);
        }
    }*/
    
    IEnumerator LerpBack()
    {
        while (_rectT.localPosition.z < 0.0f)
        {
            var localPosition = _rectT.localPosition;
            localPosition = Vector3.Lerp(localPosition,
                new Vector3(localPosition.x, localPosition.y, 0.1f), 0.05f);
            _rectT.localPosition = localPosition;
            yield return new WaitForSeconds(0.05f);
        }
    }
}
