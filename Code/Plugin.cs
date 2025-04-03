using BepInEx;
using RoR2;
using RoR2.Skills;

namespace ClassicAcridSprintCanceling
{
    [BepInPlugin(PluginGUID, PluginName, PluginVersion)]
    public class Plugin : BaseUnityPlugin
    {
        public const string PluginGUID = PluginAuthor + "." + PluginName;
        public const string PluginAuthor = "LordVGames";
        public const string PluginName = "ClassicAcridSprintCanceling";
        public const string PluginVersion = "1.0.0";
        public void Awake()
        {
            Log.Init(Logger);
            On.EntityStates.Croco.Slash.OnExit += Slash_OnExit;
        }

        // putting this into the same file since it's such a tiny change we don't need a main file
        private void Slash_OnExit(On.EntityStates.Croco.Slash.orig_OnExit orig, EntityStates.Croco.Slash self)
        {
            // resets the combo if the attack was sprint canceled
            // yes it's that easy
            if (self.characterBody.isSprinting)
            {
                SteppedSkillDef.InstanceData actualSkill = (self.skillLocator.primary.skillInstanceData as SteppedSkillDef.InstanceData);
                actualSkill.step = 0;
            }

            orig(self);
        }
    }
}