using LethalCompanyInputUtils.Api;
using LethalCompanyInputUtils.BindingPathEnums;
using UnityEngine.InputSystem;

namespace MachineGun
{
    public class MachineGunButton : LcInputActions
    {
        // Set up machine gun firing keybind
        [InputAction(KeyboardControl.F, Name = "Machine Gun", KbmInteractions = "hold")]
        public InputAction FireShotgunKey { get; set; }
    }
}
