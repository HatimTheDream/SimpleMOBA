#if UNITY_EDITOR
using NUnit.Framework;
using UnityEngine;
using UnityEditor;
using SimpleMOBA.ScriptableObjects.Minions;
using SimpleMOBA.Gameplay.Minions;
using System.Reflection;

namespace SimpleMOBA.Tests.Editor
{
    public class MinionDataTests
    {
        [Test]
        public void OnValidate_ClampsNegativeValues()
        {
            var data = ScriptableObject.CreateInstance<MinionData>();
            var so = new SerializedObject(data);

            var propMax = so.FindProperty("maxHealth");
            var propMove = so.FindProperty("moveSpeed");
            var propAttack = so.FindProperty("attackDamage");
            var propRange = so.FindProperty("attackRange");
            var propCd = so.FindProperty("attackCooldown");

            propMax.floatValue = -100f;
            propMove.floatValue = -5f;
            propAttack.floatValue = -10f;
            propRange.floatValue = -2f;
            propCd.floatValue = -1f;
            so.ApplyModifiedProperties();

            // OnValidate is private; invoke it via reflection.
            var onValidate = typeof(MinionData).GetMethod("OnValidate", BindingFlags.Instance | BindingFlags.NonPublic);
            Assert.IsNotNull(onValidate, "OnValidate method should exist on MinionData");
            onValidate.Invoke(data, null);

            Assert.GreaterOrEqual(data.MaxHealth, 0f);
            Assert.GreaterOrEqual(data.MoveSpeed, 0f);
            Assert.GreaterOrEqual(data.AttackDamage, 0f);
            Assert.GreaterOrEqual(data.AttackRange, 0f);
            Assert.GreaterOrEqual(data.AttackCooldown, 0f);

            Object.DestroyImmediate(data);
        }

        [Test]
        public void MinionStats_FromData_CopiesValues()
        {
            var data = ScriptableObject.CreateInstance<MinionData>();
            var so = new SerializedObject(data);

            so.FindProperty("maxHealth").floatValue = 150f;
            so.FindProperty("moveSpeed").floatValue = 3.5f;
            so.FindProperty("attackDamage").floatValue = 9f;
            so.FindProperty("attackRange").floatValue = 2.5f;
            so.FindProperty("attackCooldown").floatValue = 0.8f;
            so.ApplyModifiedProperties();

            var stats = MinionStats.FromData(data);

            Assert.AreEqual(150f, stats.MaxHealth);
            Assert.AreEqual(3.5f, stats.MoveSpeed);
            Assert.AreEqual(9f, stats.AttackDamage);
            Assert.AreEqual(2.5f, stats.AttackRange);
            Assert.AreEqual(0.8f, stats.AttackCooldown);

            Object.DestroyImmediate(data);
        }
    }
}
#endif
