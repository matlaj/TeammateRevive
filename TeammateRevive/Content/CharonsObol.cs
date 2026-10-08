using R2API;
using RoR2;
using TeammateRevive.Localization;
using TeammateRevive.Resources;
using UnityEngine;

namespace TeammateRevive.Content
{
    public class CharonsObol : ContentBase
    {
        public static string Name = "ITEM_CharonsObol";
        public static string NameToken = LanguageConsts.ITEM_CHARONS_OBOL_NAME;
        public static ItemIndex Index;

        public override void Init()
        {
            var generalScale = new Vector3(0.05f, 0.05f, 0.05f);
            var rules = new ItemDisplayRuleDict(new ItemDisplayRule[]
            {
                new()
                {
                    ruleType = ItemDisplayRuleType.ParentedPrefab,
                    followerPrefab = CustomResources.CharonsObolItemPrefab,
                    childName = "Pelvis",
                    localPos = new Vector3(-0.22f, 0f, 0f),
                    localAngles = new Vector3(0f, -0.05f, 0f),
                    localScale = generalScale
                }
            });
            
            ItemAPI.Add(DeprecatedCustomItem.Create(Name, 
                NameToken, 
                LanguageConsts.ITEM_CHARONS_OBOL_DESCRIPTION, 
                LanguageConsts.ITEM_CHARONS_OBOL_DESCRIPTION,
                LanguageConsts.ITEM_CHARONS_OBOL_PICKUP, 
                CustomResources.CharonsObolItemIcon,
                CustomResources.CharonsObolItemPrefab, ItemTier.Tier2, new[]
                {
                    ItemTag.Healing,
                    ItemTag.AIBlacklist
                }, 
                true, false, itemDisplayRules: rules));
        }

        public override void OnItemsAvailable() => Index = ItemCatalog.FindItemIndex(Name);

        /// <summary>
        /// Number of Obols the player can spend on an instant revive. Shared (channeled) Obols only mirror
        /// a nearby teammate's item, so they don't count, and disabled Obols can't be used at all.
        /// </summary>
        public static int GetSpendableCount(Inventory inventory)
        {
            if (inventory.GetItemCountEffective(Index) <= 0) return 0;
            return inventory.GetItemCountTemp(Index) + inventory.GetItemCountRented(Index) + inventory.GetItemCountPermanent(Index);
        }

        /// <summary>
        /// Consumes one spendable Obol. Temporary and rented ones go first, since the game takes those back anyway.
        /// </summary>
        public static bool TrySpend(Inventory inventory)
        {
            if (GetSpendableCount(inventory) <= 0) return false;

            if (inventory.GetItemCountTemp(Index) > 0) inventory.RemoveItemTemp(Index);
            else if (inventory.GetItemCountRented(Index) > 0) inventory.RemoveItemRented(Index);
            else inventory.RemoveItemPermanent(Index);
            return true;
        }
    }
}