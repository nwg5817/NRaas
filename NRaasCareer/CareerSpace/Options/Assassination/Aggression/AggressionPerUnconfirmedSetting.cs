﻿using NRaas.CommonSpace.Options;
using Sims3.Gameplay.Abstracts;
using Sims3.Gameplay.Actors;
using Sims3.Gameplay.Autonomy;
using Sims3.Gameplay.CAS;
using Sims3.Gameplay.Interactions;
using Sims3.Gameplay.Interfaces;
using Sims3.Gameplay.Skills;
using Sims3.Gameplay.Utilities;
using Sims3.SimIFace;
using Sims3.UI;
using System;
using System.Collections.Generic;
using System.Text;

namespace NRaas.CareerSpace.Options.Assassination.Aggression
{
    public class AggressionPerUnconfirmedSetting : IntegerSettingOption<GameObject>, IAggressionOption
    {
        protected override int Value
        {
            get
            {
                return NRaas.CareerSpace.Skills.Assassination.Settings.mAggressionPerUnconfirmed;
            }
            set
            {
                NRaas.CareerSpace.Skills.Assassination.Settings.mAggressionPerUnconfirmed = value;
            }
        }

        public override string GetTitlePrefix()
        {
            return "AggressionPerUnconfirmed";
        }

        public override ITitlePrefixOption ParentListingOption
        {
            get { return new ListingOption(); }
        }

        protected override bool Allow(GameHitParameters<GameObject> parameters)
        {
            return (Skills.Assassination.StaticGuid != SkillNames.None);
        }
    }
}