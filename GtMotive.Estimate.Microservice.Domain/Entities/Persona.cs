using System;

namespace GtMotive.Estimate.Microservice.Domain.Entities
{
    /// <summary>
    /// Representa una persona en el sistema.
    /// </summary>
    public class Persona
    {
        /// <summary>
        /// Gets the unique identifier of the person.
        /// </summary>
        public Guid Id { get; private set; }

        /// <summary>
        /// Gets the name of the person.
        /// </summary>
        public string Nombre { get; private set; }

        /// <summary>
        /// Gets the last name of the person.
        /// </summary>
        public string Apellidos { get; private set; }

        /// <summary>
        /// Gets the identity document of the person.
        /// </summary>
        public string DocumentoIdentidad { get; private set; }

        /// <summary>
        /// Gets a value indicating whether the person has an active rental.
        /// </summary>
        public bool TieneAlquilerActivo { get; private set; }

        // Constructor privado para EF o serialización
        private Persona()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Persona"/> class.
        /// </summary>
        /// <param name="id">The unique identifier of the person.</param>
        /// <param name="nombre">The name of the person.</param>
        /// <param name="apellidos">The last name of the person.</param>
        /// <param name="documentoIdentidad">The identity document of the person.</param>
        /// <exception cref="DomainException">Thrown when the name or identity document is empty.</exception>
        public Persona(Guid id, string nombre, string apellidos, string documentoIdentidad)
        {
            if (string.IsNullOrWhiteSpace(nombre))
            {
                throw new DomainException("El nombre no puede estar vacío.");
            }

            if (string.IsNullOrWhiteSpace(documentoIdentidad))
            {
                throw new DomainException("El documento de identidad no puede estar vacío.");
            }

            Id = id;
            Nombre = nombre;
            Apellidos = apellidos;
            DocumentoIdentidad = documentoIdentidad;
            TieneAlquilerActivo = false;
        }

        /// <summary>
        /// Marks the person as renting a vehicle.
        /// </summary>
        /// <exception cref="DomainException">Thrown when the person already has a rented vehicle.</exception>
        public void MarcarComoAlquilandoVehiculo()
        {
            if (TieneAlquilerActivo)
            {
                throw new DomainException("La persona ya tiene un vehículo alquilado.");
            }

            TieneAlquilerActivo = true;
        }

        /// <summary>
        /// Marks the person as not having an active rental.
        /// </summary>
        public void MarcarComoSinAlquiler()
        {
            TieneAlquilerActivo = false;
        }
    }
}
