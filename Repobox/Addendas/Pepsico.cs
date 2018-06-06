using System.Xml.Serialization;
using System.Xml;
using System.IO;
using System.Collections.Generic;
namespace RepoBox.Addendas.PEPSICO
{
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.33440")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.fact.com.mx/schema/pepsico")]
    [System.Xml.Serialization.XmlRootAttribute(Namespace = "http://www.fact.com.mx/schema/pepsico", IsNullable = false)]
    public partial class RequestCFD
    {
        public string Create()
        {
            XmlDocument document = new XmlDocument();
            System.Xml.Serialization.XmlSerializer x = new System.Xml.Serialization.XmlSerializer(this.GetType());
            using (MemoryStream stream = new MemoryStream())
            {
                x.Serialize(stream, this);
                stream.Position = 0L;
                StreamReader reader = new StreamReader(stream);
                string xml = reader.ReadToEnd();
                reader.Close();
                document.LoadXml(xml);
            }
            return document.InnerXml;
        }
        private RequestCFDDocumento documentoField;
        private RequestCFDProveedor proveedorField;
        private List<RequestCFDRecepcion> recepcionesField;
        private string tipoField;
        private string versionField;
        private string idPedidoField;
        private string idSolicitudPagoField;
        
        public RequestCFD()
        {
            this.tipoField = "AddendaPCO";
            this.versionField = "2.0";
        }

        /// <remarks/>
        public RequestCFDDocumento Documento
        {
            get
            {
                return this.documentoField;
            }
            set
            {
                this.documentoField = value;
            }
        }

        /// <remarks/>
        public RequestCFDProveedor Proveedor
        {
            get
            {
                return this.proveedorField;
            }
            set
            {
                this.proveedorField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlArrayItemAttribute("Recepcion", IsNullable = false)]
        public List<RequestCFDRecepcion> Recepciones
        {
            get
            {
                return this.recepcionesField;
            }
            set
            {
                this.recepcionesField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string tipo
        {
            get
            {
                return this.tipoField;
            }
            set
            {
                this.tipoField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string version
        {
            get
            {
                return this.versionField;
            }
            set
            {
                this.versionField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string idPedido
        {
            get
            {
                return this.idPedidoField;
            }
            set
            {
                this.idPedidoField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string idSolicitudPago
        {
            get
            {
                return this.idSolicitudPagoField;
            }
            set
            {
                this.idSolicitudPagoField = value;
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.33440")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.fact.com.mx/schema/pepsico")]
    public partial class RequestCFDDocumento
    {
        private string referenciaField;
        private string serieField;
        private string folioField;
        private string folioUUIDField;
        private RequestCFDDocumentoTipoDoc tipoDocField;

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string referencia
        {
            get
            {
                return this.referenciaField;
            }
            set
            {
                this.referenciaField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string serie
        {
            get
            {
                return this.serieField;
            }
            set
            {
                this.serieField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string folio
        {
            get
            {
                return this.folioField;
            }
            set
            {
                this.folioField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string folioUUID
        {
            get
            {
                return this.folioUUIDField;
            }
            set
            {
                this.folioUUIDField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public RequestCFDDocumentoTipoDoc tipoDoc
        {
            get
            {
                return this.tipoDocField;
            }
            set
            {
                this.tipoDocField = value;
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.33440")]
    [System.SerializableAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.fact.com.mx/schema/pepsico")]
    public enum RequestCFDDocumentoTipoDoc
    {

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("1")]
        Factura,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("2")]
        NotaCredito,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("3")]
        NotaCargoDebito,
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.33440")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.fact.com.mx/schema/pepsico")]
    public partial class RequestCFDProveedor
    {
        private string idProveedorField;
        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string idProveedor
        {
            get
            {
                return this.idProveedorField;
            }
            set
            {
                this.idProveedorField = value;
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.33440")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.fact.com.mx/schema/pepsico")]
    public partial class RequestCFDRecepcion
    {
        private List<RequestCFDRecepcionConcepto> conceptoField;
        private string idRecepcionField;

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("Concepto")]
        public List<RequestCFDRecepcionConcepto> Concepto
        {
            get
            {
                return this.conceptoField;
            }
            set
            {
                this.conceptoField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string idRecepcion
        {
            get
            {
                return this.idRecepcionField;
            }
            set
            {
                this.idRecepcionField = value;
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.33440")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.fact.com.mx/schema/pepsico")]
    public partial class RequestCFDRecepcionConcepto
    {
        private decimal cantidadField;
        private string unidadField;
        private string descripcionField;
        private decimal valorUnitarioField;
        private decimal importeField;

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public decimal cantidad
        {
            get
            {
                return this.cantidadField;
            }
            set
            {
                this.cantidadField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string unidad
        {
            get
            {
                return this.unidadField;
            }
            set
            {
                this.unidadField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string descripcion
        {
            get
            {
                return this.descripcionField;
            }
            set
            {
                this.descripcionField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public decimal valorUnitario
        {
            get
            {
                return this.valorUnitarioField;
            }
            set
            {
                this.valorUnitarioField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public decimal importe
        {
            get
            {
                return this.importeField;
            }
            set
            {
                this.importeField = value;
            }
        }
    }
}
