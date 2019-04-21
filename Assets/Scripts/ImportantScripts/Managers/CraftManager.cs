using System.Collections.Generic;
using ImportantScripts.CharScripts;
using ImportantScripts.ItemsScripts;
using UnityEngine;

namespace ImportantScripts.Managers
{
    public class Recipe
    {
        public Item FirstItem;
        public Item SecondItem;
        public Item ResultItem;

        public Recipe(Item firstItem, Item secondItem, Item resultItem)
        {
            FirstItem = firstItem;
            SecondItem = secondItem;
            ResultItem = resultItem;
        }
    }

    public static class AllRecipes
    {
        public static Recipe AmmoBulletPack = new Recipe(
            new Item(null,2,null,1,false,0,ItemsTypes.Junk,false,"GunPowder"),
            new Item(null,1,null,1,false,0,ItemsTypes.Junk,false,"Junk"),
            new Item(null,1,"Pack with the some amount of bullets",15,true,7,ItemsTypes.Bullet,false,"AmmoBulletPack"));

        public static List<Recipe> Recipes = new List<Recipe>
        {
            AmmoBulletPack
        };
        
    }
    
    public class CraftManager : MonoBehaviour
    {
        public Recipe[] recipes;
        
        public static CraftManager CraftManagerIn;

        public InventoryGrid[] craftGrid;

        public Recipe whatRecipeWasUsed;
        private void Awake()
        {
            CraftManagerIn = this;
            foreach (var craftgrid in craftGrid)
            {
                craftgrid.itemInThisGrid = null;
            }
        }

        private void Update()
        {
            var trytocraft = true;
            
            foreach (var craftgrid in craftGrid)
            {
                if (craftgrid.itemInThisGrid == null)
                {
                    trytocraft = false;
                    break;
                }
            }

            if (trytocraft)
            {
                if (TryToCraft(craftGrid[0].itemInThisGrid,craftGrid[1].itemInThisGrid))
                {
                   Craft(craftGrid[0].itemInThisGrid,craftGrid[1].itemInThisGrid);
                }
            }
        }

        private bool TryToCraft(Item firstItem, Item secondItem)
        {
            foreach (var recipe in AllRecipes.Recipes)
            {
                if (firstItem.Name == recipe.FirstItem.Name)
                {
                    if (firstItem.amountOfItem > recipe.FirstItem.amountOfItem)
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

            return false;
        }

        public void Craft(Item firstItem, Item secondItem)
        {
            Char.CharIn.Inventory.AddToInventory(whatRecipeWasUsed.ResultItem);
            
            foreach (var grid in InventoryManager.InventoryManagerIn.inventoryGrid)
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

            foreach (var craftgrid in craftGrid)
            {
                craftgrid.itemInThisGrid = null;
            }
            
        }
    }
}