// IMPORTANT: Read the license included with this code archive.
using System;
using System.Collections;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.ComponentModel.Design.Serialization;
using System.Drawing;
using System.Drawing.Design;
using System.Windows.Forms;

namespace Sadco.FamilyDoctor.Core.Controls.DesignerPanel {
	[ProvideProperty("Name", typeof(IComponent))]
	internal class Cl_DesignerHost : IDesignerHost, IContainer, IComponentChangeService, IExtenderProvider, ITypeDescriptorFilterService, IExtenderListService, IExtenderProviderService
	{
		// Transactions
		private Stack transactions = null;

		// Services
		private IServiceContainer p_ParentService = null;

		// Container
		private Hashtable components = null;
		private Hashtable designers = null;
		private ArrayList extenderProviders = null;
		private IComponent rootComponent = null;
        private UserControl rootUserControl {
            get {
                if (rootComponent is UserControl)
                    return (UserControl)rootComponent;
                else
                    return null;
            }
        }

        public Cl_DesignerHost(IServiceContainer a_ParentService)
		{
			// Keep the parent reference around for re-use
			this.p_ParentService = a_ParentService;

            // Initialise container helpers
            components = new Hashtable(CaseInsensitiveHashCodeProvider.Default, CaseInsensitiveComparer.Default);
			designers = new Hashtable();

			// Initialise transaction stack
			transactions = new Stack();

			// Add our own services
			a_ParentService.AddService(typeof(IDesignerHost), this);
			a_ParentService.AddService(typeof(IContainer), this);
			a_ParentService.AddService(typeof(IComponentChangeService), this);
			a_ParentService.AddService(typeof(ITypeDescriptorFilterService), this);

			// Add extender services
			extenderProviders = new ArrayList();
			a_ParentService.AddService(typeof(IExtenderListService), this);
			a_ParentService.AddService(typeof(IExtenderProviderService), this);
			AddExtenderProvider(this);

			// Add selection service
			a_ParentService.AddService(typeof(ISelectionService), new Cl_SelectionService(this));
		}

		#region IServiceContainer Implementation

		public object GetService(System.Type serviceType)
		{
			return p_ParentService.GetService(serviceType);
		}

		public void AddService(System.Type serviceType, System.ComponentModel.Design.ServiceCreatorCallback callback, bool promote)
		{
			p_ParentService.AddService(serviceType, callback, promote);
		}

		public void AddService(System.Type serviceType, System.ComponentModel.Design.ServiceCreatorCallback callback)
		{
			p_ParentService.AddService(serviceType, callback);
		}

		public void AddService(System.Type serviceType, object serviceInstance, bool promote)
		{
			p_ParentService.AddService(serviceType, serviceInstance, promote);
		}

		public void AddService(System.Type serviceType, object serviceInstance)
		{
			p_ParentService.AddService(serviceType, serviceInstance);
		}

		public void RemoveService(System.Type serviceType, bool promote)
		{
			p_ParentService.RemoveService(serviceType, promote);
		}

		public void RemoveService(System.Type serviceType)
		{
			p_ParentService.RemoveService(serviceType);
		}

		#endregion

		#region IDesignerHost Implementation

		public void Activate()
		{
			ISelectionService s = (ISelectionService)GetService(typeof(ISelectionService));

			// Simply set the root component as the primary selection
			if (s != null)
			{
				object[] o = new object[] { rootComponent };
				s.SetSelectedComponents(o);

				if (Activated != null)
					Activated(this, EventArgs.Empty);
			}
		}

