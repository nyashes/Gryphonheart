﻿namespace WoWSimulator.UISimulation.UiObjects
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Runtime.CompilerServices;
    using BlizzardApi.WidgetInterfaces;
    using CsLua.Collection;

    public class Script<T1,T2> : IScript<T1,T2>
    {
        private readonly Dictionary<T1, Action<T2, object, object, object, object>> scripts = new Dictionary<T1, Action<T2, object, object, object, object>>();
        private readonly Dictionary<T1, List<Action<T2, object, object, object, object>>> hookedScripts = new CsLuaDictionary<T1, List<Action<T2, object, object, object, object>>>();
        private T2 self;

        public Script(T2 self)
        {
            this.self = self;
        }

        public void SetScript(T1 handler, Action<T2> function)
        {
            this.SetScript(handler, (frame, o1, o2, o3, o4) => { function(frame); });
        }

        public void SetScript(T1 handler, Action<T2, object> function)
        {
            this.SetScript(handler, (frame, o1, o2, o3, o4) => { function(frame, o1); });
        }

        public void SetScript(T1 handler, Action<T2, object, object> function)
        {
            this.SetScript(handler, (frame, o1, o2, o3, o4) => { function(frame, o1, o2); });
        }

        public void SetScript(T1 handler, Action<T2, object, object, object> function)
        {
            this.SetScript(handler, (frame, o1, o2, o3, o4) => { function(frame, o1, o2, o3); });
        }

        public void SetScript(T1 handler, Action<T2, object, object, object, object> function)
        {
            this.scripts[handler] = function;
        }

        public Action<T2, object, object, object, object> GetScript(T1 handler)
        {
            return this.scripts.ContainsKey(handler) ? this.scripts[handler] : null;
        }

        public bool HasScript(T1 handler)
        {
            return this.scripts.ContainsKey(handler);
        }

        public void HookScript(T1 handler, Action<T2, object, object, object, object> function)
        {
            if (!this.hookedScripts.ContainsKey(handler))
            {
                this.hookedScripts[handler] = new List<Action<T2, object, object, object, object>>() { function };
            }
            else
            {
                this.hookedScripts[handler].Add(function);
            }
        }

        public void ExecuteScript(T1 handler, object arg1, object arg2, object arg3, object arg4)
        {
            if (this.scripts.ContainsKey(handler))
            {
                this.scripts[handler](this.self, arg1, arg2, arg3, arg4);
            }

            if (this.hookedScripts.ContainsKey(handler))
            {
                this.hookedScripts[handler].ForEach(script => script(this.self, arg1, arg2, arg3, arg4));
            }
        }
    }
}