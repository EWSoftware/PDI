---
uid: fd783396-0c32-4839-b171-a263aa40fabc
alt-uid: DataBinding
title: Data Binding Support
keywords: "usage notes, data binding support"
---

All of the vCard and calendar property collections are derived from the
[](@T:EWSoftware.PDI.Binding.ExtendedBindingList`1)
class.  This generic collection class implements support for data binding as well as many other useful methods
for searching and sorting the collection items.  In addition, top-level properties on objects such as
`VCard`, `VEvent`, `VToDo`, etc. support
data binding on their child properties.



## Data Binding to Child Properties

The `ExtendedBindingList<T>` methods used for searching and sorting
take a predicate so you are free to define the code in any way that you like in order to find or sort the
elements.  In addition, the class implements the necessary methods so that if the collection is set as the data
source for a data grid view, it will respond to search and sort requests from the data grid view control.


Most of the properties on the PDI objects such as `VCard` and
`VEvent` are complex objects themselves that can contain one or more child properties.
Support has been added for data binding to these child properties to make your code simpler.  In order to data
bind to a child property you simply specify the child property name separated from the parent property name with
an underscore.  For example:


``` cs
// Bind to the RepeatCount property on the VAlarm.Repeat property
udcRepeat.DataBindings.Add("Value", this.BindingSource, "Repeat_RepeatCount");

// Bind to the Value property on the VAlarm.Summary property
txtSummary.DataBindings.Add("Text", this.BindingSource, "Summary_Value");
```

``` vbnet
' Bind to the RepeatCount property on the VAlarm.Repeat property
udcRepeat.DataBindings.Add("Value", Me.BindingSource, "Repeat_RepeatCount")

' Bind to the Value property on the VAlarm.Summary property
txtSummary.DataBindings.Add("Text", Me.BindingSource, "Summary_Value")
```

Support is provided for binding to child properties up to three levels deep.



## Adding Child Property Binding Support to Non-PDI Classes

Child property data binding support can be extended to other classes dynamically at runtime using
the `ChildPropertyTypeDescriptionProvider.Add` method.  Simply pass it the type of the
class to which support should be extended.  This will normally be done in your application's startup code.


``` cs
// Add child property binding support to the NameAndAddress class
ChildPropertyTypeDescriptionProvider.Add(typeof(NameAndAddress));
```

``` vbnet
' Add child property binding support to the NameAndAddress class
ChildPropertyTypeDescriptionProvider.Add(GetTyppe(NameAndAddress))
```


## Using the Browse Control

`BrowseControl` is a derived `UserControl` that you
can use to browse a collection of objects one at a time.  It contains a built-in navigation tool strip.  To use
the control, derive a new user control from it and override the `BindToControls` method to
add data bindings to items in the collection and the `EnableControls` method to enable or
disable controls when the control is created or when all items in the collection have been deleted.


To specify the collection to browse, assign it to the control's
`BindingSource.DataSource` property.


``` cs
// Edit the Attendees collection
ucAttendees.BindingSource.DataSource = evt.Attendees;
```

``` vbnet
' Edit the Attendees collection
ucAttendees.BindingSource.DataSource = evt.Attendees
```

Note that assigning the collection directly to the data source property will cause changes to be
stored directly to the collection items.  If you want to edit a copy of the collection and retrieve the changes
if the user chooses to save them, you can assign a copy of the collection to it.  To retrieved the changed
values, you clear the existing collection and add the items from the modified collection.


``` cs
// Clone the Attendees collection and edit that instead
ucAttendees.BindingSource.DataSource = new AttendeePropertyCollection().CloneRange(e.Attendees);

...

// Clear the existing entries and add the modified copies
evt.Attendees.Clear();
evt.Attendees.CloneRange((AttendeePropertyCollection)ucAttendees.BindingSource.DataSource);
```

``` vbnet
// Clone the Attendees collection and edit that instead
ucAttendees.BindingSource.DataSource = New AttendeePropertyCollection().CloneRange(e.Attendees)

...

' Clear the existing entries and add the modified copies
evt.Attendees.Clear()
evt.Attendees.CloneRange(DirectCast(ucAttendees.BindingSource.DataSource, AttendeePropertyCollection));
```

The vCard browser and calendar browser demo applications contain several examples of using this
control to edit collections.



## See Also


**Other Resources**  
[](@695cc5b6-0937-42cd-9e7a-2f8aab4f09b0)  
