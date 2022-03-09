using System.Reflection;

namespace SharedKernel
{
    /// <summary>
    /// Base class for every value object.
    /// </summary>
    public abstract class ValueObject : IEquatable<ValueObject>
    {
        // This implementation of ValueObject is based on the following github repo:
        // https://github.com/jhewlett/ValueObject

        private List<PropertyInfo> _properties;
        private List<FieldInfo> _fields;

        #region Private Members

        private bool PropertiesAreEqual(object obj, PropertyInfo property)
        {
            return object.Equals(property.GetValue(this, null), property.GetValue(obj, null));
        }

        private bool FieldsAreEqual(object obj, FieldInfo field)
        {
            return object.Equals(field.GetValue(this), field.GetValue(obj));
        }

        private IEnumerable<PropertyInfo> GetProperties()
        {
            if (_properties == null)
                _properties = GetType()
                    .GetProperties(BindingFlags.Instance | BindingFlags.Public)
                    .Where(p => p.GetCustomAttribute(typeof(IgnoreMemberAttribute)) == null)
                    .ToList();

            return _properties;
        }

        private IEnumerable<FieldInfo> GetFields()
        {
            if (_fields == null)
                _fields = GetType()
                    .GetFields(BindingFlags.Instance | BindingFlags.Public)
                    .Where(x => x.GetCustomAttribute(typeof(IgnoreMemberAttribute)) == null)
                    .ToList();

            return _fields;
        }

        private int HashValue(int seed, object value)
        {
            var currentHash = value?.GetHashCode() ?? 0;
            return seed * 23 + currentHash;
        }

        #endregion

        #region Operators

        public static bool operator ==(ValueObject obj1, ValueObject obj2)
        {
            if (object.Equals(obj1, null))
                return object.Equals(obj2, null);
                
            return obj1.Equals(obj2);
        }

        public static bool operator !=(ValueObject obj1, ValueObject obj2)
        {
            return !(obj1 == obj2);
        }

        #endregion

        public bool Equals(ValueObject obj)
        {
            return Equals(obj as object);
        }

        public override bool Equals(object obj)
        {
            if (obj == null || GetType() != obj.GetType())
                return false;

            return 
                GetProperties().All(x => PropertiesAreEqual(obj, x)) && 
                GetFields().All(x => FieldsAreEqual(obj, x));
        }

        public override int GetHashCode()
        {
            unchecked   //allow overflow
            {
                int hash = 17;
                foreach (var property in GetProperties())
                {
                    var value = property.GetValue(this, null);
                    hash = HashValue(hash, value);
                }

                foreach (var field in GetFields())
                {
                    var value = field.GetValue(this);
                    hash = HashValue(hash, value);
                }

                return hash;
            }
        }
    }
}