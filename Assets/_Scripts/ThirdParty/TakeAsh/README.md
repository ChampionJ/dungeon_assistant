# cs-SerializableDictionary

## XML Serializable Dictionary Class

* based on [c# - Serialize Class containing Dictionary member - Stack Overflow](http://stackoverflow.com/questions/495647/).
* When the key and/or the value can be converted from string, the key/value are serialized not as elements but as attributes.
* Example1) SerializableDictionary&lt;double, string&gt;
```XML
<?xml version="1.0" encoding="utf-8"?>
<SerializableDictionary key_type="Double" value_type="String">
  <item key="0.1" value="Zero" />
  <item key="1.2" value="One" />
  <item key="2.3" value="Two" />
</SerializableDictionary>
```
* Example2) SerializableDictionary&lt;int, MyData2&gt; (MyData2 can convert from/to string.)
```XML
<?xml version="1.0" encoding="utf-8"?>
<SerializableDictionary key_type="Int32" value_type="MyData2">
  <item key="0" value="{ID:'0', Name:'Zero', RegisteredDate:'2014/10/20 01:59:26', Height:'170', }" />
  <item key="1" value="{ID:'1', Name:'One', RegisteredDate:'2014/10/20 01:59:26', Height:'160', }" />
  <item key="2" value="{ID:'2', Name:'Two', RegisteredDate:'2014/10/20 01:59:26', Height:'165', }" />
</SerializableDictionary>
```

## XML Listable Dictionary Class

* When the class has "getKey" method, exchange for a Dictionary with a List.
* Example3) ListableDictionary&lt;Positions, MyData5&gt; (Positions is Enum. MyData5 has "getKey" method.)
```XML
<?xml version="1.0" encoding="utf-8"?>
<MyData5s KeyType="Positions" Count="4">
  <MyData5 Name="Alpha" Position="Floor1" HitPoint="100">
    <Weapon Name="Club" Owner="Alpha" />
  </MyData5>
  <MyData5 Name="Bravo" Position="Floor3" HitPoint="200">
    <Weapon Name="Mace" Owner="Bravo" />
  </MyData5>
  <MyData5 Name="Charlie" Position="Floor4" HitPoint="300">
    <Weapon Name="Hammer" Owner="Charlie" />
  </MyData5>
  <MyData5 Name="Delta" Position="FloorB1" HitPoint="500">
    <Weapon Name="Sword" Owner="Delta" />
  </MyData5>
</MyData5s>
```
