namespace Spirit_energy_project.InputSubsystem.ActionInputSystem
{
    public struct ActionState
    {
        public bool IsRaw;
        public bool BoolDown;
        public bool BoolPressed;
        public bool BoolReleased;
        public float FloatValue;

        public ActionState()
        {
            BoolDown = false;
            BoolPressed = false;
            BoolReleased = false;
            FloatValue = 0;
        }
    }

}
