using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Media;

namespace UI.RunnerManagement.Validation
{
    public static class Validator
    {
        public static bool IsValid(DependencyObject parent)
        {
            if (parent is null)
                return false;

            // Validate all the bindings on the parent
            var localValues = parent.GetLocalValueEnumerator();
            while (localValues.MoveNext())
            {
                var entry = localValues.Current;
                if (BindingOperations.IsDataBound(parent, entry.Property))
                {
                    var binding = BindingOperations.GetBinding(parent, entry.Property);
                    foreach (var rule in binding.ValidationRules)
                    {
                        var result = rule.Validate(parent.GetValue(entry.Property), null);
                        if (!result.IsValid)
                        {
                            var expression = BindingOperations.GetBindingExpression(parent, entry.Property);
                            System.Windows.Controls.Validation.MarkInvalid(expression, new ValidationError(rule, expression, result.ErrorContent, null));
                            return false;
                        }
                    }
                }
            }

            // Validate all the bindings on the children
            for (int i = 0; i != VisualTreeHelper.GetChildrenCount(parent); ++i)
            {
                var child = VisualTreeHelper.GetChild(parent, i);
                if (!IsValid(child))
                    return false;
            }

            return true;
        }
    }
}
