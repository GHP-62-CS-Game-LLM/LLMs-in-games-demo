using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using JetBrains.Annotations;
using Unity.VisualScripting;
using UnityEngine;

public interface IContext
{
    public string Type { get; }
    
    public void WriteJson(Utf8JsonWriter writer);
    
}

public class ObjectKinematicContext : IContext
{
    public string Type { get; } = "ObjectPosition";

    private readonly Transform _transform;
    private readonly Rigidbody _rigidbody;

    public ObjectKinematicContext(Transform transform, Rigidbody rigidbody)
    {
        _transform = transform;
        _rigidbody = rigidbody;
    }

    public void WriteJson(Utf8JsonWriter writer)
    {
        writer.WriteStartObject();
        // Position
        {
            writer.WriteStartObject("Position");
            writer.WriteNumber("x", _transform.position.x);
            writer.WriteNumber("y", _transform.position.y);
            writer.WriteNumber("z", _transform.position.z);
            writer.WriteEndObject();
        }
        // Rotation
        {
            writer.WriteStartObject("Rotation");
            writer.WriteNumber("x", _transform.rotation.x);
            writer.WriteNumber("y", _transform.rotation.y);
            writer.WriteNumber("z", _transform.rotation.z);
            writer.WriteNumber("w", _transform.rotation.w);
            writer.WriteEndObject();
        }
        // Linear Velocity
        {
            writer.WriteStartObject("LinearVelocity");
            writer.WriteNumber("x", _rigidbody.linearVelocity.x);
            writer.WriteNumber("y", _rigidbody.linearVelocity.y);
            writer.WriteNumber("z", _rigidbody.linearVelocity.z);
            writer.WriteEndObject();
        }
        // Angular Velocity
        {
            writer.WriteStartObject("AngularVelocity");
            writer.WriteNumber("x", _rigidbody.angularVelocity.x);
            writer.WriteNumber("y", _rigidbody.angularVelocity.y);
            writer.WriteNumber("z", _rigidbody.angularVelocity.z);
            writer.WriteEndObject();
        }
        writer.WriteEndObject();
    }
}

public class ObjectContext : IContext
{
    public string Type { get; } = "ObjectContext";
    
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;

    [CanBeNull] public ObjectKinematicContext ObjectKinematic = null;

    [CanBeNull]
    public Dictionary<string, IContext> Other = null;

    public void WriteJson(Utf8JsonWriter writer)
    {
        writer.WriteStartObject();
        writer.WriteString("Type", Type);
        writer.WriteString("Name", Name);
        writer.WriteString("Description", Description);

        if (ObjectKinematic is not null)
        {
            writer.WritePropertyName("KinematicState");
            ObjectKinematic.WriteJson(writer);
        }
        else writer.WriteNull("KinematicState");
        writer.WriteEndObject();
    }
}

