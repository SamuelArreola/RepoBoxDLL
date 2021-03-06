﻿//------------------------------------------------------------------------------
// <auto-generated>
//     Este código fue generado por una herramienta.
//     Versión de runtime:4.0.30319.42000
//
//     Los cambios en este archivo podrían causar un comportamiento incorrecto y se perderán si
//     se vuelve a generar el código.
// </auto-generated>
//------------------------------------------------------------------------------

// 
// Microsoft.VSDesigner generó automáticamente este código fuente, versión=4.0.30319.42000.
// 
#pragma warning disable 1591

namespace RepoBox.RepoWS {
    using System;
    using System.Web.Services;
    using System.Diagnostics;
    using System.Web.Services.Protocols;
    using System.Xml.Serialization;
    using System.ComponentModel;
    
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.7.2046.0")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Web.Services.WebServiceBindingAttribute(Name="Application", Namespace="https://timbrador.cepdi.mx:8443/WSProduccion/WS?WSDL")]
    public partial class StampSOAP : System.Web.Services.Protocols.SoapHttpClientProtocol {
        
        private System.Threading.SendOrPostCallback stampOperationCompleted;
        
        private System.Threading.SendOrPostCallback stampStrOperationCompleted;
        
        private System.Threading.SendOrPostCallback stampedOperationCompleted;
        
        private System.Threading.SendOrPostCallback quick_stampOperationCompleted;
        
        private bool useDefaultCredentialsSetExplicitly;
        
        /// <remarks/>
        public StampSOAP() {
            this.Url = global::RepoBox.Properties.Settings.Default.RepoBox_RepoWS_StampSOAP;
            if ((this.IsLocalFileSystemWebService(this.Url) == true)) {
                this.UseDefaultCredentials = true;
                this.useDefaultCredentialsSetExplicitly = false;
            }
            else {
                this.useDefaultCredentialsSetExplicitly = true;
            }
        }
        
        public new string Url {
            get {
                return base.Url;
            }
            set {
                if ((((this.IsLocalFileSystemWebService(base.Url) == true) 
                            && (this.useDefaultCredentialsSetExplicitly == false)) 
                            && (this.IsLocalFileSystemWebService(value) == false))) {
                    base.UseDefaultCredentials = false;
                }
                base.Url = value;
            }
        }
        
        public new bool UseDefaultCredentials {
            get {
                return base.UseDefaultCredentials;
            }
            set {
                base.UseDefaultCredentials = value;
                this.useDefaultCredentialsSetExplicitly = true;
            }
        }
        
        /// <remarks/>
        public event stampCompletedEventHandler stampCompleted;
        
        /// <remarks/>
        public event stampStrCompletedEventHandler stampStrCompleted;
        
        /// <remarks/>
        public event stampedCompletedEventHandler stampedCompleted;
        
        /// <remarks/>
        public event quick_stampCompletedEventHandler quick_stampCompleted;
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("stamp", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Bare)]
        [return: System.Xml.Serialization.XmlElementAttribute("stampResponse", Namespace="https://timbrador.cepdi.mx:8443/WSProduccion/WS?WSDL")]
        public stampResponse stamp([System.Xml.Serialization.XmlElementAttribute("stamp", Namespace="https://timbrador.cepdi.mx:8443/WSProduccion/WS?WSDL")] stamp stamp1) {
            object[] results = this.Invoke("stamp", new object[] {
                        stamp1});
            return ((stampResponse)(results[0]));
        }
        
        /// <remarks/>
        public void stampAsync(stamp stamp1) {
            this.stampAsync(stamp1, null);
        }
        
        /// <remarks/>
        public void stampAsync(stamp stamp1, object userState) {
            if ((this.stampOperationCompleted == null)) {
                this.stampOperationCompleted = new System.Threading.SendOrPostCallback(this.OnstampOperationCompleted);
            }
            this.InvokeAsync("stamp", new object[] {
                        stamp1}, this.stampOperationCompleted, userState);
        }
        
