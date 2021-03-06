﻿using NRaas.CommonSpace.Interactions;
using NRaas.DebugEnablerSpace.Interfaces;
using Sims3.Gameplay;
using Sims3.Gameplay.Abstracts;
using Sims3.Gameplay.Actors;
using Sims3.Gameplay.ActorSystems;
using Sims3.Gameplay.Autonomy;
using Sims3.Gameplay.CAS;
using Sims3.Gameplay.Controllers;
using Sims3.Gameplay.Core;
using Sims3.Gameplay.EventSystem;
using Sims3.Gameplay.Interactions;
using Sims3.Gameplay.Interfaces;
using Sims3.Gameplay.ObjectComponents;
using Sims3.Gameplay.Objects;
using Sims3.Gameplay.Utilities;
using Sims3.Gameplay.UI;
using Sims3.SimIFace;
using Sims3.UI;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace NRaas.DebugEnablerSpace.Interactions
{
    public class ShowResourceKey : DebugEnablerInteraction<GameObject>
    {
        public static readonly InteractionDefinition Singleton = new Definition();

        public override void AddPair(GameObject obj, List<InteractionObjectPair> list)
        {
            list.Add(new InteractionObjectPair(Singleton, obj));
        }

        public override bool Run()
        {
            try
            {
                DebugEnabler.WriteLog(Target.GetResourceKey().ToString());

                SimpleMessageDialog.Show(Common.Localize("ResourceKey:MenuName"), Target.GetResourceKey().ToString());
            }
            catch (Exception exception)
            {
                Common.Exception(Actor, Target, exception);
            }
            return true;
        }

        [DoesntRequireTuning]
        private sealed class Definition : DebugEnablerDefinition<ShowResourceKey>
        {
            public override string GetInteractionName(IActor a, GameObject target, InteractionObjectPair interaction)
            {
                string name = null;

                if (target != null)
                {
                    try
                    {
                        name = target.CatalogName;
                    }
                    catch
                    { }
                }

                return Common.Localize("ResourceKey:MenuName", false, new object[] { name });
            }

            public override bool Test(IActor a, GameObject target, bool isAutonomous, ref GreyedOutTooltipCallback greyedOutTooltipCallback)
            {
                if (target is Lot) return false;

                return base.Test(a, target, isAutonomous, ref greyedOutTooltipCallback);
            }
        }
    }
}