		public System.ComponentModel.IComponent CreateComponent(System.Type componentClass, string name)
		{
			IComponent component = null;
			component = (IComponent)Activator.CreateInstance(componentClass);

            Ctrl_ToolboxService toolboxService = p_ParentService.GetService(typeof(IToolboxService)) as Ctrl_ToolboxService;
            if (componentClass == typeof(Ctrl_Element) && toolboxService != null && toolboxService.p_SelectedElement != null)
            {
                Cl_NameCreationService nameService = GetService(typeof(INameCreationService)) as Cl_NameCreationService;
                if (nameService != null)
                {
                    Ctrl_Element el = (Ctrl_Element)component;
                    el.p_Element = toolboxService.p_SelectedElement.p_Element;
                    name = nameService.f_CreateName(this, toolboxService.p_SelectedElement.p_Element.p_Name);
                    Add(component, name);

                    //if (rootUserControl != null)
                    //{
                    //    el.AutoSize = false;
                    //    el.Height = el.PreferredHeight;
                    //    if (el.f_IsLine)
                    //    {
                    //        el.Width = rootUserControl.Width;
                    //        el.BackColor = System.Drawing.Color.Blue;
                    //    }
                    //}
                }
            }
            else
            {
                Add(component, name);
            }
			return component;
		}

		public System.ComponentModel.IComponent CreateComponent(System.Type componentClass)
		{
			return CreateComponent(componentClass, null);
		}

		public System.ComponentModel.Design.DesignerTransaction CreateTransaction(string description)
		{
			DesignerTransaction transaction = null;

			// Raise event if this is the first transaction in a chain
			if (transactions.Count == 0)
			{
				if (TransactionOpening != null)
					TransactionOpening(this, EventArgs.Empty);
			}

			// Create transaction
			if (description == null)
				transaction = new Cl_MegaDesignerTransaction(this);
			else
				transaction = new Cl_MegaDesignerTransaction(this, description);
			transactions.Push(transaction);

			// Let people know a transaction has opened
			if (TransactionOpened != null)
				TransactionOpened(this, EventArgs.Empty);

			return transaction;
		}

		internal void OnTransactionClosing(bool commit)
		{
			if (TransactionClosing != null)
				TransactionClosing(this, new DesignerTransactionCloseEventArgs(commit));
		}

		internal void OnTransactionClosed(bool commit)
		{
			if (TransactionClosed != null)
				TransactionClosed(this, new DesignerTransactionCloseEventArgs(commit));

			transactions.Pop();
		}

		public System.ComponentModel.Design.DesignerTransaction CreateTransaction()
		{
			return CreateTransaction(null);
		}

		public void DestroyComponent(System.ComponentModel.IComponent component)
		{
			DesignerTransaction t = null;

			// Create transaction
			t = CreateTransaction("Destroy Component");

			// Destroy component
			if (component.Site.Container == this)
			{
				OnComponentChanging(component, null);
				Remove(component);
				component.Dispose();
				OnComponentChanged(component, null, null, null);
			}

			// Commit transaction
			t.Commit();
		}

		public System.ComponentModel.Design.IDesigner GetDesigner(System.ComponentModel.IComponent component)
		{
			if (component == null)
				return null;

			return (IDesigner)designers[component];
		}

		public System.Type GetType(string typeName)
		{
			ITypeResolutionService typeResolver = (ITypeResolutionService)GetService(typeof(ITypeResolutionService));

			if (typeResolver == null)
				return Type.GetType(typeName);
			else
				return typeResolver.GetType(typeName);
		}

		public System.ComponentModel.IContainer Container
		{
			get
			{
				return this;
			}
		}

		public bool InTransaction
		{
			get
			{
				return (transactions.Count > 0);
			}
		}

		public bool Loading
		{
			get
			{
				return false;
			}
		}

		public System.ComponentModel.IComponent RootComponent
		{
			get
			{
				return rootComponent;
			}
		}

		public string RootComponentClassName
		{
			get
			{
				return rootComponent.GetType().Name;
			}
		}

		public string TransactionDescription
		{
			get
			{
				if (InTransaction)
				{
					DesignerTransaction t = (DesignerTransaction)transactions.Peek();
					return t.Description;
				}
				else
					return null;
			}
		}

		public event System.EventHandler Activated;
		public event System.EventHandler Deactivated;
		public event System.EventHandler LoadComplete;
		public event System.ComponentModel.Design.DesignerTransactionCloseEventHandler TransactionClosed;
		public event System.ComponentModel.Design.DesignerTransactionCloseEventHandler TransactionClosing;
		public event System.EventHandler TransactionOpened;
		public event System.EventHandler TransactionOpening;

