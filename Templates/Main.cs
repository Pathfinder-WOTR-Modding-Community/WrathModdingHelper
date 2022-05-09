﻿// ------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version: 16.0.0.0
//  
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
// ------------------------------------------------------------------------------
namespace WrathModdingHelper.Templates
{
    using System.Linq;
    using System.Text;
    using System.Collections.Generic;
    using System;
    
    /// <summary>
    /// Class to produce the template output
    /// </summary>
    
    #line 1 "C:\Users\worce\source\repos\WrathModdingHelper\Templates\Main.tt"
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.VisualStudio.TextTemplating", "16.0.0.0")]
    public partial class Main : MainBase
    {
#line hidden
        /// <summary>
        /// Create the template output
        /// </summary>
        public virtual string TransformText()
        {
            this.Write("\r\nusing HarmonyLib;\r\nusing Kingmaker.Blueprints.JsonSystem;\r\nusing System;\r\nusing" +
                    " System.Collections.Generic;\r\nusing System.Linq;\r\nusing System.Reflection;\r\nusin" +
                    "g UnityModManagerNet;\r\nusing static UnityModManagerNet.UnityModManager.ModEntry;" +
                    "\r\n\r\nnamespace ");
            
            #line 16 "C:\Users\worce\source\repos\WrathModdingHelper\Templates\Main.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(Common.DefaultNamespace));
            
            #line default
            #line hidden
            this.Write("\r\n{\r\n\r\n    public interface IModlet\r\n    {\r\n        void PatchBlueprints();\r\n    " +
                    "}\r\n\r\n    public interface ILibraryAdapter\r\n    {\r\n        void Load(UnityModMana" +
                    "ger.ModEntry modEntry);\r\n    }\r\n\r\n    public static class Main\r\n    {\r\n        p" +
                    "rivate static ModLogger Logger;\r\n        private static Harmony HarmonyInstance;" +
                    "\r\n        public static List<IModlet> Modlets = new();\r\n        public static Li" +
                    "st<ILibraryAdapter> Libraries = new();\r\n\r\n        /// <summary>\r\n        /// Uni" +
                    "ty Mod Manager entry point. Called to load the mod.\r\n        /// </summary>\r\n   " +
                    "     static bool Load(UnityModManager.ModEntry modEntry)\r\n        {\r\n           " +
                    " Logger = modEntry.Logger;\r\n            try\r\n            {\r\n                Harm" +
                    "onyInstance = new Harmony(modEntry.Info.Id);\r\n                HarmonyInstance.Pa" +
                    "tchAll();\r\n                Log(\"Finished harmony patching.\");\r\n\r\n               " +
                    " foreach (var libType in Assembly.GetExecutingAssembly().GetTypes().Where(t => t" +
                    ".IsClass && typeof(ILibraryAdapter).IsAssignableFrom(t)))\r\n                {\r\n  " +
                    "                  ILibraryAdapter lib = (ILibraryAdapter)Activator.CreateInstanc" +
                    "e(libType);\r\n                    try\r\n                    {\r\n                   " +
                    "     lib.Load(modEntry);\r\n                        Libraries.Add(lib);\r\n         " +
                    "           }\r\n                    catch (Exception e)\r\n                    {\r\n  " +
                    "                      Main.Error(e);\r\n                    }\r\n                }\r\n" +
                    "\r\n                foreach (var modletType in Assembly.GetExecutingAssembly().Get" +
                    "Types().Where(t => t.IsClass && typeof(IModlet).IsAssignableFrom(t)))\r\n         " +
                    "       {\r\n                    IModlet modlet = (IModlet)Activator.CreateInstance" +
                    "(modletType);\r\n                    try\r\n                    {\r\n                 " +
                    "       Modlets.Add(modlet);\r\n                    }\r\n                    catch (E" +
                    "xception e)\r\n                    {\r\n                        Main.Error(e);\r\n    " +
                    "                }\r\n                }\r\n            }\r\n            catch (Exceptio" +
                    "n e)\r\n            {\r\n                Error(e);\r\n                return false; //" +
                    " Flags the init failure for UMM\r\n            }\r\n            return true;\r\n      " +
                    "  }\r\n\r\n        public static void Log(string message)\r\n        {\r\n            Lo" +
                    "gger.Log(message);\r\n        }\r\n\r\n        public static void Error(Exception ex)\r" +
                    "\n        {\r\n            Logger.Error(ex.Message);\r\n        }\r\n    }\r\n\r\n    /// <" +
                    "summary>\r\n    /// Patch for BlueprintsCache.Init(). This is when it is safe to a" +
                    "dd / modify blueprints.\r\n    /// </summary>\r\n    [HarmonyPatch(typeof(Blueprints" +
                    "Cache))]\r\n    static class BlueprintsCache_Patch\r\n    {\r\n        [HarmonyPatch(n" +
                    "ameof(BlueprintsCache.Init)), HarmonyPostfix]\r\n        static void Init()\r\n     " +
                    "   {\r\n            try\r\n            {\r\n                Main.Log(\"Initializing blu" +
                    "eprints.\");\r\n\r\n                // Call your configure/init methods for new conte" +
                    "nt (blueprints) here\r\n                foreach (IModlet modlet in Main.Modlets)\r\n" +
                    "                {\r\n                    modlet.PatchBlueprints();\r\n              " +
                    "  }\r\n\r\n                Main.Log(\"Initialization finished.\");\r\n            }\r\n   " +
                    "         catch (Exception e)\r\n            {\r\n                Main.Error(e);\r\n   " +
                    "         }\r\n        }\r\n    }\r\n\r\n}\r\n\r\n");
            return this.GenerationEnvironment.ToString();
        }
    }
    
