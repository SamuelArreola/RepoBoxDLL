using System;
using System.Xml.Serialization;
using System.Collections.Generic;

namespace RepoBox.Addendas
{
    /// <comentarios/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.33440")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    [System.Xml.Serialization.XmlRootAttribute(Namespace = "", IsNullable = false)]
    public partial class Pilgrims
    {

        private string proveedorField;

        private string compradorField;

        private string procesoField;

        private List<Partida> partidaField;

        /// <comentarios/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified, DataType = "nonNegativeInteger")]
        public string Proveedor
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

        /// <comentarios/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string Comprador
        {
            get
            {
                return this.compradorField;
            }
            set
            {
                this.compradorField = value;
            }
        }

        /// <comentarios/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string Proceso
        {
            get
            {
                return this.procesoField;
            }
            set
            {
                this.procesoField = value;
            }
        }

        /// <comentarios/>
        [System.Xml.Serialization.XmlElementAttribute("Partida", Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public List<Partida> Partida
        {
            get
            {
                return this.partidaField;
            }
            set
            {
                this.partidaField = value;
            }
        }
    }

    /// <comentarios/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.33440")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class Partida
    {

        private string materialField;

        private decimal cantidadField;

        private bool cantidadFieldSpecified;

        private string udeMField;

        private decimal precioField;

        private bool precioFieldSpecified;

        private string entradaField;

        private string referenciaField;

        private string pedimentoField;

        private string facturaPedimentoField;

        private Boletas boletasField;

        private string pedidoField;

        private string posicionField;

        /// <comentarios/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string Material
        {
            get
            {
                return this.materialField;
            }
            set
            {
                this.materialField = value;
            }
        }

        /// <comentarios/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public decimal Cantidad
        {
            get
            {
                return this.cantidadField;
            }
            set
            {
                this.cantidadField = value;
                this.cantidadFieldSpecified = (value > 0);
            }
        }

        /// <comentarios/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool CantidadSpecified
        {
            get
            {
                return this.cantidadFieldSpecified;
            }
            set
            {
                this.cantidadFieldSpecified = value;
            }
        }

        /// <comentarios/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string UdeM
        {
            get
            {
                return this.udeMField;
            }
            set
            {
                this.udeMField = value;
            }
        }

        /// <comentarios/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public decimal Precio
        {
            get
            {
                return this.precioField;
            }
            set
            {
                this.precioField = value;
                this.precioFieldSpecified = (value > 0);
            }
        }

        /// <comentarios/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool PrecioSpecified
        {
            get
            {
                return this.precioFieldSpecified;
            }
            set
            {
                this.precioFieldSpecified = value;
            }
        }

        /// <comentarios/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified, DataType = "nonNegativeInteger")]
        public string Entrada
        {
            get
            {
                return this.entradaField;
            }
            set
            {
                this.entradaField = value;
            }
        }

        /// <comentarios/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string Referencia
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

        /// <comentarios/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string Pedimento
        {
            get
            {
                return this.pedimentoField;
            }
            set
            {
                this.pedimentoField = value;
            }
        }

        /// <comentarios/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string FacturaPedimento
        {
            get
            {
                return this.facturaPedimentoField;
            }
            set
            {
                this.facturaPedimentoField = value;
            }
        }

        /// <comentarios/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public Boletas Boletas
        {
            get
            {
                return this.boletasField;
            }
            set
            {
                this.boletasField = value;
            }
        }

        /// <comentarios/>
        [System.Xml.Serialization.XmlAttributeAttribute(DataType = "nonNegativeInteger")]
        public string Pedido
        {
            get
            {
                return this.pedidoField;
            }
            set
            {
                this.pedidoField = value;
            }
        }

        /// <comentarios/>
        [System.Xml.Serialization.XmlAttributeAttribute(DataType = "nonNegativeInteger")]
        public string Posicion
        {
            get
            {
                return this.posicionField;
            }
            set
            {
                this.posicionField = value;
            }
        }
    }

    /// <comentarios/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.33440")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class Boletas
    {

        private List<string> boletaField;

        private List<string> textField;

        /// <comentarios/>
        [System.Xml.Serialization.XmlElementAttribute("Boleta", Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public List<string> Boleta
        {
            get
            {
                return this.boletaField;
            }
            set
            {
                this.boletaField = value;
            }
        }

        /// <comentarios/>
        [System.Xml.Serialization.XmlTextAttribute()]
        public List<string> Text
        {
            get
            {
                return this.textField;
            }
            set
            {
                this.textField = value;
            }
        }
    }
}
