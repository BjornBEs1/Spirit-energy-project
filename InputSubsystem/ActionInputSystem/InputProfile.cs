namespace Spirit_energy_project.InputSubsystem.ActionInputSystem
{
    public sealed class InputProfile
    {
        public string ProfileName { get; set; }
        public InputDevice[] InputDevices { get; set; }
        public InputActionDefinition[] Actions { get; set; }
    }
}
