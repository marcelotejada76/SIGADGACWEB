﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaModelo
{
   public class tbSolicitudPOA
    {
        public string AnioSolicitud { get; set; }
        public Int32 NumeroSolicitud { get; set; }
        public string FechaSolicitud { get; set; }
        public string TipoSolicitud { get; set; }
        public string DescripcionTipoSolicitud { get; set; }
        public string EstadoSolicitud { get; set; }
        public string EstadoAutorizacion { get; set; }
        public string EstadoTramiteSolicitud { get; set; }
        public string EstadoVerificacionFinanciera { get; set; }
        public string FechaRevision { get; set; }
        public string FechaAprobacion { get; set; }
        public Int32 SecuenciaActividad { get; set; }
        public string Observaciones { get; set; }
        public string Observaciones2 { get; set; }
        public string Observaciones6 { get; set; }
        public string ObservacionRevision1 { get; set; }
        public string ObservacionRevision2 { get; set; }
        public string ObservacionAutorizacion1 { get; set; }
        public string ObservacionAutorizacion2 { get; set; }
        public decimal NumeroCUR { get; set; }
        public string FechaCUR { get; set; }
        public Int32 NumeroModificacion { get; set; }
        public string FechaAutualizaModificacion { get; set; }
        public string UsuarioCreaAnalista { get; set; }
        public string FechaCreaAnalista { get; set; }
        public string HoraCreaAnalista { get; set; }
        public string DispositivoCreaAnalista { get; set; }
        public string UsuarioDirectorAera { get; set; }
        public string UsuarioCreacionPGE { get; set; }
        public string FechaCreacionPGE { get; set; }
        public string HoraCreacionPGE { get; set; }
        public string DispositivoCreaPGE { get; set; }
        public string UsuarioCreacionFIN_PRES { get; set; }
        public string FechaCreacionFIN_PRES { get; set; }
        public string HoraCreacionFIN_PRES { get; set; }
        public string DispositivoCreaFIN_PRES { get; set; }
        public string EstadoActualizacionPOA { get; set; }
        public string ObservacionVerificacionPresupuesto1 { get; set; }
        public string ObservacionVerificacionPresupuesto2 { get; set; }
        public string CodigoUnidadEjecucion { get; set; }
        public string CodigoDireccionPYGE { get; set; }
        public string CodigoRolPYGE { get; set; }
        public Int32 NumeroCertificadoPOA { get; set; }
        public Int32 SecuencialActualizacion { get; set; }
        public Int32 numeroDocumentoAdjunto { get; set; }
        public decimal ValOrigen { get; set; }
        public decimal ValDestino { get; set; }
        public string DescripcionActividadEjecutar { get; set; }

        public List<tbModelArchivo> oModelArchivo { get; set; }

    }
}
