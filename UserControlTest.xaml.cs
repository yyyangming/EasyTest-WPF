using System;
using System.Activities.Presentation;
using System.Activities.Presentation.Model;
using System.Activities.Presentation.View;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Test
{
    ///<summary>
    /// WPF Native PropertyGrid class, taken from Workflow Foundation Designer
    ///</summary>
    public class WpfPropertyGrid : Grid
{
#region Private fields
    private WorkflowDesigner Designer;
    private MethodInfo RefreshMethod;
    private MethodInfo OnSelectionChangedMethod;
    private TextBlock SelectionTypeLabel;
    private object TheSelectedObject = null;
    #endregion

    #region Public properties
    ///<summary>
    /// Get or sets the selected object. Can be null.
    ///</summary>
    public object SelectedObject
    {
        get
        {
            return this.TheSelectedObject;
        }
        set
        {
            this.TheSelectedObject = value;

            if (value != null)
            {
                var context = new EditingContext();
                var mtm = new ModelTreeManager(context);
                mtm.Load(value);
                var selection = Selection.Select(context, mtm.Root);

                OnSelectionChangedMethod.Invoke(Designer.PropertyInspectorView, new object[] { selection });
                this.SelectionTypeLabel.Text = value.GetType().Name;
            }
            else
            {
                OnSelectionChangedMethod.Invoke(Designer.PropertyInspectorView, new object[] { null });
                this.SelectionTypeLabel.Text = string.Empty;
            }
        }
    }

    ///<summary>
    /// XAML information with PropertyGrid's font and color information
    ///</summary>
    ///<seealso>Documentation for WorkflowDesigner.PropertyInspectorFontAndColorData</seealso>
    public string FontAndColorData
    {
        set
        {
            Designer.PropertyInspectorFontAndColorData = value;
        }
    }
    #endregion

    ///<summary>
    /// Default constructor, creates a hidden designer view and a property inspector
    ///</summary>
    public WpfPropertyGrid()
    {
        this.Designer = new WorkflowDesigner();

        var inspector = Designer.PropertyInspectorView;
        Type inspectorType = inspector.GetType();

        inspector.Visibility = Visibility.Visible;
        this.Children.Add(inspector);

        var methods = inspectorType.GetMethods(Reflection.BindingFlags.Public | Reflection.BindingFlags.NonPublic | Reflection.BindingFlags.Instance |
        Reflection.BindingFlags.DeclaredOnly);

        this.RefreshMethod = inspectorType.GetMethod("RefreshPropertyList",
        Reflection.BindingFlags.NonPublic | Reflection.BindingFlags.Instance | Reflection.BindingFlags.DeclaredOnly);
        this.OnSelectionChangedMethod = inspectorType.GetMethod("OnSelectionChanged",
        Reflection.BindingFlags.Public | Reflection.BindingFlags.Instance | Reflection.BindingFlags.DeclaredOnly);
        this.SelectionTypeLabel = inspectorType.GetMethod("get_SelectionTypeLabel",
        Reflection.BindingFlags.Public | Reflection.BindingFlags.NonPublic | Reflection.BindingFlags.Instance |
        Reflection.BindingFlags.DeclaredOnly).Invoke(inspector, new object[0]) as TextBlock;

        this.SelectionTypeLabel.Text = string.Empty;
    }

    ///<summary>
    /// Updates the PropertyGrid's properties
    ///</summary>
    public void RefreshPropertyList()
    {
        RefreshMethod.Invoke(Designer.PropertyInspectorView, new object[] { false });
    }
}
}
