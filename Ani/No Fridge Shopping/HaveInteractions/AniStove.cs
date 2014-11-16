﻿
using Sims3.Gameplay.Objects.CookingObjects;
using Sims3.Gameplay.Interactions;
using Sims3.Gameplay.Objects.Appliances;
using Sims3.Gameplay.Abstracts;
using Sims3.Gameplay.Objects.FoodObjects;
using Sims3.Gameplay.ActorSystems;
using Sims3.Gameplay.Actors;
using Sims3.Gameplay.Autonomy;
using System.Collections.Generic;
using Sims3.SimIFace;
namespace ani_GroceryShopping
{
    internal class OverridedStove_Have : Interaction<Sim, Stove>
    {
        private sealed class Definition : OverridedFoodMenuInteractionDefinition<Stove, OverridedStove_Have>
        {
            public Definition()
            {
            }
            public Definition(string menuText, Recipe recipe, string[] menuPath, GameObject objectClickedOn, Recipe.MealDestination destination, Recipe.MealQuantity quantity, Recipe.MealRepetition repetition, bool bWasHaveSomething, int cost)
                : base(menuText, recipe, menuPath, objectClickedOn, destination, quantity, repetition, bWasHaveSomething, cost)
            {
            }
            protected override OverridedFoodMenuInteractionDefinition<Stove, OverridedStove_Have> Create(string menuText, Recipe recipe, string[] menuPath, GameObject objectClickedOn, Recipe.MealDestination destination, Recipe.MealQuantity quantity, Recipe.MealRepetition repetition, bool bWasHaveSomething, int cost)
            {
                return new OverridedStove_Have.Definition(menuText, recipe, menuPath, objectClickedOn, destination, quantity, repetition, bWasHaveSomething, cost);
            }
            public override void AddInteractions(InteractionObjectPair iop, Sim sim, Stove stove, List<InteractionObjectPair> results)
            {
                base.AddFoodPrepInteractions(iop, sim, results, iop.Target as GameObject);
            }
            protected override bool SpecificTest(Sim a, Stove target, bool isAutonomous, ref GreyedOutTooltipCallback greyedOutTooltipCallback)
            {
                if (!target.CommonMakeTest(a) || target.HasGasLeak)
                {
                    return false;
                }
                Recipe.CanMakeFoodTestResult result = Food.CanMake(this.ChosenRecipe, true, true, Recipe.MealTime.DO_NOT_CHECK, this.Repetition, target.LotCurrent, a, this.Quantity, this.Cost, this.ObjectClickedOn);
                return Food.PrepareTestResultCheckAndGrayedOutPieMenuSet(a, this.ChosenRecipe, result, ref greyedOutTooltipCallback);
            }
        }
        public static readonly InteractionDefinition Singleton = new OverridedStove_Have.Definition();
        public override bool Run()
        {
            OverridedStove_Have.Definition definition = base.InteractionDefinition as OverridedStove_Have.Definition;
            TraitFunctions.CheckForNeuroticAnxiety(this.Actor, TraitFunctions.NeuroticTraitAnxietyType.Stove);
            return Fridge.ForcePushFridgeHave(this.Actor, this.Target, definition.ChosenRecipe, definition.MenuText, definition.MenuPath, definition.ObjectClickedOn, definition.Destination, definition.Quantity, definition.Repetition, false, definition.Cost);
        }
    }
}
