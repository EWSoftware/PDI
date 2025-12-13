---
uid: 7ea5cd94-fa8a-4070-ba20-6d8c3b40d7c3
alt-uid: PDIRoadMap
title: PDI Tag to Class/Property Mappings
keywords: class mappings, PDI tag mappings, "property mappings, overview"
---

The following topics define each of the vCard, vNote, and calendar objects and their properties.  For
each tag, the matching EWSoftware PDI class is given.  PDI property classes may have several attributes in
addition to their actual data value such as an encoding method, data type/location, etc.  Refer to the
[](@T:EWSoftware.PDI.Properties.BaseProperty) class
for the attributes common to all property classes.  Refer to the derived classes for type-specific attributes.


The appropriate data value property name is also listed in the tables within each topic.  For any
property class regardless of the underlying data type, the **Value** property will return
the data value in string form.  For non-string property classes, there are also type-specific properties that
return the property data in a more natural format (i.e. `DateTimeValue` for date/time
properties or `TimeSpanValue` for properties that deal with time spans).  Property classes
with compound data values have multiple properties that let you access the individual parts of the data value
(i.e. the parts of the [](@T:EWSoftware.PDI.Properties.AddressProperty)
or [](@T:EWSoftware.PDI.Properties.ProductIdProperty)).


Several properties are specific to only one version of their related specification.  This is
indicated in the notes column.  Property tags are listed in alphabetical order within the components.  However,
their actual order in the PDI data stream is not significant and the properties can appear in any order as can
the components themselves.


Many of the property objects can appear more than once in a component.  In those cases, a matching
type-safe collection class also exists to contain them.  As a convenience, the property collections contain an
overloaded `Add` method that take an object of the underlying data type so that you can
quickly add values to the collection without having to create an instance of the property type first, assign the
value to it, and then add it to the collection.


- [](@0207df86-ca35-4e95-a711-6d4725291e40)
- [](@87814df5-add9-4307-89a7-4a46a9456eed)
- [](@6fc4dbbe-a274-4221-af50-680db3717693)
- [](@22098358-f1b3-43a0-bda7-ed43313cc99d)
- [](@2728ddf1-da12-446d-9142-b4bc57540d24)
- [](@91d0576f-e904-4e75-9f95-38a9dd368871)
- [](@fe961450-54b4-474a-962f-9e91083eb9ec)
- [](@bcfa7d27-b157-4387-a4a6-74f9aa12416f)
- [](@b4c78e51-f041-4490-a637-10638ca9d748)
- [](@3b06a002-6294-40b9-b43c-93af3e3ae046)



## See Also


**Other Resources**  
[](@695cc5b6-0937-42cd-9e7a-2f8aab4f09b0)  
[](@84784fc0-1a4e-4780-9a61-4abfda5592c1)  
