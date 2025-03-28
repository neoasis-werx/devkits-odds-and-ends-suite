namespace DevKits.OddsAndEnds.Core.Collections;

using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;

[XmlRoot("Dictionary")]
public class SerializableDictionary<TKey, TValue> : Dictionary<TKey, TValue>, IXmlSerializable where TKey : notnull
{
    /// <summary>
    ///     Initializes a new instance of the <see cref="Dictionary{K,V}" /> class that is
    ///     empty, has the default initial capacity, and uses the default equality comparer for the key type.
    /// </summary>
    public SerializableDictionary()
    {
    }

    /// <summary>
    ///     Initializes a new instance of the <see cref="Dictionary{K,V}" /> class that contains
    ///     elements copied from the specified <see cref="IDictionary{K,V}" /> and uses the default
    ///     equality comparer for the key type.
    /// </summary>
    /// <param name="dictionary">
    ///     The <see cref="IDictionary{K,V}" /> whose elements are copied to the
    ///     new <see cref="Dictionary{K,V}" />.
    /// </param>
    /// <exception cref="System.ArgumentNullException">
    ///     <paramref name="dictionary" /> is <see langword="null" />.
    /// </exception>
    /// <exception cref="System.ArgumentException">
    ///     <paramref name="dictionary" /> contains one or more duplicate keys.
    /// </exception>
    public SerializableDictionary(IDictionary<TKey, TValue> dictionary) : base(dictionary)
    {
    }

    /// <summary>
    ///     Initializes a new instance of the <see cref="Dictionary{K,V}" /> class that contains
    ///     elements copied from the specified <see cref="IDictionary{K,V}" /> and uses the specified
    ///     <see cref="IEqualityComparer{T}" />.
    /// </summary>
    /// <param name="dictionary">
    ///     The <see cref="IDictionary{K,V}" /> whose elements are copied to the
    ///     new <see cref="Dictionary{K,V}" />.
    /// </param>
    /// <param name="comparer">
    ///     The <see cref="IEqualityComparer{T}" /> implementation to use when
    ///     comparing keys, or <see langword="null" /> to use the default
    ///     <see cref="EqualityComparer{T}" /> for the type of the key.
    /// </param>
    /// <exception cref="System.ArgumentNullException">
    ///     <paramref name="dictionary" /> is <see langword="null" />.
    /// </exception>
    /// <exception cref="System.ArgumentException">
    ///     <paramref name="dictionary" /> contains one or more duplicate keys.
    /// </exception>
    public SerializableDictionary(IDictionary<TKey, TValue> dictionary, IEqualityComparer<TKey>? comparer) : base(dictionary, comparer)
    {
    }

    /// <summary>
    ///     Initializes a new instance of the <see cref="Dictionary{K,V}" /> class that contains
    ///     elements copied from the specified <see cref="IEnumerable{T}" />.
    /// </summary>
    /// <param name="collection">
    ///     The <see cref="IEnumerable{T}" />  whose elements are copied to
    ///     the new <see cref="Dictionary{K,V}" />.
    /// </param>
    /// <exception cref="System.ArgumentNullException">
    ///     <paramref name="collection" /> is <see langword="null" />.
    /// </exception>
    /// <exception cref="System.ArgumentException">
    ///     <paramref name="collection" /> contains one or more duplicated keys.
    /// </exception>
    public SerializableDictionary(IEnumerable<KeyValuePair<TKey, TValue>> collection) : base(collection)
    {
    }

