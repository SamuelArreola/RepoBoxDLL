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
// This source code was auto-generated by xsd, Version=4.0.30319.33440.
// 
namespace RepoBox.CE11 {
    using System.Xml.Serialization;
    using System.Collections.Generic;
    using System.Xml;
    using System.IO;
    using System;
    
    
    /// <comentarios/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.33440")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType=true, Namespace="http://www.sat.gob.mx/ComercioExterior11")]
    [System.Xml.Serialization.XmlRootAttribute(Namespace="http://www.sat.gob.mx/ComercioExterior11", IsNullable=false)]
    public partial class ComercioExterior {
        [XmlNamespaceDeclarationsAttribute]
        public XmlSerializerNamespaces myNamespaces = new XmlSerializerNamespaces();
        public string GenerarCFD()
        {
            try
            {
                //string validacion = this.Validar();
                //if (validacion != "OK")
                //    return validacion;
                XmlDocument document = new XmlDocument();
                System.Xml.Serialization.XmlSerializer x = new System.Xml.Serialization.XmlSerializer(this.GetType());
                using (MemoryStream stream = new MemoryStream())
                {
                    x.Serialize(stream, this);
                    //electronicDocument.SaveToStream(stream);
                    stream.Position = 0L;
                    StreamReader reader = new StreamReader(stream);
                    string xml = reader.ReadToEnd();
                    reader.Close();
                    document.LoadXml(xml);
                }
                return document.InnerXml;
            }
            catch (Exception ex)
            {
                if (ex.Message.StartsWith("RepoBox"))
                    throw new Exception(ex.Message);
                else
                    throw new Exception("RepoBox: " + ex.Message);
            }
        }
        private Emisor emisorField;
        
        private List<Propietario> propietarioField;
        
        private Receptor receptorField;
        
        private List<Destinatario> destinatarioField;
        
        private List<Mercancia> mercanciasField;
        
        private string versionField;
        
        private string motivoTrasladoField;
        
        private string tipoOperacionField;
        
        private string claveDePedimentoField;
        
        private int certificadoOrigenField;
        
        private bool certificadoOrigenFieldSpecified;
        
        private string numCertificadoOrigenField;
        
        private string numeroExportadorConfiableField;
        
        private string incotermField;
        
        private int subdivisionField;
        
        private bool subdivisionFieldSpecified;
        
        private string observacionesField;
        
        private decimal tipoCambioUSDField;
        
        private bool tipoCambioUSDFieldSpecified;
        
        private decimal totalUSDField;
        
        private bool totalUSDFieldSpecified;
        
        public ComercioExterior() {
            this.versionField = "1.1";
            this.myNamespaces.Add("cce", "http://www.sat.gob.mx/ComercioExterior11");
        }
        
        /// <comentarios/>
        public Emisor Emisor {
            get {
                return this.emisorField;
            }
            set {
                this.emisorField = value;
            }
        }
        
        /// <comentarios/>
        [System.Xml.Serialization.XmlElementAttribute("Propietario")]
        public List<Propietario> Propietario {
            get {
                return this.propietarioField;
            }
            set {
                this.propietarioField = value;
            }
        }
        
        /// <comentarios/>
        public Receptor Receptor {
            get {
                return this.receptorField;
            }
            set {
                this.receptorField = value;
            }
        }
        
        /// <comentarios/>
        [System.Xml.Serialization.XmlElementAttribute("Destinatario")]
        public List<Destinatario> Destinatario {
            get {
                return this.destinatarioField;
            }
            set {
                this.destinatarioField = value;
            }
        }
        
        /// <comentarios/>
        [System.Xml.Serialization.XmlArrayItemAttribute("Mercancia", IsNullable=false)]
        public List<Mercancia> Mercancias {
            get {
                return this.mercanciasField;
            }
            set {
                this.mercanciasField = value;
            }
        }
        
        /// <comentarios/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string Version {
            get {
                return this.versionField;
            }
            set {
                this.versionField = value;
            }
        }
        
