using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RepoBox33;

namespace RepoboxDocs
{
    class CFDIv33
    {
        public void facturar()
        {
            DateTime inicio = DateTime.Now;
            bool SAT = false;
            // Los parametros necesarios son 1. Ruta del .Cer, 2. Ruta del .Key, 3. Contraseña del archivo .Key
            //RepoBox33.Comprobante factura = new RepoBox33.Comprobante(@"C:\RepoBox\NominaElectronica\Config\00001000000302316116.cer", @"C:\RepoBox\NominaElectronica\Config\CSD_matriz_SFI950101DU2_20140109_115049.key", "cristobal2012");
            RepoBox33.Comprobante factura = new RepoBox33.Comprobante(@"Ruta del archivo *.cer", @"Ruta del archivo *.key", "Password del archivo *.Key");
            factura.Version = "3.3";
            factura.Serie = "H";
            factura.Folio = "131355";// DateTime.Now.Hour.ToString("00") + DateTime.Now.Minute.ToString("00") + DateTime.Now.Second.ToString("00");
            factura._Fecha = DateTime.Now;
            factura.FormaPago = "verificar en catalogo de formas de pago SAT";
            factura.CondicionesDePago = "PUE"; // tres opciones PUE (pago en una sola exhibición), PIP (Pago inicial y parcialidades) PPD (Pago en parcialidades o diferido)
            factura.SubTotal = 10;
            factura.Descuento = 0;
            factura.Moneda = "MXN";
            factura.TipoCambio = 1;
            factura.Total = (decimal)11.60;
            factura.TipoDeComprobante = "I"; // En caso de ser factura, siempre será I
            factura.MetodoPago = "verificar en catalogo de metodos de pago SAT";
            factura.LugarExpedicion = "85000";
            //La forma de asignar valores a los objetos queda a su criterio.
            factura.Emisor = new RepoBox33.Emisor()
            {
                Rfc = "RFC CEA", 
                Nombre = "Nombre o Razón Social",
                RegimenFiscal = "603" // Clave del catalogo de SAT para Regimenes fiscales.
            };

            // Al igual que el Emisor, puede asignar valores del Receptor a su criterio.
            factura.Receptor = new RepoBox33.Receptor()
            {
                Rfc = "rfc del receptor (cliente)",
                Nombre = "Nombre o razón social del cliente.",
                UsoCFDI = "P01" // Clave del Cataloo de SAT para usos de CFDI.
            };
            // Los conceptos (productos y/o servicios) se agregna de la siguiente forma o de igual manera queda a su criterio.
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
            concepto.Impuestos.Traslados.Add(new RepoBox33.CI_Traslado() { Base = concepto.Importe, Impuesto = "002", TasaOCuota = (decimal)0.16, TipoFactor = "Tasa", Importe = concepto.Importe * (decimal)0.16 });
            factura.Conceptos.Add(concepto);

            C_Impuestos impuestos = factura.ListadoImpuestos();
            if (impuestos != null)
            {
                factura.Impuestos = new RepoBox33.Impuestos();
                if ((impuestos.trasladoIEPS + impuestos.trasladoIVA) > 0)
                {
                    factura.Impuestos.Traslados = new List<RepoBox33.Traslado>();
                    if (impuestos.trasladoIVA > 0)
                        factura.Impuestos.Traslados.Add(new RepoBox33.Traslado() { Importe = impuestos.trasladoIVA, Impuesto = "002", TasaOCuota = (decimal)0.16, TipoFactor = "Tasa" });
                    if (impuestos.trasladoIEPS > 0)
                        factura.Impuestos.Traslados.Add(new RepoBox33.Traslado() { Importe = impuestos.trasladoIEPS, Impuesto = "003", TasaOCuota = (decimal)0.16, TipoFactor = "Tasa" });
                }
                if ((impuestos.retencionISR + impuestos.retencionIVA) > 0)
                {
                    factura.Impuestos.Retenciones = new List<RepoBox33.Retencion>();
                    if (impuestos.retencionIVA > 0)
                        factura.Impuestos.Retenciones.Add(new RepoBox33.Retencion() { Importe = impuestos.retencionIVA, Impuesto = "002" });
                    if (impuestos.retencionISR > 0)
                        factura.Impuestos.Retenciones.Add(new RepoBox33.Retencion() { Importe = impuestos.retencionISR, Impuesto = "001" });
                }
            }
            // La utilización de este metodo tambien es obligatorio, asi nos evitamos realizar la sumatoria manual de los impuestos
            factura.Impuestos.CalcularTotales();
            // De cualquier forma, si desean asignar de forma manual los valores de total de impuestos pueden hacerlo con las siguientes lineas.
            //factura.Impuestos.totalImpuestosRetenidos = 0;
            //factura.Impuestos.totalImpuestosTrasladados = 0;
            factura.Banco = "Banorte 20202020202";
            factura.Observaciones = "Ahora si hay observaciones";
            // Lo siguiente es generar el archivo CFD (xml sin timbrar)
            string cfd = factura.GenerarCFD();
            // Si la respuesta del método 
            if (!cfd.StartsWith("RepoBox"))
            {
                RepoBox.CFDIv32 cfdi = new RepoBox.CFDIv32();
                bool enviado = false;
                // Timbrado del CFD
                string timbrado = cfdi.Timbrar32(cfd, SAT, "PADE", factura.Emisor.Rfc); // "timbrado" es la variable si requieren el xml en texto plano
                if (!timbrado.ToUpper().StartsWith("REPOBOX"))
                {
                    string xmlPath = factura.SaveXml(ref timbrado); // "xmlPath" es la variable que regresa la ruta donde se guardó el XML, pero el metodo es para guardar el xml en archivo.
                    // Los parametros que acepta GenerarPDF 1. xml en texto plano (ya timbrado), 2. Ruta del logo a mostrar en la factura, 3. Ruta y nombre con el que se guardará el PDF.
                    string pdfPath = cfdi.GenerarPDF(timbrado, @"C:\Repobox\Timbrado\RepoBox.jpg", xmlPath.Replace(".XML", ""));

                    // En caso de necesitar regenerar el archivo PDF, existe el siguiente método
                    // Parametros: 1. Ruta del archivo XML, 2. Ruta de logo a mostrar en la factura. El sistema nombrará al PDF igual que el XML.
                    //pdfPath = cfdi.GenerarPDF_PathFile(xmlPath, @"C:\Repobox\Timbrado\RepoBox.jpg");
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
                        Destinatarios = "samuel_arreola@msn.com;samuel.arreola@repobox.com.mx"
                    });
                }
                //if (enviado)
                //    //"Enviado Correctamente";
                //else
                //    //"No enviado."
            }
            else
            {
                //cfd --Es el mensaje de error;
            }
        }
    }
}
