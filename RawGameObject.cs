using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spirit_energy_project
{
    public abstract class RawGameObject
    {
        internal List<GameObject> children;
        public GameObject Parent { get; private set; }
        public RawGameObject()
        {
            children = new List<GameObject>();
        }

        public abstract void ObjInitialize();
        public abstract void ObjLoadContents();
        public abstract void ObjUpdate(GameTime gameTime);
        public abstract void ObjDraw(SpriteBatch spriteBatch);

        internal void intInitialize()
        {
            ObjInitialize();
            // children.ForEach((x) => x.intInitialize());
        }
        internal void intLoadContents()
        {
            ObjLoadContents();
            // children.ForEach((x) => x.intLoadContents());
        }
        internal void intUpdate(GameTime gameTime)
        {
            ObjUpdate(gameTime);
            List<GameObject> objects = children.ToList();
            objects.ForEach((x) =>
            {
                x.intUpdate(gameTime);
            });
        }
        internal void intDraw(SpriteBatch spriteBatch)
        {
            ObjDraw(spriteBatch);
            children.ForEach((x) => x.intDraw(spriteBatch));
        }

        internal void DrawBegin(SpriteBatch spriteBatch)
        {
            spriteBatch.Begin(samplerState: SamplerState.PointWrap);
        }

        internal void DrawEnd(SpriteBatch spriteBatch)
        {
            spriteBatch.End();
        }

        internal void addChlid(GameObject gameObject, GameObject parent)
        {
            gameObject.Parent = parent;
            parent.children.Add(gameObject);
        }

        internal void removeChild(GameObject rawGameObject)
        {
            children.Remove(rawGameObject);
        }
        public void RemoveChild(GameObject rawGameObject)
        {
            removeChild(rawGameObject);
        }
    }
}
