using System;

namespace ThemeEditorMaps.Redirection
{
    [AttributeUsage(AttributeTargets.Method)]
    public class RedirectMethodAttribute : Attribute
    {
        public RedirectMethodAttribute()
        {
            this.OnCreated = false;
        }

        public RedirectMethodAttribute(bool onCreated)
        {
            this.OnCreated = onCreated;
        }

        public bool OnCreated { get; }
    }
}
