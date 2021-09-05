using GenericApp.Infra.CC.Localization.Resources;
using System;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text.Json;

namespace GenericApp.Infra.Common.Extensions
{
    public static class ObjectExtensions
    {
        public static bool IsNumeric(this object valor)
        {
            try
            {
                Convert.ToInt64(valor);

                return true;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// Converte o valor em Boolean (true ou false)
        /// </summary>
        /// <param name="_obj">Objeto a ser convertido</param>
        /// <returns>true ou false</returns>
        public static bool ToBoolean(this object _obj)
        {
            return Convert.ToBoolean(_obj);
        }
        /// <summary>
        /// Converte o valor em DateTime
        /// </summary>
        /// <param name="_obj">Objeto a ser convertido</param>
        /// <returns>Uma data valida ou DateTime.MinValue se não for data</returns>
        public static DateTime ToDateTime(this object _obj)
        {
            if (_obj == null)
            {
                return DateTime.MinValue;
            }
            else
            {
                return Convert.ToDateTime(_obj);
            }
        }
        /// <summary>
        /// Converte o valor em Decimal.
        /// </summary>
        /// <typeparam name="T">Object.</typeparam>
        /// <param name="_obj">Objeto a ser convertido.</param>
        /// <returns>Um valor válido ou Decimal.MinValue se houver erro.</returns>
        public static decimal ToDecimal(this object _obj)
        {
            if (_obj == null)
            {
                return 0;
            }
            else
            {
                return Convert.ToDecimal(_obj);
            }
        }
        /// <summary>
        /// Converte o valor em Double.
        /// </summary>
        /// <typeparam name="T">Object.</typeparam>
        /// <param name="_obj">Objeto a ser convertido.</param>
        /// <returns>Um valor válido ou Double.MinValue se houver erro.</returns>
        public static double ToDouble(this object _obj)
        {
            if (_obj == null)
            {
                return 0;
            }
            else
            {
                return Convert.ToDouble(_obj);
            }
        }
        /// <summary>
        /// Converte o valor em Byte.
        /// </summary>
        /// <typeparam name="T">Object.</typeparam>
        /// <param name="_obj">Objeto a ser convertido.</param>
        /// <returns>Um valor válido ou 0 se houver erro.</returns>
        public static byte ToByte(this object _obj)
        {
            if (_obj == null)
            {
                return 0;
            }
            else
            {
                return Convert.ToByte(_obj);
            }
        }
        public static byte? ToNByte(this object _obj)
        {
            if (_obj == null)
            {
                return null;
            }
            else
            {
                return Convert.ToByte(_obj);
            }
        }

        public static bool? ToNBoolean(this object _obj)
        {
            if (_obj == null ||
               !(_obj.ToString().Equals("0") ||
                _obj.ToString().Equals("1")))
            {
                return null;
            }
            else
            {
                return Convert.ToBoolean(_obj);
            }
        }

        public static byte ToByte16(this string _obj, int fromBase = 16)
        {
            if (_obj == null)
            {
                return 0;
            }
            else
            {
                return Convert.ToByte(_obj, fromBase);
            }
        }
        public static short ToInt16(this object _obj)
        {
            if (_obj == null)
            {
                return 0;
            }
            else
            {
                return Convert.ToInt16(_obj);
            }
        }

        public static ushort ToUInt16(this object _obj)
        {
            if (_obj == null)
            {
                return 0;
            }
            else
            {
                return Convert.ToUInt16(_obj);
            }
        }

        public static short? ToNInt16(this object _obj)
        {
            if (_obj == null)
            {
                return null;
            }
            else
            {
                return Convert.ToInt16(_obj);
            }
        }

        /// <summary>
        /// Converte o valor em Int32.
        /// </summary>
        /// <typeparam name="T">Object.</typeparam>
        /// <param name="_obj">Objeto a ser convertido.</param>
        /// <returns>Um valor válido ou Int32.MinValue se houver erro.</returns>
        public static int ToInt32(this object _obj)
        {
            if (_obj == null)
            {
                return 0;
            }
            else
            {
                return Convert.ToInt32(_obj);
            }
        }

        /// <summary>
        /// Converte o valor em Int32.
        /// </summary>
        /// <typeparam name="T">Object.</typeparam>
        /// <param name="_obj">Objeto a ser convertido.</param>
        /// <returns>Um valor válido ou Null se houver erro.</returns>
        public static int? ToNInt32(this object _obj)
        {
            if (_obj == null)
            {
                return null;
            }
            else
            {
                return Convert.ToInt32(_obj);
            }
        }

        /// <summary>
        /// Converte o valor em UInt32.
        /// </summary>
        /// <typeparam name="T">Object.</typeparam>
        /// <param name="_obj">Objeto a ser convertido.</param>
        /// <returns>Um valor válido ou UInt32.MinValue se houver erro.</returns>
        public static uint ToUInt32(this object _obj)
        {
            if (_obj == null)
            {
                return 0;
            }
            else
            {
                return Convert.ToUInt32(_obj);
            }
        }

        /// <summary>
        /// Converte o valor em Int64.
        /// </summary>
        /// <typeparam name="T">Object.</typeparam>
        /// <param name="_obj">Objeto a ser convertido.</param>
        /// <returns>Um valor válido ou Int64.MinValue se houver erro.</returns>
        public static long ToInt64(this object _obj)
        {
            if (_obj == null)
            {
                return 0;
            }
            else
            {
                return Convert.ToInt64(_obj);
            }
        }

        /// <summary>
        /// Converte o valor em Int64.
        /// </summary>
        /// <typeparam name="T">Object.</typeparam>
        /// <param name="_obj">Objeto a ser convertido.</param>
        /// <returns>Um valor válido ou Null se houver erro.</returns>
        public static long? ToNInt64(this object _obj)
        {
            if (_obj == null)
            {
                return null;
            }
            else
            {
                return Convert.ToInt64(_obj);
            }
        }

        /// <summary>
        /// Converte o valor em UInt64.
        /// </summary>
        /// <typeparam name="T">Object.</typeparam>
        /// <param name="_obj">Objeto a ser convertido.</param>
        /// <returns>Um valor válido ou 0 se houver erro.</returns>
        public static ulong ToUInt64(this object _obj)
        {
            if (_obj == null)
            {
                return 0;
            }
            else
            {
                return Convert.ToUInt64(_obj);
            }
        }


        /// <summary>
        /// Converte o valor em Single.
        /// </summary>
        /// <typeparam name="T">Object.</typeparam>
        /// <param name="_obj">Objeto a ser convertido.</param>
        /// <returns>Um valor válido ou Single.MinValue se houver erro.</returns>ne 
        public static char ToChar(this object _obj)
        {
            if (_obj == null)
            {
                return char.MinValue;
            }
            else
            {
                return Convert.ToChar(_obj);
            }
        }
        public static float ToSingle(this object _obj)
        {
            if (_obj == null)
            {
                return float.NaN;
            }
            else
            {
                return Convert.ToSingle(_obj);
            }
        }

        public static string Serialize(this object _obj)
        {
            if (_obj is null)
            {
                return string.Empty;
            }
            else
            {
                return JsonSerializer.Serialize(_obj);
            }
        }

        /// <summary>
        /// Perform a deep Copy of the object.
        /// </summary>
        /// <typeparam name="T">The type of object being copied.</typeparam>
        /// <param name="source">The object instance to copy.</param>
        /// <returns>The copied object.</returns>
        public static T Clone<T>(this T source)
        {
            if (!typeof(T).IsSerializable)
            {
                throw new ArgumentException(SharedResource.TypeMustBeSerializable, nameof(source));
            }

            // Don't serialize a null object, simply return the default for that object
            if (source is null)
            {
                return default;
            }

            using MemoryStream stream = new MemoryStream();
            IFormatter formatter = new BinaryFormatter();
            formatter.Serialize(stream, source);
            stream.Seek(0, SeekOrigin.Begin);

            return (T)formatter.Deserialize(stream);
        }
    }
}
