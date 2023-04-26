using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Static Properties", menuName = "Static Properties Asset")]
public class StaticDocumentProperties : ScriptableObject
{
    [field: Header("AnimatorTags"), Space(3)]
    [field: SerializeField] public string OpenControlTag { get; private set; }

    [field: SerializeField] public string SubmitTriggerTag { get; private set; }
    [field: SerializeField] public string DiscardTriggerTag { get; private set; }

    [field: Header("AreaTags"), Space(3)]
    [field: SerializeField] public string OpenAreaTag { get; private set; }
    [field: SerializeField] public string SubmitAreaTag { get; private set; }
    [field: SerializeField] public string DiscardAreaTag { get; private set; }


    [field: Space(5), Header("Constant Values"), Space(3)]
    [field: SerializeField] public float CorrectRotationTime { get; private set; }
    [field: SerializeField] public float RandomClosingRotation { get; private set; }
    [field: SerializeField] public Vector2 FileInDistance { get; private set; }
    [field: SerializeField] public float FileInRot { get; private set; }
    [field: SerializeField] public float FileInTime { get; private set; }

    [field: Space(5), Header("Colors"), Space(3)]
    [field: SerializeField] public Color SelectedHighLightTint { get; private set; }
    [field: SerializeField] public Color PreSubmitHighlightTint { get; private set; }
    [field: SerializeField] public Color PreDiscardHighlightTint { get; private set; }
}
