using System;

namespace GtMotive.Estimate.Microservice.Domain.Entities
{
    public class Persona
    {
        public Guid Id { get; private set; }
        public string Nombre { get; private set; }
        public string Apellidos { get; private set; }
        // Si quieres un DNI o identificador, etc.
        public string DocumentoIdentidad { get; private set; }

        // Quizás un flag para saber si tiene alquiler activo,
        // o podrías llevar un listado de vehículos alquilados
        // si se permite que más adelante pueda tener más de uno (con restricciones).
        public bool TieneAlquilerActivo { get; private set; }

        // Constructor privado para EF o serialización
        private Persona()
        {
        }

        // Constructor público con validaciones de dominio
        public Persona(Guid id, string nombre, string apellidos, string documentoIdentidad)
        {
            if (string.IsNullOrWhiteSpace(nombre))
            {
                throw new DomainException("El nombre no puede estar vacío.");
            }
            if (string.IsNullOrWhiteSpace(documentoIdentidad))
                throw new DomainException("El documento de identidad no puede estar vacío.");

            Id = id;
            Nombre = nombre;
            Apellidos = apellidos;
            DocumentoIdentidad = documentoIdentidad;
            TieneAlquilerActivo = false;
        }

        // Ejemplo de método de dominio:
        public void MarcarComoAlquilandoVehiculo()
        {
            if (TieneAlquilerActivo)
                throw new DomainException("La persona ya tiene un vehículo alquilado.");

            TieneAlquilerActivo = true;
        }

        public void MarcarComoSinAlquiler()
        {
            TieneAlquilerActivo = false;
        }
    }
}
}
