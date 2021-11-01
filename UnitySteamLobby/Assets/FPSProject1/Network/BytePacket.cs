using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

namespace RB.Network
{
    public class BytePacket : IDisposable
    {
        private List<byte> buffer;
        private byte[] readableBuffer;
        private int readPos;

        public BytePacket()
        {
            buffer = new List<byte>(); // Initialize buffer
            readPos = 0; // Set readPos to 0
        }

        public BytePacket(int id)
        {
            buffer = new List<byte>(); // Initialize buffer
            readPos = 0; // Set readPos to 0

            Write(id); // Write packet id to the buffer
        }

        public BytePacket(byte[] data)
        {
            buffer = new List<byte>(); // Initialize buffer
            readPos = 0; // Set readPos to 0

            SetBytes(data);
        }

        public void SetBytes(byte[] _data)
        {
            Write(_data);
            readableBuffer = buffer.ToArray();
        }

        public void WriteLength()
        {
            buffer.InsertRange(0, BitConverter.GetBytes(buffer.Count)); // Insert the byte length of the packet at the very beginning
        }

        public void InsertInt(int _value)
        {
            buffer.InsertRange(0, BitConverter.GetBytes(_value)); // Insert the int at the start of the buffer
        }

        public byte[] ToArray()
        {
            readableBuffer = buffer.ToArray();
            return readableBuffer;
        }

        public int Length()
        {
            return buffer.Count; // Return the length of buffer
        }

        public int UnreadLength()
        {
            return Length() - readPos; // Return the remaining length (unread)
        }

        public void Write(byte _value)
        {
            buffer.Add(_value);
        }

        public void Write(byte[] _value)
        {
            buffer.AddRange(_value);
        }

        public void Write(short _value)
        {
            buffer.AddRange(BitConverter.GetBytes(_value));
        }

        public void Write(int _value)
        {
            buffer.AddRange(BitConverter.GetBytes(_value));
        }

        public void Write(uint _value)
        {
            buffer.AddRange(BitConverter.GetBytes(_value));
        }

        public void Write(long _value)
        {
            buffer.AddRange(BitConverter.GetBytes(_value));
        }

        public void Write(float _value)
        {
            buffer.AddRange(BitConverter.GetBytes(_value));
        }

        public void Write(ulong _value)
        {
            buffer.AddRange(BitConverter.GetBytes(_value));
        }

        public void Write(bool _value)
        {
            buffer.AddRange(BitConverter.GetBytes(_value));
        }

        public void Write(string _value)
        {
            Write(_value.Length); // Add the length of the string to the packet
            buffer.AddRange(Encoding.ASCII.GetBytes(_value)); // Add the string itself
        }

        public void Write(Vector3 _value)
        {
            Write(_value.x);
            Write(_value.y);
            Write(_value.z);
        }

        public void Write(Quaternion _value)
        {
            Write(_value.x);
            Write(_value.y);
            Write(_value.z);
            Write(_value.w);
        }

        public byte ReadByte(bool _moveReadPos = true)
        {
            if (buffer.Count > readPos)
            {
                // If there are unread bytes
                byte _value = readableBuffer[readPos]; // Get the byte at readPos' position
                if (_moveReadPos)
                {
                    // If _moveReadPos is true
                    readPos += 1; // Increase readPos by 1
                }
                return _value; // Return the byte
            }
            else
            {
                throw new Exception("Could not read value of type 'byte'!");
            }
        }

        public byte[] ReadBytes(int _length, bool _moveReadPos = true)
        {
            if (buffer.Count > readPos)
            {
                // If there are unread bytes
                byte[] _value = buffer.GetRange(readPos, _length).ToArray(); // Get the bytes at readPos' position with a range of _length
                if (_moveReadPos)
                {
                    // If _moveReadPos is true
                    readPos += _length; // Increase readPos by _length
                }
                return _value; // Return the bytes
            }
            else
            {
                throw new Exception("Could not read value of type 'byte[]'!");
            }
        }

        public short ReadShort(bool _moveReadPos = true)
        {
            if (buffer.Count > readPos)
            {
                // If there are unread bytes
                short _value = BitConverter.ToInt16(readableBuffer, readPos); // Convert the bytes to a short
                if (_moveReadPos)
                {
                    // If _moveReadPos is true and there are unread bytes
                    readPos += 2; // Increase readPos by 2
                }
                return _value; // Return the short
            }
            else
            {
                throw new Exception("Could not read value of type 'short'!");
            }
        }

