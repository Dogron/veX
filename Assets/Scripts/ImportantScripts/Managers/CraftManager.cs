using System.Collections.Generic;
using System.Linq;
using ImportantScripts.CharScripts;
using ImportantScripts.ItemsScripts;
using UnityEngine;

namespace ImportantScripts.Managers
{
    public class Recipe
    {
        public List<Item> ItemsNeedToCraft;
        public Item ResultItem;

        public Recipe(List<Item> itemsNeedToCraft, Item resultItem)
        {
            ItemsNeedToCraft = itemsNeedToCraft;
            ResultItem = resultItem;
        }
    }

    public static class AllRecipes
    {
        // ReSharper disable once MemberCanBePrivate.Global
        // ReSharper disable FieldCanBeMadeReadOnly.Global
        public static Recipe AmmoBulletPack = new Recipe
            (
                 new List<Item>{new Item(null,2,null,1,false,0,ItemsTypes.Junk,false,"GunPowder"),new Item(null,1,null,1,false,0,ItemsTypes.Junk,false,"Junk")}, 
                 new Item(null,1,"Pack with the some amount of bullets",15,true,7,ItemsTypes.Bullet,false,"BulletPack")
            );

        public static List<Recipe> Recipes = new List<Recipe>
        {
            AmmoBulletPack
        };
    }
    
    public class CraftManager : MonoBehaviour
    {
        public Recipe[] recipes;
        
        public static CraftManager CraftManagerIn;

        public List<InventoryGrid> craftGrid;

        public Recipe whatRecipeWasUsed;
        private void Awake()
        {
            CraftManagerIn = this;
            foreach (var craftgrid in craftGrid)
            {
                craftgrid.itemInThisGrid = null;
            }
        }

        private void FixedUpdate()
        {
            var trytocraft = craftGrid.All(craftgrid => craftgrid.itemInThisGrid != null);

            if (trytocraft)
            {
                if (TryToCraft())
                {
                   Craft();
                }
            }
        }

        private bool TryToCraft()
        {
            foreach (var recipe in AllRecipes.Recipes)
            {
                var i = 0;
                foreach (var item in recipe.ItemsNeedToCraft)
                {
                    var count = 0;
                    foreach (var grid in craftGrid)
                    {
                        if (grid.itemInThisGrid.Name == item.Name && grid.itemInThisGrid.amountOfItem >= item.amountOfItem) count++;
                    }
                                          
                    i += count;
                }

                if (i == recipe.ItemsNeedToCraft.Count)
                {
                    whatRecipeWasUsed = recipe;
                    return true;
                }
            }
            
            /*foreach (var recipe in AllRecipes.Recipes)
            {
                if (firstItem.Name == recipe.FirstItem.Name)
                {
                    if (firstItem.amountOfItem >= recipe.FirstItem.amountOfItem)
                    {
                        if (secondItem.Name == recipe.SecondItem.Name)
                        {
                            if (secondItem.amountOfItem > recipe.SecondItem.amountOfItem)
                            {
                                whatRecipeWasUsed = recipe;
                                return true;
                            }
                        }
                    }
                }

                if (firstItem.Name == recipe.SecondItem.Name)
                {
                    if (firstItem.amountOfItem > recipe.SecondItem.amountOfItem)
                    {
                        if (secondItem.Name == recipe.FirstItem.Name)
                        {
                            if (secondItem.amountOfItem > recipe.FirstItem.amountOfItem)
                            {
                                whatRecipeWasUsed = recipe;
                                return true;
                            }
                        }
                    }
                }
            }
            */

            for (var i = 0; i < 2; i++)
            {
                Char.CharIn.Inventory.AddToInventory(new Item(craftGrid[i].itemInThisGrid));
            }

            foreach (var grid in craftGrid)
            {
                grid.itemInThisGrid.amountOfItem = 0;
            }
            
            return false;
        }

        public void Craft()
        {
            Char.CharIn.Inventory.AddToInventory(whatRecipeWasUsed.ResultItem);

            foreach (var grid in craftGrid)
            {
                if (grid.itemInThisGrid.Name == whatRecipeWasUsed.ItemsNeedToCraft[0].Name)
                {
                    grid.itemInThisGrid.amountOfItem -= whatRecipeWasUsed.ItemsNeedToCraft[0].amountOfItem;
                }

                else
                {
                    grid.itemInThisGrid.amountOfItem -= whatRecipeWasUsed.ItemsNeedToCraft[1].amountOfItem;
                }
            }
            
            for (var i = 0; i < 2; i++)
            {
                Char.CharIn.Inventory.AddToInventory(new Item(craftGrid[i].itemInThisGrid));
            }

            foreach (var grid in craftGrid)
            {
                grid.itemInThisGrid.amountOfItem = 0;
            }
            
           /* foreach (var invgrid in InventoryManager.InventoryManagerIn.inventoryGrid)
            {
                foreach (var craftgrid in craftGrid)
                {
                    if (craftgrid.itemInThisGrid == invgrid.itemInThisGrid)
                    {
                        foreach (var itemneedtocraft in whatRecipeWasUsed.ItemsNeedToCraft)
                        {
                            if (itemneedtocraft.Name == craftgrid.itemInThisGrid.Name)
                            {
                                invgrid.itemInThisGrid.amountOfItem -= itemneedtocraft.amountOfItem;
                            }
                        }
                    }
                }
            }
            */
            
            /* foreach (var grid in InventoryManager.InventoryManagerIn.inventoryGrid)
            {
                if (grid.itemInThisGrid == firstItem)
                {
                    if (whatRecipeWasUsed.FirstItem.Name == firstItem.Name)
                    {
                        grid.itemInThisGrid.amountOfItem -= whatRecipeWasUsed.FirstItem.amountOfItem;
                    }

                    else
                    {
                        grid.itemInThisGrid.amountOfItem -= whatRecipeWasUsed.SecondItem.amountOfItem;
                    }
                }

                if (grid.itemInThisGrid != secondItem) continue;
                
                if (whatRecipeWasUsed.FirstItem.Name == secondItem.Name)
                {
                    grid.itemInThisGrid.amountOfItem -= whatRecipeWasUsed.FirstItem.amountOfItem;
                }

                else
                {
                    grid.itemInThisGrid.amountOfItem -= whatRecipeWasUsed.SecondItem.amountOfItem;
                }
            }
**/
        }
    }
}