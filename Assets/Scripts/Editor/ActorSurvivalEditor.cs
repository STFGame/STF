using UnityEditor;
using UnityEditorInternal;
using UnityEngine;

namespace Actor.Bubbles
{
    [CustomEditor(typeof(ActorSurvival))]
    public class ActorSurvivalEditor : Editor
    {
        private ActorSurvival survival;
        private ReorderableList list;

        private bool foldout = false;

        private void OnEnable()
        {
            survival = (ActorSurvival)target;

            list = new ReorderableList(survival.hurtBubbles, typeof(GameObject), true, true, true, true);

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

        public override void OnInspectorGUI()
        {
            GUILayout.Space(5f);

            foldout = EditorGUILayout.Foldout(foldout, "Hurt Bubbles", true);

            if (!foldout)
                list.DoLayoutList();
        }

        private void DrawHeader(Rect rect)
        {
            GUI.Label(rect, "Hurt Bubbles");
        }

        private void DrawElements(Rect rect, int index, bool isActive, bool isFocus)
        {
            GUILayout.Space(5f);

            GameObject gameObject = survival.hurtBubbles[index];

            if (gameObject.GetComponent<HurtBubble>() == null)
                gameObject.AddComponent<HurtBubble>();

            HurtBubble hurtBubble = gameObject.GetComponent<HurtBubble>();

            float singleLine = EditorGUIUtility.singleLineHeight;

            hurtBubble.aspects.bodyArea = (BodyArea)EditorGUI.EnumPopup(new Rect(rect.x, rect.y, rect.width, singleLine), "Bubble Zone", hurtBubble.aspects.bodyArea);
            hurtBubble.aspects.colliderType = (ColliderType)EditorGUI.EnumPopup(new Rect(rect.x, rect.y + 20f, rect.width, singleLine), "Collider", hurtBubble.aspects.colliderType);
            hurtBubble.aspects.parent = (Transform)EditorGUI.ObjectField(new Rect(rect.x, rect.y + 40f, rect.width, singleLine), "Parent", hurtBubble.aspects.parent, typeof(Transform), true);
            gameObject.transform.eulerAngles = EditorGUI.Vector3Field(new Rect(rect.x, rect.y + 60, rect.width, singleLine), "Rotation", gameObject.transform.eulerAngles);

            SetCollider(hurtBubble);

            if (gameObject.GetComponent<Collider>() != null)
            {
                Collider collider = hurtBubble.GetComponent<Collider>();

                collider.material = (PhysicMaterial)EditorGUI.ObjectField(new Rect(rect.x, rect.y + 140f, rect.width, singleLine), "Physics Material", collider.material, typeof(PhysicMaterial), true);
                collider.isTrigger = true;

                if (gameObject.GetComponent<Collider>().GetType() == typeof(SphereCollider))
                    SphereColliderSettings(rect, hurtBubble);
                if (gameObject.GetComponent<Collider>().GetType() == typeof(BoxCollider))
                    BoxColliderSettings(rect, hurtBubble);
            }

            hurtBubble.aspects.color = EditorGUI.ColorField(new Rect(rect.x, rect.y + 120f, rect.width, singleLine), "Gizmo Color", hurtBubble.aspects.color);
            hurtBubble.aspects.color.a = 1;

            hurtBubble.isEnabled = EditorGUI.Toggle(new Rect(rect.x, rect.y + 160f, rect.width, singleLine), "Enabled", hurtBubble.isEnabled);

            if (hurtBubble.aspects.parent != null)
            {
                gameObject.transform.SetParent(hurtBubble.aspects.parent);
                gameObject.transform.position = hurtBubble.aspects.parent.position;
            }

            gameObject.name = "Hurt Bubble " + hurtBubble.aspects.bodyArea.ToString();

            list.elementHeight = 180f;
        }

        private void SphereColliderSettings(Rect rect, HurtBubble hurtBubble)
        {
            SphereCollider sphere = hurtBubble.GetComponent<SphereCollider>();

            float singleLine = EditorGUIUtility.singleLineHeight;

            hurtBubble.aspects.center = EditorGUI.Vector3Field(new Rect(rect.x, rect.y + 80f, rect.width, singleLine), "Center", hurtBubble.aspects.center);
            hurtBubble.aspects.radius = EditorGUI.FloatField(new Rect(rect.x, rect.y + 100f, rect.width, singleLine), "Radius", hurtBubble.aspects.radius);

            sphere.center = hurtBubble.aspects.center;
            sphere.radius = hurtBubble.aspects.radius;

        }

        private void BoxColliderSettings(Rect rect, HurtBubble hurtBubble)
        {
            BoxCollider box = hurtBubble.GetComponent<BoxCollider>();

            float singleLine = EditorGUIUtility.singleLineHeight;

            hurtBubble.aspects.center = EditorGUI.Vector3Field(new Rect(rect.x, rect.y + 80f, rect.width, singleLine), "Center", hurtBubble.aspects.center);
            hurtBubble.aspects.size = EditorGUI.Vector3Field(new Rect(rect.x, rect.y + 100f, rect.width, singleLine), "Size", hurtBubble.aspects.size);

            box.center = hurtBubble.aspects.center;
            box.size = hurtBubble.aspects.size;
        }

        private void SetCollider(HurtBubble hurtBubble)
        {
            if (hurtBubble.GetComponent<Collider>() != null)
                DestroyImmediate(hurtBubble.GetComponent<Collider>());

            switch (hurtBubble.aspects.colliderType)
            {
                case ColliderType.SphereCollider:
                    hurtBubble.gameObject.AddComponent<SphereCollider>();
                    break;
                case ColliderType.BoxCollider:
                    hurtBubble.gameObject.AddComponent<BoxCollider>();
                    break;
                default:
                    break;
            }
        }

        private void AddItem(ReorderableList list)
        {
            GameObject gameObject = new GameObject();
            survival.hurtBubbles.Add(gameObject);

            EditorUtility.SetDirty(target);
        }

        private void RemoveItem(ReorderableList list)
        {
            DestroyImmediate(survival.hurtBubbles[list.index]);
            survival.hurtBubbles.RemoveAt(list.index);

            EditorUtility.SetDirty(target);
        }
    }
}
