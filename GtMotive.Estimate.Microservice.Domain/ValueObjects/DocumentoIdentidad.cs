using System;

namespace GtMotive.Estimate.Microservice.Domain.ValueObjects
{
    /// <summary>
    /// Representa un documento de identidad.
    /// </summary>
    public class DocumentoIdentidad
    {
        /// <summary>
        /// Gets el valor del documento de identidad.
        /// </summary>
        public string Valor { get; }

        /// <summary>
        /// Gets el tipo de documento de identidad.
        /// </summary>
        public TipoDocumento Tipo { get; }

        // Constructor privado para serialización/EF si es necesario
        private DocumentoIdentidad()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="DocumentoIdentidad"/> class.
        /// </summary>
        /// <param name="valor">El valor del documento de identidad.</param>
        /// <exception cref="DomainException">Se lanza cuando el valor del documento de identidad está vacío o no es válido.</exception>
        public DocumentoIdentidad(string valor)
        {
            if (string.IsNullOrWhiteSpace(valor))
            {
                throw new DomainException("El documento de identidad no puede estar vacío.");
            }

            // Podrías tener un método para detectar el tipo de documento:
            Tipo = DetectarTipoDocumento(valor);

            // Podrías tener distintas validaciones por tipo (DNI, NIE, Pasaporte, etc.)
            ValidarSegunTipo(valor, Tipo);

            Valor = valor;
        }

        private static TipoDocumento DetectarTipoDocumento(string valor)
        {
            // Lógica simple de ejemplo (solo para ilustrar):
            // - DNI: 8 números + 1 letra
            // - NIE: comienza con X, Y o Z, etc.
            // - Pasaporte: ???
            // Retorna TipoDocumento correspondiente o lanza excepción si no encaja en ningún tipo
            if (EsDni(valor))
            {
                return TipoDocumento.DNI;
            }

            if (EsNie(valor))
            {
                return TipoDocumento.NIE;
            }

            if (EsPasaporte(valor))
            {
                return TipoDocumento.Pasaporte;
            }

            throw new DomainException($"El documento '{valor}' no se corresponde con un tipo válido.");
        }

        private static void ValidarSegunTipo(string valor, TipoDocumento tipo)
        {
            // Ejemplo:
            switch (tipo)
            {
                case TipoDocumento.DNI:
                    // Valida la longitud, formato, letra final, etc.
                    if (!EsValidoDni(valor))
                    {
                        throw new DomainException($"El DNI '{valor}' no es válido.");
                    }
                    break;
                case TipoDocumento.NIE:
                    // Reglas NIE
                    break;
                case TipoDocumento.Pasaporte:
                    // Reglas pasaporte
                    break;
                default:
                    throw new DomainException($"Tipo de documento '{tipo}' no gestionado.");
            }
        }

        // Estos métodos "EsDni", "EsNie", etc. podrían estar en otra clase helper para no ensuciar el VO
        private static bool EsDni(string valor) => /* Lógica de detección... */ false;
        private static bool EsNie(string valor) => /* Lógica de detección... */ false;
        private static bool EsPasaporte(string valor) => /* Lógica... */ false;
        private static bool EsValidoDni(string valor) => /* Lógica... */ false;

        /// <summary>
        /// Determina si el objeto especificado es igual al objeto actual.
        /// </summary>
        /// <param name="obj">El objeto a comparar con el objeto actual.</param>
        /// <returns>true si el objeto especificado es igual al objeto actual; de lo contrario, false.</returns>
        public override bool Equals(object obj)
        {
            if (obj is not DocumentoIdentidad other)
            {
                return false;
            }
            return Valor.Equals(other.Valor, StringComparison.OrdinalIgnoreCase);
        }

        /// <summary>
        /// Sirve como la función hash predeterminada.
        /// </summary>
        /// <returns>Un código hash para el objeto actual.</returns>
        public override int GetHashCode() => Valor.ToUpperInvariant().GetHashCode(StringComparison.OrdinalIgnoreCase);
    }

    /// <summary>
    /// Enumera los tipos de documentos de identidad.
    /// </summary>
    public enum TipoDocumento
    {
        /// <summary>
        /// Documento Nacional de Identidad.
        /// </summary>
        DNI,

        /// <summary>
        /// Número de Identidad de Extranjero.
        /// </summary>
        NIE,

        /// <summary>
        /// Pasaporte.
        /// </summary>
        Pasaporte
    }
}
