using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaDatos
{
    class CamposUceo
    {
        //MOVIMIENTOS}
        public Int32 OIDAEROPUERTO;
        public Int32 OID;
        public Int32 OIDITEMAUTORIZACIO;
        public Int32 OIDTIPOVUELO;
        public Int32 OIDAEROVIA;
        public Int32 OIDLUGARATERRIZAJE;
        public string FECHAREAL;
        public string HORAREAL;
        public string OPERACION;

        public Int32 OIDAERONAVECOMPANI;
        public string FACTSER;
        public string FACTEST;
        public string ORIGEN;
        public string DESTINO;
        public Int32 OIDORIGEN;
        public Int32 OIDDESTINO;
        public Decimal TOTALMILLAS;
        public Int32 OIDSOLICITANTE;
        public string TIPOAUTORIZACION;
        public string NUMEROVUELO;
        public string FACTURAR;
        public string OBSERVACION;
        //PISTA
        //MOTOBOMBA
        //AUTORIZACION
        public Int32 OIDTIPOOPERACION;
        public string NVUELO;
        public string RUTAEROVIA;
        public DateTime FECHACREACION;
        public string USUARIOCREA;
        public string FECHACREA;


        public string callsign;
        public string registry;
        public string Archivo;
        public string fechaProceso;
        public string aeropuerto_operation;
        public string aeropuerto_destino;
        public string AUTORIZACION;
        public int NUMREG,PISTA;

        //deudor con matricula
        public DateTime fechadeudor;
        public string CEDULA_RUC;
        public string NOMBRECLIENTE;
        public int NUMEROFACTURA;
        //public string FECHA;
        public string FECHARECEPCION;
        //public string FECHAVENCIMIENTO;
        public Double VALORFACTURA;
        public Double SALDOVALOR;
        public string MATRICULALIMPIA;
        public string SOBREVUELO;
        public string FECHA;
        public string FECHAVENCIMIENTO;
        public decimal valor, saldo;
        public string MATRICULA;

        //liuidacion coactiva

        public string ProcedimientoCoacti, TituloCredito, TipoDocumento, Ruc, Nombrecia, FechaLiquidacion;

        public string Documento, Tipo, FechaEmision, FechaRecepcion, FechaVencimiento, FechaPago, TipoDcto;
        public decimal TotalMulta, TotalAjusteEconomi, Intereses, CostasCoactivas, Total, GestionCobro;

        public string ElaboradoPor, CargoElaborado, RevisadoPor, CargoRevisado, AprobadoPor, CargoAprobado, Año;


    }
}
