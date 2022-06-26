using Sklad_v1_001.GlobalAttributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Sklad_v1_001.HelperGlobal.Role
{
    //класс управления ролями 
    //1--админ
    //2--директор
    //3--старший менеджер
    //4--кассир 
    public class AccessRules : DependencyObject
    {
        public static readonly DependencyProperty RuleProperty =
           DependencyProperty.RegisterAttached(
             "Rule",
             typeof(Rulesss),
             typeof(UIElement),
             new PropertyMetadata(Rulesss.Cashier)
           );

        public static void SetRule(UIElement element, Rulesss value)
        {
            if ((Int32)value >= Attributes.globalData.numeric.userEdit.RoleID)
                element.IsEnabled = true;
            else
                element.IsEnabled = false;
        }

        public static Rulesss GetRule(UIElement element)
        {
            return (Rulesss)element.GetValue(RuleProperty);
        }

        public static readonly DependencyProperty RuleVisibilityProperty =
           DependencyProperty.RegisterAttached(
             "RuleVisibility",
             typeof(Rulesss),
             typeof(UIElement),
             new PropertyMetadata(Rulesss.Cashier)
           );

        public static void SetRuleVisibility(UIElement element, Rulesss value)
        {
            if ((Int32)value >= Attributes.globalData.numeric.userEdit.RoleID)
                element.Visibility = Visibility.Visible;
            else
                element.Visibility = Visibility.Collapsed;
        }

        public static Rulesss GetRuleVisibility(UIElement element)
        {
            return (Rulesss)element.GetValue(RuleVisibilityProperty);
        }

    }
    public enum Rulesss : byte
    {
        Admin = 1,
        Director = 2,
        SeniorManager = 3,
        Cashier = 4
    }
}