		#endregion

		#region IContainer Implementation

		internal bool ContainsName(string name)
		{
			return (components.Contains(name));
		}

		public void Add(System.ComponentModel.IComponent component, string name)
		{
			IDesigner designer = null;
			Cl_DesignSite site = null;

			// Check we're not trying to add a null component
			if (component == null)
				throw new ArgumentNullException("Cannot add a null component to the container.");

			// Remove this component from its existing container, if applicable
			if (component.Site != null && component.Site.Container != this)
				component.Site.Container.Remove(component);

			// Make sure we have a name for the component
			if (name == null)
			{
				INameCreationService nameService = (INameCreationService)GetService(typeof(INameCreationService));
				name = nameService.CreateName(this, component.GetType());
            }

			// Make sure there isn't already a component with this name in the container
			if (ContainsName(name))
				throw new ArgumentException("A component with this name already exists in the container.");

			// Give the new component a site
			site = new Cl_DesignSite(this, name);
			site.SetComponent(component);
			component.Site = site;

			// Let everyone know there's a component being added
			if (ComponentAdding != null)
				ComponentAdding(this, new ComponentEventArgs(component));

			// Get the designer for this component
			if (components.Count == 0)
			{
				// This is the first component being added and therefore must offer a root designer
				designer = TypeDescriptor.CreateDesigner(component, typeof(IRootDesigner));
				rootComponent = component;
			}
			else
			{
				designer = TypeDescriptor.CreateDesigner(component, typeof(IDesigner));
			}

			// If we got a designer, initialize it
			if (designer != null)
			{
				designer.Initialize(component);
                designers[component] = designer;
			}
			else
			{
				// This should never happen
				component.Site = null;
				throw new InvalidOperationException("Failed to get designer for this component.");
			}

			// Add to our list of extenderproviders if necessary
			if (component is IExtenderProvider)
			{
				IExtenderProviderService e = (IExtenderProviderService)GetService(typeof(IExtenderProviderService));
				e.AddExtenderProvider((IExtenderProvider)component);
			}

			// Finally we're able to add the component
			components.Add(component.Site.Name, component);
			if (ComponentAdded != null)
				ComponentAdded(this, new ComponentEventArgs(component));
		}

		public void Add(System.ComponentModel.IComponent component)
		{
			Add(component, null);
		}

		public void Remove(System.ComponentModel.IComponent component)
		{
			ISite site = component.Site;
			IDesigner designer = null;

			// Make sure component isn't null
			if (component == null)
				return;

			// Make sure component is sited here
			if (component.Site == null || component.Site.Container != this)
				return;

			// Let the nice people know the component is being removed
			if (ComponentRemoving != null)
				ComponentRemoving(this, new ComponentEventArgs(component));

			// Remove extender provider (if any)
			if (component is IExtenderProvider)
			{
				IExtenderProviderService e = (IExtenderProviderService)GetService(typeof(IExtenderProviderService));
				e.RemoveExtenderProvider((IExtenderProvider)component);
			}

			// Remove the component and dispose of its designer
			components.Remove(site.Name);
			designer = (IDesigner)designers[component];
			if (designer != null)
			{
				designer.Dispose();
				designers.Remove(component);
			}

			// Let the nice people know the component has been removed
			if (ComponentRemoved != null)
				ComponentRemoved(this, new ComponentEventArgs(component));

			// Kill the component's site
			component.Site = null;
		}

		public System.ComponentModel.ComponentCollection Components
		{
			get
			{
				IComponent[] c = new IComponent[] {};

				// If there are no components (shouldn't happen)
				if (components.Count == 0)
					return new ComponentCollection(c);

				// Compile list and return it
				c = new IComponent[components.Count];
				components.Values.CopyTo(c, 0);
				return new ComponentCollection(c);
			}
		}

		public void Dispose()
		{
			ICollection keys = components.Keys;

			// Remove and dispose of all components in our collection
			foreach(string key in keys)
			{
				IComponent component = (IComponent)components[key];
				components.Remove(component);
				component.Dispose();
			}

			components.Clear();
		}

