using System;
using System.Collections.Generic;
using System.Text;
using SQLite;

namespace BarCode.Tablas
{
    class T_Refacciones
    {
        [PrimaryKey,AutoIncrement]
        public int Id { get; set; }
        [MaxLength(255)]
        public string Maquina { get; set; }
        [MaxLength(255)]
        public string Nombre { get; set; }

        public double Cantidad { get; set; }
    }
}
