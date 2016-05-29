﻿using System;
using System.Diagnostics;
using FluentSharp.CoreLib;
using FluentSharp.CoreLib.APIs;
using FluentSharp.NUnit;
using NUnit.Framework;
using FluentSharp.CoreLib.API;

namespace UnitTests.FluentSharp_CoreLib.APIs
{
    [TestFixture]
    public class Test_API_NuGet
    {
        API_NuGet apiNuGet;

        [OneTimeSetUp]
        public void FixtureSetup()
        {
            apiNuGet = new API_NuGet();                
        }

        [OneTimeTearDown]
        public void FixtureTearDown()
        {
            
        }

        [Test] public void API_Nuget_Ctor()
        {
            apiNuGet.NuGet_Exe             .assert_Not_Null();
            apiNuGet.NuGet_Exe_Download_Uri.assert_Not_Null();
            
        }

        [Test] public void setup()
        {
            
            var apiNuGet_Setup = new API_NuGet();  

            apiNuGet_Setup.NuGet_Exe =  "_temp_NuGet".temp_Dir().path_Combine("nuget.exe");  //we need to change this, or there will be a lock on the nuget.exe (when used by another test)
            if(apiNuGet_Setup.NuGet_Exe.fileExists())
                apiNuGet_Setup.NuGet_Exe.assert_File_Deleted();
            
            apiNuGet_Setup.setup()  .assert_Not_Null();
            apiNuGet_Setup.NuGet_Exe.assert_File_Exists();
            
            apiNuGet_Setup.NuGet_Exe.assert_File_Deleted();
            apiNuGet_Setup.NuGet_Exe_Download_Uri = null;

            apiNuGet_Setup.setup() .assert_Is_Null();
            apiNuGet_Setup.NuGet_Exe.assert_File_Not_Exists();
        }
        [Test] public void help()
        {
			if(clr.mono())
				"Currently hanging on mono, although the nuget execution works (seems caused by not detecting that process has ended and console ReadToEnd not firing".assert_Ignore();
            new API_NuGet().help().assert_Not_Null()
                                  .assert_Contains("usage: NuGet <command> [args] [options]");            
        }

        [Test] public void list()
        {
			if (clr.mono ())
				"Same prob as help() method".assert_Ignore ();
            
            var nuGet = new API_NuGet();

            Console.WriteLine("-------");
            Console.WriteLine(nuGet.list("FluentSharp"));
            Console.WriteLine("-------");
            Console.WriteLine(nuGet.packages_FluentSharp());
            Console.WriteLine("-------");

            nuGet.list("FluentSharp").assert_Not_Empty()                                     
                                     .assert_Equal_To(nuGet.packages_FluentSharp());
        }
        [Test] public void install()
        {
            var nuGet = new API_NuGet();
            nuGet.install("FluentSharp.CoreLib").assert_Not_Null()         .assert_Folder_Exists()
											    .pathCombine(@"lib").pathCombine("net35").assert_Folder_Exists()
                                                .files().first().fileName().assert_Is_Equal_To  ("FluentSharp.CoreLib.dll");
        }
        [Test] public void extract_Installed_PackageName()
        {
            var nuGet = new API_NuGet();

            var message1 = @"Installing 'FluentSharp.CoreLib 5.5.172'.
Successfully installed 'FluentSharp.CoreLib 5.5.172'.";
            var message2 = "'FluentSharp.CoreLib 5.5.172' already installed.";

            var expectedName = "FluentSharp.CoreLib.5.5.172";

            nuGet.extract_Installed_PackageName(message1).assert_Is(expectedName);
            nuGet.extract_Installed_PackageName(message2).assert_Is(expectedName);
        }
        [Test] public void path_Package()
        {
            var packageName = "FluentSharp.CoreLib";
            var nuGet = new API_NuGet();
            nuGet.install(packageName);
            nuGet.path_Package(packageName).assert_Not_Null()
                                           .assert_Contains(packageName)
                                           .assert_Folder_Exists();
        }
        [Test] public void has_Package()
        {
            var packageName = "FluentSharp.CoreLib";
            var nuGet = new API_NuGet();
            nuGet.install(packageName);
            nuGet.has_Package(packageName).assert_True();
            
        }

    }
}
