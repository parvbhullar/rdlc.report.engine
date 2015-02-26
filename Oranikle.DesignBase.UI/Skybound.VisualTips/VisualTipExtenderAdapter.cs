using System;
using System.Drawing;
using System.Reflection;
using System.Windows.Forms;

namespace Skybound.VisualTips
{

    internal class VisualTipExtenderAdapter : Skybound.VisualTips.IVisualTipExtender
    {

        private object Target;

        public VisualTipExtenderAdapter(object target)
        {
            Target = target;
        }

        public System.Drawing.Rectangle GetBounds(object component)
        {
            System.Type[] typeArr = new System.Type[] { typeof(object) };
            System.Reflection.MethodInfo methodInfo = Skybound.VisualTips.VisualTipExtenderAdapter.GetMethod(Target, "GetBounds", typeArr);
            if (methodInfo != null)
            {
                object[] objArr = new object[] { component };
                return (System.Drawing.Rectangle)methodInfo.Invoke(Target, objArr);
            }
            return System.Drawing.Rectangle.Empty;
        }

        public object GetChildAtPoint(System.Windows.Forms.Control control, int x, int y)
        {
            System.Type[] typeArr = new System.Type[] {
                                                        typeof(System.Windows.Forms.Control), 
                                                        typeof(int), 
                                                        typeof(int) };
            object[] objArr = new object[] {
                                             control, 
                                             x, 
                                             y };
            return Skybound.VisualTips.VisualTipExtenderAdapter.GetMethod(Target, "GetChildAtPoint", typeArr).Invoke(Target, objArr);
        }

        public System.Type[] GetChildTypes()
        {
            return (System.Type[])Skybound.VisualTips.VisualTipExtenderAdapter.GetMethod(Target, "GetChildTypes", System.Type.EmptyTypes).Invoke(Target, null);
        }

        public object GetParent(object component)
        {
            System.Type[] typeArr = new System.Type[] { typeof(object) };
            object[] objArr = new object[] { component };
            return Skybound.VisualTips.VisualTipExtenderAdapter.GetMethod(Target, "GetParent", typeArr).Invoke(Target, objArr);
        }

        private static System.Reflection.MethodInfo GetMethod(object target, string name, params System.Type[] parameterTypes)
        {
            return target.GetType().GetMethod(name, System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.Public, null, parameterTypes, null);
        }

        public static bool Validate(object target)
        {
            System.Type[] typeArr1 = new System.Type[] {
                                                         typeof(System.Windows.Forms.Control), 
                                                         typeof(int), 
                                                         typeof(int) };
            System.Reflection.MethodInfo methodInfo1 = Skybound.VisualTips.VisualTipExtenderAdapter.GetMethod(target, "GetChildAtPoint", typeArr1);
            System.Type[] typeArr2 = new System.Type[] { typeof(object) };
            System.Reflection.MethodInfo methodInfo2 = Skybound.VisualTips.VisualTipExtenderAdapter.GetMethod(target, "GetParent", typeArr2);
            System.Reflection.MethodInfo methodInfo3 = Skybound.VisualTips.VisualTipExtenderAdapter.GetMethod(target, "GetChildTypes", System.Type.EmptyTypes);
            if ((methodInfo1 != null) && (methodInfo2 != null) && (methodInfo3 != null))
                return methodInfo3.ReturnType == typeof(System.Type[]);
            return false;
        }

    } // class VisualTipExtenderAdapter

}

