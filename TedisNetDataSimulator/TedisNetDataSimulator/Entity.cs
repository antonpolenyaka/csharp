using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace TedisNetDataSimulator
{
    public class Entity
    {
        #region public methods
        /// <summary>
        /// Create a new Instance of the object and copy all the properties
        /// </summary>
        /// <typeparam name="T">Type of the object</typeparam>
        /// <param name="source">Object to clone</param>
        /// <returns>the clone object</returns>
        public static T Clone<T>(T source)
        {
            T result = (T)Activator.CreateInstance(source.GetType());
            CopyValues<T>(source, result);
            return result;
        }

        /// <summary>
        /// Copy all properties of the object
        /// </summary>
        /// <typeparam name="T">Type of the object</typeparam>
        /// <param name="source">source object</param>
        /// <param name="destination">destination object</param>
        public static void CopyValues<T>(T source, T destination)
        {
            CopyValues<T>(source, destination, true);
        }

        /// <summary>
        /// Copy all properties of the object. Don't copy ref of generic classes
        /// </summary>
        /// <typeparam name="T">Type of the object</typeparam>
        /// <param name="source">source object</param>
        /// <param name="destination">destination object</param>
        public static void CopyValuesBasicTypes<T>(T source, T destination)
        {
            CopyValuesBasicTypes<T, T>(source, destination);
        }


        /// <summary>
        /// Copy all properties of the source object marked as DataMember
        /// </summary>
        /// <typeparam name="T">Type of the object</typeparam>
        /// <param name="source">source object</param>
        /// <param name="destination">destination object</param>
        public static void CopyDataMembers<T>(T source, T destination)
        {
            CopyValues<T>(source, destination, false);
        }

        /// <summary>
        /// Copy all properties of the object
        /// </summary>
        /// <typeparam name="S">Type of the Source object</typeparam>
        /// <typeparam name="D">Type of the Destination object</typeparam>
        /// <param name="source">source object</param>
        /// <param name="destination">destination object</param>
        public static void CopyValues<S, D>(S source, D destination)
        {
            CopyValues<S, D>(source, destination, true, true);
        }

        /// <summary>
        /// Copy all properties of the object
        /// </summary>
        /// <typeparam name="S">Type of the Source object</typeparam>
        /// <typeparam name="D">Type of the Destination object</typeparam>
        /// <param name="source">source object</param>
        /// <param name="destination">destination object</param>
        public static void CopyValuesBasicTypes<S, D>(S source, D destination)
        {
            CopyValues<S, D>(source, destination, true, false);
        }


        /// <summary>
        /// Copy all properties of the source object marked as DataMember
        /// </summary>
        /// <typeparam name="S">Type of the Source object</typeparam>
        /// <typeparam name="D">Type of the Destination object</typeparam>
        /// <param name="source">source object</param>
        /// <param name="destination">destination object</param>
        public static void CopyDataMembers<S, D>(S source, D destination)
        {
            CopyValues<S, D>(source, destination, false, false);
        }


        public static bool AreEqualValues<T>(T itemA, T itemB, bool onlySerializable)
        {
            IEnumerable<PropertyInfo> propInfo;

            BindingFlags bindingAttr = BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance;

            PropertyInfo[] fieldsS = itemA.GetType().GetProperties(bindingAttr);
            if (onlySerializable)
                propInfo = fieldsS.Where(pi => pi.CanRead && pi.CanWrite && pi.PropertyType.IsSerializable);
            else
                propInfo = fieldsS.Where(pi => pi.CanRead && pi.CanWrite);

            object aValue;
            object bValue;
            foreach (PropertyInfo pis in propInfo)
            {
                aValue = pis.GetValue(itemA);
                bValue = pis.GetValue(itemB);
                if ((aValue != null && bValue != null && !aValue.Equals(bValue)) || (aValue == null && bValue != null) || (aValue != null && bValue == null))
                {
                    return false;
                }
            }

            return true;

        }

        public static void CopyValue<S, D>(S source, D destination, string propertyName)
        {
            BindingFlags bindingAttr = BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance;

            PropertyInfo propInfoS = source.GetType().GetProperties(bindingAttr).Where(pi => pi.CanRead && pi.PropertyType.IsSerializable && pi.Name.Equals(propertyName)).FirstOrDefault();
            PropertyInfo propInfoD = destination.GetType().GetProperties(bindingAttr).Where(pi => pi.CanWrite && pi.PropertyType.IsSerializable && pi.Name.Equals(propertyName)).FirstOrDefault();

            if (propInfoS == null || propInfoD == null) return;

            try
            {
                object sValue = propInfoS.GetValue(source);
                object dValue = propInfoD.GetValue(destination);
                if ((sValue != null && dValue != null && !sValue.Equals(dValue)) || (sValue == null && dValue != null) || (sValue != null && dValue == null))
                {
                    propInfoD.SetValue(destination, sValue);
                }
            }
            catch (TargetException) { }
        }
        #endregion

        #region private methods
        private static void CopyValues<T>(T source, T destination, bool copyAll)
        {
            CopyValues<T, T>(source, destination, copyAll, true);
        }

        private static void CopyValues<S, D>(S source, D destination, bool copyAll, bool copyEntities)
        {
            IEnumerable<PropertyInfo> propInfoS;
            IEnumerable<PropertyInfo> propInfoD;

            BindingFlags bindingAttr = BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance;

            PropertyInfo[] fieldsS = source.GetType().GetProperties(bindingAttr);
            if (copyAll)
            {
                if (copyEntities)
                    propInfoS = fieldsS.Where(pi => pi.CanRead);
                else
                    propInfoS = fieldsS.Where(pi => pi.CanRead && ((!pi.PropertyType.IsClass && !pi.PropertyType.Name.StartsWith("ICollection")) || (pi.PropertyType.IsClass && pi.PropertyType.Name.Equals("String"))));
            }
            else
            {
                propInfoS = fieldsS.Where(pi => pi.CanRead && ((!pi.PropertyType.IsClass && !pi.PropertyType.Name.StartsWith("ICollection")) || (pi.PropertyType.IsClass && pi.PropertyType.Name.Equals("String"))) && pi.PropertyType.IsSerializable);
            }

            PropertyInfo[] fieldsD = destination.GetType().GetProperties(bindingAttr);
            if (copyAll)
            {
                if (copyEntities)
                    propInfoD = fieldsD.Where(pi => pi.CanWrite);
                else
                    propInfoD = fieldsD.Where(pi => pi.CanWrite && ((!pi.PropertyType.IsClass && !pi.PropertyType.Name.StartsWith("ICollection")) || (pi.PropertyType.IsClass && pi.PropertyType.Name.Equals("String"))));
            }
            else
            {
                propInfoD = fieldsD.Where(pi => pi.CanWrite && ((!pi.PropertyType.IsClass && !pi.PropertyType.Name.StartsWith("ICollection")) || (pi.PropertyType.IsClass && pi.PropertyType.Name.Equals("String"))) && pi.PropertyType.IsSerializable);
            }

            PropertyInfo pid;
            object sValue;
            object dValue;
            foreach (PropertyInfo pis in propInfoS)
            {
                //look for the PropertyInfo with the same Name and Type
                pid = propInfoD.Where(p => p.Name.Equals(pis.Name) && p.PropertyType == pis.PropertyType).FirstOrDefault();
                //pid = propInfoD.Where(p => p.Name.Equals(pis.Name)).FirstOrDefault();
                if (pid == null) continue;
                try
                {
                    sValue = pis.GetValue(source);
                    dValue = pid.GetValue(destination);
                    if ((sValue != null && dValue != null && !sValue.Equals(dValue)) || (sValue == null && dValue != null) || (sValue != null && dValue == null))
                    {
                        pid.SetValue(destination, sValue);
                    }
                }
                catch (TargetException) { }

            }
        }
        #endregion
    }
}
