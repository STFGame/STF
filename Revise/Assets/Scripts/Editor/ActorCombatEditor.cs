using Actor.Bubbles;
using UnityEditor;
using UnityEditorInternal;
using UnityEngine;

//namespace Actor.Combat
//{
//    [CustomEditor(typeof(ActorCombat))]
//    public class ActorCombatEditor : Editor
//    {
//        private ActorCombat combat;
//        private ReorderableList list;

//        private bool foldout;

//        private void OnEnable()
//        {
//            combat = (ActorCombat)target;

//            list = new ReorderableList(combat.hitBubbles, typeof(GameObject), true, true, true, true);

//            list.onAddCallback += AddItem;
//            list.onRemoveCallback += RemoveItem;

//            list.drawHeaderCallback += DrawHeader;
//            list.drawElementCallback += DrawElements;
//        }

//        private void OnDisable()
//        {
//            list.onAddCallback -= AddItem;
//            list.onRemoveCallback -= RemoveItem;

//            list.drawHeaderCallback -= DrawHeader;
//            list.drawElementCallback -= DrawElements;
//        }

//        private void DrawHeader(Rect rect)
//        {
//            EditorGUI.LabelField(rect, "Hit Bubble");
//        }

//        public override void OnInspectorGUI()
//        {
//            GUILayout.Space(5f);

//            foldout = EditorGUILayout.Foldout(foldout, "Hit Bubbles", true);

//            if (!foldout)
//                list.DoLayoutList();

//            base.OnInspectorGUI();
//        }

//        private void DrawElements(Rect rect, int index, bool isActive, bool isFocus)
//        {
//            GUILayout.Space(5f);

//            GameObject gameObject = combat.hitBubbles[index];

//            if (gameObject.GetComponent<HitBubble>() == null)
//                gameObject.AddComponent<HitBubble>();

//            HitBubble hitBubble = gameObject.GetComponent<HitBubble>();

//            float singleLine = EditorGUIUtility.singleLineHeight;

//            hitBubble.attributes.bodyArea = (BodyArea)EditorGUI.EnumPopup(new Rect(rect.x, rect.y, rect.width, singleLine), "Bubble Zone", hitBubble.attributes.bodyArea);
//            hitBubble.attributes.colliderType = (ColliderType)EditorGUI.EnumPopup(new Rect(rect.x, rect.y + 20f, rect.width, singleLine), "Collider", hitBubble.attributes.colliderType);
//            hitBubble.attributes.parent = (Transform)EditorGUI.ObjectField(new Rect(rect.x, rect.y + 40f, rect.width, singleLine), "Parent", hitBubble.attributes.parent, typeof(Transform), true);
//            gameObject.transform.eulerAngles = EditorGUI.Vector3Field(new Rect(rect.x, rect.y + 60, rect.width, singleLine), "Rotation", gameObject.transform.eulerAngles);

//            if (hitBubble.attributes.colliderType == ColliderType.SphereCollider)
//                SphereColliderSettings(rect, hitBubble);
//            if (hitBubble.attributes.colliderType == ColliderType.BoxCollider)
//                BoxColliderSettings(rect, hitBubble);

//            hitBubble.attributes.color = EditorGUI.ColorField(new Rect(rect.x, rect.y + 120f, rect.width, singleLine), "Gizmo Color", hitBubble.attributes.color);
//            hitBubble.attributes.color.a = 1;

//            hitBubble.isEnabled = EditorGUI.Toggle(new Rect(rect.x, rect.y + 140f, rect.width, singleLine), "Enabled", hitBubble.isEnabled);

//            if (hitBubble.attributes.parent != null)
//            {
//                gameObject.transform.SetParent(hitBubble.attributes.parent);
//                gameObject.transform.position = hitBubble.attributes.parent.position;
//                gameObject.layer = gameObject.transform.parent.gameObject.layer;
//            }

//            gameObject.name = "Hit Bubble " + hitBubble.attributes.bodyArea.ToString();
//            gameObject.tag = "HitBubble";

//            list.elementHeight = 160f;
//        }

//        private void SphereColliderSettings(Rect rect, HitBubble hitBubble)
//        {
//            float singleLine = EditorGUIUtility.singleLineHeight;

//            hitBubble.attributes.offset = EditorGUI.Vector3Field(new Rect(rect.x, rect.y + 80f, rect.width, singleLine), "Center", hitBubble.attributes.offset);
//            hitBubble.attributes.radius = EditorGUI.FloatField(new Rect(rect.x, rect.y + 100f, rect.width, singleLine), "Radius", hitBubble.attributes.radius);

//        }

//        private void BoxColliderSettings(Rect rect, HitBubble hitBubble)
//        {
//            float singleLine = EditorGUIUtility.singleLineHeight;

//            hitBubble.attributes.offset = EditorGUI.Vector3Field(new Rect(rect.x, rect.y + 80f, rect.width, singleLine), "Center", hitBubble.attributes.offset);
//            hitBubble.attributes.size = EditorGUI.Vector3Field(new Rect(rect.x, rect.y + 100f, rect.width, singleLine), "Size", hitBubble.attributes.size);
//        }

//        private void AddItem(ReorderableList list)
//        {
//            GameObject gameObject = new GameObject();
//            combat.hitBubbles.Add(gameObject);

//            EditorUtility.SetDirty(target);
//        }

//        private void RemoveItem(ReorderableList list)
//        {
//            DestroyImmediate(combat.hitBubbles[list.index]);
//            combat.hitBubbles.RemoveAt(list.index);

//            EditorUtility.SetDirty(target);
//        }
//    }
//}
