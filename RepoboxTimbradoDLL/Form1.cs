using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Xml;
using RepoBox33.SATCatalogos;
using RepoBox33;

namespace RepoboxTimbradoDLL
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        private void btnXSD32_Click(object sender, EventArgs e)
        {
            DateTime inicio = DateTime.Now;
            bool SAT = false;
            // Los parametros necesarios son 1. Ruta del .Cer, 2. Ruta del .Key, 3. Contraseña del archivo .Key
            //RepoBox.Comprobante factura = new RepoBox.Comprobante(@"C:\RepoBox\Timbrado\DEMO\CSD01_AAA010101AAA.cer", @"C:\RepoBox\Timbrado\DEMO\CSD01_AAA010101AAA.key", "12345678a");
            RepoBox.Comprobante factura = new RepoBox.Comprobante(@"C:\RepoBox\NominaElectronica\Config\00001000000302316116.cer", @"C:\RepoBox\NominaElectronica\Config\CSD_matriz_SFI950101DU2_20140109_115049.key", "cristobal2012");
            factura.version = "3.2";
            factura.serie = "H";
            factura.folio = DateTime.Now.Hour.ToString("00") + DateTime.Now.Minute.ToString("00") + DateTime.Now.Second.ToString("00");
            factura.Fecha = DateTime.Now;
            factura.Moneda = "MXN";
            factura.TipoCambio = "1";
            factura.formaDePago = "sin identificar";
            factura.condicionesDePago = "UNA EXHIBICIÓN";
            factura.subTotal = 100;
            factura.descuento = 0;
            factura.total = 116;
            factura.metodoDePago = "No identificado";
            factura.tipoDeComprobante = RepoBox.TipoDeComprobante.ingreso;
            factura.LugarExpedicion = "Obregon, Sonora";
            //La forma de asignar valores a los objetos queda a su criterio.
            factura.Emisor = new RepoBox.Emisor()
            {
                //rfc = (SAT ? "SFI950101DU2" : "AAA010101AAA" ), FINKOK
                rfc = "SFI950101DU2",
                nombre = "SECRETARIA DE FINANZAS",
                RegimenFiscal = new List<RepoBox.RegimenFiscal>(),

                DomicilioFiscal = new RepoBox.DomicilioFiscal()
                {
                    calle = "CALLE DE HERMOSILLO",
                    noExterior = "S/N",
                    noInterior = "XXXX",
                    referencia = "FALTA EL AGUA",
                    colonia = "CENTRO",
                    localidad = "HERMOSILLO",
                    municipio = "HERMOSILLO",
                    estado = "SONORA",
                    pais = "MEXICO",
                    codigoPostal = "83000"
                },
                ExpedidoEn = new RepoBox.ExpedidoEn()
                {
                    calle = "CALLE DE EJEMPLO",
                    noExterior = "S/N",
                    noInterior = "",
                    referencia = "ESQUINA CON NADA",
                    colonia = "CENTRO",
                    localidad = "SAN CARLOS",
                    municipio = "GUAYMAS",
                    estado = "SONORA",
                    pais = "MEXICO",
                    codigoPostal = "84500"
                }
            };
            // Asignacion de Regimen(es) fiscal(es)
            factura.Emisor.RegimenFiscal.Add(new RepoBox.RegimenFiscal() { Regimen = "REGIMEN GENERAL DE LAS PERSONAS MORALES" });
            // Al igual que el Emisor, puede asignar valores del Receptor a su criterio.
            factura.Receptor = new RepoBox.Receptor() { rfc = "AERS900616U25", nombre = "SAMUEL ARMANDO ARREOLA ROMERO", Domicilio = new RepoBox.Domicilio() { pais = "Mexico" } };
            // Los conceptos (productos y/o servicios) se agregna de la siguiente forma o de igual manera queda a su criterio.
            factura.Conceptos = new List<RepoBox.Concepto>();
            factura.Conceptos.Add(new RepoBox.Concepto() { cantidad = 1, descripcion = "Producto 1", importe = 10, noIdentificacion = "C1", unidad = "PZA", valorUnitario = 10 });
            factura.Conceptos.Add(new RepoBox.Concepto() { cantidad = 1, descripcion = "Producto 2", importe = 10, noIdentificacion = "C2", unidad = "PZA", valorUnitario = 10 });
            factura.Conceptos.Add(new RepoBox.Concepto() { cantidad = 1, descripcion = "Producto 3", importe = 10, noIdentificacion = "C3", unidad = "PZA", valorUnitario = 10 });
            // La declaración del nodo impuestos es obligatoria, 
            factura.Impuestos = new RepoBox.Impuestos();
            factura.Impuestos.Traslados = new List<RepoBox.Traslado>();
            factura.Impuestos.Traslados.Add(new RepoBox.Traslado() { importe = 1, impuesto = RepoBox.TrasladoImpuesto.IEPS, tasa = 3 });
            factura.Impuestos.Traslados.Add(new RepoBox.Traslado() { importe = 1, impuesto = RepoBox.TrasladoImpuesto.IVA, tasa = 16 });
            factura.Impuestos.Retenciones = new List<RepoBox.Retencion>();
            factura.Impuestos.Retenciones.Add(new RepoBox.Retencion() { importe = 1, impuesto = RepoBox.RetencionImpuesto.ISR });
            factura.Impuestos.Retenciones.Add(new RepoBox.Retencion() { importe = 1, impuesto = RepoBox.RetencionImpuesto.IVA });
            // La utilización de este metodo tambien es obligatorio, asi nos evitamos realizar la sumatoria manual de los impuestos
            factura.Impuestos.CalcularTotales();
            // De cualquier forma, si desean asignar de forma manual los valores de total de impuestos pueden hacerlo con las siguientes lineas.
            //factura.Impuestos.totalImpuestosRetenidos = 0;
            //factura.Impuestos.totalImpuestosTrasladados = 0;
            factura.Banco = "Banorte 20202020202";
            factura.Observaciones = "Ahora si hay observaciones";

            // Donatarias
            //<donat:Donatarias version="1.1" leyenda="ESTE DONATIVO ES DEDUCIBLE EN TÉRMINOS DE LA LEY DEL ISR." fechaAutorizacion="2015-10-23" noAutorizacion="Gobierno"/>
            //XmlDocument docDonatarias = new XmlDocument();
            //docDonatarias.LoadXml("<donat:Donatarias xmlns:donat=\"Donatarias\" version=\"1.1\" leyenda=\"ESTE DONATIVO ES DEDUCIBLE EN TERMINOS DE LA LEY DEL ISR.\" fechaAutorizacion=\"2015-10-23\" noAutorizacion=\"Gobierno\" />");
            //if (factura.Complemento == null)
            //    factura.Complemento = new RepoBox.ComprobanteComplemento();
            //factura.Complemento.Any = new XmlElement[1];
            //factura.Complemento.Any[0] = docDonatarias.DocumentElement;
            // Lo siguiente es generar el archivo CFD (xml sin timbrar)
            string cfd = factura.GenerarCFD();
            // Si la respuesta del método 
            if (!cfd.StartsWith("RepoBox"))
            {
                RepoBox.CFDIv32 cfdi = new RepoBox.CFDIv32();
                bool enviado = false;
                cfd = "<?xml version=\"1.0\"?><cfdi:Comprobante xmlns:xsi=\"http://www.w3.org/2001/XMLSchema-instance\" xmlns:xsd=\"http://www.w3.org/2001/XMLSchema\" xsi:schemaLocation=\"http://www.sat.gob.mx/cfd/3 http://www.sat.gob.mx/sitio_internet/cfd/3/cfdv32.xsd\" version=\"3.2\" serie=\"H\" folio=\"80\" fecha=\"2017-12-18T10:05:56\" sello=\"cHU/o5K8wvaQxR7yl5mHBQO130fIT3HZzjXKBi1ugMHTiHlfYfWacBjEr8uEvXoehfj9fZQTsuZXTwks8eSbN/JtKRTVMmVbq/Ofz4GJ2jHRj1FrBFGmFatIDy2+yyewQDhUXZLNOkKaXmdNyEM0NYxoUsdCPQixwnRK64SX2JrTojo6guSOZg906csh1c0a3cjhgEgeIdWv/+fOsOwHk3zFzOqJ8bG0k+aB3QoBPBlL93bDjRJEAS/frcn9JSURn2vn36WjqbNHOYf7QLyoTLkAWO7FyEVz6MiC+SDbTNFkfvy0GnnHuI3+yl42hihRUsryLqh2JyOfi06+71KG0Q==\" formaDePago=\"CONTADO\" noCertificado=\"00001000000403161018\" certificado=\"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\" condicionesDePago=\"UNA EXHIBICIÓN\" subTotal=\"378\" TipoCambio=\"20\" Moneda=\"MXN\" total=\"378\" tipoDeComprobante=\"ingreso\" metodoDePago=\"03-TRANSFERENCIA\" LugarExpedicion=\"CALLE 22 AV 13 NO 99  ESQ\" NumCtaPago=\"22112017\" xmlns:cfdi=\"http://www.sat.gob.mx/cfd/3\"><cfdi:Emisor rfc=\"CEA990831FI8\" nombre=\"CEA GUAYMAS\"><cfdi:DomicilioFiscal calle=\"CALLE 22 AV. 13\" noExterior=\"# 99 ESQ.\" colonia=\"CENTRO\" localidad=\"GUAYMAS\" municipio=\"GUAYMAS\" estado=\"SONORA\" pais=\"MEXICO\" codigoPostal=\"85400\" /><cfdi:ExpedidoEn calle=\"CALLE 22 AV. 13\" noExterior=\"# 99 ESQ.\" colonia=\"CENTRO\" localidad=\"GUAYMAS\" municipio=\"GUAYMAS\" estado=\"SONORA\" pais=\"MEXICO\" codigoPostal=\"85400\" /><cfdi:RegimenFiscal Regimen=\"PERSONA MORAL CON FINES NO LUCRATIVOS\" /></cfdi:Emisor><cfdi:Receptor rfc=\"CNM980114PI2\" nombre=\"AT&amp;T COMUNICACIONES\"><cfdi:Domicilio calle=\"RIO LERMA 232 PISO 20\" noExterior=\"232\" noInterior=\"1\" colonia=\"COL. CUAUHTEMOC CIUDAD\" localidad=\"CIUDAD DE MEXICO\" municipio=\"CIUDAD DE MEXICO\" estado=\"CIUDAD DE MEXICO\" pais=\"MEXICO\" codigoPostal=\"6500\" /></cfdi:Receptor><cfdi:Conceptos><cfdi:Concepto cantidad=\"1\" unidad=\"SERV.\" noIdentificacion=\"21\" descripcion=\"RECONEXIÓN\" valorUnitario=\"377.5\" importe=\"377.5\" /><cfdi:Concepto cantidad=\"1\" unidad=\"SERV.\" noIdentificacion=\"0\" descripcion=\"SALDO A FAVOR\" valorUnitario=\"0.5\" importe=\"0.5\" /></cfdi:Conceptos><cfdi:Impuestos><cfdi:Retenciones><cfdi:Retencion impuesto=\"IVA\" importe=\"0\" /></cfdi:Retenciones><cfdi:Traslados><cfdi:Traslado impuesto=\"IVA\" tasa=\"0.16\" importe=\"0\" /></cfdi:Traslados></cfdi:Impuestos></cfdi:Comprobante>";
                // Timbrado del CFD
                string timbrado = cfdi.Timbrar32(cfd, SAT, "PADE", factura.Emisor.rfc); // "timbrado" es la variable si requieren el xml en texto plano
                if (!timbrado.StartsWith("RepoBox"))
                {
                    string xmlPath = factura.SaveXmlSEFIN(timbrado); // "xmlPath" es la variable que regresa la ruta donde se guardó el XML, pero el metodo es para guardar el xml en archivo.
                    // Los parametros que acepta GenerarPDF 1. xml en texto plano (ya timbrado), 2. Ruta del logo a mostrar en la factura, 3. Ruta y nombre con el que se guardará el PDF.
                    string pdfPath = cfdi.GenerarPDF(timbrado, @"C:\Repobox\Timbrado\RepoBox.jpg", xmlPath.Replace(".XML", ""));
                    pdfPath = cfdi.GenerarPDF_PathFile(xmlPath, @"C:\Repobox\Timbrado\RepoBox.jpg");
                    // En caso de necesitar regenerar el archivo PDF, existe el siguiente método
                    // Parametros: 1. Ruta del archivo XML, 2. Ruta de logo a mostrar en la factura. El sistema nombrará al PDF igual que el XML.
                    //pdfPath = cfdi.GenerarPDF_PathFile(xmlPath, @"C:\Repobox\Timbrado\RepoBox.jpg");
                    // Metodo de envío del correo.
                    enviado = true;
                }
                //enviado = cfdi.EnviarEmail(xmlPath, pdfPath, new RepoBox.EmailParameters()
                //{
                //    Asunto = "Envio de Correo Prueba",
                //    Password = "hta.repobox",
                //    Correo = "samuel.arreola@repobox.com.mx",
                //    PuertoSMTP = 587,
                //    SSL = false,
                //    CuerpoMensaje = "Este es el cuerpo del mensaje",
                //    SMTP = "mail.repobox.com.mx",
                //    CuerpoHTML = false,
                //    // Puede incluir los correos que desee, solo necesita separarlos por ";"
                //    Destinatarios = "samuel_arreola@msn.com;samuel.arreola@repobox.com.mx"
                //});
                if (enviado)
                    MessageBox.Show(((TimeSpan)(DateTime.Now - inicio)).Seconds + ". Enviado");
                else
                    MessageBox.Show(((TimeSpan)(DateTime.Now - inicio)).Seconds + ". Mensaje: " + timbrado);
            }
            else
            {
                MessageBox.Show(((TimeSpan)(DateTime.Now - inicio)).Seconds + ". Mensaje: " + cfd);
            }
        }

        private void btnCancelar32_Click(object sender, EventArgs e)
        {
            bool acuseBase64 = false;
            RepoBox.CFDIv32 cfdi = new RepoBox.CFDIv32();
            string UUID = "E8A4A699-1D46-4700-A0EF-A07578306764";
            //string xml = cfdi.Cancelar32(false, "CEA990831FI8", "84473408-E750-463C-9493-372B000CC709", @"C:\Repobox\Timbrado\CEA990831FI8\00001000000403161018.cer", @"C:\Repobox\Timbrado\CEA990831FI8\CSD_Cananea_CEA990831FI8_20160719_124927.key", "cananea2016", acuseBase64);
            //string xml = cfdi.Cancelar32(true, "SFI950101DU2", UUID, @"C:\RepoBox\Timbrado\SFI950101DU2\00001000000302316116.cer", @"C:\RepoBox\Timbrado\SFI950101DU2\CSD_matriz_SFI950101DU2_20140109_115049.key", "cristobal2012", acuseBase64);
            string xml = cfdi.Cancelar32(true, "SFI950101DU2", UUID, @"C:\RepoBox\Timbrado\SFI950101DU2\00001000000408924535.cer", @"C:\RepoBox\Timbrado\SFI950101DU2\CSD_SECRETARIA_DE_FINANZAS_SFI950101DU2_20180111_130703.key", "Sad2018LO", acuseBase64);
            if (!xml.StartsWith("RepoBox:"))
            {
                if (acuseBase64)
                    xml = System.Text.ASCIIEncoding.ASCII.GetString(Convert.FromBase64String(xml));
                XmlDocument doc = new XmlDocument();
                doc.LoadXml(xml);
                doc.Save(@"C:\RepoBox\Timbrado\SFI950101DU2\CFDI\AcuseCancelacion_" + UUID + ".xml");
            }
            MessageBox.Show(this, xml, "respuesta", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void btnFolioSiguiente_Click(object sender, EventArgs e)
        {
            SEFINZacatecas.facturacion zac = new SEFINZacatecas.facturacion();
            SEFINZacatecas.tSiguienteFactura sig = zac.factura_siguiente();
            MessageBox.Show(this, "Folio: " + sig.folio + ". Folio Specified: " + sig.folioSpecified + ". Tipo: " + sig.tipo, "Mensajon", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void btn33_Click(object sender, EventArgs e)
        {
            #region Pruebas
            bool SAT = false;
            RepoBox33.SATCatalogos.SATCats satCats = new SATCats();
            satCats.CargarCatalogos();
            // Los parametros necesarios son 1. Ruta del .Cer, 2. Ruta del .Key, 3. Contraseña del archivo .Key
            //RepoBox33.Comprobante factura = new RepoBox33.Comprobante(@"C:\RepoBox\NominaElectronica\Config\00001000000302316116.cer", @"C:\RepoBox\NominaElectronica\Config\CSD_matriz_SFI950101DU2_20140109_115049.key", "cristobal2012");
            //RepoBox33.Comprobante factura = new RepoBox33.Comprobante(@"C:\Repobox\Timbrado\CEA990831FI8\00001000000403161018.cer", @"C:\Repobox\Timbrado\CEA990831FI8\CSD_Cananea_CEA990831FI8_20160719_124927.key", "cananea2016");
            RepoBox33.Comprobante factura = new RepoBox33.Comprobante(@"C:\RepoBox\Timbrado\DEMO\CSD01_AAA010101AAA.cer", @"C:\RepoBox\Timbrado\DEMO\CSD01_AAA010101AAA.key", "12345678a");
            factura.Version = "3.3";
            factura.Serie = "H";
            factura.Folio = "131355";// DateTime.Now.Hour.ToString("00") + DateTime.Now.Minute.ToString("00") + DateTime.Now.Second.ToString("00");
            factura._Fecha = DateTime.Now;
            factura.FormaPago = satCats.FormasDePago[0].ID; // Catalogo de formas de pago.
            factura.CondicionesDePago = "PUE";
            factura.SubTotal = 100001;
            factura.Descuento = 0;
            factura.Moneda = "MXN";
            factura.TipoCambio = 1;
            factura.Total = (decimal)100001.16;
            factura.TipoDeComprobante = "I"; // En caso de ser factura, siempre será I
            factura.MetodoPago = satCats.MetodosPago[0].ID; // tres opciones PUE (pago en una sola exhibición), PIP (Pago inicial y parcialidades) PPD (Pago en parcialidades o diferido)
            factura.LugarExpedicion = "85000";
            //La forma de asignar valores a los objetos queda a su criterio.
            factura.Emisor = new RepoBox33.Emisor()
            {
                Rfc = (SAT ? "CEA990831FI8" : "AAA010101AAA"),
                Nombre = "CEA - RAZON SOCIAL",
                RegimenFiscal = "603" // Clave del catalogo de SAT para Regimenes fiscales.
            };

            // Al igual que el Emisor, puede asignar valores del Receptor a su criterio.
            factura.Receptor = new RepoBox33.Receptor()
            {
                Rfc = "XAXX010101000",
                Nombre = "RAZON & SOCIAL",
                UsoCFDI = "P01" // Clave del Cataloo de SAT para usos de CFDI.
            };
            // Los conceptos (productos y/o servicios) se agregan de la siguiente forma o de igual manera queda a su criterio.
            factura.Conceptos = new List<RepoBox33.Concepto>();
            RepoBox33.Concepto concepto = new RepoBox33.Concepto()
            {
                Cantidad = 1,
                Descripcion = "Producto 1",
                Importe = (decimal)100000.00,
                NoIdentificacion = "C1",
                Unidad = "PZA",
                ValorUnitario = 100000,
                ClaveProdServ = "24101602",
                ClaveUnidad = "E48",
                Descuento = 0
            };
            concepto.Impuestos = new RepoBox33.C_Impuestos();
            //concepto.Impuestos.Retenciones = new List<RepoBox33.CI_Retencion>();
            //concepto.Impuestos.Retenciones.Add(new RepoBox33.CI_Retencion() { Base = concepto.Importe, Impuesto = "002", TasaOCuota = (decimal)0.16, TipoFactor = "Tasa", Importe = concepto.Importe * (decimal)0.16 });
            concepto.Impuestos.Traslados = new List<RepoBox33.CI_Traslado>();
            //concepto.Impuestos.Traslados.Add(new RepoBox33.CI_Traslado() { Base = concepto.Importe, Impuesto = "002", TasaOCuota = (decimal)0.160000, TipoFactor = "Tasa", Importe = Convert.ToDecimal((concepto.Importe * (decimal)0.160000).ToString("N2")) });
            concepto.Impuestos.Traslados.Add(new RepoBox33.CI_Traslado() { Base = concepto.Importe, Impuesto = "002", TipoFactor = "Exento", ImporteSpecified = false, TasaOCuotaSpecified = false });
            //concepto.Impuestos.Retenciones = new List<CI_Retencion>();
            //concepto.Impuestos.Retenciones.Add(new RepoBox33.CI_Retencion() { Base = concepto.Importe, Impuesto = "002", TasaOCuota = (decimal)0.106666, TipoFactor = "Tasa", Importe = (decimal)10666.67 });//Convert.ToDecimal((concepto.Importe * (decimal)0.10666667).ToString("N2"))
            //concepto.Impuestos.Retenciones.Add(new RepoBox33.CI_Retencion() { Base = concepto.Importe, Impuesto = "001", TasaOCuota = (decimal)0.1000000, TipoFactor = "Tasa", Importe = Convert.ToDecimal((concepto.Importe * (decimal)0.100000).ToString("N2")) });
            factura.Conceptos.Add(concepto);


            RepoBox33.Concepto concepto2 = new RepoBox33.Concepto()
            {
                Cantidad = 1,
                Descripcion = "Producto 2",
                Importe = (decimal)1,
                NoIdentificacion = "C1",
                Unidad = "PZA",
                ValorUnitario = 1,
                ClaveProdServ = "24101602",
                ClaveUnidad = "E48",
                Descuento = 0
            };
            concepto2.Impuestos = new RepoBox33.C_Impuestos();
            //concepto.Impuestos.Retenciones = new List<RepoBox33.CI_Retencion>();
            //concepto.Impuestos.Retenciones.Add(new RepoBox33.CI_Retencion() { Base = concepto.Importe, Impuesto = "002", TasaOCuota = (decimal)0.16, TipoFactor = "Tasa", Importe = concepto.Importe * (decimal)0.16 });
            concepto2.Impuestos.Traslados = new List<RepoBox33.CI_Traslado>();
            concepto2.Impuestos.Traslados.Add(new RepoBox33.CI_Traslado() { Base = concepto2.Importe, Impuesto = "002", TasaOCuota = (decimal)0.160000, TipoFactor = "Tasa", Importe = Convert.ToDecimal((concepto2.Importe * (decimal)0.160000).ToString("N2")) });
            //concepto2.Impuestos.Traslados.Add(new RepoBox33.CI_Traslado() { Base = concepto.Importe, Impuesto = "002", TipoFactor = "Exento", ImporteSpecified = false, TasaOCuotaSpecified = false });
            //concepto.Impuestos.Retenciones = new List<CI_Retencion>();
            //concepto.Impuestos.Retenciones.Add(new RepoBox33.CI_Retencion() { Base = concepto.Importe, Impuesto = "002", TasaOCuota = (decimal)0.106666, TipoFactor = "Tasa", Importe = (decimal)10666.67 });//Convert.ToDecimal((concepto.Importe * (decimal)0.10666667).ToString("N2"))
            //concepto.Impuestos.Retenciones.Add(new RepoBox33.CI_Retencion() { Base = concepto.Importe, Impuesto = "001", TasaOCuota = (decimal)0.1000000, TipoFactor = "Tasa", Importe = Convert.ToDecimal((concepto.Importe * (decimal)0.100000).ToString("N2")) });
            factura.Conceptos.Add(concepto2);

            C_Impuestos listaImpuestos = factura.ListadoImpuestos();

            if (listaImpuestos != null)
            {
                if ((listaImpuestos.trasladoIEPS + listaImpuestos.trasladoIVA) > 0)
                {
                    factura.Impuestos = new RepoBox33.Impuestos();
                    factura.Impuestos.Traslados = new List<RepoBox33.Traslado>();
                    if (listaImpuestos.trasladoIVA > 0)
                        factura.Impuestos.Traslados.Add(new RepoBox33.Traslado() { Importe = listaImpuestos.trasladoIVA, Impuesto = "002", TasaOCuota = (decimal)0.160000, TipoFactor = "Tasa" });
                    if (listaImpuestos.trasladoIEPS > 0)
                        factura.Impuestos.Traslados.Add(new RepoBox33.Traslado() { Importe = listaImpuestos.trasladoIEPS, Impuesto = "003", TasaOCuota = (decimal)0.16, TipoFactor = "Tasa" });
                }
                if ((listaImpuestos.retencionISR + listaImpuestos.retencionIVA) > 0)
                {
                    if (factura.Impuestos != null)
                        factura.Impuestos = new RepoBox33.Impuestos();
                    factura.Impuestos.Retenciones = new List<RepoBox33.Retencion>();
                    if (listaImpuestos.retencionIVA > 0)
                        factura.Impuestos.Retenciones.Add(new RepoBox33.Retencion() { Importe = listaImpuestos.retencionIVA, Impuesto = "002" });
                    if (listaImpuestos.retencionISR > 0)
                        factura.Impuestos.Retenciones.Add(new RepoBox33.Retencion() { Importe = listaImpuestos.retencionISR, Impuesto = "001" });
                }
            }
            if(factura.Impuestos != null)
            // La utilización de este metodo tambien es obligatorio, asi nos evitamos realizar la sumatoria manual de los impuestos
                factura.Impuestos.CalcularTotales();
            // De cualquier forma, si desean asignar de forma manual los valores de total de listaImpuestos pueden hacerlo con las siguientes lineas.
            //factura.Impuestos.totalImpuestosRetenidos = 0;
            //factura.Impuestos.totalImpuestosTrasladados = 0;
            factura.Banco = "Banorte 20202020202";
            factura.Observaciones = "Ahora si hay observaciones";
            factura.Cuenta = "1010";
            // Addenda
            //factura.AddendaCEA = new AddendaCEA(); // IMPORTANTE, instanciar la propiedad AddendaCEA.
            //factura.AddendaCEA.NoServicio = "No. 292929292"; // Indicar aqui el no. de servicio
            //// Direccion del cliente (Receptor). // De igual forma importante instanciar la propiedad ReceptorDomicilio.
            factura.ReceptorDomicilio = new RepoBox.Domicilio()
            {
                calle = "Calle 1",
                noExterior = "no. 24",
                noInterior = "int. 22",
                referencia = "en la esquina",
                colonia = "centro",
                localidad = "ciudad obregon",
                municipio = "cajeme",
                estado = "sonora",
                pais = "mexico",
                codigoPostal = "85000"
            };

            // Addenda PEPSICO
            factura.PEPSICO = new RepoBox.Addendas.PEPSICO.RequestCFD();
            factura.PEPSICO.idPedido = "01010101";
            factura.PEPSICO.idSolicitudPago = "111"; // Opcional.
            factura.PEPSICO.Documento = new RepoBox.Addendas.PEPSICO.RequestCFDDocumento();
            factura.PEPSICO.Documento.tipoDoc = RepoBox.Addendas.PEPSICO.RequestCFDDocumentoTipoDoc.Factura; // 1 Factura, 2 Nota de Crédito, 3 Nota de Cargo-Debito.
            factura.PEPSICO.Documento.serie = factura.Serie;
            factura.PEPSICO.Documento.folio = factura.Folio;
            factura.PEPSICO.Documento.referencia = ""; // UUID de la factura, solo cuando sea Nota de Crédito o Nota de Cargo-Debito (tipo 2 o 3).
            //factura.PEPSICO.Documento.folioUUID = ""; // Este va comentado, dado que se registra al momento de generarse la Addenda, que es cuando ya está timbrado el CFDI.
            factura.PEPSICO.Proveedor = new RepoBox.Addendas.PEPSICO.RequestCFDProveedor();
            factura.PEPSICO.Proveedor.idProveedor = "000000"; // Id de Proveedor-Acreedor ante PEPSICO (es de la empresa emisora de la factura).
            factura.PEPSICO.Recepciones = new List<RepoBox.Addendas.PEPSICO.RequestCFDRecepcion>();
            RepoBox.Addendas.PEPSICO.RequestCFDRecepcion recepcion = new RepoBox.Addendas.PEPSICO.RequestCFDRecepcion();
            recepcion.idRecepcion = "01010101"; // No. de recepcion Obligatorio.
            recepcion.Concepto = new List<RepoBox.Addendas.PEPSICO.RequestCFDRecepcionConcepto>();
            foreach (Concepto conceptoList in factura.Conceptos)
                recepcion.Concepto.Add(new RepoBox.Addendas.PEPSICO.RequestCFDRecepcionConcepto()
                {
                    cantidad = conceptoList.Cantidad,
                    descripcion = conceptoList.Descripcion,
                    importe = conceptoList.Importe - conceptoList.Descuento,
                    unidad = conceptoList.Unidad,
                    valorUnitario = conceptoList.ValorUnitario
                });
            factura.PEPSICO.Recepciones.Add(recepcion);

            
            // Lo siguiente es generar el archivo CFD (xml sin timbrar)
            string cfd = factura.GenerarCFD();
            //XmlDocument dccc = new XmlDocument();
            //dccc.LoadXml(cfd);
            //factura.Addenda = new Addenda();
            //factura.Addenda.Any = new XmlElement[1] { factura.CreateAddenda(dccc) };
            //cfd = factura.GenerarCFD();

            // Si la respuesta del método es positiva pasa a timbrarlo
            if (!cfd.StartsWith("RepoBox"))
            {
                RepoBox.CFDIv32 cfdi = new RepoBox.CFDIv32();
                bool enviado = false;
                // Timbrado del CFD
                string timbrado = cfdi.Timbrar33(cfd, SAT, "FINKOK", factura.Emisor.Rfc); // "timbrado" es la variable si requieren el xml en texto plano
                if (!timbrado.ToUpper().StartsWith("REPOBOX"))
                {
                    string xmlPath = factura.SaveXml(ref timbrado); // "xmlPath" es la variable que regresa la ruta donde se guardó el XML, pero el metodo es para guardar el xml en archivo.
                    
                    // Los parametros que acepta GenerarPDF 1. xml en texto plano (ya timbrado), 2. Ruta del logo a mostrar en la factura, 3. Ruta y nombre con el que se guardará el PDF.
                    string pdfPath = cfdi.GenerarPDF33(timbrado, @"C:\Repobox\Timbrado\RepoBox.jpg", xmlPath.Replace(".XML", ""));
                    // En caso de necesitar regenerar el archivo PDF, existe el siguiente método
                    // Parametros: 1. Ruta del archivo XML, 2. Ruta de logo a mostrar en la factura. El sistema nombrará al PDF igual que el XML.
                    // pdfPath = cfdi.GenerarPDF_PathFile(xmlPath, @"C:\Repobox\Timbrado\RepoBox.jpg");
                    // Metodo de envío del correo.

                    enviado = cfdi.EnviarEmail(xmlPath, pdfPath, new RepoBox.EmailParameters()
                    {
                        Asunto = "Envio de Correo Prueba",
                        Password = "Password",
                        Correo = "direccion@email.com",
                        PuertoSMTP = 587,
                        SSL = false,
                        CuerpoMensaje = "Este es el cuerpo del mensaje",
                        SMTP = "mail.repobox.com.mx",
                        CuerpoHTML = false,
                        // Puede incluir los correos que desee, solo necesita separarlos por ";"
                        Destinatarios = "jmoreno@novutek.com;dgarcia@novutek.com"
                    });
                }
            }
            #endregion
            #region Papa Alberto
            //bool SAT = true;
            //RepoBox33.SATCatalogos.SATCats satCats = new SATCats();
            //satCats.CargarCatalogos();
            //// Los parametros necesarios son 1. Ruta del .Cer, 2. Ruta del .Key, 3. Contraseña del archivo .Key
            //RepoBox33.Comprobante factura = new RepoBox33.Comprobante(@"C:\Users\Samuel.Arreola\Documents\Papa Alberto\SELLOS JUDTH PINEDO\00001000000409727251.cer", @"C:\Users\Samuel.Arreola\Documents\Papa Alberto\SELLOS JUDTH PINEDO\CSD_SELLOS_DIGITALES_PIAJ480831NQ0_20180228_090032.key", "PIAJ4808");
            //factura.Version = "3.3";
            //factura.Serie = "A";
            //factura.Folio = "1601";// DateTime.Now.Hour.ToString("00") + DateTime.Now.Minute.ToString("00") + DateTime.Now.Second.ToString("00");
            //factura._Fecha = new DateTime(2018, 2, 28, 17, 0, 0);
            //factura.FormaPago = "01"; // Catalogo de formas de pago.
            //factura.CondicionesDePago = "CONTADO";
            //factura.SubTotal = (decimal)6857.50;
            //factura.Descuento = 0;
            //factura.Moneda = "MXN";
            //factura.TipoCambio = 1;
            //factura.Total = (decimal)6857.50;
            //factura.TipoDeComprobante = "I"; // En caso de ser factura, siempre será I
            //factura.MetodoPago = "PUE"; // tres opciones PUE (pago en una sola exhibición), PIP (Pago inicial y parcialidades) PPD (Pago en parcialidades o diferido)
            //factura.LugarExpedicion = "81240";
            ////La forma de asignar valores a los objetos queda a su criterio.
            //factura.Emisor = new RepoBox33.Emisor()
            //{
            //    Rfc = (SAT ? "PIAJ480831NQ0" : "AAA010101AAA"),
            //    Nombre = "JUDITH PINEDO DE ANDA",
            //    RegimenFiscal = "621" // Clave del catalogo de SAT para Regimenes fiscales.
            //};

            //// Al igual que el Emisor, puede asignar valores del Receptor a su criterio.
            //factura.Receptor = new RepoBox33.Receptor()
            //{
            //    Rfc = "XAXX010101000",
            //    Nombre = "PUBLICO EN GENERAL",
            //    UsoCFDI = "P01" // Clave del Cataloo de SAT para usos de CFDI.
            //};
            //// Los conceptos (productos y/o servicios) se agregan de la siguiente forma o de igual manera queda a su criterio.
            //factura.Conceptos = new List<RepoBox33.Concepto>();
            //RepoBox33.Concepto concepto = new RepoBox33.Concepto()
            //{
            //    Cantidad = 14,
            //    Descripcion = "CHORIZO",
            //    Importe = (decimal)1400,
            //    Unidad = "KG",
            //    ValorUnitario = 100,
            //    ClaveProdServ = "50112000",
            //    ClaveUnidad = "KGM",
            //    Descuento = 0
            //};
            //concepto.Impuestos = new RepoBox33.C_Impuestos();
            //concepto.Impuestos.Traslados = new List<RepoBox33.CI_Traslado>();
            //concepto.Impuestos.Traslados.Add(new RepoBox33.CI_Traslado() { Base = concepto.Importe, Impuesto = "002", TasaOCuota = (decimal)0.000000, TipoFactor = "Tasa", Importe = Convert.ToDecimal((concepto.Importe * (decimal)0.000000).ToString("N2")) });
            //factura.Conceptos.Add(concepto);

            //concepto = new RepoBox33.Concepto()
            //{
            //    Cantidad = 57,
            //    Descripcion = "CHORIZO",
            //    Importe = (decimal)4845.00,
            //    Unidad = "KG",
            //    ValorUnitario = 85,
            //    ClaveProdServ = "50112000",
            //    ClaveUnidad = "KGM",
            //    Descuento = 0
            //};
            //concepto.Impuestos = new RepoBox33.C_Impuestos();
            //concepto.Impuestos.Traslados = new List<RepoBox33.CI_Traslado>();
            //concepto.Impuestos.Traslados.Add(new RepoBox33.CI_Traslado() { Base = concepto.Importe, Impuesto = "002", TasaOCuota = (decimal)0.000000, TipoFactor = "Tasa", Importe = Convert.ToDecimal((concepto.Importe * (decimal)0.000000).ToString("N2")) });
            //factura.Conceptos.Add(concepto);

            //concepto = new RepoBox33.Concepto()
            //{
            //    Cantidad = (decimal)2.750,
            //    Descripcion = "COCHINITA",
            //    Importe = (decimal)357.50,
            //    Unidad = "KG",
            //    ValorUnitario = 130,
            //    ClaveProdServ = "50112000",
            //    ClaveUnidad = "KGM",
            //    Descuento = 0
            //};
            //concepto.Impuestos = new RepoBox33.C_Impuestos();
            //concepto.Impuestos.Traslados = new List<RepoBox33.CI_Traslado>();
            //concepto.Impuestos.Traslados.Add(new RepoBox33.CI_Traslado() { Base = concepto.Importe, Impuesto = "002", TasaOCuota = (decimal)0.000000, TipoFactor = "Tasa", Importe = Convert.ToDecimal((concepto.Importe * (decimal)0.000000).ToString("N2")) });
            //factura.Conceptos.Add(concepto);

            //concepto = new RepoBox33.Concepto()
            //{
            //    Cantidad = (decimal)1.5,
            //    Descripcion = "CHILORIO",
            //    Importe = (decimal)255,
            //    Unidad = "KG",
            //    ValorUnitario = 170,
            //    ClaveProdServ = "50112000",
            //    ClaveUnidad = "KGM",
            //    Descuento = 0
            //};
            //concepto.Impuestos = new RepoBox33.C_Impuestos();
            //concepto.Impuestos.Traslados = new List<RepoBox33.CI_Traslado>();
            //concepto.Impuestos.Traslados.Add(new RepoBox33.CI_Traslado() { Base = concepto.Importe, Impuesto = "002", TasaOCuota = (decimal)0.000000, TipoFactor = "Tasa", Importe = Convert.ToDecimal((concepto.Importe * (decimal)0.000000).ToString("N2")) });
            //factura.Conceptos.Add(concepto);

            //C_Impuestos listaImpuestos = factura.ListadoImpuestos();

            //if (listaImpuestos != null)
            //{
            //    factura.Impuestos = new RepoBox33.Impuestos();
            //    if ((listaImpuestos.trasladoIEPS + listaImpuestos.trasladoIVA) > 0)
            //    {
            //        factura.Impuestos.Traslados = new List<RepoBox33.Traslado>();
            //        if (listaImpuestos.trasladoIVA > 0)
            //            factura.Impuestos.Traslados.Add(new RepoBox33.Traslado() { Importe = listaImpuestos.trasladoIVA, Impuesto = "002", TasaOCuota = (decimal)0.160000, TipoFactor = "Tasa" });
            //        if (listaImpuestos.trasladoIEPS > 0)
            //            factura.Impuestos.Traslados.Add(new RepoBox33.Traslado() { Importe = listaImpuestos.trasladoIEPS, Impuesto = "003", TasaOCuota = (decimal)0.16, TipoFactor = "Tasa" });
            //    }
            //    if ((listaImpuestos.retencionISR + listaImpuestos.retencionIVA) > 0)
            //    {
            //        factura.Impuestos.Retenciones = new List<RepoBox33.Retencion>();
            //        if (listaImpuestos.retencionIVA > 0)
            //            factura.Impuestos.Retenciones.Add(new RepoBox33.Retencion() { Importe = listaImpuestos.retencionIVA, Impuesto = "002" });
            //        if (listaImpuestos.retencionISR > 0)
            //            factura.Impuestos.Retenciones.Add(new RepoBox33.Retencion() { Importe = listaImpuestos.retencionISR, Impuesto = "001" });
            //    }
            //}

            //// La utilización de este metodo tambien es obligatorio, asi nos evitamos realizar la sumatoria manual de los impuestos
            //factura.Impuestos.CalcularTotales();
            //// De cualquier forma, si desean asignar de forma manual los valores de total de listaImpuestos pueden hacerlo con las siguientes lineas.
            ////factura.Impuestos.totalImpuestosRetenidos = 0;
            ////factura.Impuestos.totalImpuestosTrasladados = 0;
            //factura.Banco = "";
            //factura.Observaciones = "ESTAS NOTAS DE VENTA GENERADAS DURANTE EL PERIODO COMPRENDIDO DEL DIA 16 DE FEBRERO AL 28 DE FEBRERO DEL AÑO EN CURSO. SEGUN NOTAS DE VENTA FOLIADAS DEL NUMERO DE FOLIO 7863 AL FOLIO 7902.";
            //factura.Cuenta = "";
            //// Addenda
            //// Direccion del cliente (Receptor). // De igual forma imporntante instanciar la propiedad ReceptorDomicilio.
            //factura.ReceptorDomicilio = new RepoBox.Domicilio()
            //{
            //    calle = "DOMICILIO CONOCIDO",
            //    noExterior = "S/N",
            //    noInterior = "",
            //    referencia = "",
            //    colonia = "CONOCIDA",
            //    localidad = "CONOCIDA",
            //    municipio = "CONOCIDO",
            //    estado = "CONOCIDO",
            //    pais = "MEXICO",
            //    codigoPostal = "00000"
            //};


            //// Lo siguiente es generar el archivo CFD (xml sin timbrar)
            //string cfd = factura.GenerarCFD();

            //// Si la respuesta del método es positiva pasa a timbrarlo
            //if (!cfd.StartsWith("RepoBox"))
            //{
            //    cfd = cfd.Replace("<?xml version=\"1.0\"?>", "<?xml version=\"1.0\" encoding=\"UTF-8\"?>");
            //    RepoBox.CFDIv32 cfdi = new RepoBox.CFDIv32();
            //    // Timbrado del CFD
            //    string timbrado = cfdi.Timbrar33(cfd, SAT, "FINKOK", factura.Emisor.Rfc); // "timbrado" es la variable si requieren el xml en texto plano
            //    if (!timbrado.ToUpper().StartsWith("REPOBOX"))
            //    {
            //        string xmlPath = factura.SaveXml(ref timbrado); // "xmlPath" es la variable que regresa la ruta donde se guardó el XML, pero el metodo es para guardar el xml en archivo.
            //        // Los parametros que acepta GenerarPDF 1. xml en texto plano (ya timbrado), 2. Ruta del logo a mostrar en la factura, 3. Ruta y nombre con el que se guardará el PDF.
            //        string pdfPath = cfdi.GenerarPDF33(timbrado, "", xmlPath.Replace(".XML", ""));
            //    }
            //}
            #endregion
        }

        private void btnPago10_v33_Click(object sender, EventArgs e)
        {
            bool SAT = false;
            // Los parametros necesarios son 1. Ruta del .Cer, 2. Ruta del .Key, 3. Contraseña del archivo .Key
            RepoBox33.Comprobante factura = new RepoBox33.Comprobante(@"C:\Repobox\Timbrado\CEA990831FI8\00001000000403161018.cer", @"C:\Repobox\Timbrado\CEA990831FI8\CSD_Cananea_CEA990831FI8_20160719_124927.key", "cananea2016");
            factura.Version = "3.3";
            factura.Serie = "PAGO";
            factura.Folio = "10112017";// DateTime.Now.Hour.ToString("00") + DateTime.Now.Minute.ToString("00") + DateTime.Now.Second.ToString("00");
            factura._Fecha = DateTime.Now;
            // factura.FormaPago = Este campo no debe de existir.
            //factura.CondicionesDePago = Este campo no debe de existir.
            factura.Moneda = "XXX"; //"XXX" Fijo
            factura.SubTotal = 0;
            //factura.Descuento = Este campo no debe de existir.
            //factura.TipoCambio = Este campo no debe de existir.
            factura.Total = 0;
            factura.TipoDeComprobante = "P"; // Como es pago debe de ir P fija.
            //factura.MetodoPago = Este campo NO debe de existir
            factura.LugarExpedicion = "85000";
            //La forma de asignar valores a los objetos queda a su criterio.
            factura.Emisor = new RepoBox33.Emisor()
            {
                Rfc = "CEA990831FI8",
                Nombre = "CEA - RAZON SOCIAL",
                RegimenFiscal = "603" // Clave del catalogo de SAT para Regimenes fiscales.
            };

            // Al igual que el Emisor, puede asignar valores del Receptor a su criterio.
            factura.Receptor = new RepoBox33.Receptor()
            {
                Rfc = "XAXX010101000",
                Nombre = "RAZON SOCIAL",
                UsoCFDI = "P01" // P01 fija, "por definir".
            };
            // Los conceptos (productos y/o servicios) se agregan de la siguiente forma o de igual manera queda a su criterio.
            factura.Conceptos = new List<RepoBox33.Concepto>();
            RepoBox33.Concepto concepto = new RepoBox33.Concepto()
            {
                Cantidad = 1,
                Descripcion = "Pago", // Fija.
                Importe = 0,
                //NoIdentificacion = "C1", Este campo no debe existir
                //Unidad = "PZA", Este campo no debe existir
                ValorUnitario = 0,
                ClaveProdServ = "84111506", // Fija
                ClaveUnidad = "ACT", // Fija
                //Descuento = 0 Este campo no debe existir.
            };
            factura.Conceptos.Add(concepto);
            factura.Banco = "Banorte 20202020202";
            factura.Observaciones = "Ahora si hay observaciones";
            // Generar el Complemento de Recepción de Pagos.
            RepoBox.Pagos10.Pagos recepcionPagos = new RepoBox.Pagos10.Pagos();
            recepcionPagos.Version = "1.0";
            recepcionPagos.Pago = new List<RepoBox.Pagos10.Pago>();
            RepoBox.Pagos10.Pago pago = new RepoBox.Pagos10.Pago();
            pago._FechaPago = DateTime.Now.AddDays(-5); // Cuando se recibió el pago.
            pago.FormaDePagoP = "01"; // Catalogo de formas de pago, siempre debe ser diferente a 99.
            pago.MonedaP = "MXN"; // Clave de moneda.
            if (pago.MonedaP != "MXN")
            {
                pago.TipoCambioP = 1;
                pago.TipoCambioPSpecified = true;
            }
            pago.Monto = 500;
            pago.NumOperacion = "01"; // Siempre se debe registrar, puede ser No. cheque, no. transaccion SPEI, etc.
            pago.DoctoRelacionado = new List<RepoBox.Pagos10.DoctoRelacionado>();
            pago.DoctoRelacionado.Add(new RepoBox.Pagos10.DoctoRelacionado()
            {
                IdDocumento = "FF2CF576-A1D8-4388-B271-AB7C6A10A20D",
                Serie = "H",
                Folio = "10101",
                MonedaDR = "MXN",
                MetodoDePagoDR = "PPD", // Fija.
                NumParcialidad = "1",
                ImpSaldoAnt = 1000,
                ImpPagado = 500,
                ImpSaldoInsoluto = 500
            });

            if (pago.DoctoRelacionado[0].MonedaDR != "MXN")
            {
                pago.DoctoRelacionado[0].TipoCambioDR = 1;
                pago.DoctoRelacionado[0].TipoCambioDRSpecified = true;
            }
            recepcionPagos.Pago.Add(pago);
            XmlDocument docPago = new XmlDocument();
            string xmllll = recepcionPagos.GenerarCFD();
            docPago.LoadXml(xmllll);
            if (docPago.DocumentElement != null)
            {
                factura.Complemento = new List<ComprobanteComplemento>();
                ComprobanteComplemento complement = new ComprobanteComplemento();
                complement.Any = new List<XmlElement>();
                complement.Any.Add(docPago.DocumentElement);
                factura.Complemento.Add(complement);
            }
            // Lo siguiente es generar el archivo CFD (xml sin timbrar)
            string cfd = factura.GenerarCFD();

            // Si la respuesta del método es positiva pasa a timbrarlo
            if (!cfd.StartsWith("RepoBox"))
            {
                RepoBox.CFDIv32 cfdi = new RepoBox.CFDIv32();
                bool enviado = false;
                // Timbrado del CFD
                string timbrado = cfdi.Timbrar33(cfd, SAT, "PADE", factura.Emisor.Rfc); // "timbrado" es la variable si requieren el xml en texto plano
                if (!timbrado.ToUpper().StartsWith("REPOBOX"))
                {
                    string xmlPath = factura.SaveXml(ref timbrado); // "xmlPath" es la variable que regresa la ruta donde se guardó el XML, pero el metodo es para guardar el xml en archivo.
                    // Los parametros que acepta GenerarPDF 1. xml en texto plano (ya timbrado), 2. Ruta del logo a mostrar en la factura, 3. Ruta y nombre con el que se guardará el PDF.
                    string pdfPath = cfdi.GenerarPDF(timbrado, @"C:\Repobox\Timbrado\RepoBox.jpg", xmlPath.Replace(".XML", ""));

                    // En caso de necesitar regenerar el archivo PDF, existe el siguiente método
                    // Parametros: 1. Ruta del archivo XML, 2. Ruta de logo a mostrar en la factura. El sistema nombrará al PDF igual que el XML.
                    // pdfPath = cfdi.GenerarPDF_PathFile(xmlPath, @"C:\Repobox\Timbrado\RepoBox.jpg");
                    // Metodo de envío del correo.

                    enviado = cfdi.EnviarEmail(xmlPath, pdfPath, new RepoBox.EmailParameters()
                    {
                        Asunto = "Envio de Correo Prueba",
                        Password = "Password",
                        Correo = "direccion@email.com",
                        PuertoSMTP = 587,
                        SSL = false,
                        CuerpoMensaje = "Este es el cuerpo del mensaje",
                        SMTP = "mail.repobox.com.mx",
                        CuerpoHTML = false,
                        // Puede incluir los correos que desee, solo necesita separarlos por ";"
                        Destinatarios = "jmoreno@novutek.com;dgarcia@novutek.com"
                    });
                }
            }
        }

        private void btnDonatarias11_v33_Click(object sender, EventArgs e)
        {
            bool SAT = false;
            RepoBox33.SATCatalogos.SATCats satCats = new SATCats();
            satCats.CargarCatalogos();
            // Los parametros necesarios son 1. Ruta del .Cer, 2. Ruta del .Key, 3. Contraseña del archivo .Key
            //RepoBox33.Comprobante factura = new RepoBox33.Comprobante(@"C:\RepoBox\NominaElectronica\Config\00001000000302316116.cer", @"C:\RepoBox\NominaElectronica\Config\CSD_matriz_SFI950101DU2_20140109_115049.key", "cristobal2012");
            RepoBox33.Comprobante factura = new RepoBox33.Comprobante(@"C:\Repobox\Timbrado\CEA990831FI8\00001000000403161018.cer", @"C:\Repobox\Timbrado\CEA990831FI8\CSD_Cananea_CEA990831FI8_20160719_124927.key", "cananea2016");
            factura.Version = "3.3";
            factura.Serie = "H";
            factura.Folio = "131355";// DateTime.Now.Hour.ToString("00") + DateTime.Now.Minute.ToString("00") + DateTime.Now.Second.ToString("00");
            factura._Fecha = DateTime.Now;
            factura.FormaPago = satCats.FormasDePago[0].ID;
            factura.CondicionesDePago = "PUE"; // tres opciones PUE (pago en una sola exhibición), PIP (Pago inicial y parcialidades) PPD (Pago en parcialidades o diferido)
            factura.SubTotal = 10;
            factura.Descuento = 0;
            factura.Moneda = "MXN";
            factura.TipoCambio = 1;
            factura.Total = (decimal)11.60;
            factura.TipoDeComprobante = "I"; // En caso de ser factura, siempre será I
            factura.MetodoPago = satCats.MetodosPago[0].ID;
            factura.LugarExpedicion = "85000";
            //La forma de asignar valores a los objetos queda a su criterio.
            factura.Emisor = new RepoBox33.Emisor()
            {
                Rfc = "CEA990831FI8",
                Nombre = "CEA - RAZON SOCIAL",
                RegimenFiscal = "603" // Clave del catalogo de SAT para Regimenes fiscales.
            };

            // Al igual que el Emisor, puede asignar valores del Receptor a su criterio.
            factura.Receptor = new RepoBox33.Receptor()
            {
                Rfc = "XAXX010101000",
                Nombre = "RAZON SOCIAL",
                UsoCFDI = "P01" // Clave del Cataloo de SAT para usos de CFDI.
            };
            // Los conceptos (productos y/o servicios) se agregan de la siguiente forma o de igual manera queda a su criterio.
            factura.Conceptos = new List<RepoBox33.Concepto>();
            RepoBox33.Concepto concepto = new RepoBox33.Concepto()
            {
                Cantidad = 1,
                Descripcion = "Producto 1",
                Importe = 10,
                NoIdentificacion = "C1",
                Unidad = "PZA",
                ValorUnitario = 10,
                ClaveProdServ = "93161701",
                ClaveUnidad = "E48",
                Descuento = 0
            };
            concepto.Impuestos = new RepoBox33.C_Impuestos();
            //concepto.Impuestos.Retenciones = new List<RepoBox33.CI_Retencion>();
            //concepto.Impuestos.Retenciones.Add(new RepoBox33.CI_Retencion() { Base = concepto.Importe, Impuesto = "002", TasaOCuota = (decimal)0.16, TipoFactor = "Tasa", Importe = concepto.Importe * (decimal)0.16 });
            concepto.Impuestos.Traslados = new List<RepoBox33.CI_Traslado>();
            concepto.Impuestos.Traslados.Add(new RepoBox33.CI_Traslado() { Base = concepto.Importe, Impuesto = "002", TasaOCuota = (decimal)0.16, TipoFactor = "Tasa", Importe = concepto.Importe * (decimal)0.160000 });
            factura.Conceptos.Add(concepto);

            C_Impuestos listaImpuestos = factura.ListadoImpuestos();

            if (listaImpuestos != null)
            {
                factura.Impuestos = new RepoBox33.Impuestos();
                if ((listaImpuestos.trasladoIEPS + listaImpuestos.trasladoIVA) > 0)
                {
                    factura.Impuestos.Traslados = new List<RepoBox33.Traslado>();
                    if (listaImpuestos.trasladoIVA > 0)
                        factura.Impuestos.Traslados.Add(new RepoBox33.Traslado() { Importe = listaImpuestos.trasladoIVA, Impuesto = "002", TasaOCuota = (decimal)0.16, TipoFactor = "Tasa" });
                    if (listaImpuestos.trasladoIEPS > 0)
                        factura.Impuestos.Traslados.Add(new RepoBox33.Traslado() { Importe = listaImpuestos.trasladoIEPS, Impuesto = "003", TasaOCuota = (decimal)0.16, TipoFactor = "Tasa" });
                }
                if ((listaImpuestos.retencionISR + listaImpuestos.retencionIVA) > 0)
                {
                    factura.Impuestos.Retenciones = new List<RepoBox33.Retencion>();
                    if (listaImpuestos.retencionIVA > 0)
                        factura.Impuestos.Retenciones.Add(new RepoBox33.Retencion() { Importe = listaImpuestos.retencionIVA, Impuesto = "002" });
                    if (listaImpuestos.retencionISR > 0)
                        factura.Impuestos.Retenciones.Add(new RepoBox33.Retencion() { Importe = listaImpuestos.retencionISR, Impuesto = "001" });
                }
            }

            // La utilización de este metodo tambien es obligatorio, asi nos evitamos realizar la sumatoria manual de los impuestos
            factura.Impuestos.CalcularTotales();
            // De cualquier forma, si desean asignar de forma manual los valores de total de listaImpuestos pueden hacerlo con las siguientes lineas.
            //factura.Impuestos.totalImpuestosRetenidos = 0;
            //factura.Impuestos.totalImpuestosTrasladados = 0;
            factura.Banco = "Banorte 20202020202";
            factura.Observaciones = "Ahora si hay observaciones";
            // Generar el Complemento de Donatarias.
            RepoBox.Donat11.Donatarias donatarias = new RepoBox.Donat11.Donatarias();
            donatarias.fechaAutorizacion = DateTime.Now;
            donatarias.leyenda = "Leyenda Perrona";
            donatarias.noAutorizacion = "1212121212";
            donatarias.version = "1.1";
            XmlDocument xmlDonatarias = new XmlDocument();
            xmlDonatarias.LoadXml(donatarias.GenerarCFD());
            if (xmlDonatarias.InnerXml != "")
            {
                factura.Complemento = new List<ComprobanteComplemento>();
                ComprobanteComplemento complemento = new ComprobanteComplemento();
                complemento.Any = new List<XmlElement>();
                complemento.Any.Add(xmlDonatarias.DocumentElement);
            }

            // Lo siguiente es generar el archivo CFD (xml sin timbrar)
            string cfd = factura.GenerarCFD();

            // Si la respuesta del método es positiva pasa a timbrarlo
            if (!cfd.StartsWith("RepoBox"))
            {
                RepoBox.CFDIv32 cfdi = new RepoBox.CFDIv32();
                bool enviado = false;
                // Timbrado del CFD
                string timbrado = cfdi.Timbrar33(cfd, SAT, "FINKOK", factura.Emisor.Rfc); // "timbrado" es la variable si requieren el xml en texto plano
                if (!timbrado.ToUpper().StartsWith("REPOBOX"))
                {
                    string xmlPath = factura.SaveXml(ref timbrado); // "xmlPath" es la variable que regresa la ruta donde se guardó el XML, pero el metodo es para guardar el xml en archivo.
                    // Los parametros que acepta GenerarPDF 1. xml en texto plano (ya timbrado), 2. Ruta del logo a mostrar en la factura, 3. Ruta y nombre con el que se guardará el PDF.
                    string pdfPath = cfdi.GenerarPDF(timbrado, @"C:\Repobox\Timbrado\RepoBox.jpg", xmlPath.Replace(".XML", ""));

                    // En caso de necesitar regenerar el archivo PDF, existe el siguiente método
                    // Parametros: 1. Ruta del archivo XML, 2. Ruta de logo a mostrar en la factura. El sistema nombrará al PDF igual que el XML.
                    // pdfPath = cfdi.GenerarPDF_PathFile(xmlPath, @"C:\Repobox\Timbrado\RepoBox.jpg");
                    // Metodo de envío del correo.

                    enviado = cfdi.EnviarEmail(xmlPath, pdfPath, new RepoBox.EmailParameters()
                    {
                        Asunto = "Envio de Correo Prueba",
                        Password = "Password",
                        Correo = "direccion@email.com",
                        PuertoSMTP = 587,
                        SSL = false,
                        CuerpoMensaje = "Este es el cuerpo del mensaje",
                        SMTP = "mail.repobox.com.mx",
                        CuerpoHTML = false,
                        // Puede incluir los correos que desee, solo necesita separarlos por ";"
                        Destinatarios = "jmoreno@novutek.com;dgarcia@novutek.com"
                    });
                }
            }
        }

        private void btnComercioExt11_v33_Click(object sender, EventArgs e)
        {
            
            bool SAT = false;
            decimal Rimporte = Convert.ToDecimal("113958.40"), RPrecioU = Convert.ToDecimal("1.28"), RTC = Convert.ToDecimal("19.0446"), RCantidad = Convert.ToDecimal("89030");
            RepoBox33.SATCatalogos.SATCats satCats = new SATCats();
            satCats.CargarCatalogos();
            // Los parametros necesarios son 1. Ruta del .Cer, 2. Ruta del .Key, 3. Contraseña del archivo .Key
            //RepoBox33.Comprobante factura = new RepoBox33.Comprobante(@"C:\RepoBox\NominaElectronica\Config\00001000000302316116.cer", @"C:\RepoBox\NominaElectronica\Config\CSD_matriz_SFI950101DU2_20140109_115049.key", "cristobal2012");
            //RepoBox33.Comprobante factura = new RepoBox33.Comprobante(@"C:\Repobox\Timbrado\CEA990831FI8\00001000000403161018.cer", @"C:\Repobox\Timbrado\CEA990831FI8\CSD_Cananea_CEA990831FI8_20160719_124927.key", "cananea2016");
            //RepoBox33.Comprobante factura = new RepoBox33.Comprobante(@"C:\RepoBox\Timbrado\DEMO\CSD01_AAA010101AAA.cer", @"C:\RepoBox\Timbrado\DEMO\CSD01_AAA010101AAA.key", "12345678a");
            RepoBox33.Comprobante factura = new RepoBox33.Comprobante(@"C:\RepoBox\CSD\ALIMAR\00001000000303431516.cer", @"C:\RepoBox\CSD\ALIMAR\CSD_ALIMENTOS_MARINOS_Y_AGROPECUARIOS_SA_AMA941022EP2_20140321_155043.key", "IBA123456");
            factura.Version = "3.3";
            factura.Serie = "AL";
            factura.Folio = "2899";// DateTime.Now.Hour.ToString("00") + DateTime.Now.Minute.ToString("00") + DateTime.Now.Second.ToString("00");
            factura._Fecha = DateTime.Now;
            factura.FormaPago = "99";
            //factura.CondicionesDePago = "PUE"; // tres opciones PUE (pago en una sola exhibición), PIP (Pago inicial y parcialidades) PPD (Pago en parcialidades o diferido)
            factura.SubTotal = Rimporte;
            //factura.Descuento = 0;
            factura.Moneda = "USD";
            factura.TipoCambio = RTC;
            factura.TipoCambioSpecified = true;
            factura.Total = Rimporte;
            factura.TipoDeComprobante = "I"; // En caso de ser factura, siempre será I
            factura.MetodoPago = "PPD";
            factura.LugarExpedicion = "85000";
            //La forma de asignar valores a los objetos queda a su criterio.
            factura.Emisor = new RepoBox33.Emisor()
            {
                Rfc = "AMA941022EP2",
                Nombre = "ALIMENTOS MARINOS Y AGROPECUARIOS S.A",
                RegimenFiscal = "601" // Clave del catalogo de SAT para Regimenes fiscales.
            };

            // Al igual que el Emisor, puede asignar valores del Receptor a su criterio.
            factura.Receptor = new RepoBox33.Receptor()
            {
                Rfc = "XEXX010101000",
                //Nombre = "TEAMPOWER FEED &amp; GRAINS TRADING LIMITED",
                //ResidenciaFiscal = "HKG",
                Nombre = "WILBUR-ELLIS FEED, LLC",
                ResidenciaFiscal = "USA",
                UsoCFDI = "P01", // Clave del Cataloo de SAT para usos de CFDI.
                NumRegIdTrib = "000000000"
            };
            // Los conceptos (productos y/o servicios) se agregan de la siguiente forma o de igual manera queda a su criterio.
            factura.Conceptos = new List<RepoBox33.Concepto>();
            RepoBox33.Concepto concepto = new RepoBox33.Concepto()
            {
                Cantidad = RCantidad,
                Descripcion = "FISHMEAL MX 68E 23012001 HARINA DE PESCADO TOLVA TILX 640444\n"
+ "CONTRATO: 257809 SELLOS\n"
+ "FXE-C126561\n"
+ "FXE-C126562\n"
+ "FXE-C126563\n"
+ "FXE-C126564\n"
+ "FXE-C126565\n"
+ "FXE-C126566\n"
+ "FXE-C126567\n"
+ "FXE-C126568\n"
+ "FXE-C126569\n"
+ "FXE-C126570\n"
+"BENEFICIARY BANK NAME:\n"
+"BBVA BANCOMER S.A. (JP MORGAN CHASE BANK)\n"
+ "ACCOUNT: 0150582530\n"
+ "SWIFT: BCMRMXMM\n"
+ "ABBA: 021000021.",
                Importe = Rimporte,
                NoIdentificacion = "15561",
                Unidad = "01",
                ValorUnitario = RPrecioU,
                ClaveProdServ = "10171503",
                ClaveUnidad = "KGM",
                //Descuento = 0
            };
            //RepoBox33.Concepto concepto2 = new RepoBox33.Concepto()
            //{
            //    Cantidad = 1,
            //    Descripcion = "Producto 1",
            //    Importe = 50,
            //    NoIdentificacion = "C2",
            //    Unidad = "01",
            //    ValorUnitario = 50,
            //    ClaveProdServ = "93161701",
            //    ClaveUnidad = "A75",
            //    //Descuento = 0
            //};
            factura.Conceptos.Add(concepto);
            //factura.Conceptos.Add(concepto2);

            // La utilización de este metodo tambien es obligatorio, asi nos evitamos realizar la sumatoria manual de los impuestos
            //factura.Impuestos.CalcularTotales();
            // De cualquier forma, si desean asignar de forma manual los valores de total de listaImpuestos pueden hacerlo con las siguientes lineas.
            //factura.Impuestos.totalImpuestosRetenidos = 0;
            //factura.Impuestos.totalImpuestosTrasladados = 0;
            factura.Banco = "";
            factura.Observaciones = "";
            // Generar el Complemento de ComercioExterior.
            RepoBox.CE11.ComercioExterior cee = new RepoBox.CE11.ComercioExterior();
            cee.TipoOperacion = "2";
            cee.ClaveDePedimento = "A1";
            cee.CertificadoOrigen = 0;
            cee.Incoterm = "DAT";
            cee.Subdivision = 0;
            cee.TipoCambioUSD = RTC;
            cee.TotalUSD = Rimporte;

            cee.Emisor = new RepoBox.CE11.Emisor();
            cee.Emisor.Domicilio = new RepoBox.CE11.Domicilio();
            cee.Emisor.Domicilio.Calle = "SUFRAGIO EFECTIVO";
            cee.Emisor.Domicilio.NumeroExterior = "502 NTE";
            cee.Emisor.Domicilio.Colonia = "0858";
            cee.Emisor.Domicilio.Localidad = "04";
            cee.Emisor.Domicilio.Municipio = "018";
            cee.Emisor.Domicilio.Estado = "SON";
            cee.Emisor.Domicilio.Pais = "MEX";
            cee.Emisor.Domicilio.CodigoPostal = "85000";

            cee.Receptor = new RepoBox.CE11.Receptor();
            //cee.Receptor.NumRegIdTrib = "000000000";
            cee.Receptor.Domicilio = new RepoBox.CE11.Domicilio();
            // "WILBUR-ELLIS FEED, LLC"
            cee.Receptor.Domicilio.Calle = "COLUMBIA RIVER DR.";
            cee.Receptor.Domicilio.NumeroExterior = "2001";
            cee.Receptor.Domicilio.NumeroInterior = "SUITE 200";
            //cee.Receptor.Domicilio.Colonia = "GREAT EAGLE CENTRE";
            cee.Receptor.Domicilio.Localidad = "VANCOUVER";
            cee.Receptor.Domicilio.Municipio = "VANCOUVER";
            cee.Receptor.Domicilio.Estado = "WA";
            cee.Receptor.Domicilio.Pais = "USA";
            cee.Receptor.Domicilio.CodigoPostal = "98661";
            #region "TEAMPOWER FEED &amp; GRAINS TRADING LIMITED"
            //// "TEAMPOWER FEED &amp; GRAINS TRADING LIMITED"
            //cee.Receptor.Domicilio.Calle = "HARBOUR RD";
            //cee.Receptor.Domicilio.NumeroExterior = "23";
            //cee.Receptor.Domicilio.NumeroInterior = "ROOMS 1803-1805, 18/F";
            //cee.Receptor.Domicilio.Colonia = "GREAT EAGLE CENTRE";
            //cee.Receptor.Domicilio.Localidad = "WANCHAI";
            //cee.Receptor.Domicilio.Municipio = "WANCHAI";
            //cee.Receptor.Domicilio.Estado = "HONG KONG";
            //cee.Receptor.Domicilio.Pais = "HKG";
            //cee.Receptor.Domicilio.CodigoPostal = "00000";
            #endregion


            //cee.Destinatario = new List<RepoBox.CE11.Destinatario>();
            //RepoBox.CE11.Destinatario destinatario = new RepoBox.CE11.Destinatario()
            //{
            //    //NumRegIdTrib = "XEXX010101000",
            //    Nombre = "Destinatario ejemplo"
            //};
            //destinatario.Domicilio = new List<RepoBox.CE11.Domicilio>();
            //RepoBox.CE11.Domicilio domicilio = new RepoBox.CE11.Domicilio();
            //domicilio.Calle = "Tulipanes";
            //domicilio.NumeroExterior = "348";
            //domicilio.Colonia = "Jardines del Valle";
            //domicilio.Localidad = "Ciudad Obregon";
            //domicilio.Municipio = "Cajeme";
            //domicilio.Estado = "Sonora";
            //domicilio.Pais = "CHN";
            //domicilio.CodigoPostal = "00000";
            //destinatario.Domicilio.Add(domicilio);
            //cee.Destinatario.Add(destinatario);

            cee.Mercancias = new List<RepoBox.CE11.Mercancia>();
            cee.Mercancias.Add(new RepoBox.CE11.Mercancia()
            {
                NoIdentificacion = "15561",
                ValorDolares = Rimporte,
                CantidadAduana = RCantidad,
                UnidadAduana = "01",
                ValorUnitarioAduana = RPrecioU,
                FraccionArancelaria = "23012001"
            });
            //cee.Mercancias.Add(new RepoBox.CE11.Mercancia()
            //{
            //    NoIdentificacion = "C2",
            //    ValorDolares = Convert.ToDecimal("50.00"),
            //    CantidadAduana = 1,
            //    UnidadAduana = "01",
            //     ValorUnitarioAduana = Convert.ToDecimal("50.00"),
            //    FraccionArancelaria = "03051001"
            //});
            XmlDocument xmlCE11 = new XmlDocument();
            xmlCE11.LoadXml(cee.GenerarCFD());
            if (xmlCE11 != null && xmlCE11.InnerXml != "")
            {
                factura.Complemento = new List<ComprobanteComplemento>();
                ComprobanteComplemento complemento = new ComprobanteComplemento();
                complemento.Any = new List<XmlElement>();
                complemento.Any.Add(xmlCE11.DocumentElement);
                factura.Complemento.Add(complemento);
            }
            // Lo siguiente es generar el archivo CFD (xml sin timbrar)
            string cfd = factura.GenerarCFD();

            // Si la respuesta del método es positiva pasa a timbrarlo
            if (!cfd.StartsWith("RepoBox"))
            {
                RepoBox.CFDIv32 cfdi = new RepoBox.CFDIv32();
                bool enviado = false;
                // Timbrado del CFD
                string timbrado = cfdi.Timbrar33(cfd, SAT, "FINKOK", factura.Emisor.Rfc); // "timbrado" es la variable si requieren el xml en texto plano
                if (!timbrado.ToUpper().StartsWith("REPOBOX"))
                {
                    string xmlPath = factura.SaveXml(ref timbrado); // "xmlPath" es la variable que regresa la ruta donde se guardó el XML, pero el metodo es para guardar el xml en archivo.
                    // Los parametros que acepta GenerarPDF 1. xml en texto plano (ya timbrado), 2. Ruta del logo a mostrar en la factura, 3. Ruta y nombre con el que se guardará el PDF.
                    string pdfPath = cfdi.GenerarPDF33(timbrado, @"C:\Repobox\Timbrado\RepoBox.jpg", xmlPath.Replace(".XML", ""));

                    // En caso de necesitar regenerar el archivo PDF, existe el siguiente método
                    // Parametros: 1. Ruta del archivo XML, 2. Ruta de logo a mostrar en la factura. El sistema nombrará al PDF igual que el XML.
                    // pdfPath = cfdi.GenerarPDF_PathFile(xmlPath, @"C:\Repobox\Timbrado\RepoBox.jpg");
                    // Metodo de envío del correo.

                    enviado = cfdi.EnviarEmail(xmlPath, pdfPath, new RepoBox.EmailParameters()
                    {
                        Asunto = "Envio de Correo Prueba",
                        Password = "Password",
                        Correo = "direccion@email.com",
                        PuertoSMTP = 587,
                        SSL = false,
                        CuerpoMensaje = "Este es el cuerpo del mensaje",
                        SMTP = "mail.repobox.com.mx",
                        CuerpoHTML = false,
                        // Puede incluir los correos que desee, solo necesita separarlos por ";"
                        Destinatarios = "jmoreno@novutek.com;dgarcia@novutek.com"
                    });
                }
            }
        }

        private void btnPDF32_Click(object sender, EventArgs e)
        {
            try
            {
                // Zacatecas
                RepoBox.CFDIv32 cfdi = new RepoBox.CFDIv32();
                string rutanueva = cfdi.GenerarPDF_PathFile(@"C:\Users\Samuel.Arreola\Documents\Zacatecas\C9CD0F9B-F270-459E-8078-CEAFCEB80441.XML", @"C:\Repobox\Timbrado\RepoBox.jpg");
                
            }
            catch (Exception)
            { }
        }

        private void btnProbarRangoImpuestos_Click(object sender, EventArgs e)
        {
            RepoBox.SAT.Funciones funciones = new RepoBox.SAT.Funciones();
            bool resultado = funciones.Impuestos((decimal)469.23, (decimal)0.160000, (decimal)75.07);

            //bool resultado = funciones.Impuestos((decimal)3049.84, (decimal)0.04, (decimal)121.99);
            //bool resultado2 = funciones.Importes((decimal)2, (decimal)128.358122, (decimal)256.72);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            XmlDocument doc = new XmlDocument();
            doc.Load(@"C:\Users\Samuel.Arreola\Documents\SEFIN REGENERAR PDF\210C545A-D3AF-4CD6-BD0C-D5199DA986B3.xml");
            if (doc.InnerXml.Trim() != "")
            {
                RepoBox.CFDIv32 cfdi = new RepoBox.CFDIv32();
                string rutanueva = cfdi.GenerarPDF33(doc.InnerXml, @"C:\Repobox\Timbrado\RepoBox.jpg", @"C:\Users\Samuel.Arreola\Documents\SEFIN REGENERAR PDF\210C545A-D3AF-4CD6-BD0C-D5199DA986B3");
                MessageBox.Show(rutanueva);
            }
        }

        private void btnCatPyS_Click(object sender, EventArgs e)
        {
            SATCats sat = new SATCats();
            ClavePyS pys = sat.GetClavePS("01010101");
            MessageBox.Show(this, "Clave: " + pys.Clave + ". Descripcion: " + pys.Descripcion, "SAT", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void btnCatUnidad_Click(object sender, EventArgs e)
        {
            RepoBoxEmailSender.EmailSender sender1 = new RepoBoxEmailSender.EmailSender("r3c3pc10n");
            while (sender1.Mensaje != "")
            {
                sender1.To = "Samuel.Arreola@RepoBox.com.mx";
                sender1.Subject = "Prueba de envio de validacion";
                sender1.Message = "{Mensaje}";
                //sender.CC = "{Emails separados por coma para agregar N correos}";
                string msg = ((sender1.Enviar() ? "" : "No ") + "Enviado" + " " + sender1.Mensaje);
                MessageBox.Show(msg);
            }
            //SATCats sat = new SATCats();
            //ClaveUnidad unidad = sat.GetClaveUnidad("E48");
            //MessageBox.Show(this, "Clave: " + unidad.Clave + ". Descripcion: " + unidad.Descripcion, "SAT", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void btnFacturarXmlDirecto_Click(object sender, EventArgs e)
        {
            //RepoBox33.Comprobante factura = new RepoBox33.Comprobante(@"C:\Repobox\Timbrado\CEA990831FI8\00001000000403161018.cer", @"C:\Repobox\Timbrado\CEA990831FI8\CSD_Cananea_CEA990831FI8_20160719_124927.key", "cananea2016");
            RepoBox.CFDIv32 cfdi = new RepoBox.CFDIv32();
            string cfd = "<?xml version=\"1.0\"?><Comprobante xmlns:xsi=\"http://www.w3.org/2001/XMLSchema-instance\" xmlns:xsd=\"http://www.w3.org/2001/XMLSchema\" xsi:schemaLocation=\"http://www.sat.gob.mx/cfd/3 http://www.sat.gob.mx/sitio_internet/cfd/3/cfdv33.xsd\" Version=\"3.3\" Serie=\"PAGO\" Folio=\"12110\" Fecha=\"2018-05-10T01:19:13\" Sello=\"k7J4qnhobJauno4GarSCg211ezao5Ahp8rO/GME1+YSi6kA8SA7nI7WGjV/IsXIPxP/Z5xW1SbaEoIQRn+V4Af0EbeZBe5LHJcZg9BS4KOvdRSqhUNsdALIq5ic40XY+gubIh5B+tK8Leu20pCDmi2QYWuZraJ0S9sQmyANZyZ10CSHl/jSZdrZk7KOY1e9inRbZbnQBLHeZVpTRCKD+SUiMY7MjDKRjZXCK3mE1wJqRzkJWsJ4Wgw+tEVLtIDfTyuPTh6LvruUsBoc+cJ3eww5rL4hIaqF1wpyfmpa9lQ134rjy++WFPoNDoi8DROGYTvcGguvy39Ytk4cZRMG+Pg==\" NoCertificado=\"00001000000408150996\" Certificado=\"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\" SubTotal=\"0\" Moneda=\"XXX\" Total=\"0\" TipoDeComprobante=\"P\" LugarExpedicion=\"85400\" xmlns=\"http://www.sat.gob.mx/cfd/3\"><Emisor Rfc=\"CEA990831FI8\" Nombre=\"COMISION ESTATAL DEL AGUA\" RegimenFiscal=\"603\" /><Receptor Rfc=\"MOTJ890401483\" Nombre=\"MOTJ\" UsoCFDI=\"P01\" /><Conceptos><Concepto ClaveProdServ=\"84111506\" Cantidad=\"1\" ClaveUnidad=\"ACT\" Descripcion=\"Pago\" ValorUnitario=\"0\" Importe=\"0\" /></Conceptos><Complemento><pago10:Pagos xmlns:xsi=\"http://www.w3.org/2001/XMLSchema-instance\" xmlns:xsd=\"http://www.w3.org/2001/XMLSchema\" Version=\"1.0\" xmlns:pago10=\"http://www.sat.gob.mx/Pagos\"><pago10:Pago FechaPago=\"2018-05-04T18:19:14\" FormaDePagoP=\"28\" MonedaP=\"MXN\" Monto=\"80.00\" NumOperacion=\"01\"><pago10:DoctoRelacionado IdDocumento=\"C45F7FD2-D973-4C6D-84F2-FED6F108B6FC\" MonedaDR=\"MXN\" MetodoDePagoDR=\"PPD\" ImpPagado=\"80.00\" /></pago10:Pago></pago10:Pagos></Complemento></Comprobante>" ;
            string timbrado = cfdi.Timbrar33(cfd, false, "PADE", "CEA990831FI8"); // "timbrado" es la variable si requieren el xml en texto plano
            MessageBox.Show(this, timbrado, "RepoBox");
        }
    }
}