        /// <comentarios/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string MotivoTraslado {
            get {
                return this.motivoTrasladoField;
            }
            set {
                this.motivoTrasladoField = value;
            }
        }
        
        /// <comentarios/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string TipoOperacion {
            get {
                return this.tipoOperacionField;
            }
            set {
                this.tipoOperacionField = value;
            }
        }
        
        /// <comentarios/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string ClaveDePedimento {
            get {
                return this.claveDePedimentoField;
            }
            set {
                this.claveDePedimentoField = value;
            }
        }
        
        /// <comentarios/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public int CertificadoOrigen {
            get {
                return this.certificadoOrigenField;
            }
            set {
                this.certificadoOrigenField = value;
                this.certificadoOrigenFieldSpecified = true;
            }
        }
        
        /// <comentarios/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool CertificadoOrigenSpecified {
            get {
                return this.certificadoOrigenFieldSpecified;
            }
            set {
                this.certificadoOrigenFieldSpecified = value;
            }
        }
        
        /// <comentarios/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string NumCertificadoOrigen {
            get {
                return this.numCertificadoOrigenField;
            }
            set {
                this.numCertificadoOrigenField = value;
            }
        }
        
        /// <comentarios/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string NumeroExportadorConfiable {
            get {
                return this.numeroExportadorConfiableField;
            }
            set {
                this.numeroExportadorConfiableField = value;
            }
        }
        
        /// <comentarios/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string Incoterm {
            get {
                return this.incotermField;
            }
            set {
                this.incotermField = value;
            }
        }
        
        /// <comentarios/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public int Subdivision {
            get {
                return this.subdivisionField;
            }
            set {
                this.subdivisionField = value;
                this.subdivisionFieldSpecified = true;
            }
        }
        
        /// <comentarios/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool SubdivisionSpecified {
            get {
                return this.subdivisionFieldSpecified;
            }
            set {
                this.subdivisionFieldSpecified = value;
            }
        }
        
        /// <comentarios/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string Observaciones {
            get {
                return this.observacionesField;
            }
            set {
                this.observacionesField = value;
            }
        }
        
        /// <comentarios/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public decimal TipoCambioUSD {
            get {
                return this.tipoCambioUSDField;
            }
            set {
                this.tipoCambioUSDField = value;
                this.tipoCambioUSDFieldSpecified = (value > 0);
            }
        }
        
        /// <comentarios/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool TipoCambioUSDSpecified {
            get {
                return this.tipoCambioUSDFieldSpecified;
            }
            set {
                this.tipoCambioUSDFieldSpecified = value;
            }
        }
        
        /// <comentarios/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public decimal TotalUSD {
            get {
                return this.totalUSDField;
            }
            set {
                this.totalUSDField = value;
                this.totalUSDFieldSpecified = (value > 0);
            }
        }
        
        /// <comentarios/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool TotalUSDSpecified {
            get {
                return this.totalUSDFieldSpecified;
            }
            set {
                this.totalUSDFieldSpecified = value;
            }
        }
    }
    
    /// <comentarios/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.33440")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType=true, Namespace="http://www.sat.gob.mx/ComercioExterior11")]
    public partial class Emisor {
        
        private Domicilio domicilioField;
        
        private string curpField;
        
        /// <comentarios/>
        public Domicilio Domicilio {
            get {
                return this.domicilioField;
            }
            set {
                this.domicilioField = value;
            }
        }
        
        /// <comentarios/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string Curp {
            get {
                return this.curpField;
            }
            set {
                this.curpField = value;
            }
        }
    }
    
    /// <comentarios/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.33440")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType=true, Namespace="http://www.sat.gob.mx/ComercioExterior11")]
    public partial class Domicilio {
        
        private string calleField;
        
        private string numeroExteriorField;
        
        private string numeroInteriorField;
        
        private string coloniaField;
        
        private string localidadField;
        
        private string referenciaField;
        
        private string municipioField;
        
        private string estadoField;
        
        private string paisField;
        
        private string codigoPostalField;
        
        /// <comentarios/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string Calle {
            get {
                return this.calleField;
            }
            set {
                this.calleField = value;
            }
        }
        
        /// <comentarios/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string NumeroExterior {
            get {
                return this.numeroExteriorField;
            }
            set {
                this.numeroExteriorField = value;
            }
        }
        
        /// <comentarios/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string NumeroInterior {
            get {
                return this.numeroInteriorField;
            }
            set {
                this.numeroInteriorField = value;
            }
        }
        
        /// <comentarios/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string Colonia {
            get {
                return this.coloniaField;
            }
            set {
                this.coloniaField = value;
            }
        }
        
        /// <comentarios/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string Localidad {
            get {
                return this.localidadField;
            }
            set {
                this.localidadField = value;
            }
        }
        
        /// <comentarios/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string Referencia {
            get {
                return this.referenciaField;
            }
            set {
                this.referenciaField = value;
            }
        }
        
        /// <comentarios/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string Municipio {
            get {
                return this.municipioField;
            }
            set {
                this.municipioField = value;
            }
        }
        
        /// <comentarios/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string Estado {
            get {
                return this.estadoField;
            }
            set {
                this.estadoField = value;
            }
        }
        
        /// <comentarios/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string Pais {
            get {
                return this.paisField;
            }
            set {
                this.paisField = value;
            }
        }
        
        /// <comentarios/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string CodigoPostal {
            get {
                return this.codigoPostalField;
            }
            set {
                this.codigoPostalField = value;
            }
        }
    }
    
    /// <comentarios/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.33440")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType=true, Namespace="http://www.sat.gob.mx/ComercioExterior11")]
    public partial class Propietario {
        
        private string numRegIdTribField;
        
        private string residenciaFiscalField;
        
        /// <comentarios/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string NumRegIdTrib {
            get {
                return this.numRegIdTribField;
            }
            set {
                this.numRegIdTribField = value;
            }
        }
        
        /// <comentarios/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string ResidenciaFiscal {
            get {
                return this.residenciaFiscalField;
            }
            set {
                this.residenciaFiscalField = value;
            }
        }
    }
    
    /// <comentarios/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.33440")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType=true, Namespace="http://www.sat.gob.mx/ComercioExterior11")]
    public partial class Receptor {
        
        private Domicilio domicilioField;
        
        private string numRegIdTribField;
        
        /// <comentarios/>
        public Domicilio Domicilio {
            get {
                return this.domicilioField;
            }
            set {
                this.domicilioField = value;
            }
        }
        
        /// <comentarios/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string NumRegIdTrib {
            get {
                return this.numRegIdTribField;
            }
            set {
                this.numRegIdTribField = value;
            }
        }
    }
    
    /// <comentarios/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.33440")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType=true, Namespace="http://www.sat.gob.mx/ComercioExterior11")]
    internal partial class ComercioExteriorReceptorDomicilio {
        
        private string calleField;
        
        private string numeroExteriorField;
        
        private string numeroInteriorField;
        
        private string coloniaField;
        
        private string localidadField;
        
        private string referenciaField;
        
        private string municipioField;
        
        private string estadoField;
        
        private string paisField;
        
        private string codigoPostalField;
        
        /// <comentarios/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string Calle {
            get {
                return this.calleField;
            }
            set {
                this.calleField = value;
            }
        }
        
        /// <comentarios/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string NumeroExterior {
            get {
                return this.numeroExteriorField;
            }
            set {
                this.numeroExteriorField = value;
            }
        }
        
        /// <comentarios/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string NumeroInterior {
            get {
                return this.numeroInteriorField;
            }
            set {
                this.numeroInteriorField = value;
            }
        }
        
        /// <comentarios/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string Colonia {
            get {
                return this.coloniaField;
            }
            set {
                this.coloniaField = value;
            }
        }
        
        /// <comentarios/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string Localidad {
            get {
                return this.localidadField;
            }
            set {
                this.localidadField = value;
            }
        }
        
        /// <comentarios/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string Referencia {
            get {
                return this.referenciaField;
            }
            set {
                this.referenciaField = value;
            }
        }
        
        /// <comentarios/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string Municipio {
            get {
                return this.municipioField;
            }
            set {
                this.municipioField = value;
            }
        }
        
        /// <comentarios/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string Estado {
            get {
                return this.estadoField;
            }
            set {
                this.estadoField = value;
            }
        }
        
        /// <comentarios/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string Pais {
            get {
                return this.paisField;
            }
            set {
                this.paisField = value;
            }
        }
        
        /// <comentarios/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string CodigoPostal {
            get {
                return this.codigoPostalField;
            }
            set {
                this.codigoPostalField = value;
            }
        }
    }
    
    /// <comentarios/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.33440")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType=true, Namespace="http://www.sat.gob.mx/ComercioExterior11")]
    public partial class Destinatario {
        
        private List<Domicilio> domicilioField;
        
        private string numRegIdTribField;
        
        private string nombreField;
        
        /// <comentarios/>
        [System.Xml.Serialization.XmlElementAttribute("Domicilio")]
        public List<Domicilio> Domicilio
        {
            get {
                return this.domicilioField;
            }
            set {
                this.domicilioField = value;
            }
        }
        
        /// <comentarios/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string NumRegIdTrib {
            get {
                return this.numRegIdTribField;
            }
            set {
                this.numRegIdTribField = value;
            }
        }
        
        /// <comentarios/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string Nombre {
            get {
                return this.nombreField;
            }
            set {
                this.nombreField = value;
            }
        }
    }
    
    /// <comentarios/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.33440")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType=true, Namespace="http://www.sat.gob.mx/ComercioExterior11")]
    internal partial class ComercioExteriorDestinatarioDomicilio
    {
        
        private string calleField;
        
        private string numeroExteriorField;
        
        private string numeroInteriorField;
        
        private string coloniaField;
        
        private string localidadField;
        
        private string referenciaField;
        
        private string municipioField;
        
        private string estadoField;
        
        private string paisField;
        
        private string codigoPostalField;
        
        /// <comentarios/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string Calle {
            get {
                return this.calleField;
            }
            set {
                this.calleField = value;
            }
        }
        
        /// <comentarios/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string NumeroExterior {
            get {
                return this.numeroExteriorField;
            }
            set {
                this.numeroExteriorField = value;
            }
        }
        
        /// <comentarios/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string NumeroInterior {
            get {
                return this.numeroInteriorField;
            }
            set {
                this.numeroInteriorField = value;
            }
        }
        
        /// <comentarios/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string Colonia {
            get {
                return this.coloniaField;
            }
            set {
                this.coloniaField = value;
            }
        }
        
        /// <comentarios/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string Localidad {
            get {
                return this.localidadField;
            }
            set {
                this.localidadField = value;
            }
        }
        
        /// <comentarios/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string Referencia {
            get {
                return this.referenciaField;
            }
            set {
                this.referenciaField = value;
            }
        }
        
        /// <comentarios/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string Municipio {
            get {
                return this.municipioField;
            }
            set {
                this.municipioField = value;
            }
        }
        
        /// <comentarios/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string Estado {
            get {
                return this.estadoField;
            }
            set {
                this.estadoField = value;
            }
        }
        
        /// <comentarios/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string Pais {
            get {
                return this.paisField;
            }
            set {
                this.paisField = value;
            }
        }
        
        /// <comentarios/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string CodigoPostal {
            get {
                return this.codigoPostalField;
            }
            set {
                this.codigoPostalField = value;
            }
        }
    }
    
    /// <comentarios/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.33440")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType=true, Namespace="http://www.sat.gob.mx/ComercioExterior11")]
    public partial class Mercancia {
        
        private List<DescripcionesEspecificas> descripcionesEspecificasField;
        
        private string noIdentificacionField;
        
        private string fraccionArancelariaField;
        
        private decimal cantidadAduanaField;
        
        private bool cantidadAduanaFieldSpecified;
        
        private string unidadAduanaField;
        
        private decimal valorUnitarioAduanaField;
        
        private bool valorUnitarioAduanaFieldSpecified;
        
        private decimal valorDolaresField;
        
        /// <comentarios/>
        [System.Xml.Serialization.XmlElementAttribute("DescripcionesEspecificas")]
        public List<DescripcionesEspecificas> DescripcionesEspecificas {
            get {
                return this.descripcionesEspecificasField;
            }
            set {
                this.descripcionesEspecificasField = value;
            }
        }
        
        /// <comentarios/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string NoIdentificacion {
            get {
                return this.noIdentificacionField;
            }
            set {
                this.noIdentificacionField = value;
            }
        }
        
        /// <comentarios/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string FraccionArancelaria {
            get {
                return this.fraccionArancelariaField;
            }
            set {
                this.fraccionArancelariaField = value;
            }
        }
        
        /// <comentarios/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public decimal CantidadAduana {
            get {
                return this.cantidadAduanaField;
            }
            set {
                this.cantidadAduanaField = value;
                this.cantidadAduanaFieldSpecified = (value > 0);
            }
        }
        
        /// <comentarios/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool CantidadAduanaSpecified {
            get {
                return this.cantidadAduanaFieldSpecified;
            }
            set {
                this.cantidadAduanaFieldSpecified = value;
            }
        }
        
        /// <comentarios/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string UnidadAduana {
            get {
                return this.unidadAduanaField;
            }
            set {
                this.unidadAduanaField = value;
            }
        }
        
        /// <comentarios/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public decimal ValorUnitarioAduana {
            get {
                return this.valorUnitarioAduanaField;
            }
            set {
                this.valorUnitarioAduanaField = value;
                this.valorUnitarioAduanaFieldSpecified = (value > 0);
            }
        }
        
        /// <comentarios/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool ValorUnitarioAduanaSpecified {
            get {
                return this.valorUnitarioAduanaFieldSpecified;
            }
            set {
                this.valorUnitarioAduanaFieldSpecified = value;
            }
        }
        
        /// <comentarios/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public decimal ValorDolares {
            get {
                return this.valorDolaresField;
            }
            set {
                this.valorDolaresField = value;
            }
        }
    }
    
    /// <comentarios/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.33440")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType=true, Namespace="http://www.sat.gob.mx/ComercioExterior11")]
    public partial class DescripcionesEspecificas {
        
        private string marcaField;
        
        private string modeloField;
        
        private string subModeloField;
        
        private string numeroSerieField;
        
        /// <comentarios/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string Marca {
            get {
                return this.marcaField;
            }
            set {
                this.marcaField = value;
            }
        }
        
        /// <comentarios/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string Modelo {
            get {
                return this.modeloField;
            }
            set {
                this.modeloField = value;
            }
        }
        
        /// <comentarios/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string SubModelo {
            get {
                return this.subModeloField;
            }
            set {
                this.subModeloField = value;
            }
        }
        
        /// <comentarios/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string NumeroSerie {
            get {
                return this.numeroSerieField;
            }
            set {
                this.numeroSerieField = value;
            }
        }
    }
}
