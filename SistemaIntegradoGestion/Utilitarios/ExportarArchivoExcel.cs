using System;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using ClosedXML.Excel;
using OfficeOpenXml;
using OfficeOpenXml.Table;

namespace SistemaIntegradoGestion.Utilitarios
{
    public class ExportarArchivoExcel
    {
        public static ExportarArchivoExcel _instancia = null;
        private ExportarArchivoExcel()
        {

        }

        public static ExportarArchivoExcel Instancia
        {
            get
            {
                if (_instancia == null)
                {
                    _instancia = new ExportarArchivoExcel();
                }
                return _instancia;
            }
        }

        //public bool ExportExcel(DataSet dsPoaPropuesto, string archivoPath)
        //{
        //    bool estado = false;
            
        //    using (ExcelPackage pck = new ExcelPackage())
        //    {
        //        ExcelWorksheet ws = pck.Workbook.Worksheets.Add("SearchReport");
        //        ws.Cells["A1"].LoadFromDataTable(dsPoaPropuesto.Tables[0], true, TableStyles.Medium15); //You can Use TableStyles property of your desire.    
        //                                                                                                //Read the Excel file in a byte array    
        //        Byte[] fileBytes = pck.GetAsByteArray();
        //        HttpContext.Current.Response.ClearContent();
        //        HttpContext.Current.Response.AddHeader("content-disposition", "attachment;filename=" + archivoPath);
        //        HttpContext.Current.Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
        //        HttpContext.Current.Response.BinaryWrite(fileBytes);
        //        HttpContext.Current.Response.End();
        //        estado = true;
        //    }
        //    return estado;
        //}

        public void ExportDataTableToExcel(DataSet dsPoaPropuesto, string archivoPath)
        {
            using (var workbook = new XLWorkbook())
            {
                // Agregar una hoja al libro de trabajo
                var worksheet = workbook.Worksheets.Add("Sheet1");

                // Copiar los datos del DataSet a la hoja de cálculo
                worksheet.Cell(1, 1).InsertTable(dsPoaPropuesto.Tables[0]);

                // Guardar el libro de trabajo en un archivo
                workbook.SaveAs(archivoPath);
            }
        }


        public void ExportDataTableToCSV(DataSet dsPoaPropuesto, string archivoPath)
        {
            // Configuración de la codificación UTF-8 sin firma (BOM)
            var utf8WithoutBom = new System.Text.UTF8Encoding(false);

            // Escribir los datos del DataTable en el archivo CSV
            using (StreamWriter writer = new StreamWriter(archivoPath, false, utf8WithoutBom))
            {
                // Escribir encabezados de columna
                foreach (DataColumn column in dsPoaPropuesto.Tables[0].Columns)
                {
                    writer.Write(column.ColumnName);
                    if (column != dsPoaPropuesto.Tables[0].Columns[dsPoaPropuesto.Tables[0].Columns.Count - 1])
                    {
                        writer.Write(",");
                    }
                }
                writer.WriteLine();

                // Escribir datos de filas
                foreach (DataRow row in dsPoaPropuesto.Tables[0].Rows)
                {
                    for (int i = 0; i < dsPoaPropuesto.Tables[0].Columns.Count; i++)
                    {
                        writer.Write(row[i].ToString());
                        if (i != dsPoaPropuesto.Tables[0].Columns.Count - 1)
                        {
                            writer.Write(",");
                        }
                    }
                    writer.WriteLine();
                }
            }
        }
    }

    internal class ExcelPackage
    {
    }
}