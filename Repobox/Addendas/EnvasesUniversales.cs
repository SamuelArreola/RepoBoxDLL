using System;
using System.Xml;
using System.Xml.Serialization;
using System.Collections.Generic;
using System.IO;

namespace RepoBox.Addendas.EnvasesUniversales
{
    /// <summary></summary>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.33440")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://factura.envasesuniversales.com/addenda/eu")]
    [System.Xml.Serialization.XmlRootAttribute(Namespace = "http://factura.envasesuniversales.com/addenda/eu", IsNullable = false)]
    public partial class AddendaEU
    {
        [XmlNamespaceDeclarationsAttribute]
        public XmlSerializerNamespaces myNamespaces = new XmlSerializerNamespaces();
        [XmlAttribute("schemaLocation", Namespace = "http://www.w3.org/2001/XMLSchema-instance")]
        public string SchemaLocation = "http://factura.envasesuniversales.com/addenda/eu http://factura.envasesuniversales.com/addenda/eu/EU_Addenda.xsd";

        public AddendaEU()
        {
            this.myNamespaces.Add("eu", "http://factura.envasesuniversales.com/addenda/eu");
        }

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
        private TipoFactura tipoFacturaField;
        private TipoTransaccion tipoTransaccionField;
        private List<Secuencia> ordenesCompraField;
        private Moneda monedaField;
        private ImpuestosR impuestosRField;

        /// <summary></summary>
        public TipoFactura TipoFactura
        {
            get
            {
                return this.tipoFacturaField;
            }
            set
            {
                this.tipoFacturaField = value;
            }
        }

        /// <summary></summary>
        public TipoTransaccion TipoTransaccion
        {
            get
            {
                return this.tipoTransaccionField;
            }
            set
            {
                this.tipoTransaccionField = value;
            }
        }

        /// <summary></summary>
        [System.Xml.Serialization.XmlArrayAttribute(IsNullable = true)]
        [System.Xml.Serialization.XmlArrayItemAttribute("Secuencia", IsNullable = false)]
        public List<Secuencia> OrdenesCompra
        {
            get
            {
                return this.ordenesCompraField;
            }
            set
            {
                this.ordenesCompraField = value;
            }
        }

        /// <summary></summary>
        [System.Xml.Serialization.XmlElementAttribute(IsNullable = true)]
        public Moneda Moneda
        {
            get
            {
                return this.monedaField;
            }
            set
            {
                this.monedaField = value;
            }
        }

        /// <summary></summary>
        [System.Xml.Serialization.XmlElementAttribute(IsNullable = true)]
        public ImpuestosR ImpuestosR
        {
            get
            {
                return this.impuestosRField;
            }
            set
            {
                this.impuestosRField = value;
            }
        }
    }

    /// <summary></summary>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.33440")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://factura.envasesuniversales.com/addenda/eu")]
    public partial class TipoFactura
    {
        private IdFactura idFacturaField;
        private string versionField;
        private System.DateTime fechaMensajeField;

        public TipoFactura()
        {
            this.versionField = "1.0";
        }

        /// <summary></summary>
        public IdFactura IdFactura
        {
            get
            {
                return this.idFacturaField;
            }
            set
            {
                this.idFacturaField = value;
            }
        }

        /// <summary></summary>
        public string Version
        {
            get
            {
                return this.versionField;
            }
            set { this.versionField = value; }
        }

        /// <summary></summary>
        [System.Xml.Serialization.XmlElementAttribute(DataType = "date")]
        public System.DateTime FechaMensaje
        {
            get
            {
                return this.fechaMensajeField;
            }
            set
            {
                this.fechaMensajeField = value;
            }
        }
    }

