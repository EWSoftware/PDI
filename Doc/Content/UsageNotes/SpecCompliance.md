---
uid: ada69918-d59a-4340-8c77-28b9726a7205
alt-uid: SpecCompliance
title: Specifications Compliance
keywords: "usage notes, specifications compliance"
---

To the best of my knowledge, all specifications are fully implemented except for the following noted
limitations.  The five specifications contain a large amount of information and it is possible that I may have
missed something.  If you do find something that is not implemented or is not correct, please let me know and I
will do my best to correct the problem.


vCard 2.1 and vCard 3.0
:   vCard object and vCard property group names are parsed and stored and are written back out when
    saved.  However, full use of the grouping name as defined in the specifications is not supported.
    
    
    Inline binary encoded properties such as `PHOTO`, `LOGO`,
    `SOUND`, and `KEY` give you access to a byte array that represents
    the inline decoded binary data but it is up to you to convert the byte array into something more useful such as
    an image, sound, digital certificate, file, etc.  The demo applications provide some examples of this.
    

vCard 4.0
:   Support for RFC 6350 is only partially implemented.  The following items are not supported.
    They may be addressed in future releases as the need arises:
    
    
    - The `ALTID` and `LANGUAGE` parameters are not
    supported.  The introduction of these parameters significantly changes the cardinality of many properties which
    would require reimplementing both the vCard parser and the vCard object itself.  As such, these parameters will
    be ignored and will be added to the custom parameter collection on any property on which they appear.  If
    multiple vCard properties of the same type are encountered due to the use of these parameters, only the value
    from the last property instance seen will be used.  If the vCard is subsequently saved, the other properties
    instances of the same type will be lost.
    - For similar reasons, the `PREF` and `TYPE`
    parameters on the `CATEGORIES`, `FN`, `GEO`,
    `KEY`, `LOGO`, `NICKNAME`,
    `ORG`, `PHOTO`, `ROLE`, `SOUND`,
    `SOURCE`, `TITLE`, `TZ`, and `URL`
    properties are not supported either.  As above, these will pass through as custom properties and only the last
    property instance seen will be used.
    - The `MEDIATYPE` parameter is ignored on properties that support it.
    It will be passed through in the custom parameters.
    - The `CALADRURI`, `CALURI`, `FBURL`,
    `IMPP`, `LANG`, and `XML` properties are not
    handled.  These will be passed through in the custom properties collection.  Support will be added for these
    later if there is a need for them.
    
    
    The vCard 4.0 specification states that inline vCard values such as on the
    `AGENT` property are no longer supported and does not include any documentation for the
    property at all.  However, since other non-inline types are valid in the prior specifications and the 4.0
    specification does not address them at all, the	`AGENT` property will function as it did
    in the prior specifications including support for inline vCard values.
    

vCalendar 1.0 and iCalendar 2.0
:   Recurrence rules in vCalendar 1.0 basic format are supported.  The vCalendar 1.0 extended
    recurrence rule format is not supported.
    
    
    The vCalendar 1.0 `RNUM`, `DAYLIGHT`, and
    `TZ` properties are parsed and stored in the calendar object and are written back out when
    saved but they are not used for anything.  All iCalendar 2.0 recurrence and time zone properties are supported
    and utilized though.
    
    
    Inline binary encoded properties such as `ATTACH` give you access to a
    byte array that represents the inline decoded binary data but it is up to you to convert the byte array into
    something more useful such as a file.  The demo applications provide some examples of this.
    
    
    All calendar-related objects are compatible with RFC 2446 (iCalendar Transport-Independent
    Interoperability Protocol) and RFC 2447 (iCalendar Message-Based Interoperability Protocol) and implement all 
    the necessary properties.  However, there is currently no support for validating objects against RFC 2446 rules.
    Support for this may be considered at a later date.
    

All specifications
:   Custom properties (anything not recognized as part of one of the specifications) are stored in
    a `CustomProperties` property on each object. It is up to you to search the collection for
    custom properties that may be of interested to you.
    
    
    Custom parameters (anything not recognized as part of one of the specifications) are stored in
    a `CustomParameters` string property on each property object.  It is up to you to parse
    the string for custom parameters that may be of interest to you.
    


For more information and copies of the specifications, please refer to the [Internet Mail Consortium](http://www.imc.org/pdi/) web site or the [Internet Engineering Task Force](http://www.ietf.org/) web site.



## See Also


**Other Resources**  
[](@695cc5b6-0937-42cd-9e7a-2f8aab4f09b0)  
