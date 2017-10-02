namespace RemoteDesktop.LIB.ROOT.CIMV2.TERMINALSERVICES {
    using System;
    using System.ComponentModel;
    using System.Management;
    using System.Collections;
    using System.Globalization;
    using System.ComponentModel.Design.Serialization;
    using System.Reflection;
    
    
    // Functions ShouldSerialize<PropertyName> are functions used by VS property browser to check if a particular property has to be serialized. These functions are added for all ValueType properties ( properties of type Int32, BOOL etc.. which cannot be set to null). These functions use Is<PropertyName>Null function. These functions are also used in the TypeConverter implementation for the properties to check for NULL value of property so that an empty value can be shown in Property browser in case of Drag and Drop in Visual studio.
    // Functions Is<PropertyName>Null() are used to check if a property is NULL.
    // Functions Reset<PropertyName> are added for Nullable Read/Write properties. These functions are used by VS designer in property browser to set a property to NULL.
    // Every property added to the class for WMI property has attributes set to define its behavior in Visual Studio designer and also to define a TypeConverter to be used.
    // Datetime conversion functions ToDateTime and ToDmtfDateTime are added to the class to convert DMTF datetime to System.DateTime and vice-versa.
    // An Early Bound class generated for the WMI class.Win32_TerminalServiceSetting
    public class TerminalServiceSetting : System.ComponentModel.Component {
        
        // Private property to hold the WMI namespace in which the class resides.
        private static string CreatedWmiNamespace = "ROOT\\CIMV2\\TerminalServices";
        
        // Private property to hold the name of WMI class which created this class.
        private static string CreatedClassName = "Win32_TerminalServiceSetting";
        
        // Private member variable to hold the ManagementScope which is used by the various methods.
        private static System.Management.ManagementScope statMgmtScope = null;
        
        private ManagementSystemProperties PrivateSystemProperties;
        
        // Underlying lateBound WMI object.
        private System.Management.ManagementObject PrivateLateBoundObject;
        
        // Member variable to store the 'automatic commit' behavior for the class.
        private bool AutoCommitProp;
        
        // Private variable to hold the embedded property representing the instance.
        private System.Management.ManagementBaseObject embeddedObj;
        
        // The current WMI object used
        private System.Management.ManagementBaseObject curObj;
        
        // Flag to indicate if the instance is an embedded object.
        private bool isEmbedded;
        
        // Below are different overloads of constructors to initialize an instance of the class with a WMI object.
        public TerminalServiceSetting() {
            this.InitializeObject(null, null, null);
        }
        
        public TerminalServiceSetting(string keyServerName) {
            this.InitializeObject(null, new System.Management.ManagementPath(TerminalServiceSetting.ConstructPath(keyServerName)), null);
        }
        
        public TerminalServiceSetting(System.Management.ManagementScope mgmtScope, string keyServerName) {
            this.InitializeObject(((System.Management.ManagementScope)(mgmtScope)), new System.Management.ManagementPath(TerminalServiceSetting.ConstructPath(keyServerName)), null);
        }
        
        public TerminalServiceSetting(System.Management.ManagementPath path, System.Management.ObjectGetOptions getOptions) {
            this.InitializeObject(null, path, getOptions);
        }
        
        public TerminalServiceSetting(System.Management.ManagementScope mgmtScope, System.Management.ManagementPath path) {
            this.InitializeObject(mgmtScope, path, null);
        }
        
        public TerminalServiceSetting(System.Management.ManagementPath path) {
            this.InitializeObject(null, path, null);
        }
        
        public TerminalServiceSetting(System.Management.ManagementScope mgmtScope, System.Management.ManagementPath path, System.Management.ObjectGetOptions getOptions) {
            this.InitializeObject(mgmtScope, path, getOptions);
        }
        
        public TerminalServiceSetting(System.Management.ManagementObject theObject) {
            Initialize();
            if ((CheckIfProperClass(theObject) == true)) {
                PrivateLateBoundObject = theObject;
                PrivateSystemProperties = new ManagementSystemProperties(PrivateLateBoundObject);
                curObj = PrivateLateBoundObject;
            }
            else {
                throw new System.ArgumentException("Class name does not match.");
            }
        }
        
        public TerminalServiceSetting(System.Management.ManagementBaseObject theObject) {
            Initialize();
            if ((CheckIfProperClass(theObject) == true)) {
                embeddedObj = theObject;
                PrivateSystemProperties = new ManagementSystemProperties(theObject);
                curObj = embeddedObj;
                isEmbedded = true;
            }
            else {
                throw new System.ArgumentException("Class name does not match.");
            }
        }
        
        // Property returns the namespace of the WMI class.
        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public string OriginatingNamespace {
            get {
                return "ROOT\\CIMV2\\TerminalServices";
            }
        }
        
        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public string ManagementClassName {
            get {
                string strRet = CreatedClassName;
                if ((curObj != null)) {
                    if ((curObj.ClassPath != null)) {
                        strRet = ((string)(curObj["__CLASS"]));
                        if (((strRet == null) 
                                    || (strRet == string.Empty))) {
                            strRet = CreatedClassName;
                        }
                    }
                }
                return strRet;
            }
        }
        
        // Property pointing to an embedded object to get System properties of the WMI object.
        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public ManagementSystemProperties SystemProperties {
            get {
                return PrivateSystemProperties;
            }
        }
        
        // Property returning the underlying lateBound object.
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public System.Management.ManagementBaseObject LateBoundObject {
            get {
                return curObj;
            }
        }
        
        // ManagementScope of the object.
        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public System.Management.ManagementScope Scope {
            get {
                if ((isEmbedded == false)) {
                    return PrivateLateBoundObject.Scope;
                }
                else {
                    return null;
                }
            }
            set {
                if ((isEmbedded == false)) {
                    PrivateLateBoundObject.Scope = value;
                }
            }
        }
        
        // Property to show the commit behavior for the WMI object. If true, WMI object will be automatically saved after each property modification.(ie. Put() is called after modification of a property).
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public bool AutoCommit {
            get {
                return AutoCommitProp;
            }
            set {
                AutoCommitProp = value;
            }
        }
        
        // The ManagementPath of the underlying WMI object.
        [Browsable(true)]
        public System.Management.ManagementPath Path {
            get {
                if ((isEmbedded == false)) {
                    return PrivateLateBoundObject.Path;
                }
                else {
                    return null;
                }
            }
            set {
                if ((isEmbedded == false)) {
                    if ((CheckIfProperClass(null, value, null) != true)) {
                        throw new System.ArgumentException("Class name does not match.");
                    }
                    PrivateLateBoundObject.Path = value;
                }
            }
        }
        
        // Public static scope property which is used by the various methods.
        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public static System.Management.ManagementScope StaticScope {
            get {
                return statMgmtScope;
            }
            set {
                statMgmtScope = value;
            }
        }
        
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public bool IsActiveDesktopNull {
            get {
                if ((curObj["ActiveDesktop"] == null)) {
                    return true;
                }
                else {
                    return false;
                }
            }
        }
        
        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [Description("Active Desktop bit.")]
        [TypeConverter(typeof(WMIValueTypeConverter))]
        public ActiveDesktopValues ActiveDesktop {
            get {
                if ((curObj["ActiveDesktop"] == null)) {
                    return ((ActiveDesktopValues)(System.Convert.ToInt32(2)));
                }
                return ((ActiveDesktopValues)(System.Convert.ToInt32(curObj["ActiveDesktop"])));
            }
            set {
                if ((ActiveDesktopValues.NULL_ENUM_VALUE == value)) {
                    curObj["ActiveDesktop"] = null;
                }
                else {
                    curObj["ActiveDesktop"] = value;
                }
                if (((isEmbedded == false) 
                            && (AutoCommitProp == true))) {
                    PrivateLateBoundObject.Put();
                }
            }
        }
        
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public bool IsAllowTSConnectionsNull {
            get {
                if ((curObj["AllowTSConnections"] == null)) {
                    return true;
                }
                else {
                    return false;
                }
            }
        }
        
        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [Description("Describes whether new TS connections are allowed.")]
        [TypeConverter(typeof(WMIValueTypeConverter))]
        public AllowTSConnectionsValues AllowTSConnections {
            get {
                if ((curObj["AllowTSConnections"] == null)) {
                    return ((AllowTSConnectionsValues)(System.Convert.ToInt32(2)));
                }
                return ((AllowTSConnectionsValues)(System.Convert.ToInt32(curObj["AllowTSConnections"])));
            }
        }
        
        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public string Caption {
            get {
                return ((string)(curObj["Caption"]));
            }
        }
        
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public bool IsDeleteTempFoldersNull {
            get {
                if ((curObj["DeleteTempFolders"] == null)) {
                    return true;
                }
                else {
                    return false;
                }
            }
        }
        
        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [Description("Delete Temporary Folders on Exit bit.")]
        [TypeConverter(typeof(WMIValueTypeConverter))]
        public DeleteTempFoldersValues DeleteTempFolders {
            get {
                if ((curObj["DeleteTempFolders"] == null)) {
                    return ((DeleteTempFoldersValues)(System.Convert.ToInt32(2)));
                }
                return ((DeleteTempFoldersValues)(System.Convert.ToInt32(curObj["DeleteTempFolders"])));
            }
        }
        
        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public string Description {
            get {
                return ((string)(curObj["Description"]));
            }
        }
        
        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [Description("DirectConnectLicenseServers property is deprecated.")]
        public string DirectConnectLicenseServers {
            get {
                return ((string)(curObj["DirectConnectLicenseServers"]));
            }
        }
        
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public bool IsDisableForcibleLogoffNull {
            get {
                if ((curObj["DisableForcibleLogoff"] == null)) {
                    return true;
                }
                else {
                    return false;
                }
            }
        }
        
        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [Description("Indicates whether an Administrator logged onto the console can be logged off.")]
        [TypeConverter(typeof(WMIValueTypeConverter))]
        public uint DisableForcibleLogoff {
            get {
                if ((curObj["DisableForcibleLogoff"] == null)) {
                    return System.Convert.ToUInt32(0);
                }
                return ((uint)(curObj["DisableForcibleLogoff"]));
            }
        }
        
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public bool IsEnableDFSSNull {
            get {
                if ((curObj["EnableDFSS"] == null)) {
                    return true;
                }
                else {
                    return false;
                }
            }
        }
        
        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [Description("Enables Dynamic Fair-Share Scheduling: (0) Dynamic Fair-Share Scheduling is disab" +
            "led, (1) Dynamic Fair-Share Scheduling is enabled.")]
        [TypeConverter(typeof(WMIValueTypeConverter))]
        public uint EnableDFSS {
            get {
                if ((curObj["EnableDFSS"] == null)) {
                    return System.Convert.ToUInt32(0);
                }
                return ((uint)(curObj["EnableDFSS"]));
            }
            set {
                curObj["EnableDFSS"] = value;
                if (((isEmbedded == false) 
                            && (AutoCommitProp == true))) {
                    PrivateLateBoundObject.Put();
                }
            }
        }
        
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public bool IsEnableRemoteDesktopMSINull {
            get {
                if ((curObj["EnableRemoteDesktopMSI"] == null)) {
                    return true;
                }
                else {
                    return false;
                }
            }
        }
        
        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [Description("Enables Remote Desktop MSI: (0) Remote Desktop MSI is disabled, (1) Remote Deskto" +
            "p MSI is enabled.")]
        [TypeConverter(typeof(WMIValueTypeConverter))]
        public uint EnableRemoteDesktopMSI {
            get {
                if ((curObj["EnableRemoteDesktopMSI"] == null)) {
                    return System.Convert.ToUInt32(0);
                }
                return ((uint)(curObj["EnableRemoteDesktopMSI"]));
            }
            set {
                curObj["EnableRemoteDesktopMSI"] = value;
                if (((isEmbedded == false) 
                            && (AutoCommitProp == true))) {
                    PrivateLateBoundObject.Put();
                }
            }
        }
        
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public bool IsFallbackPrintDriverTypeNull {
            get {
                if ((curObj["FallbackPrintDriverType"] == null)) {
                    return true;
                }
                else {
                    return false;
                }
            }
        }
        
        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [Description("Indicates which Printer driver to Fallback to.")]
        [TypeConverter(typeof(WMIValueTypeConverter))]
        public FallbackPrintDriverTypeValues FallbackPrintDriverType {
            get {
                if ((curObj["FallbackPrintDriverType"] == null)) {
                    return ((FallbackPrintDriverTypeValues)(System.Convert.ToInt32(5)));
                }
                return ((FallbackPrintDriverTypeValues)(System.Convert.ToInt32(curObj["FallbackPrintDriverType"])));
            }
        }
        
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public bool IsGetCapabilitiesIDNull {
            get {
                if ((curObj["GetCapabilitiesID"] == null)) {
                    return true;
                }
                else {
                    return false;
                }
            }
        }
        
        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [Description("Reserved; Returns a value of one.")]
        [TypeConverter(typeof(WMIValueTypeConverter))]
        public uint GetCapabilitiesID {
            get {
                if ((curObj["GetCapabilitiesID"] == null)) {
                    return System.Convert.ToUInt32(0);
                }
                return ((uint)(curObj["GetCapabilitiesID"]));
            }
        }
        
        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [Description("Describes the Home Directory for the machine.")]
        public string HomeDirectory {
            get {
                return ((string)(curObj["HomeDirectory"]));
            }
        }
        
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public bool IsInstallDateNull {
            get {
                if ((curObj["InstallDate"] == null)) {
                    return true;
                }
                else {
                    return false;
                }
            }
        }
        
        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [TypeConverter(typeof(WMIValueTypeConverter))]
        public System.DateTime InstallDate {
            get {
                if ((curObj["InstallDate"] != null)) {
                    return ToDateTime(((string)(curObj["InstallDate"])));
                }
                else {
                    return System.DateTime.MinValue;
                }
            }
        }
        
        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [Description("A brief description for Licensing Mode")]
        public string LicensingDescription {
            get {
                return ((string)(curObj["LicensingDescription"]));
            }
        }
        
        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [Description("Name of Licensing Mode")]
        public string LicensingName {
            get {
                return ((string)(curObj["LicensingName"]));
            }
        }
        
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public bool IsLicensingTypeNull {
            get {
                if ((curObj["LicensingType"] == null)) {
                    return true;
                }
                else {
                    return false;
                }
            }
        }
        
        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [Description("Qualifier for Licensing Mode")]
        [TypeConverter(typeof(WMIValueTypeConverter))]
        public LicensingTypeValues LicensingType {
            get {
                if ((curObj["LicensingType"] == null)) {
                    return ((LicensingTypeValues)(System.Convert.ToInt32(5)));
                }
                return ((LicensingTypeValues)(System.Convert.ToInt32(curObj["LicensingType"])));
            }
        }
        
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public bool IsLimitedUserSessionsNull {
            get {
                if ((curObj["LimitedUserSessions"] == null)) {
                    return true;
                }
                else {
                    return false;
                }
            }
        }
        
        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [Description("Limited Number of User Sessions: (0) feature is disabled , a value greater than 0" +
            " represents the number of sessions allowed per connection.")]
        [TypeConverter(typeof(WMIValueTypeConverter))]
        public uint LimitedUserSessions {
            get {
                if ((curObj["LimitedUserSessions"] == null)) {
                    return System.Convert.ToUInt32(0);
                }
                return ((uint)(curObj["LimitedUserSessions"]));
            }
            set {
                curObj["LimitedUserSessions"] = value;
                if (((isEmbedded == false) 
                            && (AutoCommitProp == true))) {
                    PrivateLateBoundObject.Put();
                }
            }
        }
        
        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [Description("Indicates whether new sessions are allowed.This setting will not affect existing " +
            "sessions for Post Windows 2000 builds")]
        public string Logons {
            get {
                return ((string)(curObj["Logons"]));
            }
            set {
                curObj["Logons"] = value;
                if (((isEmbedded == false) 
                            && (AutoCommitProp == true))) {
                    PrivateLateBoundObject.Put();
                }
            }
        }
        
        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public string Name {
            get {
                return ((string)(curObj["Name"]));
            }
        }
        
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public bool IsPolicySourceAllowTSConnectionsNull {
            get {
                if ((curObj["PolicySourceAllowTSConnections"] == null)) {
                    return true;
                }
                else {
                    return false;
                }
            }
        }
        
        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [Description("PolicySourceAllowTSConnections indicates whether the property AllowTSConnections " +
            "is configured by Server(0) or Group Policy (1).")]
        [TypeConverter(typeof(WMIValueTypeConverter))]
        public uint PolicySourceAllowTSConnections {
            get {
                if ((curObj["PolicySourceAllowTSConnections"] == null)) {
                    return System.Convert.ToUInt32(0);
                }
                return ((uint)(curObj["PolicySourceAllowTSConnections"]));
            }
        }
        
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public bool IsPolicySourceConfiguredLicenseServersNull {
            get {
                if ((curObj["PolicySourceConfiguredLicenseServers"] == null)) {
                    return true;
                }
                else {
                    return false;
                }
            }
        }
        
        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [Description("PolicySourceConfiguredLicenseServers indicates whether the license servers return" +
            "ed by GetSpecifiedLSList is configured by Server(0) or Group Policy (1).")]
        [TypeConverter(typeof(WMIValueTypeConverter))]
        public uint PolicySourceConfiguredLicenseServers {
            get {
                if ((curObj["PolicySourceConfiguredLicenseServers"] == null)) {
                    return System.Convert.ToUInt32(0);
                }
                return ((uint)(curObj["PolicySourceConfiguredLicenseServers"]));
            }
        }
        
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public bool IsPolicySourceDeleteTempFoldersNull {
            get {
                if ((curObj["PolicySourceDeleteTempFolders"] == null)) {
                    return true;
                }
                else {
                    return false;
                }
            }
        }
        
        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [Description("PolicySourceDeleteTempFolders indicates whether the property DeleteTempFolders is" +
            " configured by Server(0) or Group Policy (1).")]
        [TypeConverter(typeof(WMIValueTypeConverter))]
        public uint PolicySourceDeleteTempFolders {
            get {
                if ((curObj["PolicySourceDeleteTempFolders"] == null)) {
                    return System.Convert.ToUInt32(0);
                }
                return ((uint)(curObj["PolicySourceDeleteTempFolders"]));
            }
        }
        
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public bool IsPolicySourceDirectConnectLicenseServersNull {
            get {
                if ((curObj["PolicySourceDirectConnectLicenseServers"] == null)) {
                    return true;
                }
                else {
                    return false;
                }
            }
        }
        
        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [Description("PolicySourceDirectConnectLicenseServers property is deprected.")]
        [TypeConverter(typeof(WMIValueTypeConverter))]
        public uint PolicySourceDirectConnectLicenseServers {
            get {
                if ((curObj["PolicySourceDirectConnectLicenseServers"] == null)) {
                    return System.Convert.ToUInt32(0);
                }
                return ((uint)(curObj["PolicySourceDirectConnectLicenseServers"]));
            }
        }
        
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public bool IsPolicySourceEnableDFSSNull {
            get {
                if ((curObj["PolicySourceEnableDFSS"] == null)) {
                    return true;
                }
                else {
                    return false;
                }
            }
        }
        
        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [Description("PolicySourceEnableDFSS indicates whether the property EnableDFSS is configured by" +
            " Server(0) or Group Policy (1).")]
        [TypeConverter(typeof(WMIValueTypeConverter))]
        public uint PolicySourceEnableDFSS {
            get {
                if ((curObj["PolicySourceEnableDFSS"] == null)) {
                    return System.Convert.ToUInt32(0);
                }
                return ((uint)(curObj["PolicySourceEnableDFSS"]));
            }
        }
        
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public bool IsPolicySourceEnableRemoteDesktopMSINull {
            get {
                if ((curObj["PolicySourceEnableRemoteDesktopMSI"] == null)) {
                    return true;
                }
                else {
                    return false;
                }
            }
        }
        
        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [Description("PolicySourceEnableRemoteDesktopMSI indicates whether the property EnableRemoteDes" +
            "ktopMSI is configured by Server(0) or Group Policy (1).")]
        [TypeConverter(typeof(WMIValueTypeConverter))]
        public uint PolicySourceEnableRemoteDesktopMSI {
            get {
                if ((curObj["PolicySourceEnableRemoteDesktopMSI"] == null)) {
                    return System.Convert.ToUInt32(0);
                }
                return ((uint)(curObj["PolicySourceEnableRemoteDesktopMSI"]));
            }
        }
        
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public bool IsPolicySourceFallbackPrintDriverTypeNull {
            get {
                if ((curObj["PolicySourceFallbackPrintDriverType"] == null)) {
                    return true;
                }
                else {
                    return false;
                }
            }
        }
        
        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [Description("PolicySourceFallbackPrintDriverType indicates whether the property FallbackPrintD" +
            "riverType is configured by Server(0) or Group Policy (1).")]
        [TypeConverter(typeof(WMIValueTypeConverter))]
        public uint PolicySourceFallbackPrintDriverType {
            get {
                if ((curObj["PolicySourceFallbackPrintDriverType"] == null)) {
                    return System.Convert.ToUInt32(0);
                }
                return ((uint)(curObj["PolicySourceFallbackPrintDriverType"]));
            }
        }
        
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public bool IsPolicySourceHomeDirectoryNull {
            get {
                if ((curObj["PolicySourceHomeDirectory"] == null)) {
                    return true;
                }
                else {
                    return false;
                }
            }
        }
        
        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [Description("PolicySourceHomeDirectory indicates whether the property HomeDirectory is configu" +
            "red by Server(0) or Group Policy (1).")]
        [TypeConverter(typeof(WMIValueTypeConverter))]
        public uint PolicySourceHomeDirectory {
            get {
                if ((curObj["PolicySourceHomeDirectory"] == null)) {
                    return System.Convert.ToUInt32(0);
                }
                return ((uint)(curObj["PolicySourceHomeDirectory"]));
            }
        }
        
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public bool IsPolicySourceLicensingTypeNull {
            get {
                if ((curObj["PolicySourceLicensingType"] == null)) {
                    return true;
                }
                else {
                    return false;
                }
            }
        }
        
        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [Description("PolicySourceLicensingType indicates whether the property LicensingType is configu" +
            "red by Server(0) or Group Policy (1)")]
        [TypeConverter(typeof(WMIValueTypeConverter))]
        public uint PolicySourceLicensingType {
            get {
                if ((curObj["PolicySourceLicensingType"] == null)) {
                    return System.Convert.ToUInt32(0);
                }
                return ((uint)(curObj["PolicySourceLicensingType"]));
            }
        }
        
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public bool IsPolicySourceProfilePathNull {
            get {
                if ((curObj["PolicySourceProfilePath"] == null)) {
                    return true;
                }
                else {
                    return false;
                }
            }
        }
        
        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [Description("PolicySourceProfilePath indicates whether the propertyProfilePath is configured b" +
            "y,Group Policy (1) or the Server (0).")]
        [TypeConverter(typeof(WMIValueTypeConverter))]
        public uint PolicySourceProfilePath {
            get {
                if ((curObj["PolicySourceProfilePath"] == null)) {
                    return System.Convert.ToUInt32(0);
                }
                return ((uint)(curObj["PolicySourceProfilePath"]));
            }
        }
        
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public bool IsPolicySourceSingleSessionNull {
            get {
                if ((curObj["PolicySourceSingleSession"] == null)) {
                    return true;
                }
                else {
                    return false;
                }
            }
        }
        
        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [Description("PolicySourceSingleSession indicates whether the property SingleSession is configu" +
            "red by Server(0) or Group Policy (1).")]
        [TypeConverter(typeof(WMIValueTypeConverter))]
        public uint PolicySourceSingleSession {
            get {
                if ((curObj["PolicySourceSingleSession"] == null)) {
                    return System.Convert.ToUInt32(0);
                }
                return ((uint)(curObj["PolicySourceSingleSession"]));
            }
        }
        
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public bool IsPolicySourceTimeZoneRedirectionNull {
            get {
                if ((curObj["PolicySourceTimeZoneRedirection"] == null)) {
                    return true;
                }
                else {
                    return false;
                }
            }
        }
        
        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [Description("PolicySourceTimeZoneRedirection indicates whether the property TimeZoneRedirectio" +
            "n is configured by Server(0) or Group Policy (1)")]
        [TypeConverter(typeof(WMIValueTypeConverter))]
        public uint PolicySourceTimeZoneRedirection {
            get {
                if ((curObj["PolicySourceTimeZoneRedirection"] == null)) {
                    return System.Convert.ToUInt32(0);
                }
                return ((uint)(curObj["PolicySourceTimeZoneRedirection"]));
            }
        }
        
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public bool IsPolicySourceUseTempFoldersNull {
            get {
                if ((curObj["PolicySourceUseTempFolders"] == null)) {
                    return true;
                }
                else {
                    return false;
                }
            }
        }
        
        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [Description("PolicySourceUseTempFolders indicates whether the property UseTempFolders is confi" +
            "gured by Server(0) or Group Policy (1).")]
        [TypeConverter(typeof(WMIValueTypeConverter))]
        public uint PolicySourceUseTempFolders {
            get {
                if ((curObj["PolicySourceUseTempFolders"] == null)) {
                    return System.Convert.ToUInt32(0);
                }
                return ((uint)(curObj["PolicySourceUseTempFolders"]));
            }
        }
        
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public bool IsPossibleLicensingTypesNull {
            get {
                if ((curObj["PossibleLicensingTypes"] == null)) {
                    return true;
                }
                else {
                    return false;
                }
            }
        }
        
        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [Description("Each bit of the property value acts as an index into the array of values in the B" +
            "itValues list.")]
        [TypeConverter(typeof(WMIValueTypeConverter))]
        public PossibleLicensingTypesValues PossibleLicensingTypes {
            get {
                if ((curObj["PossibleLicensingTypes"] == null)) {
                    return ((PossibleLicensingTypesValues)(System.Convert.ToInt32(10)));
                }
                return ((PossibleLicensingTypesValues)(System.Convert.ToInt32(curObj["PossibleLicensingTypes"])));
            }
        }
        
        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [Description("Describes the profile path for the machine.")]
        public string ProfilePath {
            get {
                return ((string)(curObj["ProfilePath"]));
            }
        }
        
        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [Description("Name of the Terminal Server whose properties are of interest.")]
        public string ServerName {
            get {
                return ((string)(curObj["ServerName"]));
            }
        }
        
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public bool IsSessionBrokerDrainModeNull {
            get {
                if ((curObj["SessionBrokerDrainMode"] == null)) {
                    return true;
                }
                else {
                    return false;
                }
            }
        }
        
        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [Description("Session Broker Drain Mode: (0) Allow new user logons, (1) Deny new user logons un" +
            "til reboot, (2) Deny new user logons.")]
        [TypeConverter(typeof(WMIValueTypeConverter))]
        public uint SessionBrokerDrainMode {
            get {
                if ((curObj["SessionBrokerDrainMode"] == null)) {
                    return System.Convert.ToUInt32(0);
                }
                return ((uint)(curObj["SessionBrokerDrainMode"]));
            }
            set {
                curObj["SessionBrokerDrainMode"] = value;
                if (((isEmbedded == false) 
                            && (AutoCommitProp == true))) {
                    PrivateLateBoundObject.Put();
                }
            }
        }
        
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public bool IsSingleSessionNull {
            get {
                if ((curObj["SingleSession"] == null)) {
                    return true;
                }
                else {
                    return false;
                }
            }
        }
        
        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [Description("Indicates if only one session is allowed per user.")]
        [TypeConverter(typeof(WMIValueTypeConverter))]
        public SingleSessionValues SingleSession {
            get {
                if ((curObj["SingleSession"] == null)) {
                    return ((SingleSessionValues)(System.Convert.ToInt32(2)));
                }
                return ((SingleSessionValues)(System.Convert.ToInt32(curObj["SingleSession"])));
            }
        }
        
        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public string Status {
            get {
                return ((string)(curObj["Status"]));
            }
        }
        
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public bool IsTerminalServerModeNull {
            get {
                if ((curObj["TerminalServerMode"] == null)) {
                    return true;
                }
                else {
                    return false;
                }
            }
        }
        
        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [Description("Qualifier for Mode of Terminal Service")]
        [TypeConverter(typeof(WMIValueTypeConverter))]
        public TerminalServerModeValues TerminalServerMode {
            get {
                if ((curObj["TerminalServerMode"] == null)) {
                    return ((TerminalServerModeValues)(System.Convert.ToInt32(2)));
                }
                return ((TerminalServerModeValues)(System.Convert.ToInt32(curObj["TerminalServerMode"])));
            }
        }
        
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public bool IsTimeZoneRedirectionNull {
            get {
                if ((curObj["TimeZoneRedirection"] == null)) {
                    return true;
                }
                else {
                    return false;
                }
            }
        }
        
        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [Description("Indicates if time zone redirection is enabled or disabled.")]
        [TypeConverter(typeof(WMIValueTypeConverter))]
        public uint TimeZoneRedirection {
            get {
                if ((curObj["TimeZoneRedirection"] == null)) {
                    return System.Convert.ToUInt32(0);
                }
                return ((uint)(curObj["TimeZoneRedirection"]));
            }
        }
        
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public bool IsUserPermissionNull {
            get {
                if ((curObj["UserPermission"] == null)) {
                    return true;
                }
                else {
                    return false;
                }
            }
        }
        
        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [Description("Application Compatibility bit.")]
        [TypeConverter(typeof(WMIValueTypeConverter))]
        public UserPermissionValues UserPermission {
            get {
                if ((curObj["UserPermission"] == null)) {
                    return ((UserPermissionValues)(System.Convert.ToInt32(2)));
                }
                return ((UserPermissionValues)(System.Convert.ToInt32(curObj["UserPermission"])));
            }
            set {
                if ((UserPermissionValues.NULL_ENUM_VALUE == value)) {
                    curObj["UserPermission"] = null;
                }
                else {
                    curObj["UserPermission"] = value;
                }
                if (((isEmbedded == false) 
                            && (AutoCommitProp == true))) {
                    PrivateLateBoundObject.Put();
                }
            }
        }
        
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public bool IsUseTempFoldersNull {
            get {
                if ((curObj["UseTempFolders"] == null)) {
                    return true;
                }
                else {
                    return false;
                }
            }
        }
        
        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [Description("Use Temporary Folders Per Session bit.")]
        [TypeConverter(typeof(WMIValueTypeConverter))]
        public UseTempFoldersValues UseTempFolders {
            get {
                if ((curObj["UseTempFolders"] == null)) {
                    return ((UseTempFoldersValues)(System.Convert.ToInt32(2)));
                }
                return ((UseTempFoldersValues)(System.Convert.ToInt32(curObj["UseTempFolders"])));
            }
        }
        
        private bool CheckIfProperClass(System.Management.ManagementScope mgmtScope, System.Management.ManagementPath path, System.Management.ObjectGetOptions OptionsParam) {
            if (((path != null) 
                        && (string.Compare(path.ClassName, this.ManagementClassName, true, System.Globalization.CultureInfo.InvariantCulture) == 0))) {
                return true;
            }
            else {
                return CheckIfProperClass(new System.Management.ManagementObject(mgmtScope, path, OptionsParam));
            }
        }
        
        private bool CheckIfProperClass(System.Management.ManagementBaseObject theObj) {
            if (((theObj != null) 
                        && (string.Compare(((string)(theObj["__CLASS"])), this.ManagementClassName, true, System.Globalization.CultureInfo.InvariantCulture) == 0))) {
                return true;
            }
            else {
                System.Array parentClasses = ((System.Array)(theObj["__DERIVATION"]));
                if ((parentClasses != null)) {
                    int count = 0;
                    for (count = 0; (count < parentClasses.Length); count = (count + 1)) {
                        if ((string.Compare(((string)(parentClasses.GetValue(count))), this.ManagementClassName, true, System.Globalization.CultureInfo.InvariantCulture) == 0)) {
                            return true;
                        }
                    }
                }
            }
            return false;
        }
        
        private bool ShouldSerializeActiveDesktop() {
            if ((this.IsActiveDesktopNull == false)) {
                return true;
            }
            return false;
        }
        
        private void ResetActiveDesktop() {
            curObj["ActiveDesktop"] = null;
            if (((isEmbedded == false) 
                        && (AutoCommitProp == true))) {
                PrivateLateBoundObject.Put();
            }
        }
        
        private bool ShouldSerializeAllowTSConnections() {
            if ((this.IsAllowTSConnectionsNull == false)) {
                return true;
            }
            return false;
        }
        
        private bool ShouldSerializeDeleteTempFolders() {
            if ((this.IsDeleteTempFoldersNull == false)) {
                return true;
            }
            return false;
        }
        
        private bool ShouldSerializeDisableForcibleLogoff() {
            if ((this.IsDisableForcibleLogoffNull == false)) {
                return true;
            }
            return false;
        }
        
        private bool ShouldSerializeEnableDFSS() {
            if ((this.IsEnableDFSSNull == false)) {
                return true;
            }
            return false;
        }
        
        private void ResetEnableDFSS() {
            curObj["EnableDFSS"] = null;
            if (((isEmbedded == false) 
                        && (AutoCommitProp == true))) {
                PrivateLateBoundObject.Put();
            }
        }
        
        private bool ShouldSerializeEnableRemoteDesktopMSI() {
            if ((this.IsEnableRemoteDesktopMSINull == false)) {
                return true;
            }
            return false;
        }
        
        private void ResetEnableRemoteDesktopMSI() {
            curObj["EnableRemoteDesktopMSI"] = null;
            if (((isEmbedded == false) 
                        && (AutoCommitProp == true))) {
                PrivateLateBoundObject.Put();
            }
        }
        
        private bool ShouldSerializeFallbackPrintDriverType() {
            if ((this.IsFallbackPrintDriverTypeNull == false)) {
                return true;
            }
            return false;
        }
        
        private bool ShouldSerializeGetCapabilitiesID() {
            if ((this.IsGetCapabilitiesIDNull == false)) {
                return true;
            }
            return false;
        }
        
        // Converts a given datetime in DMTF format to System.DateTime object.
        static System.DateTime ToDateTime(string dmtfDate) {
            System.DateTime initializer = System.DateTime.MinValue;
            int year = initializer.Year;
            int month = initializer.Month;
            int day = initializer.Day;
            int hour = initializer.Hour;
            int minute = initializer.Minute;
            int second = initializer.Second;
            long ticks = 0;
            string dmtf = dmtfDate;
            System.DateTime datetime = System.DateTime.MinValue;
            string tempString = string.Empty;
            if ((dmtf == null)) {
                throw new System.ArgumentOutOfRangeException();
            }
            if ((dmtf.Length == 0)) {
                throw new System.ArgumentOutOfRangeException();
            }
            if ((dmtf.Length != 25)) {
                throw new System.ArgumentOutOfRangeException();
            }
            try {
                tempString = dmtf.Substring(0, 4);
                if (("****" != tempString)) {
                    year = int.Parse(tempString);
                }
                tempString = dmtf.Substring(4, 2);
                if (("**" != tempString)) {
                    month = int.Parse(tempString);
                }
                tempString = dmtf.Substring(6, 2);
                if (("**" != tempString)) {
                    day = int.Parse(tempString);
                }
                tempString = dmtf.Substring(8, 2);
                if (("**" != tempString)) {
                    hour = int.Parse(tempString);
                }
                tempString = dmtf.Substring(10, 2);
                if (("**" != tempString)) {
                    minute = int.Parse(tempString);
                }
                tempString = dmtf.Substring(12, 2);
                if (("**" != tempString)) {
                    second = int.Parse(tempString);
                }
                tempString = dmtf.Substring(15, 6);
                if (("******" != tempString)) {
                    ticks = (long.Parse(tempString) * ((long)((System.TimeSpan.TicksPerMillisecond / 1000))));
                }
                if (((((((((year < 0) 
                            || (month < 0)) 
                            || (day < 0)) 
                            || (hour < 0)) 
                            || (minute < 0)) 
                            || (minute < 0)) 
                            || (second < 0)) 
                            || (ticks < 0))) {
                    throw new System.ArgumentOutOfRangeException();
                }
            }
            catch (System.Exception e) {
                throw new System.ArgumentOutOfRangeException(null, e.Message);
            }
            datetime = new System.DateTime(year, month, day, hour, minute, second, 0);
            datetime = datetime.AddTicks(ticks);
            System.TimeSpan tickOffset = System.TimeZone.CurrentTimeZone.GetUtcOffset(datetime);
            int UTCOffset = 0;
            int OffsetToBeAdjusted = 0;
            long OffsetMins = ((long)((tickOffset.Ticks / System.TimeSpan.TicksPerMinute)));
            tempString = dmtf.Substring(22, 3);
            if ((tempString != "******")) {
                tempString = dmtf.Substring(21, 4);
                try {
                    UTCOffset = int.Parse(tempString);
                }
                catch (System.Exception e) {
                    throw new System.ArgumentOutOfRangeException(null, e.Message);
                }
                OffsetToBeAdjusted = ((int)((OffsetMins - UTCOffset)));
                datetime = datetime.AddMinutes(((double)(OffsetToBeAdjusted)));
            }
            return datetime;
        }
        
        // Converts a given System.DateTime object to DMTF datetime format.
        static string ToDmtfDateTime(System.DateTime date) {
            string utcString = string.Empty;
            System.TimeSpan tickOffset = System.TimeZone.CurrentTimeZone.GetUtcOffset(date);
            long OffsetMins = ((long)((tickOffset.Ticks / System.TimeSpan.TicksPerMinute)));
            if ((System.Math.Abs(OffsetMins) > 999)) {
                date = date.ToUniversalTime();
                utcString = "+000";
            }
            else {
                if ((tickOffset.Ticks >= 0)) {
                    utcString = string.Concat("+", ((System.Int64 )((tickOffset.Ticks / System.TimeSpan.TicksPerMinute))).ToString().PadLeft(3, '0'));
                }
                else {
                    string strTemp = ((System.Int64 )(OffsetMins)).ToString();
                    utcString = string.Concat("-", strTemp.Substring(1, (strTemp.Length - 1)).PadLeft(3, '0'));
                }
            }
            string dmtfDateTime = ((System.Int32 )(date.Year)).ToString().PadLeft(4, '0');
            dmtfDateTime = string.Concat(dmtfDateTime, ((System.Int32 )(date.Month)).ToString().PadLeft(2, '0'));
            dmtfDateTime = string.Concat(dmtfDateTime, ((System.Int32 )(date.Day)).ToString().PadLeft(2, '0'));
            dmtfDateTime = string.Concat(dmtfDateTime, ((System.Int32 )(date.Hour)).ToString().PadLeft(2, '0'));
            dmtfDateTime = string.Concat(dmtfDateTime, ((System.Int32 )(date.Minute)).ToString().PadLeft(2, '0'));
            dmtfDateTime = string.Concat(dmtfDateTime, ((System.Int32 )(date.Second)).ToString().PadLeft(2, '0'));
            dmtfDateTime = string.Concat(dmtfDateTime, ".");
            System.DateTime dtTemp = new System.DateTime(date.Year, date.Month, date.Day, date.Hour, date.Minute, date.Second, 0);
            long microsec = ((long)((((date.Ticks - dtTemp.Ticks) 
                        * 1000) 
                        / System.TimeSpan.TicksPerMillisecond)));
            string strMicrosec = ((System.Int64 )(microsec)).ToString();
            if ((strMicrosec.Length > 6)) {
                strMicrosec = strMicrosec.Substring(0, 6);
            }
            dmtfDateTime = string.Concat(dmtfDateTime, strMicrosec.PadLeft(6, '0'));
            dmtfDateTime = string.Concat(dmtfDateTime, utcString);
            return dmtfDateTime;
        }
        
        private bool ShouldSerializeInstallDate() {
            if ((this.IsInstallDateNull == false)) {
                return true;
            }
            return false;
        }
        
        private bool ShouldSerializeLicensingType() {
            if ((this.IsLicensingTypeNull == false)) {
                return true;
            }
            return false;
        }
        
        private bool ShouldSerializeLimitedUserSessions() {
            if ((this.IsLimitedUserSessionsNull == false)) {
                return true;
            }
            return false;
        }
        
        private void ResetLimitedUserSessions() {
            curObj["LimitedUserSessions"] = null;
            if (((isEmbedded == false) 
                        && (AutoCommitProp == true))) {
                PrivateLateBoundObject.Put();
            }
        }
        
        private void ResetLogons() {
            curObj["Logons"] = null;
            if (((isEmbedded == false) 
                        && (AutoCommitProp == true))) {
                PrivateLateBoundObject.Put();
            }
        }
        
        private bool ShouldSerializePolicySourceAllowTSConnections() {
            if ((this.IsPolicySourceAllowTSConnectionsNull == false)) {
                return true;
            }
            return false;
        }
        
        private bool ShouldSerializePolicySourceConfiguredLicenseServers() {
            if ((this.IsPolicySourceConfiguredLicenseServersNull == false)) {
                return true;
            }
            return false;
        }
        
        private bool ShouldSerializePolicySourceDeleteTempFolders() {
            if ((this.IsPolicySourceDeleteTempFoldersNull == false)) {
                return true;
            }
            return false;
        }
        
        private bool ShouldSerializePolicySourceDirectConnectLicenseServers() {
            if ((this.IsPolicySourceDirectConnectLicenseServersNull == false)) {
                return true;
            }
            return false;
        }
        
        private bool ShouldSerializePolicySourceEnableDFSS() {
            if ((this.IsPolicySourceEnableDFSSNull == false)) {
                return true;
            }
            return false;
        }
        
        private bool ShouldSerializePolicySourceEnableRemoteDesktopMSI() {
            if ((this.IsPolicySourceEnableRemoteDesktopMSINull == false)) {
                return true;
            }
            return false;
        }
        
        private bool ShouldSerializePolicySourceFallbackPrintDriverType() {
            if ((this.IsPolicySourceFallbackPrintDriverTypeNull == false)) {
                return true;
            }
            return false;
        }
        
        private bool ShouldSerializePolicySourceHomeDirectory() {
            if ((this.IsPolicySourceHomeDirectoryNull == false)) {
                return true;
            }
            return false;
        }
        
        private bool ShouldSerializePolicySourceLicensingType() {
            if ((this.IsPolicySourceLicensingTypeNull == false)) {
                return true;
            }
            return false;
        }
        
        private bool ShouldSerializePolicySourceProfilePath() {
            if ((this.IsPolicySourceProfilePathNull == false)) {
                return true;
            }
            return false;
        }
        
        private bool ShouldSerializePolicySourceSingleSession() {
            if ((this.IsPolicySourceSingleSessionNull == false)) {
                return true;
            }
            return false;
        }
        
        private bool ShouldSerializePolicySourceTimeZoneRedirection() {
            if ((this.IsPolicySourceTimeZoneRedirectionNull == false)) {
                return true;
            }
            return false;
        }
        
        private bool ShouldSerializePolicySourceUseTempFolders() {
            if ((this.IsPolicySourceUseTempFoldersNull == false)) {
                return true;
            }
            return false;
        }
        
        private bool ShouldSerializePossibleLicensingTypes() {
            if ((this.IsPossibleLicensingTypesNull == false)) {
                return true;
            }
            return false;
        }
        
        private bool ShouldSerializeSessionBrokerDrainMode() {
            if ((this.IsSessionBrokerDrainModeNull == false)) {
                return true;
            }
            return false;
        }
        
        private void ResetSessionBrokerDrainMode() {
            curObj["SessionBrokerDrainMode"] = null;
            if (((isEmbedded == false) 
                        && (AutoCommitProp == true))) {
                PrivateLateBoundObject.Put();
            }
        }
        
        private bool ShouldSerializeSingleSession() {
            if ((this.IsSingleSessionNull == false)) {
                return true;
            }
            return false;
        }
        
        private bool ShouldSerializeTerminalServerMode() {
            if ((this.IsTerminalServerModeNull == false)) {
                return true;
            }
            return false;
        }
        
        private bool ShouldSerializeTimeZoneRedirection() {
            if ((this.IsTimeZoneRedirectionNull == false)) {
                return true;
            }
            return false;
        }
        
        private bool ShouldSerializeUserPermission() {
            if ((this.IsUserPermissionNull == false)) {
                return true;
            }
            return false;
        }
        
        private void ResetUserPermission() {
            curObj["UserPermission"] = null;
            if (((isEmbedded == false) 
                        && (AutoCommitProp == true))) {
                PrivateLateBoundObject.Put();
            }
        }
        
        private bool ShouldSerializeUseTempFolders() {
            if ((this.IsUseTempFoldersNull == false)) {
                return true;
            }
            return false;
        }
        
        [Browsable(true)]
        public void CommitObject() {
            if ((isEmbedded == false)) {
                PrivateLateBoundObject.Put();
            }
        }
        
        [Browsable(true)]
        public void CommitObject(System.Management.PutOptions putOptions) {
            if ((isEmbedded == false)) {
                PrivateLateBoundObject.Put(putOptions);
            }
        }
        
        private void Initialize() {
            AutoCommitProp = true;
            isEmbedded = false;
        }
        
        private static string ConstructPath(string keyServerName) {
            string strPath = "ROOT\\CIMV2\\TerminalServices:Win32_TerminalServiceSetting";
            strPath = string.Concat(strPath, string.Concat(".ServerName=", string.Concat("\"", string.Concat(keyServerName, "\""))));
            return strPath;
        }
        
        private void InitializeObject(System.Management.ManagementScope mgmtScope, System.Management.ManagementPath path, System.Management.ObjectGetOptions getOptions) {
            Initialize();
            if ((path != null)) {
                if ((CheckIfProperClass(mgmtScope, path, getOptions) != true)) {
                    throw new System.ArgumentException("Class name does not match.");
                }
            }
            PrivateLateBoundObject = new System.Management.ManagementObject(mgmtScope, path, getOptions);
            PrivateSystemProperties = new ManagementSystemProperties(PrivateLateBoundObject);
            curObj = PrivateLateBoundObject;
        }
        
        // Different overloads of GetInstances() help in enumerating instances of the WMI class.
        public static TerminalServiceSettingCollection GetInstances() {
            return GetInstances(null, null, null);
        }
        
        public static TerminalServiceSettingCollection GetInstances(string condition) {
            return GetInstances(null, condition, null);
        }
        
        public static TerminalServiceSettingCollection GetInstances(System.String [] selectedProperties) {
            return GetInstances(null, null, selectedProperties);
        }
        
        public static TerminalServiceSettingCollection GetInstances(string condition, System.String [] selectedProperties) {
            return GetInstances(null, condition, selectedProperties);
        }
        
        public static TerminalServiceSettingCollection GetInstances(System.Management.ManagementScope mgmtScope, System.Management.EnumerationOptions enumOptions) {
            if ((mgmtScope == null)) {
                if ((statMgmtScope == null)) {
                    mgmtScope = new System.Management.ManagementScope();
                    mgmtScope.Path.NamespacePath = "root\\CIMV2\\TerminalServices";
                }
                else {
                    mgmtScope = statMgmtScope;
                }
            }
            System.Management.ManagementPath pathObj = new System.Management.ManagementPath();
            pathObj.ClassName = "Win32_TerminalServiceSetting";
            pathObj.NamespacePath = "root\\CIMV2\\TerminalServices";
            System.Management.ManagementClass clsObject = new System.Management.ManagementClass(mgmtScope, pathObj, null);
            if ((enumOptions == null)) {
                enumOptions = new System.Management.EnumerationOptions();
                enumOptions.EnsureLocatable = true;
            }
            return new TerminalServiceSettingCollection(clsObject.GetInstances(enumOptions));
        }
        
        public static TerminalServiceSettingCollection GetInstances(System.Management.ManagementScope mgmtScope, string condition) {
            return GetInstances(mgmtScope, condition, null);
        }
        
        public static TerminalServiceSettingCollection GetInstances(System.Management.ManagementScope mgmtScope, System.String [] selectedProperties) {
            return GetInstances(mgmtScope, null, selectedProperties);
        }
        
        public static TerminalServiceSettingCollection GetInstances(System.Management.ManagementScope mgmtScope, string condition, System.String [] selectedProperties) {
            if ((mgmtScope == null)) {
                if ((statMgmtScope == null)) {
                    mgmtScope = new System.Management.ManagementScope();
                    mgmtScope.Path.NamespacePath = "root\\CIMV2\\TerminalServices";
                }
                else {
                    mgmtScope = statMgmtScope;
                }
            }
            System.Management.ManagementObjectSearcher ObjectSearcher = new System.Management.ManagementObjectSearcher(mgmtScope, new SelectQuery("Win32_TerminalServiceSetting", condition, selectedProperties));
            System.Management.EnumerationOptions enumOptions = new System.Management.EnumerationOptions();
            enumOptions.EnsureLocatable = true;
            ObjectSearcher.Options = enumOptions;
            return new TerminalServiceSettingCollection(ObjectSearcher.Get());
        }
        
        [Browsable(true)]
        public static TerminalServiceSetting CreateInstance() {
            System.Management.ManagementScope mgmtScope = null;
            if ((statMgmtScope == null)) {
                mgmtScope = new System.Management.ManagementScope();
                mgmtScope.Path.NamespacePath = CreatedWmiNamespace;
            }
            else {
                mgmtScope = statMgmtScope;
            }
            System.Management.ManagementPath mgmtPath = new System.Management.ManagementPath(CreatedClassName);
            System.Management.ManagementClass tmpMgmtClass = new System.Management.ManagementClass(mgmtScope, mgmtPath, null);
            return new TerminalServiceSetting(tmpMgmtClass.CreateInstance());
        }
        
        [Browsable(true)]
        public void Delete() {
            PrivateLateBoundObject.Delete();
        }
        
        public uint AddDirectConnectLicenseServer(string LicenseServerName) {
            if ((isEmbedded == false)) {
                System.Management.ManagementBaseObject inParams = null;
                inParams = PrivateLateBoundObject.GetMethodParameters("AddDirectConnectLicenseServer");
                inParams["LicenseServerName"] = ((System.String )(LicenseServerName));
                System.Management.ManagementBaseObject outParams = PrivateLateBoundObject.InvokeMethod("AddDirectConnectLicenseServer", inParams, null);
                return System.Convert.ToUInt32(outParams.Properties["ReturnValue"].Value);
            }
            else {
                return System.Convert.ToUInt32(0);
            }
        }
        
        public uint AddLSToSpecifiedLicenseServerList(string LicenseServerName) {
            if ((isEmbedded == false)) {
                System.Management.ManagementBaseObject inParams = null;
                inParams = PrivateLateBoundObject.GetMethodParameters("AddLSToSpecifiedLicenseServerList");
                inParams["LicenseServerName"] = ((System.String )(LicenseServerName));
                System.Management.ManagementBaseObject outParams = PrivateLateBoundObject.InvokeMethod("AddLSToSpecifiedLicenseServerList", inParams, null);
                return System.Convert.ToUInt32(outParams.Properties["ReturnValue"].Value);
            }
            else {
                return System.Convert.ToUInt32(0);
            }
        }
        
        public uint CanAccessLicenseServer(string ServerName, out uint AccessAllowed) {
            if ((isEmbedded == false)) {
                System.Management.ManagementBaseObject inParams = null;
                inParams = PrivateLateBoundObject.GetMethodParameters("CanAccessLicenseServer");
                inParams["ServerName"] = ((System.String )(ServerName));
                System.Management.ManagementBaseObject outParams = PrivateLateBoundObject.InvokeMethod("CanAccessLicenseServer", inParams, null);
                AccessAllowed = System.Convert.ToUInt32(outParams.Properties["AccessAllowed"].Value);
                return System.Convert.ToUInt32(outParams.Properties["ReturnValue"].Value);
            }
            else {
                AccessAllowed = System.Convert.ToUInt32(0);
                return System.Convert.ToUInt32(0);
            }
        }
        
        public uint ChangeMode(uint LicensingType) {
            if ((isEmbedded == false)) {
                System.Management.ManagementBaseObject inParams = null;
                inParams = PrivateLateBoundObject.GetMethodParameters("ChangeMode");
                inParams["LicensingType"] = ((System.UInt32 )(LicensingType));
                System.Management.ManagementBaseObject outParams = PrivateLateBoundObject.InvokeMethod("ChangeMode", inParams, null);
                return System.Convert.ToUInt32(outParams.Properties["ReturnValue"].Value);
            }
            else {
                return System.Convert.ToUInt32(0);
            }
        }
        
        public uint CreateWinstation(uint LanaId, string Name, string WinstaDriverName) {
            if ((isEmbedded == false)) {
                System.Management.ManagementBaseObject inParams = null;
                inParams = PrivateLateBoundObject.GetMethodParameters("CreateWinstation");
                inParams["LanaId"] = ((System.UInt32 )(LanaId));
                inParams["Name"] = ((System.String )(Name));
                inParams["WinstaDriverName"] = ((System.String )(WinstaDriverName));
                System.Management.ManagementBaseObject outParams = PrivateLateBoundObject.InvokeMethod("CreateWinstation", inParams, null);
                return System.Convert.ToUInt32(outParams.Properties["ReturnValue"].Value);
            }
            else {
                return System.Convert.ToUInt32(0);
            }
        }
        
        public uint DeleteDirectConnectLicenseServer(string LicenseServerName) {
            if ((isEmbedded == false)) {
                System.Management.ManagementBaseObject inParams = null;
                inParams = PrivateLateBoundObject.GetMethodParameters("DeleteDirectConnectLicenseServer");
                inParams["LicenseServerName"] = ((System.String )(LicenseServerName));
                System.Management.ManagementBaseObject outParams = PrivateLateBoundObject.InvokeMethod("DeleteDirectConnectLicenseServer", inParams, null);
                return System.Convert.ToUInt32(outParams.Properties["ReturnValue"].Value);
            }
            else {
                return System.Convert.ToUInt32(0);
            }
        }
        
        public uint EmptySpecifiedLicenseServerList() {
            if ((isEmbedded == false)) {
                System.Management.ManagementBaseObject inParams = null;
                System.Management.ManagementBaseObject outParams = PrivateLateBoundObject.InvokeMethod("EmptySpecifiedLicenseServerList", inParams, null);
                return System.Convert.ToUInt32(outParams.Properties["ReturnValue"].Value);
            }
            else {
                return System.Convert.ToUInt32(0);
            }
        }
        
        public uint FindLicenseServers(out uint Count, out System.Management.ManagementBaseObject[] LicenseServersList) {
            if ((isEmbedded == false)) {
                System.Management.ManagementBaseObject inParams = null;
                System.Management.ManagementBaseObject outParams = PrivateLateBoundObject.InvokeMethod("FindLicenseServers", inParams, null);
                Count = System.Convert.ToUInt32(outParams.Properties["Count"].Value);
                LicenseServersList = ((System.Management.ManagementBaseObject[])(outParams.Properties["LicenseServersList"].Value));
                return System.Convert.ToUInt32(outParams.Properties["ReturnValue"].Value);
            }
            else {
                Count = System.Convert.ToUInt32(0);
                LicenseServersList = null;
                return System.Convert.ToUInt32(0);
            }
        }
        
        public uint GetDomain(out string Domain) {
            if ((isEmbedded == false)) {
                System.Management.ManagementBaseObject inParams = null;
                System.Management.ManagementBaseObject outParams = PrivateLateBoundObject.InvokeMethod("GetDomain", inParams, null);
                Domain = System.Convert.ToString(outParams.Properties["Domain"].Value);
                return System.Convert.ToUInt32(outParams.Properties["ReturnValue"].Value);
            }
            else {
                Domain = null;
                return System.Convert.ToUInt32(0);
            }
        }
        
        public uint GetGracePeriodDays(out uint DaysLeft) {
            if ((isEmbedded == false)) {
                System.Management.ManagementBaseObject inParams = null;
                System.Management.ManagementBaseObject outParams = PrivateLateBoundObject.InvokeMethod("GetGracePeriodDays", inParams, null);
                DaysLeft = System.Convert.ToUInt32(outParams.Properties["DaysLeft"].Value);
                return System.Convert.ToUInt32(outParams.Properties["ReturnValue"].Value);
            }
            else {
                DaysLeft = System.Convert.ToUInt32(0);
                return System.Convert.ToUInt32(0);
            }
        }
        
        public uint GetRegisteredLicenseServerList(out string[] RegisteredLSList) {
            if ((isEmbedded == false)) {
                System.Management.ManagementBaseObject inParams = null;
                System.Management.ManagementBaseObject outParams = PrivateLateBoundObject.InvokeMethod("GetRegisteredLicenseServerList", inParams, null);
                RegisteredLSList = ((string[])(outParams.Properties["RegisteredLSList"].Value));
                return System.Convert.ToUInt32(outParams.Properties["ReturnValue"].Value);
            }
            else {
                RegisteredLSList = null;
                return System.Convert.ToUInt32(0);
            }
        }
        
        public uint GetSpecifiedLicenseServerList(out string[] SpecifiedLSList) {
            if ((isEmbedded == false)) {
                System.Management.ManagementBaseObject inParams = null;
                System.Management.ManagementBaseObject outParams = PrivateLateBoundObject.InvokeMethod("GetSpecifiedLicenseServerList", inParams, null);
                SpecifiedLSList = ((string[])(outParams.Properties["SpecifiedLSList"].Value));
                return System.Convert.ToUInt32(outParams.Properties["ReturnValue"].Value);
            }
            else {
                SpecifiedLSList = null;
                return System.Convert.ToUInt32(0);
            }
        }
        
        public uint GetTSLanaIds(out string[] LanaIdDescriptions, out uint[] LanaIdList) {
            if ((isEmbedded == false)) {
                System.Management.ManagementBaseObject inParams = null;
                System.Management.ManagementBaseObject outParams = PrivateLateBoundObject.InvokeMethod("GetTSLanaIds", inParams, null);
                LanaIdDescriptions = ((string[])(outParams.Properties["LanaIdDescriptions"].Value));
                LanaIdList = ((uint[])(outParams.Properties["LanaIdList"].Value));
                return System.Convert.ToUInt32(outParams.Properties["ReturnValue"].Value);
            }
            else {
                LanaIdDescriptions = null;
                LanaIdList = null;
                return System.Convert.ToUInt32(0);
            }
        }
        
        public uint GetTStoLSConnectivityStatus(string ServerName, out uint TStoLSConnectivityStatus) {
            if ((isEmbedded == false)) {
                System.Management.ManagementBaseObject inParams = null;
                inParams = PrivateLateBoundObject.GetMethodParameters("GetTStoLSConnectivityStatus");
                inParams["ServerName"] = ((System.String )(ServerName));
                System.Management.ManagementBaseObject outParams = PrivateLateBoundObject.InvokeMethod("GetTStoLSConnectivityStatus", inParams, null);
                TStoLSConnectivityStatus = System.Convert.ToUInt32(outParams.Properties["TStoLSConnectivityStatus"].Value);
                return System.Convert.ToUInt32(outParams.Properties["ReturnValue"].Value);
            }
            else {
                TStoLSConnectivityStatus = System.Convert.ToUInt32(0);
                return System.Convert.ToUInt32(0);
            }
        }
        
        public uint GetWinstationDriverNames(out string[] WinstaDriverNames) {
            if ((isEmbedded == false)) {
                System.Management.ManagementBaseObject inParams = null;
                System.Management.ManagementBaseObject outParams = PrivateLateBoundObject.InvokeMethod("GetWinstationDriverNames", inParams, null);
                WinstaDriverNames = ((string[])(outParams.Properties["WinstaDriverNames"].Value));
                return System.Convert.ToUInt32(outParams.Properties["ReturnValue"].Value);
            }
            else {
                WinstaDriverNames = null;
                return System.Convert.ToUInt32(0);
            }
        }
        
        public uint PingLicenseServer(string ServerName) {
            if ((isEmbedded == false)) {
                System.Management.ManagementBaseObject inParams = null;
                inParams = PrivateLateBoundObject.GetMethodParameters("PingLicenseServer");
                inParams["ServerName"] = ((System.String )(ServerName));
                System.Management.ManagementBaseObject outParams = PrivateLateBoundObject.InvokeMethod("PingLicenseServer", inParams, null);
                return System.Convert.ToUInt32(outParams.Properties["ReturnValue"].Value);
            }
            else {
                return System.Convert.ToUInt32(0);
            }
        }
        
        public uint RemoveLSFromSpecifiedLicenseServerList(string LicenseServerName) {
            if ((isEmbedded == false)) {
                System.Management.ManagementBaseObject inParams = null;
                inParams = PrivateLateBoundObject.GetMethodParameters("RemoveLSFromSpecifiedLicenseServerList");
                inParams["LicenseServerName"] = ((System.String )(LicenseServerName));
                System.Management.ManagementBaseObject outParams = PrivateLateBoundObject.InvokeMethod("RemoveLSFromSpecifiedLicenseServerList", inParams, null);
                return System.Convert.ToUInt32(outParams.Properties["ReturnValue"].Value);
            }
            else {
                return System.Convert.ToUInt32(0);
            }
        }
        
        public uint SetAllowTSConnections(uint AllowTSConnections, uint ModifyFirewallException) {
            if ((isEmbedded == false)) {
                System.Management.ManagementBaseObject inParams = null;
                inParams = PrivateLateBoundObject.GetMethodParameters("SetAllowTSConnections");
                inParams["AllowTSConnections"] = ((System.UInt32 )(AllowTSConnections));
                inParams["ModifyFirewallException"] = ((System.UInt32 )(ModifyFirewallException));
                System.Management.ManagementBaseObject outParams = PrivateLateBoundObject.InvokeMethod("SetAllowTSConnections", inParams, null);
                return System.Convert.ToUInt32(outParams.Properties["ReturnValue"].Value);
            }
            else {
                return System.Convert.ToUInt32(0);
            }
        }
        
        public uint SetDisableForcibleLogoff(uint DisableForcibleLogoff) {
            if ((isEmbedded == false)) {
                System.Management.ManagementBaseObject inParams = null;
                inParams = PrivateLateBoundObject.GetMethodParameters("SetDisableForcibleLogoff");
                inParams["DisableForcibleLogoff"] = ((System.UInt32 )(DisableForcibleLogoff));
                System.Management.ManagementBaseObject outParams = PrivateLateBoundObject.InvokeMethod("SetDisableForcibleLogoff", inParams, null);
                return System.Convert.ToUInt32(outParams.Properties["ReturnValue"].Value);
            }
            else {
                return System.Convert.ToUInt32(0);
            }
        }
        
        public uint SetFallbackPrintDriverType(uint FallbackPrintDriverType) {
            if ((isEmbedded == false)) {
                System.Management.ManagementBaseObject inParams = null;
                inParams = PrivateLateBoundObject.GetMethodParameters("SetFallbackPrintDriverType");
                inParams["FallbackPrintDriverType"] = ((System.UInt32 )(FallbackPrintDriverType));
                System.Management.ManagementBaseObject outParams = PrivateLateBoundObject.InvokeMethod("SetFallbackPrintDriverType", inParams, null);
                return System.Convert.ToUInt32(outParams.Properties["ReturnValue"].Value);
            }
            else {
                return System.Convert.ToUInt32(0);
            }
        }
        
        public uint SetHomeDirectory(string HomeDirectory) {
            if ((isEmbedded == false)) {
                System.Management.ManagementBaseObject inParams = null;
                inParams = PrivateLateBoundObject.GetMethodParameters("SetHomeDirectory");
                inParams["HomeDirectory"] = ((System.String )(HomeDirectory));
                System.Management.ManagementBaseObject outParams = PrivateLateBoundObject.InvokeMethod("SetHomeDirectory", inParams, null);
                return System.Convert.ToUInt32(outParams.Properties["ReturnValue"].Value);
            }
            else {
                return System.Convert.ToUInt32(0);
            }
        }
        
        public uint SetPolicyPropertyName(string PropertyName, bool Value) {
            if ((isEmbedded == false)) {
                System.Management.ManagementBaseObject inParams = null;
                inParams = PrivateLateBoundObject.GetMethodParameters("SetPolicyPropertyName");
                inParams["PropertyName"] = ((System.String )(PropertyName));
                inParams["Value"] = ((System.Boolean )(Value));
                System.Management.ManagementBaseObject outParams = PrivateLateBoundObject.InvokeMethod("SetPolicyPropertyName", inParams, null);
                return System.Convert.ToUInt32(outParams.Properties["ReturnValue"].Value);
            }
            else {
                return System.Convert.ToUInt32(0);
            }
        }
        
        public uint SetPrimaryLicenseServer(string LicenseServerName) {
            if ((isEmbedded == false)) {
                System.Management.ManagementBaseObject inParams = null;
                inParams = PrivateLateBoundObject.GetMethodParameters("SetPrimaryLicenseServer");
                inParams["LicenseServerName"] = ((System.String )(LicenseServerName));
                System.Management.ManagementBaseObject outParams = PrivateLateBoundObject.InvokeMethod("SetPrimaryLicenseServer", inParams, null);
                return System.Convert.ToUInt32(outParams.Properties["ReturnValue"].Value);
            }
            else {
                return System.Convert.ToUInt32(0);
            }
        }
        
        public uint SetProfilePath(string ProfilePath) {
            if ((isEmbedded == false)) {
                System.Management.ManagementBaseObject inParams = null;
                inParams = PrivateLateBoundObject.GetMethodParameters("SetProfilePath");
                inParams["ProfilePath"] = ((System.String )(ProfilePath));
                System.Management.ManagementBaseObject outParams = PrivateLateBoundObject.InvokeMethod("SetProfilePath", inParams, null);
                return System.Convert.ToUInt32(outParams.Properties["ReturnValue"].Value);
            }
            else {
                return System.Convert.ToUInt32(0);
            }
        }
        
        public uint SetSingleSession(uint SingleSession) {
            if ((isEmbedded == false)) {
                System.Management.ManagementBaseObject inParams = null;
                inParams = PrivateLateBoundObject.GetMethodParameters("SetSingleSession");
                inParams["SingleSession"] = ((System.UInt32 )(SingleSession));
                System.Management.ManagementBaseObject outParams = PrivateLateBoundObject.InvokeMethod("SetSingleSession", inParams, null);
                return System.Convert.ToUInt32(outParams.Properties["ReturnValue"].Value);
            }
            else {
                return System.Convert.ToUInt32(0);
            }
        }
        
        public uint SetSpecifiedLicenseServerList(string[] SpecifiedLSList) {
            if ((isEmbedded == false)) {
                System.Management.ManagementBaseObject inParams = null;
                inParams = PrivateLateBoundObject.GetMethodParameters("SetSpecifiedLicenseServerList");
                inParams["SpecifiedLSList"] = ((string[])(SpecifiedLSList));
                System.Management.ManagementBaseObject outParams = PrivateLateBoundObject.InvokeMethod("SetSpecifiedLicenseServerList", inParams, null);
                return System.Convert.ToUInt32(outParams.Properties["ReturnValue"].Value);
            }
            else {
                return System.Convert.ToUInt32(0);
            }
        }
        
        public uint SetTimeZoneRedirection(uint TimeZoneRedirection) {
            if ((isEmbedded == false)) {
                System.Management.ManagementBaseObject inParams = null;
                inParams = PrivateLateBoundObject.GetMethodParameters("SetTimeZoneRedirection");
                inParams["TimeZoneRedirection"] = ((System.UInt32 )(TimeZoneRedirection));
                System.Management.ManagementBaseObject outParams = PrivateLateBoundObject.InvokeMethod("SetTimeZoneRedirection", inParams, null);
                return System.Convert.ToUInt32(outParams.Properties["ReturnValue"].Value);
            }
            else {
                return System.Convert.ToUInt32(0);
            }
        }
        
        public uint UpdateDirectConnectLicenseServer(string LicenseServerList) {
            if ((isEmbedded == false)) {
                System.Management.ManagementBaseObject inParams = null;
                inParams = PrivateLateBoundObject.GetMethodParameters("UpdateDirectConnectLicenseServer");
                inParams["LicenseServerList"] = ((System.String )(LicenseServerList));
                System.Management.ManagementBaseObject outParams = PrivateLateBoundObject.InvokeMethod("UpdateDirectConnectLicenseServer", inParams, null);
                return System.Convert.ToUInt32(outParams.Properties["ReturnValue"].Value);
            }
            else {
                return System.Convert.ToUInt32(0);
            }
        }
        
        public enum ActiveDesktopValues {
            
            TRUE = 0,
            
            FALSE = 1,
            
            NULL_ENUM_VALUE = 2,
        }
        
        public enum AllowTSConnectionsValues {
            
            FALSE = 0,
            
            TRUE = 1,
            
            NULL_ENUM_VALUE = 2,
        }
        
        public enum DeleteTempFoldersValues {
            
            FALSE = 0,
            
            TRUE = 1,
            
            NULL_ENUM_VALUE = 2,
        }
        
        public enum FallbackPrintDriverTypeValues {
            
            No_fallback_dirvers_0 = 0,
            
            Best_guess_1 = 1,
            
            Best_guess_if_no_match_is_found_fallback_to_PCL_2 = 2,
            
            Best_guess_if_no_match_is_found_fallback_to_PS_3 = 3,
            
            Best_guess_if_no_match_is_found_show_both_PCL_and_PS_drivers_4 = 4,
            
            NULL_ENUM_VALUE = 5,
        }
        
        public enum LicensingTypeValues {
            
            Personal_Terminal_Server = 0,
            
            Remote_Desktop_for_Administration = 1,
            
            Per_Device = 2,
            
            Per_User = 3,
            
            Not_Configured = 4,
            
            NULL_ENUM_VALUE = 5,
        }
        
        public enum PossibleLicensingTypesValues {
            
            Personal_Terminal_Server = 0,
            
            Remote_Desktop_for_Administration = 1,
            
            Per_Device = 2,
            
            Per_User = 4,
            
            Not_Configured = 5,
            
            NULL_ENUM_VALUE = 10,
        }
        
        public enum SingleSessionValues {
            
            False = 0,
            
            True = 1,
            
            NULL_ENUM_VALUE = 2,
        }
        
        public enum TerminalServerModeValues {
            
            RemoteAdmin = 0,
            
            AppServer = 1,
            
            NULL_ENUM_VALUE = 2,
        }
        
        public enum UserPermissionValues {
            
            FALSE = 0,
            
            TRUE = 1,
            
            NULL_ENUM_VALUE = 2,
        }
        
        public enum UseTempFoldersValues {
            
            FALSE = 0,
            
            TRUE = 1,
            
            NULL_ENUM_VALUE = 2,
        }
        
        // Enumerator implementation for enumerating instances of the class.
        public class TerminalServiceSettingCollection : object, ICollection {
            
            private ManagementObjectCollection privColObj;
            
            public TerminalServiceSettingCollection(ManagementObjectCollection objCollection) {
                privColObj = objCollection;
            }
            
            public virtual int Count {
                get {
                    return privColObj.Count;
                }
            }
            
            public virtual bool IsSynchronized {
                get {
                    return privColObj.IsSynchronized;
                }
            }
            
            public virtual object SyncRoot {
                get {
                    return this;
                }
            }
            
            public virtual void CopyTo(System.Array array, int index) {
                privColObj.CopyTo(array, index);
                int nCtr;
                for (nCtr = 0; (nCtr < array.Length); nCtr = (nCtr + 1)) {
                    array.SetValue(new TerminalServiceSetting(((System.Management.ManagementObject)(array.GetValue(nCtr)))), nCtr);
                }
            }
            
            public virtual System.Collections.IEnumerator GetEnumerator() {
                return new TerminalServiceSettingEnumerator(privColObj.GetEnumerator());
            }
            
            public class TerminalServiceSettingEnumerator : object, System.Collections.IEnumerator {
                
                private ManagementObjectCollection.ManagementObjectEnumerator privObjEnum;
                
                public TerminalServiceSettingEnumerator(ManagementObjectCollection.ManagementObjectEnumerator objEnum) {
                    privObjEnum = objEnum;
                }
                
                public virtual object Current {
                    get {
                        return new TerminalServiceSetting(((System.Management.ManagementObject)(privObjEnum.Current)));
                    }
                }
                
                public virtual bool MoveNext() {
                    return privObjEnum.MoveNext();
                }
                
                public virtual void Reset() {
                    privObjEnum.Reset();
                }
            }
        }
        
        // TypeConverter to handle null values for ValueType properties
        public class WMIValueTypeConverter : TypeConverter {
            
            private TypeConverter baseConverter;
            
            private System.Type baseType;
            
            public WMIValueTypeConverter(System.Type inBaseType) {
                baseConverter = TypeDescriptor.GetConverter(inBaseType);
                baseType = inBaseType;
            }
            
            public override bool CanConvertFrom(System.ComponentModel.ITypeDescriptorContext context, System.Type srcType) {
                return baseConverter.CanConvertFrom(context, srcType);
            }
            
            public override bool CanConvertTo(System.ComponentModel.ITypeDescriptorContext context, System.Type destinationType) {
                return baseConverter.CanConvertTo(context, destinationType);
            }
            
            public override object ConvertFrom(System.ComponentModel.ITypeDescriptorContext context, System.Globalization.CultureInfo culture, object value) {
                return baseConverter.ConvertFrom(context, culture, value);
            }
            
            public override object CreateInstance(System.ComponentModel.ITypeDescriptorContext context, System.Collections.IDictionary dictionary) {
                return baseConverter.CreateInstance(context, dictionary);
            }
            
            public override bool GetCreateInstanceSupported(System.ComponentModel.ITypeDescriptorContext context) {
                return baseConverter.GetCreateInstanceSupported(context);
            }
            
            public override PropertyDescriptorCollection GetProperties(System.ComponentModel.ITypeDescriptorContext context, object value, System.Attribute[] attributeVar) {
                return baseConverter.GetProperties(context, value, attributeVar);
            }
            
            public override bool GetPropertiesSupported(System.ComponentModel.ITypeDescriptorContext context) {
                return baseConverter.GetPropertiesSupported(context);
            }
            
            public override System.ComponentModel.TypeConverter.StandardValuesCollection GetStandardValues(System.ComponentModel.ITypeDescriptorContext context) {
                return baseConverter.GetStandardValues(context);
            }
            
            public override bool GetStandardValuesExclusive(System.ComponentModel.ITypeDescriptorContext context) {
                return baseConverter.GetStandardValuesExclusive(context);
            }
            
            public override bool GetStandardValuesSupported(System.ComponentModel.ITypeDescriptorContext context) {
                return baseConverter.GetStandardValuesSupported(context);
            }
            
            public override object ConvertTo(System.ComponentModel.ITypeDescriptorContext context, System.Globalization.CultureInfo culture, object value, System.Type destinationType) {
                if ((baseType.BaseType == typeof(System.Enum))) {
                    if ((value.GetType() == destinationType)) {
                        return value;
                    }
                    if ((((value == null) 
                                && (context != null)) 
                                && (context.PropertyDescriptor.ShouldSerializeValue(context.Instance) == false))) {
                        return  "NULL_ENUM_VALUE" ;
                    }
                    return baseConverter.ConvertTo(context, culture, value, destinationType);
                }
                if (((baseType == typeof(bool)) 
                            && (baseType.BaseType == typeof(System.ValueType)))) {
                    if ((((value == null) 
                                && (context != null)) 
                                && (context.PropertyDescriptor.ShouldSerializeValue(context.Instance) == false))) {
                        return "";
                    }
                    return baseConverter.ConvertTo(context, culture, value, destinationType);
                }
                if (((context != null) 
                            && (context.PropertyDescriptor.ShouldSerializeValue(context.Instance) == false))) {
                    return "";
                }
                return baseConverter.ConvertTo(context, culture, value, destinationType);
            }
        }
        
        // Embedded class to represent WMI system Properties.
        [TypeConverter(typeof(System.ComponentModel.ExpandableObjectConverter))]
        public class ManagementSystemProperties {
            
            private System.Management.ManagementBaseObject PrivateLateBoundObject;
            
            public ManagementSystemProperties(System.Management.ManagementBaseObject ManagedObject) {
                PrivateLateBoundObject = ManagedObject;
            }
            
            [Browsable(true)]
            public int GENUS {
                get {
                    return ((int)(PrivateLateBoundObject["__GENUS"]));
                }
            }
            
            [Browsable(true)]
            public string CLASS {
                get {
                    return ((string)(PrivateLateBoundObject["__CLASS"]));
                }
            }
            
            [Browsable(true)]
            public string SUPERCLASS {
                get {
                    return ((string)(PrivateLateBoundObject["__SUPERCLASS"]));
                }
            }
            
            [Browsable(true)]
            public string DYNASTY {
                get {
                    return ((string)(PrivateLateBoundObject["__DYNASTY"]));
                }
            }
            
            [Browsable(true)]
            public string RELPATH {
                get {
                    return ((string)(PrivateLateBoundObject["__RELPATH"]));
                }
            }
            
            [Browsable(true)]
            public int PROPERTY_COUNT {
                get {
                    return ((int)(PrivateLateBoundObject["__PROPERTY_COUNT"]));
                }
            }
            
            [Browsable(true)]
            public string[] DERIVATION {
                get {
                    return ((string[])(PrivateLateBoundObject["__DERIVATION"]));
                }
            }
            
            [Browsable(true)]
            public string SERVER {
                get {
                    return ((string)(PrivateLateBoundObject["__SERVER"]));
                }
            }
            
            [Browsable(true)]
            public string NAMESPACE {
                get {
                    return ((string)(PrivateLateBoundObject["__NAMESPACE"]));
                }
            }
            
            [Browsable(true)]
            public string PATH {
                get {
                    return ((string)(PrivateLateBoundObject["__PATH"]));
                }
            }
        }
    }
}
