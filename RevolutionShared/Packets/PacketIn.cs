using RevolutionCore.Utils;
using RevolutionShared.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RevolutionShared.Networking.Packets
{
    public class PacketIn : IPacket
    {
        /// <summary>
        /// Size of a byte.
        /// </summary>
        public const int ByteSize = sizeof(byte);
        /// <summary>
        /// Size of a boolean.
        /// </summary>
        public const int BoolSize = sizeof(bool);
        /// <summary>
        /// Size of a short.
        /// </summary>
        public const int ShortSize = sizeof(short);
        /// <summary>
        /// Size of an int.
        /// </summary>
        public const int IntSize = sizeof(int);
        /// <summary>
        /// Size of a long.
        /// </summary>
        public const int LongSize = sizeof(long);
        /// <summary>
        /// Size of a float.
        /// </summary>
        public const int FloatSize = sizeof(float);
        /// <summary>
        /// Size of a double.
        /// </summary>
        public const int DoubleSize = sizeof(double);

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

        int position;

        ArraySegment<byte> segment;

        /// <summary>
        /// Parameterless constructor.
        /// </summary>
        public PacketIn()
        {
            segment = new ArraySegment<byte>();
        }

        /// <summary>
        /// Packet with starting command.
        /// </summary>
        /// <param name="command">Command.</param>
        public PacketIn(short command)
        {
            segment = new ArraySegment<byte>();

            Start(command);
        }

        /// <summary>
        /// Constructor.
        /// </summary>
        ///<param name="segment">Segment.</param>
        public PacketIn(ArraySegment<byte> segment)
        {
            this.segment = segment;
        }

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="buffer">Buffer.</param>
        public PacketIn(byte[] buffer)
        {
            this.segment = new ArraySegment<byte>(buffer);
        }

        /// <summary>
        /// Set the size of the packet.
        /// </summary>
        /// <param name="size">New size.</param>
        public void SetSize(int size)
        {
            var bytes = BitConverter.GetBytes(size);

            Buffer[0] = bytes[0];
            Buffer[1] = bytes[1];
            Buffer[2] = bytes[2];
            Buffer[3] = bytes[3];
        }

        /// <summary>
        /// Set the command of the packet.
        /// </summary>
        /// <param name="command">Command.</param>
        public void SetCommand(short command)
        {
            var bytes = BitConverter.GetBytes(command);

            Buffer[SizeLength] = bytes[0];
            Buffer[SizeLength + 1] = bytes[1];
        }

        /// <summary>
        /// Set the start of the packet.
        /// </summary>
        /// <param name="command">Command of the packet.</param>
        public void Start(short command)
        {
            //  SetSize(HeaderLength);
            SetCommand(command);
        }

        /// <summary>
        /// Set a byte in the buffer.
        /// </summary>
        /// <param name="position">Position of the byte.</param>
        /// <param name="value">Value of the byte.</param>
        public void SetByte(int position, byte value)
        {
            Buffer[position + HeaderLength] = value;
        }

        /// <summary>
        /// Set bytes in the buffer.
        /// </summary>
        /// <param name="position">Position of the byte.</param>
        /// <param name="values">Value of the byte.</param>
        public void SetBytes(int position, byte[] values)
        {
            for (int i = 0; i < values.Length; i++)
            {
                Buffer[position + HeaderLength + i] = values[i];
            }
        }

        /// <summary>
        /// Set a short in the buffer.
        /// </summary>
        /// <param name="position">Position.</param>
        /// <param name="value">Value.</param>
        public void SetShort(int position, short value)
        {
            byte[] bytes = BitConverter.GetBytes(value);

            SetBytes(position, bytes);
        }

        /// <summary>
        /// Set an int in the buffer.
        /// </summary>
        /// <param name="position">Position.</param>
        /// <param name="value">Value.</param>
        public void SetInt(int position, int value)
        {
            byte[] bytes = BitConverter.GetBytes(value);

            SetBytes(position, bytes);
        }

        /// <summary>
        /// Set a long in the buffer.
        /// </summary>
        /// <param name="position">Position.</param>
        /// <param name="value">Value.</param>
        public void SetLong(int position, long value)
        {
            byte[] bytes = BitConverter.GetBytes(value);

            SetBytes(position, bytes);
        }

        /// <summary>
        /// Set a float in the buffer.
        /// </summary>
        /// <param name="position">Position in the buffer.</param>
        /// <param name="value">Float value.</param>
        public void SetFloat(int position, float value)
        {
            byte[] bytes = BitConverter.GetBytes(value);

            SetBytes(position, bytes);
        }

        /// <summary>
        /// Get a certain byte from the buffer.
        /// </summary>
        /// <param name="position">Position to get.</param>
        /// <returns>Byte.</returns>
        public byte GetByte(int position)
        {
            return Buffer[position + HeaderLength];
        }

        /// <summary>
        /// Get the next byte from the buffer.
        /// </summary>
        /// <returns>Byte.</returns>
        public byte GetByte()
        {
            var value = Buffer[position + HeaderLength];

            position += ByteSize;

            return value;
        }

        /// <summary>
        /// Get the next bool from the buffer.
        /// </summary>
        /// <returns>Byte.</returns>
        public bool GetBool()
        {
            var value = Buffer[position + HeaderLength];

            position += BoolSize;

            return Convert.ToBoolean(value);
        }

        /// <summary>
        /// Get the next bools from the buffer.
        /// </summary>
        /// <returns>Long value.</returns>
        public bool[] GetBools()
        {
            int count = GetInt();

            var values = new bool[count];

            for (int i = 0; i < count; i++)
            {
                values[i] = GetBool();
            }

            return values;
        }

        /// <summary>
        /// Get a certain short from the buffer.
        /// </summary>
        /// <param name="position">Position to get.</param>
        /// <returns>Short.</returns>
        public short GetShort(int position)
        {
            return BitConverter.ToInt16(Buffer, position + HeaderLength);
        }

        /// <summary>
        /// Get the next short from the buffer.
        /// </summary>
        /// <returns>Short.</returns>
        public short GetShort()
        {
            var value = BitConverter.ToInt16(Buffer, position + HeaderLength);

            position += ShortSize;

            return value;
        }

        /// <summary>
        /// Get the next shorts from the buffer.
        /// </summary>
        /// <returns>Long value.</returns>
        public short[] GetShorts()
        {
            int count = GetInt();

            var values = new short[count];

            for (int i = 0; i < count; i++)
            {
                values[i] = GetShort();
            }

            return values;
        }

        /// <summary>
        /// Get a certain int from the buffer.
        /// </summary>
        /// <param name="position">Position to get.</param>
        /// <returns>Int.</returns>
        public int GetInt(int position)
        {
            return BitConverter.ToInt32(Buffer, position + HeaderLength);
        }

        /// <summary>
        /// Get the next int from the buffer.
        /// </summary>
        /// <returns>Int.</returns>
        public int GetInt()
        {
            var value = BitConverter.ToInt32(Buffer, position + HeaderLength);

            position += IntSize;

            return value;
        }

        /// <summary>
        /// Get the next ints from the buffer.
        /// </summary>
        /// <returns>Long value.</returns>
        public int[] GetInts()
        {
            int count = GetInt();

            var values = new int[count];

            for (int i = 0; i < count; i++)
            {
                values[i] = GetInt();
            }

            return values;
        }

        /// <summary>
        /// Get a certain float in the buffer.
        /// </summary>
        /// <param name="position">Position in the buffer.</param>
        /// <returns>Float value.</returns>
        public float GetFloat(int position)
        {
            return BitConverter.ToSingle(Buffer, position + HeaderLength);
        }

        /// <summary>
        /// Get the next float in the buffer.
        /// </summary>
        /// <returns>Float value.</returns>
        public float GetFloat()
        {
            var value = BitConverter.ToSingle(Buffer, position + HeaderLength);

            position += FloatSize;

            return value;
        }

        /// <summary>
        /// Get the next floats from the buffer.
        /// </summary>
        /// <returns>Float value.</returns>
        public float[] GetFloats()
        {
            int count = GetInt();

            var values = new float[count];

            for (int i = 0; i < count; i++)
            {
                values[i] = GetFloat();
            }

            return values;
        }

        /// <summary>
        /// Get a certain long in the buffer.
        /// </summary>
        /// <param name="position">Position in the buffer.</param>
        /// <returns>Float value.</returns>
        public long GetLong(int position)
        {
            return BitConverter.ToInt64(Buffer, position + HeaderLength);
        }

        /// <summary>
        /// Get the next long from the buffer.
        /// </summary>
        /// <returns>Long value.</returns>
        public long GetLong()
        {
            var value = BitConverter.ToInt64(Buffer, position + HeaderLength);

            position += LongSize;

            return value;
        }

        /// <summary>
        /// Get the next longs from the buffer.
        /// </summary>
        /// <returns>Long value.</returns>
        public long[] GetLongs()
        {
            int count = GetInt();

            var values = new long[count];

            for (int i = 0; i < count; i++)
            {
                values[i] = GetLong();
            }

            return values;
        }

        /// <summary>
        /// Get an enum.
        /// </summary>
        /// <typeparam name="T">Enum type/</typeparam>
        /// <returns>Enum value.</returns>
        /// <remarks>Note that enum are stored / read as int.</remarks>
        public T GetEnum<T>() where T : Enum
        {
            var tmp = GetInt();

            return (T)(object)tmp;
        }

        /// <summary>
        /// Get a date time.
        /// </summary>
        /// <returns>Date time.</returns>
        public DateTime GetDateTime()
        {
            var tmp = GetLong();

            return DateTime.FromBinary(tmp);
        }

        /// <summary>
        /// Get the next date times from the buffer.
        /// </summary>
        /// <returns>Long value.</returns>
        public DateTime[] GetDateTimes()
        {
            int count = GetInt();

            var values = new DateTime[count];

            for (int i = 0; i < count; i++)
            {
                values[i] = GetDateTime();
            }

            return values;
        }

        /// <summary>
        /// Get a time span.
        /// </summary>
        /// <returns>Time span.</returns>
        public TimeSpan GetTimeSpan()
        {
            var tmp = GetLong();

            return TimeSpan.FromTicks(tmp);
        }

        /// <summary>
        /// Get the next time span from the buffer.
        /// </summary>
        /// <returns>Long value.</returns>
        public TimeSpan[] GetTimeSpans()
        {
            int count = GetInt();

            var values = new TimeSpan[count];

            for (int i = 0; i < count; i++)
            {
                values[i] = GetTimeSpan();
            }

            return values;
        }

        /// <summary>
        /// Get bytes from the buffer.
        /// </summary>
        /// <param name="position">Position to start.</param>
        /// <param name="count">Count.</param>
        /// <returns>Array of bytes.</returns>
        public byte[] GetBytes(int position, int count)
        {
            byte[] bytes = new byte[count];

            for (int i = 0; i < count; i++)
            {
                bytes[i] = Buffer[position + HeaderLength + i];
            }

            return bytes;
        }

        /// <summary>
        /// Get bytes from the buffer.
        /// </summary>
        /// <param name="count">Count.</param>
        /// <returns>Array of bytes.</returns>
        public byte[] GetBytes(int count)
        {
            byte[] bytes = new byte[count];

            for (int i = 0; i < count; i++)
            {
                bytes[i] = Buffer[position + HeaderLength + i];
            }

            position += count;

            return bytes;
        }

        /// <summary>
        /// Get the next string from the buffer.
        /// </summary>
        /// <returns>String.</returns>
        public string GetString()
        {
            int length = GetInt();

            return GetCountedString(length);
        }

        /// <summary>
        /// Get counted string in the buffer.
        /// </summary>
        /// <param name="length">Length.</param>
        /// <returns>String.</returns>
        public string GetCountedString(int length)
        {
            return Encoding.UTF8.GetString(GetBytes(length));
        }

        /// <summary>
        /// Get the next strings from the buffer. Use it only if you used the Add method.
        /// </summary>
        /// <returns>String.</returns>
        public string[] GetStrings()
        {
            int count = GetInt();

            var values = new string[count];

            for (int i = 0; i < count; i++)
            {
                values[i] = GetString();
            }

            return values;
        }

        /// <summary>
        /// Get the next string from the butter but with a limit.
        /// </summary>
        /// <param name="limit">Limit.</param>
        /// <returns>String.</returns>
        public string GetString(int limit)
        {
            int length = GetInt();

            int maxBytes = Encoding.UTF8.GetMaxByteCount(limit);

            if (length > maxBytes)
            {
                length = maxBytes;
            }

            var bytes = GetBytes(length);

            var charCount = Encoding.UTF8.GetCharCount(bytes);

            if (charCount > limit)
            {
                return Encoding.UTF8.GetString(bytes).Substring(0, limit);
            }

            return Encoding.UTF8.GetString(bytes);
        }

        /// <summary>
        /// Get an object of T type.
        /// </summary>
        /// <typeparam name="T">T type.</typeparam>
        /// <returns>Object of T type.</returns>
        public T GetObject<T>()
        {
            var length = GetInt();

            var bytes = GetBytes(length);

            return bytes.Deserialize<T>();
        }

        /// <summary>
        /// Get the next objects from the buffer.
        /// </summary>
        /// <returns>Long value.</returns>
        public T[] GetObjects<T>()
        {
            int count = GetInt();

            var values = new T[count];

            for (int i = 0; i < count; i++)
            {
                values[i] = GetObject<T>();
            }

            return values;
        }

        /// <summary>
        /// Get a string from the buffer.
        /// </summary>
        /// <param name="position">Position to start.</param>
        /// <param name="count">Length of the string.</param>
        /// <returns>String.</returns>
        [Obsolete("This is a legacy method, please use the new one with internal index.")]
        public string GetCleanedString(int position, int count)
        {
            return Encoding.UTF8.GetString((GetBytes(position, count))).Split('\0')[0];
        }

        /// <summary>
        /// Get a string from the buffer.
        /// </summary>
        /// <param name="position">Position to start.</param>
        /// <param name="count">Length of the string.</param>
        /// <returns>String.</returns>
        [Obsolete("This is a legacy method, please use the new one with internal index.")]
        public string GetCleanedString(ref int position, int count)
        {
            var value = Encoding.UTF8.GetString((GetBytes(position, count))).Split('\0')[0];

            position += count;

            return value;
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
        /// Get or set the buffer of the packet.
        /// </summary>
        public byte[] Buffer
        {
            get { return segment.Array; }
            //  set { segment = new ArraySegment<byte>(value); }
        }

        /// <summary>
        /// Get or set the current position of the packet.
        /// </summary>
        public int Position
        {
            get { return position; }
            set { position = value; }
        }

        /// <summary>
        /// Get the size of the packet.
        /// </summary>
        public int Size
        {
            get { return BitConverter.ToInt16(Buffer, 0); }
        }

        /// <summary>
        /// Get the command of the packet.
        /// </summary>
        public short Command
        {
            get { return BitConverter.ToInt16(Buffer, SizeLength); }
        }

        /// <summary>
        /// Get the command of the packet.
        /// </summary>
        public string CommandString
        {
            get { return Command.ToString("X2"); }
        }

        /// <summary>
        /// Get the packet in string decimal format.
        /// </summary>
        public string StringFormat { get { return Tools.GetPacketString(this); } }
    }
}
