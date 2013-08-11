TrueTypeSharp
=============

This is a fork of the [original TrueTypeSharp by Illusory Studios LLC](http://www.zer7.com/software/truetypesharp)
which has been only slightly modified to allow it to be built as a Portable 
Class Library (PCL).

## Changes

Due to a lack of support for `System.SerializableAttribute` in .NET PCLs 
currently, serialization support has simply been removed for now. I intend to
revisit this in the future and put together a PCL-compatible equivalent.

The `TrueTypeFont` constructor overload accepting a filename string has been
changed to accept a Stream instead. This is again to be PCL-compatible.
