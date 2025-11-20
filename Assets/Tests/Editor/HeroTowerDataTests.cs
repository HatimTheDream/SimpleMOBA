#if UNITY_EDITOR
using NUnit.Framework;
using UnityEngine;
using UnityEditor;
using SimpleMOBA.ScriptableObjects.Heroes;
using SimpleMOBA.ScriptableObjects.Towers;
using SimpleMOBA.Gameplay.Heroes;
using SimpleMOBA.Gameplay.Towers;
using System.Reflection;

namespace SimpleMOBA.Tests.Editor
{
    public class HeroTowerDataTests
    {
        [Test]
        public void HeroData_OnValidate_ClampsNegativeValues()
        {
            var data = ScriptableObject.CreateInstance<HeroData>();
            var so = new SerializedObject(data);

            so.FindProperty("maxHealth").floatValue = -1f;
            so.FindProperty("moveSpeed").floatValue = -2f;
            so.FindProperty("attackDamage").floatValue = -3f;
            so.FindProperty("attackRange").floatValue = -4f;
            so.FindProperty("attackCooldown").floatValue = -5f;
            so.ApplyModifiedProperties();

            var onValidate = typeof(HeroData).GetMethod("OnValidate", BindingFlags.Instance | BindingFlags.NonPublic);
            onValidate.Invoke(data, null);

            Assert.GreaterOrEqual(data.MaxHealth, 0f);
            Assert.GreaterOrEqual(data.MoveSpeed, 0f);
            Assert.GreaterOrEqual(data.AttackDamage, 0f);
            Assert.GreaterOrEqual(data.AttackRange, 0f);
            Assert.GreaterOrEqual(data.AttackCooldown, 0f);

            Object.DestroyImmediate(data);
        }

        [Test]
        public void TowerData_OnValidate_ClampsNegativeValues()
        {
            var data = ScriptableObject.CreateInstance<TowerData>();
            var so = new SerializedObject(data);

            so.FindProperty("maxHealth").floatValue = -10f;
            so.FindProperty("attackRange").floatValue = -6f;
            so.FindProperty("attackDamage").floatValue = -7f;
            so.FindProperty("attackCooldown").floatValue = -8f;
            so.ApplyModifiedProperties();

            var onValidate = typeof(TowerData).GetMethod("OnValidate", BindingFlags.Instance | BindingFlags.NonPublic);
            onValidate.Invoke(data, null);

            Assert.GreaterOrEqual(data.MaxHealth, 0f);
            Assert.GreaterOrEqual(data.AttackRange, 0f);
            Assert.GreaterOrEqual(data.AttackDamage, 0f);
            Assert.GreaterOrEqual(data.AttackCooldown, 0f);

            Object.DestroyImmediate(data);
        }

        [Test]
        public void HeroStats_FromData_CopiesValues()
        {
            var data = ScriptableObject.CreateInstance<HeroData>();
            var so = new SerializedObject(data);

            so.FindProperty("maxHealth").floatValue = 220f;
            so.FindProperty("moveSpeed").floatValue = 5f;
            so.FindProperty("attackDamage").floatValue = 15f;
            so.FindProperty("attackRange").floatValue = 4f;
            so.FindProperty("attackCooldown").floatValue = 0.9f;
            so.ApplyModifiedProperties();

            var stats = HeroStats.FromData(data);
            Assert.AreEqual(220f, stats.MaxHealth);
            Assert.AreEqual(5f, stats.MoveSpeed);
            Assert.AreEqual(15f, stats.AttackDamage);
            Assert.AreEqual(4f, stats.AttackRange);
            Assert.AreEqual(0.9f, stats.AttackCooldown);

            Object.DestroyImmediate(data);
        }

        [Test]
        public void TowerStats_FromData_CopiesValues()
        {
            var data = ScriptableObject.CreateInstance<TowerData>();
            var so = new SerializedObject(data);

            so.FindProperty("maxHealth").floatValue = 600f;
            so.FindProperty("attackRange").floatValue = 8f;
            so.FindProperty("attackDamage").floatValue = 20f;
            so.FindProperty("attackCooldown").floatValue = 1.2f;
            so.ApplyModifiedProperties();

            var stats = TowerStats.FromData(data);
            Assert.AreEqual(600f, stats.MaxHealth);
            Assert.AreEqual(8f, stats.AttackRange);
            Assert.AreEqual(20f, stats.AttackDamage);
            Assert.AreEqual(1.2f, stats.AttackCooldown);

            Object.DestroyImmediate(data);
        }
    }
}
#endif
