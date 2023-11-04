using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace LoxStatEdit
{
    public class SortableBindingList<T> : BindingList<T>
    {
        private bool _isSorted;
        private ListSortDirection _sortDirection;
        private PropertyDescriptor _sortProperty;

        protected override bool SupportsSortingCore => true;
        protected override bool IsSortedCore => _isSorted;
        protected override PropertyDescriptor SortPropertyCore => _sortProperty;
        protected override ListSortDirection SortDirectionCore => _sortDirection;

        protected override void ApplySortCore(PropertyDescriptor prop, ListSortDirection direction)
        {
            _sortProperty = prop;
            _sortDirection = direction;

            List<T> items = this.Items as List<T>;
            if (items != null)
            {
                items.Sort(delegate (T lhs, T rhs)
                {
                    int result = OnComparison(lhs, rhs);
                    if (_sortDirection == ListSortDirection.Descending)
                    {
                        result = -result;
                    }
                    return result;
                });
            }
            _isSorted = true;
            this.OnListChanged(new ListChangedEventArgs(ListChangedType.Reset, -1));
        }

        private int OnComparison(T lhs, T rhs)
        {
            object lhsValue = lhs == null ? null : _sortProperty.GetValue(lhs);
            object rhsValue = rhs == null ? null : _sortProperty.GetValue(rhs);
            if (lhsValue == null)
            {
                return (rhsValue == null) ? 0 : -1; // nulls are equal
            }
            if (rhsValue == null)
            {
                return 1; // first has value, second doesn't
            }
            if (lhsValue is IComparable)
            {
                return ((IComparable)lhsValue).CompareTo(rhsValue);
            }
            if (lhsValue.Equals(rhsValue))
            {
                return 0; // both are the same
            }
            // not comparable, compare ToString
            return lhsValue.ToString().CompareTo(rhsValue.ToString());
        }

        protected override void RemoveSortCore()
        {
            _isSorted = false;
            this.OnListChanged(new ListChangedEventArgs(ListChangedType.Reset, -1));
        }
    }
}