        public int ReadInt(bool _moveReadPos = true)
        {
            if (buffer.Count > readPos)
            {
                // If there are unread bytes
                int _value = BitConverter.ToInt32(readableBuffer, readPos); // Convert the bytes to an int
                if (_moveReadPos)
                {
                    // If _moveReadPos is true
                    readPos += 4; // Increase readPos by 4
                }
                return _value; // Return the int
            }
            else
            {
                throw new Exception("Could not read value of type 'int'!");
            }
        }

        public uint ReadUInt(bool _moveReadPos = true)
        {
            if (buffer.Count > readPos)
            {
                uint _value = BitConverter.ToUInt32(readableBuffer, readPos);
                if (_moveReadPos)
                {
                    readPos += 4;
                }
                return _value;
            }
            else
            {
                throw new Exception("Could not read value of type 'uint'!");
            }
        }

        public long ReadLong(bool _moveReadPos = true)
        {
            if (buffer.Count > readPos)
            {
                // If there are unread bytes
                long _value = BitConverter.ToInt64(readableBuffer, readPos); // Convert the bytes to a long
                if (_moveReadPos)
                {
                    // If _moveReadPos is true
                    readPos += 8; // Increase readPos by 8
                }
                return _value; // Return the long
            }
            else
            {
                throw new Exception("Could not read value of type 'long'!");
            }
        }

        public ulong ReadULong(bool _moveReadPos = true)
        {
            if (buffer.Count > readPos)
            {
                ulong value = BitConverter.ToUInt64(readableBuffer, readPos); // Convert the bytes to a long
                if (_moveReadPos)
                {
                    readPos += 8;
                }
                return value;
            }
            else
            {
                throw new Exception("Could not read value of type 'long'!");
            }
        }

        public float ReadFloat(bool _moveReadPos = true)
        {
            if (buffer.Count > readPos)
            {
                // If there are unread bytes
                float _value = BitConverter.ToSingle(readableBuffer, readPos); // Convert the bytes to a float
                if (_moveReadPos)
                {
                    // If _moveReadPos is true
                    readPos += 4; // Increase readPos by 4
                }
                return _value; // Return the float
            }
            else
            {
                throw new Exception("Could not read value of type 'float'!");
            }
        }

        public bool ReadBool(bool _moveReadPos = true)
        {
            if (buffer.Count > readPos)
            {
                // If there are unread bytes
                bool _value = BitConverter.ToBoolean(readableBuffer, readPos); // Convert the bytes to a bool
                if (_moveReadPos)
                {
                    // If _moveReadPos is true
                    readPos += 1; // Increase readPos by 1
                }
                return _value; // Return the bool
            }
            else
            {
                throw new Exception("Could not read value of type 'bool'!");
            }
        }

        public string ReadString(bool _moveReadPos = true)
        {
            try
            {
                int _length = ReadInt(); // Get the length of the string
                string _value = Encoding.ASCII.GetString(readableBuffer, readPos, _length); // Convert the bytes to a string
                if (_moveReadPos && _value.Length > 0)
                {
                    // If _moveReadPos is true string is not empty
                    readPos += _length; // Increase readPos by the length of the string
                }
                return _value; // Return the string
            }
            catch
            {
                throw new Exception("Could not read value of type 'string'!");
            }
        }

        public Vector3 ReadVector3(bool _moveReadPos = true)
        {
            return new Vector3(ReadFloat(_moveReadPos), ReadFloat(_moveReadPos), ReadFloat(_moveReadPos));
        }

        public Quaternion ReadQuaternion(bool _moveReadPos = true)
        {
            return new Quaternion(ReadFloat(_moveReadPos), ReadFloat(_moveReadPos), ReadFloat(_moveReadPos), ReadFloat(_moveReadPos));
        }

        private bool disposed = false;

        protected virtual void Dispose(bool _disposing)
        {
            if (!disposed)
            {
                if (_disposing)
                {
                    buffer = null;
                    readableBuffer = null;
                    readPos = 0;
                }

                disposed = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}