    #line default
    #line hidden
    #region Base class
    /// <summary>
    /// Base class for this transformation
    /// </summary>
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.VisualStudio.TextTemplating", "16.0.0.0")]
    public class MainBase
    {
        #region Fields
        private global::System.Text.StringBuilder generationEnvironmentField;
        private global::System.CodeDom.Compiler.CompilerErrorCollection errorsField;
        private global::System.Collections.Generic.List<int> indentLengthsField;
        private string currentIndentField = "";
        private bool endsWithNewline;
        private global::System.Collections.Generic.IDictionary<string, object> sessionField;
        #endregion
        #region Properties
        /// <summary>
        /// The string builder that generation-time code is using to assemble generated output
        /// </summary>
        protected System.Text.StringBuilder GenerationEnvironment
        {
            get
            {
                if ((this.generationEnvironmentField == null))
                {
                    this.generationEnvironmentField = new global::System.Text.StringBuilder();
                }
                return this.generationEnvironmentField;
            }
            set
            {
                this.generationEnvironmentField = value;
            }
        }
        /// <summary>
        /// The error collection for the generation process
        /// </summary>
        public System.CodeDom.Compiler.CompilerErrorCollection Errors
        {
            get
            {
                if ((this.errorsField == null))
                {
                    this.errorsField = new global::System.CodeDom.Compiler.CompilerErrorCollection();
                }
                return this.errorsField;
            }
        }
        /// <summary>
        /// A list of the lengths of each indent that was added with PushIndent
        /// </summary>
        private System.Collections.Generic.List<int> indentLengths
        {
            get
            {
                if ((this.indentLengthsField == null))
                {
                    this.indentLengthsField = new global::System.Collections.Generic.List<int>();
                }
                return this.indentLengthsField;
            }
        }
        /// <summary>
        /// Gets the current indent we use when adding lines to the output
        /// </summary>
        public string CurrentIndent
        {
            get
            {
                return this.currentIndentField;
            }
        }
        /// <summary>
        /// Current transformation session
        /// </summary>
        public virtual global::System.Collections.Generic.IDictionary<string, object> Session
        {
            get
            {
                return this.sessionField;
            }
            set
            {
                this.sessionField = value;
            }
        }
        #endregion
        #region Transform-time helpers
        /// <summary>
        /// Write text directly into the generated output
        /// </summary>
        public void Write(string textToAppend)
        {
            if (string.IsNullOrEmpty(textToAppend))
            {
                return;
            }
            // If we're starting off, or if the previous text ended with a newline,
            // we have to append the current indent first.
            if (((this.GenerationEnvironment.Length == 0) 
                        || this.endsWithNewline))
            {
                this.GenerationEnvironment.Append(this.currentIndentField);
                this.endsWithNewline = false;
            }
            // Check if the current text ends with a newline
            if (textToAppend.EndsWith(global::System.Environment.NewLine, global::System.StringComparison.CurrentCulture))
            {
                this.endsWithNewline = true;
            }
            // This is an optimization. If the current indent is "", then we don't have to do any
            // of the more complex stuff further down.
            if ((this.currentIndentField.Length == 0))
            {
                this.GenerationEnvironment.Append(textToAppend);
                return;
            }
            // Everywhere there is a newline in the text, add an indent after it
            textToAppend = textToAppend.Replace(global::System.Environment.NewLine, (global::System.Environment.NewLine + this.currentIndentField));
            // If the text ends with a newline, then we should strip off the indent added at the very end
            // because the appropriate indent will be added when the next time Write() is called
            if (this.endsWithNewline)
            {
                this.GenerationEnvironment.Append(textToAppend, 0, (textToAppend.Length - this.currentIndentField.Length));
            }
            else
            {
                this.GenerationEnvironment.Append(textToAppend);
            }
        }
        /// <summary>
        /// Write text directly into the generated output
        /// </summary>
        public void WriteLine(string textToAppend)
        {
            this.Write(textToAppend);
            this.GenerationEnvironment.AppendLine();
            this.endsWithNewline = true;
        }
        /// <summary>
        /// Write formatted text directly into the generated output
        /// </summary>
        public void Write(string format, params object[] args)
        {
            this.Write(string.Format(global::System.Globalization.CultureInfo.CurrentCulture, format, args));
        }
        /// <summary>
        /// Write formatted text directly into the generated output
        /// </summary>
        public void WriteLine(string format, params object[] args)
        {
            this.WriteLine(string.Format(global::System.Globalization.CultureInfo.CurrentCulture, format, args));
        }
        /// <summary>
        /// Raise an error
        /// </summary>
        public void Error(string message)
        {
            System.CodeDom.Compiler.CompilerError error = new global::System.CodeDom.Compiler.CompilerError();
            error.ErrorText = message;
            this.Errors.Add(error);
        }
        /// <summary>
        /// Raise a warning
        /// </summary>
        public void Warning(string message)
        {
            System.CodeDom.Compiler.CompilerError error = new global::System.CodeDom.Compiler.CompilerError();
            error.ErrorText = message;
            error.IsWarning = true;
            this.Errors.Add(error);
        }
        /// <summary>
        /// Increase the indent
        /// </summary>
        public void PushIndent(string indent)
        {
            if ((indent == null))
            {
                throw new global::System.ArgumentNullException("indent");
            }
            this.currentIndentField = (this.currentIndentField + indent);
            this.indentLengths.Add(indent.Length);
        }
        /// <summary>
        /// Remove the last indent that was added with PushIndent
        /// </summary>
        public string PopIndent()
        {
            string returnValue = "";
            if ((this.indentLengths.Count > 0))
            {
                int indentLength = this.indentLengths[(this.indentLengths.Count - 1)];
                this.indentLengths.RemoveAt((this.indentLengths.Count - 1));
                if ((indentLength > 0))
                {
                    returnValue = this.currentIndentField.Substring((this.currentIndentField.Length - indentLength));
                    this.currentIndentField = this.currentIndentField.Remove((this.currentIndentField.Length - indentLength));
                }
            }
            return returnValue;
        }
        /// <summary>
        /// Remove any indentation
        /// </summary>
        public void ClearIndent()
        {
            this.indentLengths.Clear();
            this.currentIndentField = "";
        }
        #endregion
        #region ToString Helpers
        /// <summary>
        /// Utility class to produce culture-oriented representation of an object as a string.
        /// </summary>
        public class ToStringInstanceHelper
        {
            private System.IFormatProvider formatProviderField  = global::System.Globalization.CultureInfo.InvariantCulture;
            /// <summary>
            /// Gets or sets format provider to be used by ToStringWithCulture method.
            /// </summary>
            public System.IFormatProvider FormatProvider
            {
                get
                {
                    return this.formatProviderField ;
                }
                set
                {
                    if ((value != null))
                    {
                        this.formatProviderField  = value;
                    }
                }
            }
            /// <summary>
            /// This is called from the compile/run appdomain to convert objects within an expression block to a string
            /// </summary>
            public string ToStringWithCulture(object objectToConvert)
            {
                if ((objectToConvert == null))
                {
                    throw new global::System.ArgumentNullException("objectToConvert");
                }
                System.Type t = objectToConvert.GetType();
                System.Reflection.MethodInfo method = t.GetMethod("ToString", new System.Type[] {
                            typeof(System.IFormatProvider)});
                if ((method == null))
                {
                    return objectToConvert.ToString();
                }
                else
                {
                    return ((string)(method.Invoke(objectToConvert, new object[] {
                                this.formatProviderField })));
                }
            }
        }
        private ToStringInstanceHelper toStringHelperField = new ToStringInstanceHelper();
        /// <summary>
        /// Helper to produce culture-oriented representation of an object as a string
        /// </summary>
        public ToStringInstanceHelper ToStringHelper
        {
            get
            {
                return this.toStringHelperField;
            }
        }
        #endregion
    }
    #endregion
}
