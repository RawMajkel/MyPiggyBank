﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace MyPiggyBank.Core.Protocol.CyclicOperation {
    using System;
    
    
    /// <summary>
    ///   A strongly-typed resource class, for looking up localized strings, etc.
    /// </summary>
    // This class was auto-generated by the StronglyTypedResourceBuilder
    // class via a tool like ResGen or Visual Studio.
    // To add or remove a member, edit your .ResX file then rerun ResGen
    // with the /str option, or rebuild your VS project.
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Resources.Tools.StronglyTypedResourceBuilder", "16.0.0.0")]
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    public class CyclicOperationResources {
        
        private static global::System.Resources.ResourceManager resourceMan;
        
        private static global::System.Globalization.CultureInfo resourceCulture;
        
        [global::System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal CyclicOperationResources() {
        }
        
        /// <summary>
        ///   Returns the cached ResourceManager instance used by this class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        public static global::System.Resources.ResourceManager ResourceManager {
            get {
                if (object.ReferenceEquals(resourceMan, null)) {
                    global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("MyPiggyBank.Core.Protocol.CyclicOperation.CyclicOperationResources", typeof(CyclicOperationResources).Assembly);
                    resourceMan = temp;
                }
                return resourceMan;
            }
        }
        
        /// <summary>
        ///   Overrides the current thread's CurrentUICulture property for all
        ///   resource lookups using this strongly typed resource class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        public static global::System.Globalization.CultureInfo Culture {
            get {
                return resourceCulture;
            }
            set {
                resourceCulture = value;
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to IsIncome is required..
        /// </summary>
        public static string CyclicOperationRequestValidator_IsIncome_Empty_Error {
            get {
                return ResourceManager.GetString("CyclicOperationRequestValidator_IsIncome_Empty_Error", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Name is required..
        /// </summary>
        public static string CyclicOperationRequestValidator_Name_Empty_Error {
            get {
                return ResourceManager.GetString("CyclicOperationRequestValidator_Name_Empty_Error", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Name must have at least 4 characters..
        /// </summary>
        public static string CyclicOperationRequestValidator_Name_Length_Error {
            get {
                return ResourceManager.GetString("CyclicOperationRequestValidator_Name_Length_Error", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to NextOccurence is required..
        /// </summary>
        public static string CyclicOperationRequestValidator_NextOccurence_Empty_Error {
            get {
                return ResourceManager.GetString("CyclicOperationRequestValidator_NextOccurence_Empty_Error", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to OperationCategory is required..
        /// </summary>
        public static string CyclicOperationRequestValidator_OperationCategory_Empty_Error {
            get {
                return ResourceManager.GetString("CyclicOperationRequestValidator_OperationCategory_Empty_Error", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Period is required..
        /// </summary>
        public static string CyclicOperationRequestValidator_Period_Empty_Error {
            get {
                return ResourceManager.GetString("CyclicOperationRequestValidator_Period_Empty_Error", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Resource is required..
        /// </summary>
        public static string CyclicOperationRequestValidator_Resource_Empty_Error {
            get {
                return ResourceManager.GetString("CyclicOperationRequestValidator_Resource_Empty_Error", resourceCulture);
            }
        }
    }
}
