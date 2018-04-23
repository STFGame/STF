using Actor.Bubbles;
using UnityEditor;
using UnityEditorInternal;
using UnityEngine;

namespace Actor.Combat
{
    [CustomEditor(typeof(ActorCombat))]
    public class ActorCombatEditor : Editor
    {
        private ActorCombat combat;
        private ReorderableList list;

        private bool foldout;

        private void OnEnable()
        {
            combat = (ActorCombat)target;

            list = new ReorderableList(combat.hitBubbles, typeof(GameObject), true, true, true, true);

            list.onAddCallback += AddItem;
            list.onRemoveCallback += RemoveItem;

            list.drawHeaderCallback += DrawHeader;
            list.drawElementCallback += DrawElements;
        }

        private void OnDisable()
        {
            list.onAddCallback -= AddItem;
            list.onRemoveCallback -= RemoveItem;

            list.drawHeaderCallback -= DrawHeader;
            list.drawElementCallback -= DrawElements;
        }

        private void DrawHeader(Rect rect)
        {
            EditorGUI.LabelField(rect, "Hit Bubble");
        }

        public override void OnInspectorGUI()
        {
            GUILayout.Space(5f);

            foldout = EditorGUILayout.Foldout(foldout, "Hit Bubbles", true);

            if (!foldout)
                list.DoLayoutList();

            base.OnInspectorGUI();
        }

        private void DrawElements(Rect rect, int index, bool isActive, bool isFocus)
        {
            GUILayout.Space(5f);

            GameObject gameObject = combat.hitBubbles[index];

            if (gameObject.GetComponent<HitBubble>() == null)
                gameObject.AddComponent<HitBubble>();

            HitBubble hitBubble = gameObject.GetComponent<HitBubble>();

            float singleLine = EditorGUIUtility.singleLineHeight;

            hitBubble.aspects.bodyArea = (BodyArea)EditorGUI.EnumPopup(new Rect(rect.x, rect.y, rect.width, singleLine), "Bubble Zone", hitBubble.aspects.bodyArea);
            hitBubble.aspects.colliderType = (ColliderType)EditorGUI.EnumPopup(new Rect(rect.x, rect.y + 20f, rect.width, singleLine), "Collider", hitBubble.aspects.colliderType);
            hitBubble.aspects.parent = (Transform)EditorGUI.ObjectField(new Rect(rect.x, rect.y + 40f, rect.width, singleLine), "Parent", hitBubble.aspects.parent, typeof(Transform), true);
            gameObject.transform.eulerAngles = EditorGUI.Vector3Field(new Rect(rect.x, rect.y + 60, rect.width, singleLine), "Rotation", gameObject.transform.eulerAngles);

            SetCollider(hitBubble);

            if (gameObject.GetComponent<Collider>() != null)
            {
                Collider collider = hitBubble.GetComponent<Collider>();

                collider.material = (PhysicMaterial)EditorGUI.ObjectField(new Rect(rect.x, rect.y + 140f, rect.width, singleLine), "Physics Material", collider.material, typeof(PhysicMaterial), true);
                collider.isTrigger = true;

                if (gameObject.GetComponent<Collider>().GetType() == typeof(SphereCollider))
                    SphereColliderSettings(rect, hitBubble);
                if (gameObject.GetComponent<Collider>().GetType() == typeof(BoxCollider))
                    BoxColliderSettings(rect, hitBubble);
            }

            hitBubble.aspects.color = EditorGUI.ColorField(new Rect(rect.x, rect.y + 120f, rect.width, singleLine), "Gizmo Color", hitBubble.aspects.color);
            hitBubble.aspects.color.a = 1;

            hitBubble.isEnabled = EditorGUI.Toggle(new Rect(rect.x, rect.y + 160f, rect.width, singleLine), "Enabled", hitBubble.isEnabled);

            if (hitBubble.aspects.parent != null)
            {
                gameObject.transform.SetParent(hitBubble.aspects.parent);
                gameObject.transform.position = hitBubble.aspects.parent.position;
            }

            gameObject.name = "Hit Bubble " + hitBubble.aspects.bodyArea.ToString();

            list.elementHeight = 180f;
        }

        private void SphereColliderSettings(Rect rect, HitBubble hitBubble)
        {
            SphereCollider sphere = hitBubble.GetComponent<SphereCollider>();

            float singleLine = EditorGUIUtility.singleLineHeight;

            hitBubble.aspects.center = EditorGUI.Vector3Field(new Rect(rect.x, rect.y + 80f, rect.width, singleLine), "Center", hitBubble.aspects.center);
            hitBubble.aspects.radius = EditorGUI.FloatField(new Rect(rect.x, rect.y + 100f, rect.width, singleLine), "Radius", hitBubble.aspects.radius);

            sphere.center = hitBubble.aspects.center;
            sphere.radius = hitBubble.aspects.radius;

        }

        private void BoxColliderSettings(Rect rect, HitBubble hitBubble)
        {
            BoxCollider box = hitBubble.GetComponent<BoxCollider>();

            float singleLine = EditorGUIUtility.singleLineHeight;

            hitBubble.aspects.center = EditorGUI.Vector3Field(new Rect(rect.x, rect.y + 80f, rect.width, singleLine), "Center", hitBubble.aspects.center);
            hitBubble.aspects.size = EditorGUI.Vector3Field(new Rect(rect.x, rect.y + 100f, rect.width, singleLine), "Size", hitBubble.aspects.size);

            box.center = hitBubble.aspects.center;
            box.size = hitBubble.aspects.size;
        }

        private void SetCollider(HitBubble hitBubble)
        {
            if (hitBubble.GetComponent<Collider>() != null)
                DestroyImmediate(hitBubble.GetComponent<Collider>());

            switch (hitBubble.aspects.colliderType)
            {
                case ColliderType.SphereCollider:
                    hitBubble.gameObject.AddComponent<SphereCollider>();
                    break;
                case ColliderType.BoxCollider:
                    hitBubble.gameObject.AddComponent<BoxCollider>();
                    break;
                default:
                    break;
            }
        }

        private void AddItem(ReorderableList list)
        {
            GameObject gameObject = new GameObject();
            combat.hitBubbles.Add(gameObject);

            EditorUtility.SetDirty(target);
        }

        private void RemoveItem(ReorderableList list)
        {
            DestroyImmediate(combat.hitBubbles[list.index]);
            combat.hitBubbles.RemoveAt(list.index);

            EditorUtility.SetDirty(target);
        }
    }
}
