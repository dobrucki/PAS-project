using System.Runtime.CompilerServices;

[assembly:InternalsVisibleTo("UnitTests")]
namespace PAS_project.Models.Entities
{
    public abstract class Entity
    {
        public virtual int Id { get; protected internal set; }
    }
}