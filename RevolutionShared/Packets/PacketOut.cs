using RevolutionCore.Utils;
using RevolutionShared.Packets;
using RevolutionShared.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RevolutionShared.Networking.Packets
{
    /// <summary>
    /// Packet template.
    /// </summary>
    public class PacketOut : IPacket
    {
        public List<byte> buffer;

        private byte[] convertBuffer = new byte[4]; // This is used to avoid allocating everytime a small array for converting the size

        public const int DefaultCapacity = 4096;
        /// <summary>
        /// Offset of the size.
        /// </summary>
        static public readonly int SizeLength = 4;
        /// <summary>
        /// Offset of the flag.
        /// </summary>
        static public readonly int FlagLength = 2;
        /// <summary>
        /// Offset of the header.
        /// </summary>
        static public readonly int HeaderLength = SizeLength + FlagLength;

        /// <summary>
        /// Constructor.
        /// </summary>
        public PacketOut()
        {
            buffer = new List<byte>(DefaultCapacity) { 0, 0, 0, 0, 0, 0 };
        }

        /// <summary>
        /// Constructor with command.
        /// </summary>
        /// <param name="command">Command.</param>
        public PacketOut(short command)
        {
            var bytes = BitConverter.GetBytes(command);

            buffer = new List<byte>(DefaultCapacity) { 0, 0, 0, 0 };

            buffer.AddRange(bytes);
        }

        /// <summary>
        /// Constructor with server command.
        /// </summary>
        /// <param name="command">Server command.</param>
        public PacketOut(ServerCommands command) : this((short)command)
        {

        }

        /// <summary>
        /// Constructor with client command.
        /// </summary>
        /// <param name="command">Client command.</param>
        public PacketOut(ClientCommands command) : this((short)command)
        {

        }

        /// <summary>
        /// Constructor with command.
        /// </summary>
        /// <param name="capacity">Capacity.</param>
        /// <param name="command">Command.</param>
        public PacketOut(int capacity, short command)
        {
            var bytes = BitConverter.GetBytes(command);

            buffer = new List<byte>(capacity) { 0, 0, 0, 0 };

            buffer.AddRange(bytes);
        }

        /// <summary>
        /// Constructor for taking an existing packet.
        /// </summary>
        /// <param name="packet">Packet.</param>
        public PacketOut(PacketIn packet)
        {
            buffer = new List<byte>(packet.Buffer);
        }

        /// <summary>
        /// Add a byte value.
        /// </summary>
        /// <param name="value">Value.</param>
        public void Add(byte value)
        {
            AddByte(value);
        }

        /// <summary>
        /// Add a char value.
        /// </summary>
        /// <param name="value">Value.</param>
        public void Add(char value)
        {
            Add(BitConverter.GetBytes(value));
        }

        /// <summary>
        /// Add char values.
        /// </summary>
        /// <param name="values">Values.</param>
        public void Add(char[] values)
        {
            Add(values.Length);

            for (int i = 0; i < values.Length; i++)
            {
                Add(values[i]);
            }
        }

        /// <summary>
        /// Add an enumarable of char values.
        /// </summary>
        /// <param name="values">Values.</param>
        public void Add(IEnumerable<char> values)
        {
            Add(values.Count());

            for (int i = 0; i < values.Count(); i++)
            {
                Add(values.ElementAt(i));
            }
        }

        /// <summary>
        /// Add a bool value.
        /// </summary>
        /// <param name="value">Value.</param>
        public void Add(bool value)
        {
            Add(BitConverter.GetBytes(value));
        }

        /// <summary>
        /// Add bool values.
        /// </summary>
        /// <param name="value">Value.</param>
        public void Add(bool[] values)
        {
            Add(values.Length);

            for (int i = 0; i < values.Length; i++)
            {
                Add(values[i]);
            }
        }

        /// <summary>
        /// Add an enumarable of bool values.
        /// </summary>
        /// <param name="values">Values.</param>
        public void Add(IEnumerable<bool> values)
        {
            Add(values.Count());

            for (int i = 0; i < values.Count(); i++)
            {
                Add(values.ElementAt(i));
            }
        }

        /// <summary>
        /// Add bytes in the buffer.
        /// </summary>
        /// <param name="values">Value of the byte.</param>
        public void Add(byte[] values)
        {
            buffer.AddRange(values);

            RefreshSize();
        }

        /// <summary>
        /// Add a short value.
        /// </summary>
        /// <param name="value">Value.</param>
        public void Add(short value)
        {
            AddShort(value);
        }

        /// <summary>
        /// Add short values.
        /// </summary>
        /// <param name="value">Value.</param>
        public void Add(short[] values)
        {
            Add(values.Length);

            for (int i = 0; i < values.Length; i++)
            {
                Add(values[i]);
            }
        }

        /// <summary>
        /// Add an enumarable of short values.
        /// </summary>
        /// <param name="values">Values.</param>
        public void Add(IEnumerable<short> values)
        {
            Add(values.Count());

            for (int i = 0; i < values.Count(); i++)
            {
                Add(values.ElementAt(i));
            }
        }

        /// <summary>
        /// Add an int value.
        /// </summary>
        /// <param name="value">Value.</param>
        public void Add(int value)
        {
            AddInt(value);
        }

        /// <summary>
        /// Add int values.
        /// </summary>
        /// <param name="value">Value.</param>
        public void Add(int[] values)
        {
            Add(values.Length);

            for (int i = 0; i < values.Length; i++)
            {
                Add(values[i]);
            }
        }

        /// <summary>
        /// Add an enumarable of int values.
        /// </summary>
        /// <param name="values">Values.</param>
        public void Add(IEnumerable<int> values)
        {
            Add(values.Count());

            for (int i = 0; i < values.Count(); i++)
            {
                Add(values.ElementAt(i));
            }
        }

        /// <summary>
        /// Add an uint value.
        /// </summary>
        /// <param name="value">Value.</param>
        public void Add(uint value)
        {
            AddUInt(value);
        }

        /// <summary>
        /// Add a string value.
        /// </summary>
        /// <param name="value">Value.</param>
        public void Add(string value)
        {
            AddString(value);
        }

        /// <summary>
        /// Add a float value.
        /// </summary>
        /// <param name="value">Value.</param>
        public void Add(float value)
        {
            AddFloat(value);
        }

        /// <summary>
        /// Add float values.
        /// </summary>
        /// <param name="value">Value.</param>
        public void Add(float[] values)
        {
            Add(values.Length);

            for (int i = 0; i < values.Length; i++)
            {
                Add(values[i]);
            }
        }

        /// <summary>
        /// Add an enumarable of float values.
        /// </summary>
        /// <param name="values">Values.</param>
        public void Add(IEnumerable<float> values)
        {
            Add(values.Count());

            for (int i = 0; i < values.Count(); i++)
            {
                Add(values.ElementAt(i));
            }
        }

        /// <summary>
        /// Add a long value.
        /// </summary>
        /// <param name="value">Value.</param>
        public void Add(long value)
        {
            AddLong(value);
        }

        /// <summary>
        /// Add long values.
        /// </summary>
        /// <param name="value">Value.</param>
        public void Add(long[] values)
        {
            Add(values.Length);

            for (int i = 0; i < values.Length; i++)
            {
                Add(values[i]);
            }
        }

        /// <summary>
        /// Add an enumarable of long values.
        /// </summary>
        /// <param name="values">Values.</param>
        public void Add(IEnumerable<long> values)
        {
            Add(values.Count());

            for (int i = 0; i < values.Count(); i++)
            {
                Add(values.ElementAt(i));
            }
        }

        /// <summary>
        /// Add an array of strings.
        /// </summary>
        /// <param name="values">Values.</param>
        public void Add(string[] values)
        {
            Add(values.Length);

            for (int i = 0; i < values.Length; i++)
            {
                Add(values[i]);
            }
        }

        /// <summary>
        /// Add an array of strings.
        /// </summary>
        /// <param name="values">Values.</param>
        public void Add(IEnumerable<string> values)
        {
            Add(values.Count());

            for (int i = 0; i < values.Count(); i++)
            {
                Add(values.ElementAt(i));
            }
        }

        /// <summary>
        /// Add an enum.
        /// </summary>
        /// <typeparam name="T">Enum type.</typeparam>
        /// <param name="value">Value.</param>
        public void Add<T>(T value) where T : Enum
        {
            int enumInt = (int)(object)value;

            Add(enumInt);
        }

        /// <summary>
        /// Add a datetime object.
        /// </summary>
        /// <param name="dateTime">Date time.</param>
        public void Add(DateTime dateTime)
        {
            Add(dateTime.ToBinary());
        }

        /// <summary>
        /// Add an enumarable of Date time values.
        /// </summary>
        /// <param name="values">Values.</param>
        public void Add(IEnumerable<DateTime> values)
        {
            Add(values.Count());

            for (int i = 0; i < values.Count(); i++)
            {
                Add(values.ElementAt(i));
            }
        }

        /// <summary>
        /// Add a time span object.
        /// </summary>
        /// <param name="timeSpan">Time span.</param>
        public void Add(TimeSpan timeSpan)
        {
            Add(timeSpan.Ticks);
        }

        /// <summary>
        /// Add an enumarable of Time span values.
        /// </summary>
        /// <param name="values">Values.</param>
        public void Add(IEnumerable<TimeSpan> values)
        {
            Add(values.Count());

            for (int i = 0; i < values.Count(); i++)
            {
                Add(values.ElementAt(i));
            }
        }

        /// <summary>
        /// Add a writable.
        /// </summary>
        /// <param name="writable">Writable.</param>
        public void Add(IPacketWritable writable)
        {
            writable.WriteToPacket(this);
        }

        /// <summary>
        /// Add a writable struct object.
        /// </summary>
        /// <typeparam name="T">T</typeparam>
        /// <param name="writables">Writables.</param>
        public void Add<T>(IList<T> writables) where T : IPacketWritable
        {
            Add(writables.Count);

            for (int i = 0; i < writables.Count; i++)
            {
                var item = writables[i];

                item.WriteToPacket(this);
            }
        }

        /// <summary>
        /// Add a writable class object.
        /// </summary>
        /// <param name="writables">Writables.</param>
        public void Add<T>(T[] writables) where T : IPacketWritable
        {
            Add(writables.Length);

            for (int i = 0; i < writables.Length; i++)
            {
                writables[i].WriteToPacket(this);
            }
        }

        public void Add<T, V>(Dictionary<T, V> values) where V : IPacketWritable
        {
            Add(values.Count);

            foreach (var writable in values)
            {
                writable.Value.WriteToPacket(this);
            }
        }

        /// <summary>
        /// Add a byte in the buffer.
        /// </summary>
        /// <param name="value">Byte value.</param>
        public void AddByte(byte value)
        {
            buffer.Add(value);

            RefreshSize();
        }

        /// <summary>
        /// Add a short in the buffer.
        /// </summary>
        /// <param name="value">Short value.</param>
        public void AddShort(short value)
        {
            Add(BitConverter.GetBytes(value));
        }

        /// <summary>
        /// Add a short in the buffer.
        /// </summary>
        /// <param name="value">Short value.</param>
        public void AddShort(int value)
        {
            buffer.AddRange(BitConverter.GetBytes((short)value));
        }

        /// <summary>
        /// Add an int in the buffer.
        /// </summary>
        /// <param name="value">Short value.</param>
        public void AddInt(int value)
        {
            Add(BitConverter.GetBytes(value));
        }

        /// <summary>
        /// Add an int in the buffer.
        /// </summary>
        /// <param name="value">Short value.</param>
        public void AddUInt(uint value)
        {
            Add(BitConverter.GetBytes(value));
        }

        /// <summary>
        /// Add a string in the buffer.
        /// </summary>
        /// <param name="value">Short value.</param>
        public void AddString(string value)
        {
            if (!string.IsNullOrEmpty(value))
            {
                var data = Encoding.UTF8.GetBytes(value);

                Encoding.UTF8.GetCharCount(data);

                Add(data.Length);

                Add(data);
            }

            else
            {
                Add(0);
            }
        }

        /// <summary>
        /// Add a string in the buffer.
        /// </summary>
        /// <param name="value">Short value.</param>
        /// <param name="maximum">Maximum string length.</param>
        public void AddString(string value, int maximum)
        {
            if (!string.IsNullOrEmpty(value))
            {
                if (value.Length <= maximum)
                {
                    var data = Encoding.UTF8.GetBytes(value);

                    Add(data.Length);

                    Add(data);
                }

                else
                {
                    var data = Encoding.UTF8.GetBytes(value.Substring(0, maximum));

                    Add(maximum);

                    Add(data);
                }
            }

            else
            {
                Add(0);
            }
        }

        /// <summary>
        /// Add a float in the buffer.
        /// </summary>
        /// <param name="value">Float to add.</param>
        public void AddFloat(float value)
        {
            Add(BitConverter.GetBytes(value));
        }

        /// <summary>
        /// Add a long in the buffer.
        /// </summary>
        /// <param name="value">Long to add.</param>
        public void AddLong(long value)
        {
            Add(BitConverter.GetBytes(value));
        }

        /// <summary>
        /// Refresh the size of the packet.
        /// </summary>
        private void RefreshSize()
        {
            convertBuffer = BitConverter.GetBytes(buffer.Count - HeaderLength);

            buffer[0] = convertBuffer[0];
            buffer[1] = convertBuffer[1];
            buffer[2] = convertBuffer[2];
            buffer[3] = convertBuffer[3];
        }

        /// <summary>
        /// Packet in string format.
        /// </summary>
        /// <returns>String format.</returns>
        public override string ToString()
        {
            return StringFormat;
        }

        /// <summary>
        /// Get the size of the packet.
        /// </summary>
        public int Size { get { return Tools.Convert(buffer[0], buffer[1], buffer[2], buffer[3]); } }

        /// <summary>
        /// Get the command of the packet.
        /// </summary>
        public short Command { get { return Tools.Convert(buffer[4], buffer[5]); } }

        /// <summary>
        /// Get the buffer of the packet.
        /// </summary>
        public byte[] Buffer { get { return buffer.ToArray(); } }

        /// <summary>
        /// Get the packet in string decimal format.
        /// </summary>
        public string StringFormat { get { return Tools.GetPacketString(this); } }
    }
}
