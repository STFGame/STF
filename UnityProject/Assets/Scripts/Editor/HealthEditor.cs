using Survival;
using UnityEditor;
using UnityEngine;

namespace Character.Health
{
    [CustomEditor(typeof(CharacterHealth))]
    public class HealthEditor : Editor
    {
        private CharacterHealth characterHealth;

        private void OnEnable()
        {
            characterHealth = (CharacterHealth)target;
        }

        //public override void OnInspectorGUI()
        //{

            //characterHealth.healthType = (HealthType)EditorGUILayout.EnumPopup("Health Type", characterHealth.healthType);

            //if (characterHealth.healthType == HealthType.Regular)
            //{
                //if (characterHealth.healthRegular == null)
                //{
                    //Debug.Log("Created");
                    //characterHealth.healthRegular = new HealthRegular();
                //}

                //characterHealth.healthRegular.healthMainPath = EditorGUILayout.TextField("Health Path", characterHealth.healthRegular.healthMainPath);
                //characterHealth.healthRegular.healthSecondaryPath = EditorGUILayout.TextField("Health Path", characterHealth.healthRegular.healthSecondaryPath);
                //characterHealth.healthRegular.healthBackgroundPath = EditorGUILayout.TextField("Health Path", characterHealth.healthRegular.healthBackgroundPath);

                //characterHealth.healthDisplay = characterHealth.healthRegular;

                //EditorUtility.SetDirty(target);
            //}

            //base.OnInspectorGUI();
        //}
    }
}
