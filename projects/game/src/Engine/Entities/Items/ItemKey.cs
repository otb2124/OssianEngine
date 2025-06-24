using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public readonly struct ItemKey : IEquatable<ItemKey>
    {
        public readonly object EnumValue;
        public readonly Type EnumType;

        public ItemKey(object enumValue)
        {
            if (enumValue == null || !enumValue.GetType().IsEnum)
                throw new ArgumentException("Value must be an enum", nameof(enumValue));
            EnumValue = enumValue;
            EnumType = enumValue.GetType();
        }

        public override bool Equals(object obj) =>
            obj is ItemKey other && Equals(other);

        public bool Equals(ItemKey other) =>
            EnumType == other.EnumType && EnumValue.Equals(other.EnumValue);

        public override int GetHashCode() =>
            HashCode.Combine(EnumType, EnumValue);

        public static bool operator ==(ItemKey left, ItemKey right) => left.Equals(right);
        public static bool operator !=(ItemKey left, ItemKey right) => !left.Equals(right);

        public override string ToString() => $"{EnumType.Name}.{EnumValue}";
    }
}