		#endregion

		#region IComponentChangeService Implementation

		public void OnComponentChanged(object component, System.ComponentModel.MemberDescriptor member, object oldValue, object newValue)
		{
			if (ComponentChanged != null)
				ComponentChanged(this, new ComponentChangedEventArgs(component, member, oldValue, newValue));
		}

		public void OnComponentChanging(object component, System.ComponentModel.MemberDescriptor member)
		{
			if (ComponentChanging != null)
				ComponentChanging(this, new ComponentChangingEventArgs(component, member));
		}

		internal void OnComponentRename(object component, string oldName, string newName)
		{
			if (ComponentRename != null)
				ComponentRename(this, new ComponentRenameEventArgs(component, oldName, newName));
		}

		public event System.ComponentModel.Design.ComponentEventHandler ComponentAdded;
		public event System.ComponentModel.Design.ComponentEventHandler ComponentAdding;
		public event System.ComponentModel.Design.ComponentChangedEventHandler ComponentChanged;
		public event System.ComponentModel.Design.ComponentChangingEventHandler ComponentChanging;
		public event System.ComponentModel.Design.ComponentEventHandler ComponentRemoved;
		public event System.ComponentModel.Design.ComponentEventHandler ComponentRemoving;
		public event System.ComponentModel.Design.ComponentRenameEventHandler ComponentRename;

		#endregion

		#region IExtenderProvider Implementation

		public bool CanExtend(object extendee)
		{
			return (extendee is IComponent);
		}

		[DesignOnly(true), Category("Design"), Browsable(true), ParenthesizePropertyName(true), Description("The variable used to refer to this component in source code.")]
		public string GetName(IComponent component)
		{
			// Make sure component is sited
			if (component.Site == null)
				throw new InvalidOperationException("Component is not sited.");

			return component.Site.Name;
		}

		public void SetName(IComponent component, string name)
		{
			// Make sure component is sited
			if (component.Site == null)
				throw new InvalidOperationException("Component is not sited.");

			component.Site.Name = name;
		}

		#endregion

		#region ITypeDescriptorFilterService Implementation
	
		public bool FilterAttributes(IComponent component, IDictionary attributes)
		{
			IDesigner designer = GetDesigner(component);
			if (designer is IDesignerFilter)
			{
				((IDesignerFilter)designer).PreFilterAttributes(attributes);
				((IDesignerFilter)designer).PostFilterAttributes(attributes);
			}

			return designer == null == false;
		}
	
		public bool FilterEvents(IComponent component, IDictionary events)
		{
			IDesigner designer = GetDesigner(component);
			if (designer is IDesignerFilter)
			{
				((IDesignerFilter)designer).PreFilterEvents(events);
				((IDesignerFilter)designer).PostFilterEvents(events);
			}

			return designer == null == false;
		}
	
		public bool FilterProperties(IComponent component, IDictionary properties)
		{
			IDesigner designer = GetDesigner(component);
			if (designer is IDesignerFilter)
			{
				((IDesignerFilter)designer).PreFilterProperties(properties);
				((IDesignerFilter)designer).PostFilterProperties(properties);
			}

			return designer == null == false;
		}

		#endregion

		#region IExtenderListService Implementation

		public System.ComponentModel.IExtenderProvider[] GetExtenderProviders()
		{
			// Copy our collection in to an array to return
			IExtenderProvider[] e = new IExtenderProvider[extenderProviders.Count];
			extenderProviders.CopyTo(e, 0);

			return e;
		}

		#endregion

		#region IExtenderProviderService Implementation

		public void AddExtenderProvider(System.ComponentModel.IExtenderProvider provider)
		{
			if (!extenderProviders.Contains(provider))
				extenderProviders.Add(provider);
		}

		public void RemoveExtenderProvider(System.ComponentModel.IExtenderProvider provider)
		{
			if (extenderProviders.Contains(provider))
				extenderProviders.Remove(provider);
		}

		#endregion

	}
}