        private void OnstampOperationCompleted(object arg) {
            if ((this.stampCompleted != null)) {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.stampCompleted(this, new stampCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("stampStr", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Bare)]
        [return: System.Xml.Serialization.XmlElementAttribute("stampStrResponse", Namespace="https://timbrador.cepdi.mx:8443/WSProduccion/WS?WSDL")]
        public stampStrResponse stampStr([System.Xml.Serialization.XmlElementAttribute("stampStr", Namespace="https://timbrador.cepdi.mx:8443/WSProduccion/WS?WSDL")] stampStr stampStr1) {
            object[] results = this.Invoke("stampStr", new object[] {
                        stampStr1});
            return ((stampStrResponse)(results[0]));
        }
        
        /// <remarks/>
        public void stampStrAsync(stampStr stampStr1) {
            this.stampStrAsync(stampStr1, null);
        }
        
        /// <remarks/>
        public void stampStrAsync(stampStr stampStr1, object userState) {
            if ((this.stampStrOperationCompleted == null)) {
                this.stampStrOperationCompleted = new System.Threading.SendOrPostCallback(this.OnstampStrOperationCompleted);
            }
            this.InvokeAsync("stampStr", new object[] {
                        stampStr1}, this.stampStrOperationCompleted, userState);
        }
        
        private void OnstampStrOperationCompleted(object arg) {
            if ((this.stampStrCompleted != null)) {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.stampStrCompleted(this, new stampStrCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("stamped", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Bare)]
        [return: System.Xml.Serialization.XmlElementAttribute("stampedResponse", Namespace="https://timbrador.cepdi.mx:8443/WSProduccion/WS?WSDL")]
        public stampedResponse stamped([System.Xml.Serialization.XmlElementAttribute("stamped", Namespace="https://timbrador.cepdi.mx:8443/WSProduccion/WS?WSDL")] stamped stamped1) {
            object[] results = this.Invoke("stamped", new object[] {
                        stamped1});
            return ((stampedResponse)(results[0]));
        }
        
        /// <remarks/>
        public void stampedAsync(stamped stamped1) {
            this.stampedAsync(stamped1, null);
        }
        
        /// <remarks/>
        public void stampedAsync(stamped stamped1, object userState) {
            if ((this.stampedOperationCompleted == null)) {
                this.stampedOperationCompleted = new System.Threading.SendOrPostCallback(this.OnstampedOperationCompleted);
            }
            this.InvokeAsync("stamped", new object[] {
                        stamped1}, this.stampedOperationCompleted, userState);
        }
        
        private void OnstampedOperationCompleted(object arg) {
            if ((this.stampedCompleted != null)) {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.stampedCompleted(this, new stampedCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("quick_stamp", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Bare)]
        [return: System.Xml.Serialization.XmlElementAttribute("quick_stampResponse", Namespace="https://timbrador.cepdi.mx:8443/WSProduccion/WS?WSDL")]
        public quick_stampResponse quick_stamp([System.Xml.Serialization.XmlElementAttribute("quick_stamp", Namespace="https://timbrador.cepdi.mx:8443/WSProduccion/WS?WSDL")] quick_stamp quick_stamp1) {
            object[] results = this.Invoke("quick_stamp", new object[] {
                        quick_stamp1});
            return ((quick_stampResponse)(results[0]));
        }
        
        /// <remarks/>
        public void quick_stampAsync(quick_stamp quick_stamp1) {
            this.quick_stampAsync(quick_stamp1, null);
        }
        
        /// <remarks/>
        public void quick_stampAsync(quick_stamp quick_stamp1, object userState) {
            if ((this.quick_stampOperationCompleted == null)) {
                this.quick_stampOperationCompleted = new System.Threading.SendOrPostCallback(this.Onquick_stampOperationCompleted);
            }
            this.InvokeAsync("quick_stamp", new object[] {
                        quick_stamp1}, this.quick_stampOperationCompleted, userState);
        }
        
        private void Onquick_stampOperationCompleted(object arg) {
            if ((this.quick_stampCompleted != null)) {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.quick_stampCompleted(this, new quick_stampCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }
        
        /// <remarks/>
        public new void CancelAsync(object userState) {
            base.CancelAsync(userState);
        }
        
        private bool IsLocalFileSystemWebService(string url) {
            if (((url == null) 
                        || (url == string.Empty))) {
                return false;
            }
            System.Uri wsUri = new System.Uri(url);
            if (((wsUri.Port >= 1024) 
                        && (string.Compare(wsUri.Host, "localHost", System.StringComparison.OrdinalIgnoreCase) == 0))) {
                return true;
            }
            return false;
        }
    }
    
    /// <comentarios/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.7.2102.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="https://timbrador.cepdi.mx:8443/WSProduccion/WS?WSDL")]
    public partial class stamp {
        
        private string usernameField;
        
        private string passwordField;
        
        private byte[] xmlField;
        
        /// <comentarios/>
        [System.Xml.Serialization.XmlElementAttribute(IsNullable=true)]
        public string username {
            get {
                return this.usernameField;
            }
            set {
                this.usernameField = value;
            }
        }
        
        /// <comentarios/>
        [System.Xml.Serialization.XmlElementAttribute(IsNullable=true)]
        public string password {
            get {
                return this.passwordField;
            }
            set {
                this.passwordField = value;
            }
        }
        
        /// <comentarios/>
        [System.Xml.Serialization.XmlElementAttribute(DataType="base64Binary", IsNullable=true)]
        public byte[] xml {
            get {
                return this.xmlField;
            }
            set {
                this.xmlField = value;
            }
        }
    }
    
    /// <comentarios/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.7.2102.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="https://timbrador.cepdi.mx:8443/WSProduccion/WS?WSDL")]
    public partial class quick_stampResponse {
        
        private AcuseRecepcionCFDI quick_stampResultField;
        
        /// <comentarios/>
        [System.Xml.Serialization.XmlElementAttribute(IsNullable=true)]
        public AcuseRecepcionCFDI quick_stampResult {
            get {
                return this.quick_stampResultField;
            }
            set {
                this.quick_stampResultField = value;
            }
        }
    }
    
    /// <comentarios/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.7.2102.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="soap.complex_model")]
    public partial class AcuseRecepcionCFDI {
        
        private string xmlField;
        
        private string uUIDField;
        
        private string noCertificadoSATField;
        
        private string fechaField;
        
        private string codEstatusField;
        
        private string faultcodeField;
        
        private string satSealField;
        
        private Incidencia[] incidenciasField;
        
        private string faultstringField;
        
        /// <comentarios/>
        [System.Xml.Serialization.XmlElementAttribute(IsNullable=true)]
        public string xml {
            get {
                return this.xmlField;
            }
            set {
                this.xmlField = value;
            }
        }
        
        /// <comentarios/>
        [System.Xml.Serialization.XmlElementAttribute(IsNullable=true)]
        public string UUID {
            get {
                return this.uUIDField;
            }
            set {
                this.uUIDField = value;
            }
        }
        
        /// <comentarios/>
        [System.Xml.Serialization.XmlElementAttribute(IsNullable=true)]
        public string NoCertificadoSAT {
            get {
                return this.noCertificadoSATField;
            }
            set {
                this.noCertificadoSATField = value;
            }
        }
        
        /// <comentarios/>
        [System.Xml.Serialization.XmlElementAttribute(IsNullable=true)]
        public string Fecha {
            get {
                return this.fechaField;
            }
            set {
                this.fechaField = value;
            }
        }
        
        /// <comentarios/>
        [System.Xml.Serialization.XmlElementAttribute(IsNullable=true)]
        public string CodEstatus {
            get {
                return this.codEstatusField;
            }
            set {
                this.codEstatusField = value;
            }
        }
        
        /// <comentarios/>
        [System.Xml.Serialization.XmlElementAttribute(IsNullable=true)]
        public string faultcode {
            get {
                return this.faultcodeField;
            }
            set {
                this.faultcodeField = value;
            }
        }
        
        /// <comentarios/>
        [System.Xml.Serialization.XmlElementAttribute(IsNullable=true)]
        public string SatSeal {
            get {
                return this.satSealField;
            }
            set {
                this.satSealField = value;
            }
        }
        
        /// <comentarios/>
        [System.Xml.Serialization.XmlArrayAttribute(IsNullable=true)]
        public Incidencia[] Incidencias {
            get {
                return this.incidenciasField;
            }
            set {
                this.incidenciasField = value;
            }
        }
        
        /// <comentarios/>
        [System.Xml.Serialization.XmlElementAttribute(IsNullable=true)]
        public string faultstring {
            get {
                return this.faultstringField;
            }
            set {
                this.faultstringField = value;
            }
        }
    }
    
    /// <comentarios/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.7.2102.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="soap.complex_model")]
    public partial class Incidencia {
        
        private string idIncidenciaField;
        
        private string uuidField;
        
        private string codigoErrorField;
        
        private string workProcessIdField;
        
        private string mensajeIncidenciaField;
        
        private string rfcEmisorField;
        
        private string noCertificadoPacField;
        
        private string fechaRegistroField;
        
        /// <comentarios/>
        [System.Xml.Serialization.XmlElementAttribute(IsNullable=true)]
        public string IdIncidencia {
            get {
                return this.idIncidenciaField;
            }
            set {
                this.idIncidenciaField = value;
            }
        }
        
        /// <comentarios/>
        [System.Xml.Serialization.XmlElementAttribute(IsNullable=true)]
        public string Uuid {
            get {
                return this.uuidField;
            }
            set {
                this.uuidField = value;
            }
        }
        
        /// <comentarios/>
        [System.Xml.Serialization.XmlElementAttribute(IsNullable=true)]
        public string CodigoError {
            get {
                return this.codigoErrorField;
            }
            set {
                this.codigoErrorField = value;
            }
        }
        
        /// <comentarios/>
        [System.Xml.Serialization.XmlElementAttribute(IsNullable=true)]
        public string WorkProcessId {
            get {
                return this.workProcessIdField;
            }
            set {
                this.workProcessIdField = value;
            }
        }
        
        /// <comentarios/>
        [System.Xml.Serialization.XmlElementAttribute(IsNullable=true)]
        public string MensajeIncidencia {
            get {
                return this.mensajeIncidenciaField;
            }
            set {
                this.mensajeIncidenciaField = value;
            }
        }
        
        /// <comentarios/>
        [System.Xml.Serialization.XmlElementAttribute(IsNullable=true)]
        public string RfcEmisor {
            get {
                return this.rfcEmisorField;
            }
            set {
                this.rfcEmisorField = value;
            }
        }
        
        /// <comentarios/>
        [System.Xml.Serialization.XmlElementAttribute(IsNullable=true)]
        public string NoCertificadoPac {
            get {
                return this.noCertificadoPacField;
            }
            set {
                this.noCertificadoPacField = value;
            }
        }
        
        /// <comentarios/>
        [System.Xml.Serialization.XmlElementAttribute(IsNullable=true)]
        public string FechaRegistro {
            get {
                return this.fechaRegistroField;
            }
            set {
                this.fechaRegistroField = value;
            }
        }
    }
    
    /// <comentarios/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.7.2102.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="https://timbrador.cepdi.mx:8443/WSProduccion/WS?WSDL")]
    public partial class quick_stamp {
        
        private string usernameField;
        
        private string passwordField;
        
        private byte[] xmlField;
        
        /// <comentarios/>
        [System.Xml.Serialization.XmlElementAttribute(IsNullable=true)]
        public string username {
            get {
                return this.usernameField;
            }
            set {
                this.usernameField = value;
            }
        }
        
        /// <comentarios/>
        [System.Xml.Serialization.XmlElementAttribute(IsNullable=true)]
        public string password {
            get {
                return this.passwordField;
            }
            set {
                this.passwordField = value;
            }
        }
        
        /// <comentarios/>
        [System.Xml.Serialization.XmlElementAttribute(DataType="base64Binary", IsNullable=true)]
        public byte[] xml {
            get {
                return this.xmlField;
            }
            set {
                this.xmlField = value;
            }
        }
    }
    
    /// <comentarios/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.7.2102.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="https://timbrador.cepdi.mx:8443/WSProduccion/WS?WSDL")]
    public partial class stampedResponse {
        
        private AcuseRecepcionCFDI stampedResultField;
        
        /// <comentarios/>
        [System.Xml.Serialization.XmlElementAttribute(IsNullable=true)]
        public AcuseRecepcionCFDI stampedResult {
            get {
                return this.stampedResultField;
            }
            set {
                this.stampedResultField = value;
            }
        }
    }
    
    /// <comentarios/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.7.2102.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="https://timbrador.cepdi.mx:8443/WSProduccion/WS?WSDL")]
    public partial class stamped {
        
        private string usernameField;
        
        private string passwordField;
        
        private byte[] xmlField;
        
        /// <comentarios/>
        [System.Xml.Serialization.XmlElementAttribute(IsNullable=true)]
        public string username {
            get {
                return this.usernameField;
            }
            set {
                this.usernameField = value;
            }
        }
        
        /// <comentarios/>
        [System.Xml.Serialization.XmlElementAttribute(IsNullable=true)]
        public string password {
            get {
                return this.passwordField;
            }
            set {
                this.passwordField = value;
            }
        }
        
        /// <comentarios/>
        [System.Xml.Serialization.XmlElementAttribute(DataType="base64Binary", IsNullable=true)]
        public byte[] xml {
            get {
                return this.xmlField;
            }
            set {
                this.xmlField = value;
            }
        }
    }
    
    /// <comentarios/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.7.2102.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="https://timbrador.cepdi.mx:8443/WSProduccion/WS?WSDL")]
    public partial class stampStrResponse {
        
        private string stampStrResultField;
        
        /// <comentarios/>
        [System.Xml.Serialization.XmlElementAttribute(IsNullable=true)]
        public string stampStrResult {
            get {
                return this.stampStrResultField;
            }
            set {
                this.stampStrResultField = value;
            }
        }
    }
    
    /// <comentarios/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.7.2102.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="https://timbrador.cepdi.mx:8443/WSProduccion/WS?WSDL")]
    public partial class stampStr {
        
        private string usernameField;
        
        private string passwordField;
        
        private byte[] xmlField;
        
        /// <comentarios/>
        [System.Xml.Serialization.XmlElementAttribute(IsNullable=true)]
        public string username {
            get {
                return this.usernameField;
            }
            set {
                this.usernameField = value;
            }
        }
        
        /// <comentarios/>
        [System.Xml.Serialization.XmlElementAttribute(IsNullable=true)]
        public string password {
            get {
                return this.passwordField;
            }
            set {
                this.passwordField = value;
            }
        }
        
        /// <comentarios/>
        [System.Xml.Serialization.XmlElementAttribute(DataType="base64Binary", IsNullable=true)]
        public byte[] xml {
            get {
                return this.xmlField;
            }
            set {
                this.xmlField = value;
            }
        }
    }
    
    /// <comentarios/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.7.2102.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="https://timbrador.cepdi.mx:8443/WSProduccion/WS?WSDL")]
    public partial class stampResponse {
        
        private AcuseRecepcionCFDI stampResultField;
        
        /// <comentarios/>
        [System.Xml.Serialization.XmlElementAttribute(IsNullable=true)]
        public AcuseRecepcionCFDI stampResult {
            get {
                return this.stampResultField;
            }
            set {
                this.stampResultField = value;
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.7.2046.0")]
    public delegate void stampCompletedEventHandler(object sender, stampCompletedEventArgs e);
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.7.2046.0")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class stampCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs {
        
        private object[] results;
        
        internal stampCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) : 
                base(exception, cancelled, userState) {
            this.results = results;
        }
        
        /// <remarks/>
        public stampResponse Result {
            get {
                this.RaiseExceptionIfNecessary();
                return ((stampResponse)(this.results[0]));
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.7.2046.0")]
    public delegate void stampStrCompletedEventHandler(object sender, stampStrCompletedEventArgs e);
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.7.2046.0")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class stampStrCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs {
        
        private object[] results;
        
        internal stampStrCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) : 
                base(exception, cancelled, userState) {
            this.results = results;
        }
        
        /// <remarks/>
        public stampStrResponse Result {
            get {
                this.RaiseExceptionIfNecessary();
                return ((stampStrResponse)(this.results[0]));
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.7.2046.0")]
    public delegate void stampedCompletedEventHandler(object sender, stampedCompletedEventArgs e);
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.7.2046.0")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class stampedCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs {
        
        private object[] results;
        
        internal stampedCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) : 
                base(exception, cancelled, userState) {
            this.results = results;
        }
        
        /// <remarks/>
        public stampedResponse Result {
            get {
                this.RaiseExceptionIfNecessary();
                return ((stampedResponse)(this.results[0]));
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.7.2046.0")]
    public delegate void quick_stampCompletedEventHandler(object sender, quick_stampCompletedEventArgs e);
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.7.2046.0")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class quick_stampCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs {
        
        private object[] results;
        
        internal quick_stampCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) : 
                base(exception, cancelled, userState) {
            this.results = results;
        }
        
        /// <remarks/>
        public quick_stampResponse Result {
            get {
                this.RaiseExceptionIfNecessary();
                return ((quick_stampResponse)(this.results[0]));
            }
        }
    }
}

#pragma warning restore 1591