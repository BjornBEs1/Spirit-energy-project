using System.Text.Json.Serialization;

namespace Spirit_energy_project.InputSubsystem.ActionInputSystem
{
    public class InputActionDefinition
    {
        public string Action { get; set; }
        public ActionType ActionType { get; set; }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public InputBinding[] Bindings { get; set; } = null;
    }
}
