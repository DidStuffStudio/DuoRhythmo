﻿using System;
using System.Collections.Generic;

namespace TS
{
    public class TSPluginManager
    {
        private static List<Type> pluginList = new List<Type>()
        {
            typeof(TSPluginX),
            typeof(TSPluginY),
            typeof(TSPluginZ),
            typeof(TSPluginRotationX),
            typeof(TSPluginRotationY),
            typeof(TSPluginRotationZ),
            typeof(TSPluginScale),
            typeof(TSPluginScaleX),
            typeof(TSPluginScaleY),
            typeof(TSPluginScaleZ),
            typeof(TSPluginSizeDeltaX),
            typeof(TSPluginSizeDeltaY),
            typeof(TSPluginPositionX),
            typeof(TSPluginPositionY),
            typeof(TSPluginPositionZ),
            typeof(TSPluginAlpha),
            typeof(TSPluginAutoAlpha),
            typeof(TSPluginLocalX),
            typeof(TSPluginLocalY),
            typeof(TSPluginLocalZ),
        };
        private static Dictionary<string, Type> plugins;

        public static void Activate(Type pluginType)
        {
            TSPlugin plugin = Activator.CreateInstance(pluginType) as TSPlugin;
            if (plugin != null)
            {
                plugins[plugin.PropertyName] = pluginType;
            }
        }

        public static void Init()
        {
            if (plugins == null)
            {
                plugins = new Dictionary<string, Type>();
                foreach (Type pluginType in pluginList)
                {
                    Activate(pluginType);
                }
                pluginList = null;
            }
        }

        public static TSPlugin GetPlugin(string propertyName)
        {
            if (plugins.ContainsKey(propertyName))
            {
                return Activator.CreateInstance(plugins[propertyName]) as TSPlugin;
            }
            return null;
        }
    }

}