using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Reflection;

namespace WPFResourceManager.Resources
{
    public static class SharedResources
    {
        #region Properties

        public static readonly DependencyProperty MergedDictionariesProperty = DependencyProperty.RegisterAttached("MergedDictionaries",
                                                                                                                    typeof(string), 
                                                                                                                    typeof(SharedResources),
                                                                                                                    new FrameworkPropertyMetadata((string)null,
                                                                                                                    new PropertyChangedCallback(OnMergedDictionariesChanged)));



        #endregion

        #region Accessor Functions

        public static string GetMergedDictionaries(DependencyObject d)
        {
            return (string)d.GetValue(MergedDictionariesProperty);
        }

        public static void SetMergedDictionaries(DependencyObject d, string value)
        {
            d.SetValue(MergedDictionariesProperty, value);
        }

        #endregion
     
        #region Members

        private static Dictionary<string, WeakReference> m_shared_dictionaries = new Dictionary<string, WeakReference>();

        #endregion

        #region Methods

        private static void OnMergedDictionariesChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (!string.IsNullOrEmpty(e.NewValue as string))
            {
                foreach (string dictionary_name in (e.NewValue as string).Split(';'))
                {
                    ResourceDictionary dictionary = GetResourceDictionary(dictionary_name);

                    if (dictionary != null)
                    {
                        if (d is FrameworkElement)
                        {
                            (d as FrameworkElement).Resources.MergedDictionaries.Add(dictionary);
                        }
                        else if (d is FrameworkContentElement)
                        {
                            (d as FrameworkContentElement).Resources.MergedDictionaries.Add(dictionary);
                        }
                    }
                }
            }
        }

        private static ResourceDictionary GetResourceDictionary(string dictionary_name)
        {
            ResourceDictionary result = null;

            if (m_shared_dictionaries.ContainsKey(dictionary_name))
            {
                result = (ResourceDictionary)m_shared_dictionaries[dictionary_name].Target;
            }
            if (result == null)
            {
                string assembly_name = System.IO.Path.GetFileNameWithoutExtension(Assembly.GetExecutingAssembly().ManifestModule.Name);

                result = Application.LoadComponent(new Uri(assembly_name + ";component/Resources/" + dictionary_name + ".xaml", UriKind.Relative)) as ResourceDictionary;

                m_shared_dictionaries[dictionary_name] = new WeakReference(result);
            }
            return result;
        }

        #endregion

    }
}