    /// <summary></summary>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.33440")]
    [System.SerializableAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://factura.envasesuniversales.com/addenda/eu")]
    public enum IdFactura
    {
        /// <summary>Factura</summary>
        Factura,

        /// <summary>Nota de Crédito</summary>
        Nota_Credito,

        /// <summary>Recibo de Honorarios</summary>
        Recibo_Honorarios,

        /// <summary>Carta Porte o Documento de Traslado</summary>
        Carta_Porte,
    }

    /// <summary></summary>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.33440")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://factura.envasesuniversales.com/addenda/eu")]
    public partial class TipoTransaccion
    {
        private IdTransaccion idTransaccionField;
        private string transaccionField;

        /// <summary></summary>
        public IdTransaccion IdTransaccion
        {
            get
            {
                return this.idTransaccionField;
            }
            set
            {
                this.idTransaccionField = value;
            }
        }

        /// <summary></summary>
        public string Transaccion
        {
            get
            {
                return this.transaccionField;
            }
            set
            {
                this.transaccionField = value;
            }
        }
    }

    /// <summary></summary>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.33440")]
    [System.SerializableAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://factura.envasesuniversales.com/addenda/eu")]
    public enum IdTransaccion
    {
        /// <summary>Con Pedido Realizado.</summary>
        Con_Pedido,

        /// <summary>Carta Porte o Documento de Traslado</summary>
        Carta_Porte,

        /// <summary>A Consignación</summary>
        Consignacion,

        /// <summary>Sin Pedido.</summary>
        Sin_Pedido,

        /// <summary>Nota de Crédito</summary>
        Nota_Credito,

        /// <summary>Anticipo</summary>
        Anticipo,

        /// <summary>Aduana</summary>
        Aduana,

        /// <summary>General</summary>
        General,
    }

    /// <summary></summary>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.33440")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://factura.envasesuniversales.com/addenda/eu")]
    public partial class Secuencia
    {
        private string idPedidoField;
        private List<string> entradaAlmacenField;
        private int consecField;
        private bool consecFieldSpecified;

        /// <summary></summary>
        public string IdPedido
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

        /// <summary></summary>
        [System.Xml.Serialization.XmlArrayItemAttribute("Albaran", IsNullable = false)]
        public List<string> EntradaAlmacen
        {
            get
            {
                return this.entradaAlmacenField;
            }
            set
            {
                this.entradaAlmacenField = value;
            }
        }

        /// <summary></summary>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public int consec
        {
            get
            {
                return this.consecField;
            }
            set
            {
                this.consecField = value;
                this.consecSpecified = true;
            }
        }

        /// <summary></summary>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool consecSpecified
        {
            get
            {
                return this.consecFieldSpecified;
            }
            set
            {
                this.consecFieldSpecified = value;
            }
        }
    }

    /// <summary></summary>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.33440")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://factura.envasesuniversales.com/addenda/eu")]
    public partial class Moneda
    {
        private MonedaCve monedaCveField;
        private decimal tipoCambioField;
        private bool tipoCambioFieldSpecified;
        private decimal subtotalMField;
        private bool subtotalMFieldSpecified;
        private decimal totalMField;
        private bool totalMFieldSpecified;
        private decimal impuestoMField;
        private bool impuestoMFieldSpecified;

        /// <summary></summary>
        public MonedaCve MonedaCve
        {
            get
            {
                return this.monedaCveField;
            }
            set
            {
                this.monedaCveField = value;
            }
        }

        /// <summary></summary>
        public decimal TipoCambio
        {
            get
            {
                return this.tipoCambioField;
            }
            set
            {
                this.tipoCambioField = value;
                this.TipoCambioSpecified = true;
            }
        }

        /// <summary></summary>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool TipoCambioSpecified
        {
            get
            {
                return this.tipoCambioFieldSpecified;
            }
            set
            {
                this.tipoCambioFieldSpecified = value;
            }
        }

        /// <summary></summary>
        public decimal SubtotalM
        {
            get
            {
                return this.subtotalMField;
            }
            set
            {
                this.subtotalMField = value;
                this.SubtotalMSpecified = (subtotalMField > 0);
            }
        }

        /// <summary></summary>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool SubtotalMSpecified
        {
            get
            {
                return this.subtotalMFieldSpecified;
            }
            set
            {
                this.subtotalMFieldSpecified = value;
            }
        }

        /// <summary></summary>
        public decimal TotalM
        {
            get
            {
                return this.totalMField;
            }
            set
            {
                this.totalMField = value;
                this.TotalMSpecified = (this.totalMField > 0);
            }
        }

        /// <summary></summary>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool TotalMSpecified
        {
            get
            {
                return this.totalMFieldSpecified;
            }
            set
            {
                this.totalMFieldSpecified = value;
            }
        }

        /// <summary></summary>
        public decimal ImpuestoM
        {
            get
            {
                return this.impuestoMField;
            }
            set
            {
                this.impuestoMField = value;
                this.ImpuestoMSpecified = true;
            }
        }

        /// <summary></summary>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool ImpuestoMSpecified
        {
            get
            {
                return this.impuestoMFieldSpecified;
            }
            set
            {
                this.impuestoMFieldSpecified = value;
            }
        }
    }

    /// <summary></summary>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.33440")]
    [System.SerializableAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://factura.envasesuniversales.com/addenda/eu")]
    public enum MonedaCve
    {
        /// <summary>USD - Dolares Americanos.</summary>
        USD,
        /// <summary>EUR - Euros.</summary>
        EUR,
        /// <summary>MXN - Pesos Mexicanos</summary>
        MXN,
    }

    /// <summary></summary>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.33440")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://factura.envasesuniversales.com/addenda/eu")]
    public partial class ImpuestosR
    {
        private decimal baseImpuestoField;
        /// <summary></summary>
        public decimal BaseImpuesto
        {
            get
            {
                return this.baseImpuestoField;
            }
            set
            {
                this.baseImpuestoField = value;
            }
        }
    }
}