    /// <summary>
    ///     Initializes a new instance of the <see cref="Dictionary{K,V}" /> class that contains
    ///     elements copied from the specified <see cref="IEnumerable{T}" /> and uses the specified
    ///     <see cref="IEqualityComparer{T}" />.
    /// </summary>
    /// <param name="collection">
    ///     The <see cref="IEnumerable{T}" /> whose elements are copied to the
    ///     new <see cref="Dictionary{K,V}" />.
    /// </param>
    /// <param name="comparer">
    ///     The <see cref="IEqualityComparer{T}" /> implementation to use when
    ///     comparing keys, or <see langword="null" /> to use the default
    ///     <see cref="EqualityComparer{T}" /> for the type of the key.
    /// </param>
    /// <exception cref="System.ArgumentNullException">
    ///     <paramref name="collection" /> is <see langword="null" />.
    /// </exception>
    /// <exception cref="System.ArgumentException">
    ///     <paramref name="collection" /> contains one or more duplicated keys.
    /// </exception>
    public SerializableDictionary(IEnumerable<KeyValuePair<TKey, TValue>> collection, IEqualityComparer<TKey>? comparer) : base(collection, comparer)
    {
    }

    /// <summary>
    ///     Initializes a new instance of the <see cref="Dictionary{K,V}" /> class that is
    ///     empty, has the default initial capacity, and uses the specified
    ///     <see cref="System.Collections.Generic.IEqualityComparer{T}" />.
    /// </summary>
    /// <param name="comparer">
    ///     The <see cref="System.Collections.Generic.IEqualityComparer{T}" /> implementation to use when
    ///     comparing keys, or <see langword="null" /> to use the default
    ///     <see cref="System.Collections.Generic.EqualityComparer{T}" /> for the type of the key.
    /// </param>
    public SerializableDictionary(IEqualityComparer<TKey>? comparer) : base(comparer)
    {
    }

    /// <summary>
    ///     Initializes a new instance of the <see cref="Dictionary{K,V}" /> class that is
    ///     empty, has the specified initial capacity, and uses the default equality comparer for the key type.
    /// </summary>
    /// <param name="capacity">
    ///     The initial number of elements that the <see cref="Dictionary{K,V}" />
    ///     can contain.
    /// </param>
    /// <exception cref="System.ArgumentOutOfRangeException">
    ///     <paramref name="capacity" /> is less than 0.
    /// </exception>
    public SerializableDictionary(int capacity) : base(capacity)
    {
    }

    /// <summary>
    ///     Initializes a new instance of the <see cref="Dictionary{K,V}" /> class that is
    ///     empty, has the specified initial capacity, and uses the specified
    ///     <see cref="System.Collections.Generic.IEqualityComparer{T}" />.
    /// </summary>
    /// <param name="capacity">
    ///     The initial number of elements that the <see cref="Dictionary{K,V}" />
    ///     can contain.
    /// </param>
    /// <param name="comparer">
    ///     The <see cref="System.Collections.Generic.IEqualityComparer{T}" /> implementation to use when
    ///     comparing keys, or <see langword="null" /> to use the default
    ///     <see cref="System.Collections.Generic.EqualityComparer{T}" /> for the type of the key.
    /// </param>
    /// <exception cref="System.ArgumentOutOfRangeException">
    ///     <paramref name="capacity" /> is less than 0.
    /// </exception>
    public SerializableDictionary(int capacity, IEqualityComparer<TKey>? comparer) : base(capacity, comparer)
    {
    }


    public XmlSchema GetSchema()
    {
        return null!;
    }

    public void ReadXml(XmlReader reader)
    {
        var serializer = new XmlSerializer(typeof(Item));
        reader.ReadStartElement(); // <Dictionary>
        while (reader.NodeType != XmlNodeType.EndElement)
        {
            var item = (Item?)serializer.Deserialize(reader);
            if (item is not null)
                Add(item.Key, item.Value);
        }

        reader.ReadEndElement(); // </Dictionary>
    }

    public void WriteXml(XmlWriter writer)
    {
        var serializer = new XmlSerializer(typeof(Item));
        foreach (var kv in this)
        {
            var item = new Item { Key = kv.Key, Value = kv.Value };
            serializer.Serialize(writer, item);
        }
    }

    public class Item
    {
        public TKey Key { get; set; } = default!;
        public TValue Value { get; set; } = default!;
    }
}