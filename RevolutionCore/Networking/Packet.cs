using RevolutionCore.Configurations;
using RevolutionCore.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RevolutionCore.Networking
{
    /// <summary>
    /// Rose Packet.
    /// </summary>
    public class Packet
    {
        /// <summary>
        /// Offset of the header.
        /// </summary>
        static public readonly int HeaderSize = 6;
        /// <summary>
        /// Size of the buffer.
        /// </summary>
        static public readonly int BufferSize = 0x400;
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

        int position;

        byte[] buffer;

        /// <summary>
        /// Parameterless constructor.
        /// </summary>
        public Packet()
        {
            buffer = new byte[BufferSize];
        }

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="buffer">Buffer.</param>
        public Packet(byte[] buffer)
        {
            this.buffer = buffer;
        }

        /// <summary>
        /// Set the size of the packet.
        /// </summary>
        /// <param name="size">New size.</param>
        public void SetSize(ushort size)
        {
            var bytes = BitConverter.GetBytes(size);

            buffer[0] = bytes[0];
            buffer[1] = bytes[1];
        }

        /// <summary>
        /// Set the size of the packet.
        /// </summary>
        /// <param name="size">New size.</param>
        public void SetSize(int size)
        {
            SetSize((ushort)size);
        }

        /// <summary>
        /// Set the command of the packet.
        /// </summary>
        /// <param name="command">Command.</param>
        public void SetCommand(ushort command)
        {
            var bytes = BitConverter.GetBytes(command);

            buffer[2] = bytes[0];
            buffer[3] = bytes[1];
        }

        /// <summary>
        /// Set the start of the packet.
        /// </summary>
        /// <param name="command">Command of the packet.</param>
        public void Start(ushort command)
        {
            SetSize(HeaderSize);
            SetCommand(command);
        }

        /// <summary>
        /// Set the start of the packet.
        /// </summary>
        /// <param name="size">Size of the packet.</param>
        /// <param name="command">Command of the packet.</param>
        public void Start(ushort size, ushort command)
        {
            SetSize(size);
            SetCommand(command);
        }

        /// <summary>
        /// Set a byte in the buffer.
        /// </summary>
        /// <param name="position">Position of the byte.</param>
        /// <param name="value">Value of the byte.</param>
        public void SetByte(int position, byte value)
        {
            buffer[position + HeaderSize] = value;
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
                buffer[position + HeaderSize + i] = values[i];
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
            return buffer[position + HeaderSize];
        }

        /// <summary>
        /// Get a certain byte from the buffer.
        /// </summary>
        /// <param name="position">Position to get.</param>
        /// <returns>Byte.</returns>
        public byte GetByte(ref int position)
        {
            var value = buffer[position + HeaderSize];

            position += 1;

            return value;
        }

        /// <summary>
        /// Get a certain short from the buffer.
        /// </summary>
        /// <param name="position">Position to get.</param>
        /// <returns>Short.</returns>
        public short GetShort(int position)
        {
            return BitConverter.ToInt16(buffer, position + HeaderSize);
        }

        /// <summary>
        /// Get a certain short from the buffer.
        /// </summary>
        /// <param name="position">Position to get.</param>
        /// <returns>Short.</returns>
        public short GetShort(ref int position)
        {
            var value = BitConverter.ToInt16(buffer, position + HeaderSize);
            position += 2;

            return value;
        }

        /// <summary>
        /// Get a certain int from the buffer.
        /// </summary>
        /// <param name="position">Position to get.</param>
        /// <returns>Int.</returns>
        public int GetInt(int position)
        {
            return BitConverter.ToInt32(buffer, position + HeaderSize);
        }

        /// <summary>
        /// Get a certain int from the buffer.
        /// </summary>
        /// <param name="position">Position to get.</param>
        /// <returns>Int.</returns>
        public int GetInt(ref int position)
        {
            var value = BitConverter.ToInt32(buffer, position + HeaderSize);

            position += 4;

            return value;
        }

        /// <summary>
        /// Get a certain float in the buffer.
        /// </summary>
        /// <param name="position">Position in the buffer.</param>
        /// <returns>Float value.</returns>
        public float GetFloat(int position)
        {
            return BitConverter.ToSingle(buffer, position + HeaderSize);
        }

        /// <summary>
        /// Get a certain float in the buffer.
        /// </summary>
        /// <param name="position">Position in the buffer.</param>
        /// <returns>Float value.</returns>
        public float GetFloat(ref int position)
        {
            var value = BitConverter.ToSingle(buffer, position + HeaderSize);

            position += 4;

            return value;
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
                bytes[i] = buffer[position + HeaderSize + i];
            }

            return bytes;
        }

        /// <summary>
        /// Get a string from the buffer.
        /// </summary>
        /// <param name="count">Length of the string.</param>
        /// <returns>String.</returns>
        public string GetString(int count)
        {
            int i;

            for (i = 0; i < count; i++)
            {
                if ((char)buffer[position + HeaderSize + i] == '\0') // End of string
                {
                    break;
                }
            }

            var value = Encoding.ASCII.GetString(GetBytes(i--));

            return value;
        }

        /// <summary>
        /// Skip bytes in the buffer packet.
        /// </summary>
        /// <param name="count">Count.</param>
        public void Skip(int count)
        {
            position += count;
        }

        /// <summary>
        /// Get a string from the buffer.
        /// </summary>
        /// <param name="count">Length of the string.</param>
        /// <returns>String.</returns>
        public string GetFixedString(int count)
        {
            var value = Encoding.ASCII.GetString(GetBytes(count));

            return value;
        }

        /// <summary>
        /// Get a string from the buffer.
        /// </summary>
        /// <param name="position">Position to start.</param>
        /// <param name="count">Length of the string.</param>
        /// <returns>String.</returns>
        public string CleanString(int count)
        {
            return Encoding.ASCII.GetString((GetBytes(count))).Split('\0')[0];
        }

        /// <summary>
        /// Add a byte in the buffer.
        /// </summary>
        /// <param name="value">Byte value.</param>
        public void Add(byte value)
        {
            buffer[Size] = value;

            SetSize((ushort)(Size + 1));
        }

        /// <summary>
        /// Add a short in the buffer.
        /// </summary>
        /// <param name="value">Short value.</param>
        public void Add(short value)
        {
            byte[] bytes = BitConverter.GetBytes(value);

            for (int i = 0; i < 2; i++)
            {
                buffer[Size + i] = bytes[i];
            }

            SetSize((ushort)(Size + 2));
        }

        /// <summary>
        /// Add a short in the buffer.
        /// </summary>
        /// <param name="value">Short value.</param>
        public void Add(ushort value)
        {
            byte[] bytes = BitConverter.GetBytes(value);

            for (int i = 0; i < 2; i++)
            {
                buffer[Size + i] = bytes[i];
            }

            SetSize((ushort)(Size + 2));
        }

        /// <summary>
        /// Add an int in the buffer.
        /// </summary>
        /// <param name="value">Short value.</param>
        public void Add(int value)
        {
            byte[] bytes = BitConverter.GetBytes(value);

            for (int i = 0; i < 4; i++)
            {
                buffer[Size + i] = bytes[i];
            }

            SetSize((ushort)(Size + 4));
        }

        /// <summary>
        /// Add an int in the buffer.
        /// </summary>
        /// <param name="value">Short value.</param>
        public void Add(uint value)
        {
            byte[] bytes = BitConverter.GetBytes(value);

            for (int i = 0; i < 4; i++)
            {
                buffer[Size + i] = bytes[i];
            }

            SetSize((ushort)(Size + 4));
        }

        /// <summary>
        /// Add a string in the buffer.
        /// </summary>
        /// <param name="value">Short value.</param>
        public void Add(string value)
        {
            byte[] bytes = Encoding.ASCII.GetBytes(value);

            for (int i = 0; i < bytes.Length; i++)
            {
                buffer[Size + i] = bytes[i];
            }

            SetSize((ushort)(Size + bytes.Length));

            Add((byte)0);
        }

        /// <summary>
        /// Add a string in the buffer.
        /// </summary>
        /// <param name="value">Short value.</param>
        /// <param name="maximum">Maximum string length.</param>
        public void Add(string value, int maximum)
        {
            byte[] bytes = Encoding.ASCII.GetBytes(value);

            for (int i = 0; i < bytes.Length; i++)
            {
                buffer[Size + i] = bytes[i];
            }

            SetSize((ushort)(Size + bytes.Length));

            Add((byte)0);

            for (int i = 0; i < maximum - value.Length; i++)
            {
                Add((byte)0);
            }
        }

        /// <summary>
        /// Add a float in the buffer.
        /// </summary>
        /// <param name="value">Float to add.</param>
        public void Add(float value)
        {
            byte[] bytes = BitConverter.GetBytes(value);

            for (int i = 0; i < 4; i++)
            {
                buffer[Size + i] = bytes[i];
            }

            SetSize((ushort)(Size + 4));
        }

        /// <summary>
        /// Add a long in the buffer.
        /// </summary>
        /// <param name="value">Long to add.</param>
        public void Add(long value)
        {
            byte[] bytes = BitConverter.GetBytes(value);

            for (int i = 0; i < 8; i++)
            {
                buffer[Size + i] = bytes[i];
            }

            SetSize((ushort)(Size + 8));
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
        /// Display the packet.
        /// </summary>
        public override string ToString()
        {
            return Tools.DisplayPacket(this);
        }

        /// <summary>
        /// Display the packet.
        /// </summary>
        public void DisplayASCII()
        {
            var dataStr = Encoding.ASCII.GetString(buffer);

            Logger.LogMessage(Configuration.Verbose, "PACKET-IN", dataStr);
        }

        /// <summary>
        /// Get or set the buffer of the packet.
        /// </summary>
        public byte[] Buffer
        {
            get { return buffer; }
            set { buffer = value; }
        }

        /// <summary>
        /// Get the size of the packet.
        /// </summary>
        public ushort Size
        {
            get { return BitConverter.ToUInt16(buffer, 0); }
        }

        /// <summary>
        /// Get the command of the packet.
        /// </summary>
        public ushort Command
        {
            get { return BitConverter.ToUInt16(buffer, 2); }
        }

        /// <summary>
        /// Get the checksum of the packet.
        /// </summary>
        public ushort Checksum
        {
            get { return BitConverter.ToUInt16(buffer, 4); }
        }

        /// <summary>
        /// Get the command of the packet.
        /// </summary>
        public string CommandString
        {
            get { return Command.ToString("X2"); }
        }
    }
}
