﻿using RimWorld;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using UnityEngine;
using Verse;

namespace Psysight
{
    public class PatchOperation_SettingActive : PatchOperation
    {
        public List<string> settings;

        public PatchOperation active;

        public PatchOperation inactive;

        public override bool ApplyWorker(XmlDocument xml)
        {
            bool flag = false;

            ModContentPack modContentPack = PsysightMod.mod.Content;
            for (int i = 0; i < settings.Count(); i++)
            {
                if (!PsysightMod.settings.IsValidSetting(settings[i]))
                {
                    LogUtil.LogError("Configuration error in patch, { settings[i]} is not an existing setting in this mod. This can only check existing boolean settings.");
                }
                if (PsysightMod.settings.GetEnabledSettings.Contains(settings[i]))
                {
                    flag = true;
                    break;
                }
            }

            if (flag)
            {
                if (active != null)
                {
                    return active.Apply(xml);
                }
            }
            else if (inactive != null)
            {
                return inactive.Apply(xml);
            }
            return true;
        }
    }
}
