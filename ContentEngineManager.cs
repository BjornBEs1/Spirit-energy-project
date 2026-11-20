/*
 * File: ContentEngineManager.cs
 * File Created: 20 Nov 2026
 * Author: BjornBEs
 * -----
 * Last Modified: 20 Nov 2026
 * Modified By: BjornBEs
 * -----
 */

using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spirit_energy_project
{
    public enum ContentType
    {
        Texture2D,
    }
    public class ContentValue
    {
        public object Value { get; set; }
        public ContentType ContentType { get; set; }
        public ContentValue(object value, ContentType contentType)
        {
            Value = value;
            ContentType = contentType;
        }
    }
    public class ContentEngineManager
    {
        internal ContentManager content;

        private Dictionary<string, string> contentKeyCache;
        private Dictionary<string, ContentValue> contentCache;
        public static ContentEngineManager Instance;

        internal ContentEngineManager(ContentManager _content)
        {
            if (Instance != null)
            {
                return;
            }
            Instance = this;

            contentCache = new Dictionary<string, ContentValue>();
            contentKeyCache = new Dictionary<string, string>();
            content = _content;
        }

        internal Texture2D loadTexture2D(string key, string path)
        {
            Texture2D texture = content.Load<Texture2D>(path);
            contentCache.Add(key, new ContentValue(texture, ContentType.Texture2D));
            return texture;
        }

        public void LoadTexture2D(string key, string path)
        {
            if (contentKeyCache.TryGetValue(key, out string path1))
            {
                if (path1 == path)
                {
                    return;
                }
                throw new Exception();
            }
            contentKeyCache.Add(key, path);
        }

        public Texture2D GetTexture2D(string key)
        {
            if (contentCache.TryGetValue(key, out ContentValue contentValue))
            {
                if (contentValue.ContentType == ContentType.Texture2D)
                {
                    return (Texture2D)contentValue.Value;
                }
            }
            else
            {
                if (contentKeyCache.TryGetValue(key, out string path))
                {
                    return loadTexture2D(key, path);
                }
            }
            return null;
        }
    }
}
