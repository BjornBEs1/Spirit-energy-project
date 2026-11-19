using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace Spirit_energy_project
{
    public abstract class GameObject : RawGameObject
    {
        /// <summary>
        /// The name of the given object
        /// </summary>
        public string Name { get; set; }

        internal List<Component> components;

        /// <summary>
        /// The objects transform
        /// </summary>
        // public Transform2D Transform { get; set; }
        public GameObject()
        {
            components = new List<Component>();

            // Transform = new Transform2D();
            // addComponent(Transform);
        }
        public override void ObjInitialize()
        {
            Initialize();
        }
        public override void ObjLoadContents()
        {
            LoadContents();
        }
        public override void ObjUpdate(GameTime gameTime)
        {
            components.ForEach((x) =>
            {
                // if (x is IRendererComponent renderer)
                // {
                //     renderer.Update();
                // }
            });
            Update(gameTime);
        }
        public override void ObjDraw(SpriteBatch spriteBatch)
        {
            Draw(spriteBatch);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="rawGameObject"></param>
        public void AddChlid(GameObject rawGameObject)
        {
            rawGameObject.addChlid(rawGameObject, this);
        }
        public void Destroy()
        {
            Parent.removeChild(this);
        }

        public virtual void Initialize() { }
        public virtual void LoadContents() { }
        public virtual void Update(GameTime gameTime) { }
        public virtual void Draw(SpriteBatch spriteBatch) { }
        public virtual void OnDestroy() { }
    }
}
