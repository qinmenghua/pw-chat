#region License
/*
    Copyright (c) 2010, Pawe� Hofman (CodeTitans)
    All Rights Reserved.

    Licensed under the Apache License version 2.0.
    For more information please visit:

    http://codetitans.codeplex.com/license
        or
    http://www.apache.org/licenses/


    For latest source code, documentation, samples
    and more information please visit:

    http://codetitans.codeplex.com/
*/
#endregion

using System.Reflection;
using System.Security;


[assembly: AssemblyCompany("CodeTitans")]
[assembly: AssemblyCopyright("Copyright � 2010 Pawe� Hofman, CodeTitans")]
[assembly: AssemblyTrademark("")]

#if !PocketPC

// Version information for an assembly consists of the following four values:
//
//      Major Version
//      Minor Version 
//      Build Number
//      Revision
[assembly: AssemblyVersion("1.4.0.0")]
[assembly: AssemblyFileVersion("1.4.0.0")]

#endif

#if !PocketPC && !WINDOWS_PHONE
[assembly: AllowPartiallyTrustedCallers]
#endif