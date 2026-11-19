using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.CompilerServices.RuntimeHelpers;

namespace Spirit_energy_project.InputSubsystem
{
    public static class Input
    {
        internal static InputSystem _system;
        public static InputSystem InputSystem
        {
            get => _system;
        }

        // -------- Keyboard --------

        public static bool IsKeyDown(KeyCode key) => _system.IsKeyDown(key);
        public static bool IsKeyPressed(KeyCode key) => _system.IsKeyPressed(key);
        public static bool IsKeyReleased(KeyCode key) => _system.IsKeyReleased(key);

        // -------- Mouse --------

        public static Vector2 MousePosition => _system.MousePosition;

        public static Vector2 MousePositionDelta => _system.MousePositionDelta;
        public static float MouseScrollDelta => _system.VerticalScrollWheelValueDelta;

        public static bool IsLeftClickPressed() => _system.IsMousePressed(MouseButton.Left);

        // Actions

        /// <summary>
        /// 
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public static bool IsActionDown(string key) => _system.IsActionDown(key);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public static bool IsActionPressed(string key) => _system.IsActionPressed(key);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public static bool IsActionReleased(string key) => _system.IsActionReleased(key);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public static float GetAxis(string key)
        {
            float valueRaw = _system.GetAxis(key);
            float value = valueRaw;
            if (0f > valueRaw)
            {
                value = -value;
            }
            if (0.000001f > value)
            {
                valueRaw = 0;
            }
            return valueRaw;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public static float GetAxisRaw(string key)
        {
            float valueRaw = _system.GetAxis(key);
            return valueRaw;
        }
    }
}
